using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Data.OracleClient;

namespace DetailInfo
{
    public partial class DetailsForm : Form
    {
        public DetailsForm()
        {
            InitializeComponent();
            this.CertificateContextMenuStrip.Items[0].Visible = false;
        }

        public string spoolstr;
        public string SPOOLSTR
        {
            get { return spoolstr; }
            set { spoolstr = value; }
        }
        ArrayList streamList = new ArrayList();
        private void DetailsForm_Load(object sender, EventArgs e)
        {
            if (spoolstr != string.Empty)
            {
                string[] spoolname = spoolstr.Split(new char[] { ',' });

                //for (int i = 0; i < spoolname.Length-1; i++)
                for (int i = 0; i < DelArraySame(spoolname).Length -1; i++)
                {
                    //SpoolComboBox.Items.Add(spoolname[i]);
                    SpoolComboBox.Items.Add(DelArraySame(spoolname)[i]);
                }
            }
            this.SpoolComboBox.Text = this.SpoolComboBox.Items[0].ToString();
            //SpoolComboBox.SelectedIndex = 0;
            GetRevision();
        }

        private string[] DelArraySame(string[] TempArray)
        {
            ArrayList nStr = new ArrayList();
            for (int i = 0; i < TempArray.Length; i++)
            {
                if (!nStr.Contains(TempArray[i]))
                {
                    nStr.Add(TempArray[i]);
                }
            }
            string[] newStr = (string[])nStr.ToArray(typeof(string));
            return newStr;
        }


        /// <summary>
        /// combobox变换操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpoolComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRevision();
            RightClick.CellEvent_TabHop(sysidtb, sysnametb, gradetb, pressuretb, drawtb, blocktb, linetb, modifynotb,tabControl1, SpoolComboBox, MaterialDgv, ConnectorDgv, MachineDgv, WeldDgv, RelativePositonDgv, axAcroPDF1, axVIA3DXMLPlugin1, axAcroPDF2, axVIA3DXMLPlugin2);
            if (this.tabControl1.SelectedTab.Text == "三维模型" || this.tabControl1.SelectedTab.Text == "三维ISO模型")
            {
                MDIForm.treeview.Refresh();
            }

        }

        /// <summary>
        /// tabcontrol标签跳转操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RightClick.CellEvent_TabHop(sysidtb, sysnametb, gradetb, pressuretb, drawtb, blocktb, linetb, modifynotb,tabControl1, SpoolComboBox, MaterialDgv, ConnectorDgv, MachineDgv, WeldDgv, RelativePositonDgv, axAcroPDF1, axVIA3DXMLPlugin1, axAcroPDF2, axVIA3DXMLPlugin2);
            RightClick.ProjectDrawingVersion_TabHop(sysidtb, sysnametb, gradetb, pressuretb, drawtb, blocktb, linetb, modifynotb, tabControl1, SpoolComboBox, RevisonComboBox, MaterialDgv, ConnectorDgv, MachineDgv, WeldDgv, RelativePositonDgv, axAcroPDF1, axVIA3DXMLPlugin1, axAcroPDF2, axVIA3DXMLPlugin2);
            if (this.tabControl1.SelectedTab.Text == "三维模型" || this.tabControl1.SelectedTab.Text == "三维ISO模型" )
            {
                MDIForm.treeview.Refresh();
            }

        }

        /// <summary>
        /// 关闭窗口删除pdf或cgr文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[1].Enabled = false;
            MDIForm.treeview.Refresh();
            GC.Collect();
            try
            {
                string root = User.rootpath + "\\" + "temp";
                DirectoryInfo dir = new DirectoryInfo(root);
                if (dir.Exists)
                {
                    System.IO.Directory.Delete(root, true);
                }
                else
                {
                    return;
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }

        }

        /// <summary>
        /// 查看证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CertificateView_Click(object sender, EventArgs e)
        {
            int selcount = this.MaterialDgv.SelectedRows.Count;
            if( selcount == 1 )
            {
                int index = this.MaterialDgv.CurrentRow.Index;
                string filestr = this.MaterialDgv.Rows[index].Cells["证书号"].Value.ToString();
                if (filestr != string.Empty)
                {
                    string spoolname = MaterialDgv.CurrentRow.Cells["小票号"].Value.ToString();
                    QCAttachmentView qcav = new QCAttachmentView();
                    qcav.namestr = spoolname;
                    qcav.ShowDialog();
                }
                else
                {
                    MessageBox.Show("暂没有相关证书！", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            else
            {
                //MessageBox.Show("请确定！", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }

        private void DetailsForm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
            MDIForm.tool_strip.Items[2].Enabled = true;
        }

        [System.Runtime.InteropServices.DllImport("ole32.dll")]
        static extern void CoFreeUnusedLibraries();

        /// <summary>
        /// 关闭窗体释放tabcontrol占用的所有资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.axAcroPDF1 != null)
            {
                axAcroPDF1.Dispose();
                System.Windows.Forms.Application.DoEvents();
                CoFreeUnusedLibraries();
            }
            if (this.axAcroPDF2 != null)
            {
                axAcroPDF2.Dispose();
                System.Windows.Forms.Application.DoEvents();
                CoFreeUnusedLibraries();
            }
            if (this.axVIA3DXMLPlugin1 != null)
            {
                this.axVIA3DXMLPlugin1.Dispose();
                System.Windows.Forms.Application.DoEvents();
                CoFreeUnusedLibraries();
            }
            if (this.axVIA3DXMLPlugin2 != null)
            {
                this.axVIA3DXMLPlugin2.Dispose();
                System.Windows.Forms.Application.DoEvents();
                CoFreeUnusedLibraries();
            }
            //this.axVIA3DXMLPlugin1.Dispose();
            //this.axVIA3DXMLPlugin2.Dispose();

            //if (this.WindowState == FormWindowState.Maximized)
            //{
            //    this.WindowState = FormWindowState.Normal;

            //    foreach (Form frm in MDIForm.pMainWin.MdiChildren)
            //    {
            //        if (frm.Equals(this) == false)
            //        {
            //            frm.WindowState = FormWindowState.Maximized;
            //        }
            //    }
            //}

            //GC.Collect();
        }

        private void MaterialDgv_MouseUp(object sender, MouseEventArgs e)
        {
            int count = this.MaterialDgv.Rows.Count;
            
            if (count> 0 )
            {
                int selectcount = this.MaterialDgv.SelectedRows.Count;
                if (selectcount == 1)
                {
                    this.CertificateContextMenuStrip.Items[0].Visible = true;
                }
                else
                {
                    this.CertificateContextMenuStrip.Items[0].Visible = false;
                }
            }
        }

        private void GetRevision()
        {
            this.RevisonComboBox.Items.Clear();
            if (this.RevisonComboBox.Text.Length != 0)
            {
                this.RevisonComboBox.Text.Remove(0);
            }
            string cbtxt = this.SpoolComboBox.Text.ToString();
            string sqlstr = "select distinct t.revision from sp_spool_tab t where t.spoolname = '" + cbtxt + "'";
            DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.RevisonComboBox, sqlstr);
            //this.RevisonComboBox.Text = this.RevisonComboBox.Items[0].ToString();
            this.RevisonComboBox.SelectedIndex = 0;
        }

        private void RevisonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RightClick.ProjectDrawingVersion_TabHop(sysidtb, sysnametb, gradetb, pressuretb, drawtb, blocktb, linetb, modifynotb, tabControl1, SpoolComboBox,RevisonComboBox, MaterialDgv, ConnectorDgv, MachineDgv, WeldDgv, RelativePositonDgv, axAcroPDF1, axVIA3DXMLPlugin1, axAcroPDF2, axVIA3DXMLPlugin2);
        }

    }
}