//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
namespace Aveva.Presentation.AttributeBrowserAddin
{
    partial class AttributeListControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.attributeList = new System.Windows.Forms.ListView();
            this.Attribute = new System.Windows.Forms.ColumnHeader();
            this.Value = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // attributeList
            // 
            this.attributeList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Attribute,
            this.Value});
            this.attributeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attributeList.GridLines = true;
            this.attributeList.Location = new System.Drawing.Point(0, 0);
            this.attributeList.Name = "attributeList";
            this.attributeList.Size = new System.Drawing.Size(305, 356);
            this.attributeList.TabIndex = 0;
            this.attributeList.UseCompatibleStateImageBehavior = false;
            this.attributeList.View = System.Windows.Forms.View.Details;
            this.attributeList.SelectedIndexChanged += new System.EventHandler(this.attributeList_SelectedIndexChanged);
            // 
            // Attribute
            // 
            this.Attribute.Text = "Attribute";
            this.Attribute.Width = 155;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 132;
            // 
            // AttributeListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.attributeList);
            this.Name = "AttributeListControl";
            this.Size = new System.Drawing.Size(305, 356);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView attributeList;
        private System.Windows.Forms.ColumnHeader Attribute;
        private System.Windows.Forms.ColumnHeader Value;
    }
}
