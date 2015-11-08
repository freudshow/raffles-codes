//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Aveva.Pdms.Database;
using Aveva.Pdms.Utilities.Messaging;
using Aveva.PDMS.Database.Filters;
using Aveva.Pdms.Geometry;
using ATT = Aveva.Pdms.Database.DbAttributeInstance;
using NOUN = Aveva.Pdms.Database.DbElementTypeInstance;
using Ps = Aveva.Pdms.Database.DbPseudoAttribute;

namespace Aveva.Pdms.Examples
{
    public class ExamplePseudoAttribute
    {
        public static void Run()
        {
            // get uda attribute
            DbAttribute uda = DbAttribute.GetDbAttribute(":VOLUME");

			if (uda != null)
			{
				// Create instance of delegate containing "VolumeCalculation" method
				Ps.GetDoubleDelegate dele = new Ps.GetDoubleDelegate(VolumeCalculation);
				// Pass delegate instance to core PDMS. This will be invoked later
				Ps.AddGetDoubleAttribute(uda, NOUN.BOX, dele);

				// Now get value on a box
				double vol = Example.Instance.mBox.GetDouble(uda);
				Console.WriteLine(vol);
			}
        }

        // Double delegate for UDA
        static private double VolumeCalculation(DbElement ele, DbAttribute att, DbQualifier qualifier)
        {
            // Uda calculates the volume by multiplying the lengths along each side
            double x = ele.GetDouble(ATT.XLEN);
            double y = ele.GetDouble(ATT.YLEN);
            double z = ele.GetDouble(ATT.ZLEN);
            // Result of UDA must be returned
            return (x * y * z);
        }
    }
}
