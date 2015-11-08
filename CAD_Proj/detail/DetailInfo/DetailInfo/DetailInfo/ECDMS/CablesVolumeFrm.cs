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
    public partial class CablesVolumeFrm : Form
    {
        string sql = "select CABLESIZE_ID ID,CABLE_XH 序号,CABLE_TYPE 电缆型号,CABLE_SPEC 电缆规格,CABLE_MAXDIA 电缆直径,CREATOR 创建人,REMARK 备注 from CABLE_SIZE_TAB ";
        public CablesVolumeFrm()
        {
            InitializeComponent();          
            DataBind(sql);
            
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sqlstr"></param>
        private void DataBind(string sqlstr)
        {
            try
            {
                DataSet ds = new DataSet();
                User.DataBaseConnect(sqlstr, ds);
                this.CableSizedgv.DataSource = ds.Tables[0].DefaultView;  
                ds.Dispose();

            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
        private void datacontrol(string dcsql)
        {
            DataSet dats = new DataSet();
            User.DataBaseConnect(dcsql, dats);
            XHtb.Text = "";
            TYPEtb.Text = "";
            SPECtb.Text = "";
            MAXDIAtb.Text = "";
            REMARKtb.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text=="录入")
            {
            string sqlins = "insert into CABLE_SIZE_TAB (CABLE_XH,CABLE_TYPE,CABLE_SPEC,CABLE_MAXDIA,CREATOR,REMARK) values ('" + XHtb.Text + "','" + TYPEtb.Text + "','" + SPECtb.Text + "','" + MAXDIAtb.Text + "','" + User.cur_user + "','" + REMARKtb.Text + "')";
            datacontrol(sqlins);
            MessageBox.Show("录入成功!");
            DataBind(sql);
            }
            else if (button1.Text=="修改")
            {
                string sqldel = "update CABLE_SIZE_TAB set CABLE_XH='" + XHtb.Text + "', CABLE_TYPE='" + TYPEtb.Text + "', CABLE_SPEC='" + SPECtb.Text + "', CABLE_MAXDIA='" + MAXDIAtb.Text + "', REMARK='" + REMARKtb.Text + "' where creator='" + User.cur_user + "' and CABLESIZE_ID='"+CableSizedgv.SelectedRows[0].Cells[0].Value.ToString()+"'";
                datacontrol(sqldel);
                MessageBox.Show("修改成功！");
                button1.Text = "录入";
                DataBind(sql);
            }

        }

        private void CableSizedgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && CableSizedgv.SelectedRows.Count>0)
            {
                CABLEcontextMenuStrip1.Show(MousePosition.X, MousePosition.Y);

            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult objdialogresult=MessageBox.Show("确定要删除么!", "提示", MessageBoxButtons.YesNo);
            if (objdialogresult==DialogResult.Yes)
            {
                string sqldel = "delete from CABLE_SIZE_TAB where CABLESIZE_ID='" + CableSizedgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
                datacontrol(sqldel);
                MessageBox.Show("删除成功！");
                button1.Text = "录入";
                DataBind(sql);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XHtb.Text = CableSizedgv.SelectedRows[0].Cells[1].Value.ToString();
            TYPEtb.Text = CableSizedgv.SelectedRows[0].Cells[2].Value.ToString();
            SPECtb.Text = CableSizedgv.SelectedRows[0].Cells[3].Value.ToString();
            MAXDIAtb.Text = CableSizedgv.SelectedRows[0].Cells[4].Value.ToString();
            REMARKtb.Text = CableSizedgv.SelectedRows[0].Cells[6].Value.ToString();
            button1.Text = "修改";

        }
    }
}
