namespace DigitalSign
{
    partial class NewForm
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
            this.cmbproject = new System.Windows.Forms.ComboBox();
            this.gbproject = new System.Windows.Forms.GroupBox();
            this.lblclass = new System.Windows.Forms.Label();
            this.lblowner = new System.Windows.Forms.Label();
            this.lblpname = new System.Windows.Forms.Label();
            this.lblc = new System.Windows.Forms.Label();
            this.lblo = new System.Windows.Forms.Label();
            this.lblpj = new System.Windows.Forms.Label();
            this.lblproject = new System.Windows.Forms.Label();
            this.btnok = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblresuser = new System.Windows.Forms.Label();
            this.lblname2 = new System.Windows.Forms.Label();
            this.lblname1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbc0 = new System.Windows.Forms.RadioButton();
            this.rbc1 = new System.Windows.Forms.RadioButton();
            this.rbc2 = new System.Windows.Forms.RadioButton();
            this.rbc3 = new System.Windows.Forms.RadioButton();
            this.rbc4 = new System.Windows.Forms.RadioButton();
            this.rbb0 = new System.Windows.Forms.RadioButton();
            this.rbb1 = new System.Windows.Forms.RadioButton();
            this.rbb2 = new System.Windows.Forms.RadioButton();
            this.rbb3 = new System.Windows.Forms.RadioButton();
            this.rbb4 = new System.Windows.Forms.RadioButton();
            this.rba0 = new System.Windows.Forms.RadioButton();
            this.rba1 = new System.Windows.Forms.RadioButton();
            this.rba2 = new System.Windows.Forms.RadioButton();
            this.rba3 = new System.Windows.Forms.RadioButton();
            this.rba4 = new System.Windows.Forms.RadioButton();
            this.txtDrawNo = new System.Windows.Forms.TextBox();
            this.txtRev = new System.Windows.Forms.TextBox();
            this.cmbdiscip = new System.Windows.Forms.ComboBox();
            this.dg = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbldesc = new System.Windows.Forms.Label();
            this.lblcount = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.lblpfullname = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbproject.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbproject
            // 
            this.cmbproject.FormattingEnabled = true;
            this.cmbproject.Location = new System.Drawing.Point(14, 26);
            this.cmbproject.Name = "cmbproject";
            this.cmbproject.Size = new System.Drawing.Size(232, 21);
            this.cmbproject.TabIndex = 0;
            this.cmbproject.SelectedIndexChanged += new System.EventHandler(this.cmbproject_SelectedIndexChanged);
            // 
            // gbproject
            // 
            this.gbproject.Controls.Add(this.lblclass);
            this.gbproject.Controls.Add(this.lblowner);
            this.gbproject.Controls.Add(this.lblpname);
            this.gbproject.Controls.Add(this.lblc);
            this.gbproject.Controls.Add(this.lblo);
            this.gbproject.Controls.Add(this.lblpj);
            this.gbproject.Location = new System.Drawing.Point(12, 53);
            this.gbproject.Name = "gbproject";
            this.gbproject.Size = new System.Drawing.Size(338, 193);
            this.gbproject.TabIndex = 1;
            this.gbproject.TabStop = false;
            this.gbproject.Text = "项目信息";
            // 
            // lblclass
            // 
            this.lblclass.Location = new System.Drawing.Point(89, 122);
            this.lblclass.Name = "lblclass";
            this.lblclass.Size = new System.Drawing.Size(243, 67);
            this.lblclass.TabIndex = 6;
            // 
            // lblowner
            // 
            this.lblowner.AutoSize = true;
            this.lblowner.Location = new System.Drawing.Point(89, 80);
            this.lblowner.Name = "lblowner";
            this.lblowner.Size = new System.Drawing.Size(0, 13);
            this.lblowner.TabIndex = 5;
            // 
            // lblpname
            // 
            this.lblpname.AutoSize = true;
            this.lblpname.Location = new System.Drawing.Point(89, 36);
            this.lblpname.Name = "lblpname";
            this.lblpname.Size = new System.Drawing.Size(0, 13);
            this.lblpname.TabIndex = 4;
            // 
            // lblc
            // 
            this.lblc.AutoSize = true;
            this.lblc.Location = new System.Drawing.Point(13, 122);
            this.lblc.Name = "lblc";
            this.lblc.Size = new System.Drawing.Size(79, 13);
            this.lblc.TabIndex = 3;
            this.lblc.Text = "项目船级社：";
            // 
            // lblo
            // 
            this.lblo.AutoSize = true;
            this.lblo.Location = new System.Drawing.Point(13, 80);
            this.lblo.Name = "lblo";
            this.lblo.Size = new System.Drawing.Size(67, 13);
            this.lblo.TabIndex = 2;
            this.lblo.Text = "项目船东：";
            // 
            // lblpj
            // 
            this.lblpj.AutoSize = true;
            this.lblpj.Location = new System.Drawing.Point(13, 36);
            this.lblpj.Name = "lblpj";
            this.lblpj.Size = new System.Drawing.Size(67, 13);
            this.lblpj.TabIndex = 1;
            this.lblpj.Text = "项目名称：";
            // 
            // lblproject
            // 
            this.lblproject.AutoSize = true;
            this.lblproject.Location = new System.Drawing.Point(15, 7);
            this.lblproject.Name = "lblproject";
            this.lblproject.Size = new System.Drawing.Size(67, 13);
            this.lblproject.TabIndex = 0;
            this.lblproject.Text = "项目名称：";
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(463, 574);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(75, 25);
            this.btnok.TabIndex = 2;
            this.btnok.Text = "确定";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblresuser);
            this.groupBox1.Controls.Add(this.lblname2);
            this.groupBox1.Controls.Add(this.lblname1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 281);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 153);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图纸信息";
            // 
            // lblresuser
            // 
            this.lblresuser.AutoSize = true;
            this.lblresuser.Location = new System.Drawing.Point(89, 122);
            this.lblresuser.Name = "lblresuser";
            this.lblresuser.Size = new System.Drawing.Size(0, 13);
            this.lblresuser.TabIndex = 6;
            // 
            // lblname2
            // 
            this.lblname2.AutoSize = true;
            this.lblname2.Location = new System.Drawing.Point(89, 80);
            this.lblname2.Name = "lblname2";
            this.lblname2.Size = new System.Drawing.Size(0, 13);
            this.lblname2.TabIndex = 5;
            // 
            // lblname1
            // 
            this.lblname1.AutoSize = true;
            this.lblname1.Location = new System.Drawing.Point(89, 36);
            this.lblname1.Name = "lblname1";
            this.lblname1.Size = new System.Drawing.Size(0, 13);
            this.lblname1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "责任人：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "图纸英文名：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "图纸中文名：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "图号：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(252, 252);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "版本号：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbc0);
            this.groupBox2.Controls.Add(this.rbc1);
            this.groupBox2.Controls.Add(this.rbc2);
            this.groupBox2.Controls.Add(this.rbc3);
            this.groupBox2.Controls.Add(this.rbc4);
            this.groupBox2.Controls.Add(this.rbb0);
            this.groupBox2.Controls.Add(this.rbb1);
            this.groupBox2.Controls.Add(this.rbb2);
            this.groupBox2.Controls.Add(this.rbb3);
            this.groupBox2.Controls.Add(this.rbb4);
            this.groupBox2.Controls.Add(this.rba0);
            this.groupBox2.Controls.Add(this.rba1);
            this.groupBox2.Controls.Add(this.rba2);
            this.groupBox2.Controls.Add(this.rba3);
            this.groupBox2.Controls.Add(this.rba4);
            this.groupBox2.Location = new System.Drawing.Point(12, 441);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 170);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图纸模板";
            // 
            // rbc0
            // 
            this.rbc0.AutoSize = true;
            this.rbc0.Location = new System.Drawing.Point(192, 133);
            this.rbc0.Name = "rbc0";
            this.rbc0.Size = new System.Drawing.Size(109, 17);
            this.rbc0.TabIndex = 23;
            this.rbc0.TabStop = true;
            this.rbc0.Text = "管路小票标题框";
            this.rbc0.UseVisualStyleBackColor = true;
            // 
            // rbc1
            // 
            this.rbc1.AutoSize = true;
            this.rbc1.Location = new System.Drawing.Point(192, 109);
            this.rbc1.Name = "rbc1";
            this.rbc1.Size = new System.Drawing.Size(134, 17);
            this.rbc1.TabIndex = 22;
            this.rbc1.TabStop = true;
            this.rbc1.Text = "修改通知单A3标题框";
            this.rbc1.UseVisualStyleBackColor = true;
            // 
            // rbc2
            // 
            this.rbc2.AutoSize = true;
            this.rbc2.Location = new System.Drawing.Point(192, 86);
            this.rbc2.Name = "rbc2";
            this.rbc2.Size = new System.Drawing.Size(134, 17);
            this.rbc2.TabIndex = 21;
            this.rbc2.TabStop = true;
            this.rbc2.Text = "修改通知单A4标题框";
            this.rbc2.UseVisualStyleBackColor = true;
            // 
            // rbc3
            // 
            this.rbc3.AutoSize = true;
            this.rbc3.Location = new System.Drawing.Point(192, 62);
            this.rbc3.Name = "rbc3";
            this.rbc3.Size = new System.Drawing.Size(85, 17);
            this.rbc3.TabIndex = 20;
            this.rbc3.TabStop = true;
            this.rbc3.Text = "修改通知单";
            this.rbc3.UseVisualStyleBackColor = true;
            // 
            // rbc4
            // 
            this.rbc4.AutoSize = true;
            this.rbc4.Location = new System.Drawing.Point(192, 38);
            this.rbc4.Name = "rbc4";
            this.rbc4.Size = new System.Drawing.Size(115, 17);
            this.rbc4.TabIndex = 19;
            this.rbc4.TabStop = true;
            this.rbc4.Text = "修改通知单(加急)";
            this.rbc4.UseVisualStyleBackColor = true;
            // 
            // rbb0
            // 
            this.rbb0.AutoSize = true;
            this.rbb0.Location = new System.Drawing.Point(91, 133);
            this.rbb0.Name = "rbb0";
            this.rbb0.Size = new System.Drawing.Size(98, 17);
            this.rbb0.TabIndex = 18;
            this.rbb0.TabStop = true;
            this.rbb0.Text = "A0内容标题框";
            this.rbb0.UseVisualStyleBackColor = true;
            // 
            // rbb1
            // 
            this.rbb1.AutoSize = true;
            this.rbb1.Location = new System.Drawing.Point(91, 109);
            this.rbb1.Name = "rbb1";
            this.rbb1.Size = new System.Drawing.Size(98, 17);
            this.rbb1.TabIndex = 17;
            this.rbb1.TabStop = true;
            this.rbb1.Text = "A1内容标题框";
            this.rbb1.UseVisualStyleBackColor = true;
            // 
            // rbb2
            // 
            this.rbb2.AutoSize = true;
            this.rbb2.Location = new System.Drawing.Point(91, 86);
            this.rbb2.Name = "rbb2";
            this.rbb2.Size = new System.Drawing.Size(98, 17);
            this.rbb2.TabIndex = 16;
            this.rbb2.TabStop = true;
            this.rbb2.Text = "A2内容标题框";
            this.rbb2.UseVisualStyleBackColor = true;
            // 
            // rbb3
            // 
            this.rbb3.AutoSize = true;
            this.rbb3.Location = new System.Drawing.Point(91, 62);
            this.rbb3.Name = "rbb3";
            this.rbb3.Size = new System.Drawing.Size(98, 17);
            this.rbb3.TabIndex = 15;
            this.rbb3.TabStop = true;
            this.rbb3.Text = "A3内容标题框";
            this.rbb3.UseVisualStyleBackColor = true;
            // 
            // rbb4
            // 
            this.rbb4.AutoSize = true;
            this.rbb4.Location = new System.Drawing.Point(91, 38);
            this.rbb4.Name = "rbb4";
            this.rbb4.Size = new System.Drawing.Size(98, 17);
            this.rbb4.TabIndex = 14;
            this.rbb4.TabStop = true;
            this.rbb4.Text = "A4内容标题框";
            this.rbb4.UseVisualStyleBackColor = true;
            // 
            // rba0
            // 
            this.rba0.AutoSize = true;
            this.rba0.Location = new System.Drawing.Point(15, 133);
            this.rba0.Name = "rba0";
            this.rba0.Size = new System.Drawing.Size(62, 17);
            this.rba0.TabIndex = 13;
            this.rba0.TabStop = true;
            this.rba0.Text = "A0封面";
            this.rba0.UseVisualStyleBackColor = true;
            // 
            // rba1
            // 
            this.rba1.AutoSize = true;
            this.rba1.Location = new System.Drawing.Point(15, 111);
            this.rba1.Name = "rba1";
            this.rba1.Size = new System.Drawing.Size(62, 17);
            this.rba1.TabIndex = 12;
            this.rba1.TabStop = true;
            this.rba1.Text = "A1封面";
            this.rba1.UseVisualStyleBackColor = true;
            // 
            // rba2
            // 
            this.rba2.AutoSize = true;
            this.rba2.Location = new System.Drawing.Point(15, 86);
            this.rba2.Name = "rba2";
            this.rba2.Size = new System.Drawing.Size(62, 17);
            this.rba2.TabIndex = 11;
            this.rba2.TabStop = true;
            this.rba2.Text = "A2封面";
            this.rba2.UseVisualStyleBackColor = true;
            // 
            // rba3
            // 
            this.rba3.AutoSize = true;
            this.rba3.Location = new System.Drawing.Point(15, 62);
            this.rba3.Name = "rba3";
            this.rba3.Size = new System.Drawing.Size(62, 17);
            this.rba3.TabIndex = 10;
            this.rba3.TabStop = true;
            this.rba3.Text = "A3封面";
            this.rba3.UseVisualStyleBackColor = true;
            // 
            // rba4
            // 
            this.rba4.AutoSize = true;
            this.rba4.Location = new System.Drawing.Point(15, 38);
            this.rba4.Name = "rba4";
            this.rba4.Size = new System.Drawing.Size(62, 17);
            this.rba4.TabIndex = 9;
            this.rba4.TabStop = true;
            this.rba4.Text = "A4封面";
            this.rba4.UseVisualStyleBackColor = true;
            // 
            // txtDrawNo
            // 
            this.txtDrawNo.Location = new System.Drawing.Point(48, 252);
            this.txtDrawNo.Name = "txtDrawNo";
            this.txtDrawNo.Size = new System.Drawing.Size(198, 20);
            this.txtDrawNo.TabIndex = 7;
            this.txtDrawNo.TextChanged += new System.EventHandler(this.txtDrawNo_TextChanged);
            // 
            // txtRev
            // 
            this.txtRev.Location = new System.Drawing.Point(302, 252);
            this.txtRev.Name = "txtRev";
            this.txtRev.Size = new System.Drawing.Size(48, 20);
            this.txtRev.TabIndex = 8;
            this.txtRev.TextChanged += new System.EventHandler(this.txtRev_TextChanged);
            // 
            // cmbdiscip
            // 
            this.cmbdiscip.FormattingEnabled = true;
            this.cmbdiscip.Location = new System.Drawing.Point(565, 26);
            this.cmbdiscip.Name = "cmbdiscip";
            this.cmbdiscip.Size = new System.Drawing.Size(203, 21);
            this.cmbdiscip.TabIndex = 9;
            this.cmbdiscip.SelectedIndexChanged += new System.EventHandler(this.cmbdiscip_SelectedIndexChanged);
            // 
            // dg
            // 
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(6, 22);
            this.dg.Name = "dg";
            this.dg.RowTemplate.Height = 23;
            this.dg.Size = new System.Drawing.Size(493, 298);
            this.dg.TabIndex = 10;
            this.dg.SelectionChanged += new System.EventHandler(this.dg_SelectionChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbldesc);
            this.groupBox3.Controls.Add(this.lblcount);
            this.groupBox3.Controls.Add(this.dg);
            this.groupBox3.Location = new System.Drawing.Point(372, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(505, 451);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "项目定额明细";
            // 
            // lbldesc
            // 
            this.lbldesc.AutoSize = true;
            this.lbldesc.ForeColor = System.Drawing.Color.Red;
            this.lbldesc.Location = new System.Drawing.Point(6, 368);
            this.lbldesc.Name = "lbldesc";
            this.lbldesc.Size = new System.Drawing.Size(43, 13);
            this.lbldesc.TabIndex = 12;
            this.lbldesc.Text = "备注：";
            // 
            // lblcount
            // 
            this.lblcount.AutoSize = true;
            this.lblcount.Location = new System.Drawing.Point(6, 338);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(79, 13);
            this.lblcount.TabIndex = 11;
            this.lblcount.Text = "文档记录数：";
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(622, 574);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 25);
            this.btnclose.TabIndex = 12;
            this.btnclose.Text = "退出";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // lblpfullname
            // 
            this.lblpfullname.AutoSize = true;
            this.lblpfullname.Location = new System.Drawing.Point(461, 557);
            this.lblpfullname.Name = "lblpfullname";
            this.lblpfullname.Size = new System.Drawing.Size(0, 13);
            this.lblpfullname.TabIndex = 7;
            this.lblpfullname.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(370, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "专业名称：";
            // 
            // NewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 615);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmbdiscip);
            this.Controls.Add(this.lblpfullname);
            this.Controls.Add(this.txtRev);
            this.Controls.Add(this.txtDrawNo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.lblproject);
            this.Controls.Add(this.gbproject);
            this.Controls.Add(this.cmbproject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电子图框";
            this.Load += new System.EventHandler(this.NewForm_Load);
            this.gbproject.ResumeLayout(false);
            this.gbproject.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbproject;
        private System.Windows.Forms.GroupBox gbproject;
        private System.Windows.Forms.Label lblproject;
        private System.Windows.Forms.Label lblclass;
        private System.Windows.Forms.Label lblowner;
        private System.Windows.Forms.Label lblpname;
        private System.Windows.Forms.Label lblc;
        private System.Windows.Forms.Label lblo;
        private System.Windows.Forms.Label lblpj;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblresuser;
        private System.Windows.Forms.Label lblname2;
        private System.Windows.Forms.Label lblname1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rba0;
        private System.Windows.Forms.RadioButton rba1;
        private System.Windows.Forms.RadioButton rba2;
        private System.Windows.Forms.RadioButton rba3;
        private System.Windows.Forms.RadioButton rba4;
        private System.Windows.Forms.TextBox txtDrawNo;
        private System.Windows.Forms.TextBox txtRev;
        private System.Windows.Forms.ComboBox cmbdiscip;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbc0;
        private System.Windows.Forms.RadioButton rbc1;
        private System.Windows.Forms.RadioButton rbc2;
        private System.Windows.Forms.RadioButton rbc3;
        private System.Windows.Forms.RadioButton rbc4;
        private System.Windows.Forms.RadioButton rbb0;
        private System.Windows.Forms.RadioButton rbb1;
        private System.Windows.Forms.RadioButton rbb2;
        private System.Windows.Forms.RadioButton rbb3;
        private System.Windows.Forms.RadioButton rbb4;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Label lbldesc;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.Label lblpfullname;
        private System.Windows.Forms.Label label1;
    }
}