using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class AddMSS_NO : Form
    {
        public AddMSS_NO()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }
        public string mssno = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            mssno = this.textBox1.Text.ToString();
        }
    }

    
}
