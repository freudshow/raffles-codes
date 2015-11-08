using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class MachinningInfoFrm : Form
    {
        public MachinningInfoFrm()
        {
            InitializeComponent();
        }
        TrayClassCtl trayclassCtl;
        BlankingCtl blankingCtl;
        AssembleCtl assembleCtl;
        WeldCtl weldCtl;
        QCCtl qcCtl;
        TreatmentCtl treatmentCtl;
        DeliveryCtl deliveryCtl;
        RevieveCtl recieveCtl;
        InstallCtl installCtl;
        private void MachinningInfoFrm_Load(object sender, EventArgs e)
        {
            string frmtext = this.Text.ToString();
            switch (frmtext)
            {
                case "托盘及分类信息":
                    trayclassCtl = new TrayClassCtl();
                    trayclassCtl.Dock = DockStyle.Fill;
                    this.Controls.Add(trayclassCtl);
                    break;
                case "下料信息":
                    blankingCtl = new BlankingCtl();
                    blankingCtl.Dock = DockStyle.Fill;
                    this.Controls.Add(blankingCtl);
                    break;
                case "装配信息":
                    assembleCtl = new AssembleCtl();
                    assembleCtl.Dock = DockStyle.Fill;
                    this.Controls.Add(assembleCtl);
                    break;
                case "焊接信息":
                    weldCtl = new WeldCtl();
                    weldCtl.Dock = DockStyle.Fill;
                    this.Controls.Add(weldCtl);
                    break;
                case "报验信息":
                    qcCtl = new QCCtl();
                    qcCtl.Dock = DockStyle.Fill;
                    this.Controls.Add(qcCtl);
                    break;
                case "处理信息":
                    treatmentCtl = new TreatmentCtl();
                    treatmentCtl.Dock = DockStyle.Fill;
                    this.Controls.Add(treatmentCtl);
                    break;
                case "料场接收信息":
                    recieveCtl = new RevieveCtl();
                    recieveCtl.Dock = DockStyle.Fill;
                    this.Controls.Add(recieveCtl);
                    break;
                case "发放信息":
                    deliveryCtl = new DeliveryCtl();
                    deliveryCtl.Dock = DockStyle.Fill;
                    this.Controls.Add(deliveryCtl);
                    break;
                case "安装信息":
                    installCtl = new InstallCtl();
                    installCtl.Dock = DockStyle.Fill;
                    this.Controls.Add(installCtl);
                    break;
                default:
                    break;
            }
        }

        private void MachinningInfoFrm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = true;
        }

        private void MachinningInfoFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
        }
    }
}
