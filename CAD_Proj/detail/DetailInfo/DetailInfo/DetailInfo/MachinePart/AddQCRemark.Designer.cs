namespace DetailInfo
{
    partial class AddQCRemark
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
            this.btn_ToQCCancel = new System.Windows.Forms.Button();
            this.btn_ToQCRemark = new System.Windows.Forms.Button();
            this.tb_ToQCRemark = new System.Windows.Forms.TextBox();
            this.lb_remark = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_ToQCCancel
            // 
            this.btn_ToQCCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ToQCCancel.Location = new System.Drawing.Point(221, 88);
            this.btn_ToQCCancel.Name = "btn_ToQCCancel";
            this.btn_ToQCCancel.Size = new System.Drawing.Size(67, 23);
            this.btn_ToQCCancel.TabIndex = 7;
            this.btn_ToQCCancel.Text = "取消";
            this.btn_ToQCCancel.UseVisualStyleBackColor = true;
            this.btn_ToQCCancel.Click += new System.EventHandler(this.btn_ToQCCancel_Click);
            // 
            // btn_ToQCRemark
            // 
            this.btn_ToQCRemark.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ToQCRemark.Location = new System.Drawing.Point(146, 88);
            this.btn_ToQCRemark.Name = "btn_ToQCRemark";
            this.btn_ToQCRemark.Size = new System.Drawing.Size(67, 23);
            this.btn_ToQCRemark.TabIndex = 6;
            this.btn_ToQCRemark.Text = "确定";
            this.btn_ToQCRemark.UseVisualStyleBackColor = true;
            this.btn_ToQCRemark.Click += new System.EventHandler(this.btn_ToQCRemark_Click);
            // 
            // tb_ToQCRemark
            // 
            this.tb_ToQCRemark.Location = new System.Drawing.Point(5, 27);
            this.tb_ToQCRemark.Multiline = true;
            this.tb_ToQCRemark.Name = "tb_ToQCRemark";
            this.tb_ToQCRemark.Size = new System.Drawing.Size(283, 55);
            this.tb_ToQCRemark.TabIndex = 5;
            // 
            // lb_remark
            // 
            this.lb_remark.Location = new System.Drawing.Point(3, 10);
            this.lb_remark.Name = "lb_remark";
            this.lb_remark.Size = new System.Drawing.Size(177, 23);
            this.lb_remark.TabIndex = 4;
            this.lb_remark.Text = "请输入要转到QC的流程备注：";
            // 
            // AddQCRemark
            // 
            this.AcceptButton = this.btn_ToQCRemark;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ToQCCancel;
            this.ClientSize = new System.Drawing.Size(290, 120);
            this.Controls.Add(this.btn_ToQCCancel);
            this.Controls.Add(this.btn_ToQCRemark);
            this.Controls.Add(this.tb_ToQCRemark);
            this.Controls.Add(this.lb_remark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddQCRemark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加转到QC备注";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ToQCCancel;
        private System.Windows.Forms.Button btn_ToQCRemark;
        private System.Windows.Forms.TextBox tb_ToQCRemark;
        private System.Windows.Forms.Label lb_remark;
    }
}