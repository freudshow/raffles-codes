using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


using Autodesk.AutoCAD.ApplicationServices;

namespace Lab6
{
    /// <summary>
    /// Summary description for EmployeeOptions.
    /// </summary>
    public class EmployeeOptions : System.Windows.Forms.UserControl
    {
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox tb_DivisionManager;
        internal System.Windows.Forms.TextBox tb_EmployeeDivision;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public EmployeeOptions()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call

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
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.tb_DivisionManager = new System.Windows.Forms.TextBox();
            this.tb_EmployeeDivision = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(16, 73);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(136, 16);
            this.Label2.TabIndex = 15;
            this.Label2.Text = "Default Division Manager";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(16, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(136, 16);
            this.Label1.TabIndex = 14;
            this.Label1.Text = "Default Employee Division";
            // 
            // tb_DivisionManager
            // 
            this.tb_DivisionManager.Location = new System.Drawing.Point(16, 89);
            this.tb_DivisionManager.Name = "tb_DivisionManager";
            this.tb_DivisionManager.Size = new System.Drawing.Size(184, 22);
            this.tb_DivisionManager.TabIndex = 13;
            this.tb_DivisionManager.Text = AsdkClass1.sDivisionManager;
            // 
            // tb_EmployeeDivision
            // 
            this.tb_EmployeeDivision.Location = new System.Drawing.Point(16, 41);
            this.tb_EmployeeDivision.Name = "tb_EmployeeDivision";
            this.tb_EmployeeDivision.Size = new System.Drawing.Size(184, 22);
            this.tb_EmployeeDivision.TabIndex = 12;
            this.tb_EmployeeDivision.Text = AsdkClass1.sDivisionDefault;
            // 
            // EmployeeOptions
            // 
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.Label2,
																		  this.Label1,
																		  this.tb_DivisionManager,
																		  this.tb_EmployeeDivision});
            this.Name = "EmployeeOptions";
            this.Size = new System.Drawing.Size(216, 136);
            this.Load += new System.EventHandler(this.EmployeeOptions_Load);
            this.ResumeLayout(false);

        }
        #endregion

        public void OnOk()
        {
            AsdkClass1.sDivisionDefault = tb_EmployeeDivision.Text;
            AsdkClass1.sDivisionManager = tb_DivisionManager.Text;
        }

        public static void AddTabDialog()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.DisplayingOptionDialog += new TabbedDialogEventHandler(TabHandler);
        }

        public static void RemoveTabDialog()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.DisplayingOptionDialog -= new TabbedDialogEventHandler(TabHandler);
        }

        private static void TabHandler(object sender, Autodesk.AutoCAD.ApplicationServices.TabbedDialogEventArgs e)
        {
            EmployeeOptions EmployeeOptionsPage = new EmployeeOptions();
            e.AddTab("Acme Employee Options",
                new TabbedDialogExtension(
                EmployeeOptionsPage,
                new TabbedDialogAction(EmployeeOptionsPage.OnOk)));
        }
        private void EmployeeOptions_Load(object sender, System.EventArgs e)
        {

        }
    }
}