using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DetailInfo
{
    class LabelColor:Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.ForeColor = Color.Red;
        }
    }
}
