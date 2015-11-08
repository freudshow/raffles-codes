namespace DetailInfo
{
    partial class DrawingControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelColor4 = new DetailInfo.LabelColor();
            this.CalcBtn = new System.Windows.Forms.Button();
            this.labelColor1 = new DetailInfo.LabelColor();
            this.labelColor2 = new DetailInfo.LabelColor();
            this.labelColor3 = new DetailInfo.LabelColor();
            this.DrawingComboBox = new System.Windows.Forms.ComboBox();
            this.ProjectComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelColor4);
            this.groupBox1.Controls.Add(this.CalcBtn);
            this.groupBox1.Controls.Add(this.labelColor1);
            this.groupBox1.Controls.Add(this.labelColor2);
            this.groupBox1.Controls.Add(this.labelColor3);
            this.groupBox1.Controls.Add(this.DrawingComboBox);
            this.groupBox1.Controls.Add(this.ProjectComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(541, 61);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // labelColor4
            // 
            this.labelColor4.AutoSize = true;
            this.labelColor4.ForeColor = System.Drawing.Color.Red;
            this.labelColor4.Location = new System.Drawing.Point(333, 41);
            this.labelColor4.Name = "labelColor4";
            this.labelColor4.Size = new System.Drawing.Size(202, 13);
            this.labelColor4.TabIndex = 12;
            this.labelColor4.Text = "计算完成将有信息提示,请耐心等待！";
            // 
            // CalcBtn
            // 
            this.CalcBtn.Location = new System.Drawing.Point(455, 15);
            this.CalcBtn.Name = "CalcBtn";
            this.CalcBtn.Size = new System.Drawing.Size(75, 23);
            this.CalcBtn.TabIndex = 11;
            this.CalcBtn.Text = "计算";
            this.CalcBtn.UseVisualStyleBackColor = true;
            this.CalcBtn.Click += new System.EventHandler(this.CalcBtn_Click);
            // 
            // labelColor1
            // 
            this.labelColor1.AutoSize = true;
            this.labelColor1.ForeColor = System.Drawing.Color.Red;
            this.labelColor1.Location = new System.Drawing.Point(443, 15);
            this.labelColor1.Name = "labelColor1";
            this.labelColor1.Size = new System.Drawing.Size(11, 13);
            this.labelColor1.TabIndex = 10;
            this.labelColor1.Text = "*";
            // 
            // labelColor2
            // 
            this.labelColor2.AutoSize = true;
            this.labelColor2.ForeColor = System.Drawing.Color.Red;
            this.labelColor2.Location = new System.Drawing.Point(203, 15);
            this.labelColor2.Name = "labelColor2";
            this.labelColor2.Size = new System.Drawing.Size(11, 13);
            this.labelColor2.TabIndex = 9;
            this.labelColor2.Text = "*";
            // 
            // labelColor3
            // 
            this.labelColor3.AutoSize = true;
            this.labelColor3.ForeColor = System.Drawing.Color.Red;
            this.labelColor3.Location = new System.Drawing.Point(6, 41);
            this.labelColor3.Name = "labelColor3";
            this.labelColor3.Size = new System.Drawing.Size(131, 13);
            this.labelColor3.TabIndex = 8;
            this.labelColor3.Text = "注：带*号字段不能为空";
            // 
            // DrawingComboBox
            // 
            this.DrawingComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DrawingComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DrawingComboBox.FormattingEnabled = true;
            this.DrawingComboBox.Location = new System.Drawing.Point(271, 15);
            this.DrawingComboBox.Name = "DrawingComboBox";
            this.DrawingComboBox.Size = new System.Drawing.Size(171, 21);
            this.DrawingComboBox.TabIndex = 3;
            this.DrawingComboBox.SelectedIndexChanged += new System.EventHandler(this.DrawingComboBox_SelectedIndexChanged);
            // 
            // ProjectComboBox
            // 
            this.ProjectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProjectComboBox.FormattingEnabled = true;
            this.ProjectComboBox.Location = new System.Drawing.Point(50, 15);
            this.ProjectComboBox.Name = "ProjectComboBox";
            this.ProjectComboBox.Size = new System.Drawing.Size(150, 21);
            this.ProjectComboBox.TabIndex = 2;
            this.ProjectComboBox.SelectedIndexChanged += new System.EventHandler(this.ProjectComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "图纸号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目号";
            // 
            // DrawingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DrawingControl";
            this.Size = new System.Drawing.Size(541, 61);
            this.Load += new System.EventHandler(this.DrawingControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox DrawingComboBox;
        private System.Windows.Forms.ComboBox ProjectComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private LabelColor labelColor3;
        private LabelColor labelColor1;
        private LabelColor labelColor2;
        private System.Windows.Forms.Button CalcBtn;
        private LabelColor labelColor4;

    }
}
