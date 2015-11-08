using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Runtime;

namespace Lab6
{
    /// <summary>
    /// Summary description for ModalForm.
    /// </summary>
    public class ModalForm : System.Windows.Forms.Form
    {
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button SelectEmployeeButton;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox tb_Division;
        internal System.Windows.Forms.TextBox tb_Salary;
        internal System.Windows.Forms.TextBox tb_Name;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ModalForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button2 = new System.Windows.Forms.Button();
            this.SelectEmployeeButton = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.tb_Division = new System.Windows.Forms.TextBox();
            this.tb_Salary = new System.Windows.Forms.TextBox();
            this.tb_Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(200, 104);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(111, 34);
            this.Button2.TabIndex = 30;
            this.Button2.Text = "Close";
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // SelectEmployeeButton
            // 
            this.SelectEmployeeButton.Location = new System.Drawing.Point(200, 49);
            this.SelectEmployeeButton.Name = "SelectEmployeeButton";
            this.SelectEmployeeButton.Size = new System.Drawing.Size(111, 34);
            this.SelectEmployeeButton.TabIndex = 29;
            this.SelectEmployeeButton.Text = "Select Employee";
            this.SelectEmployeeButton.Click += new System.EventHandler(this.SelectEmployeeButton_Click);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(27, 69);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(153, 18);
            this.Label3.TabIndex = 28;
            this.Label3.Text = "Division";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(27, 122);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(153, 17);
            this.Label2.TabIndex = 27;
            this.Label2.Text = "Salary";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(27, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(153, 17);
            this.Label1.TabIndex = 26;
            this.Label1.Text = "Name";
            // 
            // tb_Division
            // 
            this.tb_Division.Location = new System.Drawing.Point(27, 90);
            this.tb_Division.Name = "tb_Division";
            this.tb_Division.Size = new System.Drawing.Size(153, 20);
            this.tb_Division.TabIndex = 25;
            // 
            // tb_Salary
            // 
            this.tb_Salary.Location = new System.Drawing.Point(27, 139);
            this.tb_Salary.Name = "tb_Salary";
            this.tb_Salary.Size = new System.Drawing.Size(153, 20);
            this.tb_Salary.TabIndex = 24;
            // 
            // tb_Name
            // 
            this.tb_Name.Location = new System.Drawing.Point(27, 35);
            this.tb_Name.Name = "tb_Name";
            this.tb_Name.Size = new System.Drawing.Size(153, 20);
            this.tb_Name.TabIndex = 23;
            // 
            // ModalForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(329, 192);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.SelectEmployeeButton);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.tb_Division);
            this.Controls.Add(this.tb_Salary);
            this.Controls.Add(this.tb_Name);
            this.Name = "ModalForm";
            this.Text = "ModalForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void SelectEmployeeButton_Click(object sender, System.EventArgs e)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            this.Hide();
            try
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    PromptEntityOptions prEnt = new PromptEntityOptions("Select an Employee");
                    PromptEntityResult prEntRes = ed.GetEntity(prEnt);
                    if (prEntRes.Status != PromptStatus.OK)
                        throw new System.Exception("Error or User Cancelled");

                    ArrayList saEmployeeList = new ArrayList();

                    AsdkClass1.ListEmployee(prEntRes.ObjectId, saEmployeeList);
                    if (saEmployeeList.Count == 4)
                    {
                        tb_Name.Text = saEmployeeList[0].ToString();
                        tb_Salary.Text = saEmployeeList[1].ToString();
                        tb_Division.Text = saEmployeeList[2].ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error Creating Employee: " + ex.Message);
            }
            finally
            {
                this.Show();
            }
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        [CommandMethod("MODALFORM")]
        public void ShowModalForm()
        {
            ModalForm modalForm = new ModalForm();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(modalForm);
        }
    }
}