using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class AddCertificateNumber : Form
    {
        public AddCertificateNumber()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }

        public string certificatenumber;
        private void button1_Click(object sender, EventArgs e)
        {
            certificatenumber = this.textBox1.Text.ToString();
        }
    }
}