namespace DetailInfo
{
    partial class ModifyDrawingControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.reason_cb = new System.Windows.Forms.ComboBox();
            this.typecob = new System.Windows.Forms.ComboBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.responsercomb = new System.Windows.Forms.ComboBox();
            this.materialcost_tb = new System.Windows.Forms.TextBox();
            this.techcost_tb = new System.Windows.Forms.TextBox();
            this.status_cb = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comcode_tb = new System.Windows.Forms.TextBox();
            this.labelColor7 = new DetailInfo.LabelColor();
            this.labelColor6 = new DetailInfo.LabelColor();
            this.labelColor5 = new DetailInfo.LabelColor();
            this.labelColor4 = new DetailInfo.LabelColor();
            this.labelColor3 = new DetailInfo.LabelColor();
            this.labelColor2 = new DetailInfo.LabelColor();
            this.labelColor1 = new DetailInfo.LabelColor();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(-246, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "生产损耗物量：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-246, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "原因：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comcode_tb);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelColor7);
            this.groupBox1.Controls.Add(this.labelColor6);
            this.groupBox1.Controls.Add(this.labelColor5);
            this.groupBox1.Controls.Add(this.labelColor4);
            this.groupBox1.Controls.Add(this.labelColor3);
            this.groupBox1.Controls.Add(this.labelColor2);
            this.groupBox1.Controls.Add(this.labelColor1);
            this.groupBox1.Controls.Add(this.reason_cb);
            this.groupBox1.Controls.Add(this.typecob);
            this.groupBox1.Controls.Add(this.linkLabel2);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.responsercomb);
            this.groupBox1.Controls.Add(this.materialcost_tb);
            this.groupBox1.Controls.Add(this.techcost_tb);
            this.groupBox1.Controls.Add(this.status_cb);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(998, 82);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // reason_cb
            // 
            this.reason_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reason_cb.FormattingEnabled = true;
            this.reason_cb.Location = new System.Drawing.Point(95, 20);
            this.reason_cb.Name = "reason_cb";
            this.reason_cb.Size = new System.Drawing.Size(198, 21);
            this.reason_cb.TabIndex = 40;
            this.reason_cb.SelectedIndexChanged += new System.EventHandler(this.reason_cb_SelectedIndexChanged);
            // 
            // typecob
            // 
            this.typecob.FormattingEnabled = true;
            this.typecob.Location = new System.Drawing.Point(95, 49);
            this.typecob.Name = "typecob";
            this.typecob.Size = new System.Drawing.Size(198, 21);
            this.typecob.TabIndex = 39;
            this.typecob.SelectedIndexChanged += new System.EventHandler(this.typecob_SelectedIndexChanged);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(924, 57);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(31, 13);
            this.linkLabel2.TabIndex = 38;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "删除";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(887, 57);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(31, 13);
            this.linkLabel1.TabIndex = 37;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "添加";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // responsercomb
            // 
            this.responsercomb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.responsercomb.FormattingEnabled = true;
            this.responsercomb.Location = new System.Drawing.Point(625, 18);
            this.responsercomb.Name = "responsercomb";
            this.responsercomb.Size = new System.Drawing.Size(121, 21);
            this.responsercomb.TabIndex = 36;
            // 
            // materialcost_tb
            // 
            this.materialcost_tb.Location = new System.Drawing.Point(394, 50);
            this.materialcost_tb.Name = "materialcost_tb";
            this.materialcost_tb.Size = new System.Drawing.Size(121, 20);
            this.materialcost_tb.TabIndex = 35;
            this.materialcost_tb.TextChanged += new System.EventHandler(this.materialcost_tb_TextChanged);
            // 
            // techcost_tb
            // 
            this.techcost_tb.Location = new System.Drawing.Point(625, 50);
            this.techcost_tb.Name = "techcost_tb";
            this.techcost_tb.Size = new System.Drawing.Size(121, 20);
            this.techcost_tb.TabIndex = 34;
            this.techcost_tb.TextChanged += new System.EventHandler(this.techcost_tb_TextChanged);
            // 
            // status_cb
            // 
            this.status_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.status_cb.FormattingEnabled = true;
            this.status_cb.Items.AddRange(new object[] {
            "产前",
            "产后"});
            this.status_cb.Location = new System.Drawing.Point(831, 20);
            this.status_cb.Name = "status_cb";
            this.status_cb.Size = new System.Drawing.Size(121, 21);
            this.status_cb.TabIndex = 33;
            this.status_cb.SelectedIndexChanged += new System.EventHandler(this.status_cb_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(533, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "技术损耗工时：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "生产损耗物量：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "损耗材料类型：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(754, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "产前/产后：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(534, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "责任主管：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "原因：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(301, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "编号：";
            // 
            // comcode_tb
            // 
            this.comcode_tb.Location = new System.Drawing.Point(394, 20);
            this.comcode_tb.Name = "comcode_tb";
            this.comcode_tb.Size = new System.Drawing.Size(121, 20);
            this.comcode_tb.TabIndex = 49;
            // 
            // labelColor7
            // 
            this.labelColor7.AutoSize = true;
            this.labelColor7.ForeColor = System.Drawing.Color.Red;
            this.labelColor7.Location = new System.Drawing.Point(519, 54);
            this.labelColor7.Name = "labelColor7";
            this.labelColor7.Size = new System.Drawing.Size(0, 13);
            this.labelColor7.TabIndex = 47;
            // 
            // labelColor6
            // 
            this.labelColor6.AutoSize = true;
            this.labelColor6.ForeColor = System.Drawing.Color.Red;
            this.labelColor6.Location = new System.Drawing.Point(613, 57);
            this.labelColor6.Name = "labelColor6";
            this.labelColor6.Size = new System.Drawing.Size(11, 13);
            this.labelColor6.TabIndex = 46;
            this.labelColor6.Text = "*";
            // 
            // labelColor5
            // 
            this.labelColor5.AutoSize = true;
            this.labelColor5.ForeColor = System.Drawing.Color.Red;
            this.labelColor5.Location = new System.Drawing.Point(817, 26);
            this.labelColor5.Name = "labelColor5";
            this.labelColor5.Size = new System.Drawing.Size(11, 13);
            this.labelColor5.TabIndex = 45;
            this.labelColor5.Text = "*";
            // 
            // labelColor4
            // 
            this.labelColor4.AutoSize = true;
            this.labelColor4.ForeColor = System.Drawing.Color.Red;
            this.labelColor4.Location = new System.Drawing.Point(82, 57);
            this.labelColor4.Name = "labelColor4";
            this.labelColor4.Size = new System.Drawing.Size(11, 13);
            this.labelColor4.TabIndex = 44;
            this.labelColor4.Text = "*";
            // 
            // labelColor3
            // 
            this.labelColor3.AutoSize = true;
            this.labelColor3.ForeColor = System.Drawing.Color.Red;
            this.labelColor3.Location = new System.Drawing.Point(381, 57);
            this.labelColor3.Name = "labelColor3";
            this.labelColor3.Size = new System.Drawing.Size(11, 13);
            this.labelColor3.TabIndex = 43;
            this.labelColor3.Text = "*";
            // 
            // labelColor2
            // 
            this.labelColor2.AutoSize = true;
            this.labelColor2.ForeColor = System.Drawing.Color.Red;
            this.labelColor2.Location = new System.Drawing.Point(590, 26);
            this.labelColor2.Name = "labelColor2";
            this.labelColor2.Size = new System.Drawing.Size(11, 13);
            this.labelColor2.TabIndex = 42;
            this.labelColor2.Text = "*";
            // 
            // labelColor1
            // 
            this.labelColor1.AutoSize = true;
            this.labelColor1.ForeColor = System.Drawing.Color.Red;
            this.labelColor1.Location = new System.Drawing.Point(37, 26);
            this.labelColor1.Name = "labelColor1";
            this.labelColor1.Size = new System.Drawing.Size(11, 13);
            this.labelColor1.TabIndex = 41;
            this.labelColor1.Text = "*";
            // 
            // ModifyDrawingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Name = "ModifyDrawingControl";
            this.Size = new System.Drawing.Size(998, 82);
            this.Tag = "";
            this.Load += new System.EventHandler(this.ModifyDrawingControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox responsercomb;
        private LabelColor labelColor6;
        private LabelColor labelColor5;
        private LabelColor labelColor4;
        private LabelColor labelColor3;
        private LabelColor labelColor2;
        private LabelColor labelColor1;
        private LabelColor labelColor7;
        public System.Windows.Forms.TextBox materialcost_tb;
        public System.Windows.Forms.TextBox techcost_tb;
        public System.Windows.Forms.ComboBox status_cb;
        public System.Windows.Forms.ComboBox typecob;
        public System.Windows.Forms.ComboBox reason_cb;
        private System.Windows.Forms.TextBox comcode_tb;
        private System.Windows.Forms.Label label3;
    }
}
