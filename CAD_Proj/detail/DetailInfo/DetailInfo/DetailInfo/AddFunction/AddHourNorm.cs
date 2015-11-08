using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class AddHourNorm : Form
    {
        public AddHourNorm()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }
        public string hour;
        private void button1_Click(object sender, EventArgs e)
        {
            hour = this.textBox1.Text.ToString();
        }
    }
}