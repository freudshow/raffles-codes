//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Aveva.Pdms.Shared;
using Aveva.Pdms.Database;
using Aveva.Pdms.Utilities.Messaging;
using Aveva.PDMS.Database.Filters;
using Aveva.Pdms.Geometry;

namespace Aveva.Pdms.Examples
{
    public class ExampleCE
    {
        //follow current element changes
        public static void Run()
        {
            //add event handler for CE changes
            CurrentElement.CurrentElementChanged += new CurrentElementChangedEventHandler(CurrentElement_CurrentElementChanged);

            //change CE
            CurrentElement.Element = Example.Instance.mWorld;
            CurrentElement.Element = Example.Instance.mEqui;
        }

        static void CurrentElement_CurrentElementChanged(object sender, CurrentElementChangedEventArgs e)
        {
            Console.WriteLine(e.Element);
        }
    }
}