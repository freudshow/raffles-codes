using System;
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using AdeskRun = Autodesk.AutoCAD.Runtime;
using AdeskWin = Autodesk.AutoCAD.Windows;
using AdeskGeo = Autodesk.AutoCAD.Geometry;
using AdeskEdIn = Autodesk.AutoCAD.EditorInput;
using AdeskGra = Autodesk.AutoCAD.GraphicsInterface;
using AdeskDBSvr = Autodesk.AutoCAD.DatabaseServices;
using AdeskAppSvr = Autodesk.AutoCAD.ApplicationServices;
using AdeskInter = Autodesk.AutoCAD.Interop;

using System.Diagnostics;
using System.Threading;

namespace GenDwg
{
    public partial class Form_GenDwg : Form
    {
        public Form_GenDwg()
        {
            InitializeComponent();
        }

        private void btn_Gen_Click(object sender, EventArgs e)
        {
            try
            {
                throw new FileNotFoundException("文件没有被找到");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}