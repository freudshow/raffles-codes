using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.IO;
using DetailInfo.Categery;
using System.Threading;
using System.Net;
namespace DetailInfo
{
    public partial class PROJECTDRAWINGINFO : Form
    {
        public PROJECTDRAWINGINFO()
        {
            InitializeComponent();
            for (int i = 0; i < DrawingCMTSTRIP.Items.Count; i++)
            {
                DrawingCMTSTRIP.Items[i].Visible = false;
            }
            this.drawingrbn.Checked = true;
            this.modifyrbn.Enabled = false;
            this.querybtn.Enabled = false;
            this.toolStripStatusLabel2.Text = null;
            this.toolStripProgressBar1.Visible = false;
        }

        private int flag = 0;

        private void GetFlag()
        {
            if (this.drawingrbn.Checked == true)
            {
                flag = 0;
            }
            else if (this.modifyrbn.Checked == true)
            {
                flag = 1;
            }
        }

        private void PROJECTDRAWINGINFO_Load(object sender, EventArgs e)
        {
            this.modifyrbn.Enabled = true;
            SetStatus();

            DataSet ds = new DataSet();
            string sqlstr = " SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82) and NAME IN (SELECT DISTINCT S.PROJECTID　FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y')   ORDER BY NAME";
            User.DataBaseConnect(sqlstr, ds);
            TreeNode tn;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i][0].ToString();
                treeView1.Nodes["PROJECTS"].Nodes.Add(tn);
                tn.ImageIndex = 3;
            }
            this.treeView1.Nodes[0].Expand();

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name == "PROJECTS")
            {
                return;
            }
            else
            {
                this.textBox1.Text = e.Node.Text.ToString();
            }

            DataBindFuntion();
        }

        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFlag();
            foreach (Form form in MDIForm.pMainWin.MdiChildren)
            {
                if (form.Text == "图纸信息")
                {
                    if (form.WindowState == FormWindowState.Minimized)
                    {
                        form.WindowState = FormWindowState.Normal;
                    }
                    form.Activate();
                    return;
                }
            }
            DrawingForm drawingform = new DrawingForm();
            drawingform.Projectid = this.textBox1.Text.ToString();
            drawingform.Indicator = flag;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.DrawingsDgv.Rows.Count; i++)
            {
                if (this.DrawingsDgv.Rows[i].Selected == true)
                {
                    string drno = this.DrawingsDgv.Rows[i].Cells["DRAWINGNO"].Value.ToString();
                    sb.Append(drno + ',');
                }
            }

            drawingform.Drawing = sb.ToString();
            drawingform.MdiParent = MDIForm.pMainWin;
            drawingform.Show();
        }

        private void 材料设备列表toolStripMenuItem_Click(object sender, EventArgs e)
        {
            string drawingno = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
            string project = this.textBox1.Text.ToString();
            GetFlag();

            MaterialRationForm rationFrom = new MaterialRationForm();
            rationFrom.Projectid = project;
            rationFrom.Drawing = drawingno;
            rationFrom.Indicator = flag;
            rationFrom.Text = "材料设备定额表" + this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
            rationFrom.MdiParent = MDIForm.pMainWin;
            rationFrom.Show();
        }

        /// <summary>
        /// 获取材料设备定额表数据集
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="projectid"></param>
        /// <param name="drawing"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DataSet GetMaterialRationDS(string queryString, string projectid, string drawing, int flag)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters["projectid_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters["drawing_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("flag_in", OracleType.VarChar).Value = flag;
            cmd.Parameters["flag_in"].Direction = System.Data.ParameterDirection.Input;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        private void 生成封面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pid = this.textBox1.Text.ToString();
            string comText = string.Empty;
            int count = this.DrawingsDgv.SelectedRows.Count;

            string drawno = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
            string sqlstr = string.Empty;
            GetFlag();
            if (flag == 0)
            {
                sqlstr = "SELECT count(*) FROM SP_CREATEPDFDRAWING T WHERE T.PROJECTID = '" + pid + "' AND T.DRAWINGNO = '" + drawno + "' AND FLAG = 'Y' AND FRONTPAGE IS NOT NULL AND T.MATERIALPDF IS NOT NULL";
            }
            object num= User.GetScalar1(sqlstr, DataAccess.OIDSConnStr);
            if (Convert.ToInt16(num) == 0)
            {
                MessageBox.Show("请确定该图纸材料信息已经导出完成！", "提示信息", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;

            }
            else
            {
                DialogResult result = MessageBox.Show("确定要添加新的封面？", "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.OK)
                {
                    AttachedFile attachform = new AttachedFile();
                    attachform.Projectid = pid;
                    attachform.Drawing = drawno;
                    attachform.ShowDialog();
                }
            }
        }

        private void 生成PDF文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFlag();
            string pid = this.textBox1.Text.ToString();
            string comText = string.Empty;
            int count = this.DrawingsDgv.SelectedRows.Count;

            string drawno = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
            int vnum = Convert.ToInt16(this.DrawingsDgv.CurrentRow.Cells["REVISION"].Value.ToString());
            string sqlstr = string.Empty;
            if (flag == 0)
            {
                sqlstr = "SELECT count(*) FROM SP_CREATEPDFDRAWING WHERE PROJECTID = '" + pid + "' AND DRAWINGNO = '" + drawno + "' and flag = 'Y' AND  UPDATEDFRONTPAGE IS NOT NULL";
            }
            else
            {
                sqlstr = "SELECT count(*) FROM SP_CREATEPDFDRAWING WHERE PROJECTID = '" + pid + "' AND DRAWINGNO = '" + drawno + "' and flag = 'Y' AND  MODIFYDRAWINGFRONTPAGE IS NOT NULL";
            }
            object num = User.GetScalar1(sqlstr, DataAccess.OIDSConnStr);
            if (Convert.ToInt16(num) == 0)
            {
                MessageBox.Show("请确定该图纸的材料和封页均已保存到数据库！", "提示信息",MessageBoxButtons.OK);
                return;
            }
            else
            {
                GenerateDrawing gdform = new GenerateDrawing();
                gdform.Pid = pid;
                gdform.Drawing = drawno;
                gdform.Indicator = flag;
                gdform.Version = vnum;
                gdform.ShowDialog();
            }

        }


        private void 导入材料信息toolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFlag();
            string drawingno = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
            string pid = this.textBox1.Text.ToString();
            if (flag == 0)
            {
                if (!DetailInfo.Categery.CREATEPDFDRAWING.ExistDrawing(pid, drawingno))
                {
                    MessageBox.Show("请确定该图纸已经存在!");
                    return;
                }
            }
            else
            {
                if (!DetailInfo.Categery.CREATEPDFDRAWING.ExistModifyDrawing(pid, drawingno))
                {
                    MessageBox.Show("请确定该修改通知单已经存在!");
                    return;
                }
            }
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "导出Excel (*.xls)|*.xls";
            //saveFileDialog.FilterIndex = 0;
            //saveFileDialog.RestoreDirectory = true;
            //saveFileDialog.CreatePrompt = true;
            //saveFileDialog.Title = "导出路径";
            //saveFileDialog.FileName=drawingno+".xls";
            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    if (saveFileDialog.FileName.Trim().Length > 0)
            //    {
                    string xlsContent = System.Text.Encoding.UTF8.GetString(Properties.Resources.DetailInfoTemplate);
                    
                    List<SpoolMaterial> PipeRationList = SpoolMaterial.Find(drawingno,flag);
                    string itemContent1 = @" <Row ss:AutoFitHeight='0'>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{0}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{1}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{2}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{3}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{4}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{5}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{6}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{7}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{8}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{9}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{10}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{11}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{12}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{13}</Data><Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{14}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{15}</Data></Cell>
        <Cell ss:StyleID='s39'><Data ss:Type='String'>{16}</Data></Cell>
      </Row>";
                    string itemContent2 = @"<Row ss:AutoFitHeight='0'>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{0}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{1}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{2}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{3}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{4}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{5}</Data></Cell>
      </Row>";
                    string itemContent4 = @"<Row ss:AutoFitHeight='0'>
    <Cell ss:Index='3' ss:StyleID='s28'><Data ss:Type='String'>{0}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{1}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{2}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{3}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{4}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{5}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{6}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{7}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{8}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{9}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{10}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{11}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{12}</Data></Cell>
   </Row>";
                    string itemContent5 = @" <Row ss:AutoFitHeight='0'>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{0}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{1}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{2}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{3}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{4}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{5}</Data></Cell>
   </Row>";
                    string itemContent6 = @" <Row ss:AutoFitHeight='0'>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{0}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{1}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{2}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{3}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{4}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{5}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{6}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{7}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{8}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{9}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{10}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{11}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{12}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{13}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{14}</Data></Cell>
        <Cell ss:StyleID='s28'><Data ss:Type='String'>{15}</Data></Cell>
      </Row>";
                    System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                    System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                    System.Text.StringBuilder sb3 = new System.Text.StringBuilder();
                    System.Text.StringBuilder sb4 = new System.Text.StringBuilder();
                    System.Text.StringBuilder sb5 = new System.Text.StringBuilder();
                    List<Acceemp> acceemplist = Acceemp.Find(drawingno, flag);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(new DataTable());
                    ds.Tables[0].Columns.Add(new DataColumn(""));
                    ds.Tables[0].Columns.Add(new DataColumn(""));
                    ds.Tables[0].Columns.Add(new DataColumn(""));
                    ds.Tables[0].Columns.Add(new DataColumn(""));
                    for (int i = 0; i < acceemplist.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            ds.Tables[0].Rows.Add("螺栓", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
                            ds.Tables[0].Rows.Add("螺母", acceemplist[i].NutStandard, (acceemplist[i].NutNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].NutWeight.ToString());
                            ds.Tables[0].Rows.Add("垫片", acceemplist[i].GasketStandard, acceemplist[i].TotalNum.ToString(), string.Empty);
                        }
                        else
                        {
                            string flag1 = "N";
                            string flag2 = "N";
                            string flag3 = "N";
                            for (int j = 0; j <ds.Tables[0].Rows.Count; j++)
                            {
                                if (ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].BoltStandard)
                                {
                                    ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString();
                                    flag1 = "Y";
                                }
                                if (ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].NutStandard)
                                {
                                    ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].NutNumber * acceemplist[i].TotalNum).ToString();
                                    flag2 = "Y";
                                }
                                if (ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].GasketStandard)
                                {
                                    ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].TotalNum).ToString();
                                    flag3 = "Y";
                                }
                            }
                            if(flag1=="N")
                                ds.Tables[0].Rows.Add("螺栓", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
                            if (flag2 == "N")
                                ds.Tables[0].Rows.Add("螺母", acceemplist[i].NutStandard, (acceemplist[i].NutNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].NutWeight.ToString());
                            if (flag3 == "N")
                                ds.Tables[0].Rows.Add("垫片", acceemplist[i].GasketStandard, acceemplist[i].TotalNum.ToString(), string.Empty);
                        }
                    }
                    int n4=0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[i][2]) != 0)
                        {
                            n4++;
                            sb4.AppendFormat(itemContent5, n4, ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], "");
                            sb5.AppendFormat(itemContent6, n4, pid, "", "", "", ds.Tables[0].Rows[i][0] + " " + ds.Tables[0].Rows[i][1], "", ds.Tables[0].Rows[i][2], "PCS", "", "", "", "", drawingno, "", ds.Tables[0].Rows[i][2]);
                        }
                    }
                    List<SpoolMaterial> newlist = new List<SpoolMaterial>();
                    List<string> sum = new List<string>();
                    List<string> sumweight = new List<string>();
                    for (int i = 0; i < PipeRationList.Count; i++)
                    {
                        string[] spacesplit = PipeRationList[i].MaterialName.Split(' ');
                        int count = spacesplit.Length - 1;
                        string name = spacesplit[count];
                        if (name.Contains("管"))//如果是管材
                        {
                            string[] xsplit = PipeRationList[i].MaterialName.Split(new char[] { 'X', ' ' });
                            if (newlist.Count == 0)
                            {
                                newlist.Add(PipeRationList[i]);
                                sum.Add(xsplit[3]);
                                sumweight.Add(PipeRationList[i].PartWeight);
                                break;
                            }
                            else//如果新列表个数不为0
                            {
                                int k = 0;
                                for (int j = 0; j < newlist.Count; j++)
                                {
                                    string[] spacesplitnew = newlist[j].MaterialName.Split(' ');
                                    int countnew = spacesplitnew.Length - 1;
                                    string namenew = spacesplitnew[countnew];
                                    if (name == namenew)
                                    {
                                        string[] xsplitnew = newlist[j].MaterialName.Split(new char[] { 'X', ' ' });
                                        if (xsplit[0] == xsplitnew[0] && xsplit[1] == xsplitnew[1] && xsplit[2] == xsplitnew[2])
                                        {
                                            sum[j] = Convert.ToString(Convert.ToDouble(sum[j]) + Convert.ToDouble(xsplit[3]));
                                            sumweight[j] = Convert.ToString(Convert.ToDouble(sumweight[j]) + Convert.ToDouble(PipeRationList[i].PartWeight));
                                            break;
                                        }
                                        else
                                        {
                                            k++;
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        k++;
                                        continue;
                                    }
                                }
                                if (k == newlist.Count)
                                {
                                    newlist.Add(PipeRationList[i]);
                                    sum.Add(xsplit[3]);
                                    sumweight.Add(PipeRationList[i].PartWeight);
                                }
                            }
                        }
                        else//如果是其他材料
                        {
                            if (newlist.Count == 0)
                            {
                                newlist.Add(PipeRationList[i]);
                                sum.Add("1");
                                sumweight.Add(PipeRationList[i].PartWeight);
                            }
                            else
                            {
                                int s = 0;
                                for (int j = 0; j < newlist.Count; j++)
                                {
                                    if (PipeRationList[i].MaterialName == newlist[j].MaterialName)
                                    {
                                        sum[j] = Convert.ToString(Convert.ToUInt32(sum[j]) + 1);
                                        sumweight[j] = Convert.ToString(Convert.ToDouble(sumweight[j]) + Convert.ToDouble(PipeRationList[i].PartWeight));
                                        break;
                                    }
                                    else
                                    {
                                        s++;
                                        continue;
                                    }
                                }
                                if (s == newlist.Count)
                                {
                                    newlist.Add(PipeRationList[i]);
                                    sum.Add("1");
                                    sumweight.Add(PipeRationList[i].PartWeight);
                                }
                            }
                        }
                    }
                    string blockno = Spool.GetBlockNo(drawingno,flag);
                    for (int i = 0; i < newlist.Count; i++)
                    {
                        string[] xsplitnew = newlist[i].MaterialName.Split(new char[] { 'X', ' ' });
                        int count = xsplitnew.Length - 1;
                        string name = xsplitnew[count];
                        string size = newlist[i].MaterialName.Remove(newlist[i].MaterialName.LastIndexOf(" "));
                        string unit = "PCS";
                        if (name.Contains("管"))
                        {
                            unit = "KG";
                            name = xsplitnew[0];
                            size = xsplitnew[1] + "X" + xsplitnew[2];
                        }
                        sb1.AppendFormat(itemContent1, i + 1, newlist[i].Projectid, "", "", newlist[i].MSSNo, newlist[i].ERPCode, newlist[i].MaterialName, "", sum[i], unit, "", blockno, "", "", drawingno, "", sum[i]);
                        sb2.AppendFormat(itemContent2, i + 1, name, size, sum[i], sumweight[i], "");
                    }
                    int n = 0;
                    List<Spool> spoolnamelist = Spool.GetSpoolName(drawingno,flag);
                    for (int i = 0; i < spoolnamelist.Count; i++)
                    {
                        List<SpoolMaterial> xlist = new List<SpoolMaterial>();
                        for (int j = 0; j < PipeRationList.Count; j++)
                        {
                            string[] xsplit = PipeRationList[j].MaterialName.Split(new char[] { 'X', ' ' });
                            int count = xsplit.Length - 1;
                            string name = xsplit[count];
                            if (name.Contains("管"))//如果是管材
                            {
                                if (PipeRationList[j].SpoolName == spoolnamelist[i].SpoolName)
                                    xlist.Add(PipeRationList[j]);
                            }
                        }
                        string itemContent3 = @"<Row ss:AutoFitHeight='0'>
    <Cell ss:MergeDown='{n3}' ss:StyleID='m102725254'><Data ss:Type='String'>{0}</Data></Cell>
    <Cell ss:MergeDown='{n3}' ss:StyleID='m102725264'><Data ss:Type='String'>{1}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{2}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{3}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{4}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{5}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{6}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{7}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{8}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{9}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{10}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{11}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{12}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{13}</Data></Cell>
    <Cell ss:StyleID='s28'><Data ss:Type='String'>{14}</Data></Cell>
   </Row>";
                        itemContent3 = itemContent3.Replace("{n3}", Convert.ToString(xlist.Count - 1));
                        string[] newsplit = xlist[0].MaterialName.Split(new char[] { 'X', ' ' });
                        string surfaceArea = Convert.ToString(Convert.ToDouble(newsplit[1]) * 0.001 * Math.PI * Convert.ToDouble(newsplit[3]) * 0.001 * 1.2);
                        sb3.AppendFormat(itemContent3, i + 1, xlist[0].SpoolName, newsplit[1] + "X" + newsplit[2], newsplit[0], spoolnamelist[i].SurfaceTreatment, spoolnamelist[i].PipeCheckField, newsplit[3], xlist[0].PartWeight, spoolnamelist[i].CabinType, spoolnamelist[i].PipeGrade, spoolnamelist[i].PressureTestField + spoolnamelist[i].WorkingPressure,System.Web.HttpUtility.HtmlEncode(spoolnamelist[i].Remark), surfaceArea, "", "");
                        for (int k = 1; k < xlist.Count; k++)
                        {
                            newsplit = xlist[k].MaterialName.Split(new char[] { 'X', ' ' });
                            surfaceArea = Convert.ToString(Convert.ToDouble(newsplit[1]) * 0.001 * Math.PI * Convert.ToDouble(newsplit[3]) * 0.001 * 1.2);
                            sb3.AppendFormat(itemContent4, newsplit[1] + "X" + newsplit[2], newsplit[0], spoolnamelist[i].SurfaceTreatment, spoolnamelist[i].PipeCheckField, newsplit[3], xlist[k].PartWeight, spoolnamelist[i].CabinType, spoolnamelist[i].PipeGrade, spoolnamelist[i].PressureTestField + spoolnamelist[i].WorkingPressure,System.Web.HttpUtility.HtmlEncode(spoolnamelist[i].Remark), surfaceArea, "", "");
                        }
                        n = n + xlist.Count;
                    }

                    xlsContent = xlsContent.Replace("{item1}", sb1.ToString()).Replace("{drawingno}", drawingno).Replace("{N1}", Convert.ToString(newlist.Count + 8)).Replace("{item2}", sb2.ToString()).Replace("{N2}", Convert.ToString(newlist.Count + 4)).Replace("{blockno}", blockno).Replace("{item3}", sb3.ToString()).Replace("{N3}", Convert.ToString(n + 4)).Replace("{item4}", sb4.ToString()).Replace("{N4}", Convert.ToString(n4+ 4)).Replace("{item5}", sb5.ToString()).Replace("{N5}", Convert.ToString(n4+ 8));
                    //FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    byte[] exportfile = System.Text.Encoding.UTF8.GetBytes(xlsContent);
                    string sql = string.Empty;
                    if(flag==0)
                       sql = "UPDATE PLM.SP_CREATEPDFDRAWING SET MATERIALINFO=:dfd WHERE PROJECTID='" + pid + "' AND DRAWINGNO='" + drawingno + "' and flag = 'Y'";
                   else
                        sql = "UPDATE PLM.SP_CREATEPDFDRAWING SET MODIFYMATERIALINFO=:dfd WHERE PROJECTID='" + pid + "' AND DRAWINGNO='" + drawingno + "' and flag = 'Y'";
                    DetailInfo.Categery.CREATEPDFDRAWING.UpdateExcelInfo(sql,exportfile);
                    //stream.Write(exportfile, 0, exportfile.Length);
                    //stream.Close();
                    //stream.Dispose();
                    MessageBox.Show("分析完成，材料数据已经生成，并已传到数据库中！");
            //    }
            //}
        }
        delegate void SetValueCallback();

        delegate void SetValue(string[] p1, string p2);
        private void 打包图纸toolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFlag();
            string pid = this.DrawingsDgv.CurrentRow.Cells["PROJECTID"].Value.ToString();
            int count = this.DrawingsDgv.SelectedRows.Count;

            string drawno = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
            string sqlstr = string.Empty;
            if (flag == 0)
            {
                sqlstr = "SELECT count(*) FROM SP_CREATEPDFDRAWING WHERE PROJECTID = '" + pid + "' AND DRAWINGNO = '" + drawno + "' AND PDFDRAWING IS NOT NULL AND MATERIALPDF IS NOT NULL and flag = 'Y'";
            }
            else if (flag == 1)
            {
                sqlstr = "SELECT count(*) FROM SP_CREATEPDFDRAWING WHERE PROJECTID = '" + pid + "' AND DRAWINGNO = '" + drawno + "' AND MODIFYDRAWINGS IS NOT NULL AND MODIFYMATERIALPDF IS NOT NULL and flag = 'Y'";
            }

            object num = User.GetScalar1(sqlstr, DataAccess.OIDSConnStr);
            if (Convert.ToInt16(num) == 0)
            {
                MessageBox.Show("请确定该图纸和材料信息已经存在！");
                return;
            }
            else
            {

                PackageDrawing pdform = new PackageDrawing();
                pdform.Projectid = pid;
                pdform.Drawing = drawno;
                pdform.Indicator = flag;
                pdform.ShowDialog();

            }
        }

        private void OpenModifyFrom(string drawing_no, string version_no, string ori_drawing )
        {
            DataSet ds = new DataSet();
            string sqlpid = "select project_id,drawing_id from project_drawing_tab where drawing_no = '" + drawing_no + "' and revision = '" + version_no + "'";
            User.DataBaseConnect(sqlpid, ds);
            User.projectid = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            int drawing_id = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            ds.Dispose();
            ModifyInfoForm miform = new ModifyInfoForm();
            miform.Drawingno = drawing_no;
            miform.Versionstr = version_no;
            miform.Drawingid = drawing_id;
            miform.Originaldrawing = ori_drawing;
            miform.MdiParent = MDIForm.pMainWin;
            miform.Show();

        }

        private void 提交审核toolStripMenuItem_Click(object sender, EventArgs e)
        {
            string drawno = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
            string version = this.DrawingsDgv.CurrentRow.Cells["REVISION"].Value.ToString();
            if (this.modifyrbn.Checked == true)
            {
                string originaldrawing = this.DrawingsDgv.CurrentRow.Cells["REALDRAWING"].Value.ToString();
                string sqlstr = "select count(*) from mf_modifyinfo_tab where drawing_id = (select drawing_id from project_drawing_tab where drawing_no = '" + drawno + "')";
                object obj = User.GetScalar(sqlstr, DataAccess.OIDSConnStr);
                if (Convert.ToInt16(obj.ToString()) == 0)
                {
                    OpenModifyFrom(drawno, version, originaldrawing);
                    return;
                }
                else
                {
                    DialogResult result;
                    result = MessageBox.Show("是否需要更新修改信息？", "修改信息更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        OpenModifyFrom(drawno, version, originaldrawing);
                    }
                    else
                    {
                        UploadToApprove(drawno);
                    }
                    return;
                }
            }
            UploadToApprove(drawno);

        }
        private void SetProcessBarValue()
        {
            if (this.lsubmit.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetProcessBarValue);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.lsubmit.Visible = true;
            }
        }

        private void UploadToApprove(string drawing_no)
        {
            if (!File.Exists(User.rootpath + "\\" + drawing_no + ".zip"))
            {
                MessageBox.Show("请确定该图纸已经打包完成！");
                return;
            }
            else
            {

                DialogResult result = MessageBox.Show("确定要上传吗？(将需要5-10秒时间，请耐心等待)", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string filepath = string.Empty;
                    string filename = drawing_no + ".zip";
                    string Cdata = "";
                    if (DataAccess.severstr == "OIDS")
                        Cdata = "http://172.16.7.55/";
                    else
                        Cdata = "http://172.20.64.3/";

                    filepath = this.UploadFile(Cdata + "ClientUpload.aspx?drawingno=" + drawing_no + "&user=" + User.cur_user, User.rootpath + "\\" + drawing_no + ".zip");
                    if (filepath.Contains("失败"))
                    {
                        MessageBox.Show(filepath, "上传失败");
                        return;
                    }
                    MessageBox.Show(filepath, "上传完毕");
                    File.Delete(User.rootpath + "\\" + drawing_no + ".zip");
                    this.DrawingsDgv.CurrentRow.Cells["STATUS"].Value = "审核中";
                    if (flag == 0)
                    {
                        DBConnection.UpdateDrawingStatus((int)FlowState.审核中, drawing_no);
                    }
                    else if (flag == 1)
                    {
                        DBConnection.UpdateModifyDrawingStatus((int)FlowState.审核中, drawing_no);
                    }
                }
            }
        }

        /// <summary>
        /// 单个文件上传至服务器
        /// </summary>
        /// <param name="uriAddress">接收文件资源的URI, 例如: http://xxxx/Upload.aspx</param>
        /// <param name="filePath">要发送的资源文件, 例如: @"D:\workspace\WebService 相关.doc</param>
        /// <returns>返回文件保存的相对路径, 例如: "upload/xxxxx.jpg" 或者出错返回 ""</returns>
        private string UploadFile(string uriAddress, string filePath)
        {
            

            //利用 WebClient
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                byte[] responseArray = webClient.UploadFile(uriAddress, "POST", filePath);
                //webClient.DownloadData();
                string savePath = Encoding.UTF8.GetString(responseArray);
                return savePath;
            }
            catch (Exception err)
            {
                return err.ToString()+"上传失败";
            }
        }
        public bool UriExists(string url)
        {
            try
            {
                new WebClient().OpenRead(url);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
        private void 导出到EXCELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.toolStripProgressBar1.Visible = true;
            User.ExportToExcel(this.DrawingsDgv, this.toolStripProgressBar1);
        }

        private void DrawingsDgv_SelectionChanged(object sender, EventArgs e)
        {
            GetFlag();
            if (flag == 0)
            {
                if (this.DrawingsDgv.SelectedRows.Count > 0)
                {
                    for (int i = 0; i < DrawingCMTSTRIP.Items.Count; i++)
                    {
                        DrawingCMTSTRIP.Items[i].Visible = true;
                    }
                }
                else
                {
                    for (int i = 0; i < DrawingCMTSTRIP.Items.Count; i++)
                    {
                        DrawingCMTSTRIP.Items[i].Visible = false;
                    }
                }
            }
            else if (flag == 1)
            {
                if (this.DrawingsDgv.SelectedRows.Count > 0)
                {
                    //this.DrawingCMTSTRIP.Items["详细信息ToolStripMenuItem"].Visible = true;
                    for (int i = 0; i < DrawingCMTSTRIP.Items.Count; i++)
                    {
                        DrawingCMTSTRIP.Items[i].Visible = true;
                    }
                    DrawingCMTSTRIP.Items["toolStripSeparator7"].Visible=this.DrawingCMTSTRIP.Items["添加封面ToolStripMenuItem"].Visible = false;
                     
                }
                else
                {
                    for (int i = 0; i < DrawingCMTSTRIP.Items.Count; i++)
                    {
                        DrawingCMTSTRIP.Items[i].Visible = false;
                    }
                }
            }
            int count = this.DrawingsDgv.SelectedRows.Count;
            this.toolStripStatusLabel2.Text = string.Format("当前选中:{0}行",count);
        }

        /// <summary>
        /// 精确查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void querybtn_Click(object sender, EventArgs e)
        {
            string sqlstr = string.Empty;
            string wheresqlstr = string.Empty;
            string pidstr_input = this.textBox1.Text.ToString();

            wheresqlstr += " and PLM.PROJECT_API.Get_PROJECT_NAME(project_id) = '" + pidstr_input + "'";


            string drawingno_input = DRAWINGNOcomboBox.Text.ToString();
            if (drawingno_input == string.Empty)
            {
                wheresqlstr += string.Empty;
            }
            else
            {
                wheresqlstr += " and DRAWING_NO  LIKE  '" + drawingno_input + "'";
            }

            wheresqlstr += "  ORDER BY DRAWING_ID DESC";
            DataSet ds = new DataSet();
            if (flag == 0)
            {
                sqlstr = "SELECT PLM.PROJECT_API.Get_PROJECT_NAME(project_id) projectname, DRAWING_NO, DRAWING_TITLE, Revision, Decode(t.flowstatus,1, '初始状态', 2, '完成但未审核', 3, '审核退回', 4, '审核通过',6, '审核中', 5,'已下发') status, PLM.USER_API.CHINESENAME(RESPONSIBLE_USER) ChineseName, RESPONSIBLE_USER  FROM PLM.PROJECT_DRAWING_TAB t where drawing_type is null  AND DOCTYPE_ID IN (7) AND DOCTYPE_ID != 71 AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' and Discipline_Id=9 ";
            }
            else if (flag == 1)
            {
                sqlstr = "SELECT PLM.PROJECT_API.Get_PROJECT_NAME(project_id) projectname, DRAWING_NO, DRAWING_TITLE, Revision, Decode(t.flowstatus,1, '初始状态', 2, '完成但未审核', 3, '审核退回', 4, '审核通过', 6, '审核中',5,'已下发') status, PLM.USER_API.CHINESENAME(RESPONSIBLE_USER) ChineseName, RESPONSIBLE_USER  FROM PLM.PROJECT_DRAWING_TAB t where drawing_type is null  AND DRAWING_NO IN (SELECT DISTINCT S.MODIFYDRAWINGNO FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y' AND S.MODIFYDRAWINGNO IS NOT NULL) AND DOCTYPE_ID = 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' and Discipline_Id=9 ";
            }
            User.DataBaseConnect(sqlstr + wheresqlstr, ds);
            this.DrawingsDgv.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
            SetStatus();
        }

        /// <summary>
        ///设置状态栏
        /// </summary>
        private void SetStatus()
        {
            int count = this.DrawingsDgv.Rows.Count;
            this.toolStripStatusLabel1.Text = string.Format(" 当前总记录数：{0}个",count);
        }

        private void PROJECTDRAWINGINFO_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
            MDIForm.tool_strip.Items[2].Enabled = true;
        }

        /// <summary>
        /// 窗体控件的数据绑定
        /// </summary>
        private void DataBindFuntion()
        {
            string drawingstr = string.Empty;
            string sqlstr = string.Empty;

            this.DRAWINGNOcomboBox.Items.Clear();
            if (this.DRAWINGNOcomboBox.Text.Length !=0)
            {
                this.DRAWINGNOcomboBox.Text.Remove(0);
            }
            this.querybtn.Enabled = true;
            this.DRAWINGNOcomboBox.Items.Clear();

            if (this.drawingrbn.Checked == true)
            {
                sqlstr = "SELECT DRAWING_NO FROM PLM.PROJECT_DRAWING_TAB where drawing_type is null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + this.textBox1.Text.ToString() + "') AND DOCTYPE_ID IN (7)  AND DOCTYPE_ID != 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' ORDER BY DRAWING_ID DESC";
            }
            else if (this.modifyrbn.Checked == true)
            {
                sqlstr = "SELECT DRAWING_NO FROM PLM.PROJECT_DRAWING_TAB where drawing_type is null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + this.textBox1.Text.ToString() + "') AND DRAWING_NO IN (SELECT DISTINCT S.MODIFYDRAWINGNO FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y' AND S.MODIFYDRAWINGNO IS NOT NULL) AND DOCTYPE_ID = 71 AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' ORDER BY DRAWING_ID DESC";
            }
            DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.DRAWINGNOcomboBox, sqlstr);

            DataSet ds = new DataSet();

            if (this.drawingrbn.Checked == true)
            {

                drawingstr = "SELECT PLM.PROJECT_API.Get_PROJECT_NAME(project_id) projectname, DRAWING_NO, DRAWING_TITLE, Revision, Decode(t.flowstatus,1, '初始状态', 2, '完成但未审核', 3, '审核退回', 4, '审核通过', 6, '审核中', 5, '已下发') status, PLM.USER_API.CHINESENAME(RESPONSIBLE_USER) ChineseName,RESPONSIBLE_USER FROM PLM.PROJECT_DRAWING_TAB t where drawing_type is null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + this.textBox1.Text.ToString() + "') AND DOCTYPE_ID IN (7) AND DOCTYPE_ID != 71 AND FLOWSTATUS IN (1,2,3,4,5,6) AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' and Discipline_Id=9 ORDER BY DRAWING_ID DESC";

            }
            else if (this.modifyrbn.Checked ==true)
            {
                drawingstr = "SELECT A.projectname,A.DRAWING_NO, B.REALDRAWING, A.DRAWING_TITLE,A.Revision, A.status, A.ChineseName, A.RESPONSIBLE_USER  FROM (SELECT PLM.PROJECT_API.Get_PROJECT_NAME(project_id) projectname, DRAWING_NO, DRAWING_TITLE, Revision, Decode(t.flowstatus,1, '初始状态', 2, '完成但未审核', 3, '审核退回', 4, '审核通过', 6, '审核中', 5, '已下发') status, PLM.USER_API.CHINESENAME(RESPONSIBLE_USER) ChineseName,RESPONSIBLE_USER FROM PLM.PROJECT_DRAWING_TAB t where drawing_type is null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + this.textBox1.Text.ToString() + "') AND DRAWING_NO IN (SELECT DISTINCT S.MODIFYDRAWINGNO FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y' AND S.MODIFYDRAWINGNO IS NOT NULL) AND DOCTYPE_ID = 71 AND FLOWSTATUS IN (1,2,3,4,5,6) AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' and Discipline_Id=9 ORDER BY DRAWING_ID DESC) A, (SELECT DRAWING_NO,(SELECT d.drawing_No from project_drawing_tab d left join drawing_modification_tab t on t.drawing_id = d.drawing_id where t.modification_id = pd.drawing_id and d.delete_flag = 'N') REALDRAWING  FROM PLM.PROJECT_DRAWING_TAB pd where drawing_type is null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME = '" + this.textBox1.Text.ToString() + "') AND DRAWING_NO IN (SELECT DISTINCT S.MODIFYDRAWINGNO FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y' AND S.MODIFYDRAWINGNO IS NOT NULL) AND DOCTYPE_ID = 71 AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' ORDER BY DRAWING_ID DESC) B  WHERE A.DRAWING_NO = B.DRAWING_NO";

            }
            User.DataBaseConnect(drawingstr, ds);
            this.DrawingsDgv.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
            SetStatus();

        }

        private void DrawingsDgv_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }
            else
            {
                int count = this.DrawingsDgv.SelectedRows.Count;
                GetFlag();
                string filestr = string.Empty;

                if (count > 1)
                {
                    for (int i = 1; i < DrawingCMTSTRIP.Items.Count - 1; i++)
                    {
                        DrawingCMTSTRIP.Items[i].Enabled = false;
                    }
                    if (flag == 1)
                    {
                        DrawingCMTSTRIP.Items["toolStripSeparator7"].Visible = DrawingCMTSTRIP.Items["添加封面ToolStripMenuItem"].Visible = false;
                    }
                }
                else if (count == 1)
                {
                    string drawing = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
                    string status = this.DrawingsDgv.CurrentRow.Cells["STATUS"].Value.ToString();
                    string userstr = this.DrawingsDgv.CurrentRow.Cells["RESPONSIBLEUSER"].Value.ToString();
                    if (status == "初始状态" || status == "审核退回" || status == "完成但未审核")
                    {
                        if (userstr == User.cur_user)
                        {
                            filestr = User.rootpath + "\\" + drawing + ".zip";

                            if (System.IO.File.Exists(filestr))
                            {
                                for (int i = 0; i < DrawingCMTSTRIP.Items.Count; i++)
                                {
                                    DrawingCMTSTRIP.Items[i].Enabled = true;
                                }

                            }
                            else
                            {
                                for (int i = 0; i < DrawingCMTSTRIP.Items.Count; i++)
                                {
                                    DrawingCMTSTRIP.Items[i].Enabled = true;
                                }
                                DrawingCMTSTRIP.Items["提交审核toolStripMenuItem"].Enabled = false;
                            }
                        }
                        else
                        {
                            for (int i = 3; i < DrawingCMTSTRIP.Items.Count - 1; i++)
                            {
                                DrawingCMTSTRIP.Items[i].Enabled = false;
                            }
                        }

                        if (flag == 1)
                        {
                            DrawingCMTSTRIP.Items["toolStripSeparator7"].Visible = DrawingCMTSTRIP.Items["添加封面ToolStripMenuItem"].Visible = false;
                        }
                    }
                    else
                    {
                        for (int i = 3; i < DrawingCMTSTRIP.Items.Count - 1; i++)
                        {
                            DrawingCMTSTRIP.Items[i].Enabled = false;
                        }
                    }
                    DrawingCMTSTRIP.Items["材料设备列表toolStripMenuItem"].Enabled = true;
                }
                else
                {
                    return;
                }
            }
        }

        private void drawingrbn_CheckedChanged(object sender, EventArgs e)
        {
            this.DrawingsDgv.DataSource = null;
            this.DrawingsDgv.Columns.Clear();
            DrawingsTileBind();
            DataBindFuntion();
        }

        private void modifyrbn_CheckedChanged(object sender, EventArgs e)
        {
            this.DrawingsDgv.DataSource = null;
            this.DrawingsDgv.Columns.Clear();
            DrawingsTileBind();

            DataBindFuntion();
        }

        private void DrawingsTileBind()
        {
            this.DrawingsDgv.Columns.Add("PROJECTID", "项目号"); this.DrawingsDgv.Columns["PROJECTID"].DataPropertyName = "projectname";
            this.DrawingsDgv.Columns.Add("DRAWINGNO", "图纸号"); this.DrawingsDgv.Columns["DRAWINGNO"].DataPropertyName = "DRAWING_NO";
            if (this.modifyrbn.Checked == true)
            {
                this.DrawingsDgv.Columns.Add("REALDRAWING", "原图纸号"); this.DrawingsDgv.Columns["REALDRAWING"].DataPropertyName = "REALDRAWING";
            }
            this.DrawingsDgv.Columns.Add("DRAWING_TITLE", "图纸标题");this.DrawingsDgv.Columns["DRAWING_TITLE"].DataPropertyName = "DRAWING_TITLE";
            this.DrawingsDgv.Columns.Add("REVISION", "版本号");this.DrawingsDgv.Columns["REVISION"].DataPropertyName = "Revision";
            this.DrawingsDgv.Columns.Add("STATUS", "当前状态");this.DrawingsDgv.Columns["STATUS"].DataPropertyName = "status";
            this.DrawingsDgv.Columns.Add("RESPONSIBLE_USER", "责任人"); this.DrawingsDgv.Columns["RESPONSIBLE_USER"].DataPropertyName = "ChineseName";
            this.DrawingsDgv.Columns.Add("RESPONSIBLEUSER", "责任人英文名"); this.DrawingsDgv.Columns["RESPONSIBLEUSER"].DataPropertyName = "RESPONSIBLE_USER";
            this.DrawingsDgv.Columns["RESPONSIBLEUSER"].Visible = false;
        }

        private void PROJECTDRAWINGINFO_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[1].Enabled = false;
        }


        /// <summary>
        /// 下载EXCEL格式的材料表到本地
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="filepath"></param>
        private void DownLoadFiles(string sqlstr, string filepath)
        {
            OracleDataReader dr = null;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            try
            {
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlstr;
                dr = cmd.ExecuteReader();
                byte[] File = null;
                if (dr.Read())
                {
                    File = (byte[])dr[0];
                }
                FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);

                bw.Write(File, 0, File.Length);

                bw.Close();
                fs.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message, ToString());
            }

            finally
            {
                conn.Close();
            }
        }


        private void 管系托盘表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetailInfo.MaterialReport.SpPipeMaterialForm spform = new MaterialReport.SpPipeMaterialForm();
            string DrawingNoTmp = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
            
            spform.Drawing = DrawingNoTmp;
            GetFlag();
            if(flag==0)
                User.bcProDrawSql = " where drawingno ='" + DrawingNoTmp + "' and modifydrawingno is null";
            else
                User.bcProDrawSql = " where MODIFYDRAWINGNO ='" + DrawingNoTmp + "' ";

            string sql = "SELECT count(*) FROM SPL_VIEW " +  User.bcProDrawSql + " AND" +  " PROJECTID = '" + this.textBox1.Text.ToString() + "'" ;
            DataSet ds = new DataSet();
            OracleConnection sqlCon = new OracleConnection(DataAccess.OIDSConnStr);
            OracleCommand sqlCmd = new OracleCommand(sql, sqlCon);
            sqlCon.Open();
            int num = Convert.ToInt32(sqlCmd.ExecuteScalar());
            ds.Dispose();
            if (num > 0)
            {
                spform.MdiParent = MDIForm.pMainWin;
                spform.Show();
            }
            else
            {
                MessageBox.Show("没有此图号的管系托盘信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void 管系材料表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFlag();
            string sflag = string.Empty;
            string sql = string.Empty;
            if (flag == 0)
                sql = "SELECT count(*) FROM fun_pipematerial_lsh where DRAWINGNO='" + this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString() + "' and projectid='" + this.DrawingsDgv.CurrentRow.Cells["PROJECTID"].Value.ToString() + "' and modifydrawingno is null  ";

            else

                sql = "SELECT count(*) FROM fun_pipematerial_lsh where modifydrawingno='" + this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString() + "' and projectid='" + this.DrawingsDgv.CurrentRow.Cells["PROJECTID"].Value.ToString() + "'";
           
            
            DataSet ds = new DataSet();
            OracleConnection sqlCon = new OracleConnection(DataAccess.OIDSConnStr);
            OracleCommand sqlCmd = new OracleCommand(sql, sqlCon);
            sqlCon.Open();
            int num = Convert.ToInt32(sqlCmd.ExecuteScalar());
            ds.Dispose();
            if (num > 0)
            {
                DetailInfo.MaterialReport.TotalPipeMaterialForm spform = new MaterialReport.TotalPipeMaterialForm();
                spform.DrawingNo = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
                spform.ProjectNo = this.DrawingsDgv.CurrentRow.Cells["PROJECTID"].Value.ToString();
                GetFlag();
                spform.Flag = flag;
             
                spform.MdiParent = MDIForm.pMainWin;
                spform.Show();
            }
            else
            {
                MessageBox.Show("没有此图号的管系材料信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void 附件材料表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFlag();
            string sqlstr = string.Empty;
            if (flag == 0)
            {
                sqlstr = "select count(*) from sp_acceemp where PROJECTID = '" + this.textBox1.Text.ToString() + "'  AND FLAG = 'Y' and spoolname in (select s.spoolname from plm.sp_spool_tab s where s.drawingno='" + this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString() + "' and s.flag='Y' and MODIFYDRAWINGNO is null)";
            }
            else
            {
                sqlstr = "select count(*) from sp_acceemp where PROJECTID = '" + this.textBox1.Text.ToString() + "'  AND FLAG = 'Y' and spoolname in (select s.spoolname from plm.sp_spool_tab s where s.modifydrawingno='" + this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString() + "' and s.flag='Y')";
            }
            DataSet ds = new DataSet();
            OracleConnection sqlCon = new OracleConnection(DataAccess.OIDSConnStr);
            OracleCommand sqlCmd = new OracleCommand(sqlstr, sqlCon);
            sqlCon.Open();
            int num = Convert.ToInt32(sqlCmd.ExecuteScalar());
            ds.Dispose();
            if (num > 0)
            {
                DetailInfo.MaterialReport.AccessoriesRpt accerpt = new DetailInfo.MaterialReport.AccessoriesRpt();
                accerpt.Drawingno = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
                accerpt.Project = this.DrawingsDgv.CurrentRow.Cells["PROJECTID"].Value.ToString();
                accerpt.Blockno = Spool.GetBlockNo(this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString(),flag);
                accerpt.Flag = flag;
                accerpt.MdiParent = MDIForm.pMainWin;
                accerpt.Show();
            }
            else
            {
                MessageBox.Show("没有此图号的附件材料信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void 重量重心表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFlag();
            string sflag = string.Empty;
            string sql = string.Empty;
            if (flag == 0)
            {
                sflag = " and modifydrawingno is null";
                sql = "SELECT count(*) FROM SP_SPOOL_TAB where DRAWINGNO='" + this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString() + "' and projectid='" + this.DrawingsDgv.CurrentRow.Cells["PROJECTID"].Value.ToString() + "'" + sflag + " ";
            }
            else
            {
                sflag = " and modifydrawingno <> null";
                sql = "SELECT count(*) FROM SP_SPOOL_TAB where MODIFYDRAWINGNO='" + this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString() + "' and projectid='" + this.DrawingsDgv.CurrentRow.Cells["PROJECTID"].Value.ToString()+"'";
            }
            
            DataSet ds = new DataSet();
            OracleConnection sqlCon = new OracleConnection(DataAccess.OIDSConnStr);
            OracleCommand sqlCmd = new OracleCommand(sql, sqlCon);
            sqlCon.Open();
            int num = Convert.ToInt32(sqlCmd.ExecuteScalar());
            ds.Dispose();
            if (num > 0)
            {
                DetailInfo.MaterialReport.WeightBarycenterForm wbf = new DetailInfo.MaterialReport.WeightBarycenterForm();
                wbf._DrawingNo = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
                wbf._ProjectId = this.DrawingsDgv.CurrentRow.Cells["PROJECTID"].Value.ToString();
                wbf._Flag = flag;
                wbf.MdiParent = MDIForm.pMainWin;
                wbf.Show();
            }
            else
            {
                MessageBox.Show("没有此图号的重量中心信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void 合并PDF附页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string drawingno = this.DrawingsDgv.CurrentRow.Cells["DRAWINGNO"].Value.ToString();
            string project = this.textBox1.Text.ToString();
            GetFlag();
            GenerateMaterialForm gmfrm = new GenerateMaterialForm();
            gmfrm.Projectid = project;
            gmfrm.Drawing = drawingno;
            gmfrm.Indicator = flag;
            gmfrm.Version = Convert.ToInt16(this.DrawingsDgv.CurrentRow.Cells["REVISION"].Value.ToString());
            gmfrm.ShowDialog();
        }


    }
}
