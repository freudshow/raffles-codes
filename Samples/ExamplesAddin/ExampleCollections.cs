//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Aveva.Pdms.Database;
using Aveva.Pdms.Utilities.Messaging;
using Aveva.PDMS.Database.Filters;
using Aveva.Pdms.Geometry;

namespace Aveva.Pdms.Examples
{
    public class ExampleCollections
    {
        //Create and iterate through collections
        public static void Run()
        {
            //Scan Nozzles below equi
            TypeFilter filt = new TypeFilter(DbElementTypeInstance.NOZZLE);
            DBElementCollection collection = new DBElementCollection(Example.Instance.mEqui, filt);
            DbAttribute att = DbAttributeInstance.FLNN;
            foreach (DbElement ele in collection)
            {
                Console.WriteLine(ele.GetAsString(att));
            }

            //Scan branches below site
            DBElementCollection coll = new DBElementCollection(Example.Instance.mSite);
            coll.IncludeRoot = true;
            coll.Filter = new TypeFilter(DbElementTypeInstance.BRANCH);
            DBElementEnumerator iter = (DBElementEnumerator)coll.GetEnumerator();
            while (iter.MoveNext())
            {
                Console.WriteLine(iter.Current.ToString());
            }
        }
    }
}
