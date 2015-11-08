using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Runtime;

namespace Lab6
{
    /// <summary>
    /// Summary description for ModelessForm.
    /// </summary>
    public class ModelessForm : System.Windows.Forms.UserControl
    {
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox tb_Salary;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox tb_Division;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox tb_Name;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ModelessForm()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
            Label4.MouseMove += new System.Windows.Forms.MouseEventHandler(Label4_MouseMove);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.tb_Salary = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.tb_Division = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.tb_Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.Label5.Location = new System.Drawing.Point(16, 24);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(160, 23);
            this.Label5.TabIndex = 31;
            this.Label5.Text = "Employee Details";
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(32, 280);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(136, 16);
            this.Label4.TabIndex = 30;
            this.Label4.Text = "Drag to Create Employee";
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(16, 208);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(128, 16);
            this.Label3.TabIndex = 29;
            this.Label3.Text = "Salary";
            // 
            // tb_Salary
            // 
            this.tb_Salary.Location = new System.Drawing.Point(16, 224);
            this.tb_Salary.Name = "tb_Salary";
            this.tb_Salary.Size = new System.Drawing.Size(184, 22);
            this.tb_Salary.TabIndex = 28;
            this.tb_Salary.Text = "42000";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(16, 136);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(128, 16);
            this.Label2.TabIndex = 27;
            this.Label2.Text = "Division";
            // 
            // tb_Division
            // 
            this.tb_Division.Location = new System.Drawing.Point(16, 152);
            this.tb_Division.Name = "tb_Division";
            this.tb_Division.Size = new System.Drawing.Size(184, 22);
            this.tb_Division.TabIndex = 26;
            this.tb_Division.Text = "Sales";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(16, 64);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(128, 16);
            this.Label1.TabIndex = 25;
            this.Label1.Text = "Name";
            // 
            // tb_Name
            // 
            this.tb_Name.Location = new System.Drawing.Point(16, 80);
            this.tb_Name.Name = "tb_Name";
            this.tb_Name.Size = new System.Drawing.Size(184, 22);
            this.tb_Name.TabIndex = 24;
            this.tb_Name.Text = "Delton T. Cransley";
            // 
            // ModelessForm
            // 
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.Label5,
																		  this.Label4,
																		  this.Label3,
																		  this.tb_Salary,
																		  this.Label2,
																		  this.tb_Division,
																		  this.Label1,
																		  this.tb_Name});
            this.Name = "ModelessForm";
            this.Size = new System.Drawing.Size(212, 352);
            this.Load += new System.EventHandler(this.ModelessForm_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private void ModelessForm_Load(object sender, System.EventArgs e)
        {

        }

        private void Label4_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (System.Windows.Forms.Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {
                // start dragDrop operation, MyDropTarget will be called when the cursor enters the AutoCAD view area.
                Autodesk.AutoCAD.ApplicationServices.Application.DoDragDrop(this, this, System.Windows.Forms.DragDropEffects.All, new MyDropTarget());
            }
        }

        private Autodesk.AutoCAD.Windows.PaletteSet ps;

        [CommandMethod("Palette")]
        public void testPalette()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (ps == null)
                {
                    // use constructor with Guid so that we can save/load user data
                    ps = new Autodesk.AutoCAD.Windows.PaletteSet("Test Palette Set"); // New Guid("63B8DB5B-10E4-4924-B8A2-A9CF9158E4F6"))
                    ps.Style = PaletteSetStyles.ShowTabForSingle;
                    //ps.Style = 16; //PaletteSetStyles.NameEditable;
                    //ps.Style = 4; //PaletteSetStyles.ShowPropertiesMenu;
                    //ps.Style = 2; //PaletteSetStyles.ShowAutoHideButton;
                    //ps.Style = 8; //PaletteSetStyles.ShowCloseButton
                    ps.Opacity = 90;
                    ps.MinimumSize = new System.Drawing.Size(300, 300);
                    System.Windows.Forms.UserControl myCtrl = new ModelessForm();
                    //ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
                    ps.Add("test", myCtrl);
                    ps.Visible = true;
                }
            }
            catch
            {
                ed.WriteMessage("Error Showing Palette");
            }
        }
    } // class ModelessForm

    public class MyDropTarget : Autodesk.AutoCAD.Windows.DropTarget
    {
        override public void OnDrop(System.Windows.Forms.DragEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Point3d pt = ed.PointToWorld(new Point(e.X, e.Y));

                using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    ModelessForm ctrl = (ModelessForm)e.Data.GetData(typeof(ModelessForm));
                    AsdkClass1.CreateDivision(ctrl.tb_Division.Text, AsdkClass1.sDivisionManager);
                    AsdkClass1.CreateEmployee(ctrl.tb_Name.Text, ctrl.tb_Division.Text, Convert.ToDouble(ctrl.tb_Salary.Text), pt);
                }
            }
            catch
            {
                ed.WriteMessage("Error handling OnDrop");
            }
        }
    } // class MyDropTarget
}