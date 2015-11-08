using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using System.Runtime.InteropServices;

namespace YCROAppUp
{
    public partial class YCROAPPForm : Form
    {
        public YCROAPPForm()
        {
            InitializeComponent();
        }

        private void AUTOCAD2006ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string progID = "AutoCAD.Application";
            Autodesk.AutoCAD.Interop.AcadApplication CADAPP = null;
            try
            {
                CADAPP = (Autodesk.AutoCAD.Interop.AcadApplication)Marshal.GetActiveObject(progID);
            }
            catch
            {
                try
                {
                    Type SType = Type.GetTypeFromProgID(progID);
                    CADAPP = (Autodesk.AutoCAD.Interop.AcadApplication)Activator.CreateInstance(SType, true);
                    CADAPP.Visible = true;
                }
                catch
                {
                    this.Close();
                }
            }
            if (CADAPP != null)
            {
                CADAPP.Visible = true;
                CADAPP.ActiveDocument.SendCommand("filedia\r0\r");
                CADAPP.ActiveDocument.SendCommand("netload\r" + Application.StartupPath + "\\MYCAD.dll" + "\r");
                CADAPP.ActiveDocument.SendCommand("filedia\r1\r");
                this.Close();
            }
        }
    }
}