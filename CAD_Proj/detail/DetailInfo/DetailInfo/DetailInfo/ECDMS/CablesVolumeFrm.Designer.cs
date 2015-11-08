namespace DetailInfo
{
    partial class CablesVolumeFrm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CableSizedgv = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.XHtb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TYPEtb = new System.Windows.Forms.TextBox();
            this.SPECtb = new System.Windows.Forms.TextBox();
            this.MAXDIAtb = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.REMARKtb = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.CABLEcontextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CableSizedgv)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.CABLEcontextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.CableSizedgv);
            this.groupBox1.Location = new System.Drawing.Point(0, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 288);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "电缆规格信息";
            // 
            // CableSizedgv
            // 
            this.CableSizedgv.AllowUserToAddRows = false;
            this.CableSizedgv.AllowUserToDeleteRows = false;
            this.CableSizedgv.AllowUserToOrderColumns = true;
            this.CableSizedgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.CableSizedgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CableSizedgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CableSizedgv.Location = new System.Drawing.Point(3, 17);
            this.CableSizedgv.Name = "CableSizedgv";
            this.CableSizedgv.ReadOnly = true;
            this.CableSizedgv.RowTemplate.Height = 23;
            this.CableSizedgv.Size = new System.Drawing.Size(573, 268);
            this.CableSizedgv.TabIndex = 0;
            this.CableSizedgv.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CableSizedgv_CellMouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "序号：";
            // 
            // XHtb
            // 
            this.XHtb.Location = new System.Drawing.Point(73, 24);
            this.XHtb.Name = "XHtb";
            this.XHtb.Size = new System.Drawing.Size(158, 21);
            this.XHtb.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "电缆型号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "电缆规格：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "电缆外径：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "备注：";
            // 
            // TYPEtb
            // 
            this.TYPEtb.Location = new System.Drawing.Point(308, 23);
            this.TYPEtb.Name = "TYPEtb";
            this.TYPEtb.Size = new System.Drawing.Size(202, 21);
            this.TYPEtb.TabIndex = 8;
            // 
            // SPECtb
            // 
            this.SPECtb.Location = new System.Drawing.Point(74, 60);
            this.SPECtb.Name = "SPECtb";
            this.SPECtb.Size = new System.Drawing.Size(158, 21);
            this.SPECtb.TabIndex = 9;
            // 
            // MAXDIAtb
            // 
            this.MAXDIAtb.Location = new System.Drawing.Point(309, 60);
            this.MAXDIAtb.Name = "MAXDIAtb";
            this.MAXDIAtb.Size = new System.Drawing.Size(201, 21);
            this.MAXDIAtb.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.REMARKtb);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.MAXDIAtb);
            this.groupBox2.Controls.Add(this.XHtb);
            this.groupBox2.Controls.Add(this.SPECtb);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.TYPEtb);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(579, 159);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // REMARKtb
            // 
            this.REMARKtb.Location = new System.Drawing.Point(73, 98);
            this.REMARKtb.Multiline = true;
            this.REMARKtb.Name = "REMARKtb";
            this.REMARKtb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.REMARKtb.Size = new System.Drawing.Size(159, 51);
            this.REMARKtb.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "录入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CABLEcontextMenuStrip1
            // 
            this.CABLEcontextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.修改ToolStripMenuItem});
            this.CABLEcontextMenuStrip1.Name = "CABLEcontextMenuStrip1";
            this.CABLEcontextMenuStrip1.Size = new System.Drawing.Size(95, 48);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 修改ToolStripMenuItem
            // 
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.修改ToolStripMenuItem.Text = "修改";
            this.修改ToolStripMenuItem.Click += new System.EventHandler(this.修改ToolStripMenuItem_Click);
            // 
            // CablesVolumeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 453);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CablesVolumeFrm";
            this.Text = "电缆册";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CableSizedgv)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.CABLEcontextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView CableSizedgv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox XHtb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TYPEtb;
        private System.Windows.Forms.TextBox SPECtb;
        private System.Windows.Forms.TextBox MAXDIAtb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox REMARKtb;
        private System.Windows.Forms.ContextMenuStrip CABLEcontextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改ToolStripMenuItem;
    }
}