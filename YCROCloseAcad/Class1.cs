using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AdeskRun = Autodesk.AutoCAD.Runtime;
using AdeskWin = Autodesk.AutoCAD.Windows;
using AdeskGeo = Autodesk.AutoCAD.Geometry;
using AdeskEdIn = Autodesk.AutoCAD.EditorInput;
using AdeskGra = Autodesk.AutoCAD.GraphicsInterface;
using AdeskDBSvr = Autodesk.AutoCAD.DatabaseServices;
using AdeskAppSvr = Autodesk.AutoCAD.ApplicationServices;
using AdeskInter = Autodesk.AutoCAD.Interop;
using System.Windows.Forms;
using System.Threading;

namespace YCROCloseAcad
{
    public class Class1 : AdeskRun.IExtensionApplication
    {
        public void Initialize()
        {
            if ( (MessageBox.Show("Òª¹Ø±ÕcadÂð£¿", "ÇëÑ¡Ôñ ", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == System.Windows.Forms.DialogResult.Yes)
            {
                //Process.Start();
                //Thread update = new Thread(new ThreadStart(MessageBox.Show("Done!")));
                update.Start();
                Thread.Sleep(3000);
                closeapp();
            }
        }

        public void Terminate()
        {

        }

        [AdeskRun.CommandMethod("de")]
        public void closeapp()
        {
            string progID = "AutoCAD.Application";
            AdeskInter.AcadApplication CADAPP = null;
            try
            {
                CADAPP = (AdeskInter.AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject(progID);
                CADAPP.Quit();
            }
            catch
            {
                try
                {
                    Type SType = Type.GetTypeFromProgID(progID);
                    CADAPP = (AdeskInter.AcadApplication)System.Activator.CreateInstance(SType, true);
                    CADAPP.Quit();
                }
                catch
                {
                    
                }
            }
        }

    }
}
