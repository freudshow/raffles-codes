using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class AddWeldBy : Form
    {
        public AddWeldBy()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }

        public string nameby;

        private void button1_Click(object sender, EventArgs e)
        {
            nameby = this.textBox1.Text.ToString();
        }
    }
}