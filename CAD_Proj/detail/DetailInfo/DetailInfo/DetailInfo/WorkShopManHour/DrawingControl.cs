using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

namespace DetailInfo
{
    public partial class DrawingControl : UserControl
    {
        public DrawingControl()
        {
            InitializeComponent();
        }
        string sqlStr = string.Empty;
        string projectStr = string.Empty;
        string drawingStr = string.Empty;
        string spoolStr = string.Empty;
        private void DrawingControl_Load(object sender, EventArgs e)
        {
            //sqlStr = @"SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82)   ORDER BY NAME";
            sqlStr = @"SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82) and NAME IN (SELECT DISTINCT S.PROJECTID　FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y')   ORDER BY NAME";
            DetailInfo.Application_Code.FillComboBox.FillComb(this.ProjectComboBox, sqlStr);
            this.CalcBtn.Enabled = false;
        }

        private void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DrawingComboBox.Items.Clear();
            projectStr = this.ProjectComboBox.Text.ToString();
            //sqlStr = "select drawing_no from project_drawing_tab where project_id = (select id from project_tab where name = '" + projectStr + "') and lastflag = 'Y' and delete_flag = 'N' and ISSUED_TIME is not null";
            sqlStr = "SELECT DRAWING_NO FROM PLM.PROJECT_DRAWING_TAB where drawing_type is null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + projectStr + "') AND DOCTYPE_ID IN (7)  AND DOCTYPE_ID != 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' ORDER BY DRAWING_ID DESC";
            DetailInfo.Application_Code.FillComboBox.FillComb(this.DrawingComboBox, sqlStr);
            //this.CalcBtn.Enabled = true;
        }

        public bool IsNumberic(string s)
        {
            string text1 = @"^\-?[0-9]+$";
            return Regex.IsMatch(s, text1);
            //return Regex.IsMatch(s, RegexManager.GetRegex(ValidateType.IntZeroPostive).Regex);
        }

        private void DrawingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CalcBtn.Enabled = true;
        }

        private void CalcBtn_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            ThreadStart threadstart = new ThreadStart(Calculate);
            Thread thread = new Thread(threadstart);
            thread.IsBackground = true;
            thread.Start();

            //this.CalcBtn.Enabled = false;
            //string frmtext = this.ParentForm.Text.ToString();
            //projectStr = this.ProjectComboBox.Text.ToString();
            //drawingStr = this.DrawingComboBox.Text.ToString().Trim();
            //string queryStr = string.Empty;
            //if (string.IsNullOrEmpty(projectStr) || string.IsNullOrEmpty(drawingStr))
            //{
            //    MessageBox.Show("项目和图纸号都不能为空!", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //DataSet SpoolDS;
            //switch (frmtext)
            //{
            //    case "下料工时定额":
            //        queryStr = "SP_GetPipeMaterialDS";
            //        SpoolDS = new DataSet();
            //        SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS" ,projectStr, drawingStr);
            //        if (SpoolDS.Tables[0].Rows.Count == 0)
            //        {
            //            MessageBox.Show("数据库暂没有查询到该图纸信息！","信息提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //            return;
            //        }
            //        for (int j = 0; j < SpoolDS.Tables[0].Rows.Count; j++)
            //        {
            //            spoolStr = SpoolDS.Tables[0].Rows[j][0].ToString();
            //            DataSet myDS;
            //            myDS = ManHourManage.GetPipeMaterialDS(queryStr, projectStr, spoolStr);
            //            int bevelcount = 0;
            //            int elbowcount = 0;
            //            for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
            //            {
            //                double norm = Convert.ToDouble(myDS.Tables[0].Rows[i][2].ToString());
            //                if (myDS.Tables[0].Rows[i][3].ToString() != "0")
            //                {
            //                    bevelcount = Convert.ToInt16(myDS.Tables[0].Rows[i][3].ToString());
            //                }
            //                if (myDS.Tables[0].Rows[i][4].ToString() != "0")
            //                {
            //                    elbowcount = Convert.ToInt16(myDS.Tables[0].Rows[i][4].ToString());
            //                }
            //                string pipeStr = myDS.Tables[0].Rows[i][5].ToString();
            //                string factorStr = ManHourManage.GetPreMaterialFacor(norm);
            //                string pipcount = ManHourManage.GetPipeCount(projectStr, spoolStr, norm);
            //                string[] factorname = factorStr.Split(new char[] { '-' });
            //                double totaltime = Convert.ToDouble(factorname[0]) * Convert.ToInt16(pipcount) + Convert.ToDouble(factorname[1]) * bevelcount + Convert.ToDouble(factorname[2]) * elbowcount;
            //                ManHourManage.UpdateMaterialPrepareTime(projectStr, spoolStr, pipeStr, totaltime);
            //            }
            //            myDS.Dispose();
            //        }
            //        SpoolDS.Dispose();
            //        MessageBox.Show("-------------完成------------");
            //        break;

            //    case "装配工时定额":
            //        SpoolDS = new DataSet();
            //        SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS" ,projectStr, drawingStr);
            //        if (SpoolDS.Tables[0].Rows.Count == 0)
            //        {
            //            MessageBox.Show("没有查询到该图纸相关小票信息，请与设计员联系！","信息提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //            return;
            //        }
            //        for (int i = 0; i < SpoolDS.Tables[0].Rows.Count; i++)
            //        {
            //            double assTime = 0;
            //            spoolStr = SpoolDS.Tables[0].Rows[i][0].ToString();
            //            /*******************************计算法兰工时*********************************/
            //            DataSet flangeDS = new DataSet();
            //            flangeDS = ManHourManage.GetNormCount("SP_GetFlangeNormCount", projectStr, spoolStr);
            //            for (int f = 0; f < flangeDS.Tables[0].Rows.Count; f++)
            //            {
            //                double flange = Convert.ToDouble(flangeDS.Tables[0].Rows[f][0].ToString());
            //                if (flange == 0)
            //                {
            //                    continue;
            //                }
            //                int flangCount = Convert.ToInt16(flangeDS.Tables[0].Rows[f][1].ToString());
            //                double flangefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetFlangeFactor",flange));
            //                assTime += flangefactor * flangCount;
            //            }
            //            flangeDS.Dispose();

            //            /*******************************计算定型弯头工时******************************/
            //            DataSet elbowDS = new DataSet();
            //            elbowDS = ManHourManage.GetNormCount("SP_GetElbowNormCount", projectStr, spoolStr);
            //            for (int b = 0; b < elbowDS.Tables[0].Rows.Count; b++)
            //            {
            //                double elbow = Convert.ToDouble(elbowDS.Tables[0].Rows[b][0].ToString());
            //                if (elbow == 0)
            //                {
            //                    continue;
            //                }
            //                int elbowCount = Convert.ToInt16(elbowDS.Tables[0].Rows[b][1].ToString());
            //                double elbowfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetElbowFactor",elbow));
            //                assTime += elbowfactor * elbowCount;
            //            }
            //            elbowDS.Dispose();

            //            /****************************计算承插弯头弯头工时**************************/
            //            DataSet inelbowDS = new DataSet();
            //            inelbowDS = ManHourManage.GetNormCount("SP_GetInElbowNormCount", projectStr, spoolStr);
            //            for (int ib = 0; ib < inelbowDS.Tables[0].Rows.Count; ib++)
            //            {
            //                double inelbow = Convert.ToDouble(inelbowDS.Tables[0].Rows[ib][0].ToString());
            //                if (inelbow == 0)
            //                {
            //                    continue;
            //                }
            //                int inelbowCount = Convert.ToInt16(inelbowDS.Tables[0].Rows[ib][1].ToString());
            //                double inelbowfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetInElbowFactor", inelbow));
            //                assTime += inelbowfactor * inelbowCount;

            //            }
            //            inelbowDS.Dispose();

            //            /*******************************计算异径工时******************************/
            //            DataSet reducerDS = new DataSet();
            //            reducerDS = ManHourManage.GetNormCount("SP_GetReducerNormCount", projectStr, spoolStr);
            //            for (int r = 0; r < reducerDS.Tables[0].Rows.Count; r++)
            //            {
            //                double reducer = Convert.ToDouble(reducerDS.Tables[0].Rows[r][0].ToString());
            //                if (reducer == 0)
            //                {
            //                    continue;
            //                }
            //                int reducerCount = Convert.ToInt16(reducerDS.Tables[0].Rows[r][1].ToString());
            //                double reducerfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetReducerFactor",reducer));
            //                assTime += reducerfactor * reducerCount;
            //            }
            //            reducerDS.Dispose();
            //            /*******************************计算承插三通工时******************************/
            //            DataSet insertedpassDS = new DataSet();
            //            insertedpassDS = ManHourManage.GetNormCount("SP_GetInsertedPassNormCount", projectStr, spoolStr);
            //            for (int c = 0; c < insertedpassDS.Tables[0].Rows.Count; c++)
            //            {
            //                double insertedpass = Convert.ToDouble(insertedpassDS.Tables[0].Rows[c][0].ToString());
            //                if (insertedpass == 0)
            //                {
            //                    continue;
            //                }
            //                int insertedpassCount = Convert.ToInt16(insertedpassDS.Tables[0].Rows[c][1].ToString());
            //                double insertedfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetInsertedPassFactor", insertedpass));
            //                assTime += insertedfactor * insertedpassCount * 3;
            //            }
            //            insertedpassDS.Dispose();
            //            /*******************************计算对接三通工时******************************/
            //            DataSet dockpassDS = new DataSet();
            //            dockpassDS = ManHourManage.GetNormCount("SP_GetDockPassNormCount", projectStr, spoolStr);
            //            for (int d = 0; d < dockpassDS.Tables[0].Rows.Count; d++)
            //            {
            //                double dockpass = Convert.ToDouble(dockpassDS.Tables[0].Rows[d][0].ToString());
            //                if (dockpass == 0)
            //                {
            //                    continue;
            //                }
            //                int dockpassCount = Convert.ToInt16(dockpassDS.Tables[0].Rows[d][1].ToString());
            //                double dockpassfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetDockPassFactor", dockpass));
            //                assTime += dockpassfactor * dockpassCount * 3;
            //            }
            //            dockpassDS.Dispose();
            //            /*******************************计算通舱套筒工时******************************/
            //            DataSet tcasingDS = new DataSet();
            //            tcasingDS = ManHourManage.GetNormCount("SP_GetTCasingNormCount", projectStr, spoolStr);
            //            for (int t = 0; t < tcasingDS.Tables[0].Rows.Count; t++)
            //            {
            //                double tcasing = Convert.ToDouble(tcasingDS.Tables[0].Rows[t][0].ToString());
            //                if (tcasing == 0)
            //                {
            //                    continue;
            //                }
            //                int tcasingCount = Convert.ToInt16(tcasingDS.Tables[0].Rows[t][1].ToString());
            //                double tcasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetTCasingFactor", tcasing));
            //                assTime += tcasingfactor * tcasingCount * 2;
            //            }
            //            tcasingDS.Dispose();
            //            /*******************************计算连接套筒工时******************************/
            //            DataSet lcasingDS = new DataSet();
            //            lcasingDS = ManHourManage.GetNormCount("SP_GetLCasingNormCount", projectStr, spoolStr);
            //            for (int t1 = 0; t1 < lcasingDS.Tables[0].Rows.Count; t1++)
            //            {
            //                double lcasing = Convert.ToDouble(lcasingDS.Tables[0].Rows[t1][0].ToString());
            //                if (lcasing == 0)
            //                {
            //                    continue;
            //                }
            //                int lcasingCount = Convert.ToInt16(lcasingDS.Tables[0].Rows[t1][1].ToString());
            //                double lcasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetTCasingFactor", lcasing));
            //                assTime += lcasingfactor * lcasingCount;
            //            }
            //            lcasingDS.Dispose();
            //            /*******************************计算承插套筒工时******************************/
            //            DataSet icasingDS = new DataSet();
            //            icasingDS = ManHourManage.GetNormCount("SP_GetICasingNormCount", projectStr, spoolStr);
            //            for (int t2 = 0; t2 < icasingDS.Tables[0].Rows.Count; t2++)
            //            {
            //                string icasingStr = icasingDS.Tables[0].Rows[t2][0].ToString();
            //                double icasing = Convert.ToDouble(icasingStr.Replace("DN",""));
            //                if (icasing == 0)
            //                {
            //                    continue;
            //                }
            //                int icasingCount = Convert.ToInt16(icasingDS.Tables[0].Rows[t2][1].ToString());
            //                double icasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetICasingFactor", icasing));
            //                assTime += icasingfactor * icasingCount;
            //            }
            //            icasingDS.Dispose();
            //            /*******************************计算支管工时******************************/
            //            DataSet branchDS = new DataSet();
            //            branchDS = ManHourManage.GetNormCount("SP_GetBranchNormCount", projectStr, spoolStr);
            //            for (int b = 0; b < branchDS.Tables[0].Rows.Count; b++)
            //            {
            //                double branch = Convert.ToDouble(branchDS.Tables[0].Rows[b][0].ToString());
            //                if (branch == 0)
            //                {
            //                    continue;
            //                }
            //                int branchCount = Convert.ToInt16(branchDS.Tables[0].Rows[b][1].ToString());
            //                double branchfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetBranchFactor", branch));
            //                assTime += branchfactor * branchCount;
            //            }
            //            branchDS.Dispose();

            //            /******************************计算焊接座工时*****************************/
            //            DataSet weldbaseDS = new DataSet();
            //            weldbaseDS = ManHourManage.GetNormCount("SP_GetWeldBaseNormCount", projectStr, spoolStr);
            //            for (int wd = 0; wd < weldbaseDS.Tables[0].Rows.Count; wd++)
            //            {
            //                string weldbaseStr = weldbaseDS.Tables[0].Rows[wd][0].ToString();
            //                //if (Convert.ToDouble(weldbaseStr) == 0)
            //                if (weldbaseStr == "0")
            //                {
            //                    continue;
            //                }
            //                int weldbaseCount = Convert.ToInt16(weldbaseDS.Tables[0].Rows[wd][1].ToString());
            //                double weldbasefactor = 0;
            //                if (weldbaseStr.Contains("DN"))
            //                {
            //                    double weldbase = Convert.ToDouble(weldbaseStr.Replace("DN", "").Trim());
            //                    weldbasefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetWBDNFactor", weldbase));
                                
            //                }
            //                else
            //                {
            //                    weldbasefactor = Convert.ToDouble(ManHourManage.GetMaterialStrFactor("SP_GetWBFactor", weldbaseStr));
            //                }
            //                assTime += weldbasefactor * weldbaseCount;
            //            }
            //            weldbaseDS.Dispose();

            //            /*******************************计算腹板工时******************************/
            //            DataSet webplateDS = new DataSet();
            //            webplateDS = ManHourManage.GetNormCount("SP_GetWebPlateNormCount",projectStr,spoolStr);
            //            for (int w = 0; w < webplateDS.Tables[0].Rows.Count; w++)
            //            {
            //                double webplate = Convert.ToDouble(webplateDS.Tables[0].Rows[w][0].ToString());
            //                if (webplate == 0)
            //                {
            //                    continue;
            //                }
            //                int webplateCount = Convert.ToInt16(webplateDS.Tables[0].Rows[w][1].ToString());
            //                double webplatefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetWebPlateFactor", webplate));
            //                assTime += webplatefactor * webplateCount;
            //            }
            //            webplateDS.Dispose();
            //            /*******************************计算打磨工时*******************************/
            //            //string checkfield = ManHourManage.JudgePipeCheckField(projectStr, spoolStr);
            //            //if (checkfield == "内场" )
            //            //{
            //            string normStr = ManHourManage.GetPipMaxNorm(projectStr, spoolStr);
            //            double norm = Convert.ToDouble(normStr);
            //            double polishfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetPrePolishFactor", norm));
            //            assTime += polishfactor;
            //            //}
            //            //MessageBox.Show(spoolStr + "-----"+ assTime.ToString());
            //            ManHourManage.UpdateSpoolQuotaTime(frmtext, projectStr, spoolStr, assTime);
            //        }
            //        SpoolDS.Dispose();
            //        MessageBox.Show("-------------完成------------");
            //        break;

            //    case "焊接工时定额":
            //        queryStr = "SP_GetPipeMaterialDS";
            //        SpoolDS = new DataSet();
            //        SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS" ,projectStr, drawingStr);
            //        if (SpoolDS.Tables[0].Rows.Count == 0)
            //        {
            //            MessageBox.Show("没有查询到该图纸相关小票信息，请与设计员联系！","信息提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //            return;
            //        }
                    
            //        for (int i = 0; i < SpoolDS.Tables[0].Rows.Count; i++)
            //        {
            //            double assTime = 0;
            //            double polishfactor = 0;
            //            spoolStr = SpoolDS.Tables[0].Rows[i][0].ToString();
            //            /*******************************计算法兰工时*********************************/
            //            DataSet flangeDS = new DataSet();
            //            flangeDS = ManHourManage.GetNormCount("SP_GetFlangeNormCount", projectStr, spoolStr);
            //            for (int f = 0; f < flangeDS.Tables[0].Rows.Count; f++)
            //            {
            //                double flange = Convert.ToDouble(flangeDS.Tables[0].Rows[f][0].ToString());
            //                if (flange == 0 || flange == 63)
            //                {
            //                    continue;
            //                }
            //                int flangCount = Convert.ToInt16(flangeDS.Tables[0].Rows[f][1].ToString());
            //                double flangefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetFlangeFactor", flange));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetDNPolishFactor", flange));

            //                assTime += (flangefactor + polishfactor) * flangCount;
            //            }
            //            flangeDS.Dispose();

            //            /*******************************计算定型弯头工时******************************/
            //            DataSet elbowDS = new DataSet();
            //            elbowDS = ManHourManage.GetNormCount("SP_GetElbowNormCount", projectStr, spoolStr);
            //            for (int b = 0; b < elbowDS.Tables[0].Rows.Count; b++)
            //            {
            //                double elbow = Convert.ToDouble(elbowDS.Tables[0].Rows[b][0].ToString());
            //                if (elbow == 0)
            //                {
            //                    continue;
            //                }
            //                int elbowCount = Convert.ToInt16(elbowDS.Tables[0].Rows[b][1].ToString());
            //                double elbowfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetElbowFactor", elbow));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", elbow));
            //                //MessageBox.Show("计算定型弯头工时" + polishfactor.ToString());
            //                assTime += (elbowfactor + polishfactor) * elbowCount;
            //            }
            //            elbowDS.Dispose();
            //            /****************************计算承插弯头弯头工时**************************/
            //            DataSet inelbowDS = new DataSet();
            //            inelbowDS = ManHourManage.GetNormCount("SP_GetInElbowNormCount", projectStr, spoolStr);
            //            for (int ib = 0; ib < inelbowDS.Tables[0].Rows.Count; ib++)
            //            {
            //                if (IsNumberic(inelbowDS.Tables[0].Rows[ib][0].ToString()) == true)
            //                {


            //                    double inelbow = Convert.ToDouble(inelbowDS.Tables[0].Rows[ib][0].ToString());
            //                    if (inelbow == 0)
            //                    {
            //                        continue;
            //                    }
            //                    int inelbowCount = Convert.ToInt16(inelbowDS.Tables[0].Rows[ib][1].ToString());
            //                    double inelbowfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetInElbowFactor", inelbow));
            //                    polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetDNPolishFactor", inelbow));
            //                    assTime += (inelbowfactor + polishfactor) * inelbowCount;
            //                }
            //                else
            //                {
            //                    continue;
            //                }

            //            }
            //            inelbowDS.Dispose();

            //            /*******************************计算异径工时******************************/
            //            DataSet reducerDS = new DataSet();
            //            reducerDS = ManHourManage.GetNormCount("SP_GetReducerNormCount", projectStr, spoolStr);
            //            for (int r = 0; r < reducerDS.Tables[0].Rows.Count; r++)
            //            {
            //                double reducer = Convert.ToDouble(reducerDS.Tables[0].Rows[r][0].ToString());
            //                if (reducer == 0)
            //                {
            //                    continue;
            //                }
            //                int reducerCount = Convert.ToInt16(reducerDS.Tables[0].Rows[r][1].ToString());
            //                double reducerfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetReducerFactor", reducer));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", reducer));
            //                //MessageBox.Show("计算异径工时" + polishfactor.ToString());
            //                assTime += (reducerfactor + polishfactor) * reducerCount;
            //            }
            //            reducerDS.Dispose();
            //            /*******************************计算承插三通工时******************************/
            //            DataSet insertedpassDS = new DataSet();
            //            insertedpassDS = ManHourManage.GetNormCount("SP_GetInsertedPassNormCount", projectStr, spoolStr);
            //            for (int c = 0; c < insertedpassDS.Tables[0].Rows.Count; c++)
            //            {
            //                double insertedpass = Convert.ToDouble(insertedpassDS.Tables[0].Rows[c][0].ToString());
            //                if (insertedpass == 0)
            //                {
            //                    continue;
            //                }
            //                int insertedpassCount = Convert.ToInt16(insertedpassDS.Tables[0].Rows[c][1].ToString());
            //                double insertedfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetInsertedPassFactor", insertedpass));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetDNPolishFactor", insertedpass));
            //                assTime += (insertedfactor + polishfactor) * insertedpassCount * 3;
            //            }
            //            insertedpassDS.Dispose();
            //            /*******************************计算对接三通工时******************************/
            //            DataSet dockpassDS = new DataSet();
            //            dockpassDS = ManHourManage.GetNormCount("SP_GetDockPassNormCount", projectStr, spoolStr);
            //            for (int d = 0; d < dockpassDS.Tables[0].Rows.Count; d++)
            //            {
            //                if (dockpassDS.Tables[0].Rows[d][0].ToString() == string.Empty)
            //                {
            //                    continue;
            //                }
            //                double dockpass = Convert.ToDouble(dockpassDS.Tables[0].Rows[d][0].ToString());
            //                if (dockpass == 0)
            //                {
            //                    continue;
            //                }
            //                int dockpassCount = Convert.ToInt16(dockpassDS.Tables[0].Rows[d][1].ToString());
            //                double dockpassfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetDockPassFactor", dockpass));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", dockpass));
            //                //MessageBox.Show("计算对接三通工时" + polishfactor.ToString());
            //                assTime += (dockpassfactor + polishfactor) * dockpassCount * 3;
            //            }
            //            dockpassDS.Dispose();

            //            /*******************************计算通舱套筒工时******************************/
            //            DataSet tcasingDS = new DataSet();
            //            tcasingDS = ManHourManage.GetNormCount("SP_GetTCasingNormCount", projectStr, spoolStr);
            //            for (int t = 0; t < tcasingDS.Tables[0].Rows.Count; t++)
            //            {
            //                double tcasing = Convert.ToDouble(tcasingDS.Tables[0].Rows[t][0].ToString());
            //                if (tcasing == 0)
            //                {
            //                    continue;
            //                }
            //                int tcasingCount = Convert.ToInt16(tcasingDS.Tables[0].Rows[t][1].ToString());
            //                double tcasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetTCasingFactor", tcasing));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", tcasing));
            //                //MessageBox.Show("计算通舱套筒工时" + polishfactor.ToString());
            //                assTime += (tcasingfactor + polishfactor) * tcasingCount * 2;
            //            }
            //            tcasingDS.Dispose();

            //            /*******************************计算连接套筒工时******************************/
            //            DataSet lcasingDS = new DataSet();
            //            lcasingDS = ManHourManage.GetNormCount("SP_GetLCasingNormCount", projectStr, spoolStr);
            //            for (int t1 = 0; t1 < lcasingDS.Tables[0].Rows.Count; t1++)
            //            {
            //                double lcasing = Convert.ToDouble(lcasingDS.Tables[0].Rows[t1][0].ToString());
            //                if (lcasing == 0)
            //                {
            //                    continue;
            //                }
            //                int lcasingCount = Convert.ToInt16(lcasingDS.Tables[0].Rows[t1][1].ToString());
            //                double lcasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetTCasingFactor", lcasing));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", lcasing));
            //                //MessageBox.Show("计算连接套筒工时" + polishfactor.ToString());
            //                assTime += (lcasingfactor + polishfactor) * lcasingCount;
            //            }
            //            lcasingDS.Dispose();

            //            /*******************************计算承插套筒工时******************************/
            //            DataSet icasingDS = new DataSet();
            //            icasingDS = ManHourManage.GetNormCount("SP_GetICasingNormCount", projectStr, spoolStr);
            //            for (int t2 = 0; t2 < icasingDS.Tables[0].Rows.Count; t2++)
            //            {
            //                string icasingStr = icasingDS.Tables[0].Rows[t2][0].ToString();
            //                double icasing = Convert.ToDouble(icasingStr.Replace("DN", ""));
            //                if (icasing == 0)
            //                {
            //                    continue;
            //                }
            //                int icasingCount = Convert.ToInt16(icasingDS.Tables[0].Rows[t2][1].ToString());
            //                double icasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetICasingFactor", icasing));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetDNPolishFactor", icasing));
                            
            //                assTime += (icasingfactor + polishfactor) * icasingCount;
            //            }
            //            icasingDS.Dispose();

            //            /*******************************计算支管工时******************************/
            //            DataSet branchDS = new DataSet();
            //            branchDS = ManHourManage.GetNormCount("SP_GetBranchNormCount", projectStr, spoolStr);
            //            for (int b = 0; b < branchDS.Tables[0].Rows.Count; b++)
            //            {
            //                double branch = Convert.ToDouble(branchDS.Tables[0].Rows[b][0].ToString());
            //                if (branch == 0)
            //                {
            //                    continue;
            //                }
            //                int branchCount = Convert.ToInt16(branchDS.Tables[0].Rows[b][1].ToString());
            //                double branchfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetBranchFactor", branch));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", branch));
            //                //MessageBox.Show("计算支管工时" + polishfactor.ToString());
            //                assTime += (branchfactor + polishfactor) * branchCount;
            //            }
            //            branchDS.Dispose();

            //            /*******************************计算腹板工时******************************/
            //            DataSet webplateDS = new DataSet();
            //            webplateDS = ManHourManage.GetNormCount("SP_GetWebPlateNormCount", projectStr, spoolStr);
            //            for (int w = 0; w < webplateDS.Tables[0].Rows.Count; w++)
            //            {
            //                double webplate = Convert.ToDouble(webplateDS.Tables[0].Rows[w][0].ToString());
            //                if (webplate == 0)
            //                {
            //                    continue;
            //                }
            //                int webplateCount = Convert.ToInt16(webplateDS.Tables[0].Rows[w][1].ToString());
            //                double webplatefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext,"SP_GetWebPlateFactor", webplate));
            //                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", webplate));
            //                //MessageBox.Show("计算腹板工时" + polishfactor.ToString());
            //                assTime += (webplatefactor + polishfactor) * webplateCount;
            //            }
            //            webplateDS.Dispose();
            //            //MessageBox.Show(spoolStr + "-----" + assTime.ToString());
            //            ManHourManage.UpdateSpoolQuotaTime(frmtext, projectStr, spoolStr, assTime);
            //        }
            //        SpoolDS.Dispose();
            //        MessageBox.Show("------完成------");
            //        break;

            //    case "报验工时定额":
            //        #region 报验工时计算
            //        SpoolDS = new DataSet();
            //        SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS", projectStr, drawingStr);
            //        for (int i = 0; i < SpoolDS.Tables[0].Rows.Count; i++)
            //        {
            //            spoolStr = SpoolDS.Tables[0].Rows[i][0].ToString();
            //            //string checkfield = ManHourManage.JudgePipeCheckField(projectStr, spoolStr);
            //            //if (checkfield == "外场")
            //            //{
            //            //    continue;
            //            //}
            //            string normStr = ManHourManage.GetPipMaxNorm(projectStr, spoolStr);
            //            //MessageBox.Show(spoolStr);
            //            //MessageBox.Show(normStr.ToString());
            //            double norm = Convert.ToDouble(normStr);
            //            string factorStr = ManHourManage.GetQCORTransORPresFactor(norm);
            //            string[] factorname = factorStr.Split(new char[] { '-' });
            //            double toqcTime = Convert.ToDouble(factorname[0]);
            //            ManHourManage.UpdateSpoolQuotaTime(frmtext, projectStr, spoolStr, toqcTime);

            //        }
            //        SpoolDS.Dispose();
            //        MessageBox.Show("-------------完成------------");
            //        #endregion
            //        break;

            //    case "料场工时定额":
            //        #region 料场公式计算
            //        SpoolDS = new DataSet();
            //        SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS", projectStr, drawingStr);
            //        for (int i = 0; i < SpoolDS.Tables[0].Rows.Count; i++)
            //        {
            //            spoolStr = SpoolDS.Tables[0].Rows[i][0].ToString();
            //            string normStr = ManHourManage.GetPipMaxNorm(projectStr, spoolStr);
            //            double norm = Convert.ToDouble(normStr);
            //            string factorStr = ManHourManage.GetQCORTransORPresFactor(norm);
            //            string[] factorname = factorStr.Split(new char[] { '-' });
            //            double tranTime = Convert.ToDouble(factorname[1]);
            //            ManHourManage.UpdateSpoolQuotaTime(frmtext, projectStr, spoolStr, tranTime);
            //        }
            //        SpoolDS.Dispose();
            //        MessageBox.Show("-------------完成------------");
            //        #endregion
            //        break;

            //    default:
            //        break;
            //} 
        }

        public void Calculate()
        {
            //this.CalcBtn.Enabled = false;
            string frmtext = this.ParentForm.Text.ToString();
            projectStr = this.ProjectComboBox.Text.ToString();
            drawingStr = this.DrawingComboBox.Text.ToString().Trim();
            string queryStr = string.Empty;
            if (string.IsNullOrEmpty(projectStr) || string.IsNullOrEmpty(drawingStr))
            {
                MessageBox.Show("项目和图纸号都不能为空!", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataSet SpoolDS;
            switch (frmtext)
            {
                case "下料工时定额":
                    queryStr = "SP_GetPipeMaterialDS";
                    SpoolDS = new DataSet();
                    SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS", projectStr, drawingStr);
                    if (SpoolDS.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("数据库暂没有查询到该图纸信息！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    for (int j = 0; j < SpoolDS.Tables[0].Rows.Count; j++)
                    {
                        spoolStr = SpoolDS.Tables[0].Rows[j][0].ToString();
                        DataSet myDS;
                        myDS = ManHourManage.GetPipeMaterialDS(queryStr, projectStr, spoolStr);
                        int bevelcount = 0;
                        int elbowcount = 0;
                        for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                        {
                            double norm = Convert.ToDouble(myDS.Tables[0].Rows[i][2].ToString());
                            if (myDS.Tables[0].Rows[i][3].ToString() != "0")
                            {
                                bevelcount = Convert.ToInt16(myDS.Tables[0].Rows[i][3].ToString());
                            }
                            if (myDS.Tables[0].Rows[i][4].ToString() != "0")
                            {
                                elbowcount = Convert.ToInt16(myDS.Tables[0].Rows[i][4].ToString());
                            }
                            string pipeStr = myDS.Tables[0].Rows[i][5].ToString();
                            string factorStr = ManHourManage.GetPreMaterialFacor(norm);
                            string pipcount = ManHourManage.GetPipeCount(projectStr, spoolStr, norm);
                            string[] factorname = factorStr.Split(new char[] { '-' });
                            double totaltime = Convert.ToDouble(factorname[0]) * Convert.ToInt16(pipcount) + Convert.ToDouble(factorname[1]) * bevelcount + Convert.ToDouble(factorname[2]) * elbowcount;
                            ManHourManage.UpdateMaterialPrepareTime(projectStr, spoolStr, pipeStr, totaltime);
                        }
                        myDS.Dispose();
                    }
                    SpoolDS.Dispose();
                    MessageBox.Show("-------------完成------------");
                    break;

                case "装配工时定额":
                    SpoolDS = new DataSet();
                    SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS", projectStr, drawingStr);
                    if (SpoolDS.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("没有查询到该图纸相关小票信息，请与设计员联系！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    for (int i = 0; i < SpoolDS.Tables[0].Rows.Count; i++)
                    {
                        double assTime = 0;
                        spoolStr = SpoolDS.Tables[0].Rows[i][0].ToString();
                        /*******************************计算法兰工时*********************************/
                        DataSet flangeDS = new DataSet();
                        flangeDS = ManHourManage.GetNormCount("SP_GetFlangeNormCount", projectStr, spoolStr);
                        for (int f = 0; f < flangeDS.Tables[0].Rows.Count; f++)
                        {
                            double flange = Convert.ToDouble(flangeDS.Tables[0].Rows[f][0].ToString());
                            if (flange == 0)
                            {
                                continue;
                            }
                            int flangCount = Convert.ToInt16(flangeDS.Tables[0].Rows[f][1].ToString());
                            double flangefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetFlangeFactor", flange));
                            assTime += flangefactor * flangCount;
                        }
                        flangeDS.Dispose();

                        /*******************************计算定型弯头工时******************************/
                        DataSet elbowDS = new DataSet();
                        elbowDS = ManHourManage.GetNormCount("SP_GetElbowNormCount", projectStr, spoolStr);
                        for (int b = 0; b < elbowDS.Tables[0].Rows.Count; b++)
                        {
                            double elbow = Convert.ToDouble(elbowDS.Tables[0].Rows[b][0].ToString());
                            if (elbow == 0)
                            {
                                continue;
                            }
                            int elbowCount = Convert.ToInt16(elbowDS.Tables[0].Rows[b][1].ToString());
                            double elbowfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetElbowFactor", elbow));
                            assTime += elbowfactor * elbowCount;
                        }
                        elbowDS.Dispose();

                        /****************************计算承插弯头弯头工时**************************/
                        DataSet inelbowDS = new DataSet();
                        inelbowDS = ManHourManage.GetNormCount("SP_GetInElbowNormCount", projectStr, spoolStr);
                        for (int ib = 0; ib < inelbowDS.Tables[0].Rows.Count; ib++)
                        {
                            double inelbow = Convert.ToDouble(inelbowDS.Tables[0].Rows[ib][0].ToString());
                            if (inelbow == 0)
                            {
                                continue;
                            }
                            int inelbowCount = Convert.ToInt16(inelbowDS.Tables[0].Rows[ib][1].ToString());
                            double inelbowfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetInElbowFactor", inelbow));
                            assTime += inelbowfactor * inelbowCount;

                        }
                        inelbowDS.Dispose();

                        /*******************************计算异径工时******************************/
                        DataSet reducerDS = new DataSet();
                        reducerDS = ManHourManage.GetNormCount("SP_GetReducerNormCount", projectStr, spoolStr);
                        for (int r = 0; r < reducerDS.Tables[0].Rows.Count; r++)
                        {
                            double reducer = Convert.ToDouble(reducerDS.Tables[0].Rows[r][0].ToString());
                            if (reducer == 0)
                            {
                                continue;
                            }
                            int reducerCount = Convert.ToInt16(reducerDS.Tables[0].Rows[r][1].ToString());
                            double reducerfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetReducerFactor", reducer));
                            assTime += reducerfactor * reducerCount;
                        }
                        reducerDS.Dispose();
                        /*******************************计算承插三通工时******************************/
                        DataSet insertedpassDS = new DataSet();
                        insertedpassDS = ManHourManage.GetNormCount("SP_GetInsertedPassNormCount", projectStr, spoolStr);
                        for (int c = 0; c < insertedpassDS.Tables[0].Rows.Count; c++)
                        {
                            double insertedpass = Convert.ToDouble(insertedpassDS.Tables[0].Rows[c][0].ToString());
                            if (insertedpass == 0)
                            {
                                continue;
                            }
                            int insertedpassCount = Convert.ToInt16(insertedpassDS.Tables[0].Rows[c][1].ToString());
                            double insertedfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetInsertedPassFactor", insertedpass));
                            assTime += insertedfactor * insertedpassCount * 3;
                        }
                        insertedpassDS.Dispose();
                        /*******************************计算对接三通工时******************************/
                        DataSet dockpassDS = new DataSet();
                        dockpassDS = ManHourManage.GetNormCount("SP_GetDockPassNormCount", projectStr, spoolStr);
                        for (int d = 0; d < dockpassDS.Tables[0].Rows.Count; d++)
                        {
                            double dockpass = Convert.ToDouble(dockpassDS.Tables[0].Rows[d][0].ToString());
                            if (dockpass == 0)
                            {
                                continue;
                            }
                            int dockpassCount = Convert.ToInt16(dockpassDS.Tables[0].Rows[d][1].ToString());
                            double dockpassfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetDockPassFactor", dockpass));
                            assTime += dockpassfactor * dockpassCount * 3;
                        }
                        dockpassDS.Dispose();
                        /*******************************计算通舱套筒工时******************************/
                        DataSet tcasingDS = new DataSet();
                        tcasingDS = ManHourManage.GetNormCount("SP_GetTCasingNormCount", projectStr, spoolStr);
                        for (int t = 0; t < tcasingDS.Tables[0].Rows.Count; t++)
                        {
                            double tcasing = Convert.ToDouble(tcasingDS.Tables[0].Rows[t][0].ToString());
                            if (tcasing == 0)
                            {
                                continue;
                            }
                            int tcasingCount = Convert.ToInt16(tcasingDS.Tables[0].Rows[t][1].ToString());
                            double tcasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetTCasingFactor", tcasing));
                            assTime += tcasingfactor * tcasingCount * 2;
                        }
                        tcasingDS.Dispose();
                        /*******************************计算连接套筒工时******************************/
                        DataSet lcasingDS = new DataSet();
                        lcasingDS = ManHourManage.GetNormCount("SP_GetLCasingNormCount", projectStr, spoolStr);
                        for (int t1 = 0; t1 < lcasingDS.Tables[0].Rows.Count; t1++)
                        {
                            double lcasing = Convert.ToDouble(lcasingDS.Tables[0].Rows[t1][0].ToString());
                            if (lcasing == 0)
                            {
                                continue;
                            }
                            int lcasingCount = Convert.ToInt16(lcasingDS.Tables[0].Rows[t1][1].ToString());
                            double lcasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetTCasingFactor", lcasing));
                            assTime += lcasingfactor * lcasingCount;
                        }
                        lcasingDS.Dispose();
                        /*******************************计算承插套筒工时******************************/
                        DataSet icasingDS = new DataSet();
                        icasingDS = ManHourManage.GetNormCount("SP_GetICasingNormCount", projectStr, spoolStr);
                        for (int t2 = 0; t2 < icasingDS.Tables[0].Rows.Count; t2++)
                        {
                            string icasingStr = icasingDS.Tables[0].Rows[t2][0].ToString();
                            double icasing = Convert.ToDouble(icasingStr.Replace("DN", ""));
                            if (icasing == 0)
                            {
                                continue;
                            }
                            int icasingCount = Convert.ToInt16(icasingDS.Tables[0].Rows[t2][1].ToString());
                            double icasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetICasingFactor", icasing));
                            assTime += icasingfactor * icasingCount;
                        }
                        icasingDS.Dispose();
                        /*******************************计算支管工时******************************/
                        DataSet branchDS = new DataSet();
                        branchDS = ManHourManage.GetNormCount("SP_GetBranchNormCount", projectStr, spoolStr);
                        for (int b = 0; b < branchDS.Tables[0].Rows.Count; b++)
                        {
                            double branch = Convert.ToDouble(branchDS.Tables[0].Rows[b][0].ToString());
                            if (branch == 0)
                            {
                                continue;
                            }
                            int branchCount = Convert.ToInt16(branchDS.Tables[0].Rows[b][1].ToString());
                            double branchfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetBranchFactor", branch));
                            assTime += branchfactor * branchCount;
                        }
                        branchDS.Dispose();

                        /******************************计算焊接座工时*****************************/
                        DataSet weldbaseDS = new DataSet();
                        weldbaseDS = ManHourManage.GetNormCount("SP_GetWeldBaseNormCount", projectStr, spoolStr);
                        for (int wd = 0; wd < weldbaseDS.Tables[0].Rows.Count; wd++)
                        {
                            string weldbaseStr = weldbaseDS.Tables[0].Rows[wd][0].ToString();
                            //if (Convert.ToDouble(weldbaseStr) == 0)
                            if (weldbaseStr == "0")
                            {
                                continue;
                            }
                            int weldbaseCount = Convert.ToInt16(weldbaseDS.Tables[0].Rows[wd][1].ToString());
                            double weldbasefactor = 0;
                            if (weldbaseStr.Contains("DN"))
                            {
                                double weldbase = Convert.ToDouble(weldbaseStr.Replace("DN", "").Trim());
                                weldbasefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetWBDNFactor", weldbase));

                            }
                            else
                            {
                                weldbasefactor = Convert.ToDouble(ManHourManage.GetMaterialStrFactor("SP_GetWBFactor", weldbaseStr));
                            }
                            assTime += weldbasefactor * weldbaseCount;
                        }
                        weldbaseDS.Dispose();

                        /*******************************计算腹板工时******************************/
                        DataSet webplateDS = new DataSet();
                        webplateDS = ManHourManage.GetNormCount("SP_GetWebPlateNormCount", projectStr, spoolStr);
                        for (int w = 0; w < webplateDS.Tables[0].Rows.Count; w++)
                        {
                            double webplate = Convert.ToDouble(webplateDS.Tables[0].Rows[w][0].ToString());
                            if (webplate == 0)
                            {
                                continue;
                            }
                            int webplateCount = Convert.ToInt16(webplateDS.Tables[0].Rows[w][1].ToString());
                            double webplatefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetWebPlateFactor", webplate));
                            assTime += webplatefactor * webplateCount;
                        }
                        webplateDS.Dispose();
                        /*******************************计算打磨工时*******************************/
                        //string checkfield = ManHourManage.JudgePipeCheckField(projectStr, spoolStr);
                        //if (checkfield == "内场" )
                        //{
                        string normStr = ManHourManage.GetPipMaxNorm(projectStr, spoolStr);
                        double norm = Convert.ToDouble(normStr);
                        double polishfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetPrePolishFactor", norm));
                        assTime += polishfactor;
                        //}
                        //MessageBox.Show(spoolStr + "-----"+ assTime.ToString());
                        ManHourManage.UpdateSpoolQuotaTime(frmtext, projectStr, spoolStr, assTime);
                    }
                    SpoolDS.Dispose();
                    MessageBox.Show("-------------完成------------");
                    break;

                case "焊接工时定额":
                    queryStr = "SP_GetPipeMaterialDS";
                    SpoolDS = new DataSet();
                    SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS", projectStr, drawingStr);
                    if (SpoolDS.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("没有查询到该图纸相关小票信息，请与设计员联系！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    for (int i = 0; i < SpoolDS.Tables[0].Rows.Count; i++)
                    {
                        double assTime = 0;
                        double polishfactor = 0;
                        spoolStr = SpoolDS.Tables[0].Rows[i][0].ToString();
                        /*******************************计算法兰工时*********************************/
                        DataSet flangeDS = new DataSet();
                        flangeDS = ManHourManage.GetNormCount("SP_GetFlangeNormCount", projectStr, spoolStr);
                        for (int f = 0; f < flangeDS.Tables[0].Rows.Count; f++)
                        {
                            double flange = Convert.ToDouble(flangeDS.Tables[0].Rows[f][0].ToString());
                            if (flange == 0 || flange == 63)
                            {
                                continue;
                            }
                            int flangCount = Convert.ToInt16(flangeDS.Tables[0].Rows[f][1].ToString());
                            double flangefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetFlangeFactor", flange));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetDNPolishFactor", flange));

                            assTime += (flangefactor + polishfactor) * flangCount;
                        }
                        flangeDS.Dispose();

                        /*******************************计算定型弯头工时******************************/
                        DataSet elbowDS = new DataSet();
                        elbowDS = ManHourManage.GetNormCount("SP_GetElbowNormCount", projectStr, spoolStr);
                        for (int b = 0; b < elbowDS.Tables[0].Rows.Count; b++)
                        {
                            double elbow = Convert.ToDouble(elbowDS.Tables[0].Rows[b][0].ToString());
                            if (elbow == 0)
                            {
                                continue;
                            }
                            int elbowCount = Convert.ToInt16(elbowDS.Tables[0].Rows[b][1].ToString());
                            double elbowfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetElbowFactor", elbow));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", elbow));
                            //MessageBox.Show("计算定型弯头工时" + polishfactor.ToString());
                            assTime += (elbowfactor + polishfactor) * elbowCount;
                        }
                        elbowDS.Dispose();
                        /****************************计算承插弯头弯头工时**************************/
                        DataSet inelbowDS = new DataSet();
                        inelbowDS = ManHourManage.GetNormCount("SP_GetInElbowNormCount", projectStr, spoolStr);
                        for (int ib = 0; ib < inelbowDS.Tables[0].Rows.Count; ib++)
                        {
                            if (IsNumberic(inelbowDS.Tables[0].Rows[ib][0].ToString()) == true)
                            {


                                double inelbow = Convert.ToDouble(inelbowDS.Tables[0].Rows[ib][0].ToString());
                                if (inelbow == 0)
                                {
                                    continue;
                                }
                                int inelbowCount = Convert.ToInt16(inelbowDS.Tables[0].Rows[ib][1].ToString());
                                double inelbowfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetInElbowFactor", inelbow));
                                polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetDNPolishFactor", inelbow));
                                assTime += (inelbowfactor + polishfactor) * inelbowCount;
                            }
                            else
                            {
                                continue;
                            }

                        }
                        inelbowDS.Dispose();

                        /*******************************计算异径工时******************************/
                        DataSet reducerDS = new DataSet();
                        reducerDS = ManHourManage.GetNormCount("SP_GetReducerNormCount", projectStr, spoolStr);
                        for (int r = 0; r < reducerDS.Tables[0].Rows.Count; r++)
                        {
                            double reducer = Convert.ToDouble(reducerDS.Tables[0].Rows[r][0].ToString());
                            if (reducer == 0)
                            {
                                continue;
                            }
                            int reducerCount = Convert.ToInt16(reducerDS.Tables[0].Rows[r][1].ToString());
                            double reducerfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetReducerFactor", reducer));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", reducer));
                            //MessageBox.Show("计算异径工时" + polishfactor.ToString());
                            assTime += (reducerfactor + polishfactor) * reducerCount;
                        }
                        reducerDS.Dispose();
                        /*******************************计算承插三通工时******************************/
                        DataSet insertedpassDS = new DataSet();
                        insertedpassDS = ManHourManage.GetNormCount("SP_GetInsertedPassNormCount", projectStr, spoolStr);
                        for (int c = 0; c < insertedpassDS.Tables[0].Rows.Count; c++)
                        {
                            double insertedpass = Convert.ToDouble(insertedpassDS.Tables[0].Rows[c][0].ToString());
                            if (insertedpass == 0)
                            {
                                continue;
                            }
                            int insertedpassCount = Convert.ToInt16(insertedpassDS.Tables[0].Rows[c][1].ToString());
                            double insertedfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetInsertedPassFactor", insertedpass));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetDNPolishFactor", insertedpass));
                            assTime += (insertedfactor + polishfactor) * insertedpassCount * 3;
                        }
                        insertedpassDS.Dispose();
                        /*******************************计算对接三通工时******************************/
                        DataSet dockpassDS = new DataSet();
                        dockpassDS = ManHourManage.GetNormCount("SP_GetDockPassNormCount", projectStr, spoolStr);
                        for (int d = 0; d < dockpassDS.Tables[0].Rows.Count; d++)
                        {
                            if (dockpassDS.Tables[0].Rows[d][0].ToString() == string.Empty)
                            {
                                continue;
                            }
                            double dockpass = Convert.ToDouble(dockpassDS.Tables[0].Rows[d][0].ToString());
                            if (dockpass == 0)
                            {
                                continue;
                            }
                            int dockpassCount = Convert.ToInt16(dockpassDS.Tables[0].Rows[d][1].ToString());
                            double dockpassfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetDockPassFactor", dockpass));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", dockpass));
                            //MessageBox.Show("计算对接三通工时" + polishfactor.ToString());
                            assTime += (dockpassfactor + polishfactor) * dockpassCount * 3;
                        }
                        dockpassDS.Dispose();

                        /*******************************计算通舱套筒工时******************************/
                        DataSet tcasingDS = new DataSet();
                        tcasingDS = ManHourManage.GetNormCount("SP_GetTCasingNormCount", projectStr, spoolStr);
                        for (int t = 0; t < tcasingDS.Tables[0].Rows.Count; t++)
                        {
                            double tcasing = Convert.ToDouble(tcasingDS.Tables[0].Rows[t][0].ToString());
                            if (tcasing == 0)
                            {
                                continue;
                            }
                            int tcasingCount = Convert.ToInt16(tcasingDS.Tables[0].Rows[t][1].ToString());
                            double tcasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetTCasingFactor", tcasing));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", tcasing));
                            //MessageBox.Show("计算通舱套筒工时" + polishfactor.ToString());
                            assTime += (tcasingfactor + polishfactor) * tcasingCount * 2;
                        }
                        tcasingDS.Dispose();

                        /*******************************计算连接套筒工时******************************/
                        DataSet lcasingDS = new DataSet();
                        lcasingDS = ManHourManage.GetNormCount("SP_GetLCasingNormCount", projectStr, spoolStr);
                        for (int t1 = 0; t1 < lcasingDS.Tables[0].Rows.Count; t1++)
                        {
                            double lcasing = Convert.ToDouble(lcasingDS.Tables[0].Rows[t1][0].ToString());
                            if (lcasing == 0)
                            {
                                continue;
                            }
                            int lcasingCount = Convert.ToInt16(lcasingDS.Tables[0].Rows[t1][1].ToString());
                            double lcasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetTCasingFactor", lcasing));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", lcasing));
                            //MessageBox.Show("计算连接套筒工时" + polishfactor.ToString());
                            assTime += (lcasingfactor + polishfactor) * lcasingCount;
                        }
                        lcasingDS.Dispose();

                        /*******************************计算承插套筒工时******************************/
                        DataSet icasingDS = new DataSet();
                        icasingDS = ManHourManage.GetNormCount("SP_GetICasingNormCount", projectStr, spoolStr);
                        for (int t2 = 0; t2 < icasingDS.Tables[0].Rows.Count; t2++)
                        {
                            string icasingStr = icasingDS.Tables[0].Rows[t2][0].ToString();
                            double icasing = Convert.ToDouble(icasingStr.Replace("DN", ""));
                            if (icasing == 0)
                            {
                                continue;
                            }
                            int icasingCount = Convert.ToInt16(icasingDS.Tables[0].Rows[t2][1].ToString());
                            double icasingfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetICasingFactor", icasing));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetDNPolishFactor", icasing));

                            assTime += (icasingfactor + polishfactor) * icasingCount;
                        }
                        icasingDS.Dispose();

                        /*******************************计算支管工时******************************/
                        DataSet branchDS = new DataSet();
                        branchDS = ManHourManage.GetNormCount("SP_GetBranchNormCount", projectStr, spoolStr);
                        for (int b = 0; b < branchDS.Tables[0].Rows.Count; b++)
                        {
                            double branch = Convert.ToDouble(branchDS.Tables[0].Rows[b][0].ToString());
                            if (branch == 0)
                            {
                                continue;
                            }
                            int branchCount = Convert.ToInt16(branchDS.Tables[0].Rows[b][1].ToString());
                            double branchfactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetBranchFactor", branch));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", branch));
                            //MessageBox.Show("计算支管工时" + polishfactor.ToString());
                            assTime += (branchfactor + polishfactor) * branchCount;
                        }
                        branchDS.Dispose();

                        /*******************************计算腹板工时******************************/
                        DataSet webplateDS = new DataSet();
                        webplateDS = ManHourManage.GetNormCount("SP_GetWebPlateNormCount", projectStr, spoolStr);
                        for (int w = 0; w < webplateDS.Tables[0].Rows.Count; w++)
                        {
                            double webplate = Convert.ToDouble(webplateDS.Tables[0].Rows[w][0].ToString());
                            if (webplate == 0)
                            {
                                continue;
                            }
                            int webplateCount = Convert.ToInt16(webplateDS.Tables[0].Rows[w][1].ToString());
                            double webplatefactor = Convert.ToDouble(ManHourManage.GetMaterialFactor(frmtext, "SP_GetWebPlateFactor", webplate));
                            polishfactor = Convert.ToDouble(ManHourManage.GetInPolishFactor("SP_GetPolishFactor", webplate));
                            //MessageBox.Show("计算腹板工时" + polishfactor.ToString());
                            assTime += (webplatefactor + polishfactor) * webplateCount;
                        }
                        webplateDS.Dispose();
                        //MessageBox.Show(spoolStr + "-----" + assTime.ToString());
                        ManHourManage.UpdateSpoolQuotaTime(frmtext, projectStr, spoolStr, assTime);
                    }
                    SpoolDS.Dispose();
                    MessageBox.Show("------完成------");
                    break;

                case "报验工时定额":
                    #region 报验工时计算
                    SpoolDS = new DataSet();
                    SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS", projectStr, drawingStr);
                    for (int i = 0; i < SpoolDS.Tables[0].Rows.Count; i++)
                    {
                        spoolStr = SpoolDS.Tables[0].Rows[i][0].ToString();
                        //string checkfield = ManHourManage.JudgePipeCheckField(projectStr, spoolStr);
                        //if (checkfield == "外场")
                        //{
                        //    continue;
                        //}
                        string normStr = ManHourManage.GetPipMaxNorm(projectStr, spoolStr);
                        //MessageBox.Show(spoolStr);
                        //MessageBox.Show(normStr.ToString());
                        double norm = Convert.ToDouble(normStr);
                        string factorStr = ManHourManage.GetQCORTransORPresFactor(norm);
                        string[] factorname = factorStr.Split(new char[] { '-' });
                        double toqcTime = Convert.ToDouble(factorname[0]);
                        ManHourManage.UpdateSpoolQuotaTime(frmtext, projectStr, spoolStr, toqcTime);

                    }
                    SpoolDS.Dispose();
                    MessageBox.Show("-------------完成------------");
                    #endregion
                    break;

                case "料场工时定额":
                    #region 料场公式计算
                    SpoolDS = new DataSet();
                    SpoolDS = ManHourManage.GetSpoolDS("SP_GETSPOOLDS", projectStr, drawingStr);
                    for (int i = 0; i < SpoolDS.Tables[0].Rows.Count; i++)
                    {
                        spoolStr = SpoolDS.Tables[0].Rows[i][0].ToString();
                        string normStr = ManHourManage.GetPipMaxNorm(projectStr, spoolStr);
                        double norm = Convert.ToDouble(normStr);
                        string factorStr = ManHourManage.GetQCORTransORPresFactor(norm);
                        string[] factorname = factorStr.Split(new char[] { '-' });
                        double tranTime = Convert.ToDouble(factorname[1]);
                        ManHourManage.UpdateSpoolQuotaTime(frmtext, projectStr, spoolStr, tranTime);
                    }
                    SpoolDS.Dispose();
                    MessageBox.Show("-------------完成------------");
                    #endregion
                    break;

                default:
                    break;
            } 
        }


    }
}
