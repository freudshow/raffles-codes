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
    public class ExampleTreeNavigator
    {
        public static void Run()
        {
            // Set up a navigator which looks at sites/pipes/Nozzles/Tee only
            TypeFilter filt = new TypeFilter();
            filt.Add(DbElementTypeInstance.SITE);
            filt.Add(DbElementTypeInstance.PIPE);
            filt.Add(DbElementTypeInstance.NOZZLE);
            filt.Add(DbElementTypeInstance.TEE);
            CompoundFilter filt2 = new CompoundFilter();
            filt2.AddShow(filt);
            ElementTreeNavigator navi = new ElementTreeNavigator(DbElement.GetElement("/*"), filt2);

            // Test FirstMember
            DbElement site = navi.FirstMemberInScan(Example.Instance.mWorld);
            DbElement nozz = navi.FirstMemberInScan(Example.Instance.mZone);
            nozz = navi.FirstMemberInScan(Example.Instance.mEqui);
            DbElement ele = navi.FirstMemberInScan(nozz);

            // Next
            DbElement zone = site.FirstMember();
            DbElement next = navi.NextInScan(zone);

            // parent
            DbElement parent = navi.Parent(Example.Instance.mEqui);
            parent = navi.Parent(nozz);
            parent = navi.Parent(parent);

            //All Members
            DbElement[] tees = navi.MembersInScan(Example.Instance.mPipe);
        }
    }
}