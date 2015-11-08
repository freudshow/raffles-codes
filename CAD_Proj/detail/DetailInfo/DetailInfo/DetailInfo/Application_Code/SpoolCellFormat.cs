using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    class SpoolCellFormat
    {
        public static void FormatCell(DataGridView dgv)
        {
            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                if (dgvc.ValueType == typeof(decimal))
                {
                    dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (dgvc.Name != "ÐÐºÅ")
                    {
                        dgvc.DefaultCellStyle.Format = "N2";
                    }
                }
                if (dgvc.ValueType == typeof(DateTime))
                {
                    dgvc.DefaultCellStyle.Format = "yy-MM-dd hh:mm:ss"; 

                }
            }
        }
    }
}
