using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class IntroForm : Form
    {
        public IntroForm()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }

        private void IntroForm_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "本软件属于ECDMS系统的子系统，适用于所有项目的管路设计、加工、质检、安装、调试、电子办公以及数据管理，用户范围涉及详细设计，施工设计，管加工，质检，安装以及调试部门。其主要功能包括：组合条件查询（例如： 材料信息、连接件信息、加工信息、焊缝信息、相对坐标信息），电子签名，自动生成PDF图纸，材料信息的导入，小票的电子审核，信息反馈，小票详细信息展示，生产任务分配，批量添加删除附件，项目进度监控，炉批号/证书号/托盘号管理，焊接追踪管理，生产计划管理，工时定额管理，报表的导入与导出以及基础数据的维护.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
