//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;

using Aveva.Pdms.Database;
using Aveva.Pdms.Utilities.Messaging;
using Aveva.PDMS.Database.Filters;
using Aveva.Pdms.Geometry;

namespace Aveva.Pdms.Examples
{
    public class ExampleMDB
    {
        public static void Run()
        {
            //First world
            DbElement el = MDB.CurrentMDB.GetFirstWorld(DbType.Design);
            string firstWorldName = el.GetString(DbAttributeInstance.NAME);

            //Get name
            string name = MDB.CurrentMDB.Name;

            //Save work
            Example.Instance.mEqui.InsertAfterLast(Example.Instance.mZone);
            MDB.CurrentMDB.SaveWork("Moved /ExampleEqui to be the last element in parent zone.");
            MDB.CurrentMDB.Close();

            //Quit work
            Example.Instance.mEqui.InsertAfterLast(Example.Instance.mZone);
            MDB.CurrentMDB.QuitWork();		
        }
    }
}