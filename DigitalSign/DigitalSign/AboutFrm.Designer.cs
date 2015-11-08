namespace DigitalSign
{
    partial class AboutFrm
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
            this.label_about = new System.Windows.Forms.Label();
            this.label_ver = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_about
            // 
            this.label_about.AutoSize = true;
            this.label_about.Location = new System.Drawing.Point(12, 9);
            this.label_about.Name = "label_about";
            this.label_about.Size = new System.Drawing.Size(57, 13);
            this.label_about.TabIndex = 0;
            this.label_about.Text = "YRO 插件";
            // 
            // label_ver
            // 
            this.label_ver.AutoSize = true;
            this.label_ver.Location = new System.Drawing.Point(12, 32);
            this.label_ver.Name = "label_ver";
            this.label_ver.Size = new System.Drawing.Size(70, 13);
            this.label_ver.TabIndex = 1;
            this.label_ver.Text = "版本: 1.0.0.0";
            // 
            // AboutFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(117, 55);
            this.Controls.Add(this.label_ver);
            this.Controls.Add(this.label_about);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_about;
        private System.Windows.Forms.Label label_ver;
    }
}