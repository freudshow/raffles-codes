namespace DetailInfo
{
    partial class GenerateDrawing
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
            this.components = new System.ComponentModel.Container();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InsertPageNobtn = new System.Windows.Forms.Button();
            this.previewbtn = new System.Windows.Forms.Button();
            this.cbtn = new System.Windows.Forms.Button();
            this.mergebtn = new System.Windows.Forms.Button();
            this.savebtn = new System.Windows.Forms.Button();
            this.DelRowConSM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除该行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.labelColor2 = new DetailInfo.LabelColor();
            this.labelColor1 = new DetailInfo.LabelColor();
            this.groupBox1.SuspendLayout();
            this.DelRowConSM.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(3, 16);
            this.progressBar1.Maximum = 1500;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(443, 40);
            this.progressBar1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelColor2);
            this.groupBox1.Controls.Add(this.labelColor1);
            this.groupBox1.Controls.Add(this.InsertPageNobtn);
            this.groupBox1.Controls.Add(this.previewbtn);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.cbtn);
            this.groupBox1.Controls.Add(this.mergebtn);
            this.groupBox1.Controls.Add(this.savebtn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 106);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件生成";
            // 
            // InsertPageNobtn
            // 
            this.InsertPageNobtn.Location = new System.Drawing.Point(92, 70);
            this.InsertPageNobtn.Name = "InsertPageNobtn";
            this.InsertPageNobtn.Size = new System.Drawing.Size(94, 25);
            this.InsertPageNobtn.TabIndex = 7;
            this.InsertPageNobtn.Text = "插入页码";
            this.InsertPageNobtn.UseVisualStyleBackColor = true;
            this.InsertPageNobtn.Click += new System.EventHandler(this.InsertPageNobtn_Click);
            // 
            // previewbtn
            // 
            this.previewbtn.Location = new System.Drawing.Point(289, 69);
            this.previewbtn.Name = "previewbtn";
            this.previewbtn.Size = new System.Drawing.Size(77, 25);
            this.previewbtn.TabIndex = 5;
            this.previewbtn.Text = "预览";
            this.previewbtn.UseVisualStyleBackColor = true;
            this.previewbtn.Click += new System.EventHandler(this.previewbtn_Click);
            // 
            // cbtn
            // 
            this.cbtn.Location = new System.Drawing.Point(372, 69);
            this.cbtn.Name = "cbtn";
            this.cbtn.Size = new System.Drawing.Size(70, 25);
            this.cbtn.TabIndex = 4;
            this.cbtn.UseVisualStyleBackColor = true;
            this.cbtn.Click += new System.EventHandler(this.cbtn_Click);
            // 
            // mergebtn
            // 
            this.mergebtn.Location = new System.Drawing.Point(6, 69);
            this.mergebtn.Name = "mergebtn";
            this.mergebtn.Size = new System.Drawing.Size(80, 25);
            this.mergebtn.TabIndex = 2;
            this.mergebtn.Text = "合并PDF";
            this.mergebtn.UseVisualStyleBackColor = true;
            this.mergebtn.Click += new System.EventHandler(this.mergebtn_Click);
            // 
            // savebtn
            // 
            this.savebtn.Location = new System.Drawing.Point(192, 69);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(91, 25);
            this.savebtn.TabIndex = 3;
            this.savebtn.Text = "保存到数据库";
            this.savebtn.UseVisualStyleBackColor = true;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click);
            // 
            // DelRowConSM
            // 
            this.DelRowConSM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除该行ToolStripMenuItem});
            this.DelRowConSM.Name = "DelRowConSM";
            this.DelRowConSM.Size = new System.Drawing.Size(123, 26);
            // 
            // 删除该行ToolStripMenuItem
            // 
            this.删除该行ToolStripMenuItem.Name = "删除该行ToolStripMenuItem";
            this.删除该行ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.删除该行ToolStripMenuItem.Text = "删除该行";
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 109);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(449, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(326, 17);
            this.toolStripStatusLabel1.Text = "提示：点击合并PDF按钮后需几秒钟启动时间，请耐心等待！";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // labelColor2
            // 
            this.labelColor2.AutoSize = true;
            this.labelColor2.ForeColor = System.Drawing.Color.Red;
            this.labelColor2.Location = new System.Drawing.Point(132, 51);
            this.labelColor2.Name = "labelColor2";
            this.labelColor2.Size = new System.Drawing.Size(0, 13);
            this.labelColor2.TabIndex = 9;
            // 
            // labelColor1
            // 
            this.labelColor1.AutoSize = true;
            this.labelColor1.ForeColor = System.Drawing.Color.Red;
            this.labelColor1.Location = new System.Drawing.Point(6, 59);
            this.labelColor1.Name = "labelColor1";
            this.labelColor1.Size = new System.Drawing.Size(0, 13);
            this.labelColor1.TabIndex = 8;
            this.labelColor1.Visible = false;
            // 
            // GenerateDrawing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(449, 131);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GenerateDrawing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成PDF图纸";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GenerateDrawing_FormClosed);
            this.Load += new System.EventHandler(this.GenerateDrawing_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DelRowConSM.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button mergebtn;
        private System.Windows.Forms.Button savebtn;
        private System.Windows.Forms.Button cbtn;
        private System.Windows.Forms.Button previewbtn;
        private System.Windows.Forms.ContextMenuStrip DelRowConSM;
        private System.Windows.Forms.ToolStripMenuItem 删除该行ToolStripMenuItem;
        private System.Windows.Forms.Button InsertPageNobtn;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private LabelColor labelColor1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private LabelColor labelColor2;
    }
}