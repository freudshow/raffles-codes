namespace DetailInfo
{
    partial class PackageDrawing
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
            this.mergebtn = new System.Windows.Forms.Button();
            this.groupbox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.completebtn = new System.Windows.Forms.Button();
            this.previewbtn = new System.Windows.Forms.Button();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.groupbox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mergebtn
            // 
            this.mergebtn.Location = new System.Drawing.Point(12, 105);
            this.mergebtn.Name = "mergebtn";
            this.mergebtn.Size = new System.Drawing.Size(75, 25);
            this.mergebtn.TabIndex = 5;
            this.mergebtn.Text = "开始打包";
            this.mergebtn.UseVisualStyleBackColor = true;
            this.mergebtn.Click += new System.EventHandler(this.mergebtn_Click);
            // 
            // groupbox1
            // 
            this.groupbox1.Controls.Add(this.label3);
            this.groupbox1.Controls.Add(this.textBox2);
            this.groupbox1.Controls.Add(this.label2);
            this.groupbox1.Controls.Add(this.textBox1);
            this.groupbox1.Controls.Add(this.label1);
            this.groupbox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupbox1.Location = new System.Drawing.Point(0, 0);
            this.groupbox1.Name = "groupbox1";
            this.groupbox1.Size = new System.Drawing.Size(292, 99);
            this.groupbox1.TabIndex = 4;
            this.groupbox1.TabStop = false;
            this.groupbox1.Text = "打包信息";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(274, 23);
            this.label3.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(64, 35);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(137, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "图纸号:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(137, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目号:";
            // 
            // completebtn
            // 
            this.completebtn.Location = new System.Drawing.Point(93, 105);
            this.completebtn.Name = "completebtn";
            this.completebtn.Size = new System.Drawing.Size(75, 25);
            this.completebtn.TabIndex = 6;
            this.completebtn.UseVisualStyleBackColor = true;
            this.completebtn.Click += new System.EventHandler(this.completebtn_Click);
            // 
            // previewbtn
            // 
            this.previewbtn.Location = new System.Drawing.Point(174, 105);
            this.previewbtn.Name = "previewbtn";
            this.previewbtn.Size = new System.Drawing.Size(75, 25);
            this.previewbtn.TabIndex = 7;
            this.previewbtn.Text = "预览";
            this.previewbtn.UseVisualStyleBackColor = true;
            this.previewbtn.Click += new System.EventHandler(this.previewbtn_Click);
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // PackageDrawing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(292, 143);
            this.ControlBox = false;
            this.Controls.Add(this.previewbtn);
            this.Controls.Add(this.completebtn);
            this.Controls.Add(this.mergebtn);
            this.Controls.Add(this.groupbox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PackageDrawing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打包图纸";
            this.Load += new System.EventHandler(this.PackageDrawing_Load);
            this.groupbox1.ResumeLayout(false);
            this.groupbox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button mergebtn;
        private System.Windows.Forms.GroupBox groupbox1;
        private System.Windows.Forms.Button completebtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button previewbtn;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
    }
}