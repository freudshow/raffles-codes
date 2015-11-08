using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OracleClient;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;

namespace CAD_Practice
{
    public partial class NewForm : Form
    {
        public NewForm()
        {
            InitializeComponent();
        }
        ProjectQuota.ProjectInfo pi = new CAD_Practice.ProjectQuota.ProjectInfo();
        
        private void NewForm_Load(object sender, EventArgs e)
        {

            string[] projectlist = pi.GetProjectQuotas(); 
            foreach(string s  in projectlist )
            {
                cmbproject.Items.Add(s);
            }


        }

        private void cmbproject_SelectedIndexChanged(object sender, EventArgs e)
        {
            dg.Columns.Clear();
            cmbdiscip.Items.Clear();

            string projectlist = pi.GetProjectInfosQuota(cmbproject.Text);
            lblpname.Text  = cmbproject.Text;
            string [] stmp=  projectlist.Split(';');
            lblowner.Text = stmp[0];
            lblclass.Text = stmp[1];
            lblpfullname.Text = stmp[2];
            string[] disciplist = pi.GetDisciplineQuota (cmbproject.Text);
            foreach (string s in disciplist)
            {
                cmbdiscip.Items.Add(s);
            }

  
        }

        private void cmbdiscip_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet ds=pi.GetDrawingQuota(cmbproject.Text,cmbdiscip .Text );
            dg.AllowUserToAddRows = false;
            DataView dv = ds.Tables[0].DefaultView;
            dg.DataSource = dv;
            dg.RowHeadersVisible = false;
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg.MultiSelect = false;
            dg.ReadOnly = true;
            
            dg.Columns[0].HeaderText = "文档类型";
            dg.Columns[1].HeaderText = "烟台";
            dg.Columns[2].HeaderText = "海阳";
            dg.Columns[3].HeaderText = "龙口";
            dg.Columns[4].HeaderText = "外包";
            dg.Columns[5].HeaderText = "质检";
            dg.Columns[6].HeaderText = "项目";
            dg.Columns[7].HeaderText = "计划";
            dg.Columns[8].HeaderText = "总计";

            dg.Columns[9].HeaderText = "备注";
            dg.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dg.Columns[1].Width = 35;
            dg.Columns[2].Width = 35;
            dg.Columns[3].Width = 35;
            dg.Columns[4].Width = 35;
            dg.Columns[5].Width = 35;
            dg.Columns[6].Width = 35;
            dg.Columns[7].Width = 35;
            dg.Columns[8].Width = 35;
            dg.Columns[9].Width = 200;
          
            lblcount.Text ="文档记录数：" +ds.Tables[0].Rows.Count.ToString();
            

        }

        private void txtDrawNo_TextChanged(object sender, EventArgs e)
        {
            if (txtDrawNo.Text == "")
            {
                lblname1.Text = lblname2.Text = lblresuser.Text = "";
            }
            if (cmbproject.Text != "" && txtDrawNo.Text != "" && txtRev.Text != "")
            {
                string sdrawing =pi.GetDrawingTitleQuota (cmbproject.Text, txtDrawNo.Text, txtRev.Text);
                if (sdrawing == "")
                {
                    lblname1.Text = "你输入图号或版本号有误或与项目名不符，请重新输入！";
                     lblname1.ForeColor = System.Drawing.Color.Red;
                    lblname2.Text = lblresuser.Text = "";
                }
                else
                {
                    string[] stmp = sdrawing.Split(';');
                    lblname2.Text = stmp[0];
                    lblname1.Text = stmp[1];
                   
                    lblresuser.Text = stmp[2];
                }
            }
        }

        private void txtRev_TextChanged(object sender, EventArgs e)
        {
            if (txtRev.Text == "")
            {
                lblname1.Text = lblname2.Text = lblresuser.Text = "";
            }
            if (cmbproject.Text != "" && txtDrawNo.Text != "" && txtRev.Text != "")
            {
                string sdrawing = pi.GetDrawingTitleQuota (cmbproject.Text.Trim(), txtDrawNo.Text.Trim(), txtRev.Text.Trim());
                if (sdrawing == "")
                {
                    lblname1.Text = "你输入图号或版本号有误或与项目名不符，请重新输入！";
                    lblname1.ForeColor = System.Drawing.Color.Red;
                    lblname2.Text = lblresuser.Text = "";
                }
                else
                {
                    string[] stmp = sdrawing.Split(';');
                    lblname1.Text = stmp[0];
                    lblname2.Text = stmp[1];
                    lblresuser.Text = stmp[2];
                }
            }
        }

        //返回选择的图框模板
        public string GetRbCheck()
        {
            string stmplate=string.Empty;
            if (rba4.Checked)
                stmplate = rba4.Text;
            else if (rba3.Checked)
                stmplate = rba3.Text;
            else if (rba2.Checked)
                stmplate = rba2.Text;
            else if (rba1.Checked)
                stmplate = rba1.Text;
            else if (rba0.Checked)
                stmplate = rba0.Text;
            if (rbb4.Checked)
                stmplate = rbb4.Text;
            else if (rbb3.Checked)
                stmplate = rbb3.Text;
            else if (rbb2.Checked)
                stmplate = rbb2.Text;
            else if (rbb1.Checked)
                stmplate = rbb1.Text;
            else if (rbb0.Checked)
                stmplate = rbb0.Text;
            if (rbc4.Checked)
                stmplate = rbc4.Text;
            else if (rbc3.Checked)
                stmplate = rbc3.Text;
            else if (rbc2.Checked)
                stmplate = rbc2.Text;
            else if (rbc1.Checked)
                stmplate = rbc1.Text;
            else if (rbc0.Checked)
                stmplate = rbc0.Text;
            return stmplate.Trim();
           
        }

        private void btnok_Click(object sender, EventArgs e)
        {
           
            //判断输入数据是否为空
            if (lblname1.Text == "" || cmbdiscip.Text == "" || total =="" || txtDrawNo.Text == "" || txtRev.Text == "" )
            {
                MessageBox.Show("输入信息不完善！", "Error");
            }
            else
            {
                this.Hide();
                SetData();
            }
         }

        // 插入图框
        public void SetData( )
        {

            string stmplate = string.Empty;
            stmplate = GetRbCheck();
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
             DocumentLock docLock = doc.LockDocument();
            Editor ed = doc.Editor;

            string PathName = @"\\172.16.7.55\dt$\" + stmplate + ".dwg";
            ed.WriteMessage(PathName);
            try
            {
                using (Database dbsource = new Database(false, false))
                {
                    dbsource.ReadDwgFile(PathName, System.IO.FileShare.Read, true, null);

                    PromptPointOptions pmops = new PromptPointOptions("please select a point :");
                    PromptPointResult pmres;
                    pmres = ed.GetPoint(pmops);
                    Point3d insertPt = pmres.Value;
                    ed.WriteMessage(insertPt[0].ToString());
                    ObjectId blockId = ObjectId.Null;

                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        #region 根据选择图框类型，来更新信息并插入图框块

                        //7种标准的+5种只更新图号和版本=12
                        if (stmplate == "A4封面" || stmplate == "A3封面" || stmplate == "A2封面" || stmplate == "A1封面" || stmplate == "A0封面" || stmplate == "修改通知单" || stmplate == "修改通知单(加急)" || stmplate == "A4内容标题框" || stmplate == "A3内容标题框" || stmplate == "A2内容标题框" || stmplate == "A1内容标题框" || stmplate == "A0内容标题框")
                        {

                            blockId = db.Insert("tk", dbsource, false);
                            BlockTableRecord btupdate = (BlockTableRecord)tr.GetObject(blockId, OpenMode.ForWrite);
                            foreach (ObjectId otmp in btupdate)
                            {
                                DBObject dbo = tr.GetObject(otmp, OpenMode.ForWrite);
                                if (dbo is Autodesk.AutoCAD.DatabaseServices.DBText)
                                {

                                    DBText mText = (DBText)dbo;
                                    switch (mText.TextString)
                                    {
                                        case "Input Project Name Here":
                                            mText.TextString = lblpfullname.Text;
                                            break;

                                        case "Input Owner Here":
                                            mText.TextString = lblowner.Text;
                                            break;
                                        case "YRO***-***-***":
                                            mText.TextString = txtDrawNo.Text;
                                            break;
                                        case "0":
                                            mText.TextString = txtRev.Text;
                                            break;

                                    }


                                }
                                else if (dbo is Autodesk.AutoCAD.DatabaseServices.MText)
                                {
                                    MText mText = (MText)dbo;

                                    switch (mText.Contents)
                                    {
                                        case "图纸标题":

                                            mText.Contents = lblname1.Text;
                                            break;
                                        case "Input Title Here":
                                            mText.Contents = lblname2.Text;
                                            break;
                                        case "Input Class Here":
                                            mText.Contents = lblclass.Text;
                                            break;
                                    }
                                }
                            }

                            if (stmplate == "A4封面" || stmplate == "A3封面" || stmplate == "A2封面" || stmplate == "A1封面" || stmplate == "A0封面" || stmplate == "修改通知单" || stmplate == "修改通知单(加急)")
                            {
                                double qtnum = 0.0; ;

                                if (stmplate == "A4封面" || stmplate == "修改通知单(加急)" || stmplate == "修改通知单")
                                    qtnum = 63.3;
                                else if (stmplate == "A2封面" || stmplate == "A1封面" || stmplate == "A0封面")
                                    qtnum = 68.5;
                                else
                                    qtnum = 66.3;

                                Point3d insertionPointnums = new Point3d(0, 0, 0);
                                DBText txt = new DBText();
                                try
                                {
                                    if (yt != "")
                                    {
                                        insertionPointnums = new Point3d(19, qtnum - 4.5, 0);
                                        txt = new DBText();
                                        txt.TextString = yt;
                                        txt.Position = insertionPointnums;

                                        btupdate.AppendEntity(txt);
                                        tr.AddNewlyCreatedDBObject(txt, true);
                                    }
                                    if (hy != "")
                                    {
                                        insertionPointnums = new Point3d(19, qtnum - 4.5 * 2, 0);
                                        txt = new DBText();
                                        txt.TextString = hy;
                                        txt.Position = insertionPointnums;
                                        btupdate.AppendEntity(txt);
                                        tr.AddNewlyCreatedDBObject(txt, true);
                                    }
                                    if (lk != "")
                                    {
                                        insertionPointnums = new Point3d(19, qtnum - 4.5 * 3, 0);
                                        txt.TextString = lk;
                                        txt.Position = insertionPointnums;
                                        btupdate.AppendEntity(txt);
                                        tr.AddNewlyCreatedDBObject(txt, true);
                                    }
                                    if (om != "")
                                    {
                                        insertionPointnums = new Point3d(19, qtnum - 4.5 * 4, 0);
                                        txt.TextString = om;
                                        txt.Position = insertionPointnums;
                                        btupdate.AppendEntity(txt);
                                        tr.AddNewlyCreatedDBObject(txt, true);
                                    }
                                    if (qc != "")
                                    {
                                        insertionPointnums = new Point3d(19, qtnum - 4.5 * 5, 0);
                                        txt = new DBText();
                                        txt.TextString = qc;
                                        txt.Position = insertionPointnums;
                                        btupdate.AppendEntity(txt);
                                        tr.AddNewlyCreatedDBObject(txt, true);
                                    }
                                    if (xm != "")
                                    {
                                        insertionPointnums = new Point3d(19, qtnum - 4.5 * 6, 0);
                                        txt = new DBText();
                                        txt.TextString = xm;
                                        txt.Position = insertionPointnums;
                                        btupdate.AppendEntity(txt);
                                        tr.AddNewlyCreatedDBObject(txt, true);
                                    }
                                    if (pc != "")
                                    {
                                        insertionPointnums = new Point3d(19, qtnum - 4.5 * 7, 0);
                                        txt.TextString = om;
                                        txt.Position = insertionPointnums;
                                        btupdate.AppendEntity(txt);
                                        tr.AddNewlyCreatedDBObject(txt, true);
                                    }
                                    if (total != "")
                                    {
                                        insertionPointnums = new Point3d(19, qtnum - 4.5 * 9, 0);
                                        txt = new DBText();
                                        txt.TextString = total;
                                        txt.Position = insertionPointnums;
                                        btupdate.AppendEntity(txt);
                                        tr.AddNewlyCreatedDBObject(txt, true);
                                    }

                                }
                                catch (Autodesk.AutoCAD.Runtime.Exception re)
                                {
                                    MessageBox.Show(re.Message);
                                }



                            }

                        }

                        //else if (stmplate == "管路小票标题框" || stmplate == "修改通知单A4标题框" || stmplate == "修改通知单A3标题框") { }



                        BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                        BlockReference bref = new BlockReference(insertPt, blockId);

                        btr.AppendEntity(bref);
                        tr.AddNewlyCreatedDBObject(bref, true);
                        tr.Commit();


                        // bref.ExplodeToOwnerSpace();

                        #endregion
                    }
                }

                #region 插入打印记录
                string sp = string.Empty;
                sp = cmbproject.Text + ";" + txtDrawNo.Text + ";" + txtRev.Text + ";" + yt + ";" + hy + ";" + lk + ";" + om + ";" + qc + ";" + xm + ";" + pc + ";" + total;
                if (pi.GetDrawingPrintAddQuota(sp) != "0")
                    MessageBox.Show("已插入图框模板，并添加了此图号的打印申请");
                #endregion
            }

            catch (System.Exception et)
            {
                MessageBox.Show(et.Message);
            }
            finally
            {
                docLock.Dispose();

                this.Show();
            }
        
        }

        string yt,hy,lk,xm,qc,pc,om,total=string.Empty ;

        private void dg_SelectionChanged(object sender, EventArgs e)
        {
            yt = hy = lk = xm = qc = pc = om = total = "";
            DataGridViewRow dgr = dg.CurrentRow;

            System.Diagnostics.Trace.WriteLine(dgr.Cells[0].Value + "," + dgr.Cells[1].Value);
            lbldesc.Text = "备注：" + dgr.Cells[9].Value.ToString();
            yt = dgr.Cells[1].Value.ToString(); hy = dgr.Cells[2].Value.ToString(); lk = dgr.Cells[3].Value.ToString();
            om = dgr.Cells[4].Value.ToString(); qc = dgr.Cells[5].Value.ToString(); xm = dgr.Cells[6].Value.ToString();
            pc = dgr.Cells[7].Value.ToString(); total = dgr.Cells[8].Value.ToString();
            
            
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.Exit();

        }



    }




    //public class DataAccess
    //{

    //    public static string connectionString = "Data Source=OIDS;User ID=plm;Password=123!feed;Unicode=True";
    //    public static List<string> GetProject()
    //    {
    //        OracleConnection cn = new OracleConnection(DataAccess.connectionString);
    //        string strcmd = "select distinct (p.name)  from drawing_ifc_quota_new_tab t, project_tab p  where p.id = t.project_id   order by p.name ";
    //        OracleCommand cmd = new OracleCommand(strcmd, cn);
    //        cn.Open();
    //        OracleDataReader odr = cmd.ExecuteReader();
    //        List<string> lst = new List<string>();
    //        while (odr.Read())
    //        {
    //            lst.Add(odr[0].ToString());
    //        }
    //        odr.Close();
    //        cn.Close();
    //        return lst;
    //    }
    //    public static string GetProjectInfos(string project)
    //    {
    //        OracleConnection cn = new OracleConnection(DataAccess.connectionString);
    //        string strcmd = "select owner,class,full_name from PLM.PROJECT_TAB  where  name = '" + project + "'";
    //        OracleCommand cmd = new OracleCommand(strcmd, cn);
    //        cn.Open();
    //        OracleDataReader odr = cmd.ExecuteReader();
    //        StringBuilder sbProject = new StringBuilder();
    //        while (odr.Read())
    //        {
    //            sbProject.AppendFormat("{0};{1};{2}", odr[0].ToString(), odr[1].ToString(), odr[2].ToString());
    //        }
    //        odr.Close();
    //        cn.Close();
    //        return sbProject.ToString();
    //    }
    //    public static List<string> GetDiscipline(string project)
    //    {
    //        OracleConnection cn = new OracleConnection(DataAccess.connectionString);
    //        string strcmd = " select distinct(t.discipline_quota) from drawing_ifc_quota_new_tab t where PLM.PROJECT_API.Get_PROJECT_NAME(project_id)='" + project + "' order by t.discipline_quota ";
    //        OracleCommand cmd = new OracleCommand(strcmd, cn);
    //        cn.Open();
    //        OracleDataReader odr = cmd.ExecuteReader();
    //        List<string> lst = new List<string>();
    //        while (odr.Read())
    //        {
    //            lst.Add(odr[0].ToString());
    //        }
    //        odr.Close();
    //        cn.Close();
    //        return lst;
    //    }
    //    public static string GetDrawingInfo(string projectName, string drawingNo, string rev)
    //    {
    //        OracleConnection cn = new OracleConnection(DataAccess.connectionString);
    //        string strcmd = "SELECT DRAWING_TITLE_CN,DRAWING_TITLE , RESPONSIBLE_USER FROM PLM.PROJECT_DRAWING_TAB WHERE TRIM(LOWER(DRAWING_NO))='" + drawingNo.ToLower() + "' AND REVISION='" + rev + "' AND DELETE_FLAG='N' AND PLM.PROJECT_API.Get_PROJECT_NAME(PROJECT_ID)='" + projectName + "'";
    //        OracleCommand cmd = new OracleCommand(strcmd, cn);
    //        cn.Open();
    //        OracleDataReader odr = cmd.ExecuteReader();
    //        StringBuilder sb = new StringBuilder();
    //        while (odr.Read())
    //        {
    //            sb.AppendFormat("{0};{1};{2}", odr[0], odr[1], odr[2]);
    //        }
    //        odr.Close();
    //        cn.Close();
    //        return sb.ToString();
    //    }

    //    public static DataSet  GetDrawingQuota(string projectid, string disciplineid)
    //    {
    //        OracleConnection cn = new OracleConnection(DataAccess.connectionString);
    //        string sql = "SELECT p.DOC_DESCRIPTION ,p.DISPENSE_YT,p.DISPENSE_HY,p.DISPENSE_LC,p.DISPENSE_OM,p.DISPENSE_QC,p.DISPENSE_XM,p.DISPENSE_PC,p.DISPENSE_ALL   ,p.MARK  FROM PLM.DRAWING_IFC_QUOTA_NEW_TAB p  WHERE PLM.PROJECT_API.Get_PROJECT_NAME(project_id) ='" + projectid + "'  and  discipline_quota='" + disciplineid  + "'  and DELETEFLAG='N' ORDER BY doc_description ";
    //        OracleDataAdapter oadapt = new OracleDataAdapter(sql, cn);
    //        DataSet ds = new DataSet();
    //         oadapt.Fill(ds);
    //         return ds;
    //    }

        

        


    //}
}

