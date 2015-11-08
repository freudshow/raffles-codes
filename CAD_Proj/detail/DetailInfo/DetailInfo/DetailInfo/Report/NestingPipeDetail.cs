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
	public partial class NestingPipeDetail : Form
	{		
		public NestingPipeDetail()
		{
			InitializeComponent();
		}

		private DataSet GetDs()
		{
			DataSet ds = new DataSet();
			if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLWAREHOUSEUSERS"))
			{
				
				DataTable dtNest = User.NestPipeTab;//套料详表				
				dtNest.TableName = "Nest_Tab";								
				ds.Tables.Add(dtNest.Copy());
				return ds;
			}
			return ds;
		}

		private void NestingDetailViewer_Load(object sender, EventArgs e)
		{
			DataSet ds = new DataSet();
			ds = GetDs();
			if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLWAREHOUSEUSERS"))
			{
				NestingPipeRpt pmrpt = new NestingPipeRpt();
				pmrpt.SetDataSource(ds);
				NestingDetailViewer.ReportSource = pmrpt;
			}
			
			ParameterFields paramFields = new ParameterFields();

			ParameterField paramField1 = new ParameterField();
			ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();			
			paramField1.ParameterFieldName = "kickoffdate";			
			discreteVal.Value = User.KickOffDate;
			paramField1.CurrentValues.Add(discreteVal);			
			paramFields.Add(paramField1);
			
			ParameterField paramField2 = new ParameterField();
			ParameterDiscreteValue discreteVa2 = new ParameterDiscreteValue();
			paramField2.ParameterFieldName = "Margin";
			discreteVa2.Value = User.Margin;
			paramField2.CurrentValues.Add(discreteVa2);
			paramFields.Add(paramField2);
			
			ParameterField paramField3 = new ParameterField();
			ParameterDiscreteValue discreteVal3 = new ParameterDiscreteValue();
			paramField3.ParameterFieldName = "TotalBaseLength";
			discreteVal3.Value = User.TotalBaseLength;
			paramField3.CurrentValues.Add(discreteVal3);
			paramFields.Add(paramField3);
			
			ParameterField paramField4 = new ParameterField();
			ParameterDiscreteValue discreteVal4 = new ParameterDiscreteValue();
			paramField4.ParameterFieldName = "PipeRatio";
			discreteVal4.Value = User.PipeRatio;
			paramField4.CurrentValues.Add(discreteVal4);
			paramFields.Add(paramField4);

			ParameterField paramField5 = new ParameterField();
			ParameterDiscreteValue discreteVal5 = new ParameterDiscreteValue();
			paramField5.ParameterFieldName = "kickoffdateStart";
			discreteVal5.Value = User.KickOffDate_start;
			paramField5.CurrentValues.Add(discreteVal5);
			paramFields.Add(paramField5);

			ParameterField paramField6 = new ParameterField();
			ParameterDiscreteValue discreteVal6= new ParameterDiscreteValue();
			paramField6.ParameterFieldName = "kickoffdateEnd";
			discreteVal6.Value = User.KickOffDate_end;
			paramField6.CurrentValues.Add(discreteVal6);
			paramFields.Add(paramField6);

			NestingDetailViewer.ParameterFieldInfo = paramFields;
		}
	}
}
