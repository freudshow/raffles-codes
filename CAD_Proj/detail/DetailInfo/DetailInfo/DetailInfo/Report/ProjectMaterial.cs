using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared; 

namespace DetailInfo.Report
{
   
    public partial class ProjectMaterial : Form
    {
       
        public ProjectMaterial()
        {
            InitializeComponent();
        }
        
        private DataSet GetDs()
        {
			DataSet ds1 = new DataSet();
			if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLWAREHOUSEUSERS"))

			{
				DataTable dtBaseCal = User.Gather;
				
				DataTable dtFlage = User.FlageTab;
				DataTable dtElbow = User.ElbowTab;
				DataTable dtSleeve = User.SleeveTab;
				DataTable dtOtherAttach = User.OtherAttach;
				dtBaseCal.TableName = "Gather";				
				dtFlage.TableName = "flage";
				dtElbow.TableName = "elbow";
				dtSleeve.TableName = "sleeve";
				dtOtherAttach.TableName = "OtherAttach";

				ds1.Tables.Add(dtBaseCal.Copy());
				ds1.Tables.Add(dtOtherAttach.Copy());
				ds1.Tables.Add(dtFlage.Copy());
				ds1.Tables.Add(dtElbow.Copy());
				ds1.Tables.Add(dtSleeve.Copy());				
				return ds1;
			}
			else
			{
				DataTable dt = User.PipeTab;
				DataTable dt1 = User.PartTab;
				//ProjectMaterialDS ds = new ProjectMaterialDS();
				dt.TableName = "pipe_tab";
				dt1.TableName = "part_tab";
				ds1.Tables.Add(dt.Copy());
				ds1.Tables.Add(dt1.Copy());

				return ds1;
			}
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = GetDs();
			if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLWAREHOUSEUSERS"))
			{
				NestProjectMaterialRpt pmrpt = new NestProjectMaterialRpt();
				pmrpt.SetDataSource(ds);
				crystalReportViewer1.ReportSource = pmrpt;
			}
			else
			{
				ProjectMaterialRpt pmrpt = new ProjectMaterialRpt();
				pmrpt.SetDataSource(ds);
				crystalReportViewer1.ReportSource = pmrpt;
			}
		

            //crystalReportViewer1.DisplayGroupTree = false;
            ParameterFields paramFields = new ParameterFields();
            ParameterField paramField1 = new ParameterField();
            ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
            //   第一个参数是具有多个值的离散参数。设置参数字段的名称，它必须和报表中的参数相符。
            paramField1.ParameterFieldName = "kickoffdate";
            //   设置离散值并将其传递给该参数。
            discreteVal.Value = User .KickOffDate ;
            paramField1.CurrentValues.Add(discreteVal);
            //   将该参数添加到参数字段集合。
            paramFields.Add(paramField1);
			
			ParameterField paramField2 = new ParameterField();
			ParameterDiscreteValue discreteVal2 = new ParameterDiscreteValue();
			paramField2.ParameterFieldName = "TotalBaseLength";
			discreteVal2.Value = User.TotalBaseLength;
			paramField2.CurrentValues.Add(discreteVal2);
			paramFields.Add(paramField2);

			ParameterField paramField5 = new ParameterField();
			ParameterDiscreteValue discreteVal5 = new ParameterDiscreteValue();
			paramField5.ParameterFieldName = "kickoffdateStart";
			discreteVal5.Value = User.KickOffDate_start;
			paramField5.CurrentValues.Add(discreteVal5);
			paramFields.Add(paramField5);

			ParameterField paramField6 = new ParameterField();
			ParameterDiscreteValue discreteVal6 = new ParameterDiscreteValue();
			paramField6.ParameterFieldName = "kickoffdateEnd";
			discreteVal6.Value = User.KickOffDate_end;
			paramField6.CurrentValues.Add(discreteVal6);
			paramFields.Add(paramField6);

			ParameterField paramField7 = new ParameterField();
			ParameterDiscreteValue discreteVal7 = new ParameterDiscreteValue();
			paramField7.ParameterFieldName = "TotalWeight";
			discreteVal7.Value = User.PipeBaseTotalWeight;
			paramField7.CurrentValues.Add(discreteVal7);
			paramFields.Add(paramField7);

            crystalReportViewer1.ParameterFieldInfo = paramFields;
        }
    }
}
