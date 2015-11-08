using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.IO;
namespace jzpl.Lib
{
    public class Misc
    {
        public Misc()
        {

        }

        public static string GetHtmlRequestValue(HttpRequest request, string name)
        {
            string ret = "";
            if (request == null) return ret;
            if (request.HttpMethod == "GET")
            {
                if (request.QueryString[name] != null) ret = request.QueryString[name];
            }
            else
            {
                if (request.Form[name] != null) ret = request.Form[name];
            }
            return ret;
        }

        public static void Message(HttpResponse response,string msg)
        {
            response.Write("<script type=\"text/javascript\">");
            response.Write(string.Format("alert('{0}')", msg));
            response.Write("</script>");
            response.Write("<script>document.location=document.location;</script>");
        }

        public static void Message(Type t,ClientScriptManager cs,string msg)
        {
            cs.RegisterStartupScript(t,"提示", string.Format("<script>alert('{0}')</script>",msg));
        }

        public static void RegisterClientScript(Type t, string key, ClientScriptManager cs, string script)
        {
            cs.RegisterStartupScript(t, key, script);
        }

        public static string GetErrorStrFromDBException(string errMsg)
        {
            const string ERR_START_STRING = "ORA-2000:";
            int from_;
            int to_;
            string errMsgTmp_;
            from_ = errMsg.IndexOf(ERR_START_STRING) + ERR_START_STRING.Length+1;
            errMsgTmp_ = errMsg.Substring(from_);
            to_ = errMsgTmp_.IndexOf('\n');
            return errMsgTmp_.Substring(0, to_);
        }

        public static bool DBDataToTxtFile(string sqlstr, string path)
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(Lib.DBHelper.OleConnectionString);
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(sqlstr,conn);
            System.Data.OleDb.OleDbDataReader reader;
            conn.Open();
            reader = cmd.ExecuteReader();

            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
            System.IO.StreamWriter wr = new System.IO.StreamWriter(fs);
            int _filedCount = reader.FieldCount;
            for (int i = 0; i < _filedCount; i++)
            {
                wr.Write(reader.GetName(i));
                if (i < reader.FieldCount - 1)
                {
                    wr.Write("\t");
                }
            }
            wr.WriteLine(); 
            while (reader.Read())
            {
                for (int i = 0; i < _filedCount; i++)
                {
                    if (!reader.IsDBNull(i))
                    {
                        string s;
                        s = reader.GetDataTypeName(i);
                        if (s == "DBTYPE_I4")
                        {
                            wr.Write(reader.GetInt32(i).ToString());
                        }
                        else if (s == "DBTYPE_DATE")
                        {
                            wr.Write(reader.GetDateTime(i).ToString("d"));
                        }
                        else if (s == "DBTYPE_WVARCHAR")
                        {
                            wr.Write(reader.GetString(i));
                        }
                        else if(s=="DBTYPE_R8")
                        {
                            wr.Write(reader.GetDouble(i));
                        }
                        else if (s == "DBTYPE_VARCHAR")
                        {
                            wr.Write(reader.GetString(i));
                        }
                    }
                    if (i < _filedCount-1) wr.Write("\t");
                }
                wr.WriteLine(); 
            } 
            wr.Flush();
            fs.Close();
            return true;
            
        }
        public static bool DBDataToXls(string sqlstr, string path)
        {
            // Excel object references.
            Excel.Application m_objExcel = null;
            Excel.Workbooks m_objBooks = null;
            Excel._Workbook m_objBook = null;
            Excel.Sheets m_objSheets = null;
            Excel._Worksheet m_objSheet = null;
            Excel.Range m_objRange = null;
            Excel.Font m_objFont = null;
            //Excel.QueryTables m_objQryTables = null;
            //Excel._QueryTable m_objQryTable = null;

            // Frequenty-used variable for optional arguments.
            object m_objOpt = System.Reflection.Missing.Value;

            // Paths used by the sample code for accessing and storing data.
            object m_strSampleFolder = "C:\\ExcelData\\";

            m_objExcel = new Excel.Application();
            m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
            m_objBook = (Excel._Workbook)(m_objBooks.Add(m_objOpt));
            m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
            m_objSheet = (Excel._Worksheet)(m_objSheets.get_Item(1));
            
            OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlstr,conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = null;
            int fieldCount_;
            int rowsCount_;
            object[] objHeader;
            object[,] objData;

            da.Fill(ds);
            dt = ds.Tables[0];            
            fieldCount_ = dt.Columns.Count;
            rowsCount_ = dt.Rows.Count;

            if (rowsCount_ == 0) return false;

            m_objRange = m_objSheet.get_Range(m_objSheet.Cells[1, 1], m_objSheet.Cells[1, fieldCount_]);
            objHeader = new string[fieldCount_];
            for (int i = 0; i < fieldCount_; i++)
            {
                objHeader[i] = dt.Columns[i].ColumnName;
            }
            m_objRange.set_Value(m_objOpt, objHeader);
            m_objFont = m_objRange.Font;
            m_objFont.Bold = true;
            m_objFont.Size = 9;
            m_objFont.Name = "MS Sans Serif";
            //object[rowscount,columnscount]

            objData = new object[rowsCount_, fieldCount_];
            for (int i = 0; i < rowsCount_; i++)
            {
                for (int j = 0; j < fieldCount_; j++)
                {
                    objData[i, j] = dt.Rows[i][j];
                }
            }

            m_objRange = m_objSheet.get_Range("A2", m_objOpt);
            m_objRange = m_objRange.get_Resize(rowsCount_, fieldCount_);
            m_objRange.NumberFormatLocal = "@";
            m_objRange.set_Value(m_objOpt, objData);
            m_objFont = m_objRange.Font;
            m_objFont.Size = 9;
            m_objFont.Name = "MS Sans Serif";


            m_objBook.SaveAs(path, m_objOpt, m_objOpt,
                m_objOpt, m_objOpt, m_objOpt, Excel.XlSaveAsAccessMode.xlNoChange,
                m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);
            m_objBook.Close(false, m_objOpt, m_objOpt);
            m_objExcel.Quit();
            return true;
        }

        public static decimal DBStrToNumber(string NumStr)
        {
            try
            {
                if (NumStr == "") return 0;
                return Convert.ToDecimal(NumStr);
            }
            catch (Exception)
            {                
                throw new Exception("字符串数值转换错误。"+NumStr);
            }
        }

        public static Boolean CheckNumber(string num)
        {
            try
            {
                if (num == "") return true;
                decimal temp = Convert.ToDecimal(num);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CheckIsDBCustomException(Exception ex)
        {
            if (ex.Message.IndexOf("ORA-2") != -1) return true;
            return false;
        }
        public static string GetDBCustomException(Exception ex)
        {
            int begin_;
            int end_;

            begin_ = ex.Message.IndexOf("ORA-2");

            end_ = ex.Message.IndexOf("_end_");

            if (begin_ == -1) begin_ = 0;
            if (end_ == -1) end_ = 100;

            return ex.Message.Substring(begin_, end_ - begin_);

        }
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char)(c[i] - 65248);
                }
            }

            return new string(c);
        }
    }
}
