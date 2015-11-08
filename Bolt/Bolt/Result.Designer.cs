namespace Bolt
{
    partial class Form_Result
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
            this.label_bolt = new System.Windows.Forms.Label();
            this.label_PlainWasher = new System.Windows.Forms.Label();
            this.label_nut = new System.Windows.Forms.Label();
            this.label_SpringWasher = new System.Windows.Forms.Label();
            this.textBox_Bolt = new System.Windows.Forms.TextBox();
            this.textBox_Nut = new System.Windows.Forms.TextBox();
            this.textBox_PWasher = new System.Windows.Forms.TextBox();
            this.textBox_SWasher = new System.Windows.Forms.TextBox();
            this.btn_CopyTo = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_bolt
            // 
            this.label_bolt.AutoSize = true;
            this.label_bolt.Location = new System.Drawing.Point(10, 27);
            this.label_bolt.Name = "label_bolt";
            this.label_bolt.Size = new System.Drawing.Size(31, 13);
            this.label_bolt.TabIndex = 0;
            this.label_bolt.Text = "螺栓";
            // 
            // label_PlainWasher
            // 
            this.label_PlainWasher.AutoSize = true;
            this.label_PlainWasher.Location = new System.Drawing.Point(10, 81);
            this.label_PlainWasher.Name = "label_PlainWasher";
            this.label_PlainWasher.Size = new System.Drawing.Size(43, 13);
            this.label_PlainWasher.TabIndex = 1;
            this.label_PlainWasher.Text = "平垫圈";
            // 
            // label_nut
            // 
            this.label_nut.AutoSize = true;
            this.label_nut.Location = new System.Drawing.Point(10, 55);
            this.label_nut.Name = "label_nut";
            this.label_nut.Size = new System.Drawing.Size(31, 13);
            this.label_nut.TabIndex = 2;
            this.label_nut.Text = "螺母";
            // 
            // label_SpringWasher
            // 
            this.label_SpringWasher.AutoSize = true;
            this.label_SpringWasher.Location = new System.Drawing.Point(10, 108);
            this.label_SpringWasher.Name = "label_SpringWasher";
            this.label_SpringWasher.Size = new System.Drawing.Size(43, 13);
            this.label_SpringWasher.TabIndex = 3;
            this.label_SpringWasher.Text = "弹垫圈";
            // 
            // textBox_Bolt
            // 
            this.textBox_Bolt.Location = new System.Drawing.Point(75, 20);
            this.textBox_Bolt.Name = "textBox_Bolt";
            this.textBox_Bolt.Size = new System.Drawing.Size(133, 20);
            this.textBox_Bolt.TabIndex = 4;
            // 
            // textBox_Nut
            // 
            this.textBox_Nut.Location = new System.Drawing.Point(75, 52);
            this.textBox_Nut.Name = "textBox_Nut";
            this.textBox_Nut.Size = new System.Drawing.Size(133, 20);
            this.textBox_Nut.TabIndex = 5;
            // 
            // textBox_PWasher
            // 
            this.textBox_PWasher.Location = new System.Drawing.Point(75, 78);
            this.textBox_PWasher.Name = "textBox_PWasher";
            this.textBox_PWasher.Size = new System.Drawing.Size(133, 20);
            this.textBox_PWasher.TabIndex = 6;
            // 
            // textBox_SWasher
            // 
            this.textBox_SWasher.Location = new System.Drawing.Point(75, 105);
            this.textBox_SWasher.Name = "textBox_SWasher";
            this.textBox_SWasher.Size = new System.Drawing.Size(133, 20);
            this.textBox_SWasher.TabIndex = 7;
            // 
            // btn_CopyTo
            // 
            this.btn_CopyTo.Location = new System.Drawing.Point(1, 147);
            this.btn_CopyTo.Name = "btn_CopyTo";
            this.btn_CopyTo.Size = new System.Drawing.Size(90, 26);
            this.btn_CopyTo.TabIndex = 8;
            this.btn_CopyTo.Text = "复制到粘贴板";
            this.btn_CopyTo.UseVisualStyleBackColor = true;
            this.btn_CopyTo.Click += new System.EventHandler(this.btn_CopyTo_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(129, 147);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(98, 26);
            this.btn_Exit.TabIndex = 9;
            this.btn_Exit.Text = "退出";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // Form_Result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 177);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_CopyTo);
            this.Controls.Add(this.textBox_SWasher);
            this.Controls.Add(this.textBox_PWasher);
            this.Controls.Add(this.textBox_Nut);
            this.Controls.Add(this.textBox_Bolt);
            this.Controls.Add(this.label_SpringWasher);
            this.Controls.Add(this.label_nut);
            this.Controls.Add(this.label_PlainWasher);
            this.Controls.Add(this.label_bolt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Result";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Result";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_bolt;
        private System.Windows.Forms.Label label_PlainWasher;
        private System.Windows.Forms.Label label_nut;
        private System.Windows.Forms.Label label_SpringWasher;
        public System.Windows.Forms.TextBox textBox_Bolt;
        public System.Windows.Forms.TextBox textBox_Nut;
        public System.Windows.Forms.TextBox textBox_PWasher;
        public System.Windows.Forms.TextBox textBox_SWasher;
        private System.Windows.Forms.Button btn_CopyTo;
        private System.Windows.Forms.Button btn_Exit;
    }
}