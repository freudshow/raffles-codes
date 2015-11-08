using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bolt
{
    public partial class Form_Result : Form
    {
        public Form_Result()
        {
            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_CopyTo_Click(object sender, EventArgs e)
        {
            string FinalText = string.Empty;
            FinalText += @"ÂÝË¨£º" + this.textBox_Bolt.Text + "\r\n" +
                         "ÂÝÄ¸£º" + this.textBox_Nut.Text + "\r\n" +
                         "Æ½µæ£º" + this.textBox_PWasher.Text ;
            if (!(this.textBox_SWasher.Text==string.Empty))
            {
                FinalText += "\r\n" + "µ¯µæ£º" + this.textBox_SWasher.Text;
            }

            Clipboard.SetDataObject(FinalText);
        }
    }
}