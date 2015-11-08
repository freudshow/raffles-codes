using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using DetailInfo.Application_Code;
namespace DetailInfo.ECDMS
{
    public partial class CableListFrm : Form
    {

        string sql = "select ID,电缆TAG,电缆规格,图号,起始设备,终止设备,责任人 from CABLE_LIST_VIEW where 图号='" + ProjectDrawingCableFrm.drawingno.TrimEnd() + "'";
        string devicesql = "select '[' || tag_no || ']' || equipment from device_tab where project_id=(select id from project_tab where name='"+ProjectDrawingCableFrm.projectid+"') and newflag='Y'";
        string cablesizesql = "select cable_type || '[' || cable_spec || ']' from cable_size_tab";
        public CableListFrm()
        {
            InitializeComponent();
            drwtb.Text = ProjectDrawingCableFrm.drawingno;
            drwentb.Text = ProjectDrawingCableFrm.drawingtitle;
            drwcntb.Text = ProjectDrawingCableFrm.drawingtitlecn;
            textBox1.Text = ProjectDrawingCableFrm.duty;
            DataBind(sql);
            FillComboBox.GetFlowStatus(fequipcb, devicesql);
            FillComboBox.GetFlowStatus(tequipcb, devicesql);
            FillComboBox.GetFlowStatus(cablesizecb, cablesizesql);
        }
        /// <summary>
        /// datagridview数据绑定
        /// </summary>
        /// <param name="sqlstr"></param>
        private void DataBind(string sqlstr)
        {
            try
            {
                OracleConnection con = new OracleConnection(DataAccess.OIDSConnStr);
                OracleDataAdapter oda = new OracleDataAdapter(sqlstr, con);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                CableListdgv.DataSource = ds.Tables[0].DefaultView;
                ds.Dispose();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
        /// <summary>
        /// 操作数据库
        /// </summary>
        private void opdatabase(string sqlstr)
        {
            try
            {
                OracleConnection con = new OracleConnection(DataAccess.OIDSConnStr);
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = sqlstr;
                int rows = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }   
        }
        private void cablecb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string combosql = "";
            //CableListdgv.CurrentCell.Value = cablesizecb.SelectedItem;
            //CableListdgv.SelectedRows[0].Cells[2].Value = ProjectDrawingCableFrm.drawingno;

            //if (CableListdgv_cabletag == "" && CableListdgv_cablesizeid == "" && CableListdgv_fdevice == "" && CableListdgv_tdevice == "")
            //{
            //    combosql = "insert into cable_list_tab (cablesize_id,drawing_id) select a.cablesize_id,b.drawing_id from cable_size_tab a,project_drawing_tab b where a.CABLE_TYPE || '[' || a.CABLE_SPEC || ']'='" + CableListdgv.CurrentCell.Value + "' and b.drawing_no='" + CableListdgv_drawingid.TrimEnd() + "'";
            //}
            //else
            //{
            //    combosql = "update cable_list_tab set cablesize_id=(select cablesize_id from cable_size_tab where CABLE_TYPE || '[' || CABLE_SPEC || ']'='" + CableListdgv.CurrentCell.Value + "')";
            //}
            //opdatabase(combosql);
            //DataBind(sql);
        }
        private void datacontrol(string dcsql)
        {
            DataSet dats = new DataSet();
            User.DataBaseConnect(dcsql, dats);
            cabletagtb.Text = "";
            cablesizecb.Text = "";
            fequipcb.Text = "";
            tequipcb.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "录入")
            {
                try
                {
                    string inssql = @"insert into cable_list_tab
  (drawing_id,cable_tag,cablesize_id, from_device_id, to_device_id,operator)
  select (select a.drawing_id
            from project_drawing_tab a
           where a.drawing_no = '" + ProjectDrawingCableFrm.drawingno +
                   "' and a.project_id=(select id from project_tab where name = '" + ProjectDrawingCableFrm.projectid + "')),'" + cabletagtb.Text +
                   "',(select b.cablesize_id from cable_size_tab b where b.CABLE_TYPE || '[' || b.CABLE_SPEC || ']' = '" + cablesizecb.Text +
                   "'),(select c.device_id from device_tab c where '[' || c.tag_no || ']' || c.equipment ='" + fequipcb.Text +
                   "'),(select c.device_id from device_tab c where '[' || c.tag_no || ']' || c.equipment ='" + tequipcb.Text + "'),'" + User.cur_user +
                   "' from dual";
                    datacontrol(inssql);
                    DataBind(sql);
                    MessageBox.Show("录入成功！");
                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
                
            }
            else if (button1.Text == "修改")
            {
                try
                {
                    string updsql = @"update  cable_list_tab
            set drawing_id=(select a.drawing_id
                            from project_drawing_tab a
                            where a.drawing_no = '" + ProjectDrawingCableFrm.drawingno +
                               "' and a.project_id=(select id from project_tab where name = '" + ProjectDrawingCableFrm.projectid + "')),cable_tag='" + cabletagtb.Text +
                               "',cablesize_id=(select b.cablesize_id from cable_size_tab b where b.CABLE_TYPE || '[' || b.CABLE_SPEC || ']' = '" + cablesizecb.Text +
                               "'), from_device_id=(select c.device_id from device_tab c where '[' || c.tag_no || ']' || c.equipment ='" + fequipcb.Text +
                               "'), to_device_id=(select c.device_id from device_tab c where '[' || c.tag_no || ']' || c.equipment ='" + tequipcb.Text + "') where cable_id='" + CableListdgv.SelectedRows[0].Cells[0].Value.ToString() + "' and operator='" + User.cur_user + "'";
                    datacontrol(updsql);
                    MessageBox.Show("修改成功！");
                    button1.Text = "录入";
                    DataBind(sql);
                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cabletagtb.Text = CableListdgv.SelectedRows[0].Cells[1].Value.ToString();
            cablesizecb.Text = CableListdgv.SelectedRows[0].Cells[2].Value.ToString();
            fequipcb.Text = CableListdgv.SelectedRows[0].Cells[4].Value.ToString();
            tequipcb.Text = CableListdgv.SelectedRows[0].Cells[5].Value.ToString();
            button1.Text = "修改";

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult objdialogresult = MessageBox.Show("确定要删除么!", "提示", MessageBoxButtons.YesNo);
            if (objdialogresult == DialogResult.Yes)
            {
                try
                {
                    string sqldel = "delete from CABLE_LIST_TAB where CABLE_ID='" + CableListdgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
                    datacontrol(sqldel);
                    MessageBox.Show("删除成功！");
                    button1.Text = "录入";
                    DataBind(sql);
                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }
        }

        private void CableListdgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && CableListdgv.SelectedRows.Count > 0)
            {
                cablelistMenuStrip1.Show(MousePosition.X, MousePosition.Y);

            }
        }

    }
}
