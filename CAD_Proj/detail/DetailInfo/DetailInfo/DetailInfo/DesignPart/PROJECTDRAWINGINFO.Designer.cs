namespace DetailInfo
{
    partial class PROJECTDRAWINGINFO
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("项目");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PROJECTDRAWINGINFO));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.modifyrbn = new System.Windows.Forms.RadioButton();
            this.drawingrbn = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsubmit = new System.Windows.Forms.Label();
            this.DrawingsDgv = new System.Windows.Forms.DataGridView();
            this.DrawingCMTSTRIP = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.详细信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.材料设备列表toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.生成材料附页StripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.管系托盘表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.管系材料表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.附件材料表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.重量重心表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.合并PDF附页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.添加封面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.生成PDF文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.打包图纸toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.提交审核toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.导出到EXCELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.querybtn = new System.Windows.Forms.Button();
            this.DRAWINGNOcomboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingsDgv)).BeginInit();
            this.DrawingCMTSTRIP.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(879, 548);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 1;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "PROJECTS";
            treeNode1.Text = "项目";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(127, 548);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "系统图标.ico");
            this.imageList1.Images.SetKeyName(1, "folder_documents.png");
            this.imageList1.Images.SetKeyName(2, "kappfinder.png");
            this.imageList1.Images.SetKeyName(3, "kpersonalizer.png");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.modifyrbn);
            this.groupBox3.Controls.Add(this.drawingrbn);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(748, 43);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "类型选择";
            // 
            // modifyrbn
            // 
            this.modifyrbn.AutoSize = true;
            this.modifyrbn.Enabled = false;
            this.modifyrbn.Location = new System.Drawing.Point(118, 20);
            this.modifyrbn.Name = "modifyrbn";
            this.modifyrbn.Size = new System.Drawing.Size(85, 17);
            this.modifyrbn.TabIndex = 1;
            this.modifyrbn.TabStop = true;
            this.modifyrbn.Text = "修改通知单";
            this.modifyrbn.UseVisualStyleBackColor = true;
            this.modifyrbn.CheckedChanged += new System.EventHandler(this.modifyrbn_CheckedChanged);
            // 
            // drawingrbn
            // 
            this.drawingrbn.AutoSize = true;
            this.drawingrbn.Location = new System.Drawing.Point(8, 20);
            this.drawingrbn.Name = "drawingrbn";
            this.drawingrbn.Size = new System.Drawing.Size(73, 17);
            this.drawingrbn.TabIndex = 0;
            this.drawingrbn.TabStop = true;
            this.drawingrbn.Text = "项目图纸";
            this.drawingrbn.UseVisualStyleBackColor = true;
            this.drawingrbn.CheckedChanged += new System.EventHandler(this.drawingrbn_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(748, 23);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 18);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(522, 18);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 17);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lsubmit);
            this.groupBox2.Controls.Add(this.DrawingsDgv);
            this.groupBox2.Location = new System.Drawing.Point(2, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(746, 420);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图纸信息";
            // 
            // lsubmit
            // 
            this.lsubmit.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lsubmit.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsubmit.Location = new System.Drawing.Point(67, 132);
            this.lsubmit.Name = "lsubmit";
            this.lsubmit.Size = new System.Drawing.Size(502, 40);
            this.lsubmit.TabIndex = 1;
            this.lsubmit.Text = "正在将图纸提交审核，请稍后…………";
            this.lsubmit.Visible = false;
            // 
            // DrawingsDgv
            // 
            this.DrawingsDgv.AllowUserToAddRows = false;
            this.DrawingsDgv.AllowUserToDeleteRows = false;
            this.DrawingsDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DrawingsDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DrawingsDgv.ContextMenuStrip = this.DrawingCMTSTRIP;
            this.DrawingsDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingsDgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DrawingsDgv.Location = new System.Drawing.Point(3, 18);
            this.DrawingsDgv.Name = "DrawingsDgv";
            this.DrawingsDgv.RowTemplate.Height = 23;
            this.DrawingsDgv.Size = new System.Drawing.Size(740, 399);
            this.DrawingsDgv.TabIndex = 0;
            this.DrawingsDgv.SelectionChanged += new System.EventHandler(this.DrawingsDgv_SelectionChanged);
            this.DrawingsDgv.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingsDgv_MouseUp);
            // 
            // DrawingCMTSTRIP
            // 
            this.DrawingCMTSTRIP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.详细信息ToolStripMenuItem,
            this.toolStripSeparator3,
            this.材料设备列表toolStripMenuItem,
            this.toolStripSeparator12,
            this.生成材料附页StripMenuItem1,
            this.toolStripSeparator7,
            this.添加封面ToolStripMenuItem,
            this.toolStripSeparator5,
            this.生成PDF文件ToolStripMenuItem,
            this.toolStripSeparator2,
            this.打包图纸toolStripMenuItem,
            this.toolStripSeparator4,
            this.提交审核toolStripMenuItem,
            this.toolStripSeparator6,
            this.导出到EXCELToolStripMenuItem});
            this.DrawingCMTSTRIP.Name = "DrawingCMTSTRIP";
            this.DrawingCMTSTRIP.Size = new System.Drawing.Size(193, 222);
            // 
            // 详细信息ToolStripMenuItem
            // 
            this.详细信息ToolStripMenuItem.Name = "详细信息ToolStripMenuItem";
            this.详细信息ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.详细信息ToolStripMenuItem.Text = "详细信息";
            this.详细信息ToolStripMenuItem.Click += new System.EventHandler(this.详细信息ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(189, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // 材料设备列表toolStripMenuItem
            // 
            this.材料设备列表toolStripMenuItem.Name = "材料设备列表toolStripMenuItem";
            this.材料设备列表toolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.材料设备列表toolStripMenuItem.Text = "查看材料设备列表";
            this.材料设备列表toolStripMenuItem.Click += new System.EventHandler(this.材料设备列表toolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(189, 6);
            // 
            // 生成材料附页StripMenuItem1
            // 
            this.生成材料附页StripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管系托盘表ToolStripMenuItem,
            this.toolStripSeparator9,
            this.管系材料表ToolStripMenuItem,
            this.toolStripSeparator8,
            this.附件材料表ToolStripMenuItem,
            this.toolStripSeparator10,
            this.重量重心表ToolStripMenuItem,
            this.toolStripSeparator11,
            this.合并PDF附页ToolStripMenuItem});
            this.生成材料附页StripMenuItem1.Name = "生成材料附页StripMenuItem1";
            this.生成材料附页StripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.生成材料附页StripMenuItem1.Text = "生成材料附页";
            // 
            // 管系托盘表ToolStripMenuItem
            // 
            this.管系托盘表ToolStripMenuItem.Name = "管系托盘表ToolStripMenuItem";
            this.管系托盘表ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.管系托盘表ToolStripMenuItem.Text = "管系托盘表";
            this.管系托盘表ToolStripMenuItem.Click += new System.EventHandler(this.管系托盘表ToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(138, 6);
            // 
            // 管系材料表ToolStripMenuItem
            // 
            this.管系材料表ToolStripMenuItem.Name = "管系材料表ToolStripMenuItem";
            this.管系材料表ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.管系材料表ToolStripMenuItem.Text = "管系附件表";
            this.管系材料表ToolStripMenuItem.Click += new System.EventHandler(this.管系材料表ToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(138, 6);
            // 
            // 附件材料表ToolStripMenuItem
            // 
            this.附件材料表ToolStripMenuItem.Name = "附件材料表ToolStripMenuItem";
            this.附件材料表ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.附件材料表ToolStripMenuItem.Text = "附件材料表";
            this.附件材料表ToolStripMenuItem.Click += new System.EventHandler(this.附件材料表ToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(138, 6);
            // 
            // 重量重心表ToolStripMenuItem
            // 
            this.重量重心表ToolStripMenuItem.Name = "重量重心表ToolStripMenuItem";
            this.重量重心表ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.重量重心表ToolStripMenuItem.Text = "重量重心表";
            this.重量重心表ToolStripMenuItem.Click += new System.EventHandler(this.重量重心表ToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(138, 6);
            // 
            // 合并PDF附页ToolStripMenuItem
            // 
            this.合并PDF附页ToolStripMenuItem.Name = "合并PDF附页ToolStripMenuItem";
            this.合并PDF附页ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.合并PDF附页ToolStripMenuItem.Text = "合并PDF附页";
            this.合并PDF附页ToolStripMenuItem.Click += new System.EventHandler(this.合并PDF附页ToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(189, 6);
            // 
            // 添加封面ToolStripMenuItem
            // 
            this.添加封面ToolStripMenuItem.Name = "添加封面ToolStripMenuItem";
            this.添加封面ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.添加封面ToolStripMenuItem.Text = "生成封面";
            this.添加封面ToolStripMenuItem.Click += new System.EventHandler(this.生成封面ToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(189, 6);
            // 
            // 生成PDF文件ToolStripMenuItem
            // 
            this.生成PDF文件ToolStripMenuItem.Name = "生成PDF文件ToolStripMenuItem";
            this.生成PDF文件ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.生成PDF文件ToolStripMenuItem.Text = "生成PDF图纸";
            this.生成PDF文件ToolStripMenuItem.Click += new System.EventHandler(this.生成PDF文件ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(189, 6);
            // 
            // 打包图纸toolStripMenuItem
            // 
            this.打包图纸toolStripMenuItem.Name = "打包图纸toolStripMenuItem";
            this.打包图纸toolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.打包图纸toolStripMenuItem.Text = "打包图纸信息";
            this.打包图纸toolStripMenuItem.Click += new System.EventHandler(this.打包图纸toolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(189, 6);
            // 
            // 提交审核toolStripMenuItem
            // 
            this.提交审核toolStripMenuItem.Name = "提交审核toolStripMenuItem";
            this.提交审核toolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.提交审核toolStripMenuItem.Text = "提交至ECDMS进行审核";
            this.提交审核toolStripMenuItem.Click += new System.EventHandler(this.提交审核toolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(189, 6);
            // 
            // 导出到EXCELToolStripMenuItem
            // 
            this.导出到EXCELToolStripMenuItem.Name = "导出到EXCELToolStripMenuItem";
            this.导出到EXCELToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.导出到EXCELToolStripMenuItem.Text = "导出到EXCEL";
            this.导出到EXCELToolStripMenuItem.Click += new System.EventHandler(this.导出到EXCELToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.querybtn);
            this.groupBox1.Controls.Add(this.DRAWINGNOcomboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(748, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // querybtn
            // 
            this.querybtn.Location = new System.Drawing.Point(449, 11);
            this.querybtn.Name = "querybtn";
            this.querybtn.Size = new System.Drawing.Size(75, 25);
            this.querybtn.TabIndex = 6;
            this.querybtn.Text = "查询";
            this.querybtn.UseVisualStyleBackColor = true;
            this.querybtn.Click += new System.EventHandler(this.querybtn_Click);
            // 
            // DRAWINGNOcomboBox
            // 
            this.DRAWINGNOcomboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DRAWINGNOcomboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DRAWINGNOcomboBox.FormattingEnabled = true;
            this.DRAWINGNOcomboBox.Location = new System.Drawing.Point(219, 13);
            this.DRAWINGNOcomboBox.Name = "DRAWINGNOcomboBox";
            this.DRAWINGNOcomboBox.Size = new System.Drawing.Size(199, 21);
            this.DRAWINGNOcomboBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "图号:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(126, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目:";
            // 
            // PROJECTDRAWINGINFO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 548);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "PROJECTDRAWINGINFO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "项目图纸号概览";
            this.Activated += new System.EventHandler(this.PROJECTDRAWINGINFO_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PROJECTDRAWINGINFO_FormClosed);
            this.Load += new System.EventHandler(this.PROJECTDRAWINGINFO_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DrawingsDgv)).EndInit();
            this.DrawingCMTSTRIP.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView DrawingsDgv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button querybtn;
        private System.Windows.Forms.ComboBox DRAWINGNOcomboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip DrawingCMTSTRIP;
        private System.Windows.Forms.ToolStripMenuItem 详细信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加封面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成PDF文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出到EXCELToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem 打包图纸toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 提交审核toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Label lsubmit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton modifyrbn;
        private System.Windows.Forms.RadioButton drawingrbn;
        private System.Windows.Forms.ToolStripMenuItem 生成材料附页StripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem 管系材料表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem 附件材料表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem 管系托盘表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem 重量重心表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem 合并PDF附页ToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem 材料设备列表toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;


    }
}