//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using Aveva.PDMS.PMLNet;

namespace Aveva.Pdms.Examples
{
    [PMLNetCallable()]
    public class PMLNetExample
    {
        [PMLNetCallable()]
        public event PMLNetDelegate.PMLNetEventHandler PMLNetExampleEvent;

        [PMLNetCallable()]
        public PMLNetExample()
        {
        }

        [PMLNetCallable()]
        public void Assign(PMLNetExample that)
        {
            //No state
        }

        [PMLNetCallable()]
        public void RaiseExampleEvent()
        {
            ArrayList args = new ArrayList();
            args.Add("ExampleEvent");
            if (PMLNetExampleEvent != null)
                PMLNetExampleEvent(args);
        }

        
        [PMLNetCallable()]
        public void Method()
        {
            MessageBox.Show("Called Method");
        }
    }
}
