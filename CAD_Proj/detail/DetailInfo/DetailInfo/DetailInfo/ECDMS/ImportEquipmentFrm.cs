using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Equipment.BatchImport.Core;
using Equipment.BatchImport;
using System.Text.RegularExpressions;

namespace DetailInfo
{
    public partial class ImportEquipmentFrm : Form
    {


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out   int ID);



        private Microsoft.Office.Interop.Excel.Application m_app = null;
        private Workbooks m_books = null;
        Workbook m_wkbook = null;
        Worksheet m_wksheet = null;


        private _Workbook m_book = null;
        private Sheets m_sheets = null;
        private _Worksheet m_sheet = null;


        private Range m_range = null;
        private object m_obj = System.Reflection.Missing.Value;

        Dictionary<string, int> dic = null;


        public ImportEquipmentFrm()
        {
            EquiDataAccess.EquipOIDSConnStr = DataAccess.OIDSConnStr;
           
            InitializeComponent();

            cbProject.EmptyTextTip = "请选择项目";
            cbProject.EmptyTextTipColor = Color.Indigo;



            BindCombobox<Project>.FillCombo(cbProject);


            tbFilePath.EmptyTextTip = "请选择xls文件的路径";
            tbFilePath.EmptyTextTipColor = Color.Indigo;

            tbCreater.EmptyTextTip = "请输入您的名字(名.姓)";
            tbCreater.EmptyTextTipColor = Color.Indigo;

            

        }

        protected string excelFile = string.Empty;

        private void btnGo_Click(object sender, EventArgs e)
        {
            tbInfo.Text = "输出信息";
            excelFile = tbFilePath.Text.Trim();

            if (excelFile == string.Empty)
            {
                MessageBox.Show("请先选择要导入的设备文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SelectFile();
                return;
            }

            if (!File.Exists(excelFile))
            {
                MessageBox.Show("选择的文件不存在，请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SelectFile();
                return;
            }



            int pid = Project.FindId(cbProject.Text);
            if (pid > 0)
            {
                // ReadXlsData(pid.ToString(), tbCreater.Text.Trim(), excelFile);
                // MessageBox.Show(pid.ToString());

                if (User.Exist(tbCreater.Text.Trim().ToLower()))
                {
                    GetListDataHanlder getList = new GetListDataHanlder(ReadXlsData);
                    getList.BeginInvoke(pid.ToString(), tbCreater.Text.Trim().ToLower(), excelFile, null, null);
                    //  ReadXlsData(cbProject.SelectedValue.ToString()  , tbCreater.Text.Trim(),excelFile );
                }
                else
                {
                    MessageBox.Show("您的名字在ECDM系统中查询不到", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }
            }
            else
            { MessageBox.Show("请选择正确的项目", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


        }

        public delegate void  GetListDataHanlder(string sp,string suser,string sfilename);


        public delegate void DisplayInfoHanlder(string sinfo,string scale,int step );

        public void Disp(string sinfo,string  scale,int step)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DisplayInfoHanlder(Disp), sinfo,scale,step );
                return;
            }
            label3.Text = sinfo + scale ;
            progressBar1.Value = step ;
        
        }


  
        private void close()
        {

            m_app.DisplayAlerts = false;
            m_app.AlertBeforeOverwriting = false;
            m_app.Quit();

            IntPtr t = new IntPtr(m_app.Hwnd);
            int k = 0;
            GetWindowThreadProcessId(t, out   k);
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
            p.Kill();
            m_app = null;
            m_book = null; m_books = null;

            ReleaseObj(m_book);
            ReleaseObj(m_books);
            ReleaseObj(m_sheet);
            ReleaseObj(m_sheets);
            ReleaseObj(m_app);
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();



        }

        private void ReleaseObj(object o)
        {
            try { System.Runtime.InteropServices.Marshal.ReleaseComObject(o); }
            catch { }
            finally { o = null; }
        }


        delegate void SetInfoTextCallBack(string info);
        public  void SetInfoText(string info)
        {
            if (tbInfo.InvokeRequired)
            {
                
                SetInfoTextCallBack d = new SetInfoTextCallBack(SetInfoText);
                this.Invoke(d, new object[] { info });
            }
            else
            {
                tbInfo.Text += "\r\n" + info;
            }
        }


        public void ReadXlsData(string sProject, string sUser, string filename)
        {
            //DisplayInfoHanlder displayinfo = new DisplayInfoHanlder(Disp);
            //this.Invoke(displayinfo, new object[] { "准备提取数据 ", "",0 });

        
            Disp("准备提取数据  ", "", 0);

            m_app = new Microsoft.Office.Interop.Excel.Application();
            m_app.UserControl = true;//干什么用的？
            m_app.Visible = false; m_app.DisplayAlerts = false;


            m_wkbook = m_app.Workbooks.Add(filename);
            m_wksheet = m_wkbook.Worksheets.get_Item(1) as Worksheet;
            m_range = m_wksheet.UsedRange;

            int count = m_range.Rows.Count;



            string projectId = sProject;

            List<ProjectBlock> blockList = ProjectBlock.FindAll(projectId);
            List<ProjectSystem> systemList = ProjectSystem.FindAll(int.Parse(projectId));
            List<ProjectDeck> deckList = ProjectDeck.FindAll(projectId);
            List<ProjectRoom> roomList = ProjectRoom.FindAll(projectId);
            List<ProjectZone> zoneList = ProjectZone.FindAll(projectId);
          //  List<string> taglist = Device.FindAllTagNo(projectId);

            string blockName="", upblockName=""; ProjectBlock pbTmp = null, pubTmp = null;
            string systemName=""; ProjectSystem psTmp = null;
            string deckName=""; ProjectDeck pdTmp = null;
            string roomName=""; ProjectRoom prTmp = null;
            string zoneName=""; ProjectZone pzTmp = null;


            string pdName1 = ""; Equipment.BatchImport.Core.ProjectDrawing pdTmp1 = null;
            string pdName2 = ""; Equipment.BatchImport.Core.ProjectDrawing pdTmp2 = null;
            string spack = ""; DevicePackage DevicePackageTem = null;
            List<DevicePackage> devicePackList = DevicePackage.FindAll(int.Parse(projectId));
            List< Equipment.BatchImport.Core.ProjectDrawing> pdrawingList = Equipment.BatchImport.Core.ProjectDrawing.FindAll(projectId);
            List<DeviceProdReqDpt> DvProDepList = DeviceProdReqDpt.FindAll();
            List<DeviceDiscipline> DVDspList = DeviceDiscipline.FindAll(int.Parse(projectId));
            string sProdRequireDept_Name="", sDiscipline_Name="";
            DeviceProdReqDpt cDvProDept = null;
            DeviceDiscipline cDvDs = null;


            
            List<Device> deviceList = new List<Device>();


            List<Device> deviceListadd = new List<Device>();
            List<Device> deviceListupdate = new List<Device>();

            Device d = null;
            System.Diagnostics.Trace.WriteLine(count);


            bool hasError = false, hanErrorAll = false; int errcount = 0;
            string tagno = "", equname = "", equnamec = "", majequ = "", equtype = "";

            string blockIds = string.Empty;  string upblockIds = string.Empty;

            string txtfilename =  System.Windows.Forms.Application.StartupPath+"\\错误信息.txt";
            StreamWriter sw = new StreamWriter(txtfilename, false, Encoding.GetEncoding("gb2312"));
         
            try
            {
                for (int i = 4; i <= count; i++)
                {
                    hasError = false;

                    blockIds = upblockIds = string.Empty;
                    blockName = upblockName = deckName = roomName = zoneName = equname = equnamec = string.Empty;
                   spack = tagno = majequ = equtype=sProdRequireDept_Name = sDiscipline_Name = pdName1 = pdName2 = string.Empty;

                    pbTmp = null;
                    pubTmp = null;
                    psTmp = null;
                     pdTmp = null;
                     prTmp = null;
                     pzTmp = null;
                     pdTmp1 = null;
                     pdTmp2 = null;
                     DevicePackageTem = null;
                     cDvProDept = null;
                     cDvDs = null;


                     if (m_range.get_Range("C" + i, "C" + i).Value2 == null)
                        break;

                    //*****判断是否为空，要运行测试

                    Disp("正在提取数据  ", Math.Round((i - 3.0) / count, 2) * 100.0 + "%", Convert.ToInt32(Math.Round((i - 3.0) / count, 2) * 100));
                    //this.Invoke(displayinfo, new object[] { "正在提取数据", Math.Round((i - 3.0) / count, 2) * 100.0 + "%", Convert.ToInt32(Math.Round((i - 3.0) / count, 2) * 100) });


                    #region  判断数据


                    //EquiName_eng
                    if (m_range.get_Range("C" + i, "C" + i).Value2 != null)
                    {
                        equname = m_range.get_Range("C" + i, "C" + i).Value2.ToString().Trim();
                        if (equname == string.Empty)
                        {
                            SetInfoText("第 " + i + "行数据中[" + equname + "]设备英文名有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + equname + "]设备英文名有误！请修改！");
                            sw.WriteLine("第 " + i + "行数据中[" + equname + "]设备英文名有误！请修改！");
                        }
                    }
                    else
                    {
                        SetInfoText("第 " + i + "行数据中[" + equname + "]设备英文名有误1！请修改！");
                        hasError = true;
                        hanErrorAll = true;
                        System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + equname + "]设备英文名有误！请修改！");
                        sw.WriteLine("第 " + i + "行数据中[" + equname + "]设备英文名有误！请修改！");
                        //   continue;
                    }

                    //EquiName_chn

                    if (m_range.get_Range("D" + i, "D" + i).Value2 != null)
                    {

                        equnamec = m_range.get_Range("D" + i, "D" + i).Value2.ToString().Trim();
                        if (equnamec == string.Empty)
                        {
                            SetInfoText("第 " + i + "行数据中[" + equnamec + "]设备中文名有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + equnamec + "]设备中文名有误！请修改！");
                            sw.WriteLine("第 " + i + "行数据中[" + equnamec + "]设备中文名有误！请修改！");
                        }
                    }
                    else
                    {
                        SetInfoText("第 " + i + "行数据中[" + equnamec + "]设备中文名有误！请修改！");
                        hasError = true;
                        hanErrorAll = true;
                        System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + equnamec + "]设备中文名有误！请修改！");
                        sw.WriteLine("第 " + i + "行数据中[" + equnamec + "]设备中文名有误！请修改！");
                    }


                    //包号  devicePackList
                    if (m_range.get_Range("L" + i, "L" + i).Value2 != null && m_range.get_Range("L" + i, "L" + i).Value2.ToString().Trim() != "")
                    {

                        spack = m_range.get_Range("L" + i, "L" + i).Value2.ToString().Trim();
                        System.Diagnostics.Trace.WriteLine(spack);
                        DevicePackageTem = devicePackList.Find((DevicePackage dp) => dp.PACKAGE_NO.Trim().ToUpper() == spack.ToUpper());
                        if (DevicePackageTem == null)
                        {
                            SetInfoText("第 " + i + "行数据中[" + spack + "]包号有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            sw.WriteLine("第 " + i + "行数据中[" + spack + "]包号有误！请修改！");

                        }



                        //    Core.Device.GetPackageId(Convert.ToInt32(projectId), m_range.get_Range("L" + i, "L" + i).Value2.ToString().Trim());
                        //System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + ipack + "]包号 ");
                        //if (ipack == 0)
                        //{
                        //    SetInfoText("第 " + i + "行数据中[" + pdName1 + "]包号有误！请修改！");
                        //    hasError = true;
                        //    hanErrorAll = true;
                        //    sw.WriteLine("第 " + i + "行数据中[" + pdName1 + "]包号有误！请修改！");

                        //    //  continue;
                        //}

                    }


                    //系统原理图号
                    if (m_range.get_Range("T" + i, "T" + i).Value2 != null && m_range.get_Range("T" + i, "T" + i).Value2.ToString().Trim() != "")
                    {
                        pdName1 = m_range.get_Range("T" + i, "T" + i).Value2.ToString().Trim();
                        pdTmp1 = pdrawingList.Find(( Equipment.BatchImport.Core.ProjectDrawing pdraw) => pdraw.DRAWING_NO.Trim().ToUpper() == pdName1.ToUpper());
                        if (pdTmp1 == null)
                        {
                            SetInfoText("第 " + i + "行数据中[" + pdName1 + "]系统原理图号有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            sw.WriteLine("第 " + i + "行数据中[" + pdName1 + "]系统原理图号有误！请修改！");

                            //  continue;
                        }

                    }


                    //布置图号
                    if (m_range.get_Range("U" + i, "U" + i).Value2 != null && m_range.get_Range("U" + i, "U" + i).Value2.ToString().Trim() != "")
                    {
                        pdName2 = m_range.get_Range("U" + i, "U" + i).Value2.ToString().Trim();
                        pdTmp2 = pdrawingList.Find(( Equipment.BatchImport.Core.ProjectDrawing pdraw) => pdraw.DRAWING_NO.Trim().ToUpper() == pdName2.ToUpper());
                        if (pdTmp2 == null)
                        {
                            SetInfoText("第 " + i + "行数据中[" + pdName2 + "]系统布置图号有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            sw.WriteLine("第 " + i + "行数据中[" + pdName2 + "]系统布置图号有误！请修改！");

                            //  continue;
                        }

                    }
               


                 
                    //生产需求部门
                    if (m_range.get_Range("V" + i, "V" + i).Value2 != null && m_range.get_Range("V" + i, "V" + i).Value2.ToString().Trim() != "")
                    {
                        sProdRequireDept_Name = m_range.get_Range("V" + i, "V" + i).Value2.ToString().Trim();
                      
                        cDvProDept = DvProDepList.Find((DeviceProdReqDpt dvprodept) => dvprodept.DptName.Trim() == sProdRequireDept_Name);
                      
                        if (cDvProDept == null)
                        {
                            SetInfoText("第 " + i + "行数据中[" + sProdRequireDept_Name + "]生产需求部门名有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            sw.WriteLine("第 " + i + "行数据中[" + sProdRequireDept_Name + "]生产需求部门名有误！请修改！");
                        }

                    }
                    //else
                    //{
                    //    SetInfoText("第 " + i + "行数据中[" + sProdRequireDept_Name + "]生产需求部门名有误！请修改！");
                    //    hasError = true;
                    //    hanErrorAll = true;
                    //    sw.WriteLine("第 " + i + "行数据中[" + sProdRequireDept_Name + "]生产需求部门名有误！请修改！");

                    //}
                    //技术专业
                    if (m_range.get_Range("W" + i, "W" + i).Value2 != null && m_range.get_Range("W" + i, "W" + i).Value2.ToString().Trim() != "")
                    {
                        sDiscipline_Name = m_range.get_Range("W" + i, "W" + i).Value2.ToString().Trim();
                        cDvDs = DVDspList.Find((DeviceDiscipline dvdisp) => dvdisp.CODE.Trim() == sDiscipline_Name);
                        if (cDvDs == null)
                        {
                            SetInfoText("第 " + i + "行数据中[" + sDiscipline_Name + "]技术专业名有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            sw.WriteLine("第 " + i + "行数据中[" + sDiscipline_Name + "]技术专业名有误！请修改！");

                            //  continue;
                        }

                    }
                    //else
                    //{
                    //    SetInfoText("第 " + i + "行数据中[" + sDiscipline_Name + "]技术专业名有误！请修改！");
                    //    hasError = true;
                    //    hanErrorAll = true;
                    //    sw.WriteLine("第 " + i + "行数据中[" + sDiscipline_Name + "]技术专业名有误！请修改！");
                    //}


                    //System
                    if (m_range.get_Range("AC" + i, "AC" + i).Value2 != null && m_range.get_Range("AC" + i, "AC" + i).Value2.ToString().Trim() != "")
                    {
                        systemName = m_range.get_Range("AC" + i, "AC" + i).Value2.ToString().Trim();
                        //systemName = systemName.Substring(5, systemName.Length - 5);
                        System.Diagnostics.Trace.WriteLine(systemName);
                        psTmp = systemList.Find(delegate(ProjectSystem ps) { return ps.Code.Trim()==systemName; });
                        if (psTmp == null)
                        {
                            SetInfoText("第 " + i + "行数据中[" + systemName + "]子系统名有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + systemName + "]子系统名有误！请修改！");
                            sw.WriteLine("第 " + i + "行数据中[" + systemName + "]子系统名有误！请修改！");
                            //   continue;
                        }

                    }
                    //else
                    //{
                    //    SetInfoText("第 " + i + "行数据中[" + systemName + "]子系统名有误！请修改！");
                    //    hasError = true;
                    //    hanErrorAll = true; sw.WriteLine("第 " + i + "行数据中[" + systemName + "]子系统名有误！请修改！");
                    //}

                    System.Uri u;
                    
                 

                    //Block 
                    if (m_range.get_Range("AD" + i, "AD" + i).Value2 != null && m_range.get_Range("AD" + i, "AD" + i).Value2.ToString().Trim().ToLower() != "")
                    {
                        blockName = m_range.get_Range("AD" + i, "AD" + i).Value2.ToString().Trim().ToLower();
                       
                        foreach (string block in blockName.Split(','))
                        {


                            pbTmp = blockList.Find(delegate(ProjectBlock pb) { return pb.Description.Trim().ToLower() == block.Trim(); });
                            if (pbTmp == null)
                            {
                                SetInfoText("第 " + i + "行数据中[" + block + "]分段名有误！请修改！");
                                hasError = true;
                                hanErrorAll = true;
                                System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + block + "]分段名有误！请修改！");
                                sw.WriteLine("第 " + i + "行数据中[" + block + "]分段名有误！请修改！");

                            }
                            else
                                blockIds += "," + pbTmp.Block_Id.ToString();
                        }
                        if (blockIds.StartsWith(",")) blockIds = blockIds.Substring(1);

                    }

                    //Upper Block
                    if (m_range.get_Range("AE" + i, "AE" + i).Value2 != null && m_range.get_Range("AE" + i, "AE" + i).Value2.ToString().Trim().ToLower() != "")
                    {
                        upblockName = m_range.get_Range("AE" + i, "AE" + i).Value2.ToString().Trim().ToLower();

                        foreach (string block in upblockName.Split(','))
                        {

                            pubTmp = blockList.Find(delegate(ProjectBlock pb) { return pb.Description.Trim().ToLower() == block.Trim(); });
                            if (pubTmp == null)
                            {
                                SetInfoText("第 " + i + "行数据中[" + block + "]上分段名有误！请修改！");
                                hasError = true;
                                hanErrorAll = true;
                                System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + block + "]上分段名有误！请修改！");
                                sw.WriteLine("第 " + i + "行数据中[" + block + "]上分段名有误！请修改！");
                            }
                            else
                                upblockIds += "," + pubTmp.Block_Id.ToString();
                        }
                        if (upblockIds.StartsWith(",")) upblockIds = upblockIds.Substring(1);
                    }

                    //Deck 
                    if (m_range.get_Range("AF" + i, "AF" + i).Value2 != null && m_range.get_Range("AF" + i, "AF" + i).Value2.ToString().Trim() != "")
                    {
                        deckName = m_range.get_Range("AF" + i, "AF" + i).Value2.ToString().Trim();
                        pdTmp = deckList.Find(delegate(ProjectDeck pd) { return pd.Description.Trim().ToLower() == deckName.ToLower(); });
                        if (pdTmp == null)
                        {
                            //  excelRange.get_Range("P" + i, "P" + i).Value2 = "未找到Zone[" + zoneName + "]";
                            SetInfoText("第 " + i + "行数据中[" + deckName + "]甲板名有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + deckName + "]甲板名有误！请修改！");
                            sw.WriteLine("第 " + i + "行数据中[" + deckName + "]甲板名有误！请修改！");
                            // continue;
                        }

                    }
                   // else
                    //{
                        
                        //SetInfoText("第 " + i + "行数据中[" + deckName + "]甲板名有误！请修改！");
                        //hasError = true;
                        //hanErrorAll = true; sw.WriteLine("第 " + i + "行数据中[" + deckName + "]甲板名有误！请修改！");
                   // }


                    //Room
                    if (m_range.get_Range("AG" + i, "AG" + i).Value2 != null && m_range.get_Range("AG" + i, "AG" + i).Value2.ToString().Trim().ToLower() != "")
                    {

                        roomName = m_range.get_Range("AG" + i, "AG" + i).Value2.ToString().Trim().ToLower();


                        prTmp = roomList.Find(delegate(ProjectRoom pr) { return pr.Room_Key.Trim().ToLower() == roomName; });
                        if (prTmp == null)
                        {
                            SetInfoText("第 " + i + "行数据中[" + roomName + "]房间名有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + roomName + "]房间名有误！请修改！");
                            sw.WriteLine("第 " + i + "行数据中[" + roomName + "]房间名有误！请修改！");
                            //sw.WriteLine(roomName);
                        }

                    }
                    //else
                    //{
                    //    SetInfoText("第 " + i + "行数据中[" + roomName + "]房间名有误！请修改！");
                    //    hasError = true;
                    //    hanErrorAll = true; sw.WriteLine("第 " + i + "行数据中[" + roomName + "]房间名有误！请修改！");
                    //}

                    //Zone //
                    if (m_range.get_Range("AH" + i, "AH" + i).Value2 != null && m_range.get_Range("AH" + i, "AH" + i).Value2.ToString().Trim() != "")
                    {
                        zoneName = m_range.get_Range("AH" + i, "AH" + i).Value2.ToString().Trim();
                        pzTmp = zoneList.Find(delegate(ProjectZone pz) { return pz.Description.Trim().ToLower() == zoneName.ToLower(); });
                        if (pzTmp == null)
                        {
                            SetInfoText("第 " + i + "行数据中[" + zoneName + "]区域名有误！请修改！");
                            hasError = true;
                            hanErrorAll = true;
                            System.Diagnostics.Trace.WriteLine("第 " + i + "行数据中[" + zoneName + "]区域名有误！请修改！");
                            sw.WriteLine("第 " + i + "行数据中[" + zoneName + "]区域名有误！请修改！");
                            //   continue;
                        }

                    }
                    //else
                    //{
                    //    SetInfoText("第 " + i + "行数据中[" + zoneName + "]区域名有误！请修改！");
                    //    hasError = true;
                    //    hanErrorAll = true; sw.WriteLine("第 " + i + "行数据中[" + zoneName + "]区域名有误！请修改！");
                    //}

                    //主设备
                    if (m_range.get_Range("AL" + i, "AL" + i).Value2 != null)
                    {
                        majequ = m_range.get_Range("AL" + i, "AL" + i).Value2.ToString().Trim().ToUpper();
                        if (majequ == "N") majequ = "F";
                        else if (majequ == "Y") majequ = "M";
                    }
                    else
                    {
                        majequ = "";
                    }



                    #endregion
                    string des1, des2, tec1, tec2;
                 
                    #region //将数据加到列表中

                    if (!hasError)
                    {
                        d = new Device();

                        d.Project_Id = projectId.ToString();
                        tagno = m_range.get_Range("B" + i, "B" + i).Value2 == null ? "" : m_range.get_Range("B" + i, "B" + i).Value2.ToString().Trim().ToUpper();
                        d.Tag_No = tagno;
                        d.Equipment = equname;
                        d.EQ_NAME_CN = equnamec;
                        d.DUTYORSTANDBY = (m_range.get_Range("E" + i, "E" + i).Value2 == null) ? string.Empty : m_range.get_Range("E" + i, "E" + i).Value2.ToString().Trim();//Duty或备用
                        d.Power = (m_range.get_Range("F" + i, "F" + i).Value2 == null) ? string.Empty : m_range.get_Range("F" + i, "F" + i).Value2.ToString().Trim();//功率
                        d.Power_Factor = (m_range.get_Range("G" + i, "G" + i).Value2 == null) ? string.Empty : m_range.get_Range("G" + i, "G" + i).Value2.ToString().Trim();//功率因数
                        des1 = (m_range.get_Range("H" + i, "H" + i).Value2 == null) ? string.Empty : m_range.get_Range("H" + i, "H" + i).Value2.ToString().Trim();
                        des2 = (m_range.get_Range("I" + i, "I" + i).Value2 == null) ? string.Empty : m_range.get_Range("I" + i, "I" + i).Value2.ToString().Trim();
                        d.DETAIL_DESCRIPTION = des1 + "###" + des2 == "###" ? "" : des1 + "###" + des2;//详细描述
                        tec1 = (m_range.get_Range("J" + i, "J" + i).Value2 == null) ? string.Empty : m_range.get_Range("J" + i, "J" + i).Value2.ToString().Trim();
                        tec2 = (m_range.get_Range("K" + i, "K" + i).Value2 == null) ? string.Empty : m_range.get_Range("K" + i, "K" + i).Value2.ToString().Trim();
                        d.Remark = tec1 + "###" + tec2 == "###" ? "" : tec1 + "###" + tec2;//技术参数


                       //L
                        d.PackageId = DevicePackageTem == null ? 0 : DevicePackageTem.ID;
                        //if ((m_range.get_Range("L" + i, "L" + i).Value2 != null))
                        //{
                           
                        // // d.PackageId = m_range.get_Range("L" + i, "L" + i).Value2.ToString().Trim() == "" ? 0 : Core.Device.GetPackageId(Convert.ToInt32(projectId), m_range.get_Range("L" + i, "L" + i).Value2.ToString().Trim());
                        //}


                        d.ERP_CODE = (m_range.get_Range("M" + i, "M" + i).Value2 == null) ? string.Empty : m_range.get_Range("M" + i, "M" + i).Value2.ToString().Trim();//物资编码
                        d.MEO_NO = m_range.get_Range("N" + i, "N" + i).Value2 == null ? string.Empty : m_range.get_Range("N" + i, "N" + i).Value2.ToString().Trim();//MEO
                        d.MSS_NO = m_range.get_Range("O" + i, "O" + i).Value2 == null ? string.Empty : m_range.get_Range("O" + i, "O" + i).Value2.ToString().Trim(); ;//MSS NO.
                        d.Maker = m_range.get_Range("P" + i, "P" + i).Value2 == null ? string.Empty : m_range.get_Range("P" + i, "P" + i).Value2.ToString().Trim();//生产厂家
                        d.Contractor = m_range.get_Range("Q" + i, "Q" + i).Value2 == null ? string.Empty : m_range.get_Range("Q" + i, "Q" + i).Value2.ToString().Trim();//合同方名称
                        d.InfoStatusD = m_range.get_Range("R" + i, "R" + i).Value2 == null ? string.Empty : m_range.get_Range("R" + i, "R" + i).Value2.ToString().Trim();
                        d.InfoStatusF = m_range.get_Range("S" + i, "S" + i).Value2 == null ? string.Empty : m_range.get_Range("S" + i, "S" + i).Value2.ToString().Trim();

                        d.FoundationFlag = m_range.get_Range("X" + i, "X" + i).Value2 == null ? string.Empty : m_range.get_Range("X" + i, "X" + i).Value2.ToString().Trim(); //是否需要基座
                        d.CoamingFlag = m_range.get_Range("Y" + i, "Y" + i).Value2 == null ? string.Empty : m_range.get_Range("Y" + i, "Y" + i).Value2.ToString().Trim();//是否需要接水/油盘
                        d.Ofe_Bfe = m_range.get_Range("Z" + i, "Z" + i).Value2 == null ? string.Empty : m_range.get_Range("Z" + i, "Z" + i).Value2.ToString().Trim();

                        d.StoreMaintainFlag = m_range.get_Range("AA" + i, "AA" + i).Value2 == null ? string.Empty : m_range.get_Range("AA" + i, "AA" + i).Value2.ToString().Trim();
                        d.CableConectFlag = m_range.get_Range("AB" + i, "AB" + i).Value2 == null ? string.Empty : m_range.get_Range("AB" + i, "AB" + i).Value2.ToString().Trim();





                        
                        d.ProdRequireDept = cDvProDept==null?0:cDvProDept.Id;
                        d.Discipline_Id =cDvDs==null?0: cDvDs.Id;

                        d.System_Id = psTmp == null ? 0 : psTmp.System_Id;
                        d.Deck_Id = deckName==string.Empty?0: pdTmp.Deck_Id;
                        d.Room_Id = roomName==string.Empty ?0: prTmp.Room_ID;
                        d.Zone_Id = zoneName ==string.Empty?0: pzTmp.Zone_Id;



                        d.Dry_Weight = m_range.get_Range("AI" + i, "AI" + i).Value2 == null ? string.Empty : m_range.get_Range("AI" + i, "AI" + i).Value2.ToString().Trim();
                        d.Operating_weight = m_range.get_Range("AJ" + i, "AJ" + i).Value2 == null ? string.Empty : m_range.get_Range("AJ" + i, "AJ" + i).Value2.ToString().Trim();

                        d.OVERALL_DIMENSION = m_range.get_Range("AK" + i, "AK" + i).Value2 == null ? string.Empty : m_range.get_Range("AK" + i, "AK" + i).Value2.ToString().Trim();

                        d.MajorFlag = majequ;//AL


                        d.Creater = tbCreater.Text.Trim();// "shihai.liu";
                        
                        d.NewFlag = "Y";
                        d.DeleteFlag = "N";

                        d.EquipType = equtype;

               

                        d.newBlock = blockIds;// pbTmp.Block_Id;
                        d.UPPERFLAG_NO = "N";

                        d.UpperBlock = upblockIds;// pubTmp.Block_Id;
                        d.UPPERFLAG_YES = "Y";
                        
                        //原理图和布置图
                        d.PIDDrawNo =pdTmp1==null?0: pdTmp1.DRAWING_ID;
                        d.ArrangementDrawNo =pdTmp2==null?0: pdTmp2.DRAWING_ID;
                   
                        

                         deviceList.Add(d);


            

                    }
                    else
                        errcount++;
                    #endregion
                }



                Disp("提取数据完成  ", "100%", 100);
                //区分添加和更新
                //取出所有设备ID,TAGNO
                Disp("正在转换数据  ", "", 0);
                dic = DeviceTag .GetDeviceTag(projectId);
                
                int tagcount = deviceList.Count;
                double  tagnum = 0;
                foreach (Device de in deviceList)
                {
                    tagnum++;
                    Disp("正在转换数据  ", Math.Round((tagnum) / tagcount, 2) * 100.0 + "%", Convert.ToInt32(Math.Round((tagnum) / tagcount, 2) * 100));
                 
                    if (de.Tag_No == "")
                        deviceListadd.Add(de);
                    else
                    {

                        if (dic.ContainsKey(de.Tag_No))
                        {
                            de.Device_Id = dic[de.Tag_No];
                            de.Updater = tbCreater.Text.Trim().ToLower();
                   //         if (deviceListupdate.Exists(delegate(Device dd) { return dd.Tag_No == de.Tag_No; }))
                            if (deviceListupdate.Exists((dd) =>  dd.Tag_No == de.Tag_No ))
                            {
                                SetInfoText("Tag_No为[" + de.Tag_No + "]的设备有重复！请修改！");
                                hasError = true;
                                hanErrorAll = true;
                                System.Diagnostics.Trace.WriteLine("Tag_No为[" + de.Tag_No + "]的设备有重复！请修改！");
                                sw.WriteLine("Tag_No为[" + de.Tag_No + "]的设备有重复！请修改！");
                            }
                            else
                                deviceListupdate.Add(de);
                        }
                        else
                        {
                            if (deviceListadd.Exists(  dd=>  dd.Tag_No == de.Tag_No))
                            {
                                SetInfoText("Tag_No为[" + de.Tag_No + "]的设备有重复！请修改！");
                                hasError = true;
                                hanErrorAll = true;
                                System.Diagnostics.Trace.WriteLine("Tag_No为[" + de.Tag_No + "]的设备有重复！请修改！");
                                sw.WriteLine("Tag_No为[" + de.Tag_No + "]的设备有重复！请修改！");
                            }
                            else
                                deviceListadd.Add(de);
                        }
                          

                    }

                }

              //  this.Invoke(displayinfo, new object[] { "准备导入数据库 ", "", 0 });

                Disp("准备导入数据库 ", "", 0);
                System.Diagnostics.Trace.WriteLine(deviceListadd.Count);
                System.Diagnostics.Trace.WriteLine(deviceListupdate.Count);

          
            }

            catch (Exception et) { MessageBox.Show(et.Message); }


            finally
            {
                if (hanErrorAll == true)
                {
                    if (errcount > 10)
                     MessageBox.Show("错误数据太多，程序已暂时结束，请修改后再进行此操作！", "Import Equipment Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    MessageBox.Show("数据有错误，请修改后再进行此操作！", "Import Equipment Information", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                else
                {
                    if (deviceListadd.Count > 0)//添加新的设备纪录
                    {
                        
                        //Device.md = new Device.mydel(this.Disp);

                        //if (Device.Add(deviceListadd))
                        //{

                        //    SetInfoText("成功导入成功了" + deviceListadd.Count + "个设备信息！");
                        //    System.Diagnostics.Trace.WriteLine("成功导入成功了" + deviceListadd.Count + "个设备信息！");
                        //}
                        //else
                        //{
                        //    SetInfoText("导入出现错误！");
                        //    System.Diagnostics.Trace.WriteLine("导入出现错误！");


                        //}

                    }

                    if (deviceListupdate.Count > 0)//更新设备纪录
                    {
                        //Device.md = new Device.mydel(this.Disp);

                        //if (Device.Update(deviceListupdate))
                        //{
                        //    SetInfoText("成功更新成功了" + deviceListupdate.Count + "个设备信息！");
                        //    System.Diagnostics.Trace.WriteLine("成功更新成功了" + deviceListupdate.Count + "个设备信息！");
                        //}
                        //else
                        //{
                        //    SetInfoText("更新出现错误！");
                        //    System.Diagnostics.Trace.WriteLine("更新导入出现错误！");

                        //}
                    }
                }

                Disp("程序运行完毕 ", "", 0);

                string s = string.Format("符合数据{0}条，不匹配数据{1}条", deviceListadd.Count + deviceListupdate.Count , errcount);
                System.Diagnostics.Trace.WriteLine(s);
                SetInfoText(s);

                sw.Close();
                close();

            }

        }

 
   
 



        private void btnBrowser_Click(object sender, EventArgs e)
        {
            
            SelectFile();

        }

        public void SelectFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "打开(Open)";
            dlg.FileName = "";
            dlg.Filter = "Excel文件(*.xls)|*.xls";
            dlg.ValidateNames = true;
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;

            if (dlg.ShowDialog() == DialogResult.OK)

                tbFilePath.Text = dlg.FileName;

            dlg.Dispose();
        }

        private void ImportEquipmentFrm_Load(object sender, EventArgs e)
        {

           
                
        }

      

  

  




    }
}


　　//    　　