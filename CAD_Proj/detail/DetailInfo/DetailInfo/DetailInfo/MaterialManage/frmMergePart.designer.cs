namespace DetailInfo
{
    partial class frmMergePart
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_release = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmb_parttype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_partname = new System.Windows.Forms.ComboBox();
            this.cmb_partno = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_copy = new System.Windows.Forms.Button();
            this.btn_merge = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_query = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 411);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "材料采购申请及下发情况列表";
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.Location = new System.Drawing.Point(3, 17);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv1.Size = new System.Drawing.Size(898, 391);
            this.dgv1.TabIndex = 1;
            this.dgv1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_release);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btn_copy);
            this.groupBox1.Controls.Add(this.btn_merge);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btn_query);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 138);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "                                                ";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btn_release
            // 
            this.btn_release.Location = new System.Drawing.Point(515, 66);
            this.btn_release.Name = "btn_release";
            this.btn_release.Size = new System.Drawing.Size(98, 23);
            this.btn_release.TabIndex = 42;
            this.btn_release.Text = "解除对应关系";
            this.btn_release.UseVisualStyleBackColor = true;
            this.btn_release.Click += new System.EventHandler(this.btn_release_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmb_parttype);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmb_partname);
            this.groupBox3.Controls.Add(this.cmb_partno);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(25, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 124);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            // 
            // cmb_parttype
            // 
            this.cmb_parttype.FormattingEnabled = true;
            this.cmb_parttype.Location = new System.Drawing.Point(122, 78);
            this.cmb_parttype.Name = "cmb_parttype";
            this.cmb_parttype.Size = new System.Drawing.Size(231, 20);
            this.cmb_parttype.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "零件类型";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "零件名称及规格";
            // 
            // cmb_partname
            // 
            this.cmb_partname.FormattingEnabled = true;
            this.cmb_partname.Location = new System.Drawing.Point(122, 50);
            this.cmb_partname.Name = "cmb_partname";
            this.cmb_partname.Size = new System.Drawing.Size(231, 20);
            this.cmb_partname.TabIndex = 32;
            // 
            // cmb_partno
            // 
            this.cmb_partno.FormattingEnabled = true;
            this.cmb_partno.Location = new System.Drawing.Point(122, 24);
            this.cmb_partno.Name = "cmb_partno";
            this.cmb_partno.Size = new System.Drawing.Size(231, 20);
            this.cmb_partno.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "零件编码";
            // 
            // btn_copy
            // 
            this.btn_copy.Location = new System.Drawing.Point(411, 99);
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.Size = new System.Drawing.Size(98, 23);
            this.btn_copy.TabIndex = 38;
            this.btn_copy.Text = "复制到标准码库";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_copy_Click);
            // 
            // btn_merge
            // 
            this.btn_merge.Location = new System.Drawing.Point(411, 66);
            this.btn_merge.Name = "btn_merge";
            this.btn_merge.Size = new System.Drawing.Size(98, 23);
            this.btn_merge.TabIndex = 37;
            this.btn_merge.Text = "合         并";
            this.btn_merge.UseVisualStyleBackColor = true;
            this.btn_merge.Click += new System.EventHandler(this.btn_merge_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "关        闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_query
            // 
            this.btn_query.Location = new System.Drawing.Point(411, 33);
            this.btn_query.Name = "btn_query";
            this.btn_query.Size = new System.Drawing.Size(98, 23);
            this.btn_query.TabIndex = 33;
            this.btn_query.Text = "查询（ERP）";
            this.btn_query.UseVisualStyleBackColor = true;
            this.btn_query.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // frmMergePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 549);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMergePart";
            this.Text = "合并编码";
            this.Load += new System.EventHandler(this.frmRequisitionRationStatistic_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_query;
        private System.Windows.Forms.ComboBox cmb_partname;
        private System.Windows.Forms.ComboBox cmb_partno;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_merge;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmb_parttype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_release;
    }
}