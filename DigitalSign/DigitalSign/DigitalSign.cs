using System;
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using AdeskRun = Autodesk.AutoCAD.Runtime;
using AdeskWin = Autodesk.AutoCAD.Windows;
using AdeskGeo = Autodesk.AutoCAD.Geometry;
using AdeskEdIn = Autodesk.AutoCAD.EditorInput;
using AdeskGra = Autodesk.AutoCAD.GraphicsInterface;
using AdeskDBSvr = Autodesk.AutoCAD.DatabaseServices;
using AdeskAppSvr = Autodesk.AutoCAD.ApplicationServices;
using AdeskInter = Autodesk.AutoCAD.Interop;

using System.Diagnostics;
using System.Threading;

namespace DigitalSign
{
    public class DigitalSign : System.Windows.Forms.Form
    {
        #region declarations
        private Label label_ICCard;
        private TextBox textBox_ICCard;
        private TextBox textBox_scale;
        private Label label_scale;
        private TextBox textBox_edion;
        private Label label_dwgID;
        private Label label_edition;
        private TextBox textBox_dwgID;
        private Label label_approveMan;
        private Label label_approveManSign;
        private ListBox listBox_approveMan;
        private GroupBox groupBox_dwginfo;
        private Button button_preview;
        private Button button_insert;
        private Button button_exit;
        private Label label1;
        private PictureBox pictureBox001;
        private PictureBox pictureBox005;
        private PictureBox pictureBox004;
        private PictureBox pictureBox003;
        private PictureBox pictureBox002;
        private IContainer components;
        public List<string> ICCardList;
        #endregion

        public DigitalSign()
        {
            //
            // Required for Windows Form Designer support
            //            
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            string iccard = string.Empty;
            if ((iccard = GetIC(System.Environment.UserName)) == string.Empty)
            {
                MessageBox.Show("系统查找不到您的IC卡号，请手动填写");
            }
            else
            {
                this.textBox_ICCard.Text = iccard;
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.label_ICCard = new System.Windows.Forms.Label();
            this.textBox_ICCard = new System.Windows.Forms.TextBox();
            this.textBox_scale = new System.Windows.Forms.TextBox();
            this.label_scale = new System.Windows.Forms.Label();
            this.label_approveMan = new System.Windows.Forms.Label();
            this.label_approveManSign = new System.Windows.Forms.Label();
            this.listBox_approveMan = new System.Windows.Forms.ListBox();
            this.textBox_edion = new System.Windows.Forms.TextBox();
            this.label_dwgID = new System.Windows.Forms.Label();
            this.label_edition = new System.Windows.Forms.Label();
            this.textBox_dwgID = new System.Windows.Forms.TextBox();
            this.groupBox_dwginfo = new System.Windows.Forms.GroupBox();
            this.pictureBox005 = new System.Windows.Forms.PictureBox();
            this.pictureBox004 = new System.Windows.Forms.PictureBox();
            this.pictureBox003 = new System.Windows.Forms.PictureBox();
            this.pictureBox002 = new System.Windows.Forms.PictureBox();
            this.pictureBox001 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_preview = new System.Windows.Forms.Button();
            this.button_insert = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.groupBox_dwginfo.SuspendLayout();
            this.ICCardList = new List<string>();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox005)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox004)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox003)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox002)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox001)).BeginInit();
            this.SuspendLayout();
            // 
            // label_ICCard
            // 
            this.label_ICCard.AutoSize = true;
            this.label_ICCard.Location = new System.Drawing.Point(4, 30);
            this.label_ICCard.Name = "label_ICCard";
            this.label_ICCard.Size = new System.Drawing.Size(90, 13);
            this.label_ICCard.TabIndex = 32;
            this.label_ICCard.Text = "设计员IC卡号:";
            // 
            // textBox_ICCard
            // 
            this.textBox_ICCard.Location = new System.Drawing.Point(97, 27);
            this.textBox_ICCard.Name = "textBox_ICCard";
            this.textBox_ICCard.Size = new System.Drawing.Size(100, 20);
            this.textBox_ICCard.TabIndex = 33;
            this.textBox_ICCard.Text = "Y00";
            // 
            // textBox_scale
            // 
            this.textBox_scale.Location = new System.Drawing.Point(281, 28);
            this.textBox_scale.Name = "textBox_scale";
            this.textBox_scale.Size = new System.Drawing.Size(50, 20);
            this.textBox_scale.TabIndex = 35;
            // 
            // label_scale
            // 
            this.label_scale.AutoSize = true;
            this.label_scale.Location = new System.Drawing.Point(205, 30);
            this.label_scale.Name = "label_scale";
            this.label_scale.Size = new System.Drawing.Size(70, 13);
            this.label_scale.TabIndex = 34;
            this.label_scale.Text = "插入比例 1: ";
            // 
            // label_approveMan
            // 
            this.label_approveMan.AutoSize = true;
            this.label_approveMan.Location = new System.Drawing.Point(6, 58);
            this.label_approveMan.Name = "label_approveMan";
            this.label_approveMan.Size = new System.Drawing.Size(46, 13);
            this.label_approveMan.TabIndex = 41;
            this.label_approveMan.Text = "审核人:";
            // 
            // label_approveManSign
            // 
            this.label_approveManSign.AutoSize = true;
            this.label_approveManSign.Location = new System.Drawing.Point(186, 58);
            this.label_approveManSign.Name = "label_approveManSign";
            this.label_approveManSign.Size = new System.Drawing.Size(70, 13);
            this.label_approveManSign.TabIndex = 42;
            this.label_approveManSign.Text = "审核人签字:";
            // 
            // listBox_approveMan
            // 
            this.listBox_approveMan.FormattingEnabled = true;
            this.listBox_approveMan.Location = new System.Drawing.Point(8, 74);
            this.listBox_approveMan.Name = "listBox_approveMan";
            this.listBox_approveMan.Size = new System.Drawing.Size(151, 264);
            this.listBox_approveMan.TabIndex = 40;
            // 
            // textBox_edition
            // 
            this.textBox_edion.Location = new System.Drawing.Point(235, 28);
            this.textBox_edion.Name = "textBox_edion";
            this.textBox_edion.Size = new System.Drawing.Size(50, 20);
            this.textBox_edion.TabIndex = 39;
            // 
            // label_dwgID
            // 
            this.label_dwgID.AutoSize = true;
            this.label_dwgID.Location = new System.Drawing.Point(5, 31);
            this.label_dwgID.Name = "label_dwgID";
            this.label_dwgID.Size = new System.Drawing.Size(46, 13);
            this.label_dwgID.TabIndex = 36;
            this.label_dwgID.Text = "图纸号:";
            // 
            // label_edion
            // 
            this.label_edition.AutoSize = true;
            this.label_edition.Location = new System.Drawing.Point(186, 34);
            this.label_edition.Name = "label_edion";
            this.label_edition.Size = new System.Drawing.Size(49, 13);
            this.label_edition.TabIndex = 38;
            this.label_edition.Text = "版本号: ";
            // 
            // textBox_dwgID
            // 
            this.textBox_dwgID.Location = new System.Drawing.Point(57, 28);
            this.textBox_dwgID.Name = "textBox_dwgID";
            this.textBox_dwgID.Size = new System.Drawing.Size(100, 20);
            this.textBox_dwgID.TabIndex = 37;
            // 
            // groupBox_dwginfo
            // 
            this.groupBox_dwginfo.Controls.Add(this.pictureBox005);
            this.groupBox_dwginfo.Controls.Add(this.pictureBox004);
            this.groupBox_dwginfo.Controls.Add(this.pictureBox003);
            this.groupBox_dwginfo.Controls.Add(this.pictureBox002);
            this.groupBox_dwginfo.Controls.Add(this.pictureBox001);
            this.groupBox_dwginfo.Controls.Add(this.label1);
            this.groupBox_dwginfo.Controls.Add(this.textBox_dwgID);
            this.groupBox_dwginfo.Controls.Add(this.label_approveMan);
            this.groupBox_dwginfo.Controls.Add(this.label_edition);
            this.groupBox_dwginfo.Controls.Add(this.label_dwgID);
            this.groupBox_dwginfo.Controls.Add(this.label_approveManSign);
            this.groupBox_dwginfo.Controls.Add(this.textBox_edion);
            this.groupBox_dwginfo.Controls.Add(this.listBox_approveMan);
            this.groupBox_dwginfo.Location = new System.Drawing.Point(7, 54);
            this.groupBox_dwginfo.Name = "groupBox_dwginfo";
            this.groupBox_dwginfo.Size = new System.Drawing.Size(324, 388);
            this.groupBox_dwginfo.TabIndex = 43;
            this.groupBox_dwginfo.TabStop = false;
            this.groupBox_dwginfo.Text = "图纸信息";
            // 
            // pictureBox005
            // 
            this.pictureBox005.Location = new System.Drawing.Point(189, 293);
            this.pictureBox005.Name = "pictureBox005";
            this.pictureBox005.Size = new System.Drawing.Size(108, 51);
            this.pictureBox005.TabIndex = 48;
            this.pictureBox005.TabStop = false;
            // 
            // pictureBox004
            // 
            this.pictureBox004.Location = new System.Drawing.Point(189, 238);
            this.pictureBox004.Name = "pictureBox004";
            this.pictureBox004.Size = new System.Drawing.Size(108, 51);
            this.pictureBox004.TabIndex = 47;
            this.pictureBox004.TabStop = false;
            // 
            // pictureBox003
            // 
            this.pictureBox003.Location = new System.Drawing.Point(189, 184);
            this.pictureBox003.Name = "pictureBox003";
            this.pictureBox003.Size = new System.Drawing.Size(108, 51);
            this.pictureBox003.TabIndex = 46;
            this.pictureBox003.TabStop = false;
            // 
            // pictureBox002
            // 
            this.pictureBox002.Location = new System.Drawing.Point(189, 128);
            this.pictureBox002.Name = "pictureBox002";
            this.pictureBox002.Size = new System.Drawing.Size(108, 51);
            this.pictureBox002.TabIndex = 45;
            this.pictureBox002.TabStop = false;
            // 
            // pictureBox001
            // 
            this.pictureBox001.Location = new System.Drawing.Point(189, 74);
            this.pictureBox001.Name = "pictureBox001";
            this.pictureBox001.Size = new System.Drawing.Size(108, 51);
            this.pictureBox001.TabIndex = 44;
            this.pictureBox001.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 26);
            this.label1.TabIndex = 43;
            this.label1.Text = "注意: 如果左边审核人信息有误,\r\n请联系您主管登陆到ECDMS中修改此纸的审批模版";
            // 
            // button_preview
            // 
            this.button_preview.Location = new System.Drawing.Point(19, 448);
            this.button_preview.Name = "button_preview";
            this.button_preview.Size = new System.Drawing.Size(75, 23);
            this.button_preview.TabIndex = 44;
            this.button_preview.Text = "预览";
            this.button_preview.UseVisualStyleBackColor = true;
            this.button_preview.Click += new System.EventHandler(this.button_preview_Click);
            // 
            // button_insert
            // 
            this.button_insert.Enabled = false;
            this.button_insert.Location = new System.Drawing.Point(136, 448);
            this.button_insert.Name = "button_insert";
            this.button_insert.Size = new System.Drawing.Size(75, 23);
            this.button_insert.TabIndex = 45;
            this.button_insert.Text = "插入";
            this.button_insert.UseVisualStyleBackColor = true;
            this.button_insert.Click += new System.EventHandler(this.button_insert_Click);
            //
            // button_exit
            //
            this.button_exit.Location = new System.Drawing.Point(242, 448);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(75, 23);
            this.button_exit.TabIndex = 46;
            this.button_exit.Text = "退出";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            //
            // DigitalSign
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(340, 475);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_insert);
            this.Controls.Add(this.button_preview);
            this.Controls.Add(this.groupBox_dwginfo);
            this.Controls.Add(this.textBox_scale);
            this.Controls.Add(this.label_scale);
            this.Controls.Add(this.textBox_ICCard);
            this.Controls.Add(this.label_ICCard);
            this.Name = "DigitalSign";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电子签名";
            this.groupBox_dwginfo.ResumeLayout(false);
            this.groupBox_dwginfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox005)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox004)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox003)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox002)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox001)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        #region Preview

        /// <summary>
        /// preview approving men informations and their signs
        /// </summary>        
        private void button_preview_Click(object sender, EventArgs e)
        {

            if (!checkInfo())
                return;

            string Approved = GetApproveMan(this.textBox_ICCard.Text.ToString(), this.textBox_dwgID.Text.ToString(), this.textBox_edion.Text.ToString());

            if (Approved == "NoDrawing")
            {
                MessageBox.Show("在系统里没有找到该版图纸!");
                return;
            }
            else if (Approved == "NoApproveTemplate")
            {
                MessageBox.Show("系统里存在该版图纸，但无审核模板，请联系您主管登陆到ECDMS中修改此纸的审批模版!");
                return;
            }
            else
            {
                string[] ApproveManArr = Approved.Split(';');
                this.ICCardList = GetICCardList(ApproveManArr);//Save IC card list
                this.listBox_approveMan.Items.Clear();//clear items first
                for (int i = 0; i < ApproveManArr.Length; i++)
                    this.listBox_approveMan.Items.Add(ApproveManArr[i]);

                AddImage(ApproveManArr);
                this.button_insert.Enabled = true;
            }
        }

        /// <summary>
        /// check if all informations are input properly
        /// if one or more informations are not valid
        ///     return false
        /// else 
        ///     return true
        /// </summary> 
        private bool checkInfo()
        {
            if (this.textBox_ICCard.Text.Length != 7)
            {
                MessageBox.Show("IC卡号输入有误,请重新输入!");
                return false;
            }

            if (this.textBox_dwgID.Text == "")
            {
                MessageBox.Show("请输入图纸号!");
                return false;
            }

            if (this.textBox_edion.Text == "")
            {
                MessageBox.Show("请输入版本号!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// get approve men's IC:name list using web reference
        /// </summary>
        /// <param name="IC_Card">IC number</param>
        /// <param name="dwg_id">drawing number</param>
        /// <param name="dwg_edition">drawing edition</param>
        /// <returns>approve men's names spliting by semicolon</returns>
        public string GetApproveMan(string IC_Card, string dwg_id, string dwg_edition)
        {
            return (new mWebRef.Drawing()).GetApproveTemplate(IC_Card, dwg_id, dwg_edition.ToString());
        }

        /// <summary>
        /// get designer's IC card
        /// </summary>
        /// <param name="username">designer's name</param>
        /// <returns>designer's ic card</returns>
        public string GetIC(string username)
        {
            return (new mUser.User()).GetICcard(username);
        }

        /// <summary>
        /// add approve men's signs onto pictureboxes
        /// </summary> 
        /// <param name="ApproveManArr">approve men's name list</param>
        private void AddImage(string[] ApproveManArr)
        {
            string jpgPath = @"\\172.16.7.55\sign$\jpg\";
            List<string> jpgNameList = GetSignJpg(ApproveManArr);

            List<string> NotFoundJpgList = FoundSign(jpgPath, jpgNameList);
            if (NotFoundJpgList.Count > 0)
            {
                string NotFoundStr = string.Empty;
                for (int i = 0; i < NotFoundJpgList.Count; i++)
                {
                    NotFoundStr += NotFoundJpgList[i].ToString() + ';';
                }
                MessageBox.Show("未找到 " + NotFoundStr + " 的预览签名图片，请联系软件开发组!");
                return;
            }

            List<PictureBox> picBoxList = new List<PictureBox>();
            picBoxList.Add(pictureBox001);
            picBoxList.Add(pictureBox002);
            picBoxList.Add(pictureBox003);
            picBoxList.Add(pictureBox004);
            picBoxList.Add(pictureBox005);
            //clear picture boxes first
            for (int i = 0; i < picBoxList.Count; i++)
                picBoxList[i].ImageLocation = string.Empty;
            //add images
            for (int i = 0; i < jpgNameList.Count; i++)
                picBoxList[i].ImageLocation = jpgPath + jpgNameList[i].ToString();
        }

        /// <summary>
        /// get approve men's IC Card List
        /// </summary>
        /// <param name="ApproveManArr">approve men's name list</param>
        /// <returns>approve men's IC Card List</returns>
        private List<string> GetICCardList(string[] ApproveManArr)
        {
            List<string> ICList = new List<string>();
            for (int i = 0; i < ApproveManArr.Length; i++)
                ICList.Add(ApproveManArr[i].Split(':')[0].ToString());

            return ICList;
        }

        /// <summary>
        /// get approve men's signature jpg-files's name list
        /// </summary>
        /// <param name="ApproveManArr">approve men's name list</param>
        /// <returns>signature jpg-files's name list</returns>
        private List<string> GetSignJpg(string[] ApproveManArr)
        {
            List<string> jpgNameList = new List<string>();
            for (int i = 0; i < ApproveManArr.Length; i++)
                jpgNameList.Add(ApproveManArr[i].Split(':')[0].ToString() + ".jpg");

            return jpgNameList;
        }

        /// <summary>
        /// check if approve men's signature files exist
        /// </summary>
        /// <param name="jpgPath">Directory that contains signature files</param>
        /// <param name="jpgNameList">signature files name list</param>
        /// <returns>the men's name list that their sign is not found</returns>
        private List<string> FoundSign(string SignPath, List<string> SignNameList)
        {
            List<string> NotFoundList = new List<string>();
            for (int i = 0; i < SignNameList.Count; i++)
                if (!System.IO.File.Exists(SignPath + SignNameList[i]))
                    NotFoundList.Add(SignNameList[i].ToString());

            return NotFoundList;
        }
        #endregion

        #region insert
        private void button_insert_Click(object sender, EventArgs e)
        {
            #region check info
            //if (this.textBox_scale.Text == "")
            //{
            //    MessageBox.Show("请输入图纸比例!");
            //    return;
            //}

            string dwgPath = @"\\172.16.7.55\sign$\dwg\";
            if (this.ICCardList.Count == 0)
            {
                MessageBox.Show("请先选择图纸及其责任人");
                return;
            }
            List<string> dwgNameList = GetSignDwg(this.ICCardList);
            //检查签名dwg是否存在
            List<string> NotFoundDwgList = FoundSign(dwgPath, dwgNameList);
            if (NotFoundDwgList.Count > 0)
            {
                string NotFoundStr = string.Empty;
                for (int i = 0; i < NotFoundDwgList.Count; i++)
                {
                    NotFoundStr += NotFoundDwgList[i].ToString() + ';';
                }
                MessageBox.Show("未找到 " + NotFoundStr + " 的预览签名文件，请联系软件开发组!");
                return;
            }
            #endregion

            this.Hide();
            InsertSign(dwgPath, dwgNameList);
            this.Show();
        }

        /// <summary>
        /// get Signature drawings
        /// </summary>
        /// <param name="IC_list"></param>
        /// <returns>signature dwg list</returns>
        private List<string> GetSignDwg(List<string> IC_list)
        {
            List<string> dwgNameList = new List<string>();
            for (int i = 0; i < IC_list.Count; i++)
                dwgNameList.Add(IC_list[i].ToString() + ".dwg");

            return dwgNameList;
        }

        /// <summary>
        /// insert signatures
        /// </summary>
        /// <param name="dwgPath">Signature drawings' path</param>
        /// <param name="dwgNameList">Signature drawing list</param>
        public void InsertSign(string dwgPath, List<string> dwgNameList)
        {
            AdeskAppSvr.Document doc = AdeskAppSvr.Application.DocumentManager.MdiActiveDocument;
            AdeskDBSvr.Database CurrDB = doc.Database;
            AdeskEdIn.Editor ed = doc.Editor;
            AdeskEdIn.PromptPointResult prPointRes_Base = ed.GetPoint(new AdeskEdIn.PromptPointOptions("选择插入的基点"));
            AdeskEdIn.PromptPointResult prPointRes_opp = ed.GetPoint(new AdeskEdIn.PromptPointOptions("选择基点的对角点"));
            //In order to make the signs look nicely , calculate the base point and its opposite point
            AdeskGeo.Point3d P_base = CalPoint(prPointRes_Base.Value, prPointRes_opp.Value, 0.1);
            AdeskGeo.Point3d P_opp = CalPoint(prPointRes_Base.Value, prPointRes_opp.Value, 0.9);
            //sign's width and height
            double SignWidth = P_opp.X - P_base.X;
            double SignHeight = P_opp.Y - P_base.Y;
            //distance between each other
            double distanceW = prPointRes_opp.Value.X - prPointRes_Base.Value.X;
            double distanceH = prPointRes_opp.Value.Y - prPointRes_Base.Value.Y;

            //current date
            string date = System.DateTime.Today.ToLocalTime().ToString().Split(' ')[0];

            try
            {
                for (int i = 0; i < dwgNameList.Count; i++)
                {
                    using (AdeskDBSvr.Database tmpdb = new AdeskDBSvr.Database(false, false))
                    {
                        //read drawing
                        tmpdb.ReadDwgFile(dwgPath + dwgNameList[i], FileShare.Read, true, null);
                        //insert Signature drawing as a new block into current drawing
                        AdeskDBSvr.ObjectId idBTR = CurrDB.Insert(this.ICCardList[i], tmpdb, false);
                        //scale of signature. 36 is the width of sign, and 17 is the height. you should adjust them in your condition.
                        double WidthOfSign;
                        double HeightOfSign;
                        double scaleWidth = SignWidth / 36;
                        double scaleHeight = SignHeight / 17;
                        using (AdeskDBSvr.Transaction trans = CurrDB.TransactionManager.StartTransaction())
                        {
                            AdeskDBSvr.BlockTable bt = (AdeskDBSvr.BlockTable)trans.GetObject(CurrDB.BlockTableId, AdeskDBSvr.OpenMode.ForRead);
                            AdeskDBSvr.BlockTableRecord btr = (AdeskDBSvr.BlockTableRecord)trans.GetObject(bt[AdeskDBSvr.BlockTableRecord.ModelSpace], AdeskDBSvr.OpenMode.ForWrite);

                            AdeskDBSvr.BlockTableRecord InBtr = (AdeskDBSvr.BlockTableRecord)trans.GetObject(idBTR, AdeskDBSvr.OpenMode.ForRead);
                            foreach (AdeskDBSvr.ObjectId tmpid in InBtr)
                            {
                                AdeskDBSvr.DBObject dbo = trans.GetObject(tmpid, AdeskDBSvr.OpenMode.ForRead);
                                if (dbo is AdeskDBSvr.Ole2Frame)
                                {
                                    AdeskDBSvr.Ole2Frame mOle = (AdeskDBSvr.Ole2Frame)dbo;
                                    WidthOfSign = mOle.WcsWidth;
                                    HeightOfSign = mOle.WcsHeight;
                                    scaleWidth = SignWidth / WidthOfSign;
                                    scaleHeight = SignHeight / HeightOfSign;
                                    break;
                                }
                            }
                            
                            //insert point of each signature and date from top to bottom
                            AdeskGeo.Point3d inPt = new AdeskGeo.Point3d(P_base.X, P_base.Y - i * distanceH, P_base.Z);

                            #region signature date
                            //signature date
                            AdeskDBSvr.MText SignDate = new AdeskDBSvr.MText();
                            AdeskDBSvr.TextStyleTable TextStyleTB = (AdeskDBSvr.TextStyleTable)trans.GetObject(CurrDB.TextStyleTableId, AdeskDBSvr.OpenMode.ForWrite);
                            AdeskDBSvr.TextStyleTableRecord TextStyleTBRec = new AdeskDBSvr.TextStyleTableRecord();

                            TextStyleTBRec.Font = new AdeskGra.FontDescriptor("宋体", true, false, 0, 0);
                            TextStyleTB.Add(TextStyleTBRec);
                            trans.AddNewlyCreatedDBObject(TextStyleTBRec, true);
                            SignDate.TextStyle = TextStyleTBRec.Id;
                            SignDate.Contents = date;
                            SignDate.TextHeight = SignHeight / 2;
                            SignDate.Width = SignWidth / 3;
                            //date's location should fit the frame
                            SignDate.Location = new AdeskGeo.Point3d((inPt.X + distanceW), (inPt.Y + 1.5 * SignDate.TextHeight), inPt.Z);
                            btr.AppendEntity(SignDate);
                            trans.AddNewlyCreatedDBObject(SignDate, true);
                            #endregion

                            try
                            {
                                //create a ref to the block
                                using (AdeskDBSvr.BlockReference bref = new AdeskDBSvr.BlockReference(inPt, idBTR))
                                {
                                    bref.ScaleFactors = new AdeskGeo.Scale3d(scaleWidth, scaleHeight, 1.0);
                                    btr.AppendEntity(bref);
                                    trans.AddNewlyCreatedDBObject(bref, true);
                                }
                                trans.Commit();
                            }
                            catch (System.Exception err)
                            {
                                MessageBox.Show("one: " + err.Message);
                            }
                        }
                    }
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception err)
            {
                MessageBox.Show("insert: " + err.Message);
            }
        }

        /// <summary>
        /// return a point on the segment of two points
        /// </summary>
        /// <param name="p1">first point</param>
        /// <param name="p2">second point</param>
        /// <param name="fac">factor</param>
        /// <returns>point on the segment of p1 and p2</returns>
        public AdeskGeo.Point3d CalPoint(AdeskGeo.Point3d p1, AdeskGeo.Point3d p2, double fac)
        {
            return (new AdeskGeo.Point3d(((1 - fac) * p1.X + fac * p2.X), ((1 - fac) * p1.Y + fac * p2.Y), ((1 - fac) * p1.Z + fac * p2.Z)));
        }

        /// <summary>
        /// exit this form
        /// </summary>
        private void button_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #endregion
    }

    public class AddMenu : AdeskRun.IExtensionApplication
    {
        private static AdeskWin.ContextMenuExtension mDigitalMenu;
        object path = null;
        public void Initialize()
        {
            if (!Runmain())
            {
                YCRO_menu();
                Attach();
            }
            else
            {
                MessageBox.Show("检测到电子签名程序需要更新，请稍等...","发现更新");
                System.Diagnostics.Process.Start(Path.GetDirectoryName(path.ToString()) + "\\" + "UpdateSoftProgram.exe");
                CloseCAD();
            }
        }

        public void Terminate()
        {
            Remove_YCRO_menu();
            Detach();
        }

        /// <summary>
        /// add context menus
        /// </summary>
        public static void Attach()
        {
            mDigitalMenu = new AdeskWin.ContextMenuExtension();
            mDigitalMenu.Title = "YCRO(.NET)";

            AdeskWin.MenuItem digital_mi = new AdeskWin.MenuItem("电子签名");
            digital_mi.Click += new EventHandler(ShowDigital);
            AdeskWin.MenuItem frame_mi = new AdeskWin.MenuItem("电子图框");
            frame_mi.Click += new EventHandler(ShowFrame);
            AdeskWin.MenuItem about_mi = new AdeskWin.MenuItem("关于");
            about_mi.Click += new EventHandler(ShowAbout);
            mDigitalMenu.MenuItems.Add(digital_mi);
            mDigitalMenu.MenuItems.Add(frame_mi);
            mDigitalMenu.MenuItems.Add(about_mi);
            AdeskAppSvr.Application.AddDefaultContextMenuExtension(mDigitalMenu);
        }

        /// <summary>
        /// remove context menus after exit AutoCAD
        /// </summary>
        public static void Detach()
        {
            AdeskAppSvr.Application.RemoveDefaultContextMenuExtension(mDigitalMenu);
        }

        /// <summary>
        /// add menus bar using com
        /// </summary>
        public static void YCRO_menu()
        {
            try
            {
                AdeskInter.AcadApplication acadApp = (AdeskInter.AcadApplication)AdeskAppSvr.Application.AcadApplication;
                AdeskInter.AcadPopupMenu mYCRO = acadApp.MenuGroups.Item(0).Menus.Add("YCRO(.NET)");
                AdeskInter.AcadPopupMenuItem mDigital = mYCRO.AddMenuItem(mYCRO.Count + 1, "电子签名", "ShowDigital ");
                AdeskInter.AcadPopupMenuItem mFrame = mYCRO.AddMenuItem(mYCRO.Count + 1, "电子图框", "ShowFrame ");
                AdeskInter.AcadPopupMenuItem mAbout = mYCRO.AddMenuItem(mYCRO.Count + 1, "关于", "ShowAbout ");
                mYCRO.InsertInMenuBar(acadApp.MenuBar.Count + 1);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// remove menus bar after exit AutoCAD
        /// </summary>
        public static void Remove_YCRO_menu()
        {
            AdeskInter.AcadApplication acadApp = (AdeskInter.AcadApplication)AdeskAppSvr.Application.AcadApplication;
            acadApp.MenuGroups.Item(0).Menus.RemoveMenuFromMenuBar("YCRO");
        }

        public static void ShowDigital(Object Sender, EventArgs e)
        {
            AdeskAppSvr.Document doc = AdeskAppSvr.Application.DocumentManager.MdiActiveDocument;
            doc.SendStringToExecute("UCS W ", false, false, true);//make current coordinate system to be WCS
            AdeskAppSvr.DocumentLock docLock = doc.LockDocument();
            DigitalSign mDigital = new DigitalSign();
            AdeskAppSvr.Application.ShowModalDialog(mDigital);
            docLock.Dispose();
        }

        /// <summary>
        /// show form
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        public static void ShowFrame(Object Sender, EventArgs e)
        {
            AdeskAppSvr.Document doc = AdeskAppSvr.Application.DocumentManager.MdiActiveDocument;
            doc.SendStringToExecute("UCS W ", false, false, true);//make current coordinate system to be WCS
            AdeskAppSvr.DocumentLock docLock = doc.LockDocument();
            NewForm mDigital = new NewForm();
            AdeskAppSvr.Application.ShowModalDialog(mDigital);
            docLock.Dispose();
        }

        /// <summary>
        /// show about form
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        public static void ShowAbout(Object Sender, EventArgs e)
        {
            AboutFrm mDigital = new AboutFrm();
            AdeskAppSvr.Application.ShowModalDialog(mDigital);
        }

        [AdeskRun.CommandMethod("ShowDigital")]
        public static void ShowDigitalMenu()
        {
            AdeskAppSvr.Document doc = AdeskAppSvr.Application.DocumentManager.MdiActiveDocument;
            doc.SendStringToExecute("UCS W ", false, false, true);//make current coordinate system to be WCS
            AdeskAppSvr.DocumentLock docLock = doc.LockDocument();
            DigitalSign mDigital = new DigitalSign();
            AdeskAppSvr.Application.ShowModalDialog(mDigital);
            docLock.Dispose();
        }

        [AdeskRun.CommandMethod("ShowFrame")]
        public static void ShowFrameMenu()
        {
            AdeskAppSvr.Document doc = AdeskAppSvr.Application.DocumentManager.MdiActiveDocument;
            doc.SendStringToExecute("UCS W ", false, false, true);//make current coordinate system to be WCS
            AdeskAppSvr.DocumentLock docLock = doc.LockDocument();
            NewForm mDigital = new NewForm();
            AdeskAppSvr.Application.ShowModalDialog(mDigital);
            docLock.Dispose();
        }

        [AdeskRun.CommandMethod("ShowAbout")]
        public static void ShowAbout()
        {
            AboutFrm mDigital = new AboutFrm();
            AdeskAppSvr.Application.ShowModalDialog(mDigital);
        }

        /// <summary>
        /// check updates
        /// </summary>
        /// <returns></returns>
        public bool Runmain()
        {
            RegistryKey YCRO = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Autodesk\AutoCAD\R16.2\ACAD-4001:409\Applications\YCRO_Digital");
            path = YCRO.GetValue("LOADER");
            SoftUpdate app = new SoftUpdate(path.ToString(), "UpdateProgram.zip");
            if (app.IsUpdate)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// close AutoCAD
        /// </summary>
        public void CloseCAD()
        {
            Process[] myProcesseses = Process.GetProcessesByName("acad");
            foreach (Process myProcess in myProcesseses)
            {
                myProcess.Kill();
            }
        }
    }
}