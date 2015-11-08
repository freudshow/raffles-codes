namespace DetailInfo.MaterialManage
{
    partial class ERP_PROJECTTREE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP_PROJECTTREE));
            this.tvMType = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tvMType
            // 
            this.tvMType.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tvMType.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvMType.Location = new System.Drawing.Point(0, 0);
            this.tvMType.Name = "tvMType";
            this.tvMType.Size = new System.Drawing.Size(245, 663);
            this.tvMType.TabIndex = 0;
            this.tvMType.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMType_AfterSelect);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(245, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 663);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "系统图标.ico");
            this.imageList1.Images.SetKeyName(1, "folder_documents.png");
            this.imageList1.Images.SetKeyName(2, "kappfinder.png");
            this.imageList1.Images.SetKeyName(3, "kpersonalizer.png");
            this.imageList1.Images.SetKeyName(4, "Rank.ico");
            this.imageList1.Images.SetKeyName(5, "20090314152858Z1.jpg");
            // 
            // ERP_PROJECTTREE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tvMType);
            this.Name = "ERP_PROJECTTREE";
            this.Size = new System.Drawing.Size(251, 663);
            this.Load += new System.EventHandler(this.ERP_PROJECTTREE_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvMType;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imageList1;
    }
}
