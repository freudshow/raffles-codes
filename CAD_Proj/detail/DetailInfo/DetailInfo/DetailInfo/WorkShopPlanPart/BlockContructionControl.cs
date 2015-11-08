using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class BlockContructionControl : UserControl
    {
        private WorkShopPlan form;
        public BlockContructionControl(WorkShopPlan frm)
        {
            form = frm;
        }

        public BlockContructionControl()
        {
            InitializeComponent();
        }


    }
}
