using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class AddActualHour : Form
    {
        public AddActualHour()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }

        public string time;
        private void button1_Click(object sender, EventArgs e)
        {
            time = this.textBox1.Text.ToString();
        }
    }
}