//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;

using Aveva.Pdms.Database;
using Aveva.Pdms.Utilities.Messaging;
using Aveva.PDMS.Database.Filters;
using Aveva.Pdms.Geometry;
using Aveva.Pdms.Shared;

namespace Aveva.Pdms.Examples
{
    public class ExampleUserChanges
    {
        public static void Run()
        {
            //add database changes event handler
            DatabaseService.Changes += new DbChangesEventHandler(DatabaseService_Changes);

            //
            // Element Creation, Deletion, Re-order, Include, Copy
            //
            DbElement nozz1 = Example.Instance.mEqui.CreateFirst(DbElementTypeInstance.NOZZLE);
            Pdms.Utilities.CommandLine.Command.Update();

            DbElement nozz2 = Example.Instance.mEqui.CreateLast(DbElementTypeInstance.NOZZLE);
            Pdms.Utilities.CommandLine.Command.Update();

            nozz1.Copy(Example.Instance.mNozz1); // Copy element
            nozz1.SetAttribute(DbAttributeInstance.DESC, "nozzle"); // Change only one part
            Pdms.Utilities.CommandLine.Command.Update(); // Make sure these changes are ignored
            nozz1.Copy(Example.Instance.mNozz1); // Re-Copy element - should indicate only the description part has changed
            Pdms.Utilities.CommandLine.Command.Update();

            nozz1.InsertAfterLast(Example.Instance.mEqui);
            Pdms.Utilities.CommandLine.Command.Update();

            nozz1.Delete();
            Pdms.Utilities.CommandLine.Command.Update();
        }

        static void DatabaseService_Changes(object sender, DbChangesEventArgs e)
        {
            try
            {
                DbElement[] creations = e.ChangeList.GetCreations();
                DbElement[] deletions = e.ChangeList.GetDeletions();
                DbElement[] memberChanges = e.ChangeList.GetMemberChanges();
                DbElement[] modifications = e.ChangeList.GetModified();
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}