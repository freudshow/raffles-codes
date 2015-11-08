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
    public partial class inventoryFrm : Form
    {
		OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
        OracleDataAdapter mOrclAdapter = new OracleDataAdapter();
        DataSet MyDataSet = new DataSet();
		DataTable InventoryTable = new DataTable();
		
        public inventoryFrm()
        {
            InitializeComponent();
			InventoryTable.Columns.Add("类型",typeof(String));
			InventoryTable.Columns.Add("长度", typeof(Int32));
			InventoryTable.Columns.Add("炉批号", typeof(String));
			InventoryTable.Columns.Add("ERP编码", typeof(String));
			InventoryTable.Columns.Add("重量", typeof(Double));
			InventoryTable.Columns.Add("数量", typeof(Int32));
        }

        private void inventory_Load(object sender, EventArgs e)
        {
            try
            {
				string PipeBaseSQLString = @"SELECT PIPETYPE 类型,PIPELENGTH 长度,ERPCODE ERP编码,BATCH_NO 炉批号,WEIGHT 单重 ,sum(WEIGHT) 总重量,COUNT(*) 数量 FROM PIPE_INVENTORY_SINGLE_SPOOL GROUP BY pipetype,pipelength,erpcode,BATCH_NO,WEIGHT";
                mOrclAdapter.SelectCommand = new OracleCommand(PipeBaseSQLString, conn);
                mOrclAdapter.Fill(MyDataSet);
				if (MyDataSet.Tables.Count > 0)
				{
					for (int i = 0; i < MyDataSet.Tables[0].Rows.Count; i++)
					{
 						DataRow InvenRow = InventoryTable.NewRow();
						InvenRow["类型"] = MyDataSet.Tables[0].Rows[i]["类型"];
						InvenRow["长度"] = MyDataSet.Tables[0].Rows[i]["长度"];
						InvenRow["炉批号"] = MyDataSet.Tables[0].Rows[i]["炉批号"];
						InvenRow["ERP编码"] = MyDataSet.Tables[0].Rows[i]["ERP编码"];
						InvenRow["重量"] = MyDataSet.Tables[0].Rows[i]["总重量"];
						InvenRow["数量"] = MyDataSet.Tables[0].Rows[i]["数量"];
						InventoryTable.Rows.Add(InvenRow);
					}
					dataGridView1.DataSource = InventoryTable;
				}
                
                this.dataGridView1.ReadOnly = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + "请联系管理员");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
			try
			{
				OracleConnection OrclConn = new OracleConnection(DataAccess.OIDSConnStr);
				OrclConn.Open();
				OracleDataAdapter OrclNestAdapter = new OracleDataAdapter();
				OracleCommand OrclNestCmd = OrclConn.CreateCommand();
				OracleCommand OrclCmd = OrclConn.CreateCommand();
				//OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(mOrclAdapter);
				//mOrclAdapter.Update(MyDataSet);
				//conn.Close();
				//for each row and each spool , insert into single_inventory_table
				for (int i = 0; i < InventoryTable.Rows.Count; i++)
				{
					string pipetype = InventoryTable.Rows[i]["类型"].ToString();
					Int32 pipelength = Convert.ToInt32(InventoryTable.Rows[i]["长度"].ToString());
					string erpcode = InventoryTable.Rows[i]["ERP编码"].ToString();
					double weight = (Convert.ToDouble(InventoryTable.Rows[i]["重量"].ToString())) / (Convert.ToInt32(InventoryTable.Rows[i]["数量"].ToString()));
					string batch_no = InventoryTable.Rows[i]["炉批号"].ToString();
					for (int j = 0; j < Convert.ToInt32(InventoryTable.Rows[i]["数量"].ToString()); j++)
					{
						//insert into oracle						
						OrclCmd.CommandText = "INSERT INTO pipe_inventory_single_spool (PIPETYPE,PIPELENGTH,ERPCODE,WEIGHT,BATCH_NO,STATE) VALUES ('" + pipetype + "'," + pipelength + ",'" + erpcode + "'," + weight + ",'" + batch_no + "',1)";
						OrclCmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
			MessageBox.Show("更新成功!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
		}

		private void textBox_quantity_KeyDown(object sender, KeyEventArgs e)
		{
			//add table row
			if (e.KeyCode == Keys.Enter)			
			{
				DataRow InvenRow = InventoryTable.NewRow();
				InvenRow["类型"] = textBox_type.Text;
				InvenRow["长度"] = textBox_length.Text;
				InvenRow["炉批号"] = textBox_batchNO.Text;
				InvenRow["ERP编码"] = textBox_ERPCODE.Text;
				InvenRow["重量"] = textBox_weight.Text;
				InvenRow["数量"] = textBox_quantity.Text;
				InventoryTable.Rows.Add(InvenRow);
				this.dataGridView1.DataSource = InventoryTable;
			}
		}
    }
}