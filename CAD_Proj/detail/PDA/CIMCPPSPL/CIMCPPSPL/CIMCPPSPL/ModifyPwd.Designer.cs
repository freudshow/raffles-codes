namespace CIMCPPSPL
{
    partial class ModifyPwd
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
            this.btn_confirm = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.TCP = new System.Windows.Forms.TextBox();
            this.TPW = new System.Windows.Forms.TextBox();
            this.TCNP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_confirm
            // 
            this.btn_confirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_confirm.Location = new System.Drawing.Point(37, 171);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(72, 20);
            this.btn_confirm.TabIndex = 0;
            this.btn_confirm.Text = "确  认";
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_cancel.Location = new System.Drawing.Point(141, 171);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(72, 20);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "取  消";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // TCP
            // 
            this.TCP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TCP.Location = new System.Drawing.Point(113, 74);
            this.TCP.Name = "TCP";
            this.TCP.PasswordChar = '*';
            this.TCP.Size = new System.Drawing.Size(100, 21);
            this.TCP.TabIndex = 2;
            // 
            // TPW
            // 
            this.TPW.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TPW.Location = new System.Drawing.Point(113, 102);
            this.TPW.Name = "TPW";
            this.TPW.PasswordChar = '*';
            this.TPW.Size = new System.Drawing.Size(100, 21);
            this.TPW.TabIndex = 3;
            // 
            // TCNP
            // 
            this.TCNP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TCNP.Location = new System.Drawing.Point(113, 130);
            this.TCNP.Name = "TCNP";
            this.TCNP.PasswordChar = '*';
            this.TCNP.Size = new System.Drawing.Size(100, 21);
            this.TCNP.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Location = new System.Drawing.Point(36, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.Text = "原密码:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Location = new System.Drawing.Point(36, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "新密码:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.Location = new System.Drawing.Point(36, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.Text = "确认新密码:";
            // 
            // ModifyPwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TCNP);
            this.Controls.Add(this.TPW);
            this.Controls.Add(this.TCP);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_confirm);
            this.Menu = this.mainMenu1;
            this.Name = "ModifyPwd";
            this.Text = "修改密码";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox TCP;
        private System.Windows.Forms.TextBox TPW;
        private System.Windows.Forms.TextBox TCNP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}