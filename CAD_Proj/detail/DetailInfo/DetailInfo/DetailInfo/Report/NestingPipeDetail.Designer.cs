namespace DetailInfo.Report
{
	partial class NestingPipeDetail
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
			this.NestingDetailViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // NestingDetailViewer
            // 
			this.NestingDetailViewer.ActiveViewIndex = -1;
			this.NestingDetailViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.NestingDetailViewer.Cursor = System.Windows.Forms.Cursors.Default;
			this.NestingDetailViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.NestingDetailViewer.Location = new System.Drawing.Point(0, 0);
			this.NestingDetailViewer.Name = "NestingDetailViewer";
			this.NestingDetailViewer.Size = new System.Drawing.Size(965, 583);
			this.NestingDetailViewer.TabIndex = 0;
			this.NestingDetailViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
			this.NestingDetailViewer.Load += new System.EventHandler(this.NestingDetailViewer_Load);            
            // 
            // NestingPipeDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 431);
			this.Controls.Add(this.NestingDetailViewer);
            this.Name = "NestingPipeDetail";
            this.Text = "套料详表";
            this.ResumeLayout(false);
		}

		#endregion

		private CrystalDecisions.Windows.Forms.CrystalReportViewer NestingDetailViewer;        
	}
}