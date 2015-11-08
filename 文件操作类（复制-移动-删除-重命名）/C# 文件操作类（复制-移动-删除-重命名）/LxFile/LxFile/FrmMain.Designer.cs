namespace LxFile
{
    partial class FrmMain
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
            this.btnDelFile = new System.Windows.Forms.Button();
            this.btnDelFiles = new System.Windows.Forms.Button();
            this.btnCopyFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnCopyFiles = new System.Windows.Forms.Button();
            this.btnMoveFile = new System.Windows.Forms.Button();
            this.btnMoveFiles = new System.Windows.Forms.Button();
            this.btnReNameFile = new System.Windows.Forms.Button();
            this.txtDelFile = new System.Windows.Forms.TextBox();
            this.btnBrowser1 = new System.Windows.Forms.Button();
            this.btnBrowser2 = new System.Windows.Forms.Button();
            this.txtDelFiles = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMoveFiles = new System.Windows.Forms.TextBox();
            this.btnBrowser5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMoveDesPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowser4 = new System.Windows.Forms.Button();
            this.txtMoveFile = new System.Windows.Forms.TextBox();
            this.btnBrowser3 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCopyFiles = new System.Windows.Forms.TextBox();
            this.btnBrowser7 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCopyDesPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBrowser8 = new System.Windows.Forms.Button();
            this.txtCopyFile = new System.Windows.Forms.TextBox();
            this.btnBrowser6 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnReNameFile2 = new System.Windows.Forms.Button();
            this.txtNewName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowser9 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelFile
            // 
            this.btnDelFile.Location = new System.Drawing.Point(522, 21);
            this.btnDelFile.Name = "btnDelFile";
            this.btnDelFile.Size = new System.Drawing.Size(85, 23);
            this.btnDelFile.TabIndex = 1;
            this.btnDelFile.Text = "删除文件";
            this.btnDelFile.UseVisualStyleBackColor = true;
            this.btnDelFile.Click += new System.EventHandler(this.btnDelFile_Click);
            // 
            // btnDelFiles
            // 
            this.btnDelFiles.Location = new System.Drawing.Point(522, 51);
            this.btnDelFiles.Name = "btnDelFiles";
            this.btnDelFiles.Size = new System.Drawing.Size(85, 23);
            this.btnDelFiles.TabIndex = 2;
            this.btnDelFiles.Text = "删除多文件";
            this.btnDelFiles.UseVisualStyleBackColor = true;
            this.btnDelFiles.Click += new System.EventHandler(this.btnDelFiles_Click);
            // 
            // btnCopyFile
            // 
            this.btnCopyFile.Location = new System.Drawing.Point(433, 106);
            this.btnCopyFile.Name = "btnCopyFile";
            this.btnCopyFile.Size = new System.Drawing.Size(85, 23);
            this.btnCopyFile.TabIndex = 3;
            this.btnCopyFile.Text = "复制文件";
            this.btnCopyFile.UseVisualStyleBackColor = true;
            this.btnCopyFile.Click += new System.EventHandler(this.btnCopyFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblInfo);
            this.groupBox1.Location = new System.Drawing.Point(8, 461);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(607, 41);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作显示";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(10, 23);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 12);
            this.lblInfo.TabIndex = 0;
            // 
            // btnCopyFiles
            // 
            this.btnCopyFiles.Location = new System.Drawing.Point(524, 106);
            this.btnCopyFiles.Name = "btnCopyFiles";
            this.btnCopyFiles.Size = new System.Drawing.Size(85, 23);
            this.btnCopyFiles.TabIndex = 5;
            this.btnCopyFiles.Text = "复制多文件";
            this.btnCopyFiles.UseVisualStyleBackColor = true;
            this.btnCopyFiles.Click += new System.EventHandler(this.btnCopyFiles_Click);
            // 
            // btnMoveFile
            // 
            this.btnMoveFile.Location = new System.Drawing.Point(431, 109);
            this.btnMoveFile.Name = "btnMoveFile";
            this.btnMoveFile.Size = new System.Drawing.Size(85, 23);
            this.btnMoveFile.TabIndex = 6;
            this.btnMoveFile.Text = "移动文件";
            this.btnMoveFile.UseVisualStyleBackColor = true;
            this.btnMoveFile.Click += new System.EventHandler(this.btnMoveFile_Click);
            // 
            // btnMoveFiles
            // 
            this.btnMoveFiles.Location = new System.Drawing.Point(524, 109);
            this.btnMoveFiles.Name = "btnMoveFiles";
            this.btnMoveFiles.Size = new System.Drawing.Size(85, 23);
            this.btnMoveFiles.TabIndex = 7;
            this.btnMoveFiles.Text = "移动多文件";
            this.btnMoveFiles.UseVisualStyleBackColor = true;
            this.btnMoveFiles.Click += new System.EventHandler(this.btnMoveFiles_Click);
            // 
            // btnReNameFile
            // 
            this.btnReNameFile.Location = new System.Drawing.Point(458, 42);
            this.btnReNameFile.Name = "btnReNameFile";
            this.btnReNameFile.Size = new System.Drawing.Size(74, 23);
            this.btnReNameFile.TabIndex = 8;
            this.btnReNameFile.Text = "重命名 1";
            this.btnReNameFile.UseVisualStyleBackColor = true;
            this.btnReNameFile.Click += new System.EventHandler(this.btnReNameFile_Click);
            // 
            // txtDelFile
            // 
            this.txtDelFile.BackColor = System.Drawing.SystemColors.Info;
            this.txtDelFile.Location = new System.Drawing.Point(8, 22);
            this.txtDelFile.Name = "txtDelFile";
            this.txtDelFile.ReadOnly = true;
            this.txtDelFile.Size = new System.Drawing.Size(444, 21);
            this.txtDelFile.TabIndex = 9;
            // 
            // btnBrowser1
            // 
            this.btnBrowser1.Location = new System.Drawing.Point(458, 21);
            this.btnBrowser1.Name = "btnBrowser1";
            this.btnBrowser1.Size = new System.Drawing.Size(58, 23);
            this.btnBrowser1.TabIndex = 10;
            this.btnBrowser1.Text = "浏览...";
            this.btnBrowser1.UseVisualStyleBackColor = true;
            this.btnBrowser1.Click += new System.EventHandler(this.btnBrowser1_Click);
            // 
            // btnBrowser2
            // 
            this.btnBrowser2.Location = new System.Drawing.Point(458, 51);
            this.btnBrowser2.Name = "btnBrowser2";
            this.btnBrowser2.Size = new System.Drawing.Size(58, 23);
            this.btnBrowser2.TabIndex = 12;
            this.btnBrowser2.Text = "浏览...";
            this.btnBrowser2.UseVisualStyleBackColor = true;
            this.btnBrowser2.Click += new System.EventHandler(this.btnBrowser2_Click);
            // 
            // txtDelFiles
            // 
            this.txtDelFiles.BackColor = System.Drawing.SystemColors.Info;
            this.txtDelFiles.Location = new System.Drawing.Point(8, 52);
            this.txtDelFiles.Name = "txtDelFiles";
            this.txtDelFiles.ReadOnly = true;
            this.txtDelFiles.Size = new System.Drawing.Size(444, 21);
            this.txtDelFiles.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDelFiles);
            this.groupBox2.Controls.Add(this.btnBrowser2);
            this.groupBox2.Controls.Add(this.txtDelFile);
            this.groupBox2.Controls.Add(this.btnBrowser1);
            this.groupBox2.Controls.Add(this.btnDelFiles);
            this.groupBox2.Controls.Add(this.btnDelFile);
            this.groupBox2.Location = new System.Drawing.Point(8, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(615, 86);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "删除文件";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtMoveFiles);
            this.groupBox3.Controls.Add(this.btnBrowser5);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtMoveDesPath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnMoveFiles);
            this.groupBox3.Controls.Add(this.btnBrowser4);
            this.groupBox3.Controls.Add(this.btnMoveFile);
            this.groupBox3.Controls.Add(this.txtMoveFile);
            this.groupBox3.Controls.Add(this.btnBrowser3);
            this.groupBox3.Location = new System.Drawing.Point(8, 96);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(615, 139);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "移动文件";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "多文件：";
            // 
            // txtMoveFiles
            // 
            this.txtMoveFiles.BackColor = System.Drawing.Color.Bisque;
            this.txtMoveFiles.Location = new System.Drawing.Point(62, 51);
            this.txtMoveFiles.Name = "txtMoveFiles";
            this.txtMoveFiles.ReadOnly = true;
            this.txtMoveFiles.Size = new System.Drawing.Size(390, 21);
            this.txtMoveFiles.TabIndex = 16;
            // 
            // btnBrowser5
            // 
            this.btnBrowser5.Location = new System.Drawing.Point(458, 80);
            this.btnBrowser5.Name = "btnBrowser5";
            this.btnBrowser5.Size = new System.Drawing.Size(58, 23);
            this.btnBrowser5.TabIndex = 17;
            this.btnBrowser5.Text = "浏览...";
            this.btnBrowser5.UseVisualStyleBackColor = true;
            this.btnBrowser5.Click += new System.EventHandler(this.btnBrowser5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "目的地：";
            // 
            // txtMoveDesPath
            // 
            this.txtMoveDesPath.BackColor = System.Drawing.Color.Bisque;
            this.txtMoveDesPath.Location = new System.Drawing.Point(62, 82);
            this.txtMoveDesPath.Name = "txtMoveDesPath";
            this.txtMoveDesPath.ReadOnly = true;
            this.txtMoveDesPath.Size = new System.Drawing.Size(390, 21);
            this.txtMoveDesPath.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "源文件：";
            // 
            // btnBrowser4
            // 
            this.btnBrowser4.Location = new System.Drawing.Point(458, 49);
            this.btnBrowser4.Name = "btnBrowser4";
            this.btnBrowser4.Size = new System.Drawing.Size(58, 23);
            this.btnBrowser4.TabIndex = 12;
            this.btnBrowser4.Text = "浏览...";
            this.btnBrowser4.UseVisualStyleBackColor = true;
            this.btnBrowser4.Click += new System.EventHandler(this.btnBrowser4_Click);
            // 
            // txtMoveFile
            // 
            this.txtMoveFile.BackColor = System.Drawing.Color.Bisque;
            this.txtMoveFile.Location = new System.Drawing.Point(62, 22);
            this.txtMoveFile.Name = "txtMoveFile";
            this.txtMoveFile.ReadOnly = true;
            this.txtMoveFile.Size = new System.Drawing.Size(390, 21);
            this.txtMoveFile.TabIndex = 9;
            // 
            // btnBrowser3
            // 
            this.btnBrowser3.Location = new System.Drawing.Point(458, 21);
            this.btnBrowser3.Name = "btnBrowser3";
            this.btnBrowser3.Size = new System.Drawing.Size(58, 23);
            this.btnBrowser3.TabIndex = 10;
            this.btnBrowser3.Text = "浏览...";
            this.btnBrowser3.UseVisualStyleBackColor = true;
            this.btnBrowser3.Click += new System.EventHandler(this.btnBrowser3_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtCopyFiles);
            this.groupBox4.Controls.Add(this.btnBrowser7);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtCopyDesPath);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.btnBrowser8);
            this.groupBox4.Controls.Add(this.txtCopyFile);
            this.groupBox4.Controls.Add(this.btnBrowser6);
            this.groupBox4.Controls.Add(this.btnCopyFiles);
            this.groupBox4.Controls.Add(this.btnCopyFile);
            this.groupBox4.Location = new System.Drawing.Point(8, 241);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(615, 139);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "复制文件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "多文件：";
            // 
            // txtCopyFiles
            // 
            this.txtCopyFiles.BackColor = System.Drawing.Color.Pink;
            this.txtCopyFiles.Location = new System.Drawing.Point(62, 48);
            this.txtCopyFiles.Name = "txtCopyFiles";
            this.txtCopyFiles.ReadOnly = true;
            this.txtCopyFiles.Size = new System.Drawing.Size(390, 21);
            this.txtCopyFiles.TabIndex = 26;
            // 
            // btnBrowser7
            // 
            this.btnBrowser7.Location = new System.Drawing.Point(458, 47);
            this.btnBrowser7.Name = "btnBrowser7";
            this.btnBrowser7.Size = new System.Drawing.Size(58, 23);
            this.btnBrowser7.TabIndex = 27;
            this.btnBrowser7.Text = "浏览...";
            this.btnBrowser7.UseVisualStyleBackColor = true;
            this.btnBrowser7.Click += new System.EventHandler(this.btnBrowser7_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "目的地：";
            // 
            // txtCopyDesPath
            // 
            this.txtCopyDesPath.BackColor = System.Drawing.Color.Pink;
            this.txtCopyDesPath.Location = new System.Drawing.Point(62, 79);
            this.txtCopyDesPath.Name = "txtCopyDesPath";
            this.txtCopyDesPath.ReadOnly = true;
            this.txtCopyDesPath.Size = new System.Drawing.Size(390, 21);
            this.txtCopyDesPath.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "源文件：";
            // 
            // btnBrowser8
            // 
            this.btnBrowser8.Location = new System.Drawing.Point(458, 77);
            this.btnBrowser8.Name = "btnBrowser8";
            this.btnBrowser8.Size = new System.Drawing.Size(58, 23);
            this.btnBrowser8.TabIndex = 22;
            this.btnBrowser8.Text = "浏览...";
            this.btnBrowser8.UseVisualStyleBackColor = true;
            this.btnBrowser8.Click += new System.EventHandler(this.btnBrowser8_Click);
            // 
            // txtCopyFile
            // 
            this.txtCopyFile.BackColor = System.Drawing.Color.Pink;
            this.txtCopyFile.Location = new System.Drawing.Point(62, 19);
            this.txtCopyFile.Name = "txtCopyFile";
            this.txtCopyFile.ReadOnly = true;
            this.txtCopyFile.Size = new System.Drawing.Size(390, 21);
            this.txtCopyFile.TabIndex = 20;
            // 
            // btnBrowser6
            // 
            this.btnBrowser6.Location = new System.Drawing.Point(458, 18);
            this.btnBrowser6.Name = "btnBrowser6";
            this.btnBrowser6.Size = new System.Drawing.Size(58, 23);
            this.btnBrowser6.TabIndex = 21;
            this.btnBrowser6.Text = "浏览...";
            this.btnBrowser6.UseVisualStyleBackColor = true;
            this.btnBrowser6.Click += new System.EventHandler(this.btnBrowser6_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnReNameFile2);
            this.groupBox5.Controls.Add(this.txtNewName);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtFilePath);
            this.groupBox5.Controls.Add(this.btnBrowser9);
            this.groupBox5.Controls.Add(this.btnReNameFile);
            this.groupBox5.Location = new System.Drawing.Point(8, 386);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(615, 71);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "文件重命名";
            // 
            // btnReNameFile2
            // 
            this.btnReNameFile2.Location = new System.Drawing.Point(535, 42);
            this.btnReNameFile2.Name = "btnReNameFile2";
            this.btnReNameFile2.Size = new System.Drawing.Size(74, 23);
            this.btnReNameFile2.TabIndex = 34;
            this.btnReNameFile2.Text = "重命名2";
            this.btnReNameFile2.UseVisualStyleBackColor = true;
            this.btnReNameFile2.Click += new System.EventHandler(this.btnReNameFile2_Click);
            // 
            // txtNewName
            // 
            this.txtNewName.Location = new System.Drawing.Point(64, 44);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(388, 21);
            this.txtNewName.TabIndex = 33;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "新名称：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 31;
            this.label7.Text = "源文件：";
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.Color.White;
            this.txtFilePath.Location = new System.Drawing.Point(62, 18);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(390, 21);
            this.txtFilePath.TabIndex = 29;
            // 
            // btnBrowser9
            // 
            this.btnBrowser9.Location = new System.Drawing.Point(458, 17);
            this.btnBrowser9.Name = "btnBrowser9";
            this.btnBrowser9.Size = new System.Drawing.Size(58, 23);
            this.btnBrowser9.TabIndex = 30;
            this.btnBrowser9.Text = "浏览...";
            this.btnBrowser9.UseVisualStyleBackColor = true;
            this.btnBrowser9.Click += new System.EventHandler(this.btnBrowser9_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 514);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMain";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDelFile;
        private System.Windows.Forms.Button btnDelFiles;
        private System.Windows.Forms.Button btnCopyFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnCopyFiles;
        private System.Windows.Forms.Button btnMoveFile;
        private System.Windows.Forms.Button btnMoveFiles;
        private System.Windows.Forms.Button btnReNameFile;
        private System.Windows.Forms.TextBox txtDelFile;
        private System.Windows.Forms.Button btnBrowser1;
        private System.Windows.Forms.Button btnBrowser2;
        private System.Windows.Forms.TextBox txtDelFiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMoveDesPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowser4;
        private System.Windows.Forms.TextBox txtMoveFile;
        private System.Windows.Forms.Button btnBrowser3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMoveFiles;
        private System.Windows.Forms.Button btnBrowser5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCopyFiles;
        private System.Windows.Forms.Button btnBrowser7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCopyDesPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBrowser8;
        private System.Windows.Forms.TextBox txtCopyFile;
        private System.Windows.Forms.Button btnBrowser6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtNewName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowser9;
        private System.Windows.Forms.Button btnReNameFile2;
    }
}