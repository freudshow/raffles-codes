namespace DetailInfo
{
    partial class SpoolRecoveryFm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ProjectComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.DrawingComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.DeletedRecordDgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.恢复数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeletedRecordDgv)).BeginInit();
            this.DelContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ProjectComboBox,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.DrawingComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(990, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(71, 22);
            this.toolStripLabel1.Text = "请选择项目:";
            // 
            // ProjectComboBox
            // 
            this.ProjectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProjectComboBox.Name = "ProjectComboBox";
            this.ProjectComboBox.Size = new System.Drawing.Size(175, 25);
            this.ProjectComboBox.Sorted = true;
            this.ProjectComboBox.SelectedIndexChanged += new System.EventHandler(this.ProjectComboBox_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(79, 22);
            this.toolStripLabel2.Text = "请选择图纸：";
            // 
            // DrawingComboBox
            // 
            this.DrawingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrawingComboBox.Name = "DrawingComboBox";
            this.DrawingComboBox.Size = new System.Drawing.Size(175, 25);
            this.DrawingComboBox.Sorted = true;
            this.DrawingComboBox.SelectedIndexChanged += new System.EventHandler(this.DrawingComboBox_SelectedIndexChanged);
            // 
            // DeletedRecordDgv
            // 
            this.DeletedRecordDgv.AllowUserToAddRows = false;
            this.DeletedRecordDgv.AllowUserToDeleteRows = false;
            this.DeletedRecordDgv.AllowUserToResizeColumns = false;
            this.DeletedRecordDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DeletedRecordDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DeletedRecordDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column12,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16});
            this.DeletedRecordDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeletedRecordDgv.Location = new System.Drawing.Point(0, 25);
            this.DeletedRecordDgv.Name = "DeletedRecordDgv";
            this.DeletedRecordDgv.Size = new System.Drawing.Size(990, 494);
            this.DeletedRecordDgv.TabIndex = 1;
            this.DeletedRecordDgv.SelectionChanged += new System.EventHandler(this.DeletedRecordDgv_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SPOOLNAME";
            this.Column1.HeaderText = "小票号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 63;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "BLOCKNO";
            this.Column12.HeaderText = "分段号";
            this.Column12.Name = "Column12";
            this.Column12.Width = 63;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "SYSTEMID";
            this.Column2.HeaderText = "系统号";
            this.Column2.Name = "Column2";
            this.Column2.Width = 63;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "SYSTEMNAME";
            this.Column3.HeaderText = "系统名";
            this.Column3.Name = "Column3";
            this.Column3.Width = 63;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "PIPEGRADE";
            this.Column4.HeaderText = "管路等级";
            this.Column4.Name = "Column4";
            this.Column4.Width = 63;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "SURFACETREATMENT";
            this.Column5.HeaderText = "表面处理";
            this.Column5.Name = "Column5";
            this.Column5.Width = 63;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "WORKINGPRESSURE";
            this.Column6.HeaderText = "工作压力";
            this.Column6.Name = "Column6";
            this.Column6.Width = 63;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "PRESSURETESTFIELD";
            this.Column7.HeaderText = "压力试验场所";
            this.Column7.Name = "Column7";
            this.Column7.Width = 74;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "PIPECHECKFIELD";
            this.Column8.HeaderText = "校管场所";
            this.Column8.Name = "Column8";
            this.Column8.Width = 63;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "SPOOLWEIGHT";
            this.Column9.HeaderText = "小票重量";
            this.Column9.Name = "Column9";
            this.Column9.Width = 63;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "PAINTCOLOR";
            this.Column10.HeaderText = "油漆颜色";
            this.Column10.Name = "Column10";
            this.Column10.Width = 63;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "CABINTYPE";
            this.Column11.HeaderText = "舱室种类";
            this.Column11.Name = "Column11";
            this.Column11.Width = 63;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "REMARK";
            this.Column13.HeaderText = "备注";
            this.Column13.Name = "Column13";
            this.Column13.Width = 52;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "LOGNAME";
            this.Column14.HeaderText = "录入人";
            this.Column14.Name = "Column14";
            this.Column14.Width = 63;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "LOGDATE";
            this.Column15.HeaderText = "录入日期";
            this.Column15.Name = "Column15";
            this.Column15.Width = 63;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "DELETEPERSON";
            this.Column16.HeaderText = "记录删除人";
            this.Column16.Name = "Column16";
            this.Column16.Visible = false;
            this.Column16.Width = 74;
            // 
            // DelContextMenuStrip
            // 
            this.DelContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.恢复数据ToolStripMenuItem,
            this.toolStripSeparator2,
            this.导出ExcelToolStripMenuItem});
            this.DelContextMenuStrip.Name = "DelContextMenuStrip";
            this.DelContextMenuStrip.Size = new System.Drawing.Size(124, 54);
            // 
            // 恢复数据ToolStripMenuItem
            // 
            this.恢复数据ToolStripMenuItem.Name = "恢复数据ToolStripMenuItem";
            this.恢复数据ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.恢复数据ToolStripMenuItem.Text = "恢复数据";
            this.恢复数据ToolStripMenuItem.Click += new System.EventHandler(this.恢复数据ToolStripMenuItem_Click);
            // 
            // 导出ExcelToolStripMenuItem
            // 
            this.导出ExcelToolStripMenuItem.Name = "导出ExcelToolStripMenuItem";
            this.导出ExcelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.导出ExcelToolStripMenuItem.Text = "导出Excel";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // SpoolRecoveryFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 519);
            this.Controls.Add(this.DeletedRecordDgv);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SpoolRecoveryFm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "小票数据恢复";
            this.Activated += new System.EventHandler(this.SpoolRecoveryFm_Activated);
            this.Load += new System.EventHandler(this.SpoolRecoveryFm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeletedRecordDgv)).EndInit();
            this.DelContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox ProjectComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox DrawingComboBox;
        private System.Windows.Forms.DataGridView DeletedRecordDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.ContextMenuStrip DelContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 恢复数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 导出ExcelToolStripMenuItem;

    }
}