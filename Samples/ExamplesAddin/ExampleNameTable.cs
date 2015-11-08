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
    public class ExampleNameTable
    {
        public static void Run()
        {
            Db[] dbs = MDB.CurrentMDB.GetDBArray(DbType.Design);
            foreach (Db db in dbs)
            {
                NameTable ntable = NameTable.GetNameTable(db, DbAttributeInstance.NAME, "/Example", "");
                foreach (DbElement e in ntable)
                {
                    string elementName = e.GetString(DbAttributeInstance.NAME);
                }
            }
        }
    }
}