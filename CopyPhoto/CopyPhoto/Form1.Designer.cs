namespace CopyPhoto
{
    partial class Form_copy
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
            this.btn_browse = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.label_path = new System.Windows.Forms.Label();
            this.btn_CopyByIC = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_browse
            // 
            this.btn_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_browse.Location = new System.Drawing.Point(240, 12);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(47, 23);
            this.btn_browse.TabIndex = 11;
            this.btn_browse.Text = "浏览";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // textPath
            // 
            this.textPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textPath.Location = new System.Drawing.Point(53, 13);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(170, 20);
            this.textPath.TabIndex = 10;
            // 
            // label_path
            // 
            this.label_path.AutoSize = true;
            this.label_path.Location = new System.Drawing.Point(4, 16);
            this.label_path.Name = "label_path";
            this.label_path.Size = new System.Drawing.Size(43, 13);
            this.label_path.TabIndex = 9;
            this.label_path.Text = "文件夹";
            // 
            // btn_CopyByIC
            // 
            this.btn_CopyByIC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_CopyByIC.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_CopyByIC.ForeColor = System.Drawing.Color.Black;
            this.btn_CopyByIC.Location = new System.Drawing.Point(7, 44);
            this.btn_CopyByIC.Name = "btn_CopyByIC";
            this.btn_CopyByIC.Size = new System.Drawing.Size(116, 23);
            this.btn_CopyByIC.TabIndex = 12;
            this.btn_CopyByIC.Text = "复制照片(按工号)";
            this.btn_CopyByIC.UseVisualStyleBackColor = true;
            this.btn_CopyByIC.Click += new System.EventHandler(this.btn_CopyByIC_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(212, 44);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 13;
            this.btn_exit.Text = "退出";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // Form_copy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 70);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_CopyByIC);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.label_path);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_copy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "复制照片";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Label label_path;
        private System.Windows.Forms.Button btn_CopyByIC;
        private System.Windows.Forms.Button btn_exit;
    }
}

