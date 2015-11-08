namespace DetailInfo.MaterialManage
{
    partial class MaterialDetail
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
            this.dgv_meo = new System.Windows.Forms.DataGridView();
            this.dgv_reserved = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_meo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reserved)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_meo
            // 
            this.dgv_meo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_meo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_meo.Location = new System.Drawing.Point(3, 17);
            this.dgv_meo.Name = "dgv_meo";
            this.dgv_meo.RowTemplate.Height = 23;
            this.dgv_meo.Size = new System.Drawing.Size(969, 287);
            this.dgv_meo.TabIndex = 0;
            // 
            // dgv_reserved
            // 
            this.dgv_reserved.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_reserved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_reserved.Location = new System.Drawing.Point(3, 17);
            this.dgv_reserved.Name = "dgv_reserved";
            this.dgv_reserved.RowTemplate.Height = 23;
            this.dgv_reserved.Size = new System.Drawing.Size(969, 267);
            this.dgv_reserved.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_meo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(975, 307);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "材料申请详情";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_reserved);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 307);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(975, 287);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "材料预留详情";
            // 
            // MaterialDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 594);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MaterialDetail";
            this.Text = "材料申请详情";
            this.Load += new System.EventHandler(this.MaterialDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_meo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reserved)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_meo;
        private System.Windows.Forms.DataGridView dgv_reserved;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}