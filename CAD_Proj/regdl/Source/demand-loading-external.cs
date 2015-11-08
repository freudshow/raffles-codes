// (C) Copyright 2009-2012 by Autodesk, Inc. 
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to 
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System;
using Microsoft.Win32;

namespace DemandLoading
{
  public class RegistryUpdate
  {
    public static bool RegisterForDemandLoading(
      Assembly assem, bool currentUser, bool startup, bool force
    )
    {
      // Get the assembly, its name and location

      string name = assem.GetName().Name;
      string path = assem.Location;

      // We'll collect information on the commands
      // (we could have used a map or a more complex
      // container for the global and localized names
      // - the assumption is we will have an equal
      // number of each with possibly fewer groups)

      List<string> globCmds = new List<string>();
      List<string> locCmds = new List<string>();
      List<string> groups = new List<string>();

      // Iterate through the modules in the assembly

      Module[] mods = assem.GetModules(true);
      foreach (Module mod in mods)
      {
        // Within each module, iterate through the types

        Type[] types = mod.GetTypes();

        foreach (Type type in types)
        {
          // We may need to get a type's resources

          ResourceManager rm =
            new ResourceManager(type.FullName, assem);
          rm.IgnoreCase = true;

          // Get each method on a type

          MethodInfo[] meths = type.GetMethods();
          foreach (MethodInfo meth in meths)
          {
            // Get the method's custom attribute(s)

            IList<CustomAttributeData> attbs =
              CustomAttributeData.GetCustomAttributes(meth);

            foreach (CustomAttributeData attb in attbs)
            {
              // We only care about our specific attribute type

              if (attb.Constructor.DeclaringType.Name ==
                  "CommandMethodAttribute")
              {
                // Harvest the information about each command

                string grpName = "";;
                string globName = "";
                string locName = "";
                string lid = "";

                // Our processing will depend on the number of
                // parameters passed into the constructor

                int paramCount = attb.ConstructorArguments.Count;
                
                if (paramCount == 1 || paramCount == 2)
                {
                  // Constructor options here are:

                  //  globName (1 argument)
                  //  grpName, globName (2 args)

                  globName =
                    attb.ConstructorArguments[0].ToString();
                  locName = globName;
                }
                else if (paramCount >= 3)
                {
                  // Constructor options here are:

                  //  grpName, globName, flags (3 args)
                  //  grpName, globName, locNameId, flags (4 args)
                  //  grpName, globName, locNameId, flags,
                  //    hlpTopic (5 args)
                  //  grpName, globName, locNameId, flags,
                  //    contextMenuType (5 args)
                  //  grpName, globName, locNameId, flags,
                  //    contextMenuType, hlpFile, helpTpic (7 args)

                  CustomAttributeTypedArgument arg0, arg1;
                  arg0 = attb.ConstructorArguments[0];
                  arg1 = attb.ConstructorArguments[1];

                  // All options start with grpName, globName

                  grpName = arg0.Value as string;
                  globName = arg1.Value as string;
                  locName = globName;

                  // If we have a localized command ID,
                  // let's look it up in our resources

                  if (paramCount >= 4)
                  {
                    // Get the localized string ID

                    lid = attb.ConstructorArguments[2].ToString();

                    // Strip off the enclosing quotation marks

                    if (lid != null && lid.Length > 2)
                      lid = lid.Substring(1, lid.Length - 2);

                    // Let's put a try-catch block around this
                    // Failure just means we use the global
                    // name twice (the default)

                    if (lid != null && lid != "")
                    {
                      try
                      {
                        locName = rm.GetString(lid);
                      }
                      catch
                      { }
                    }
                  }
                }


                if (globName != null)
                {
                  // Add the information to our data structures

                  globCmds.Add(globName);
                  locCmds.Add(locName);

                  if (grpName != null && !groups.Contains(grpName))
                    groups.Add(grpName);
                }
              }
            }
          }
        }
      }

      // Let's register the application to load on AutoCAD
      // startup (2) if specified or if it contains no
      // commands. Otherwise we will have it load on
      // command invocation (12)

      int flags = (!startup && globCmds.Count > 0 ? 12 : 2);

      // Now create our Registry keys

      return CreateDemandLoadingEntries(
        name, path, globCmds, locCmds, groups,
        flags, currentUser, force
      );
    }

    public static bool UnregisterForDemandLoading(Assembly assem)
    {
      // Get the name of the application to unregister

      string appName = assem.GetName().Name;
      
      // Unregister it for both HKCU and HKLM

      bool res = RemoveDemandLoadingEntries(appName, true);
      res &= RemoveDemandLoadingEntries(appName, false);
      
      // If one call failed, we also fail (could change this)

      return res;
    }

    // Helper functions

    private static bool CreateDemandLoadingEntries(
      string appName,
      string path,
      List<string> globCmds,
      List<string> locCmds,
      List<string> groups,
      int flags,
      bool currentUser,
      bool force
    )
    {
      string ackName = GetAutoCADKey();
      RegistryKey hive =
        (currentUser ? Registry.CurrentUser : Registry.LocalMachine);

      // We may need to create the Applications key, as some AutoCAD
      // verticals do not contain it under HKCU by default
      
      // CreateSubKey just opens existing keys for write, anyway

      RegistryKey appk =
        hive.CreateSubKey(ackName + "\\" + "Applications");
      using (appk)
      {
        // Already registered? Just return (unless forcing)

        if (!force)
        {
          string[] subKeys = appk.GetSubKeyNames();
          foreach (string subKey in subKeys)
          {
            if (subKey.Equals(appName))
            {
              return false;
            }
          }
        }

        // Create the our application's root key and its values

        RegistryKey rk = appk.CreateSubKey(appName);
        using (rk)
        {
          rk.SetValue(
            "DESCRIPTION", appName, RegistryValueKind.String
          );
          rk.SetValue("LOADCTRLS", flags, RegistryValueKind.DWord);
          rk.SetValue("LOADER", path, RegistryValueKind.String);
          rk.SetValue("MANAGED", 1, RegistryValueKind.DWord);

          // Create a subkey if there are any commands...

          if ((globCmds.Count == locCmds.Count) &&
               globCmds.Count > 0)
          {
            RegistryKey ck = rk.CreateSubKey("Commands");
            using (ck)
            {
              for (int i = 0; i < globCmds.Count; i++)
                ck.SetValue(
                  globCmds[i],
                  locCmds[i],
                  RegistryValueKind.String
                );
            }
          }

          // And the command groups, if there are any

          if (groups.Count > 0)
          {
            RegistryKey gk = rk.CreateSubKey("Groups");
            using (gk)
            {
              foreach (string grpName in groups)
                gk.SetValue(
                  grpName, grpName, RegistryValueKind.String
                );
            }
          }
        }
      }
      return true;
    }

    private static bool RemoveDemandLoadingEntries(
      string appName, bool currentUser
    )
    {
      try
      {
        string ackName = GetAutoCADKey();

        // Choose a Registry hive based on the function input

        RegistryKey hive =
          (currentUser ?
            Registry.CurrentUser :
            Registry.LocalMachine);

        // Open the applications key

        RegistryKey appk =
          hive.OpenSubKey(ackName + "\\" + "Applications", true);
        using (appk)
        {
          // Delete the key with the same name as this assembly

          appk.DeleteSubKeyTree(appName);
        }
      }
      catch
      {
        return false;
      }
      return true;
    }

    private static string GetAutoCADKey()
    {
      // Start by getting the CurrentUser location

      RegistryKey hive = Registry.CurrentUser;

      // Open the main AutoCAD key

      RegistryKey ack =
        hive.OpenSubKey(
          "Software\\Autodesk\\AutoCAD"
        );
      using (ack)
      {
        // Get the current major version and its key

        string ver = ack.GetValue("CurVer") as string;
        if (ver == null)
        {
          return "";
        }
        else
        {
          RegistryKey verk = ack.OpenSubKey(ver);
          using (verk)
          {
            // Get the vertical/language version and its key

            string lng = verk.GetValue("CurVer") as string;
            if (lng == null)
            {
              return "";
            }
            else
            {
              RegistryKey lngk = verk.OpenSubKey(lng);
              using (lngk)
              {
                // And finally return the path to the key,
                // without the hive prefix

                return lngk.Name.Substring(hive.Name.Length + 1);
              }
            }
          }
        }
      }
    }
  }
}