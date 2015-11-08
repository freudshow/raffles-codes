using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Framework;
using System.IO;
namespace DetailInfo
{
    public partial class M_Estimate : Form
    {
        private string ProjectId = string.Empty, mSite = string.Empty, DisciplineId = string.Empty;
        public M_Estimate()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private DataSet getconnect(string filepath, string sheetname)
        {
            //创建一个数据链接 
            string strCon = " Provider = Microsoft.ACE.OLEDB.12.0 ; Data Source = " + filepath + ";Extended Properties=Excel 12.0";
            OleDbConnection myConn = new OleDbConnection(strCon);
            string strCom = " SELECT * FROM [" + sheetname + "$]";
            myConn.Open();
            //打开数据链接，得到一个数据集 
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
            //创建一个 DataSet对象 
            DataSet myDataSet = new DataSet();
            //得到自己的DataSet对象 
            myCommand.Fill(myDataSet, "[" + sheetname + "$]");

            return myDataSet;
            //dataGridView1.DataSource = myDataSet.Tables["Sheet1$"].DefaultView;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "xls files|*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string aimfile = openFileDialog1.FileName.ToString();
                //MessageBox.Show(aimfile);
                //textBox1.Text = aimfile;
                //getconnect(aimfile, "Sheet1");
                dgv2.DataMember = "[Sheet1$]";
                dgv2.DataSource = getconnect(aimfile, "Sheet1");
                DataSet DS_MEO = getconnect(aimfile, "Sheet1");
                DataTable MEOView = DS_MEO.Tables[0];
                int rowcou = MEOView.Rows.Count;
                //MessageBox.Show( MEOView.Rows[5][2].ToString());
                //MessageBox.Show( MEOView.Rows[5][3].ToString());
                //MessageBox.Show( MEOView.Rows[5][4].ToString());
                //MessageBox.Show( MEOView.Rows[5][5].ToString());
                //dgv2.Columns[0].ReadOnly = true;
                //dgv2.Columns[1].ReadOnly = true;
                //dgv2.Columns[2].ReadOnly = true;
                //dgv2.Columns[3].ReadOnly = true;
                //dgv2.Columns[4].ReadOnly = true;
                //dgv2.Columns[5].ReadOnly = true;
                //dgv2.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dgv2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dgv2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dgv2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dgv2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dgv2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //读取excel 文件
            }
            else
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("1.首先检查要导入的Excel数据中ERP代码是否在ERP系统中存在；\n2.将该项目该专业的原先的预估数据历史保存，然后将新数据写入数据库；\n3.完成导入；","实现步骤说明");
            mSite = cmb_site.SelectedValue.ToString();
            DisciplineId = cmb_dpname.SelectedValue.ToString();
            ProjectCmbItem item = (ProjectCmbItem)cmb_project.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择项目", "提示");
                return;
            }
            if (mSite == string.Empty)
            {
                MessageBox.Show("请选择域");
                return;
            }
            if (DisciplineId == string.Empty)
            {
                MessageBox.Show("请选择专业");
                return;
            }
            
            ProjectId = item.Value;
            if (dgv2.Rows.Count > 0)
            {
                
                CheckImportDataHanlder CheckData = new CheckImportDataHanlder(CheckImportData);
                CheckData.BeginInvoke(null, null);
                
                
            }

            
        }
        public delegate void CheckImportDataHanlder();
        public delegate void ImportDataHanlder();
        public void CheckImportData()
        {
            
            #region 首先检查要导入的Excel数据中ERP代码是否在ERP系统中存在
            string txtfilename = System.Windows.Forms.Application.StartupPath + "\\错误信息.txt";
            StreamWriter sw = new StreamWriter(txtfilename, false, Encoding.GetEncoding("gb2312"));
            int flag = 0;
            try
            {
                for (int i = 0; i < dgv2.Rows.Count - 1; i++)
                {
                    string partno = dgv2.Rows[i].Cells[4].Value.ToString();
                    if (!string.IsNullOrEmpty(partno))
                    {
                        InventoryPart invpartnew = InventoryPart.FindInvInfor(partno, mSite);
                        if (invpartnew == null)
                        {
                            System.Diagnostics.Trace.WriteLine("第" + (i + 1) + "行材料编码 " + partno + " 在ERP中所选择域无此材料编码，请联系ERP操作员，在ERP中添加。");
                            sw.WriteLine("第" + (i + 1) + "行材料编码 " + partno + " 在ERP中所选择域无此材料编码，请联系ERP操作员，在ERP中添加。", "温馨提示");
                            flag = 1;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine("第" + (i + 1) + "行材料编码 " + partno + " 在没有填写，在ERP中添加活查询后填写。");
                        sw.WriteLine("第" + (i + 1) + "行材料编码 " + partno + " 在没有填写，在ERP中添加活查询后填写。", "温馨提示");
                        flag = 1;
                    }
                    if (!string.IsNullOrEmpty(dgv2.Rows[i].Cells[6].Value.ToString()))
                    {
                        string rqty = dgv2.Rows[i].Cells[6].Value.ToString();
                        if (BaseClass.validateNum(rqty) == false)
                        {
                            System.Diagnostics.Trace.WriteLine("请确认第" + (i + 1) + "行" + partno + " 预估数量输入数字!!!");
                            sw.WriteLine("请确认第" + (i + 1) + "行" + partno + " 预估数量输入数字!!!", "温馨提示");
                            flag = 1;
                        }

                    }
                    if (!string.IsNullOrEmpty(dgv2.Rows[i].Cells[7].Value.ToString()))
                    {
                        string rqty = dgv2.Rows[i].Cells[7].Value.ToString();
                        if (BaseClass.validateNum(rqty) == false)
                        {
                            System.Diagnostics.Trace.WriteLine("请确认第" + (i + 1) + "行 " + partno + " 第一批生产需求数量输入数字!!!");
                            sw.WriteLine("请确认第" + (i + 1) + "行 " + partno + " 第一批生产需求数量输入数字!!!", "温馨提示");
                            flag = 1;
                        }

                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine("请确认第" + (i + 1) + "行 " + partno + " 第一批生产需求数量没有输入数字!!!");
                        sw.WriteLine("请确认第" + (i + 1) + "行 " + partno + " 第一批生产需求数量没有输入数字!!!", "温馨提示");
                        flag = 1;
                    }
                    if (!string.IsNullOrEmpty(dgv2.Rows[i].Cells[9].Value.ToString()))
                    {
                        string rqty = dgv2.Rows[i].Cells[9].Value.ToString();
                        if (BaseClass.validateNum(rqty) == false)
                        {
                            System.Diagnostics.Trace.WriteLine("请确认第" + (i + 1) + "行 " + partno + " 第一批生产需求数量输入数字!!!");
                            sw.WriteLine("请确认第" + (i + 1) + "行 " + partno + " 第一批生产需求数量输入数字!!!", "温馨提示");
                            flag = 1;
                        }

                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine("请确认第" + (i + 1) + "行 " + partno + " 第二批生产需求数量没有输入数字!!!");
                        sw.WriteLine("请确认第" + (i + 1) + "行 " + partno + " 第二批生产需求数量没有输入数字!!!", "温馨提示");
                        flag = 1;
                    }
                }
                if (flag == 1)
                {
                    sw.Close();
                    MessageBox.Show("要导入的预估文件数据有误，请检查！");
                    System.Diagnostics.Process.Start("explorer.exe", txtfilename);
                    return;
                }
            #endregion
                DialogResult dgresult = MessageBox.Show("确定要将预估结果进行导入吗？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dgresult == DialogResult.No) return;
                #region 首先将上次的预估结果保存为历史记录(按照预估人，专业，区域进行覆盖)
                
                PartParameter.DeleteEstimate(mSite, ProjectId, DisciplineId);
                #endregion
                ImportDataHanlder ImportData = new ImportDataHanlder(ImportParaData);
                ImportData.BeginInvoke(null, null);
            }
            catch (Exception et) { MessageBox.Show(et.Message); }
            finally { MessageBox.Show("预估数据导入成功！！！", "操作提示"); }
        }
        public void ImportParaData()
        {
            #region 逐行导入预估结果
            for (int i = 0; i < dgv2.Rows.Count - 1; i++)
            {
                string partno = dgv2.Rows[i].Cells[4].Value.ToString().Trim();
                string quyu = dgv2.Rows[i].Cells[1].Value.ToString().Trim();
                string zhuanye = DisciplineId;
                string parttype = dgv2.Rows[i].Cells[3].Value.ToString().Trim();
                string partname = dgv2.Rows[i].Cells[5].Value.ToString().Trim();
                string predict_qty = dgv2.Rows[i].Cells[6].Value.ToString().Trim();
                string firstbatchqty = dgv2.Rows[i].Cells[7].Value.ToString().Trim();
                string firstbatchdate = dgv2.Rows[i].Cells[8].Value.ToString().Trim();
                string secondbatchqty = dgv2.Rows[i].Cells[9].Value.ToString().Trim();
                string secondbatchdate = dgv2.Rows[i].Cells[10].Value.ToString().Trim();
                string unit_meas = dgv2.Rows[i].Cells[11].Value.ToString().Trim();
                string predict_date = dgv2.Rows[i].Cells[12].Value.ToString().Trim();
                string predict_person = dgv2.Rows[i].Cells[13].Value.ToString().Trim();
                PartParameter predictset = new PartParameter();
                predictset.OPERATOR = User.cur_user;
                predictset.PREDICT_DATE = Convert.ToDateTime(predict_date);
                predictset.PREDICT_CREATOR = predict_person;
                predictset.PREDICTION_QTY = string.IsNullOrEmpty(predict_qty) == true ? 0 : decimal.Parse(predict_qty);
                predictset.PART_NO = partno;
                predictset.PROJECT_ZONE = quyu;
                predictset.DISCIPLINE = zhuanye;
                predictset.PART_TYPE = parttype;
                predictset.DESCRIPTION = partname;
                predictset.CONTRACT = mSite;
                predictset.LAST_FLAG = 1;
                predictset.FIRSTBATCH_QTY = string.IsNullOrEmpty(firstbatchqty) == true ? 0 : decimal.Parse(firstbatchqty);
                predictset.FIRSTBATCH_DATE = Convert.ToDateTime(firstbatchdate);
                predictset.SECONDBATCH_QTY = string.IsNullOrEmpty(secondbatchqty) == true ? 0 : decimal.Parse(secondbatchqty);
                predictset.SECONDBATCH_DATE = Convert.ToDateTime(secondbatchdate);
                predictset.FINAL_PREDICTION_QTY = string.IsNullOrEmpty(predict_qty) == true ? 0 : decimal.Parse(predict_qty);
                predictset.UNIT = unit_meas;
                predictset.ECPROJECTID = ProjectSystem.FindProjectid(ProjectId); ;
                predictset.PROJECTID = ProjectId;
                int count = predictset.Add();
            }
            
            #endregion
        }
        private void M_Estimate_Load(object sender, EventArgs e)
        {
            ProjectCmbItem.ProjectCmbBind(cmb_project);
            ProjectCmbItem.BindDiscipline(cmb_dpname);
            ProjectCmbItem.SiteCmbBind(cmb_site); 
        }
    }
}
