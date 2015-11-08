namespace DetailInfo
{
    partial class ApproveForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.disbtn = new System.Windows.Forms.Button();
            this.requestbtn = new System.Windows.Forms.Button();
            this.DRAWINGNOcomboBox = new System.Windows.Forms.ComboBox();
            this.PIDcomboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Appdgv = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Materialdgv = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Partdgv = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Appdgv)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Materialdgv)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Partdgv)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.disbtn);
            this.groupBox1.Controls.Add(this.requestbtn);
            this.groupBox1.Controls.Add(this.DRAWINGNOcomboBox);
            this.groupBox1.Controls.Add(this.PIDcomboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(713, 43);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "审核条件";
            // 
            // disbtn
            // 
            this.disbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.disbtn.Location = new System.Drawing.Point(475, 10);
            this.disbtn.Name = "disbtn";
            this.disbtn.Size = new System.Drawing.Size(75, 23);
            this.disbtn.TabIndex = 5;
            this.disbtn.Text = "不同意";
            this.disbtn.UseVisualStyleBackColor = true;
            this.disbtn.Click += new System.EventHandler(this.disbtn_Click);
            // 
            // requestbtn
            // 
            this.requestbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.requestbtn.Location = new System.Drawing.Point(384, 10);
            this.requestbtn.Name = "requestbtn";
            this.requestbtn.Size = new System.Drawing.Size(75, 23);
            this.requestbtn.TabIndex = 4;
            this.requestbtn.UseVisualStyleBackColor = true;
            this.requestbtn.Click += new System.EventHandler(this.requestbtn_Click);
            // 
            // DRAWINGNOcomboBox
            // 
            this.DRAWINGNOcomboBox.FormattingEnabled = true;
            this.DRAWINGNOcomboBox.Location = new System.Drawing.Point(246, 11);
            this.DRAWINGNOcomboBox.Name = "DRAWINGNOcomboBox";
            this.DRAWINGNOcomboBox.Size = new System.Drawing.Size(121, 20);
            this.DRAWINGNOcomboBox.TabIndex = 3;
            this.DRAWINGNOcomboBox.SelectedIndexChanged += new System.EventHandler(this.DRAWINGNOcomboBox_SelectedIndexChanged);
            // 
            // PIDcomboBox
            // 
            this.PIDcomboBox.FormattingEnabled = true;
            this.PIDcomboBox.Location = new System.Drawing.Point(56, 11);
            this.PIDcomboBox.Name = "PIDcomboBox";
            this.PIDcomboBox.Size = new System.Drawing.Size(121, 20);
            this.PIDcomboBox.TabIndex = 2;
            this.PIDcomboBox.SelectedIndexChanged += new System.EventHandler(this.PIDcomboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "图号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目号：";
            // 
            // Appdgv
            // 
            this.Appdgv.AllowUserToAddRows = false;
            this.Appdgv.AllowUserToDeleteRows = false;
            this.Appdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Appdgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Appdgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Appdgv.Location = new System.Drawing.Point(3, 3);
            this.Appdgv.Name = "Appdgv";
            this.Appdgv.RowTemplate.Height = 23;
            this.Appdgv.Size = new System.Drawing.Size(699, 275);
            this.Appdgv.TabIndex = 1;
            this.Appdgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Appdgv_CellFormatting);
            this.Appdgv.SelectionChanged += new System.EventHandler(this.Appdgv_SelectionChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 358);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(713, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(567, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(713, 306);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Appdgv);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(705, 281);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "小票信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Materialdgv);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(705, 281);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "材料信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Materialdgv
            // 
            this.Materialdgv.AllowUserToAddRows = false;
            this.Materialdgv.AllowUserToDeleteRows = false;
            this.Materialdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Materialdgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Materialdgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Materialdgv.Location = new System.Drawing.Point(3, 3);
            this.Materialdgv.Name = "Materialdgv";
            this.Materialdgv.RowTemplate.Height = 23;
            this.Materialdgv.Size = new System.Drawing.Size(699, 275);
            this.Materialdgv.TabIndex = 0;
            this.Materialdgv.SelectionChanged += new System.EventHandler(this.Materialdgv_SelectionChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Partdgv);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(705, 281);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "附件信息";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Partdgv
            // 
            this.Partdgv.AllowUserToAddRows = false;
            this.Partdgv.AllowUserToDeleteRows = false;
            this.Partdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Partdgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Partdgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Partdgv.Location = new System.Drawing.Point(3, 3);
            this.Partdgv.Name = "Partdgv";
            this.Partdgv.RowTemplate.Height = 23;
            this.Partdgv.Size = new System.Drawing.Size(699, 275);
            this.Partdgv.TabIndex = 0;
            this.Partdgv.SelectionChanged += new System.EventHandler(this.Partdgv_SelectionChanged);
            // 
            // ApproveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 380);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ApproveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.ApproveForm_Load);
            this.Activated += new System.EventHandler(this.ApproveForm_Activated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Appdgv)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Materialdgv)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Partdgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox DRAWINGNOcomboBox;
        private System.Windows.Forms.ComboBox PIDcomboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView Appdgv;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button requestbtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView Materialdgv;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView Partdgv;
        private System.Windows.Forms.Button disbtn;
    }
}