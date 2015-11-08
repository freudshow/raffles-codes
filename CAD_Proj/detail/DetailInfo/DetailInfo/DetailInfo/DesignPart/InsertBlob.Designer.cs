namespace DetailInfo
{
    partial class InsertBlob
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelColor2 = new DetailInfo.LabelColor();
            this.labelColor1 = new DetailInfo.LabelColor();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.CALbtn = new System.Windows.Forms.Button();
            this.OKbtn = new System.Windows.Forms.Button();
            this.FilePathLb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelColor2);
            this.groupBox1.Controls.Add(this.labelColor1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.CALbtn);
            this.groupBox1.Controls.Add(this.OKbtn);
            this.groupBox1.Controls.Add(this.FilePathLb);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "封面信息";
            // 
            // labelColor2
            // 
            this.labelColor2.AutoSize = true;
            this.labelColor2.ForeColor = System.Drawing.Color.Red;
            this.labelColor2.Location = new System.Drawing.Point(47, 182);
            this.labelColor2.Name = "labelColor2";
            this.labelColor2.Size = new System.Drawing.Size(291, 13);
            this.labelColor2.TabIndex = 10;
            this.labelColor2.Text = "如果选择“修改通知单封面”则图纸号填写修改通知单号";
            // 
            // labelColor1
            // 
            this.labelColor1.AutoSize = true;
            this.labelColor1.ForeColor = System.Drawing.Color.Red;
            this.labelColor1.Location = new System.Drawing.Point(12, 166);
            this.labelColor1.Name = "labelColor1";
            this.labelColor1.Size = new System.Drawing.Size(219, 13);
            this.labelColor1.TabIndex = 9;
            this.labelColor1.Text = "提醒：如果选择“图纸封面”则填写图纸号";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(121, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(109, 17);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "修改通知单封面";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(73, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "图纸封面";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // CALbtn
            // 
            this.CALbtn.Location = new System.Drawing.Point(106, 136);
            this.CALbtn.Name = "CALbtn";
            this.CALbtn.Size = new System.Drawing.Size(75, 23);
            this.CALbtn.TabIndex = 7;
            this.CALbtn.Text = "取消";
            this.CALbtn.UseVisualStyleBackColor = true;
            this.CALbtn.Click += new System.EventHandler(this.CALbtn_Click);
            // 
            // OKbtn
            // 
            this.OKbtn.Location = new System.Drawing.Point(13, 136);
            this.OKbtn.Name = "OKbtn";
            this.OKbtn.Size = new System.Drawing.Size(75, 23);
            this.OKbtn.TabIndex = 6;
            this.OKbtn.Text = "确定";
            this.OKbtn.UseVisualStyleBackColor = true;
            this.OKbtn.Click += new System.EventHandler(this.OKbtn_Click);
            // 
            // FilePathLb
            // 
            this.FilePathLb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FilePathLb.Location = new System.Drawing.Point(71, 101);
            this.FilePathLb.Name = "FilePathLb";
            this.FilePathLb.ReadOnly = true;
            this.FilePathLb.Size = new System.Drawing.Size(246, 20);
            this.FilePathLb.TabIndex = 5;
            this.FilePathLb.Click += new System.EventHandler(this.FilePathLb_Click);

            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "目标文件";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(71, 71);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(246, 20);
            this.textBox2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(71, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(246, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "图纸号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目号";
            // 
            // InsertBlob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(347, 204);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "InsertBlob";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "更新封面";
            this.Activated += new System.EventHandler(this.InsertBlob_Activated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox FilePathLb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CALbtn;
        private System.Windows.Forms.Button OKbtn;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private LabelColor labelColor2;
        private LabelColor labelColor1;
    }
}