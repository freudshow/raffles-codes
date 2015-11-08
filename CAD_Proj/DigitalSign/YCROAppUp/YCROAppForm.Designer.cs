namespace YCROAppUp
{
    partial class YCROAPPForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.autoCAD_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AUTOCAD2006ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.APP_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Help_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoCAD_ToolStripMenuItem,
            this.APP_ToolStripMenuItem,
            this.Help_ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(198, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // autoCAD_ToolStripMenuItem
            // 
            this.autoCAD_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AUTOCAD2006ToolStripMenuItem});
            this.autoCAD_ToolStripMenuItem.Name = "autoCAD_ToolStripMenuItem";
            this.autoCAD_ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.autoCAD_ToolStripMenuItem.Text = "AutoCAD";
            // 
            // AUTOCAD2006ToolStripMenuItem
            // 
            this.AUTOCAD2006ToolStripMenuItem.Name = "AUTOCAD2006ToolStripMenuItem";
            this.AUTOCAD2006ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AUTOCAD2006ToolStripMenuItem.Text = "AUTOCAD 2006";
            this.AUTOCAD2006ToolStripMenuItem.Click += new System.EventHandler(this.AUTOCAD2006ToolStripMenuItem_Click);
            // 
            // APP_ToolStripMenuItem
            // 
            this.APP_ToolStripMenuItem.Name = "APP_ToolStripMenuItem";
            this.APP_ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.APP_ToolStripMenuItem.Text = "插件管理";
            // 
            // Help_ToolStripMenuItem
            // 
            this.Help_ToolStripMenuItem.Name = "Help_ToolStripMenuItem";
            this.Help_ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.Help_ToolStripMenuItem.Text = "帮助";
            // 
            // YCROAPPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 160);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "YCROAPPForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YCRO App";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem autoCAD_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem APP_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Help_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AUTOCAD2006ToolStripMenuItem;
    }
}

