namespace DetailInfo
{
    partial class QCCtl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.项目号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.图纸号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.分段号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.系统名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.系统代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.小票号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.规格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.重量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.处理方式 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.发图时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.申请报验日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.报验通过日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.请求报验ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.导出ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.项目号,
            this.图纸号,
            this.分段号,
            this.系统名称,
            this.系统代码,
            this.小票号,
            this.规格,
            this.重量,
            this.处理方式,
            this.发图时间,
            this.申请报验日期,
            this.报验通过日期});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(943, 396);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // 项目号
            // 
            this.项目号.DataPropertyName = "PROJECTID";
            this.项目号.HeaderText = "项目号";
            this.项目号.Name = "项目号";
            this.项目号.Width = 63;
            // 
            // 图纸号
            // 
            this.图纸号.DataPropertyName = "DRAWINGNO";
            this.图纸号.HeaderText = "图纸号";
            this.图纸号.Name = "图纸号";
            this.图纸号.Width = 63;
            // 
            // 分段号
            // 
            this.分段号.DataPropertyName = "BLOCKNO";
            this.分段号.HeaderText = "分段号";
            this.分段号.Name = "分段号";
            this.分段号.Width = 63;
            // 
            // 系统名称
            // 
            this.系统名称.DataPropertyName = "SYSTEMNAME";
            this.系统名称.HeaderText = "系统名称";
            this.系统名称.Name = "系统名称";
            this.系统名称.Width = 63;
            // 
            // 系统代码
            // 
            this.系统代码.DataPropertyName = "SYSTEMID";
            this.系统代码.HeaderText = "系统代码";
            this.系统代码.Name = "系统代码";
            this.系统代码.Width = 63;
            // 
            // 小票号
            // 
            this.小票号.DataPropertyName = "SPOOLNAME";
            this.小票号.HeaderText = "小票号";
            this.小票号.Name = "小票号";
            this.小票号.Width = 63;
            // 
            // 规格
            // 
            this.规格.DataPropertyName = "NORM";
            this.规格.HeaderText = "管径/壁厚";
            this.规格.Name = "规格";
            this.规格.Width = 68;
            // 
            // 重量
            // 
            this.重量.DataPropertyName = "SPOOLWEIGHT";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.重量.DefaultCellStyle = dataGridViewCellStyle1;
            this.重量.HeaderText = "重量(kg)";
            this.重量.Name = "重量";
            this.重量.Width = 69;
            // 
            // 处理方式
            // 
            this.处理方式.DataPropertyName = "SURFACETREATMENT";
            this.处理方式.HeaderText = "处理方式";
            this.处理方式.Name = "处理方式";
            this.处理方式.Width = 63;
            // 
            // 发图时间
            // 
            this.发图时间.DataPropertyName = "ISSUED_TIME";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.发图时间.DefaultCellStyle = dataGridViewCellStyle2;
            this.发图时间.HeaderText = "发图时间";
            this.发图时间.Name = "发图时间";
            this.发图时间.Width = 63;
            // 
            // 申请报验日期
            // 
            this.申请报验日期.DataPropertyName = "TOQCDATE";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.申请报验日期.DefaultCellStyle = dataGridViewCellStyle3;
            this.申请报验日期.HeaderText = "申请报验日期";
            this.申请报验日期.Name = "申请报验日期";
            this.申请报验日期.Width = 74;
            // 
            // 报验通过日期
            // 
            this.报验通过日期.DataPropertyName = "QCPASSDATE";
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.报验通过日期.DefaultCellStyle = dataGridViewCellStyle4;
            this.报验通过日期.HeaderText = "报验通过日期";
            this.报验通过日期.Name = "报验通过日期";
            this.报验通过日期.Width = 74;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.请求报验ToolStripMenuItem,
            this.toolStripSeparator1,
            this.导出ExcelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 76);
            // 
            // 请求报验ToolStripMenuItem
            // 
            this.请求报验ToolStripMenuItem.Name = "请求报验ToolStripMenuItem";
            this.请求报验ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.请求报验ToolStripMenuItem.Text = "请求报验";
            this.请求报验ToolStripMenuItem.Visible = false;
            this.请求报验ToolStripMenuItem.Click += new System.EventHandler(this.请求报验ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            this.toolStripSeparator1.Visible = false;
            // 
            // 导出ExcelToolStripMenuItem
            // 
            this.导出ExcelToolStripMenuItem.Name = "导出ExcelToolStripMenuItem";
            this.导出ExcelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.导出ExcelToolStripMenuItem.Text = "导出Excel";
            this.导出ExcelToolStripMenuItem.Visible = false;
            this.导出ExcelToolStripMenuItem.Click += new System.EventHandler(this.导出ExcelToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 43);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Visible = false;
            // 
            // QCCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "QCCtl";
            this.Size = new System.Drawing.Size(943, 396);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 请求报验ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 导出ExcelToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 项目号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 图纸号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 分段号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 系统名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 系统代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 小票号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 规格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 重量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 处理方式;
        private System.Windows.Forms.DataGridViewTextBoxColumn 发图时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 申请报验日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 报验通过日期;
    }
}
