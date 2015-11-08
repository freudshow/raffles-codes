namespace DetailInfo
{
    partial class inventoryFrm
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_type = new System.Windows.Forms.TextBox();
			this.textBox_length = new System.Windows.Forms.TextBox();
			this.textBox_batchNO = new System.Windows.Forms.TextBox();
			this.textBox_ERPCODE = new System.Windows.Forms.TextBox();
			this.textBox_weight = new System.Windows.Forms.TextBox();
			this.textBox_quantity = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button1.Location = new System.Drawing.Point(77, 343);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(106, 28);
			this.button1.TabIndex = 1;
			this.button1.Text = "保存数据";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button2.Location = new System.Drawing.Point(471, 343);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(114, 27);
			this.button2.TabIndex = 2;
			this.button2.Text = "退出";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(12, 63);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(690, 274);
			this.dataGridView1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(40, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "类型(材质 外径*壁厚)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(209, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "长度(MM)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(310, 12);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "炉批号";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(403, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "ERP编码";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(507, 12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 12);
			this.label5.TabIndex = 7;
			this.label5.Text = "重量(千克)";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(617, 12);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 12);
			this.label6.TabIndex = 8;
			this.label6.Text = "数量(根)";
			// 
			// textBox_type
			// 
			this.textBox_type.Location = new System.Drawing.Point(42, 36);
			this.textBox_type.Name = "textBox_type";
			this.textBox_type.Size = new System.Drawing.Size(118, 21);
			this.textBox_type.TabIndex = 9;
			// 
			// textBox_length
			// 
			this.textBox_length.Location = new System.Drawing.Point(209, 36);
			this.textBox_length.Name = "textBox_length";
			this.textBox_length.Size = new System.Drawing.Size(53, 21);
			this.textBox_length.TabIndex = 10;
			// 
			// textBox_batchNO
			// 
			this.textBox_batchNO.Location = new System.Drawing.Point(295, 36);
			this.textBox_batchNO.Name = "textBox_batchNO";
			this.textBox_batchNO.Size = new System.Drawing.Size(74, 21);
			this.textBox_batchNO.TabIndex = 11;
			// 
			// textBox_ERPCODE
			// 
			this.textBox_ERPCODE.Location = new System.Drawing.Point(395, 36);
			this.textBox_ERPCODE.Name = "textBox_ERPCODE";
			this.textBox_ERPCODE.Size = new System.Drawing.Size(64, 21);
			this.textBox_ERPCODE.TabIndex = 12;
			// 
			// textBox_weight
			// 
			this.textBox_weight.Location = new System.Drawing.Point(509, 36);
			this.textBox_weight.Name = "textBox_weight";
			this.textBox_weight.Size = new System.Drawing.Size(48, 21);
			this.textBox_weight.TabIndex = 13;
			// 
			// textBox_quantity
			// 
			this.textBox_quantity.Location = new System.Drawing.Point(619, 36);
			this.textBox_quantity.Name = "textBox_quantity";
			this.textBox_quantity.Size = new System.Drawing.Size(48, 21);
			this.textBox_quantity.TabIndex = 14;
			this.textBox_quantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_quantity_KeyDown);
			// 
			// inventoryFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(714, 383);
			this.Controls.Add(this.textBox_quantity);
			this.Controls.Add(this.textBox_weight);
			this.Controls.Add(this.textBox_ERPCODE);
			this.Controls.Add(this.textBox_batchNO);
			this.Controls.Add(this.textBox_length);
			this.Controls.Add(this.textBox_type);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "inventoryFrm";
			this.Text = "管路物料信息管理";
			this.Load += new System.EventHandler(this.inventory_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_type;
		private System.Windows.Forms.TextBox textBox_length;
		private System.Windows.Forms.TextBox textBox_batchNO;
		private System.Windows.Forms.TextBox textBox_ERPCODE;
		private System.Windows.Forms.TextBox textBox_weight;
		private System.Windows.Forms.TextBox textBox_quantity;
    }
}