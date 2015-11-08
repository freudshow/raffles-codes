namespace DetailInfo
{
    partial class AssignTask
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.teamcb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OKbtn = new System.Windows.Forms.Button();
            this.Cancelbtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.workerdgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.picture = new System.Windows.Forms.DataGridViewImageColumn();
            this.NAME_CHN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME_ENG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROCESS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDCARD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workerdgv)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.teamcb);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(691, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "派工组选择";
            // 
            // teamcb
            // 
            this.teamcb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.teamcb.FormattingEnabled = true;
            this.teamcb.Location = new System.Drawing.Point(70, 13);
            this.teamcb.Name = "teamcb";
            this.teamcb.Size = new System.Drawing.Size(249, 21);
            this.teamcb.TabIndex = 1;
            this.teamcb.SelectedIndexChanged += new System.EventHandler(this.teamcb_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择组：";
            // 
            // OKbtn
            // 
            this.OKbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbtn.Location = new System.Drawing.Point(487, 382);
            this.OKbtn.Name = "OKbtn";
            this.OKbtn.Size = new System.Drawing.Size(90, 25);
            this.OKbtn.TabIndex = 1;
            this.OKbtn.Text = "确定";
            this.OKbtn.UseVisualStyleBackColor = true;
            this.OKbtn.Click += new System.EventHandler(this.OKbtn_Click);
            // 
            // Cancelbtn
            // 
            this.Cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelbtn.Location = new System.Drawing.Point(589, 382);
            this.Cancelbtn.Name = "Cancelbtn";
            this.Cancelbtn.Size = new System.Drawing.Size(90, 25);
            this.Cancelbtn.TabIndex = 2;
            this.Cancelbtn.Text = "取消";
            this.Cancelbtn.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.workerdgv);
            this.groupBox2.Location = new System.Drawing.Point(0, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(691, 326);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工人信息";
            // 
            // workerdgv
            // 
            this.workerdgv.AllowUserToAddRows = false;
            this.workerdgv.AllowUserToDeleteRows = false;
            this.workerdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.workerdgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.picture,
            this.NAME_CHN,
            this.NAME_ENG,
            this.PROCESS,
            this.IDCARD,
            this.TEL_NO});
            this.workerdgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workerdgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.workerdgv.Location = new System.Drawing.Point(3, 18);
            this.workerdgv.Name = "workerdgv";
            this.workerdgv.RowTemplate.Height = 23;
            this.workerdgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.workerdgv.Size = new System.Drawing.Size(685, 304);
            this.workerdgv.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.FalseValue = "0";
            this.Column1.HeaderText = "选择";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.TrueValue = "1";
            this.Column1.Width = 40;
            // 
            // picture
            // 
            this.picture.HeaderText = "照片";
            this.picture.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.picture.Name = "picture";
            this.picture.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // NAME_CHN
            // 
            this.NAME_CHN.DataPropertyName = "NAME_CHN";
            this.NAME_CHN.HeaderText = "姓名(中文)";
            this.NAME_CHN.Name = "NAME_CHN";
            this.NAME_CHN.ReadOnly = true;
            // 
            // NAME_ENG
            // 
            this.NAME_ENG.DataPropertyName = "NAME_ENG";
            this.NAME_ENG.HeaderText = "姓名(英文)";
            this.NAME_ENG.Name = "NAME_ENG";
            this.NAME_ENG.ReadOnly = true;
            // 
            // PROCESS
            // 
            this.PROCESS.DataPropertyName = "PROCESS";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PROCESS.DefaultCellStyle = dataGridViewCellStyle1;
            this.PROCESS.HeaderText = "预制中(数量)";
            this.PROCESS.Name = "PROCESS";
            this.PROCESS.ReadOnly = true;
            // 
            // IDCARD
            // 
            this.IDCARD.DataPropertyName = "IDCARD";
            this.IDCARD.HeaderText = "ID卡号";
            this.IDCARD.Name = "IDCARD";
            this.IDCARD.ReadOnly = true;
            // 
            // TEL_NO
            // 
            this.TEL_NO.DataPropertyName = "TEL_NO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.TEL_NO.DefaultCellStyle = dataGridViewCellStyle2;
            this.TEL_NO.HeaderText = "联系电话";
            this.TEL_NO.Name = "TEL_NO";
            this.TEL_NO.ReadOnly = true;
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // AssignTask
            // 
            this.AcceptButton = this.OKbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(691, 410);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Cancelbtn);
            this.Controls.Add(this.OKbtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AssignTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分派任务";
            this.Activated += new System.EventHandler(this.AssignTask_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AssignTask_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AssignTask_FormClosed);
            this.Load += new System.EventHandler(this.AssignTask_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.workerdgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox teamcb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OKbtn;
        private System.Windows.Forms.Button Cancelbtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView workerdgv;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn picture;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME_CHN;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME_ENG;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROCESS;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCARD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEL_NO;
    }
}