namespace Bolt
{
    partial class Form_bolt
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Hole = new System.Windows.Forms.TextBox();
            this.textBox_Thickness = new System.Windows.Forms.TextBox();
            this.btn_calc = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.radioButton_SNut = new System.Windows.Forms.RadioButton();
            this.radioButton_DNut = new System.Windows.Forms.RadioButton();
            this.textBox_SWasher = new System.Windows.Forms.TextBox();
            this.textBox_PWasher = new System.Windows.Forms.TextBox();
            this.textBox_Nut = new System.Windows.Forms.TextBox();
            this.textBox_Bolt = new System.Windows.Forms.TextBox();
            this.label_SpringWasher = new System.Windows.Forms.Label();
            this.label_nut = new System.Windows.Forms.Label();
            this.label_PlainWasher = new System.Windows.Forms.Label();
            this.label_bolt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "螺孔φ(mm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "紧固厚度(mm)";
            // 
            // textBox_Hole
            // 
            this.textBox_Hole.Location = new System.Drawing.Point(89, 29);
            this.textBox_Hole.Name = "textBox_Hole";
            this.textBox_Hole.Size = new System.Drawing.Size(113, 20);
            this.textBox_Hole.TabIndex = 2;
            // 
            // textBox_Thickness
            // 
            this.textBox_Thickness.Location = new System.Drawing.Point(89, 72);
            this.textBox_Thickness.Name = "textBox_Thickness";
            this.textBox_Thickness.Size = new System.Drawing.Size(113, 20);
            this.textBox_Thickness.TabIndex = 3;
            // 
            // btn_calc
            // 
            this.btn_calc.Location = new System.Drawing.Point(5, 241);
            this.btn_calc.Name = "btn_calc";
            this.btn_calc.Size = new System.Drawing.Size(72, 22);
            this.btn_calc.TabIndex = 4;
            this.btn_calc.Text = "计算";
            this.btn_calc.UseVisualStyleBackColor = true;
            this.btn_calc.Click += new System.EventHandler(this.btn_calc_Click);
            this.btn_calc.Enter += new System.EventHandler(this.btn_calc_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(130, 241);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(72, 22);
            this.btn_exit.TabIndex = 5;
            this.btn_exit.Text = "退出";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // radioButton_SNut
            // 
            this.radioButton_SNut.AutoSize = true;
            this.radioButton_SNut.Location = new System.Drawing.Point(16, 104);
            this.radioButton_SNut.Name = "radioButton_SNut";
            this.radioButton_SNut.Size = new System.Drawing.Size(61, 17);
            this.radioButton_SNut.TabIndex = 6;
            this.radioButton_SNut.TabStop = true;
            this.radioButton_SNut.Text = "单螺母";
            this.radioButton_SNut.UseVisualStyleBackColor = true;
            // 
            // radioButton_DNut
            // 
            this.radioButton_DNut.AutoSize = true;
            this.radioButton_DNut.Location = new System.Drawing.Point(130, 104);
            this.radioButton_DNut.Name = "radioButton_DNut";
            this.radioButton_DNut.Size = new System.Drawing.Size(61, 17);
            this.radioButton_DNut.TabIndex = 7;
            this.radioButton_DNut.TabStop = true;
            this.radioButton_DNut.Text = "双螺母";
            this.radioButton_DNut.UseVisualStyleBackColor = true;
            // 
            // textBox_SWasher
            // 
            this.textBox_SWasher.Location = new System.Drawing.Point(69, 213);
            this.textBox_SWasher.Name = "textBox_SWasher";
            this.textBox_SWasher.Size = new System.Drawing.Size(133, 20);
            this.textBox_SWasher.TabIndex = 15;
            // 
            // textBox_PWasher
            // 
            this.textBox_PWasher.Location = new System.Drawing.Point(69, 186);
            this.textBox_PWasher.Name = "textBox_PWasher";
            this.textBox_PWasher.Size = new System.Drawing.Size(133, 20);
            this.textBox_PWasher.TabIndex = 14;
            // 
            // textBox_Nut
            // 
            this.textBox_Nut.Location = new System.Drawing.Point(69, 160);
            this.textBox_Nut.Name = "textBox_Nut";
            this.textBox_Nut.Size = new System.Drawing.Size(133, 20);
            this.textBox_Nut.TabIndex = 13;
            // 
            // textBox_Bolt
            // 
            this.textBox_Bolt.Location = new System.Drawing.Point(69, 128);
            this.textBox_Bolt.Name = "textBox_Bolt";
            this.textBox_Bolt.Size = new System.Drawing.Size(133, 20);
            this.textBox_Bolt.TabIndex = 12;
            // 
            // label_SpringWasher
            // 
            this.label_SpringWasher.AutoSize = true;
            this.label_SpringWasher.Location = new System.Drawing.Point(4, 216);
            this.label_SpringWasher.Name = "label_SpringWasher";
            this.label_SpringWasher.Size = new System.Drawing.Size(43, 13);
            this.label_SpringWasher.TabIndex = 11;
            this.label_SpringWasher.Text = "弹垫圈";
            // 
            // label_nut
            // 
            this.label_nut.AutoSize = true;
            this.label_nut.Location = new System.Drawing.Point(4, 163);
            this.label_nut.Name = "label_nut";
            this.label_nut.Size = new System.Drawing.Size(31, 13);
            this.label_nut.TabIndex = 10;
            this.label_nut.Text = "螺母";
            // 
            // label_PlainWasher
            // 
            this.label_PlainWasher.AutoSize = true;
            this.label_PlainWasher.Location = new System.Drawing.Point(4, 189);
            this.label_PlainWasher.Name = "label_PlainWasher";
            this.label_PlainWasher.Size = new System.Drawing.Size(43, 13);
            this.label_PlainWasher.TabIndex = 9;
            this.label_PlainWasher.Text = "平垫圈";
            // 
            // label_bolt
            // 
            this.label_bolt.AutoSize = true;
            this.label_bolt.Location = new System.Drawing.Point(4, 135);
            this.label_bolt.Name = "label_bolt";
            this.label_bolt.Size = new System.Drawing.Size(31, 13);
            this.label_bolt.TabIndex = 8;
            this.label_bolt.Text = "螺栓";
            // 
            // Form_bolt
            // 
            this.AcceptButton = this.btn_calc;
            this.ClientSize = new System.Drawing.Size(212, 268);
            this.Controls.Add(this.textBox_SWasher);
            this.Controls.Add(this.textBox_PWasher);
            this.Controls.Add(this.textBox_Nut);
            this.Controls.Add(this.textBox_Bolt);
            this.Controls.Add(this.label_SpringWasher);
            this.Controls.Add(this.label_nut);
            this.Controls.Add(this.label_PlainWasher);
            this.Controls.Add(this.label_bolt);
            this.Controls.Add(this.radioButton_DNut);
            this.Controls.Add(this.radioButton_SNut);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_calc);
            this.Controls.Add(this.textBox_Thickness);
            this.Controls.Add(this.textBox_Hole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_bolt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "螺栓";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Hole;
        private System.Windows.Forms.TextBox textBox_Thickness;
        private System.Windows.Forms.Button btn_calc;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.RadioButton radioButton_SNut;
        private System.Windows.Forms.RadioButton radioButton_DNut;
        public System.Windows.Forms.TextBox textBox_SWasher;
        public System.Windows.Forms.TextBox textBox_PWasher;
        public System.Windows.Forms.TextBox textBox_Nut;
        public System.Windows.Forms.TextBox textBox_Bolt;
        private System.Windows.Forms.Label label_SpringWasher;
        private System.Windows.Forms.Label label_nut;
        private System.Windows.Forms.Label label_PlainWasher;
        private System.Windows.Forms.Label label_bolt;
    }
}

