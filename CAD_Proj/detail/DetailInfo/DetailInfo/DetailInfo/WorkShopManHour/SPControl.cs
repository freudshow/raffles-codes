using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class SPControl : UserControl
    {
        public SPControl()
        {
            InitializeComponent();
        }
        string sqlStr = string.Empty;
        string projectStr = string.Empty;
        string spoolStr = string.Empty;
        private void SPControl_Load(object sender, EventArgs e)
        {
            sqlStr = @"SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82)   ORDER BY NAME";
            DetailInfo.Application_Code.FillComboBox.FillComb(this.ProjectComboBox,sqlStr);
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            string frmtext = this.ParentForm.Text.ToString();
            projectStr = this.ProjectComboBox.Text.ToString();
            spoolStr = this.textBox1.Text.ToString();
            if (string.IsNullOrEmpty(projectStr) || string.IsNullOrEmpty(spoolStr))
            {                
                MessageBox.Show("项目号和小票号不能为空!","信息提示！", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            string normStr = ManHourManage.GetPipMaxNorm(projectStr, spoolStr);
            double norm = Convert.ToDouble(normStr);
            string factorStr = ManHourManage.GetQCORTransORPresFactor(norm);
            string[] factorname = factorStr.Split(new char[] { '-' });
            double presstestTime = Convert.ToDouble(factorname[2]);
            ManHourManage.UpdateSpoolQuotaTime(frmtext, projectStr, spoolStr, presstestTime);
            MessageBox.Show("-------------完成------------");    
        }
    }
}
