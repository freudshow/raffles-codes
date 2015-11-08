// (C) Copyright 2010 by Autodesk, Inc. 
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

using System.Reflection;
using System.IO;
using System;
using DemandLoading;

namespace RegDL
{
  class Program
  {
    static void Main(string[] args)
    {
      // We need at least one argument (the assembly name)
      // or the request for help

      if (args.Length <= 0 || args.Length == 1 && (args[0] == "/?" || args[0].ToLower() == "/help"))
      {
        PrintUsage();
        return;
      }

      // Get the first argument and check the file exists

      string asmName = args[0];
      if (!File.Exists(asmName))
      {
        Console.WriteLine("RegDL : Unable to locate input assembly '{0}'.", asmName);
        return;
      }

      // Now we get the optional flags

      bool startup = false;
      bool hklm = false;
      bool unreg = false;
      bool force = false;

      for (int i=1; i < args.Length; i++)
      {
        string arg = args[i].ToLower();
        startup |= (arg == "/startup");
        hklm |= (arg == "/hklm");
        unreg |= (arg == "/unregister");
        force |= (arg == "/force");
      }

      // As each dependent assembly is resolved, we need to make
      // sure it is loaded via Reflection-Only, not a full Load

      AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += delegate(object sender, ResolveEventArgs rea)
                                                                        {
                                                                          return Assembly.ReflectionOnlyLoad(rea.Name);
                                                                        };
      
      // Let's load our assembly via Reflection-Only

      Assembly assem = Assembly.ReflectionOnlyLoadFrom(asmName);

      bool res = false;

      if (unreg)
      {
        // Unregister the assembly, if possible

        try
        {
          res = RegistryUpdate.UnregisterForDemandLoading(assem);
        }
        catch(Exception ex)
        {
          Console.WriteLine("Exception: {0}", ex);
        }

        if (res)
        {
          Console.WriteLine(
            "Removed demand-loading information for '{0}'.",
            asmName
          );
        }
        else
        {
          Console.WriteLine(
            "Could not remove demand-loading information for '{0}'.",
            asmName
          );
        }
      }
      else
      {
        // Register the assembly, if possible

        try
        {
          res =
            RegistryUpdate.RegisterForDemandLoading(
              assem, !hklm, startup, force
          );
        }
        catch(Exception ex)
        {
          if (ex is ReflectionTypeLoadException)
          {
            Console.WriteLine(
              "Trouble loading types: do you have AcMgd.dll and " +
              "AcDbMgd.dll in the same folder as RegDL.exe?"
            );
          }
          else
          {
            Console.WriteLine("Exception: {0}", ex);
          }
        }

        if (res)
        {
          Console.WriteLine(
            "Registered assembly '{0}' for AutoCAD demand-loading.",
            asmName
          );
        }
        else
        {
          Console.WriteLine(
            "Could not register '{0}' for AutoCAD demand-loading.",
            asmName
          );
        }
      }
    }

    // Print a usage message

    static void PrintUsage()
    {
      const string indent = "    ";
      const string start = "Version=";

      string version =
        Assembly.GetExecutingAssembly().FullName;
      
      if (version.Contains(start))
      {
        // Get the string starting with the version number

        version =
          version.Substring(
            version.IndexOf(start) + start.Length
          );

        // Strip off anything after (and including) the comma

        version =
          version.Remove(version.IndexOf(','));
      }
      else
        version = "";

      Console.WriteLine(
         "AutoCAD .NET Assembly Demand-Loading Registration " +
         "Utility {0}", version
       );
      Console.WriteLine(
        "Written by Kean Walmsley, Autodesk."
      );
      Console.WriteLine(
        "http://blogs.autodesk.com/through-the-interface"
      );
      Console.WriteLine();
      Console.WriteLine("Syntax: RegDL AssemblyName [Options]");
      Console.WriteLine("Options:");
      Console.WriteLine(
        indent +
        "/unregister  Remove demand-loading keys for this assembly"
      );
      Console.WriteLine(
        indent +
        "/hklm        Write keys under HKLM rather than HKCU"
      );
      Console.WriteLine(
        indent +
        "/startup     Assembly to be loaded on AutoCAD startup"
      );
      Console.WriteLine(
        indent +
        "/force       Overwrite keys, should they already exist"
      );
      Console.WriteLine(
        indent +
        "/? or /help  Display this usage message"
      );
    }
  }
}