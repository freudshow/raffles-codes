namespace DetailInfo
{
    partial class ModulationFeedBack
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
            this.components = new System.ComponentModel.Container();
            this.dgv_modulationfeedback = new System.Windows.Forms.DataGridView();
            this.ModulationFeedBackContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.接受处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出Excel表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_modulationfeedback)).BeginInit();
            this.ModulationFeedBackContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_modulationfeedback
            // 
            this.dgv_modulationfeedback.AllowUserToAddRows = false;
            this.dgv_modulationfeedback.AllowUserToDeleteRows = false;
            this.dgv_modulationfeedback.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_modulationfeedback.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_modulationfeedback.ContextMenuStrip = this.ModulationFeedBackContextMenuStrip;
            this.dgv_modulationfeedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_modulationfeedback.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_modulationfeedback.Location = new System.Drawing.Point(0, 0);
            this.dgv_modulationfeedback.Name = "dgv_modulationfeedback";
            this.dgv_modulationfeedback.RowTemplate.Height = 23;
            this.dgv_modulationfeedback.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_modulationfeedback.Size = new System.Drawing.Size(732, 439);
            this.dgv_modulationfeedback.TabIndex = 0;
            this.dgv_modulationfeedback.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_modulationfeedback_CellFormatting);
            // 
            // ModulationFeedBackContextMenuStrip
            // 
            this.ModulationFeedBackContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.接受处理ToolStripMenuItem,
            this.导出Excel表ToolStripMenuItem});
            this.ModulationFeedBackContextMenuStrip.Name = "ModulationFeedBackContextMenuStrip";
            this.ModulationFeedBackContextMenuStrip.Size = new System.Drawing.Size(137, 48);
            // 
            // 接受处理ToolStripMenuItem
            // 
            this.接受处理ToolStripMenuItem.Name = "接受处理ToolStripMenuItem";
            this.接受处理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.接受处理ToolStripMenuItem.Text = "接受处理";
            this.接受处理ToolStripMenuItem.Click += new System.EventHandler(this.接受处理ToolStripMenuItem_Click);
            // 
            // 导出Excel表ToolStripMenuItem
            // 
            this.导出Excel表ToolStripMenuItem.Name = "导出Excel表ToolStripMenuItem";
            this.导出Excel表ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.导出Excel表ToolStripMenuItem.Text = "导出Excel表";
            this.导出Excel表ToolStripMenuItem.Click += new System.EventHandler(this.导出Excel表ToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // ModulationFeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 439);
            this.Controls.Add(this.dgv_modulationfeedback);
            this.Controls.Add(this.progressBar1);
            this.Name = "ModulationFeedBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "调试反馈小票";
            this.Load += new System.EventHandler(this.ModulationFeedBack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_modulationfeedback)).EndInit();
            this.ModulationFeedBackContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_modulationfeedback;
        private System.Windows.Forms.ContextMenuStrip ModulationFeedBackContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 接受处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出Excel表ToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}