namespace DetailInfo
{
    partial class frmPartInventory
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPartInventory));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pb1 = new System.Windows.Forms.ProgressBar();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.txt_unit = new System.Windows.Forms.TextBox();
            this.txt_preqty = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_project = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btn_export = new System.Windows.Forms.Button();
            this.tb_type = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.cmb_partno = new System.Windows.Forms.ComboBox();
            this.lbl_seqno = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_query = new System.Windows.Forms.Button();
            this.cmb_site = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_designcode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "系统图标.ico");
            this.imageList1.Images.SetKeyName(1, "folder_documents.png");
            this.imageList1.Images.SetKeyName(2, "kappfinder.png");
            this.imageList1.Images.SetKeyName(3, "kpersonalizer.png");
            this.imageList1.Images.SetKeyName(4, "Rank.ico");
            this.imageList1.Images.SetKeyName(5, "20090314152858Z1.jpg");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 540);
            this.splitter1.TabIndex = 24;
            this.splitter1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pb1);
            this.groupBox2.Controls.Add(this.dgv1);
            this.groupBox2.Controls.Add(this.txt_unit);
            this.groupBox2.Controls.Add(this.txt_preqty);
            this.groupBox2.Location = new System.Drawing.Point(2, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1090, 418);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "材料库存列表(双击材料进行查看申请详情和预留情况)";
            // 
            // pb1
            // 
            this.pb1.Location = new System.Drawing.Point(299, 214);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(498, 14);
            this.pb1.TabIndex = 53;
            this.pb1.Visible = false;
            // 
            // dgv1
            // 
            this.dgv1.AllowDrop = true;
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv1.Location = new System.Drawing.Point(3, 17);
            this.dgv1.MultiSelect = false;
            this.dgv1.Name = "dgv1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv1.Size = new System.Drawing.Size(1084, 398);
            this.dgv1.TabIndex = 1;
            this.dgv1.DoubleClick += new System.EventHandler(this.dgv1_DoubleClick);
            // 
            // txt_unit
            // 
            this.txt_unit.Location = new System.Drawing.Point(729, 40);
            this.txt_unit.Name = "txt_unit";
            this.txt_unit.ReadOnly = true;
            this.txt_unit.Size = new System.Drawing.Size(125, 21);
            this.txt_unit.TabIndex = 45;
            this.txt_unit.Visible = false;
            // 
            // txt_preqty
            // 
            this.txt_preqty.Location = new System.Drawing.Point(750, 40);
            this.txt_preqty.Name = "txt_preqty";
            this.txt_preqty.Size = new System.Drawing.Size(125, 21);
            this.txt_preqty.TabIndex = 34;
            this.txt_preqty.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_designcode);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmb_project);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.btn_export);
            this.groupBox1.Controls.Add(this.tb_type);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btn_close);
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Controls.Add(this.cmb_partno);
            this.groupBox1.Controls.Add(this.lbl_seqno);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_query);
            this.groupBox1.Controls.Add(this.cmb_site);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1089, 96);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(383, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 79;
            this.label7.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(54, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 78;
            this.label6.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(384, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 77;
            this.label5.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(54, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 76;
            this.label1.Text = "*";
            // 
            // cmb_project
            // 
            this.cmb_project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_project.FormattingEnabled = true;
            this.cmb_project.Location = new System.Drawing.Point(78, 16);
            this.cmb_project.Name = "cmb_project";
            this.cmb_project.Size = new System.Drawing.Size(228, 20);
            this.cmb_project.TabIndex = 75;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 74;
            this.label14.Text = "项目号";
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(152, 67);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(75, 23);
            this.btn_export.TabIndex = 23;
            this.btn_export.Text = "导出Excel";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.button3_Click);
            // 
            // tb_type
            // 
            this.tb_type.Location = new System.Drawing.Point(400, 42);
            this.tb_type.Name = "tb_type";
            this.tb_type.Size = new System.Drawing.Size(201, 21);
            this.tb_type.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(332, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 49;
            this.label4.Text = "零件描述";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(229, 67);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(71, 23);
            this.btn_close.TabIndex = 47;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(5, 67);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(71, 23);
            this.btn_new.TabIndex = 46;
            this.btn_new.Text = "清空";
            this.btn_new.UseVisualStyleBackColor = true;
            // 
            // cmb_partno
            // 
            this.cmb_partno.FormattingEnabled = true;
            this.cmb_partno.Location = new System.Drawing.Point(78, 41);
            this.cmb_partno.Name = "cmb_partno";
            this.cmb_partno.Size = new System.Drawing.Size(228, 20);
            this.cmb_partno.TabIndex = 41;
            // 
            // lbl_seqno
            // 
            this.lbl_seqno.AutoSize = true;
            this.lbl_seqno.ForeColor = System.Drawing.Color.Transparent;
            this.lbl_seqno.Location = new System.Drawing.Point(440, 7);
            this.lbl_seqno.Name = "lbl_seqno";
            this.lbl_seqno.Size = new System.Drawing.Size(0, 12);
            this.lbl_seqno.TabIndex = 40;
            this.lbl_seqno.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "零件编号";
            // 
            // btn_query
            // 
            this.btn_query.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_query.Location = new System.Drawing.Point(78, 67);
            this.btn_query.Name = "btn_query";
            this.btn_query.Size = new System.Drawing.Size(71, 23);
            this.btn_query.TabIndex = 26;
            this.btn_query.Text = "查询";
            this.btn_query.UseVisualStyleBackColor = true;
            this.btn_query.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // cmb_site
            // 
            this.cmb_site.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_site.FormattingEnabled = true;
            this.cmb_site.Location = new System.Drawing.Point(400, 16);
            this.cmb_site.Name = "cmb_site";
            this.cmb_site.Size = new System.Drawing.Size(201, 20);
            this.cmb_site.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "域";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1095, 540);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            // 
            // tb_designcode
            // 
            this.tb_designcode.Location = new System.Drawing.Point(691, 15);
            this.tb_designcode.Name = "tb_designcode";
            this.tb_designcode.Size = new System.Drawing.Size(201, 21);
            this.tb_designcode.TabIndex = 81;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(623, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 80;
            this.label8.Text = "DesignCode";
            // 
            // frmPartInventory
            // 
            this.AcceptButton = this.btn_query;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 540);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.splitter1);
            this.Name = "frmPartInventory";
            this.Text = "项目库存查询";
            this.Load += new System.EventHandler(this.frmPartInventory_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar pb1;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.TextBox txt_unit;
        private System.Windows.Forms.TextBox txt_preqty;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_project;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.TextBox tb_type;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.ComboBox cmb_partno;
        private System.Windows.Forms.Label lbl_seqno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_query;
        private System.Windows.Forms.ComboBox cmb_site;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_designcode;
        private System.Windows.Forms.Label label8;

    }
}