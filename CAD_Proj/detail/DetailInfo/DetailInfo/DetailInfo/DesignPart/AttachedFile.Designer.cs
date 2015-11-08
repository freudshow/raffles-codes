namespace DetailInfo
{
    partial class AttachedFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Startbtn = new System.Windows.Forms.Button();
            this.Quitbtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AttachFiledgv = new System.Windows.Forms.DataGridView();
            this.附页 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AttachFiledgv)).BeginInit();
            this.SuspendLayout();
            // 
            // Startbtn
            // 
            this.Startbtn.Location = new System.Drawing.Point(6, 186);
            this.Startbtn.Name = "Startbtn";
            this.Startbtn.Size = new System.Drawing.Size(75, 24);
            this.Startbtn.TabIndex = 0;
            this.Startbtn.Text = "确定";
            this.Startbtn.UseVisualStyleBackColor = true;
            this.Startbtn.Click += new System.EventHandler(this.Startbtn_Click);
            // 
            // Quitbtn
            // 
            this.Quitbtn.Location = new System.Drawing.Point(87, 186);
            this.Quitbtn.Name = "Quitbtn";
            this.Quitbtn.Size = new System.Drawing.Size(75, 24);
            this.Quitbtn.TabIndex = 2;
            this.Quitbtn.UseVisualStyleBackColor = true;
            this.Quitbtn.Click += new System.EventHandler(this.Quitbtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AttachFiledgv);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 180);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "附页数据";
            // 
            // AttachFiledgv
            // 
            this.AttachFiledgv.AllowUserToAddRows = false;
            this.AttachFiledgv.AllowUserToDeleteRows = false;
            this.AttachFiledgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.AttachFiledgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AttachFiledgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.附页,
            this.StartPage,
            this.EndPage});
            this.AttachFiledgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.AttachFiledgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.AttachFiledgv.Location = new System.Drawing.Point(3, 16);
            this.AttachFiledgv.Name = "AttachFiledgv";
            this.AttachFiledgv.RowTemplate.Height = 23;
            this.AttachFiledgv.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.AttachFiledgv.Size = new System.Drawing.Size(337, 154);
            this.AttachFiledgv.TabIndex = 1;
            // 
            // 附页
            // 
            this.附页.HeaderText = "附页";
            this.附页.Name = "附页";
            this.附页.ReadOnly = true;
            this.附页.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.附页.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StartPage
            // 
            this.StartPage.HeaderText = "起始页";
            this.StartPage.Name = "StartPage";
            this.StartPage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StartPage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EndPage
            // 
            this.EndPage.HeaderText = "尾页";
            this.EndPage.Name = "EndPage";
            this.EndPage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EndPage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // AttachedFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(343, 213);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Startbtn);
            this.Controls.Add(this.Quitbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AttachedFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "封面及附页";
            this.Load += new System.EventHandler(this.AttachedFile_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AttachFiledgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Startbtn;
        private System.Windows.Forms.Button Quitbtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView AttachFiledgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn 附页;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndPage;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
    }
}