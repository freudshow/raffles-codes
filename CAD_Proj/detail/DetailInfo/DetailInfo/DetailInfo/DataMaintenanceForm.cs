using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.OracleClient;
using System.IO;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;


namespace DetailInfo
{
    public partial class DataMaintenanceForm : Form
    {
        private OracleDataAdapter oda = new OracleDataAdapter();
        private BindingSource bs = new BindingSource();  
        public DataMaintenanceForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sqlstr"></param>
        private void DataBind(string sqlstr)
        {
            try
            {
                OracleConnection con = new OracleConnection(DataAccess.OIDSConnStr);
                con.Open();
                oda = new OracleDataAdapter(sqlstr, con);
                OracleCommandBuilder builder = new OracleCommandBuilder(oda);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                bs.DataSource = ds.Tables[0];
                this.EditDgv.DataSource = bs;
                this.bindingNavigator1.BindingSource = bs;
                con.Close();
                ds.Dispose();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        } 

        private void DataMaintenanceForm_Load(object sender, EventArgs e)
        {
            if (User.cur_user == "fulong.wu")
            {
                this.bindingNavigatorDeleteItem.Visible = false;
            }
            
            string title = this.Text;
            switch (title)
            {
                case "弯头列表":
                    string sqlSP_BEND = "select ID 序号, PROJECT_ID  项目号, M_NO 材料代号, DN 通径,ONE_DXW  \"1倍定型弯头(mm)\",ONEHALF_DXW  \"1倍半定型弯头(mm)\", ONE_JCDXW  \"3倍45度定型弯头(mm)\", ONEHALF_JCDXW  \"3倍90度定型弯头(mm)\"  FROM  SP_BEND ORDER BY ID ASC";
                    DataBind(sqlSP_BEND);
                    this.EditDgv.Columns["序号"].Visible = false;

                    break;

                case "舱室列表":
                    string sqlSP_CABIN = "select ID 序号, PROJECT_ID 项目号, EN_CABIN 舱室英文名称, CH_CABIN 舱室中文名称 FROM SP_CABIN  ORDER BY ID ASC";
                    DataBind(sqlSP_CABIN);
                    this.EditDgv.Columns["序号"].Visible = false;
                    break;

                case "连接件列表":
                    string sqlSP_CONNECTOR = "select ID 序号,PROJECT_ID 项目号,NAME 法兰名称, PARTCODE 零件代码,  OUTDIAMETER \"外径(mm)\",  NUTWEIGHT \"螺母重(kg)\", BOLTWEIGHT \"螺栓重(kg)\" FROM SP_CONNECTOR  ORDER BY ID ASC";
                    DataBind(sqlSP_CONNECTOR);
                    this.EditDgv.Columns["序号"].Visible = false;

                    break;

                case "弯头材料对照列表":
                    string sqlSP_ELBOWMATERIAL = "select ID 序号,PROJECT_ID 项目号, PIPEMATERIAL 管材质, EMATERIAL 弯头材质, FLAGE  \"弯头标识\" FROM SP_ELBOWMATERIAL  ORDER BY ID ASC";
                    DataBind(sqlSP_ELBOWMATERIAL);
                    this.EditDgv.Columns["序号"].Visible = false;
                    break;

                case "弯模列表":
                    string sqlSP_PSTAD = "select ID 序号,PROJECT_ID 项目号, OUTSIDEDIAMETER \"管材的外径(mm)\", WAMO \"弯模(mm)\", QIANJA \"前夹长度(mm)\", HOUJA \"后夹长度(mm)\",  SECMACHINE \"弯管机弯模大小(mm)\" FROM SP_PSTAD  ORDER BY ID ASC";
                    DataBind(sqlSP_PSTAD);
                    this.EditDgv.Columns["序号"].Visible = false;

                    break;

                case "承接弯头列表":
                    string sqlSocketElow = "select ID 序号,PROJECT_ID 项目号, DN \"通径(mm)\", ELBOWONE \"45度承插弯头\", ELBOWTWO \"90度承插弯头\" FROM SP_SOCKETELBOW  ORDER BY ID ASC";
                    DataBind(sqlSocketElow);
                    this.EditDgv.Columns["序号"].Visible = false;
                    break;

                case "表面处理列表":
                    string sqlSP_SURFACE = "select ID 序号,PROJECT_ID 项目号,CODE 代码, DESCRIPTION 描述 FROM SP_SURFACE";
                    DataBind(sqlSP_SURFACE);
                    this.EditDgv.Columns["序号"].Visible = false;
                    break;

                case "系统列表":
                    string sqlSystem = "select ID 序号,PROJECT_ID 项目号, SYSID 系统代号, SYSCODE 系统代码, SYSNAME 系统名, GASKET 连接垫片, BENDMACHINE 弯管机型号 FROM SP_SYSTEM  ORDER BY ID ASC";
                    DataBind(sqlSystem);
                    this.EditDgv.Columns["序号"].Visible = false;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要保存吗？", "保存数据", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                this.Validate();
                bs.EndEdit();
                try
                {
                    if (((System.Data.DataTable)bs.DataSource).GetChanges() != null)
                    {
                        oda.Update(((System.Data.DataTable)bs.DataSource).GetChanges());
                        DataMaintenanceForm_Load(sender,e);
                        MessageBox.Show("保存数据成功");
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        /// <summary>
        /// 批量导入到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //bool ret = false;
            DataSet ds;
            DataSet sheetds = new DataSet();
            int dsLength;
            string formtext = this.Text;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Excel文件";
            ofd.FileName = "";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//为了获取特定的系统文件夹，可以使用System.Environment类的静态方法GetFolderPath()。该方法接受一个Environment.SpecialFolder枚举，其中可以定义要返回路径的哪个系统目录
            ofd.Filter = "Excel文件(*.xls)|*.xls";
            ofd.ValidateNames = true;     //文件有效性验证ValidateNames，验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true;  //验证路径有效性
            ofd.CheckPathExists = true; //验证文件有效性
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string sql = "SELECT EXCELTABLE FROM DATATABLE_TAB WHERE DESCRIPTION = '" + formtext + "'";
                User.DataBaseConnect(sql,sheetds);
                string excelsheet = sheetds.Tables[0].Rows[0][0].ToString();
                ds = ImportExcel(ofd.FileName,excelsheet);//获得Excel   
                if (ds == null)
                {
                    return;
                }

            }
            else
            {
                return;
            }

            int odr = 0;

            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接
            conn.Open();
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = conn.CreateCommand();
            cmd.Transaction = trans;
            try
            {
                switch (formtext)
                {
                    case "弯头列表":
                        cmd.CommandText = "INSERT INTO SP_BEND (M_NO, DN, ONE_DXW, ONEHALF_DXW, ONE_JCDXW, ONEHALF_JCDXW, PROJECT_ID) VALUES (:xh,:hpzl,:hphm,:bz,:larq,:fdjh,:clpp) ";
                        cmd.Parameters.Add("xh", OracleType.Number);
                        cmd.Parameters.Add("hpzl", OracleType.VarChar);
                        cmd.Parameters.Add("hphm", OracleType.Number);
                        cmd.Parameters.Add("bz", OracleType.Number);
                        cmd.Parameters.Add("larq", OracleType.Number);
                        cmd.Parameters.Add("fdjh", OracleType.Number);
                        cmd.Parameters.Add("clpp", OracleType.VarChar);
                        
                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["bz"].Value = ds.Tables[0].Rows[i][3];
                            cmd.Parameters["larq"].Value = ds.Tables[0].Rows[i][4];
                            cmd.Parameters["fdjh"].Value = ds.Tables[0].Rows[i][5];
                            cmd.Parameters["clpp"].Value = ds.Tables[0].Rows[i][6];

                            odr = cmd.ExecuteNonQuery();//提交  
                        }
                        break;

                    case "舱室列表":
                        cmd.CommandText = "INSERT INTO SP_CABIN (PROJECT_ID, EN_CABIN, CH_CABIN) VALUES(:xh,:hpzl,:hphm) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.VarChar);
                        cmd.Parameters.Add("hphm", OracleType.VarChar);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }

                        break;

                    case "连接件列表":
                        cmd.CommandText = "INSERT INTO SP_CONNECTOR (NAME, PARTCODE, OUTDIAMETER, NUTWEIGHT, BOLTWEIGHT,PROJECT_ID) VALUES(:xh,:hpzl,:cjh,:jdcsyr,:cllx,:csys) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.VarChar);
                        cmd.Parameters.Add("cjh", OracleType.Number);
                        cmd.Parameters.Add("jdcsyr", OracleType.Number);
                        cmd.Parameters.Add("cllx", OracleType.Number);
                        cmd.Parameters.Add("csys", OracleType.VarChar);

                        dsLength = ds.Tables[0].Rows.Count;//获得Excel中数据长度   
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["cjh"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["jdcsyr"].Value = ds.Tables[0].Rows[i][3];
                            cmd.Parameters["cllx"].Value = ds.Tables[0].Rows[i][4];
                            cmd.Parameters["csys"].Value = ds.Tables[0].Rows[i][5];

                            odr = cmd.ExecuteNonQuery();
                        }

                        break;

                    case "弯头材料对照列表":
                        cmd.CommandText = "INSERT INTO SP_ELBOWMATERIAL (PROJECT_ID, PIPEMATERIAL, EMATERIAL, FLAGE) VALUES(:xh,:hpzl,:hphm,:bz) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.VarChar);
                        cmd.Parameters.Add("hphm", OracleType.VarChar);
                        cmd.Parameters.Add("bz", OracleType.VarChar);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["bz"].Value = ds.Tables[0].Rows[i][3];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    case "弯模列表":
                        cmd.CommandText = "INSERT INTO SP_PSTAD (OUTSIDEDIAMETER, WAMO, QIANJA, HOUJA, PROJECT_ID, SECMACHINE) VALUES(:xh,:hpzl,:hphm,:bz,:larq,:fdjh) ";
                        cmd.Parameters.Add("xh", OracleType.Number);
                        cmd.Parameters.Add("hpzl", OracleType.VarChar);
                        cmd.Parameters.Add("hphm", OracleType.Number);
                        cmd.Parameters.Add("bz", OracleType.Number);
                        cmd.Parameters.Add("larq", OracleType.VarChar);
                        cmd.Parameters.Add("fdjh", OracleType.VarChar);
                        
                        dsLength = ds.Tables[0].Rows.Count;//获得Excel中数据长度   
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["bz"].Value = ds.Tables[0].Rows[i][3];
                            cmd.Parameters["larq"].Value = ds.Tables[0].Rows[i][4];
                            cmd.Parameters["fdjh"].Value = ds.Tables[0].Rows[i][5];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    case "承接弯头列表":
                        cmd.CommandText = "INSERT INTO SP_SOCKETELBOW (PROJECT_ID, DN, ELBOWONE, ELBOWTWO) VALUES(:xh,:hpzl,:hphm,:bz) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.VarChar);
                        cmd.Parameters.Add("hphm", OracleType.Number);
                        cmd.Parameters.Add("bz", OracleType.Number);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["bz"].Value = ds.Tables[0].Rows[i][3];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    case "表面处理列表":
                        cmd.CommandText = "INSERT INTO SP_SURFACE (CODE, DESCRIPTION, PROJECT_ID) VALUES(:xh,:hpzl,:hphm) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.VarChar);
                        cmd.Parameters.Add("hphm", OracleType.VarChar);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    case "系统列表":
                        cmd.CommandText = "INSERT INTO SP_SYSTEM (PROJECT_ID, SYSID, SYSCODE, SYSNAME, GASKET, BENDMACHINE) VALUES(:xh,:hpzl,:hphm,:bz,:larq,:fdjh) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar); 
                        cmd.Parameters.Add("hpzl", OracleType.VarChar);
                        cmd.Parameters.Add("hphm", OracleType.VarChar);
                        cmd.Parameters.Add("bz", OracleType.VarChar);
                        cmd.Parameters.Add("larq", OracleType.VarChar);
                        cmd.Parameters.Add("fdjh", OracleType.VarChar);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["bz"].Value = ds.Tables[0].Rows[i][3];
                            cmd.Parameters["larq"].Value = ds.Tables[0].Rows[i][4];
                            cmd.Parameters["fdjh"].Value = ds.Tables[0].Rows[i][5];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    case "审核人列表":
                        cmd.CommandText = "INSERT INTO PROJECTAPPROVE (PROJECTID, ASSESOR, INDEX_ID) VALUES(:xh,:hpzl,:hphm) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.VarChar);
                        cmd.Parameters.Add("hphm", OracleType.Number);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    case "金属管线下料时间定额列表":
                        cmd.CommandText = "INSERT INTO BAITINGNORM_METALPIPE_TAB (CODE, NORM, PIPEMACHINING,EQUIPMENTOPERATION,UNPRODUCTIVETIME) VALUES(:xh,:hpzl,:hphm:bz,:larq) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.Number);
                        cmd.Parameters.Add("hphm", OracleType.Number);
                        cmd.Parameters.Add("bz", OracleType.Number);
                        cmd.Parameters.Add("larq", OracleType.Number);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["bz"].Value = ds.Tables[0].Rows[i][3];
                            cmd.Parameters["larq"].Value = ds.Tables[0].Rows[i][4];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    case "坡口加工时间定额列表":
                        cmd.CommandText = "INSERT INTO BEVEL_HOUR_NORM_TAB (CODE, NORM, EQUIPMENTOPERATION,UNPRODUCTIVETIME) VALUES(:xh,:hpzl,:hphm:bz) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.Number);
                        cmd.Parameters.Add("hphm", OracleType.Number);
                        cmd.Parameters.Add("bz", OracleType.Number);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["bz"].Value = ds.Tables[0].Rows[i][3];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    case "弯管时间定额列表":
                        cmd.CommandText = "INSERT INTO ELBOW_HOUR_NORM_TAB (CODE, NORM, PIPEMACHINING,EQUIPMENTOPERATION,UNPRODUCTIVETIME) VALUES(:xh,:hpzl,:hphm:bz,:larq) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.Number);
                        cmd.Parameters.Add("hphm", OracleType.Number);
                        cmd.Parameters.Add("bz", OracleType.Number);
                        cmd.Parameters.Add("larq", OracleType.Number);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["bz"].Value = ds.Tables[0].Rows[i][3];
                            cmd.Parameters["larq"].Value = ds.Tables[0].Rows[i][4];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    case "校管时间定额列表":
                        cmd.CommandText = "INSERT INTO PIPECHECKING_HOUR_NORM_TAB (CODE, NORM, PIPEMACHINING,EQUIPMENTOPERATION,WELD_WORKING,UNPRODUCTIVETIME) VALUES(:xh,:hpzl,:hphm:bz,:larq,:fdjh) ";
                        cmd.Parameters.Add("xh", OracleType.VarChar);
                        cmd.Parameters.Add("hpzl", OracleType.Number);
                        cmd.Parameters.Add("hphm", OracleType.Number);
                        cmd.Parameters.Add("bz", OracleType.Number);
                        cmd.Parameters.Add("larq", OracleType.Number);
                        cmd.Parameters.Add("fdjh", OracleType.Number);

                        dsLength = ds.Tables[0].Rows.Count;
                        for (int i = 1; i < dsLength; i++)
                        {
                            cmd.Parameters["xh"].Value = ds.Tables[0].Rows[i][0];
                            cmd.Parameters["hpzl"].Value = ds.Tables[0].Rows[i][1];
                            cmd.Parameters["hphm"].Value = ds.Tables[0].Rows[i][2];
                            cmd.Parameters["bz"].Value = ds.Tables[0].Rows[i][3];
                            cmd.Parameters["larq"].Value = ds.Tables[0].Rows[i][4];
                            cmd.Parameters["fdjh"].Value = ds.Tables[0].Rows[i][5];

                            odr = cmd.ExecuteNonQuery();//提交   
                        }
                        break;

                    default:
                        break;
                }

                trans.Commit();
                MessageBox.Show("导入成功");
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// 获得Excel的数据集
        /// </summary>
        /// <param name="file"></param>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        private  DataSet ImportExcel(string file,string worksheet)
        {
            FileInfo fileInfo = new FileInfo(file);
            if (!fileInfo.Exists)
                return null;

            string strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
            OleDbConnection objConn = new OleDbConnection(strConn);
            DataSet dsExcel = new DataSet();
            try
            {
                objConn.Open();
                string strSql = "select * from  "+worksheet+"";
                OleDbDataAdapter odbcExcelDataAdapter = new OleDbDataAdapter(strSql, objConn);
                odbcExcelDataAdapter.Fill(dsExcel);
                return dsExcel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "导入失败，请查看要导入的excel文档格式");
                return null;
                //throw ex;
            }
        }  

        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataMaintenanceForm_Load(sender,e);
        }

        /// <summary>
        /// 下载模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "导出Excel (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "下载文件保存路径";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName.Trim().Length > 0)
                {
                    byte[] template = Properties.Resources.数据导入模板;
                    FileStream stream = new FileStream(saveFileDialog.FileName,FileMode.OpenOrCreate);
                    stream.Write(template,0,template.Length);
                    stream.Close();
                    stream.Dispose();
                    MessageBox.Show("下载模板成功！");
                }
            }
        }

        /// <summary>
        /// 激活页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataMaintenanceForm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
            MDIForm.tool_strip.Items[2].Enabled = true;
        }

        private void EditDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(this.EditDgv.Columns[e.ColumnIndex].HeaderText + " is \" " + this.EditDgv.Columns[e.ColumnIndex].ValueType + "\". Data error. 请检查.");
            return;
        }

        private void EditDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn dgvc in this.EditDgv.Columns)
            {
                if (dgvc.ValueType == typeof(decimal))
                {
                    dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (dgvc.Name != "序号" && dgvc.Name != "材料代号" && dgvc.Name != "审核级别" && dgvc.Name != "规格(mm)")
                    {
                        dgvc.DefaultCellStyle.Format = "N2";
                    }
                }
            }
        }

        private void DataMaintenanceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }
    }
}