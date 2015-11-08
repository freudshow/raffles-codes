using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bolt
{
    public partial class Form_bolt : Form
    {
        public Form_bolt()
        {
            InitializeComponent();
            this.radioButton_SNut.Checked = true;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_calc_Click(object sender, EventArgs e)
        {
            string Hole = string.Empty;
            if ((Hole = this.textBox_Hole.Text) == string.Empty)
            {
                MessageBox.Show("请输入孔径");
                return;
            }
            double ActDN = CalcDN("GB.csv", Convert.ToDouble(Hole));
            if (ActDN == 0)
            {
                MessageBox.Show("表中无适合的螺栓，请检查孔径输入是否正确,或维护螺母厚度表!");
                return;
            }
            double NutThick = GetThicknessByName(@"GB.csv", ActDN, "nut");
            if (NutThick == 0.0)
            {
                MessageBox.Show("表中无此通径螺母，请维护数据表!");
                return;
            }
            double PlainWThick = GetThicknessByName(@"GB.csv", ActDN, "plain washer");
            if (PlainWThick == 0.0)
            {
                MessageBox.Show("表中无此通径平垫，请维护数据表!");
                return;
            }
            double SpringWThick = GetThicknessByName(@"GB.csv", ActDN, "spring washer");
            if (SpringWThick == 0.0)
            {
                MessageBox.Show("表中无此通径弹垫，请维护数据表!");
                return;
            }

            double TotalThick = 0.0;
            if (this.textBox_Thickness.Text == string.Empty)
            {
                MessageBox.Show("请填写紧固厚度");
                return;
            }
            TotalThick += Convert.ToDouble(this.textBox_Thickness.Text) + 2 * PlainWThick;

            if (this.radioButton_SNut.Checked)
            {
                TotalThick += SpringWThick + NutThick;
                this.textBox_PWasher.Text = "GB97.1 " + "M" + ActDN;
                this.textBox_SWasher.Text = "GB93 " + "M" + ActDN;
            }
            else if (this.radioButton_DNut.Checked)
            {
                TotalThick += 2 * NutThick;
                this.textBox_PWasher.Text = "GB97.1 " + "M" + ActDN;
            }
            else
            {
                MessageBox.Show("请选择单/双螺母");
                return;
            }

            double ScrewPitch = GetThicknessByName("GB.csv", ActDN, "screw pitch");
            double RecommandLength = CalcLengthOfBolt(TotalThick, ScrewPitch);
            this.textBox_Bolt.Text = "GB5783 M" + ActDN + "X" + RecommandLength;
            this.textBox_Nut.Text = "GB6170 " + "M" + ActDN;
        }


        private double CalcDN(string TextTableName, double Hole)
        {
            DataTable std_table = GetTable(TextTableName);
            if (std_table.Rows.Count == 0)
            {
                return 0.0;
            }
            if (Hole > Convert.ToDouble(std_table.Rows[std_table.Rows.Count - 1][0]) || Hole < Convert.ToDouble(std_table.Rows[0][0]))
            {
                return 0.0;
            }
            int i = 0;
            for (; Hole > Convert.ToDouble(std_table.Rows[i][0]); i++)
            {

            }
            i--;
            return Convert.ToDouble(std_table.Rows[i][0].ToString());
        }

        private DataTable GetTable(string TextTableName)
        {
            DataTable std_table = new DataTable();
            try
            {
                StreamReader sr = new StreamReader(TextTableName);
                string txt = sr.ReadLine();
                string[] heads = txt.Split(',');
                for (int i = 0; i < heads.Length; i++)
                {
                    std_table.Columns.Add(heads[i]);
                }

                while (!sr.EndOfStream)
                {
                    txt = sr.ReadLine();
                    string[] datas = txt.Split(',');
                    DataRow DTRow = std_table.NewRow();
                    for (int i = 0; i < datas.Length; i++)
                    {
                        DTRow[heads[i]] = datas[i];
                    }
                    std_table.Rows.Add(DTRow);
                }
                sr.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return std_table;
        }

        private double CalcLengthOfBolt(double length, double ScrewPitch)
        {
            double RecommendLength = length + 3 * ScrewPitch;
            int i = 0;
            while ((RecommendLength - (RecommendLength % 5) + i * 5) < RecommendLength)
            {
                i++;
            }
            return (RecommendLength - (RecommendLength % 5) + i * 5);
        }

        private double GetThickness(string TextTableName, double ActDN)
        {
            DataTable TextTable = GetTable(TextTableName);
            for (int i = 1; i < TextTable.Rows.Count;i++ )
            {
                if (Convert.ToDouble(TextTable.Rows[i][0].ToString()) == ActDN )
                {
                    return Convert.ToDouble(TextTable.Rows[i][1]);
                }
            }
            return 0.0;
        }

        private double GetThicknessByName(string TextTableName, double ActDN, string PartName)
        {
            DataTable TextTable = GetTable(TextTableName);
            for (int i = 1; i < TextTable.Rows.Count; i++)
            {
                if (Convert.ToDouble(TextTable.Rows[i]["DN"].ToString()) == ActDN)
                {
                    return Convert.ToDouble(TextTable.Rows[i][PartName]);
                }
            }
            return 0.0;
        }
    }
}