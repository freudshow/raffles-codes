namespace OutSourcingx64
{
    partial class Form_PhotoManager
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
            this.dTime_start = new System.Windows.Forms.DateTimePicker();
            this.dTime_end = new System.Windows.Forms.DateTimePicker();
            this.dGV_infolist = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_copy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_path = new System.Windows.Forms.Label();
            this.textPath = new System.Windows.Forms.TextBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.label_intime = new System.Windows.Forms.Label();
            this.btn_CreateDir = new System.Windows.Forms.Button();
            this.btn_CopyByIC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_infolist)).BeginInit();
            this.SuspendLayout();
            // 
            // dTime_start
            // 
            this.dTime_start.Location = new System.Drawing.Point(76, 13);
            this.dTime_start.Name = "dTime_start";
            this.dTime_start.Size = new System.Drawing.Size(97, 20);
            this.dTime_start.TabIndex = 0;
            // 
            // dTime_end
            // 
            this.dTime_end.Location = new System.Drawing.Point(204, 12);
            this.dTime_end.Name = "dTime_end";
            this.dTime_end.Size = new System.Drawing.Size(97, 20);
            this.dTime_end.TabIndex = 1;
            // 
            // dGV_infolist
            // 
            this.dGV_infolist.AllowUserToAddRows = false;
            this.dGV_infolist.AllowUserToDeleteRows = false;
            this.dGV_infolist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dGV_infolist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dGV_infolist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_infolist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dGV_infolist.Location = new System.Drawing.Point(12, 42);
            this.dGV_infolist.Name = "dGV_infolist";
            this.dGV_infolist.RowHeadersWidth = 20;
            this.dGV_infolist.Size = new System.Drawing.Size(660, 358);
            this.dGV_infolist.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "RECORD_ID";
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ID_NO";
            this.Column2.HeaderText = "身份证";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ID_PREFIX";
            this.Column3.HeaderText = "身份证后6位";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "NAME";
            this.Column4.HeaderText = "姓名";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "USED_NAME";
            this.Column5.HeaderText = "IC卡号";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "CREATE_DATE";
            this.Column6.HeaderText = "入职时间";
            this.Column6.Name = "Column6";
            // 
            // btn_copy
            // 
            this.btn_copy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_copy.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_copy.ForeColor = System.Drawing.Color.Black;
            this.btn_copy.Location = new System.Drawing.Point(12, 406);
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.Size = new System.Drawing.Size(139, 23);
            this.btn_copy.TabIndex = 3;
            this.btn_copy.Text = "复制照片(按身份证)";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_copy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "至";
            // 
            // label_path
            // 
            this.label_path.AutoSize = true;
            this.label_path.Location = new System.Drawing.Point(313, 14);
            this.label_path.Name = "label_path";
            this.label_path.Size = new System.Drawing.Size(43, 13);
            this.label_path.TabIndex = 6;
            this.label_path.Text = "文件夹";
            // 
            // textPath
            // 
            this.textPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textPath.Location = new System.Drawing.Point(362, 11);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(163, 20);
            this.textPath.TabIndex = 7;
            // 
            // btn_browse
            // 
            this.btn_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_browse.Location = new System.Drawing.Point(542, 10);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(47, 23);
            this.btn_browse.TabIndex = 8;
            this.btn_browse.Text = "浏览";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // label_intime
            // 
            this.label_intime.AutoSize = true;
            this.label_intime.Location = new System.Drawing.Point(16, 15);
            this.label_intime.Name = "label_intime";
            this.label_intime.Size = new System.Drawing.Size(55, 13);
            this.label_intime.TabIndex = 9;
            this.label_intime.Text = "入职时间";
            // 
            // btn_CreateDir
            // 
            this.btn_CreateDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CreateDir.ForeColor = System.Drawing.Color.Black;
            this.btn_CreateDir.Location = new System.Drawing.Point(595, 11);
            this.btn_CreateDir.Name = "btn_CreateDir";
            this.btn_CreateDir.Size = new System.Drawing.Size(84, 22);
            this.btn_CreateDir.TabIndex = 10;
            this.btn_CreateDir.Text = "创建文件夹";
            this.btn_CreateDir.UseVisualStyleBackColor = true;
            this.btn_CreateDir.Click += new System.EventHandler(this.btn_CreateDir_Click);
            // 
            // btn_CopyByIC
            // 
            this.btn_CopyByIC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_CopyByIC.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_CopyByIC.ForeColor = System.Drawing.Color.Black;
            this.btn_CopyByIC.Location = new System.Drawing.Point(556, 406);
            this.btn_CopyByIC.Name = "btn_CopyByIC";
            this.btn_CopyByIC.Size = new System.Drawing.Size(116, 23);
            this.btn_CopyByIC.TabIndex = 11;
            this.btn_CopyByIC.Text = "复制照片(按工号)";
            this.btn_CopyByIC.UseVisualStyleBackColor = true;
            this.btn_CopyByIC.Click += new System.EventHandler(this.btn_CopyByIC_Click);
            // 
            // Form_PhotoManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 432);
            this.Controls.Add(this.btn_CopyByIC);
            this.Controls.Add(this.btn_CreateDir);
            this.Controls.Add(this.label_intime);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.label_path);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_copy);
            this.Controls.Add(this.dGV_infolist);
            this.Controls.Add(this.dTime_end);
            this.Controls.Add(this.dTime_start);
            this.Name = "Form_PhotoManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "照片管理";
            ((System.ComponentModel.ISupportInitialize)(this.dGV_infolist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dTime_start;
        private System.Windows.Forms.DateTimePicker dTime_end;
        private System.Windows.Forms.DataGridView dGV_infolist;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_path;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Label label_intime;
        private System.Windows.Forms.Button btn_CreateDir;
        private System.Windows.Forms.Button btn_CopyByIC;
    }
}

