using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DetailInfo
{
    public partial class BatchSheet : Form
    {
        public BatchSheet()
        {
			InitializeComponent();
		}

        ArrayList countlist = new ArrayList();
        ArrayList arlist = new ArrayList();
        public string kickoffstr = string.Empty;
		public string kickoffstr_start = string.Empty;
		public string kickoffstr_end = string.Empty;
		string projectidStr = string.Empty;
        List<PipeBase> PipeBaseList = new List<PipeBase>();//母材列表
        List<PipeBase> UsedPipeBaseList = new List<PipeBase>();//已使用母材列表
        List<PipePart> PipePartList = new List<PipePart>();//管路零件列表
        List<PipePart> NotNestPipe = new List<PipePart>();//未被套料管路列表
        private void BatchSheet_Load(object sender, EventArgs e)
        {
            DataSet PidData = new DataSet();
            string pidSql = "select distinct PROJECTID from SP_SPOOLMATERIAL_TAB WHERE FLAG = 'Y'";
            User.DataBaseConnect(pidSql, PidData);
            for (int i = 0; i < PidData.Tables[0].Rows.Count ; i++)
            {
                this.PidComb.Items.Add(PidData.Tables[0].Rows[i][0].ToString());
            }
            PidData.Dispose();

        }

        /// <summary>
        /// 管路材料数据展示格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PipeDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn dgvc in this.PipeDgv.Columns)
            {
                if (dgvc.ValueType == typeof(Decimal) || dgvc.ValueType == typeof(Int16))
                {
                    dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        /// <summary>
		/// 同类型管子套料程序,返回已使用的母材列表(贪心算法)；
		/// List<PipeBase> mPipeBaseList 初始母材列表;
        /// List<PipePart> mPipePartList 零件列表;
        /// List<PipeBase> mUsedPipeBaseList (返回)
		/// 已使用母材列表;
        /// </summary>
        /// <param name="mPipeBaseList"></param>
        /// <param name="mPipePartList"></param>
        /// <param name="mUsedPipeBaseList"></param>
        public void PipeNesting(List<PipeBase> mPipeBaseList, List<PipePart> mPipePartList, List<PipeBase> mUsedPipeBaseList)
        {
            //排序,仓库管路按照升序排列，零件长度按照降序排列
            mPipeBaseList.Sort(delegate(PipeBase a, PipeBase b) { return a.Length.CompareTo(b.Length); });
            mPipePartList.Sort(delegate(PipePart a, PipePart b) { return b.Length.CompareTo(a.Length); });

            int i, j;
            for (i = 0; i < mPipePartList.Count; i++)
            {
                for (j = 0; j < mPipeBaseList.Count; j++)
                {
                    //如果管路类型相同，且零件长度短于母材长度，则将其排入此母材的加工序列
                    if ((mPipePartList[i].PipeType == mPipeBaseList[j].PipeType) && (mPipePartList[i].Length <= mPipeBaseList[j].Length))
                    {
                        mPipePartList[i].NestedBasePipe = mPipeBaseList[j].ID;
                        mPipePartList[i].Nested = true;
						mPipePartList[i].Dispatch_date = DateTime.Now.ToString("yyyy-mm-dd");
                        mPipeBaseList[j].ContainsID.Add(mPipePartList[i].ID);
                        mPipeBaseList[j].ContainsPipe.Add(mPipePartList[i]);
                        mPipeBaseList[j].Length = mPipeBaseList[j].Length - mPipePartList[i].Length;
                        //重新按长度对母材库进行升序排列
                        mPipeBaseList.Sort(delegate(PipeBase a, PipeBase b) { return a.Length.CompareTo(b.Length); });
                        //已将当前管路零件套料完毕，跳出循环，继续下一根零件的套料
                        break;
                    }
                }
            }

            //获取已被使用的母材列表
            for (i = 0; i < mPipeBaseList.Count; i++)
                if (mPipeBaseList[i].ContainsID.Count > 0)
                    mUsedPipeBaseList.Add(mPipeBaseList[i]);

            return;
        }

        /// <summary>
        /// 选择日期后生成物料需求清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void dtPicker_end_CloseUp(object sender, EventArgs e)
        {
            countlist.Clear(); arlist.Clear();
			PipeBaseList.Clear();
			PipePartList.Clear();
			UsedPipeBaseList.Clear();
			NotNestPipe.Clear();
			this.dgv_NestedPipes.DataSource = null;
			kickoffstr = this.dtPicker_end.Value.Date.ToShortDateString();//当前选择的开工日期
			kickoffstr_start = this.dtPicker_start.Value.Date.ToShortDateString();//当前选择的开工日期
			kickoffstr_end = this.dtPicker_end.Value.Date.ToShortDateString();//当前选择的开工日期			
			DateTime dat1 = DateTime.Parse(kickoffstr_start);
			DateTime dat2 = DateTime.Parse(kickoffstr_end);

			object obj = this.PidComb.SelectedItem;
			if (obj == null)
			{
				DialogResult result;
				result = MessageBox.Show("请先选择项目！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				if (result == System.Windows.Forms.DialogResult.OK)
				{
					this.dtPicker_end.Checked = false;
					return;
				}
			}
			if (dat2.CompareTo(dat1) < 0)
			{				
				this.PipeDgv.DataSource = null;
				this.dgv_NestedPipes.DataSource = null;
				this.PartDgv.DataSource = null;
				MessageBox.Show("结束日期必须晚于开始日期...请正确选择日期顺序！");
				return;
			}
			User.getkickoffdate_start(kickoffstr_start);
			User.getkickoffdate_end(kickoffstr_end);
            
           
            projectidStr = obj.ToString();

            
            string sqlString = string.Empty;
            DataSet Mydata = new DataSet();
			sqlString = "SELECT T.MATERIALNAME FROM SP_SPOOLMATERIAL_TAB T ,SP_SPOOL_TAB S WHERE T.SPOOLNAME=S.SPOOLNAME AND T.PROJECTID = '" + projectidStr + "' AND T.FLAG = 'Y' AND (T.MATERIALNAME LIKE '%主管%' OR T.MATERIALNAME LIKE '%支管%') AND to_date(to_char(S.ALLOCATIONTIME, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd')";            
			OracleConnection OrclConn = new OracleConnection(DataAccess.OIDSConnStr);
			OrclConn.Open();
			OracleDataAdapter OrclMaterialAdapter = new OracleDataAdapter();
			OracleCommand OrclPartCmd = OrclConn.CreateCommand();
			OrclMaterialAdapter.SelectCommand = OrclPartCmd;
			OrclPartCmd.CommandText = sqlString;
			OrclMaterialAdapter.Fill(Mydata);

            if (Mydata.Tables[0].Rows.Count > 0)
            {
/**********************************套料*************************************************/
				if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLWAREHOUSEUSERS"))
                {
					
                    //获取管路零件列表
                    try
                    {
                        DataSet Partdata = new DataSet();
						string PartStr = "SELECT T.PARTWEIGHT,to_char(S.ALLOCATIONTIME,'yyyy-mm-dd') kickoff_date, T.MATERIALNAME ,T.SPOOLNAME ,T.AMOUNT ,T.ERPCODE FROM SP_SPOOLMATERIAL_TAB T ,SP_SPOOL_TAB S WHERE  T.SPOOLNAME=S.SPOOLNAME AND T.PROJECTID = '" + projectidStr + "' AND T.FLAG = 'Y' AND (T.MATERIALNAME LIKE '%主管%' OR T.MATERIALNAME LIKE '%支管%') AND to_date(to_char(S.ALLOCATIONTIME, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd')";
						OrclPartCmd.CommandText = PartStr;
						OrclMaterialAdapter.Fill(Partdata);
                        string RowString = string.Empty;//材料内容                        
                        for (int m = 0; m < Partdata.Tables[0].Rows.Count; m++)
                        {
							RowString = Partdata.Tables[0].Rows[m]["MATERIALNAME"].ToString();
							string tmpstr = Partdata.Tables[0].Rows[m]["PARTWEIGHT"].ToString();
							double tmpweight = 0.0;
							if (tmpstr == "")
								tmpweight = 0.0;
							else
								tmpweight = Convert.ToDouble(tmpstr);
							PipePartList.Add(
                                                new PipePart()
                                                {
													ID = m,
													weight = tmpweight,
													kickoff_date=Partdata.Tables[0].Rows[m]["kickoff_date"].ToString(),
													ERPCODE = Partdata.Tables[0].Rows[m]["ERPCODE"].ToString(),
													SpoolName = Partdata.Tables[0].Rows[m]["SPOOLNAME"].ToString(),
													PartName=Partdata.Tables[0].Rows[m]["AMOUNT"].ToString(),
                                                    PipeType = RowString.Substring(0, RowString.LastIndexOf('X')),
                                                    Length = Convert.ToInt32(RowString.Substring(RowString.LastIndexOf('X') + 1, RowString.LastIndexOf(' ') - RowString.LastIndexOf('X')))
                                                }
                                            );
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //获取管路母材列表
                    DataSet PipeBaseTable = new DataSet();
                    try
                    {
						string PipeBaseSQLString = "select PIPETYPE,PIPELENGTH,ERPCODE,QUANTITY,STATE,WEIGHT,PIPE_BASE_ID from pipe_inventory_table where state=1";
                        User.DataBaseConnect(PipeBaseSQLString, PipeBaseTable);
						if (PipeBaseTable.Tables[0].Rows.Count == 0)
						{
							MessageBox.Show("母材库为空，请联系维护人员");
							return;
						}
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message + "请联系管理员");
						return;
                    }
					
                    int PipeBaseIndex = 0;
                    for (int m = 0; m < PipeBaseTable.Tables[0].Rows.Count; m++)
                    {
						int Quantity=Convert.ToInt32(PipeBaseTable.Tables[0].Rows[m]["QUANTITY"]);//母材数量					
						double totalW = Convert.ToDouble(PipeBaseTable.Tables[0].Rows[m]["WEIGHT"].ToString());//母材总重
						for (int n = 0; n < Quantity; n++)
                        {
                            PipeBaseList.Add(
                                                new PipeBase()
                                                {
                                                    ID = PipeBaseIndex,
													weight = ( totalW / Quantity ),
													ERPCODE=PipeBaseTable.Tables[0].Rows[m]["ERPCODE"].ToString(),
                                                    PipeType = PipeBaseTable.Tables[0].Rows[m]["PIPETYPE"].ToString(),
                                                    Length = Convert.ToInt32(PipeBaseTable.Tables[0].Rows[m]["PIPELENGTH"]),
                                                    OriginalLength = Convert.ToInt32(PipeBaseTable.Tables[0].Rows[m]["PIPELENGTH"])
                                                }
                                            );
                            PipeBaseIndex++;
                        }
                    }


                    //套料                
                    PipeNesting(PipeBaseList, PipePartList, UsedPipeBaseList);                                        

                    List<CalPipeBase> CalBaseTable = new List<CalPipeBase>();
                    if (UsedPipeBaseList.Count != 0)
                    {
                        //为每一个母材设置标志位，以判断其型号是否被统计过
                        List<Boolean> flagCal = new List<Boolean>();
                        for (int i = 0; i < UsedPipeBaseList.Count; i++)
                            flagCal.Add(false);

                        for (int i = 0; i < UsedPipeBaseList.Count; i++)
                        {
                            for (int j = 0; j < CalBaseTable.Count; j++)
                            {
                                //若已被统计，则此型号的总数加1，重量加1，且合并其包含的零件列表，标志位设为真
                                if ((UsedPipeBaseList[i].OriginalLength == CalBaseTable[j].OriginalLength) && (UsedPipeBaseList[i].PipeType == CalBaseTable[j].PipeType) && (flagCal[i] == false))
                                {
                                    CalBaseTable[j].ToltalNum = CalBaseTable[j].ToltalNum + 1;
									CalBaseTable[j].weight += UsedPipeBaseList[i].weight;
                                    flagCal[i] = true;
                                    for (int m = 0; m < UsedPipeBaseList[i].ContainsPipe.Count; m++)
                                        CalBaseTable[j].ContainsPipe.Add(UsedPipeBaseList[i].ContainsPipe[m]);
                                    break;
                                }
                            }
                            //若未被统计，则此型号的总数置 1，加入其包含的领件列表
                            if (flagCal[i] == false)
                            {
                                CalBaseTable.Add(new CalPipeBase());
                                int Index = CalBaseTable.Count - 1;
								CalBaseTable[Index].ERPCODE = UsedPipeBaseList[i].ERPCODE;
                                CalBaseTable[Index].PipeType = UsedPipeBaseList[i].PipeType;
                                CalBaseTable[Index].OriginalLength = UsedPipeBaseList[i].OriginalLength;
                                CalBaseTable[Index].ToltalNum = 1;
								CalBaseTable[Index].weight += UsedPipeBaseList[i].weight;
                                flagCal[i] = true;
                                for (int m = 0; m < UsedPipeBaseList[i].ContainsPipe.Count; m++)
                                    CalBaseTable[Index].ContainsPipe.Add(UsedPipeBaseList[i].ContainsPipe[m]);
                            }
                        }
                    }

                    
					//管路母材汇总
					DataTable PipeBaseDataTable = new DataTable();
					PipeBaseDataTable.Columns.Add("ERP编码", typeof(String));
					PipeBaseDataTable.Columns.Add("名称", typeof(String));
					PipeBaseDataTable.Columns.Add("型号", typeof(String));
					PipeBaseDataTable.Columns.Add("长度(mm)", typeof(String));
					PipeBaseDataTable.Columns.Add("数量(根)", typeof(Int32));
					PipeBaseDataTable.Columns.Add("重量", typeof(Int32));
                    if (CalBaseTable.Count != 0)
                    {
                        for (int i = 0; i < CalBaseTable.Count; i++)
                        {
                            DataRow PipeBaseRow = PipeBaseDataTable.NewRow();
							PipeBaseRow["ERP编码"] = CalBaseTable[i].ERPCODE;

							switch (CalBaseTable[i].PipeType.Split(' ')[0])
							{
								case "A106B":
									PipeBaseRow["名称"] = "无缝钢管";
									break;
								case "TP316L":
									PipeBaseRow["名称"] = "不锈钢管";
									break;
								default:
									PipeBaseRow["名称"] = "不锈钢管";
									break;
							}
														 
							PipeBaseRow["型号"] = CalBaseTable[i].PipeType;
                            PipeBaseRow["长度(mm)"] = CalBaseTable[i].OriginalLength.ToString();
							PipeBaseRow["数量(根)"] = CalBaseTable[i].ToltalNum;
							PipeBaseRow["重量"] = CalBaseTable[i].weight;
                            PipeBaseDataTable.Rows.Add(PipeBaseRow);
                        }
                    }
                    
                    User.GetBatchSheetGatherTab(PipeBaseDataTable);
                    this.PipeDgv.DataSource = PipeBaseDataTable;
                    this.PipeDgv.AutoResizeColumns();
                    this.PipeDgv.AutoResizeRows();
                    this.PipeDgv.AllowUserToAddRows = false;					
										
					try
					{

						double LeftBase = 0;
						double TotalBase = 0;
						double TotalWeight = 0;
						for (int i = 0; i < UsedPipeBaseList.Count; i++)
						{
							TotalWeight += UsedPipeBaseList[i].weight;
							TotalBase += UsedPipeBaseList[i].OriginalLength;
							LeftBase += UsedPipeBaseList[i].Length;
						}
						User.GetMargin(LeftBase.ToString());
						User.GetTotalBaseLength(TotalBase.ToString());
						User.GetPipeRatio(((1 - (LeftBase / TotalBase)) * 100).ToString("0.00") + '%');
						User.getPipeBaseTotalWeight(TotalWeight.ToString("0.00"));

						DataSet AttachSet = new DataSet();
						sqlString = "SELECT T.MATERIALNAME 附件名,COUNT(T.MATERIALNAME) 数量 FROM SP_SPOOLMATERIAL_TAB T WHERE T.PROJECTID = '" + projectidStr + "' AND T.FLAG = 'Y' AND T.MATERIALNAME NOT LIKE '%管%' AND to_date(to_char(T.KICKOFF_DATE, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') GROUP BY T.MATERIALNAME, T.PARTWEIGHT ORDER BY T.MATERIALNAME";
						OrclPartCmd.CommandText = sqlString;
						OrclMaterialAdapter.SelectCommand = OrclPartCmd;
						OrclMaterialAdapter.Fill(AttachSet);
						this.PartDgv.DataSource = AttachSet.Tables[0];
						this.PartDgv.AllowUserToAddRows = false;
						this.PartDgv.AutoResizeColumns();
						this.PartDgv.AutoResizeRows();											
					}
					catch (System.Exception ex)
					{
						MessageBox.Show(ex.Message);
					}

					//统计未被套料的管路零件
					for (int i = 0; i < PipePartList.Count; i++)
						if (PipePartList[i].Nested == false)
							NotNestPipe.Add(PipePartList[i]);

					//显示当前行的套料详表
					try
					{
						OracleConnection OrclViewPipeConn = new OracleConnection(DataAccess.OIDSConnStr);
						OrclViewPipeConn.Open();
						OracleDataAdapter OrclViewAdapter = new OracleDataAdapter();
						OracleCommand OrclNestViewCmd = OrclViewPipeConn.CreateCommand();
						int CurRowIndex = this.PipeDgv.CurrentRow.Index;
						OrclNestViewCmd.CommandText = @"select T.INVENTORY_ID 母材临时编码, T.nesting_pipe_erpcode ERP编码, T.PIPETYPE 型号, T.PIPELENGTH 长度mm,T.NESTING_PIPE_LENGTH 零件长度, T.NESTING_PIPE_SPOOLNAME 小票号,T.NESTING_PIPE_PART_NO 零件号,T.NESTING_PIPE_WEIGHT 重量 from pipe_inventory_dispatch T where T.PROJECT_ID = '" + projectidStr + "' AND to_date(to_char(T.KICKOFF_DATE, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') AND T.PIPETYPE='" + this.PipeDgv.Rows[CurRowIndex].Cells["型号"].Value.ToString() + "' and T.PIPELENGTH=" + this.PipeDgv.Rows[CurRowIndex].Cells["长度(mm)"].Value.ToString() + "ORDER BY T.INVENTORY_ID";

						DataSet TmpViewTable = new DataSet();
						OrclViewAdapter.SelectCommand = OrclNestViewCmd;
						OrclViewAdapter.Fill(TmpViewTable);
						if (TmpViewTable.Tables.Count == 0)
						{
							MessageBox.Show("请先分配物料");
						}
						else
						{
							DataTable NestViewDataTable = new DataTable();
							NestViewDataTable.Columns.Add("母材临时编码", typeof(String));
							NestViewDataTable.Columns.Add("ERP编码", typeof(String));
							NestViewDataTable.Columns.Add("名称", typeof(String));
							NestViewDataTable.Columns.Add("型号", typeof(String));
							NestViewDataTable.Columns.Add("长度(mm)", typeof(Int32));
							NestViewDataTable.Columns.Add("小票号", typeof(String));
							NestViewDataTable.Columns.Add("零件号", typeof(String));
							NestViewDataTable.Columns.Add("零件长度", typeof(Int32));
							NestViewDataTable.Columns.Add("重量", typeof(Double));
							NestViewDataTable.Columns.Add("数量(根)", typeof(Int32));
							for (int i = 0; i < TmpViewTable.Tables[0].Rows.Count; i++)
							{
								DataRow NestViewDataRow = NestViewDataTable.NewRow();
								NestViewDataRow["母材临时编码"] = TmpViewTable.Tables[0].Rows[i]["母材临时编码"].ToString();
								NestViewDataRow["ERP编码"] = TmpViewTable.Tables[0].Rows[i]["ERP编码"].ToString();
								switch (TmpViewTable.Tables[0].Rows[i]["型号"].ToString().Split(' ')[0])
								{
									case "A106B":
										NestViewDataRow["名称"] = "无缝钢管";
										break;
									case "TP316L":
										NestViewDataRow["名称"] = "不锈钢管";
										break;
									default:
										NestViewDataRow["名称"] = "不锈钢管";
										break;
								}
								NestViewDataRow["型号"] = TmpViewTable.Tables[0].Rows[i]["型号"].ToString();
								NestViewDataRow["长度(mm)"] = TmpViewTable.Tables[0].Rows[i]["长度mm"].ToString();
								NestViewDataRow["小票号"] = TmpViewTable.Tables[0].Rows[i]["小票号"].ToString();
								NestViewDataRow["零件号"] = TmpViewTable.Tables[0].Rows[i]["零件号"].ToString();
								NestViewDataRow["零件长度"] = TmpViewTable.Tables[0].Rows[i]["零件长度"].ToString();
								NestViewDataRow["重量"] = TmpViewTable.Tables[0].Rows[i]["重量"];
								NestViewDataRow["数量(根)"] = '1'.ToString();
								NestViewDataTable.Rows.Add(NestViewDataRow);
							}
							this.dgv_NestedPipes.DataSource = NestViewDataTable;
							this.dgv_NestedPipes.AutoResizeColumns();
							this.dgv_NestedPipes.AutoResizeRows();
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message + "请联系管理员");
					}
                }
/**********************************套料完毕*********************************************/
                else
                {
					this.btn_distribute.Visible = false;
					this.dgv_NestedPipes.Visible = false;
                    for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                    {
                        countlist.Add(Mydata.Tables[0].Rows[i][0].ToString());
                    }
                    Dictionary<string, int> countdic = new Dictionary<string, int>();
                    for (int i = 0; i < countlist.Count; i++)
                    {
                        if (countdic.ContainsKey(countlist[i].ToString()))
                        {
                            countdic[countlist[i].ToString()]++;
                        }
                        else
                        {
                            countdic.Add(countlist[i].ToString(), 1);
                        }
                    }

                    DataTable dictable = new DataTable();
                    dictable.Columns.Add("管路型号", typeof(String));
                    dictable.Columns.Add("长度(mm)", typeof(Decimal));
                    dictable.Columns.Add("数量", typeof(Int16));
                    foreach (KeyValuePair<string, int> kvp in countdic)
                    {
                        DataRow newRow = dictable.NewRow();
                        string dicKey = kvp.Key.ToString();
                        int dicValue = kvp.Value;

                        int Id = dicKey.LastIndexOf('X'); int Sd = dicKey.LastIndexOf(' ');
                        string NormKey = dicKey.Substring(0, Id);
                        Decimal LengthKey = Convert.ToDecimal(dicKey.Substring(Id + 1, Sd - Id));

                        newRow["管路型号"] = NormKey; newRow["长度(mm)"] = LengthKey; newRow["数量"] = dicValue; dictable.Rows.Add(newRow);
                    }

                    this.PipeDgv.DataSource = dictable;
                    User.GetBatchSheetPipeTable(dictable);
                    Mydata.Dispose();
                    dictable.Dispose();
                    countdic.Clear();

					DataSet ds = new DataSet();
                    sqlString = "SELECT T.MATERIALNAME,T.PARTWEIGHT FROM SP_SPOOLMATERIAL_TAB T,SP_SPOOL_TAB S WHERE T.PROJECTID = '" + this.PidComb.SelectedItem + "' AND T.FLAG = 'Y' AND T.MATERIALNAME NOT LIKE '%管%' AND to_date(to_char(T.ALLOCATIONTIME, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') AND T.SPOOLNAME = S.SPOOLNAME";
					OrclPartCmd.CommandText = sqlString;
					OrclMaterialAdapter.SelectCommand = OrclPartCmd;
					OrclMaterialAdapter.Fill(ds);
					if (ds.Tables[0].Rows.Count != 0)
					{
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							arlist.Add(ds.Tables[0].Rows[i][0].ToString());
						}


						Dictionary<string, decimal> dict = new Dictionary<string, decimal>();

						for (int i = 0; i < arlist.Count; i++)
						{
							if (dict.ContainsKey(arlist[i].ToString()))
							{
								dict[arlist[i].ToString()]++;
							}
							else
							{
								dict.Add(arlist[i].ToString(), 1);
							}
						}


						System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();
						bs.DataSource = dict;
						this.PartDgv.DataSource = bs;
						this.PartDgv.Columns[0].HeaderText = "零件名";
						this.PartDgv.Columns[1].HeaderText = "数量";
						this.PartDgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
						DataTable dt = new DataTable();
						dt.Columns.Add("零件名", typeof(String));
						dt.Columns.Add("数量", typeof(Int32));
						foreach (KeyValuePair<string, decimal> kvp in dict)
						{
							DataRow newRow = dt.NewRow();
							string dicKey = kvp.Key.ToString();
							int dicValue = Convert.ToInt32(kvp.Value);
							newRow["零件名"] = dicKey; newRow["数量"] = dicValue;
							dt.Rows.Add(newRow);
						}
						User.GetBatchSheetPartTable(dt);
						ds.Dispose();
						dict.Clear();
					}
					else
					{
						this.PartDgv.DataSource = null;
						DataTable dt = new DataTable();
						User.GetBatchSheetPartTable(dt);
					}
                }
            }
            else
            {				
                this.PipeDgv.DataSource = null;
				this.PartDgv.DataSource = null;
				this.dgv_NestedPipes.DataSource = null;
				MessageBox.Show("此时间段内尚未检测到需加工管路");
				this.PipeDgv.ContextMenuStrip = null;
                DataTable dt = new DataTable();
                User.GetBatchSheetPipeTable(dt);
            }			
        }
		

        private void BatchSheet_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
            MDIForm.tool_strip.Items[2].Enabled = false;
        }

		private void OutToolStripMenuItem_Click(object sender, EventArgs e)
        {
			try
			{
				OracleConnection OrclConn = new OracleConnection(DataAccess.OIDSConnStr);
				OrclConn.Open();
				OracleDataAdapter OrclNestAdapter = new OracleDataAdapter();
				OracleCommand OrclNestCmd = OrclConn.CreateCommand();
				//法兰汇总
				OrclNestCmd.CommandText = "select ERPCODE,MATERIAL_CODE,MATERIAL_TYPE,MATERIAL_NAME,MATERIALNAME,COUNT(MATERIALNAME) 数量 ,SUM(PARTWEIGHT) 重量 from pre_dispatch_flage T where T.PROJECTID = '" + projectidStr + "' AND  to_date(to_char(T.ALLOCATIONTIME, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') group by ERPCODE, MATERIAL_CODE, MATERIAL_TYPE, MATERIALNAME";
				DataSet FlageSet = new DataSet();
				OrclNestAdapter.SelectCommand = OrclNestCmd;
				OrclNestAdapter.Fill(FlageSet);
				DataTable FlageTable = new DataTable();
				if (FlageSet.Tables[0].Rows.Count > 0)
				{
					FlageTable.Columns.Add("ERP编码", typeof(String));
					FlageTable.Columns.Add("名称", typeof(String));
					FlageTable.Columns.Add("型号", typeof(String));
					FlageTable.Columns.Add("重量", typeof(Double));
					FlageTable.Columns.Add("数量（个）", typeof(Int32));
					for (int i = 0; i < FlageSet.Tables[0].Rows.Count; i++)
					{
						DataRow FlageRow = FlageTable.NewRow();
						FlageRow["ERP编码"] = FlageSet.Tables[0].Rows[i]["ERPCODE"];
						int idx = FlageSet.Tables[0].Rows[i]["MATERIAL_NAME"].ToString().Split(' ').Length - 1;
						FlageRow["名称"] = FlageSet.Tables[0].Rows[i]["MATERIAL_NAME"].ToString().Split(' ')[idx];

						string[] typename = FlageSet.Tables[0].Rows[i]["MATERIALNAME"].ToString().Split(' ');
						idx = typename.Length - 1;
						ArrayList al = new ArrayList(typename);
						al.Remove(typename[idx]);
						typename = (string[])al.ToArray(typeof(string));
						string PartType = string.Empty;
						for (int m = 0; m < typename.Length; m++)
						{
							PartType = PartType + typename[m] + ' ';
						}
						string PartTp = string.Empty;
						for (int m = 0; m < PartType.Length - 1; m++)
						{
							PartTp = PartTp + PartType[m];
						}
						FlageRow["型号"] = PartTp;
						FlageRow["重量"] = FlageSet.Tables[0].Rows[i]["重量"];
						FlageRow["数量（个）"] = FlageSet.Tables[0].Rows[i]["数量"];
						FlageTable.Rows.Add(FlageRow);
					}
					User.GetBatchSheetFlageTab(FlageTable);
				}
				//弯头汇总
				OrclNestCmd.CommandText = "select ERPCODE,MATERIAL_CODE,MATERIAL_TYPE,MATERIAL_NAME,MATERIALNAME,COUNT(MATERIALNAME) 数量 ,SUM(PARTWEIGHT) 重量 from pre_dispatch_elbow T where to_date(to_char(T.ALLOCATIONTIME, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') group by ERPCODE, MATERIAL_CODE, MATERIAL_TYPE, MATERIALNAME";
				DataSet ElbowSet = new DataSet();
				OrclNestAdapter.SelectCommand = OrclNestCmd;
				OrclNestAdapter.Fill(ElbowSet);
				DataTable ElbowTable = new DataTable();
				if (ElbowSet.Tables[0].Rows.Count > 0)
				{
					ElbowTable.Columns.Add("ERP编码", typeof(String));
					ElbowTable.Columns.Add("名称", typeof(String));
					ElbowTable.Columns.Add("型号", typeof(String));
					ElbowTable.Columns.Add("重量", typeof(Double));
					ElbowTable.Columns.Add("数量（个）", typeof(Int32));
					for (int i = 0; i < ElbowSet.Tables[0].Rows.Count; i++)
					{
						DataRow ElbowRow = ElbowTable.NewRow();
						ElbowRow["ERP编码"] = ElbowSet.Tables[0].Rows[i]["ERPCODE"];

						int idx = ElbowSet.Tables[0].Rows[i]["MATERIAL_NAME"].ToString().Split(' ').Length - 1;
						ElbowRow["名称"] = ElbowSet.Tables[0].Rows[i]["MATERIAL_NAME"].ToString().Split(' ')[idx];

						string[] typename = ElbowSet.Tables[0].Rows[i]["MATERIALNAME"].ToString().Split(' ');
						idx = typename.Length - 1;
						ArrayList al = new ArrayList(typename);
						al.Remove(typename[idx]);
						typename = (string[])al.ToArray(typeof(string));
						string PartType = string.Empty;
						for (int m = 0; m < typename.Length; m++)
						{
							PartType = PartType + typename[m] + ' ';
						}
						string PartTp = string.Empty;
						for (int m = 0; m < PartType.Length - 1; m++)
						{
							PartTp = PartTp + PartType[m];
						}
						ElbowRow["型号"] = PartTp;
						ElbowRow["重量"] = ElbowSet.Tables[0].Rows[i]["重量"];
						ElbowRow["数量（个）"] = ElbowSet.Tables[0].Rows[i]["数量"];
						ElbowTable.Rows.Add(ElbowRow);
					}
					User.GetBatchSheetElbowTab(ElbowTable);
				}
				//套筒汇总
				OrclNestCmd.CommandText = "select ERPCODE,MATERIAL_CODE,MATERIAL_TYPE,MATERIAL_NAME,MATERIALNAME,COUNT(MATERIALNAME) 数量 ,SUM(PARTWEIGHT) 重量 from pre_dispatch_sleeve T where to_date(to_char(T.ALLOCATIONTIME, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') group by ERPCODE, MATERIAL_CODE, MATERIAL_TYPE, MATERIALNAME";
				DataSet SleeveSet = new DataSet();
				OrclNestAdapter.SelectCommand = OrclNestCmd;
				OrclNestAdapter.Fill(SleeveSet);
				DataTable SleeveTable = new DataTable();
				if (SleeveSet.Tables[0].Rows.Count > 0)
				{
					SleeveTable.Columns.Add("ERP编码", typeof(String));
					SleeveTable.Columns.Add("名称", typeof(String));
					SleeveTable.Columns.Add("型号", typeof(String));
					SleeveTable.Columns.Add("重量", typeof(Double));
					SleeveTable.Columns.Add("数量（个）", typeof(Int32));
					for (int i = 0; i < SleeveSet.Tables[0].Rows.Count; i++)
					{
						DataRow SleeveRow = SleeveTable.NewRow();

						SleeveRow["ERP编码"] = SleeveSet.Tables[0].Rows[i]["ERPCODE"];


						SleeveRow["名称"] = SleeveSet.Tables[0].Rows[i]["MATERIAL_NAME"].ToString().Split(' ')[0];

						string[] typename = SleeveSet.Tables[0].Rows[i]["MATERIALNAME"].ToString().Split(' ');
						int idx = typename.Length - 1;
						ArrayList al = new ArrayList(typename);
						if (SleeveSet.Tables[0].Rows[i]["MATERIAL_NAME"].ToString().IndexOf('#') < 0)
							al.Remove(typename[idx]);
						else
							al.Remove(typename[idx - 1]);

						typename = (string[])al.ToArray(typeof(string));
						string PartType = string.Empty;
						for (int m = 0; m < typename.Length; m++)
						{
							PartType = PartType + typename[m] + ' ';
						}
						string PartTp = string.Empty;
						for (int m = 0; m < PartType.Length - 1; m++)
						{
							PartTp = PartTp + PartType[m];
						}
						SleeveRow["型号"] = PartTp;
						SleeveRow["重量"] = SleeveSet.Tables[0].Rows[i]["重量"];
						SleeveRow["数量（个）"] = SleeveSet.Tables[0].Rows[i]["数量"];
						SleeveTable.Rows.Add(SleeveRow);
					}
					User.GetBatchSheetSleeveTab(SleeveTable);
					//其他附件汇总
					OrclNestCmd.CommandText = "SELECT T.MATERIALNAME 附件名,COUNT(T.MATERIALNAME) 数量 FROM SP_SPOOLMATERIAL_TAB T WHERE T.MATERIALNAME NOT LIKE '%法兰%' AND T.MATERIALNAME NOT LIKE '%弯头%' AND T.MATERIALNAME NOT LIKE '%套筒%' AND T.PROJECTID = '" + projectidStr + "' AND T.FLAG = 'Y' AND T.MATERIALNAME NOT LIKE '%管%' AND to_date(to_char(T.ALLOCATIONTIME, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') GROUP BY T.MATERIALNAME, T.PARTWEIGHT ORDER BY T.MATERIALNAME";					
					DataSet OtherAttachSet = new DataSet();
					OrclNestAdapter.SelectCommand = OrclNestCmd;
					OrclNestAdapter.Fill(OtherAttachSet);
					User.GetOtherAttachTab(OtherAttachSet.Tables[0]);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			DetailInfo.Report.ProjectMaterial materialRpt = new DetailInfo.Report.ProjectMaterial();
			materialRpt.MdiParent = MDIForm.pMainWin;
			materialRpt.Show();			
        }

		private void NestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OracleConnection OrclConn = new OracleConnection(DataAccess.OIDSConnStr);
				OrclConn.Open();				
				OracleDataAdapter OrclNestAdapter = new OracleDataAdapter();
				OracleCommand OrclNestCmd = OrclConn.CreateCommand();
 				OrclNestCmd.CommandText = @"select T.INVENTORY_ID 母材临时编码, T.nesting_pipe_erpcode ERP编码, T.PIPETYPE 型号, T.PIPELENGTH 长度mm,T.NESTING_PIPE_LENGTH 零件长度, T.NESTING_PIPE_SPOOLNAME 小票号,T.NESTING_PIPE_PART_NO 零件号 , T.NESTING_PIPE_WEIGHT 重量 from pipe_inventory_dispatch T where T.PROJECT_ID = '" + projectidStr + "' AND to_date(to_char(T.KICKOFF_DATE, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') ORDER BY T.INVENTORY_ID";
				DataSet TmpNestTable = new DataSet();
				OrclNestAdapter.SelectCommand = OrclNestCmd;
				OrclNestAdapter.Fill(TmpNestTable);
				if (TmpNestTable.Tables[0].Rows.Count == 0)
				{
					MessageBox.Show("套料详表未写入,请先分配物料");
					return;		
				}
				else
				{
					DataTable NestDataTable = new DataTable();
					NestDataTable.Columns.Add("母材临时编码", typeof(String));
					NestDataTable.Columns.Add("ERP编码", typeof(String));
					NestDataTable.Columns.Add("名称", typeof(String));
					NestDataTable.Columns.Add("型号", typeof(String));
					NestDataTable.Columns.Add("长度(mm)", typeof(Int32));
					NestDataTable.Columns.Add("小票号", typeof(String));
					NestDataTable.Columns.Add("零件号", typeof(String));
					NestDataTable.Columns.Add("零件长度", typeof(Int32));
					NestDataTable.Columns.Add("重量", typeof(Double));
					NestDataTable.Columns.Add("数量(根)", typeof(Int32));
					for (int i = 0; i < TmpNestTable.Tables[0].Rows.Count; i++)
					{
						DataRow NestDataRow = NestDataTable.NewRow();
						NestDataRow["母材临时编码"] = TmpNestTable.Tables[0].Rows[i]["母材临时编码"].ToString();
						NestDataRow["ERP编码"] = TmpNestTable.Tables[0].Rows[i]["ERP编码"].ToString();
						switch (TmpNestTable.Tables[0].Rows[i]["型号"].ToString().Split(' ')[0])
						{
							case "A106B":
								NestDataRow["名称"] = "无缝钢管";
								break;
							case "TP316L":
								NestDataRow["名称"] = "不锈钢管";
								break;
							default:
								NestDataRow["名称"] = "不锈钢管";
								break;
						}
						NestDataRow["型号"] = TmpNestTable.Tables[0].Rows[i]["型号"].ToString();
						NestDataRow["长度(mm)"] = TmpNestTable.Tables[0].Rows[i]["长度mm"];
						NestDataRow["小票号"] = TmpNestTable.Tables[0].Rows[i]["小票号"].ToString();
						NestDataRow["零件号"] = TmpNestTable.Tables[0].Rows[i]["零件号"].ToString();
						NestDataRow["零件长度"] = TmpNestTable.Tables[0].Rows[i]["零件长度"];
						NestDataRow["重量"] = TmpNestTable.Tables[0].Rows[i]["重量"];
						NestDataRow["数量(根)"] = 1;
						NestDataTable.Rows.Add(NestDataRow);
					}
					User.GetBatchSheetNestPipeTab(NestDataTable);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			try
			{
				DetailInfo.Report.NestingPipeDetail NestingDetail = new Report.NestingPipeDetail();
				NestingDetail.MdiParent = MDIForm.pMainWin;
				NestingDetail.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void btn_distribute_Click(object sender, EventArgs e)
        {
			if (PipePartList.Count == 0||UsedPipeBaseList.Count == 0)
				return;
			//分配物料及维护管路母材库

			//判断分配表中是否已分配当天物料，如没有，则分配；反之，更新
			try
			{
				OracleConnection OrclConn = new OracleConnection(DataAccess.OIDSConnStr);
				OrclConn.Open();
				OracleDataAdapter OrclNestAdapter = new OracleDataAdapter();
				OracleCommand OrclNestCmd = OrclConn.CreateCommand();
				OracleCommand OrclCmd = OrclConn.CreateCommand();

				OrclNestCmd.CommandText = @"select * from pipe_inventory_dispatch T where T.PROJECT_ID = '" + projectidStr + "' AND to_date(to_char(T.KICKOFF_DATE, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd')";
				DataSet TmpDateTable = new DataSet();
				OrclNestAdapter.SelectCommand = OrclNestCmd;
				OrclNestAdapter.Fill(TmpDateTable);
				
				if (TmpDateTable.Tables[0].Rows.Count > 0)
				{
					if ((MessageBox.Show("检测到当日物料已分配，确定重新分配物料吗？", "请选择 ", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == System.Windows.Forms.DialogResult.Yes)
					{
						//先删除当日已分配的数据
						try
						{
							OrclCmd.CommandText = "delete from pipe_inventory_dispatch T where T.PROJECT_ID = '" + projectidStr + "' AND to_date(to_char(T.KICKOFF_DATE, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd')";
							OrclCmd.ExecuteNonQuery();
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message + ",请联系数据库管理员!");
						}
						//重新分配物料
						try
						{
							for (int i = 0; i < UsedPipeBaseList.Count; i++)
							{
								for (int j = 0; j < UsedPipeBaseList[i].ContainsPipe.Count; j++)
								{								
									//写入物料分配表
									OrclCmd.CommandText = "INSERT INTO pipe_inventory_dispatch (PROJECT_ID,PIPETYPE,PIPELENGTH,NESTING_PIPE_LENGTH,INVENTORY_ID,KICKOFF_DATE,NESTING_PIPE_SPOOLNAME,NESTING_PIPE_ERPCODE,NESTING_PIPE_PART_NO,NESTING_PIPE_WEIGHT) VALUES ('" + projectidStr + "','" + UsedPipeBaseList[i].PipeType + "'," + UsedPipeBaseList[i].OriginalLength + "," + UsedPipeBaseList[i].ContainsPipe[j].Length + "," + UsedPipeBaseList[i].ID + ",to_date('" + UsedPipeBaseList[i].ContainsPipe[j].kickoff_date + "', 'yyyy-mm-dd'),'" + UsedPipeBaseList[i].ContainsPipe[j].SpoolName + "','" + UsedPipeBaseList[i].ContainsPipe[j].ERPCODE + "','" + UsedPipeBaseList[i].ContainsPipe[j].PartName + "'," + UsedPipeBaseList[i].ContainsPipe[j].weight + ")"; 
										OrclCmd.ExecuteNonQuery();								
								}
							}

							MessageBox.Show("分配物料成功！");
						}
						catch (System.Exception ex)
						{
							MessageBox.Show(ex.Message + ",请联系数据库管理员!");
						}
						if (NotNestPipe.Count > 0)
						{
							try
							{
								//写入未分配管子库								
								for (int i = 0; i < NotNestPipe.Count; i++)
								{
									OrclCmd.CommandText = "INSERT INTO PIPE_NOT_NESTED (PIPE_ID,PROJECT_ID,SPOOL_NAME,PART_NO,PIPE_TYPE,PIPE_LENGTH,KICKOFF_DATE,PIPE_WEIGHT) VALUES (" + NotNestPipe[i].ID + ",'" + projectidStr + "','" + NotNestPipe[i].SpoolName + "','" + NotNestPipe[i].PartName + "','" + NotNestPipe[i].PipeType + "'," + NotNestPipe[i].Length + ",TO_DATE('" + NotNestPipe[i].kickoff_date + "','YYYY-MM-DD')," + NotNestPipe[i].weight + ")";
									OrclCmd.ExecuteNonQuery();
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message);
							}
							MessageBox.Show("还有暂未分配的物料");
							return;
						}
						//维护母材库
					}
					else
						return;
				}
				else
				{
					//分配物料
					try
					{
						for (int i = 0; i < UsedPipeBaseList.Count; i++)
						{
							for (int j = 0; j < UsedPipeBaseList[i].ContainsPipe.Count; j++)
							{
							
									//写入物料分配表
								OrclCmd.CommandText = "INSERT INTO pipe_inventory_dispatch (PROJECT_ID,PIPETYPE,PIPELENGTH,NESTING_PIPE_LENGTH,INVENTORY_ID,KICKOFF_DATE,NESTING_PIPE_SPOOLNAME,NESTING_PIPE_ERPCODE,NESTING_PIPE_PART_NO,NESTING_PIPE_WEIGHT) VALUES ('" + projectidStr + "','" + UsedPipeBaseList[i].PipeType + "'," + UsedPipeBaseList[i].OriginalLength + "," + UsedPipeBaseList[i].ContainsPipe[j].Length + "," + UsedPipeBaseList[i].ID + ",to_date('" + UsedPipeBaseList[i].ContainsPipe[j].kickoff_date + "', 'yyyy-mm-dd'),'" + UsedPipeBaseList[i].ContainsPipe[j].SpoolName + "','" + UsedPipeBaseList[i].ContainsPipe[j].ERPCODE + "','" + UsedPipeBaseList[i].ContainsPipe[j].PartName + "'," + UsedPipeBaseList[i].ContainsPipe[j].weight + ")";
									OrclCmd.ExecuteNonQuery();
							
							
							}
						}
						MessageBox.Show("分配物料成功！");
					}
					catch (System.Exception ex)
					{
						MessageBox.Show(ex.Message + ",请联系数据库管理员!");
					}
					if (NotNestPipe.Count > 0)
					{
						try
						{
							//写入未分配管子库							
							for (int i = 0; i < NotNestPipe.Count; i++)
							{
								OrclCmd.CommandText = "INSERT INTO PIPE_NOT_NESTED (PIPE_ID,PROJECT_ID,SPOOL_NAME,PART_NO,PIPE_TYPE,PIPE_LENGTH,KICKOFF_DATE,PIPE_WEIGHT) VALUES (" + NotNestPipe[i].ID + ",'" + projectidStr + "','" + NotNestPipe[i].SpoolName + "','" + NotNestPipe[i].PartName + "','" + NotNestPipe[i].PipeType + "'," + NotNestPipe[i].Length + ",TO_DATE('" + NotNestPipe[i].kickoff_date + "','YYYY-MM-DD')," + NotNestPipe[i].weight + ")";
								OrclCmd.ExecuteNonQuery();
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						MessageBox.Show("还有暂未分配的物料");
					}					

					//维护母材库
					
					return;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + ",请联系数据库管理员!");
			}
			//分配完毕，显示当前行的数据
			try
			{
				OracleConnection OrclViewPipeConn = new OracleConnection(DataAccess.OIDSConnStr);
				OrclViewPipeConn.Open();
				OracleDataAdapter OrclViewAdapter = new OracleDataAdapter();
				OracleCommand OrclNestCmd = OrclViewPipeConn.CreateCommand();
				int CurRowIndex = this.PipeDgv.CurrentRow.Index;
				OrclNestCmd.CommandText = @"select T.INVENTORY_ID 母材临时编码, T.nesting_pipe_erpcode ERP编码, T.PIPETYPE 型号, T.PIPELENGTH 长度mm,T.NESTING_PIPE_LENGTH 零件长度, T.NESTING_PIPE_SPOOLNAME 小票号,T.NESTING_PIPE_PART_NO 零件号,T.NESTING_PIPE_WEIGHT 重量 from pipe_inventory_dispatch T where T.PROJECT_ID = '" + projectidStr + "' AND to_date(to_char(T.KICKOFF_DATE, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') AND T.PIPETYPE='" + this.PipeDgv.Rows[CurRowIndex].Cells["型号"].Value.ToString() + "' and T.PIPELENGTH=" + this.PipeDgv.Rows[CurRowIndex].Cells["长度(mm)"].Value.ToString() + "ORDER BY T.INVENTORY_ID";

				DataSet TmpViewTable = new DataSet();
				OrclViewAdapter.SelectCommand = OrclNestCmd;
				OrclViewAdapter.Fill(TmpViewTable);
				if (TmpViewTable.Tables.Count == 0)
				{
					MessageBox.Show("请先分配物料");
				}
				else
				{
					DataTable NestViewDataTable = new DataTable();
					NestViewDataTable.Columns.Add("母材临时编码", typeof(String));
					NestViewDataTable.Columns.Add("ERP编码", typeof(String));
					NestViewDataTable.Columns.Add("名称", typeof(String));
					NestViewDataTable.Columns.Add("型号", typeof(String));
					NestViewDataTable.Columns.Add("长度(mm)", typeof(Int32));
					NestViewDataTable.Columns.Add("小票号", typeof(String));
					NestViewDataTable.Columns.Add("零件号", typeof(String));
					NestViewDataTable.Columns.Add("零件长度", typeof(Int32));
					NestViewDataTable.Columns.Add("重量", typeof(Double));
					NestViewDataTable.Columns.Add("数量(根)", typeof(Int32));
					for (int i = 0; i < TmpViewTable.Tables[0].Rows.Count; i++)
					{
						DataRow NestViewDataRow = NestViewDataTable.NewRow();
						NestViewDataRow["母材临时编码"] = TmpViewTable.Tables[0].Rows[i]["母材临时编码"].ToString();
						NestViewDataRow["ERP编码"] = TmpViewTable.Tables[0].Rows[i]["ERP编码"].ToString();
						switch (TmpViewTable.Tables[0].Rows[i]["型号"].ToString().Split(' ')[0])
						{
							case "A106B":
								NestViewDataRow["名称"] = "无缝钢管";
								break;
							case "TP316L":
								NestViewDataRow["名称"] = "不锈钢管";
								break;
							default:
								NestViewDataRow["名称"] = "不锈钢管";
								break;
						}
						NestViewDataRow["型号"] = TmpViewTable.Tables[0].Rows[i]["型号"].ToString();
						NestViewDataRow["长度(mm)"] = TmpViewTable.Tables[0].Rows[i]["长度mm"].ToString();
						NestViewDataRow["小票号"] = TmpViewTable.Tables[0].Rows[i]["小票号"].ToString();
						NestViewDataRow["零件号"] = TmpViewTable.Tables[0].Rows[i]["零件号"].ToString();
						NestViewDataRow["零件长度"] = TmpViewTable.Tables[0].Rows[i]["零件长度"].ToString();
						NestViewDataRow["重量"] = TmpViewTable.Tables[0].Rows[i]["重量"];
						NestViewDataRow["数量(根)"] = '1'.ToString();
						NestViewDataTable.Rows.Add(NestViewDataRow);
					}
					this.dgv_NestedPipes.DataSource = NestViewDataTable;
					this.dgv_NestedPipes.AutoResizeColumns();
					this.dgv_NestedPipes.AutoResizeRows();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "请联系管理员");
			}
        }

        //显示已套料管路
        private void PipeDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLWAREHOUSEUSERS"))
            {
				try
				{
					OracleConnection OrclViewPipeConn = new OracleConnection(DataAccess.OIDSConnStr);
					OrclViewPipeConn.Open();
					OracleDataAdapter OrclViewAdapter = new OracleDataAdapter();
					OracleCommand OrclNestCmd = OrclViewPipeConn.CreateCommand();
					int CurRowIndex = this.PipeDgv.CurrentRow.Index;
					OrclNestCmd.CommandText = @"select T.INVENTORY_ID 母材临时编码, T.nesting_pipe_erpcode ERP编码, T.PIPETYPE 型号, T.PIPELENGTH 长度mm,T.NESTING_PIPE_LENGTH 零件长度, T.NESTING_PIPE_SPOOLNAME 小票号,T.NESTING_PIPE_PART_NO 零件号  ,T.NESTING_PIPE_WEIGHT 重量 from pipe_inventory_dispatch T where T.PROJECT_ID = '" + projectidStr + "' AND to_date(to_char(T.KICKOFF_DATE, 'yyyy-mm-dd'), 'yyyy-mm-dd') between to_date('" + kickoffstr_start + "', 'yyyy-mm-dd') and to_date('" + kickoffstr_end + "', 'yyyy-mm-dd') AND T.PIPETYPE='" + this.PipeDgv.Rows[CurRowIndex].Cells["型号"].Value.ToString() + "' and T.PIPELENGTH=" + this.PipeDgv.Rows[CurRowIndex].Cells["长度(mm)"].Value.ToString() + "ORDER BY T.INVENTORY_ID ";

					DataSet TmpViewTable = new DataSet();
					OrclViewAdapter.SelectCommand = OrclNestCmd;
					OrclViewAdapter.Fill(TmpViewTable);

					DataTable NestViewDataTable = new DataTable();
					NestViewDataTable.Columns.Add("母材临时编码", typeof(String));
					NestViewDataTable.Columns.Add("ERP编码", typeof(String));
					NestViewDataTable.Columns.Add("名称", typeof(String));
					NestViewDataTable.Columns.Add("型号", typeof(String));
					NestViewDataTable.Columns.Add("长度(mm)", typeof(Int32));
					NestViewDataTable.Columns.Add("小票号", typeof(String));
					NestViewDataTable.Columns.Add("零件号", typeof(String));
					NestViewDataTable.Columns.Add("零件长度", typeof(Int32));
					NestViewDataTable.Columns.Add("重量", typeof(Double));
					NestViewDataTable.Columns.Add("数量(根)", typeof(Int32));
					for (int i = 0; i < TmpViewTable.Tables[0].Rows.Count; i++)
					{
						DataRow NestViewDataRow = NestViewDataTable.NewRow();
						NestViewDataRow["母材临时编码"] = TmpViewTable.Tables[0].Rows[i]["母材临时编码"].ToString();
						NestViewDataRow["ERP编码"] = TmpViewTable.Tables[0].Rows[i]["ERP编码"].ToString();
						switch (TmpViewTable.Tables[0].Rows[i]["型号"].ToString().Split(' ')[0])
						{
							case "A106B":
								NestViewDataRow["名称"] = "无缝钢管";
								break;
							case "TP316L":
								NestViewDataRow["名称"] = "不锈钢管";
								break;
							default:
								NestViewDataRow["名称"] = "不锈钢管";
								break;
						}
						NestViewDataRow["型号"] = TmpViewTable.Tables[0].Rows[i]["型号"].ToString();
						NestViewDataRow["长度(mm)"] = TmpViewTable.Tables[0].Rows[i]["长度mm"].ToString();
						NestViewDataRow["小票号"] = TmpViewTable.Tables[0].Rows[i]["小票号"].ToString();
						NestViewDataRow["零件号"] = TmpViewTable.Tables[0].Rows[i]["零件号"].ToString();
						NestViewDataRow["零件长度"] = TmpViewTable.Tables[0].Rows[i]["零件长度"].ToString();
						NestViewDataRow["重量"] = TmpViewTable.Tables[0].Rows[i]["重量"];
						NestViewDataRow["数量(根)"] = 1;
						NestViewDataTable.Rows.Add(NestViewDataRow);
					}
					this.dgv_NestedPipes.DataSource = NestViewDataTable;
					this.dgv_NestedPipes.AutoResizeColumns();
					this.dgv_NestedPipes.AutoResizeRows();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "请联系管理员");
				}
            }
        }

		private void PipeDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
    }


    //管路母材类 
    public class PipeBase
    {
		public double weight = 0.0;
		public string ERPCODE;
        public Int32 ID { get; set; }//仓库管路编码
        public string PipeType { get; set; }//管路的类型(外径*壁厚，材质编码等)
        public Int32 Length { get; set; }//现长度
        public Int32 OriginalLength { get; set; }//原长度
        public Int32 state = 1;//现状态，默认为“库存”
        public List<Int32> ContainsID = new List<Int32>();//已套进的管路零件id
        public List<string> ContainsSpoolName = new List<string>();//已套进的管路零件小票号
        public List<PipePart> ContainsPipe = new List<PipePart>();//已套进的管路零件
    }

    //为统计方便，加入母材个数
    public class CalPipeBase
    {
		public double weight = 0.0;
		public string ERPCODE;
        public string PipeType { get; set; }//管路的类型(外径*壁厚，材质编码等)        
        public Int32 OriginalLength { get; set; }//原长度
        public Int32 ToltalNum = 0;
        public List<PipePart> ContainsPipe = new List<PipePart>();//已套进的管路零件        
    }

    //管路零件类
    public class PipePart
    {
		public double weight = 0.0;
		public string kickoff_date=string.Empty;
		public string ERPCODE;
        public string SpoolName = string.Empty;//小票号
		public string PartName = string.Empty;//零件号
        public Int32 ID { get; set; }//零件编码
        public string PipeType { get; set; }//管路的类型(外径*壁厚，材质编码等)
        public Int32 Length { get; set; }//现长度
        public Boolean Nested = false;//是否被套料
        public Int32 NestedBasePipe = -1;//被套进的母材id
		public string Dispatch_date = string.Empty;
    }
}