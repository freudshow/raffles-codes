namespace DetailInfo
{
    partial class Add_Remark
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
            this.lb_remark = new System.Windows.Forms.Label();
            this.tb_remark = new System.Windows.Forms.TextBox();
            this.btn_remark = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.SuspendLayout();
            // 
            // lb_remark
            // 
            this.lb_remark.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb_remark.Location = new System.Drawing.Point(0, 0);
            this.lb_remark.Name = "lb_remark";
            this.lb_remark.Size = new System.Drawing.Size(292, 23);
            this.lb_remark.TabIndex = 0;
            this.lb_remark.Text = "请输入相关备注：";
            // 
            // tb_remark
            // 
            this.tb_remark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_remark.Location = new System.Drawing.Point(5, 26);
            this.tb_remark.Multiline = true;
            this.tb_remark.Name = "tb_remark";
            this.tb_remark.Size = new System.Drawing.Size(283, 55);
            this.tb_remark.TabIndex = 1;
            // 
            // btn_remark
            // 
            this.btn_remark.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_remark.Location = new System.Drawing.Point(146, 87);
            this.btn_remark.Name = "btn_remark";
            this.btn_remark.Size = new System.Drawing.Size(67, 23);
            this.btn_remark.TabIndex = 2;
            this.btn_remark.Text = "确定";
            this.btn_remark.UseVisualStyleBackColor = true;
            this.btn_remark.Click += new System.EventHandler(this.btn_remark_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(221, 87);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(67, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // Add_Remark
            // 
            this.AcceptButton = this.btn_remark;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(292, 113);
            this.Controls.Add(this.tb_remark);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_remark);
            this.Controls.Add(this.lb_remark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Add_Remark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加备注";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_remark;
        private System.Windows.Forms.TextBox tb_remark;
        private System.Windows.Forms.Button btn_remark;
        private System.Windows.Forms.Button btn_cancel;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
    }
}