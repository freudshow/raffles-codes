using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Data.OracleClient;

namespace DetailInfo
{
    public partial class WorkShopPlanDetail : Form
    {
        public WorkShopPlanDetail()
        {
            InitializeComponent();
        }

        string catStr = string.Empty;
        string proStr = string.Empty;
        string blockStr = string.Empty;
        string sqlStr = string.Empty;

        private void WorkShopPlanDetail_Load(object sender, EventArgs e)
        {
            string[] TypeList = new string[] { "托盘材料配套明细", "托盘预制明细", "托盘安装明细" };
            foreach (string item in TypeList)
            {
                this.CatComboBox.ComboBox.Items.Add(item);
            }
            sqlStr = @"SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82)   ORDER BY NAME";
            ComFill(this.ProjectComboBox,sqlStr);
        }

        /// <summary>
        /// 计划明细类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ProjectComboBox.ComboBox.SelectedIndex = 0;
            //this.BlockComboBox.ComboBox.Items.Clear();
            string filePath = string.Empty;
            catStr = this.CatComboBox.SelectedItem.ToString();
            switch (catStr)
            {
                case "托盘材料配套明细":
                    filePath = System.Windows.Forms.Application.StartupPath + "\\Resources\\舾装托盘材料配套明细表.xls";
                    OpenExelFile(filePath);
                    
                    break;
                case "托盘预制明细":
                    filePath = System.Windows.Forms.Application.StartupPath + "\\Resources\\舾件托盘预制明细表.xls";
                    OpenExelFile(filePath);
                    break;
                case "托盘安装明细":
                    filePath = System.Windows.Forms.Application.StartupPath + "\\Resources\\舾件托盘安装明细表.xls";
                    OpenExelFile(filePath);
                    break;
                default:
                    break;
            }
        }

        private void OpenExelFile(string fileName)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            if (app == null)
            {
                MessageBox.Show("Excel打开失败！");
                return;
            }
            app.Visible = false;
            app.UserControl = true;
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);
            app.DisplayAlerts = false;
            try
            {
                app.CopyObjectsWithCells = true;
                Microsoft.Office.Interop.Excel._Worksheet sheet1 = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets[1];
                sheet1.UsedRange.Copy(Type.Missing);
                AxMicrosoft.Office.Interop.Owc11.AxSpreadsheet spreadsheet = new AxMicrosoft.Office.Interop.Owc11.AxSpreadsheet();
                ((System.ComponentModel.ISupportInitialize)(spreadsheet)).BeginInit();
                this.Controls.Add(spreadsheet);
                ((System.ComponentModel.ISupportInitialize)(spreadsheet)).EndInit();
                spreadsheet.get_Range(this.axSpreadsheet1.Cells[1, 1]).Paste();
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message.ToString());
                return;
            }
            app.Quit();

        }

        /// <summary>
        /// 保存目标excel文件到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            catStr = this.CatComboBox.ComboBox.Text.ToString();
            proStr = this.ProjectComboBox.ComboBox.Text.ToString();
            blockStr = this.BlockComboBox.ComboBox.Text.ToString();
            
            if (catStr == string.Empty || proStr == string.Empty || blockStr == string.Empty)
            {
                MessageBox.Show("计划类型，项目,托盘号(分段号)均不能为空！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.Title = "保存";
            ofdialog.InitialDirectory = "D:";
            ofdialog.Filter = "Supported Image Types (*.xls)|*.xls";
            if (ofdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePathStr = ofdialog.FileName.ToString();
                object count = null;
                string updateStr = string.Empty;
                switch (catStr)
                {
                    case "托盘材料配套明细":
                        sqlStr = @"INSERT INTO SP_TRAYMATERIALDETAIL_TAB (PROJECTID, TRAYNO,PLANFILE,CREATER) VALUES ('" + proStr + "', '" + blockStr + "', :dfd, '"+User.cur_user+"' )";
                        count = PlanIndicator("SP_TRAYMATERIALDETAIL_TAB", proStr, blockStr);
                        if (Convert.ToInt16(count.ToString()) == 0)
                        {
                            UpdatePlanDetail(filePathStr, sqlStr);
                        }
                        else
                        {
                            updateStr = @"UPDATE SP_TRAYMATERIALDETAIL_TAB SET VERSION = 'N' WHERE PROJECTID = '" + proStr + "' AND TRAYNO = '" + blockStr + "'";
                            User.UpdateCon(updateStr, DataAccess.OIDSConnStr);
                            UpdatePlanDetail(filePathStr, sqlStr);
                        }
                        MessageBox.Show("------数据保存成功！------");

                        break;
                    case "托盘预制明细":
                        sqlStr = @"INSERT INTO SP_TRAYPREDETAIL_TAB (PROJECTID, TRAYNO,PLANFILE,CREATER) VALUES ('" + proStr + "', '" + blockStr + "', :dfd, '" + User.cur_user + "' )";
                        count = PlanIndicator("SP_TRAYPREDETAIL_TAB", proStr, blockStr);
                        if (Convert.ToInt16(count.ToString()) == 0)
                        {
                            UpdatePlanDetail(filePathStr, sqlStr);
                        }
                        else
                        {
                            updateStr = @"UPDATE SP_TRAYPREDETAIL_TAB SET VERSION = 'N' WHERE PROJECTID = '" + proStr + "' AND TRAYNO = '" + blockStr +"'";
                            User.UpdateCon(updateStr, DataAccess.OIDSConnStr);
                            UpdatePlanDetail(filePathStr, sqlStr);
                        }
                        MessageBox.Show("------数据保存成功！------");
                        break;

                    case "托盘安装明细":
                        sqlStr = @"INSERT INTO SP_TRAYINSTALLDETAIL_TAB (PROJECTID, TRAYNO,PLANFILE,CREATER) VALUES ('" + proStr + "', '" + blockStr + "', :dfd, '" + User.cur_user + "' )";
                        count = PlanIndicator("SP_TRAYINSTALLDETAIL_TAB", proStr, blockStr);
                        if (Convert.ToInt16(count.ToString()) == 0)
                        {
                            UpdatePlanDetail(filePathStr, sqlStr);
                        }
                        else
                        {
                            updateStr = @"UPDATE SP_TRAYINSTALLDETAIL_TAB SET VERSION = 'N' WHERE PROJECTID = '" + proStr + "' AND TRAYNO = '" + blockStr + "'";
                            User.UpdateCon(updateStr, DataAccess.OIDSConnStr);
                            UpdatePlanDetail(filePathStr, sqlStr);
                        }
                        MessageBox.Show("------数据保存成功！------");
                        break;

                    default:
                        break;
                }

            }


        }

        /// <summary>
        /// 分段选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlockComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            catStr = this.CatComboBox.ComboBox.Text.ToString();
            proStr = this.ProjectComboBox.ComboBox.Text.ToString();
            blockStr = this.BlockComboBox.ComboBox.Text.ToString();
            if (catStr == string.Empty || proStr == string.Empty)
            {
                MessageBox.Show("类型和项目不能为空！","信息提示！",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            switch (catStr)
            {
                case "托盘材料配套明细":
                    DownLoadExcel("SP_TRAYMATERIALDETAIL_TAB", proStr, blockStr);
                    break;
                default:
                    break;
            }
            

        }

        /// <summary>
        /// 填充combobox
        /// </summary>
        /// <param name="tscomb"></param>
        /// <param name="sql"></param>
        public static void ComFill(ToolStripComboBox tscomb, string sql)
        {
            DataSet ds = new DataSet();
            User.DataBaseConnect(sql, ds);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = " ";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tscomb.ComboBox.Items.Add(ds.Tables[0].Rows[i][0]);
            }
            ds.Dispose();
        }

        /// <summary>
        /// 更新计划
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="sqlstr"></param>
        private void UpdatePlanDetail(string filepath,string sql)
        {
            BinaryReader reader = null;
            FileStream myfilestream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            try
            {
                reader = new BinaryReader(myfilestream);
                byte[] file = reader.ReadBytes((int)myfilestream.Length);
                using (OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        OracleParameter op = new OracleParameter("dfd", OracleType.Blob);
                        op.Value = file;
                        if (file.Length == 0)
                        {
                            MessageBox.Show("插入文档不能为空！", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        else
                        {
                            cmd.Parameters.Add(op);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                    myfilestream.Close();
                    conn.Close();
                }

            }
            catch (OracleException  oex)
            {
                MessageBox.Show(oex.Message.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        private static object PlanIndicator(string tableName, string project, string block)
        {
            string sql = @"select count(*) from "+tableName+" where PROJECTID = '"+project+"' AND TRAYNO = '"+block+"' AND VERSION = 'Y'";
            object obj = User.GetScalar(sql, DataAccess.OIDSConnStr);
            return obj;
        }

        private void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BlockComboBox.ComboBox.Items.Clear();
            proStr = this.ProjectComboBox.ComboBox.Text.ToString();
            sqlStr = @"select description from project_block_tab where project_id = (select id from project_tab where name = '"+proStr+"')";
            ComFill(BlockComboBox, sqlStr);
        }

        /// <summary>
        /// 下载excel文档到本地
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="project"></param>
        /// <param name="block"></param>
        private void DownLoadExcel(string tableName, string project, string block)
        {
            using (OracleConnection connection = new OracleConnection(DataAccess.OIDSConnStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandText = @"select * from  " + tableName + " WHERE 1=1 AND PROJECTID = '" + project + "' AND TRAYNO = '" + block + "'";
                OracleDataReader dr = command.ExecuteReader();
                string filepath = string.Empty;
                while (dr.Read())
                {
                    if (dr["PLANFILE"] != null)//如果文章内容为空 不能转二进制
                    {
                        try
                        {
                            byte[] b1 = (byte[])dr["PLANFILE"];

                            string pathstr = User.rootpath + "\\" + "plantemp";
                            if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                            {
                                Directory.CreateDirectory(pathstr); //新建文件夹   
                            }

                            filepath = pathstr + "\\" + tableName + "_" + project + "_" + block + ".xls";
                            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
                            BinaryWriter bw = new BinaryWriter(fs);
                            bw.Write(b1, 0, b1.Length);
                            bw.Close();
                            fs.Close();
                        }
                        catch (SystemException ex)
                        {
                            return;
                        }

                        //OpenExelFile(filepath);
                    }
                    OpenExelFile(filepath);
                }
                dr.Close();
                if (filepath == string.Empty)
                {
                    MessageBox.Show("暂没有查询到相关计划数据！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkShopPlanDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            string pathStr = User.rootpath + "\\" + "plantemp";
            if (Directory.Exists(pathStr))
            {
                Directory.Delete(pathStr, true);
            }
        }

        /// <summary>
        /// 激活窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkShopPlanDetail_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }


    }
}
