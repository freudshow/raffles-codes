using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cutting.Lib.Geometry2D;
using System.Diagnostics;

namespace Cutting
{
    public partial class Form_cutting : Form
    {
        public Form_cutting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Vector2D vec1 = new Vector2D();
                Vector2D vec2 = new Vector2D();
                Point2D center = new Point2D(2003.4945, 1103.6469);
                Point2D start = new Point2D(2494.6048, 982.6129);
                Point2D end = new Point2D(1499.1729, 1142.3536);
                Debug.WriteLine("distance between p1 p2 is : " + center.GetDistanceTo(start).ToString());
                Debug.WriteLine("distance between p2 p3 is : " + end.GetDistanceTo(start).ToString());
                Arc2D arc1 = new Arc2D(center, start, end);
                vec1 = center.GetVector2DTo(start);
                vec2 = center.GetVector2DTo(end);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private string GetExt(string filename)
        {
            if (filename.StartsWith("."))
            {
                throw new Exception("file must has filename");
            }
            int dotPos = filename.LastIndexOf('.');
            string ext = filename.Substring(dotPos);
            return ext;
        }
    }
}
