using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class AddHeatNumber : Form
    {
        public AddHeatNumber()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }

        public string heatnumber;
        private void button1_Click(object sender, EventArgs e)
        {
            heatnumber = this.textBox1.Text.ToString();
        }

        private void AddHeatNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }
    }


}