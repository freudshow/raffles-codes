//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Aveva.Pdms.Database;
using Aveva.PDMS.Database.Filters;

using Aveva.ApplicationFramework.Presentation;
using Aveva.Pdms.Presentation;

namespace Aveva.Pdms.Examples
{
	/// <summary>
	/// Summary description for AddinControl.
	/// </summary>
	public class NetGridAddinControl : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Button button1;
		private Aveva.Pdms.Presentation.NetGridControl netGridControl1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
		private MenuTool mSelectionMenu;
		private MenuTool mHeaderMenu;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;


		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public NetGridAddinControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//Initialise Grid Control Data
			initialiseGrid();

		}

		public MenuTool SelectionMenu 
		{
			get 
			{
				return mSelectionMenu;
			}
			set 
			{
				mSelectionMenu = value;
				netGridControl1.PopupMenu = mSelectionMenu;
			}
		}

		public MenuTool HeaderMenu
		{
			get 
			{
				return mHeaderMenu;
			}
			set 
			{
				mHeaderMenu = value;
				netGridControl1.PopupMenuHeader = mHeaderMenu;
			}
		}

		private void initialiseGrid()
		{
			//Add a default set if there are no user defined columns
			Hashtable atts = new Hashtable();
			atts[1.0] = "Name";
			atts[2.0] = "Type";
			atts[3.0] = "Owner";

			//Add a default set if there are no user defined columns
			Hashtable titles = new Hashtable();
			titles[1.0] = "Name of Item";
			titles[2.0] = "Type of Item";
			titles[3.0] = "Owner of Item";

			//Get Design World
			DbElement worldElement = MDB.CurrentMDB.GetFirstWorld(Aveva.Pdms.Database.DbType.Design);
	
			//Find all Equipment elements
			TypeFilter filter = new TypeFilter(DbElementTypeInstance.EQUIPMENT);
			DBElementCollection coll = new DBElementCollection(worldElement, filter);
			
			//Collect the items
			Hashtable items = new Hashtable();
			int i = 1;
			foreach (DbElement ele in coll) 
			{
				items.Add((double)i, ele);
				i++;
			}

			//Set Grid Name
			string tableName = "Example Grid";

			//Create a data source and bind it to the Grid Control
			NetDataSource dataSource;
			dataSource = new NetDataSource(tableName, atts, titles, items);
			this.netGridControl1.BindToDataSource(dataSource);

			//Display the default PDMS graphical icons in the grid with the equipment item names
			this.netGridControl1.setNameColumnImage();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.netGridControl1 = new Aveva.Pdms.Presentation.NetGridControl();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 264);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "Export to Excel";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// netGridControl1
			// 
			this.netGridControl1.allGridEvents = true;
			this.netGridControl1.ColumnExcelFilter = true;
			this.netGridControl1.ColumnSummaries = true;
			this.netGridControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.netGridControl1.EditableGrid = false;
			this.netGridControl1.ErrorIcon = true;
			this.netGridControl1.ExtendLastColumn = true;
			this.netGridControl1.FixedHeaders = true;
			this.netGridControl1.FixedRows = false;
			this.netGridControl1.GridHeight = 240;
			this.netGridControl1.HeaderSort = true;
			this.netGridControl1.HideGroupByBox = false;
			this.netGridControl1.Location = new System.Drawing.Point(0, 0);
			this.netGridControl1.Name = "netGridControl1";
			this.netGridControl1.OutlookGroupStyle = true;
			this.netGridControl1.RowAddDeleteGrid = false;
			this.netGridControl1.ScrollSelectedRowToView = true;
			this.netGridControl1.SingleRowSelection = false;
			this.netGridControl1.Size = new System.Drawing.Size(544, 240);
			this.netGridControl1.TabIndex = 1;
			this.netGridControl1.Updates = false;
			this.netGridControl1.AfterSelectChange += new Aveva.PDMS.PMLNet.PMLNetDelegate.PMLNetEventHandler(this.netGridControl1_AfterSelectChange);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(104, 264);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(168, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "c:\\GridOutput.XLS";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(8, 296);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "Print Preview";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.Items.AddRange(new object[] {
														   "Red",
														   "Green",
														   "Blue",
														   "Yellow",
														   "Silver"});
			this.comboBox1.Location = new System.Drawing.Point(152, 328);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 4;
			this.comboBox1.Text = "Red";
			this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 336);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Set Colour of first row";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 368);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 23);
			this.label2.TabIndex = 6;
			this.label2.Text = "Number of selected rows";
			// 
			// textBox2
			// 
			this.textBox2.Enabled = false;
			this.textBox2.Location = new System.Drawing.Point(152, 368);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(120, 20);
			this.textBox2.TabIndex = 7;
			this.textBox2.Text = "0";
			// 
			// AddinControl
			// 
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.netGridControl1);
			this.Controls.Add(this.button1);
			this.Name = "AddinControl";
			this.Size = new System.Drawing.Size(544, 408);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			//Save the grid data to an excel spreadsheet
			this.netGridControl1.saveGridToExcel(textBox1.Text);
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			//Open the print preview dialog
			this.netGridControl1.printPreview();
		}

		private void comboBox1_TextChanged(object sender, System.EventArgs e)
		{
			//Set the row colour
			string strColour = comboBox1.Text;
			this.netGridControl1.setRowColor(1.0, strColour);
		}

		private void netGridControl1_AfterSelectChange(System.Collections.ArrayList args)
		{
			//Print the number of the selected rows in textbox2
			if (args == null)
			{
				return;
			}
			Hashtable al = new Hashtable();
			al = (Hashtable)args[0];

			if (al == null)
			{
				return;
			}
			this.textBox2.Text = al.Count.ToString();
		}
	}
}
