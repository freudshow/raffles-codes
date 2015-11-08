using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Collections;
using System.IO;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;



namespace DetailInfo
{
    class RightClick
    {
        public static void AddPDFInfo(AxAcroPDFLib.AxAcroPDF pdf, TabControl tabcontrol)
        {
            pdf.Visible = false;
            Label l = new Label();
            l.Width = 500; l.Height = 50;
            l.Text = "暂没有查询到该小票二维图的相关数据！";
            Font f = new Font("宋体", 16, FontStyle.Bold);
            l.Font = f;

            pdf.Parent.Controls.Add(l);

            int x = tabcontrol.Width / 2 - 200;

            l.Location = new Point(x, 0);
            l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
        }
        public static void AddCGRInfo(AxVIA3DXMLPluginLib.AxVIA3DXMLPlugin cgr, TabControl tabcontrol)
        {
            cgr.Visible = false;
            Label l = new Label();
            l.Width = 500; l.Height = 50;
            l.Text = "暂没有查询到该小票三维模型相关数据！";
            Font f = new Font("宋体", 16, FontStyle.Bold);
            l.Font = f;
            cgr.Parent.Controls.Add(l);
            int x = tabcontrol.Width / 2 - 200;

            l.Location = new Point(x, 0);
            l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
        }

        public static void CellEvent_TabHop(TextBox tb1, TextBox tb2,TextBox tb3, TextBox tb4, TextBox tb5, TextBox tb6, TextBox tb7,TextBox tb8, System.Windows.Forms.TabControl tab,System.Windows.Forms.ComboBox cb, DataGridView dgv1, DataGridView dgv2, DataGridView dgv3, DataGridView dgv4, DataGridView dgv5, AxAcroPDFLib.AxAcroPDF pdf1, AxVIA3DXMLPluginLib.AxVIA3DXMLPlugin via, AxAcroPDFLib.AxAcroPDF pdf2, AxVIA3DXMLPluginLib.AxVIA3DXMLPlugin cat)
        {
            string spname = cb.SelectedItem.ToString();
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            string queryString = "SP_GetSpoolFieldContent";
            conn.Open();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spname;
            cmd.Parameters["spool_in"].Direction = System.Data.ParameterDirection.Input;
            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tb1.Text = dr.GetOracleString(0).ToString();
                    tb2.Text = dr.GetOracleString(1).ToString();
                    tb3.Text = dr.GetOracleString(2).ToString();
                    tb4.Text = dr.GetOracleString(3).ToString();
                    tb5.Text = dr.GetOracleString(4).ToString();
                    tb6.Text = dr.GetOracleString(5).ToString();
                    tb7.Text = dr.GetOracleString(6).ToString();
                    tb8.Text = dr.GetOracleString(7).ToString();
                }
                dr.Close();
                conn.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("数据库错误！");
                return;
            }
            
            //string querystr = "SP_GetSpoolMaterialDetail";
            //string tabt = tab.SelectedTab.Text.ToString();
            //if (tab.SelectedTab.Text == "材料信息")
            //{
            //    DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, dgv1);

            //}

            //else if (tab.SelectedTab.Text == "连接件信息")
            //{
            //    DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, dgv2);
            //}

            //else if (tab.SelectedTab.Text == "加工信息")
            //{

            //    DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, dgv3);

            //}

            //else if (tab.SelectedTab.Text == "焊接信息")
            //{

            //    DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, dgv4);

            //}
            //else if (tab.SelectedTab.Text == "相对坐标信息")
            //{

            //    DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, dgv5);

            //}
            //else if (tab.SelectedTab.Text == "二维图")
            //{
            //    string filepath = User.rootpath + "\\" + "temp";
            //    if (!Directory.Exists(filepath))//若文件夹不存在则新建文件夹   
            //    {
            //        Directory.CreateDirectory(filepath); //新建文件夹   
            //    }
                
            //    DataSet ds = new DataSet();
            //    string sqlstr = "select t.pdfpath from sp_pdf_tab t where t.spoolname = '" + cb.SelectedItem.ToString() + "' and t.flag = 'Y'";
            //    User.DataBaseConnect(sqlstr,ds);
            //    if (ds.Tables[0].Rows.Count != 0)
            //    {
            //        if (ds.Tables[0].Rows[0][0].ToString() != string.Empty)
            //        {
            //            string pdfpath = ds.Tables[0].Rows[0][0].ToString();
            //            ds.Dispose();
            //            string filenamestr = pdfpath.Substring(pdfpath.LastIndexOf("\\"));
            //            string despath = filepath + filenamestr;
            //            System.IO.File.Copy(pdfpath, despath, true);
            //            pdf1.src = despath;
            //        }
            //        else
            //        {
            //            AddPDFInfo(pdf1, tab);
            //        }
            //    }

            //    else
            //    {
            //        AddPDFInfo(pdf1, tab);
            //    }

            //}
            //else if (tab.SelectedTab.Text == "三维模型")
            //{
            //    string filepath = User.rootpath + "\\" + "temp";
            //    if (!Directory.Exists(filepath))//若文件夹不存在则新建文件夹   
            //    {
            //        Directory.CreateDirectory(filepath); //新建文件夹   
            //    }

            //    DataSet ds = new DataSet();
            //    string sqlstr = "select t.cgrpath from sp_cgr_tab t where t.spoolname = '" + cb.SelectedItem.ToString() + "' and t.flag = 'Y'";
            //    User.DataBaseConnect(sqlstr,ds);
            //    if (ds.Tables[0].Rows.Count != 0 )
            //    {
            //        if (ds.Tables[0].Rows[0][0].ToString() != string.Empty)
            //        {
            //            User.DataBaseConnect(sqlstr, ds);
            //            string cgrpath = ds.Tables[0].Rows[0][0].ToString();
            //            ds.Dispose();
            //            string filenamestr = cgrpath.Substring(cgrpath.LastIndexOf("\\"));
            //            string despath = filepath + filenamestr;
            //            System.IO.File.Copy(cgrpath, despath, true);
            //            via.DocumentFile = despath;
            //        }
            //        else 
            //        {
            //            AddCGRInfo(via,tab);
            //        }

            //    }
            //    else
            //    {
            //        AddCGRInfo(via, tab);
            //    }
            //}

            //else if (tab.SelectedTab.Text == "二维ISO图")
            //{
            //    using (OracleConnection connection = new OracleConnection(DataAccess.OIDSConnStr))
            //    {
            //        connection.Open();
            //        OracleCommand command = connection.CreateCommand();
            //        command.CommandText = "select * from  SP_LINE_ISOMETRIC_TAB WHERE 1=1  AND FLAG = 'Y' AND LINENAME = (SELECT LINENAME FROM SP_SPOOL_TAB WHERE SPOOLNAME = '" + cb.SelectedItem.ToString() + "' AND FLAG = 'Y')";
            //        OracleDataReader dr = command.ExecuteReader();
            //        string filepath = string.Empty;
            //        while (dr.Read())
            //        {
            //            if (dr["PDF_ISOMETRIC"] != null)//如果文章内容为空 不能转二进制
            //            {
            //                try
            //                {
            //                    byte[] b1 = (byte[])dr["PDF_ISOMETRIC"];

            //                    string pathstr = User.rootpath + "\\" + "temp";
            //                    if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
            //                    {
            //                        Directory.CreateDirectory(pathstr); //新建文件夹   
            //                    }

            //                    filepath = pathstr + "\\" + dr["LINENAME"] + ".pdf";
            //                    FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
            //                    BinaryWriter bw = new BinaryWriter(fs);
            //                    bw.Write(b1, 0, b1.Length);
            //                    bw.Close();
            //                    fs.Close();
            //                }
            //                catch (SystemException ex)
            //                {
            //                    return;
            //                }
            //            }
            //            pdf2.src = filepath;
            //        }
            //        dr.Close();
            //        if (pdf2.src == null)
            //        {
            //            pdf2.Visible = false;
            //            Label l = new Label();
            //            l.Width = 500; l.Height = 50;
            //            l.Text = "暂没有查询到相关的二维ISO图的数据！";
            //            Font f = new Font("宋体", 16, FontStyle.Bold);
            //            l.Font = f;
            //            pdf2.Parent.Controls.Add(l);
            //            int x = tab.Width / 2 - 200;

            //            l.Location = new Point(x, 0);
            //            l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
            //        }
            //    }

            //}

            //else if (tab.SelectedTab.Text == "三维ISO模型")
            //{
            //    using (OracleConnection connection = new OracleConnection(DataAccess.OIDSConnStr))
            //    {
            //        connection.Open();
            //        OracleCommand command = connection.CreateCommand();
            //        command.CommandText = "select * from  SP_LINE_ISOMETRIC_TAB WHERE 1=1  AND FLAG = 'Y' AND LINENAME = (SELECT LINENAME FROM SP_SPOOL_TAB WHERE SPOOLNAME = '" + cb.SelectedItem.ToString() + "' AND FLAG = 'Y')";
            //        OracleDataReader dr = command.ExecuteReader();
            //        string filepath = string.Empty;
            //        while (dr.Read())
            //        {
            //            if (dr["XML_ISOMETRIC"] != null)//如果文章内容为空 不能转二进制
            //            {
            //                try
            //                {
            //                    byte[] b1 = (byte[])dr["XML_ISOMETRIC"];

            //                    string pathstr = User.rootpath + "\\" + "temp";
            //                    if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
            //                    {
            //                        Directory.CreateDirectory(pathstr); //新建文件夹   
            //                    }

            //                    filepath = pathstr + "\\" + dr["LINENAME"] + ".cgr";
            //                    FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
            //                    BinaryWriter bw = new BinaryWriter(fs);
            //                    bw.Write(b1, 0, b1.Length);
            //                    bw.Close();
            //                    fs.Close();
            //                }
            //                catch (SystemException ex)
            //                {
            //                    return;
            //                }
            //            }
            //            cat.DocumentFile = filepath;
            //        }
            //        dr.Close();
            //        if (cat.DocumentFile == string.Empty)
            //        {
            //            cat.Visible = false;
            //            Label l = new Label();
            //            l.Width = 500; l.Height = 50;
            //            l.Text = "暂没有查询到相关三维ISO模型数据！";
            //            Font f = new Font("宋体", 16, FontStyle.Bold);
            //            l.Font = f;
            //            cat.Parent.Controls.Add(l);
            //            int x = tab.Width / 2 - 200;

            //            l.Location = new Point(x, 0);
            //            l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
            //        }
            //    }
            //}
        }

        public static void ProjectDrawingDetails_TabHop(int index, TextBox tb, System.Windows.Forms.ComboBox cb, TabControl tab, DataGridView dgv1, DataGridView dgv2, DataGridView dgv3, AxAcroPDFLib.AxAcroPDF pdf1, AxAcroPDFLib.AxAcroPDF pdf2, DataGridView dgv4, DataGridView dgv5, DataGridView dgv6, DataGridView dgv7, DataGridView dgv8, AxAcroPDFLib.AxAcroPDF pdf3, DataGridView dgv9)
        {
            string sqlstr = string.Empty;
            string pid = tb.Text.ToString();
            string drawingno = cb.SelectedItem.ToString();
            string querystr = "SP_GetDrawingDetail";
            string tabtext = tab.SelectedTab.Text.ToString();
            if (tab.SelectedTab.Text == "小票信息")
            {
                DBConnection.GetDrawingDetail(index, tabtext, querystr, pid, drawingno, dgv1);
            }

            else if (tab.SelectedTab.Text == "材料信息")
            {
                DBConnection.GetDrawingDetail(index, tabtext, querystr, pid, drawingno, dgv2);
            }

            else if (tab.SelectedTab.Text == "附件信息")
            {
                DBConnection.GetDrawingDetail(index, tabtext, querystr, pid, drawingno, dgv3);
            }
            else if (tab.SelectedTab.Text == "连接件信息")
            {
                DBConnection.GetDrawingDetail(index, tabtext, querystr, pid, drawingno, dgv9);
            }

            else if (tab.SelectedTab.Text == "图纸封面")
            {
                using (OracleConnection connection = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandText = "select * from  SP_CREATEPDFDRAWING WHERE 1=1 AND PROJECTID = '" + tb.Text.ToString() + "' AND DRAWINGNO = '" + cb.SelectedItem + "' AND FLAG = 'Y'";
                    OracleDataReader dr = command.ExecuteReader();
                    string filepath = string.Empty;
                    while (dr.Read())
                    {
                        if (dr["UPDATEDFRONTPAGE"] != null)//如果文章内容为空 不能转二进制
                        {
                            try
                            {
                                byte[] b1 = (byte[])dr["UPDATEDFRONTPAGE"];

                                string pathstr = User.rootpath + "\\" + "temp";
                                if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                                {
                                    Directory.CreateDirectory(pathstr); //新建文件夹   
                                }
                                DirectoryInfo dirInfo = new DirectoryInfo(pathstr);
                                FileInfo[] files = dirInfo.GetFiles();
                                foreach (FileInfo file in files)
                                {
                                    file.Delete();
                                }
                                filepath = pathstr + "\\" + dr["DRAWINGNO"] + ".pdf";
                                FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
                                BinaryWriter bw = new BinaryWriter(fs);
                                bw.Write(b1, 0, b1.Length);
                                bw.Close();
                                fs.Close();

                            }
                            catch (SystemException ex)
                            {
                                //MessageBox.Show("系统没有查询到相关文件，请与管理员联系导入文件");
                                return;
                            }
                        }
                        
                        pdf1.src = filepath;
                    }
                    dr.Close();
                    if (pdf1.src == null)
                    {
                        pdf1.Visible = false;
                        Label l = new Label();
                        l.Width = 500; l.Height = 50;
                        l.Text = "暂没有查询到该图纸封面的PDF格式文件！";
                        Font f = new Font("宋体", 16, FontStyle.Bold);
                        l.Font = f;
                        pdf1.Parent.Controls.Add(l);
                        int x = tab.Width / 2 - 200;

                        l.Location = new Point(x, 0);
                        l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
                    }
                }
            }

            else if (tab.SelectedTab.Text == "图纸")
            {
                string character = string.Empty;
                using (OracleConnection connection = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    //command.CommandText = "select * from  SP_CREATEPDFDRAWING WHERE 1=1 AND PROJECTID = '" + tb.Text.ToString() + "' AND DRAWINGNO = '" + cb.SelectedItem + "' AND FLAG = 'Y'";
                    if (index == 0)
                    {
                        command.CommandText = @"select * from (select *
          from sp_createpdfdrawing t
         where t.PDFDRAWING is not null
           and t.PROJECTID = '" + tb.Text.ToString() + "' AND t.DRAWINGNO = '" + cb.SelectedItem + "' order by t.createdate desc )  where rownum = 1";
                        character = "PDFDRAWING";
                    }
                    else if (index == 1)
                    {
                        command.CommandText = @"select * from (select *
          from sp_createpdfdrawing t
         where t.MODIFYDRAWINGS is not null
           and t.PROJECTID = '" + tb.Text.ToString() + "' AND t.DRAWINGNO = '" + cb.SelectedItem + "' order by t.createdate desc )  where rownum = 1";
                        character = "MODIFYDRAWINGS";
                    }

                    OracleDataReader dr = command.ExecuteReader();
                    string filepath = string.Empty;
                    while (dr.Read())
                    {
                        if (dr[character] != null)//如果文章内容为空 不能转二进制
                        {
                            try
                            {
                                byte[] b1 = (byte[])dr[character];

                                string pathstr = User.rootpath + "\\" + "temp";
                                if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                                {
                                    Directory.CreateDirectory(pathstr); //新建文件夹   
                                }
                                DirectoryInfo dirInfo = new DirectoryInfo(pathstr);
                                FileInfo[] files = dirInfo.GetFiles();
                                foreach (FileInfo file in files)
                                {
                                    file.Delete();
                                }
                                filepath = pathstr + "\\" + dr["DRAWINGNO"] + ".pdf";
                                FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
                                BinaryWriter bw = new BinaryWriter(fs);
                                bw.Write(b1, 0, b1.Length);
                                bw.Close();
                                fs.Close();
                            }
                            catch (SystemException ex)
                            {

                                //MessageBox.Show("系统没有查询到相关文件，请与管理员联系导入文件");
                                return;
                            }
                        }
                        pdf2.src = filepath;
                    }
                    dr.Close();
                    if (pdf2.src == null)
                    {
                        pdf2.Visible = false;
                        Label l = new Label();
                        l.Width = 500; l.Height = 50;
                        l.Text = "暂没有查询到该图纸的PDF格式文件！";

                        Font f = new Font("宋体", 16, FontStyle.Bold);
                        l.Font = f;
                        pdf2.Parent.Controls.Add(l);

                        int x = tab.Width / 2 -200;

                        l.Location = new Point(x, 0);
                        l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
                    }
                }
            }

            else if (tab.SelectedTab.Text == "材料附件")
            {
                string character = string.Empty;
                using (OracleConnection connection = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandText = "select * from  SP_CREATEPDFDRAWING WHERE 1=1 AND PROJECTID = '" + tb.Text.ToString() + "' AND DRAWINGNO = '" + cb.SelectedItem + "' AND FLAG = 'Y'";
                    if (index == 0)
                    {
                        command.CommandText = @"select * from (select *
          from sp_createpdfdrawing t
         where t.MATERIALPDF is not null
           and t.PROJECTID = '" + tb.Text.ToString() + "' AND t.DRAWINGNO = '" + cb.SelectedItem + "' order by t.createdate desc )  where rownum = 1";
                        character = "MATERIALPDF";
                    }
                    else if (index == 1)
                    {
                        command.CommandText = @"select * from (select *
          from sp_createpdfdrawing t
         where t.MODIFYMATERIALPDF is not null
           and t.PROJECTID = '" + tb.Text.ToString() + "' AND t.DRAWINGNO = '" + cb.SelectedItem + "' order by t.createdate desc )  where rownum = 1";
                        character = "MODIFYMATERIALPDF";
                    }

                    OracleDataReader dr = command.ExecuteReader();
                    string filepath = string.Empty;
                    while (dr.Read())
                    {
                        if (dr[character] != null)//如果文章内容为空 不能转二进制
                        {
                            try
                            {
                                byte[] b1 = (byte[])dr[character];

                                string pathstr = User.rootpath + "\\" + "temp";
                                if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                                {
                                    Directory.CreateDirectory(pathstr); //新建文件夹   
                                }
                                DirectoryInfo dirInfo = new DirectoryInfo(pathstr);
                                FileInfo[] files = dirInfo.GetFiles();
                                foreach (FileInfo file in files)
                                {
                                    file.Delete();
                                }
                                filepath = pathstr + "\\" + dr["DRAWINGNO"] + "-附页"+ ".pdf";
                                FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
                                BinaryWriter bw = new BinaryWriter(fs);
                                bw.Write(b1, 0, b1.Length);
                                bw.Close();
                                fs.Close();
                            }
                            catch (SystemException ex)
                            {

                                //MessageBox.Show("系统没有查询到相关文件，请与管理员联系导入文件");
                                return;
                            }
                        }
                        pdf3.src = filepath;
                    }
                    dr.Close();
                    if (pdf3.src == null)
                    {
                        pdf3.Visible = false;
                        Label l = new Label();
                        l.Width = 500; l.Height = 50;
                        l.Text = "暂没有查询到该图纸的PDF格式文件！";

                        Font f = new Font("宋体", 16, FontStyle.Bold);
                        l.Font = f;
                        pdf3.Parent.Controls.Add(l);

                        int x = tab.Width / 2 - 200;

                        l.Location = new Point(x, 0);
                        l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
                    }
                }
            }

            else if (tab.SelectedTab.Text == "附件材料表" || tab.SelectedTab.Text == "附件材料设备定额表" || tab.SelectedTab.Text == "管系托盘表" || tab.SelectedTab.Text == "管系材料表" || tab.SelectedTab.Text == "管系材料设备定额表")
            {
                string character = string.Empty;
                using (OracleConnection connection = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandText = "select * from  SP_CREATEPDFDRAWING WHERE FLAG='Y' AND PROJECTID = '" + tb.Text.ToString() + "' AND DRAWINGNO = '" + cb.SelectedItem + "'";
                    OracleDataReader dr = command.ExecuteReader();
                    string filepath = string.Empty;
                    if (index == 0)
                    {
                        character = "MATERIALINFO";
                    }
                    else if (index == 1)
                    {
                        character = "MODIFYMATERIALINFO";
                    }
                    while (dr.Read())
                    {
                        if (dr[character] != null)//如果文章内容为空 不能转二进制
                        {
                            try
                            {
                                byte[] b1 = (byte[])dr[character];

                                string pathstr = User.rootpath + "\\" + "temp";
                                if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                                {
                                    Directory.CreateDirectory(pathstr); //新建文件夹   
                                }
                                if (index == 0)
                                    filepath = pathstr + "\\" + dr["DRAWINGNO"] + ".xls";
                                else
                                    filepath = pathstr + "\\Modify_" + dr["DRAWINGNO"] + ".xls";
                                FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
                                BinaryWriter bw = new BinaryWriter(fs);
                                bw.Write(b1, 0, b1.Length);
                                bw.Close();
                                fs.Close();
                            }
                            catch (SystemException ex)
                            {
                                MessageBox.Show("系统没有查询到相关文件!");
                                return;
                            }
                            #region 读取数据
                            Excel.Application excel = null;
                            Excel.Workbooks wbs = null;
                            Excel.Workbook wb = null;
                            Excel.Worksheet ws = null;
                            Excel.Range range1 = null;
                            DataTable dt = new DataTable();
                            object Nothing = System.Reflection.Missing.Value;
                            try
                            {
                                excel = new Excel.Application();
                                excel.UserControl = true;
                                excel.DisplayAlerts = false;
                                excel.Application.Workbooks.Open(filepath, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);
                                wbs = excel.Workbooks;
                                wb = wbs[1];
                                if (tab.SelectedTab.Text == "附件材料表")
                                {
                                    ws = (Excel.Worksheet)wb.Worksheets["AccessoriesExcel"];
                                    int rowCount = ws.UsedRange.Rows.Count;
                                    object[] row = new object[6];
                                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("序号", typeof(int)), new DataColumn("附件名称", typeof(string)), new DataColumn("规格", typeof(string)), new DataColumn("数量", typeof(string)), new DataColumn("重量(kg)", typeof(string)), new DataColumn("备注", typeof(string)) });
                                    for (int i = 5; i < rowCount + 1; i++)
                                    {
                                        for (int j = 0; j < 6; j++)
                                        {
                                            range1 = ws.get_Range(ws.Cells[i, j + 1], ws.Cells[i, j + 1]);
                                            row[j] = range1.Value;
                                        }
                                        DataRow dr1 = dt.NewRow();
                                        dr1.ItemArray = row;
                                        dt.Rows.Add(dr1);
                                    }
                                    dgv4.DataSource = dt;
                                    dt.Dispose();
                                }
                                else if (tab.SelectedTab.Text == "附件材料设备定额表")
                                {
                                    ws = (Excel.Worksheet)wb.Worksheets["AccessoriesRation"];
                                    int rowCount = ws.UsedRange.Rows.Count;
                                    object[] row = new object[16];
                                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("序号", typeof(int)), new DataColumn("项目", typeof(string)), new DataColumn("系统", typeof(string)), new DataColumn("采购件号", typeof(string)), new DataColumn("ERP代码", typeof(string)), new DataColumn("材料名称规格及型号", typeof(string)), new DataColumn("申请号", typeof(string)), new DataColumn("数量", typeof(string)), new DataColumn("单位", typeof(string)), new DataColumn("积压库存使用(可/否)", typeof(string)), new DataColumn("用途", typeof(string)), new DataColumn("需求原因代码", typeof(string)), new DataColumn("需求原因", typeof(string)), new DataColumn("图纸代码/图纸名称", typeof(string)), new DataColumn("长度/m", typeof(string)), new DataColumn("数量2", typeof(string)) });
                                    for (int i = 4; i < rowCount - 4; i++)
                                    {
                                        for (int j = 0; j < 16; j++)
                                        {
                                            range1 = ws.get_Range(ws.Cells[i, j + 1], ws.Cells[i, j + 1]);
                                            row[j] = range1.Value;
                                        }
                                        DataRow dr1 = dt.NewRow();
                                        dr1.ItemArray = row;
                                        dt.Rows.Add(dr1);
                                    }
                                    dgv5.DataSource = dt;
                                    dt.Dispose();
                                }
                                else if (tab.SelectedTab.Text == "管系托盘表")
                                {
                                    ws = (Excel.Worksheet)wb.Worksheets["MaterialExcel"];
                                    int rowCount = ws.UsedRange.Rows.Count;
                                    object[] row = new object[15];
                                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("序号", typeof(int)), new DataColumn("管名", typeof(string)), new DataColumn("规格", typeof(string)), new DataColumn("材料", typeof(string)), new DataColumn("表面处理", typeof(string)), new DataColumn("校管", typeof(string)), new DataColumn("长度(mm)", typeof(string)), new DataColumn("重量(kg)", typeof(string)), new DataColumn("所属舱室", typeof(string)), new DataColumn("管路等级", typeof(string)), new DataColumn("试验场所及压力", typeof(string)), new DataColumn("备注", typeof(string)), new DataColumn("表面积(m^2)", typeof(string)), new DataColumn("修改原因", typeof(string)), new DataColumn("原因描述", typeof(string)) });
                                    for (int i = 5; i < rowCount + 1; i++)
                                    {
                                        for (int j = 0; j < 15; j++)
                                        {
                                            range1 = ws.get_Range(ws.Cells[i, j + 1], ws.Cells[i, j + 1]);
                                            row[j] = range1.Value;
                                        }
                                        DataRow dr1 = dt.NewRow();
                                        dr1.ItemArray = row;
                                        dt.Rows.Add(dr1);
                                    }
                                    dgv6.DataSource = dt;
                                    dt.Dispose();
                                }
                                else if (tab.SelectedTab.Text == "管系材料表")
                                {
                                    ws = (Excel.Worksheet)wb.Worksheets["TotalPipeMaterialExcel"];
                                    int rowCount = ws.UsedRange.Rows.Count;
                                    object[] row = new object[6];
                                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("序号", typeof(int)), new DataColumn("材料名称", typeof(string)), new DataColumn("规格", typeof(string)), new DataColumn("长度(mm)", typeof(string)), new DataColumn("重量(kg)", typeof(string)), new DataColumn("备注", typeof(string)) });
                                    for (int i = 5; i < rowCount + 1; i++)
                                    {
                                        for (int j = 0; j < 6; j++)
                                        {
                                            range1 = ws.get_Range(ws.Cells[i, j + 1], ws.Cells[i, j + 1]);
                                            row[j] = range1.Value;
                                        }
                                        DataRow dr1 = dt.NewRow();
                                        dr1.ItemArray = row;
                                        dt.Rows.Add(dr1);
                                    }
                                    dgv7.DataSource = dt;
                                    dt.Dispose();
                                }
                                else if (tab.SelectedTab.Text == "管系材料设备定额表")
                                {
                                    ws = (Excel.Worksheet)wb.Worksheets["PipeRation"];
                                    int rowCount = ws.UsedRange.Rows.Count;
                                    object[] row = new object[17];
                                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("序号", typeof(int)), new DataColumn("项目", typeof(string)), new DataColumn("系统", typeof(string)), new DataColumn("采购件号", typeof(string)), new DataColumn("MSS号", typeof(string)), new DataColumn("ERP代码", typeof(string)), new DataColumn("材料名称规格及型号", typeof(string)), new DataColumn("申请号", typeof(string)), new DataColumn("数量", typeof(string)), new DataColumn("单位", typeof(string)), new DataColumn("积压库存使用(可/否)", typeof(string)), new DataColumn("用途", typeof(string)), new DataColumn("需求原因代码", typeof(string)), new DataColumn("需求原因", typeof(string)), new DataColumn("图纸代码/图纸名称", typeof(string)), new DataColumn("长度/m", typeof(string)), new DataColumn("数量2", typeof(string)) });
                                    for (int i = 4; i < rowCount - 4; i++)
                                    {
                                        for (int j = 0; j < 17; j++)
                                        {
                                            range1 = ws.get_Range(ws.Cells[i, j + 1], ws.Cells[i, j + 1]);
                                            row[j] = range1.Value;
                                        }
                                        DataRow dr1 = dt.NewRow();
                                        dr1.ItemArray = row;
                                        dt.Rows.Add(dr1);
                                    }
                                    dgv8.DataSource = dt;
                                    dt.Dispose();
                                }
                            }
                            finally
                            {
                                if (excel != null)
                                {
                                    if (wbs != null)
                                    {
                                        if (wb != null)
                                        {
                                            if (ws != null)
                                            {
                                                if (range1 != null)
                                                {
                                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(range1);
                                                    range1 = null;
                                                }
                                                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                                                ws = null;
                                            }
                                            wb.Close(false, Nothing, Nothing);
                                            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                                            wb = null;
                                        }
                                        wbs.Close();
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wbs);
                                        wbs = null;
                                    }
                                    excel.Application.Workbooks.Close();
                                    excel.Quit();
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                                    excel = null;
                                    GC.Collect();
                                }
                            }
                            #endregion
                        }
                    }
                    dr.Close();
                    switch (tab.SelectedTab.Text)
                    {
                        case "附件材料表":
                            if (dgv4.DataSource == null)
                            {
                                dgv4.Visible = false;
                                Label l = new Label();
                                l.Width = 500; l.Height = 50;
                                l.Text = "暂没有查询到该图纸的" + tab.SelectedTab.Text + "！";

                                Font f = new Font("宋体", 16, FontStyle.Bold);
                                l.Font = f;
                                dgv4.Parent.Controls.Add(l);

                                int x = tab.Width / 2 - 200;

                                l.Location = new Point(x, 0);
                                l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
                            }
                            break;
                        case "附件材料设备定额表":
                            if (dgv5.DataSource == null)
                            {
                                dgv5.Visible = false;
                                Label l = new Label();
                                l.Width = 500; l.Height = 50;
                                l.Text = "暂没有查询到该图纸的" + tab.SelectedTab.Text + "！";

                                Font f = new Font("宋体", 16, FontStyle.Bold);
                                l.Font = f;
                                dgv5.Parent.Controls.Add(l);

                                int x = tab.Width / 2 - 200;

                                l.Location = new Point(x, 0);
                                l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
                            }
                            break;
                        case "管系托盘表":
                            if (dgv6.DataSource == null)
                            {
                                dgv6.Visible = false;
                                Label l = new Label();
                                l.Width = 500; l.Height = 50;
                                l.Text = "暂没有查询到该图纸的" + tab.SelectedTab.Text + "！";

                                Font f = new Font("宋体", 16, FontStyle.Bold);
                                l.Font = f;
                                dgv6.Parent.Controls.Add(l);

                                int x = tab.Width / 2 - 200;

                                l.Location = new Point(x, 0);
                                l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
                            }
                            break;
                        case "管系材料表":
                            if (dgv7.DataSource == null)
                            {
                                dgv7.Visible = false;
                                Label l = new Label();
                                l.Width = 500; l.Height = 50;
                                l.Text = "暂没有查询到该图纸的" + tab.SelectedTab.Text + "！";

                                Font f = new Font("宋体", 16, FontStyle.Bold);
                                l.Font = f;
                                dgv7.Parent.Controls.Add(l);

                                int x = tab.Width / 2 - 200;

                                l.Location = new Point(x, 0);
                                l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
                            }
                            break;
                        case "管系材料设备定额表":
                            if (dgv8.DataSource == null)
                            {
                                dgv8.Visible = false;
                                Label l = new Label();
                                l.Width = 500; l.Height = 50;
                                l.Text = "暂没有查询到该图纸的" + tab.SelectedTab.Text + "！";

                                Font f = new Font("宋体", 16, FontStyle.Bold);
                                l.Font = f;
                                dgv8.Parent.Controls.Add(l);

                                int x = tab.Width / 2 - 200;

                                l.Location = new Point(x, 0);
                                l.Anchor = AnchorStyles.Left; l.Anchor = AnchorStyles.Right; l.Anchor = AnchorStyles.Top;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static void ProjectDrawingVersion_TabHop(TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, TextBox tb6, TextBox tb7, TextBox tb8, System.Windows.Forms.TabControl tab, System.Windows.Forms.ComboBox cb1, System.Windows.Forms.ComboBox cb2,DataGridView dgv1, DataGridView dgv2, DataGridView dgv3, DataGridView dgv4, DataGridView dgv5, AxAcroPDFLib.AxAcroPDF pdf1, AxVIA3DXMLPluginLib.AxVIA3DXMLPlugin via, AxAcroPDFLib.AxAcroPDF pdf2, AxVIA3DXMLPluginLib.AxVIA3DXMLPlugin cat)
        {
            string spname = cb1.Text.ToString();
            int version = Convert.ToInt16( cb2.SelectedItem.ToString());
            int cb2count = cb2.Items.Count;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            string queryString = "SP_GetHistorySpoolFieldContent";
            conn.Open();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spname;
            cmd.Parameters["spool_in"].Direction = System.Data.ParameterDirection.Input;

            cmd.Parameters.Add("version_in", OracleType.Number).Value = version;
            cmd.Parameters["version_in"].Direction = System.Data.ParameterDirection.Input;

            cmd.Parameters.Add("cb2count_in", OracleType.Number).Value = cb2count;
            cmd.Parameters["cb2count_in"].Direction = System.Data.ParameterDirection.Input;

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tb1.Text = dr.GetOracleString(0).ToString();
                    tb2.Text = dr.GetOracleString(1).ToString();
                    tb3.Text = dr.GetOracleString(2).ToString();
                    tb4.Text = dr.GetOracleString(3).ToString();
                    tb5.Text = dr.GetOracleString(4).ToString();
                    tb6.Text = dr.GetOracleString(5).ToString();
                    tb7.Text = dr.GetOracleString(6).ToString();
                    tb8.Text = dr.GetOracleString(7).ToString();
                }
                dr.Close();
                conn.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            string sqlstr = string.Empty;
            string querystr = "SP_GetSpoolMaterialDetail";
            string tabt = tab.SelectedTab.Text.ToString();
            if (tab.SelectedTab.Text == "材料信息")
            {
                DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, version,cb2count,dgv1);
            }

            else if (tab.SelectedTab.Text == "连接件信息")
            {
                DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, version, cb2count, dgv2);
            }

            else if (tab.SelectedTab.Text == "加工信息")
            {
                DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, version, cb2count, dgv3);
            }

            else if (tab.SelectedTab.Text == "焊接信息")
            {
                DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, version, cb2count, dgv4);
            }
            else if (tab.SelectedTab.Text == "相对坐标信息")
            {
                DBConnection.GetSpoolMaterialDetail(querystr, tabt, spname, version, cb2count, dgv5);
            }
            else if (tab.SelectedTab.Text == "二维图")
            {
                
                string filepath = User.rootpath + "\\" + "temp";
                if (!Directory.Exists(filepath))//若文件夹不存在则新建文件夹   
                {
                    Directory.CreateDirectory(filepath); //新建文件夹   
                }

                DataSet ds = new DataSet();
                if (cb2count == version + 1)
                {
                    sqlstr = @"select t.pdfpath from sp_pdf_tab t where t.spoolname = '" + cb1.SelectedItem.ToString() + "' and t.flag = 'Y'";
                }
                else
                {
                    sqlstr = @"select t.pdfpath from sp_pdf_tab t where t.ID = (select s.sp_id from sp_spool_tab s where s.revision = '"+version+"'  and s.flowstatus >= 2 and s.flowstatus != 3 and s.spoolname = '" + cb1.SelectedItem.ToString() + "')";
                }
                //sqlstr = "select t.pdfpath from sp_pdf_tab t where t.spoolname = '" + cb1.SelectedItem.ToString() + "' and t.flag = 'Y'";
                User.DataBaseConnect(sqlstr, ds);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() != string.Empty)
                    {
                        string pdfpath = ds.Tables[0].Rows[0][0].ToString();
                        ds.Dispose();
                        string filenamestr = pdfpath.Substring(pdfpath.LastIndexOf("\\"));
                        string despath = filepath + filenamestr;
                        System.IO.File.Copy(pdfpath, despath, true);
                        pdf1.Visible = true;
                        pdf1.src = despath;
                    }
                    else
                    {
                        AddPDFInfo(pdf1, tab);
                    }
                }

                else
                {
                    AddPDFInfo(pdf1, tab);
                }
            }

            else if (tab.SelectedTab.Text == "三维模型")
            {
                string filepath = User.rootpath + "\\" + "temp";
                if (!Directory.Exists(filepath))//若文件夹不存在则新建文件夹   
                {
                    Directory.CreateDirectory(filepath); //新建文件夹   
                }

                DataSet ds = new DataSet();
                if (cb2count == version + 1)
                {
                    sqlstr = "select t.cgrpath from sp_cgr_tab t where t.spoolname = '" + cb1.SelectedItem.ToString() + "' and t.flag = 'Y'";
                }
                else
                {
                    sqlstr = @"select t.cgrpath from sp_cgr_tab t where t.ID = (select s.sp_id from sp_spool_tab s where s.revision = '" + version + "'  and s.flowstatus >= 2 and s.flowstatus != 3 and s.spoolname = '" + cb1.SelectedItem.ToString() + "')";
                }
                //string sqlstr = "select t.cgrpath from sp_cgr_tab t where t.spoolname = '" + cb.SelectedItem.ToString() + "' and t.flag = 'Y'";
                User.DataBaseConnect(sqlstr, ds);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() != string.Empty)
                    {
                        User.DataBaseConnect(sqlstr, ds);
                        string cgrpath = ds.Tables[0].Rows[0][0].ToString();
                        ds.Dispose();
                        string filenamestr = cgrpath.Substring(cgrpath.LastIndexOf("\\"));
                        string despath = filepath + filenamestr;
                        System.IO.File.Copy(cgrpath, despath, true);
                        via.Visible = true;
                        via.DocumentFile = despath;
                    }
                    else
                    {
                        AddCGRInfo(via, tab);
                    }

                }
                else
                {
                    AddCGRInfo(via, tab);
                }
            }

        }


    
    }

}
   
