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
    public class ExampleFilters
    {
        public static void Run()
        {
            //FirstMember
            TypeFilter filt = new TypeFilter(DbElementTypeInstance.NOZZLE);
            DbElement nozz1 = filt.FirstMember(Example.Instance.mEqui);
            DbElementType type = nozz1.GetElementType();

            //NextMember
            DbElement nozz2 = filt.Next(nozz1);

            //NextMember
            DbElement[] members = filt.Members(Example.Instance.mEqui);

            //parent
            TypeFilter filt2 = new TypeFilter(DbElementTypeInstance.SITE);
            DbElement site = filt2.Parent(nozz1);
            type = site.GetElementType();

            //Valid
            AndFilter andFilt4 = new AndFilter();
            andFilt4.Add(new TypeFilter(DbElementTypeInstance.EQUIPMENT));
            andFilt4.Add(new TrueFilter());
            bool valid = andFilt4.Valid(Example.Instance.mEqui);

            // attribute true
            AttributeTrueFilter filt1 = new AttributeTrueFilter(DbAttributeInstance.ISNAME);
            //should be true for named element
            bool named = filt1.Valid(Example.Instance.mEqui);
            //for first member it is also true
            named = filt1.Valid(Example.Instance.mEqui.FirstMember());

            //Element type at or below
            BelowOrAtType filt3 = new BelowOrAtType(DbElementTypeInstance.EQUIPMENT);
            bool below = filt3.ScanBelow(Example.Instance.mEqui);
        }
    }
}