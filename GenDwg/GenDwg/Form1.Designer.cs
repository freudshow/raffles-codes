namespace GenDwg
{
    partial class Form_GenDwg
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
            this.btn_Gen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Gen
            // 
            this.btn_Gen.Location = new System.Drawing.Point(58, 189);
            this.btn_Gen.Name = "btn_Gen";
            this.btn_Gen.Size = new System.Drawing.Size(80, 32);
            this.btn_Gen.TabIndex = 0;
            this.btn_Gen.Text = "Gen";
            this.btn_Gen.UseVisualStyleBackColor = true;
            this.btn_Gen.Click += new System.EventHandler(this.btn_Gen_Click);
            // 
            // Form_GenDwg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btn_Gen);
            this.Name = "Form_GenDwg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenDwg";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Gen;
    }
}

