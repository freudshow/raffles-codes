using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class SpoolRecoveryFm : Form
    {
        public SpoolRecoveryFm()
        {
            InitializeComponent();
            for (int i = 0; i < this.DelContextMenuStrip.Items.Count; i++)
            {
                this.DelContextMenuStrip.Items[i].Visible = false;
            }
        }

        string sqlStr = string.Empty;

        private void SpoolRecoveryFm_Load(object sender, EventArgs e)
        {
            sqlStr = "SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82)   ORDER BY NAME";
            ComFill(ProjectComboBox,sqlStr);
        }

        /// <summary>
        /// 填充项目combobox
        /// </summary>
        /// <param name="tscomb"></param>
        /// <param name="sql"></param>
        private void ComFill(ToolStripComboBox tscomb, string sql)
        {
            DataSet ds = new DataSet();
            User.DataBaseConnect(sql, ds);
            DataRow dr = ds.Tables[0].NewRow();
            //dr[0] = "-所有项目-";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tscomb.ComboBox.Items.Add(ds.Tables[0].Rows[i][0]);
            }
            ds.Dispose();
        }

        /// <summary>
        /// 激活窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpoolRecoveryFm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
            MDIForm.tool_strip.Items[2].Enabled = false;
        }

        /// <summary>
        /// 选择项目填充图纸combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DrawingComboBox.ComboBox.Items.Clear();
            string projectid= this.ProjectComboBox.ComboBox.SelectedItem.ToString();
            sqlStr = @"SELECT DRAWING_NO
  FROM PLM.PROJECT_DRAWING_TAB
 where drawing_type is null
   AND Project_Id =
       (select T.ID
          from PROJECT_TAB T
         where T.NAME = '" + projectid + "') AND DOCTYPE_ID IN (7) AND DOCTYPE_ID != 71 AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' AND RESPONSIBLE_USER = '"+User.cur_user+"' ORDER BY DRAWING_ID DESC";
            ComFill(DrawingComboBox, sqlStr);
        }
        

        private void DrawingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projectStr = this.ProjectComboBox.ComboBox.SelectedItem.ToString();
            string drawingStr = this.DrawingComboBox.ComboBox.SelectedItem.ToString();
            sqlStr = @"select SPOOLNAME, BLOCKNO, SYSTEMID, SYSTEMNAME, PIPEGRADE, SURFACETREATMENT,WORKINGPRESSURE, PRESSURETESTFIELD, PIPECHECKFIELD, SPOOLWEIGHT, PAINTCOLOR,CABINTYPE,REMARK, LOGNAME, LOGDATE, DELETEPERSON  FROM SP_SPOOL_TAB
            WHERE PROJECTID = '"+projectStr+"' AND DRAWINGNO = '"+drawingStr+"' AND FLAG = 'N' AND DELETEMARK = 'N' AND DELETEPERSON = '"+User.cur_user+"'";
            DataSet MyData = new DataSet();
            User.DataBaseConnect(sqlStr, MyData);
            this.DeletedRecordDgv.DataSource = MyData.Tables[0].DefaultView;
            MyData.Dispose();
        }

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 恢复数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.DeletedRecordDgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                DialogResult result;
                result = MessageBox.Show("确定要恢复所选中的小票以及相关信息？", "信息提示！", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    foreach (DataGridViewRow drow in this.DeletedRecordDgv.SelectedRows)
                    {
                        int rowindex = drow.Index;
                        string project = this.DeletedRecordDgv.Rows[rowindex].Cells["项目号"].Value.ToString();
                        string spool = this.DeletedRecordDgv.Rows[rowindex].Cells["小票号"].Value.ToString();
                        string drawno = this.DeletedRecordDgv.Rows[rowindex].Cells["图号"].Value.ToString();

                        //DelSpoolRecord.MarkDeletedSpoolRecord(project, spool, drawno, User.cur_user);
                    }

                    MessageBox.Show("-数据恢复完毕！-请重新查询并验证！");

                }
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// 选择行发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletedRecordDgv_SelectionChanged(object sender, EventArgs e)
        {
            int rowcount = this.DeletedRecordDgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                for (int i = 0; i < this.DelContextMenuStrip.Items.Count; i++)
                {
                    this.DelContextMenuStrip.Items[i].Visible = true;
                }
            }
            else
            {
                return;
            }
        }

    }
}
