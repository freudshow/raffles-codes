namespace CIMCPPSPL
{
    partial class LoginIn
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.PW = new System.Windows.Forms.Label();
            this.UN = new System.Windows.Forms.Label();
            this.TPW = new System.Windows.Forms.TextBox();
            this.TUN = new System.Windows.Forms.TextBox();
            this.QX = new System.Windows.Forms.Button();
            this.LG = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PW
            // 
            this.PW.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PW.Location = new System.Drawing.Point(26, 133);
            this.PW.Name = "PW";
            this.PW.Size = new System.Drawing.Size(52, 20);
            this.PW.Text = "密   码";
            // 
            // UN
            // 
            this.UN.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UN.Location = new System.Drawing.Point(26, 101);
            this.UN.Name = "UN";
            this.UN.Size = new System.Drawing.Size(52, 20);
            this.UN.Text = "用户名";
            // 
            // TPW
            // 
            this.TPW.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TPW.Location = new System.Drawing.Point(81, 130);
            this.TPW.Name = "TPW";
            this.TPW.PasswordChar = '*';
            this.TPW.Size = new System.Drawing.Size(137, 21);
            this.TPW.TabIndex = 9;
            // 
            // TUN
            // 
            this.TUN.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TUN.Location = new System.Drawing.Point(81, 101);
            this.TUN.Name = "TUN";
            this.TUN.Size = new System.Drawing.Size(137, 21);
            this.TUN.TabIndex = 8;
            // 
            // QX
            // 
            this.QX.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.QX.BackColor = System.Drawing.Color.CornflowerBlue;
            this.QX.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.QX.Location = new System.Drawing.Point(136, 167);
            this.QX.Name = "QX";
            this.QX.Size = new System.Drawing.Size(64, 23);
            this.QX.TabIndex = 6;
            this.QX.Text = "取  消";
            this.QX.Click += new System.EventHandler(this.QX_Click_1);
            // 
            // LG
            // 
            this.LG.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LG.BackColor = System.Drawing.Color.CornflowerBlue;
            this.LG.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.LG.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LG.Location = new System.Drawing.Point(55, 166);
            this.LG.Name = "LG";
            this.LG.Size = new System.Drawing.Size(64, 23);
            this.LG.TabIndex = 4;
            this.LG.Text = "登  录";
            this.LG.Click += new System.EventHandler(this.LG_Click_1);
            // 
            // LoginIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.PW);
            this.Controls.Add(this.UN);
            this.Controls.Add(this.TPW);
            this.Controls.Add(this.TUN);
            this.Controls.Add(this.QX);
            this.Controls.Add(this.LG);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "LoginIn";
            this.Text = "登录窗口";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LoginIn_Load);
            this.Closed += new System.EventHandler(this.LoginIn_Closed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginIn_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PW;
        private System.Windows.Forms.Label UN;
        private System.Windows.Forms.TextBox TPW;
        private System.Windows.Forms.TextBox TUN;
        private System.Windows.Forms.Button QX;
        private System.Windows.Forms.Button LG;
    }
}