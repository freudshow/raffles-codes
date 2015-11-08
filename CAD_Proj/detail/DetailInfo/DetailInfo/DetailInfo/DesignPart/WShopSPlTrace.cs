using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace DetailInfo
{
    public partial class WShopSPlTrace : Form
    {
        public WShopSPlTrace()
        {
            InitializeComponent();
        }
        string sqlStr = string.Empty;
        private void WShopSPlTrace_Load(object sender, EventArgs e)
        {
            sqlStr = " SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82) and NAME IN (SELECT DISTINCT S.PROJECTID　FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y')   ORDER BY NAME";
            FillComb(this.ProjectComboBox, sqlStr);
        }

        private void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DrawingComboBox.Items.Clear();
            this.textBox1.Text = " ";
            string projectStr = this.ProjectComboBox.Text.ToString();
            sqlStr = "SELECT DRAWING_NO FROM PLM.PROJECT_DRAWING_TAB where drawing_type is null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + projectStr + "') AND DOCTYPE_ID IN (7)  AND DOCTYPE_ID != 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' ORDER BY DRAWING_ID DESC";
            FillComb(this.DrawingComboBox, sqlStr);
        }

        private void DrawingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projectStr = this.ProjectComboBox.Text.ToString();
            string drawingStr = this.DrawingComboBox.Text.ToString();
            if (string.IsNullOrEmpty(drawingStr))
            {
                return;
            }
            sqlStr = "select ISSUED_TIME from project_drawing_tab where project_id = (select id from project_tab where name = '" + projectStr + "') and drawing_no = '"+drawingStr+"' and lastflag = 'Y' and delete_flag = 'N'";
            DataSet ds = new DataSet();
            User.DataBaseConnect(sqlStr, ds);
            this.textBox1.Text = ds.Tables[0].Rows[0][0].ToString();
            ds.Dispose();
            string querystr = "SP_GetSpoolTrace";
            WorkShopClass.GetSpoolTrace(querystr, projectStr, drawingStr,SPTraceDgv);
        }

        /// <summary>
        /// 激活窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WShopSPlTrace_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WShopSPlTrace_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }
        /// <summary>
        /// 格式化数据列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SPTraceDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            this.SPTraceDgv.Columns["管径壁厚"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.SPTraceDgv.Columns["总数"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.SPTraceDgv.Columns["内场"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.SPTraceDgv.Columns["外场"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <summary>
        /// 填充combox
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="sql"></param>
        private void FillComb(ComboBox cb,string sql)
        {
            try
            {
                OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    cb.Items.Clear();
                    cb.Items.Add("");
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                    conn.Close();
                    dr.Close();
                }
            }
            catch (OracleException ox)
            {
                MessageBox.Show(ox.Message.ToString());
                return;
            }
        }
    }
}
