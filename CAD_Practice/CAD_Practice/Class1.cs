using System;
using System.Collections.Generic;
using System.Text;

using AcadApp= Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Colors;

using System.Configuration;
//acdbmgd.dll包含ObjectDBX托管类 这个对象中的类将被用来访问和编辑AutoCAD图形中的实体，
//而acmgd.dll包含AutoCAD托管类。
//这两个文件包含了.NET API中所有的外包类。
using System.Management;
using System.IO;
using System.Threading;
using UpdateSoft;
using System.Windows.Forms;
using Microsoft.Win32;
using AdeskInter = Autodesk.AutoCAD.Interop;



namespace CAD_Practice
{

    public class Initializa : IExtensionApplication //初始化代码类
    {
        public void Initialize()
        {
            
            Editor ed = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("初始化....");
           // Commands.test0817();
        }
        public void Terminate()
        {
            System.Diagnostics.Trace.WriteLine("Cleaning up...");
        }

    }


    public class Practice
    {
        [CommandMethod("hello")]//要加入能在AutoCAD 中调用的命令,你必须使用“CommandMethod”属性。
        public static void test0817()
        {
            Editor ed = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Hello AutoCAD!!");

            Runmain();

            NewForm nf = new NewForm();
            nf.Show();

        }

        //C:\Program Files\YCRO\Digital 更新路径
        public static  void Runmain()
        {

            bool bCreatedNew;
            Mutex m = new Mutex(false, "myUniqueName", out bCreatedNew);



            RegistryKey YCRO = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Autodesk\AutoCAD\R16.2\ACAD-4001:409\Applications\YRO_Digital");

            object path=  YCRO.GetValue("LOADER");

            SoftUpdate app = new SoftUpdate(path.ToString(), "UpdateProgram.zip");

            app.UpdateFinish += new UpdateState(app_UpdateFinish);

            if (app.IsUpdate)
            {


                System.Diagnostics.Process.Start(Path.GetDirectoryName(path.ToString()) + "\\" + "UpdateSoftProgram.exe");
                Application.Exit();
                closeapp();

            }

            else
            {
                if (bCreatedNew)
                {
                   
                    //try
                    //{
                    //    ////处理未捕获的异常   
                    //    //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    //    ////处理UI线程异常   
                    //    //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                    //    ////处理非UI线程异常   
                    //    //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                    //    Application.EnableVisualStyles();
                    //    Application.SetCompatibleTextRenderingDefault(false);
                    //    //LoginForm lform = new LoginForm();
                    //    //if (lform.ShowDialog() == DialogResult.OK)
                    //    //{
                    //    //    string sqlStr = string.Empty;
                    //    //    string user_name = lform.LoginUserName;
                    //    //    User.Get_CurrentUser(user_name);
                    //    //    Application.Run(new MDIForm());
                    //    //}
                    //}
                    //catch (System.Exception ex)
                    //{
                    //    string str = "";
                    //    string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";

                    //    if (ex != null)
                    //    {
                    //        str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                    //             ex.GetType().Name, ex.Message, ex.StackTrace);
                    //    }
                    //    else
                    //    {
                    //        str = string.Format("应用程序线程错误:{0}", ex);
                    //    }


                    //    writeLog(str);
                    //    MessageBox.Show(str);
                    //    //MessageBox.Show("发生致命错误，请及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    Application.Exit();

                    //}
                }
            }
        }

        public  static  void closeapp()
        {
            string progID = "AutoCAD.Application";
            AdeskInter.AcadApplication CADAPP = null;
            try
            {
                CADAPP = (AdeskInter.AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject(progID);
                CADAPP.Quit();
                
            }
            catch
            {
                try
                {
                    Type SType = Type.GetTypeFromProgID(progID);
                    CADAPP = (AdeskInter.AcadApplication)System.Activator.CreateInstance(SType, true);
                    CADAPP.Quit();
                }
                catch
                {

                }
            }
        }

        static void app_UpdateFinish()
        {
            //MessageBox.Show("更新完成，请重新启动程序！");
        }

        static void writeLog(string str)
        {
            //string pathstr = Path.Combine(User.rootpath, "\\ErrLog");
            //string pathstr = User.rootpath + "\\" + "ErrLog";
            //if (!Directory.Exists(pathstr))
            //{
            //    Directory.CreateDirectory(pathstr);
            //}

            //using (StreamWriter sw = new StreamWriter(pathstr + "\\" + "ErrLog.txt", true))
            //{
            //    sw.WriteLine(str);
            //    sw.WriteLine("---------------------------------------------------------");
            //    sw.Close();
            //}
        }



        // 创建一条直线 
        [CommandMethod("addlc")]
        public void Main()
        {
            ObjectIdCollection objcoll = new ObjectIdCollection();

            //   objcoll= AddLineandcirclr();
            //CreateGroup(objcoll, "ASDK_TEST_GROUP");　　
        }

    }
}



//[assembly: ExtensionApplication(typeof(LDotNetApi.Initializa))]
//[assembly: CommandClass(typeof(LDotNetApi.Commands))]

namespace LDotNetApi
{

    public class Initializa : IExtensionApplication //初始化代码类
    {
        public void Initialize()
        {
            Editor ed = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("初始化....");
           // Commands.test0817();
        }
        public void Terminate()
        {
            System.Diagnostics.Trace.WriteLine("Cleaning up...");
        }
    }


    public class Commands //命令实现类
    {



        public ObjectIdCollection AddLineandcirclr()
        {
            ObjectIdCollection objcoll = new ObjectIdCollection();
            //过程都是先得到数据库，然后依次打开块表、块表记录，接着添加实体，最后关闭块表、块表记录。

            //    Database db = HostApplicationServices.WorkingDatabase;//获得当前工作空间的数据库
            AcadApp.Document doc = AcadApp.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            //  开始事务处理，也就是往CAD中加入东西
            Circle circle;
            ObjectId objcid, objlid;
            using (Transaction tran = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tran.GetObject(db.BlockTableId, OpenMode.ForRead);// 得到块表
                // //  获得AutoCAD块表，AutoCAD将加入到图形中的对象的信息都放在这个表中
                BlockTableRecord btr = (BlockTableRecord)tran.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);//得到模型空间的块表纪录
                //***                 // 使用当前的空间Id来获取块表记录——注意我们是打开它用来写入
                // ***   BlockTableRecord btr = (BlockTableRecord)tran.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);


                Line l = new Line(new Point3d(0, 0, 0), new Point3d(200, 200, 0));

                // 定义一个Circle对象来表示你要生成的圆，传入的第二个参数为圆的法向，就是把圆生成在什么面上，因为AutoCAD程序一般都是平面问题，因此你一般都把这个法向量定义成//z轴方向。

                circle = new Circle(new Point3d(200, 200, 0), new Vector3d(0.0, 0.0, 1.0), 50); // Vector3d.ZAxis


                objlid = btr.AppendEntity(l);//将直线加入到模型空间中    //   向块表记录加入圆的相关信息
                tran.AddNewlyCreatedDBObject(l, true);

                objcoll.Add(objlid);


                objcid = btr.AppendEntity(circle);
                objcoll.Add(objcid);
                tran.AddNewlyCreatedDBObject(circle, true);
                tran.Commit();  //一旦完成以上操作，我们就提交事务处理，这样以上所做的改变就被保存了……

                //然后销毁事务处理，因为我们已经完成了相关的操作（事务处理不是数据库驻留对象，可以销毁）

            }

            System.Diagnostics.Trace.WriteLine(objcid.ToString());

            //面的代码是根据用户在命令行中的选择来改变圆的颜色。
            Editor ed = doc.Editor;
            //Editor ed = Entities.Editor;
            // PromptKeywordOptions定义一个关键字列表选项
            PromptKeywordOptions opt = new PromptKeywordOptions("选择颜色[绿色(G)/蓝色(B)]<红色(R)>");
            //加入关键字列表
            opt.Keywords.Add("R");
            opt.Keywords.Add("G");
            opt.Keywords.Add("B");
            //获取用户输入的关键字
            PromptResult result = ed.GetKeywords(opt);
            //判断是否输入了定义的关键字
            if (result.Status == PromptStatus.OK)
            {
                //根据用户选择的关键字，来改变圆的颜色
                switch (result.StringResult)
                {
                    case "R":
                        // PutColorIndex是ZHFARX库中改变对象颜色的函数

                        ChangeColorIndex(objcoll, 1);
                        break;
                    case "G":
                        ChangeColorIndex(objcoll, 3);
                        break;
                    case "B":
                        ChangeColorIndex(objcoll, 5);
                        break;
                }
            }
            return objcoll;
        }

        public static void ChangeColorIndex(ObjectIdCollection c, int i)
        {
            AcadApp.Document doc = AcadApp.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction tran = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tran.GetObject(db.BlockTableId, OpenMode.ForRead);// 得到块表
                BlockTableRecord btr = (BlockTableRecord)tran.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                foreach (ObjectId obj in c)
                {
                    Entity acEnt = tran.GetObject(obj, OpenMode.ForWrite) as Entity;

                    acEnt.ColorIndex = i;
                }
                tran.Commit();
            }
        }

        public void CreateGroup(ObjectIdCollection objIds, string groupName)
        {
            Group gp = new Group(groupName, true);         //创建名为groupName的组
            Database db = AcadApp.Application.DocumentManager.MdiActiveDocument.Database;
            //获取事务处理管理器，用来对数据库进行操作
            using (Transaction ta = db.TransactionManager.StartTransaction())
            {
                DBDictionary dict = (DBDictionary)ta.GetObject(db.GroupDictionaryId, OpenMode.ForWrite, false);　　//获取组所在的"Group"字典		
                dict.SetAt(groupName, gp);　　//在"Group"字典中加入组对象		
                foreach (ObjectId thisId in objIds)
                {
                    gp.Append(thisId);　　　//在组中加入ObjectId为thisId的实体
                }
                ta.AddNewlyCreatedDBObject(gp, true);
                ta.Commit();
            }
        }

    }

    public class CADNote //提示注释
    {

        [CommandMethod("Getpoint")]
        public void SelectOt()  //"please select a point ,then get the coordinate :"
        {
            PromptPointOptions pmops = new PromptPointOptions("please select a point ,then get the coordinate :");
            
            PromptPointResult pmres;
            Editor ed = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;
            
            pmres = ed.GetPoint(pmops);

            if (pmres.Status != PromptStatus.OK)
                ed.WriteMessage("Error");
            else
            {
                ed.WriteMessage("You selected point " + pmres.Value.ToString());
            }

        }

        [CommandMethod("zhushi")]
        public static void zhushi()
        {

            //1getstsring()

            AcadApp.Document acDoc = AcadApp.Application.DocumentManager.MdiActiveDocument;

            //PromptStringOptions pStrOpts = new PromptStringOptions("\nEnter your name: ");

            //pStrOpts.AllowSpaces = true;//AllowSpaces属性控制提示是否可以输入空格，如果设置为false，按空格键就终止输入。

            //PromptResult pStrRes = acDoc.Editor.GetString(pStrOpts);

            //AcadApp.Application.ShowAlertDialog("The name entered was: " + pStrRes.StringResult);



            //GetPoint方法提示用户在Command提示时指定一个点。PromptPointOptions对象的UseBasePoint属性和BasePoint属性控制是否从基点绘制一条橡皮带线。PromptPointOptions对象的Keywords属性用来定义除了指定点外还可以在Command提示光标处输入的关键字。

            //Database acCurDb = acDoc.Database;

            //PromptPointResult pPtRes;

            //PromptPointOptions pPtOpts = new PromptPointOptions("1");

            //// Prompt for the start point

            //pPtOpts.Message = "\nEnter the start point of the line: ";

            //pPtRes = acDoc.Editor.GetPoint(pPtOpts);

            //Point3d ptStart = pPtRes.Value;

            //// Exit if the user presses ESC or cancels the command

            //if (pPtRes.Status == PromptStatus.Cancel) return;

            //// Prompt for the end point

            //pPtOpts.Message = "\nEnter the end point of the line: ";

            //pPtOpts.UseBasePoint = true;

            //pPtOpts.BasePoint = ptStart;

            //pPtRes = acDoc.Editor.GetPoint(pPtOpts);

            //Point3d ptEnd = pPtRes.Value;

            //if (pPtRes.Status == PromptStatus.Cancel) return;

            //// Start a transaction

            //using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            //{

            //    BlockTable acBlkTbl;

            //    BlockTableRecord acBlkTblRec;

            //    // Open Model space for write

            //    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

            //    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

            //    // Define the new line

            //    Line acLine = new Line(ptStart, ptEnd);

            //    // Add the line to the drawing

            //    acBlkTblRec.AppendEntity(acLine);

            //    acTrans.AddNewlyCreatedDBObject(acLine, true);

            //    // Zoom to the extents or limits of the drawing

            //    acDoc.SendStringToExecute("._zoom _all ", true, false, false);

            //          acTrans.Commit();

            //}


            //GetKeywords方法提示用户在Command提示光标处输入一个关键字。PromptKeywordOptions对象用来控制键入及提示信息呈现方式。PromptKeywordOptions对象的Keywords属性用来定义可在Command提示光标处键入的关键字。
            //从AutoCAD命令行获取用户输入的关键字
            //下例将PromptKeywordOptions对象的AllowNone属性设置为false（不允许直接回车），这样使用户必须输入一个关键字。Keywords属性用来添加允许的合法关键字。

            PromptKeywordOptions pKeyOpts = new PromptKeywordOptions("");

            pKeyOpts.Message = "\nEnter an option ";

            pKeyOpts.Keywords.Add("Line");

            pKeyOpts.Keywords.Add("Circle");

            pKeyOpts.Keywords.Add("Arc");

            //pKeyOpts.AllowNone = false;
            pKeyOpts.Keywords.Default = "Arc";

            pKeyOpts.AllowNone = true;

            PromptResult pKeyRes = acDoc.Editor.GetKeywords(pKeyOpts);

            AcadApp.Application.ShowAlertDialog("Entered keyword: " + pKeyRes.StringResult);

            ////更用户友好的关键字提示方式是如果用户按Enter键（没有输入）时提供一个缺省值。注意下面的这个小小改动


            //4
            //PromptIntegerOptions pIntOpts = new PromptIntegerOptions("");

            //pIntOpts.Message = "\nEnter the size or ";

            //// Restrict input to positive and non-negative values

            ////限制输入必须大于0;

            //pIntOpts.AllowZero = false;

            //pIntOpts.AllowNegative = false;

            //// Define the valid keywords and allow Enter

            ////定义合法关键字并允许直接按Enter键;

            //pIntOpts.Keywords.Add("Big");

            //pIntOpts.Keywords.Add("Small");

            //pIntOpts.Keywords.Add("Regular");

            //pIntOpts.Keywords.Default = "Regular";

            //pIntOpts.AllowNone = true;

            //// Get the value entered by the user

            ////获取用户键入的值

            //PromptIntegerResult pIntRes = acDoc.Editor.GetInteger(pIntOpts);

            //if (pIntRes.Status == PromptStatus.Keyword)
            //{

            //  AcadApp.  Application.ShowAlertDialog("Entered keyword: " + pIntRes.StringResult);

            //}

            //else
            //{

            //    AcadApp.Application.ShowAlertDialog("Entered value: " + pIntRes.Value.ToString());

            //}

            ////PromptKeywordOptions opts=new PromptKeywordOptions("Offset Project");　　　//用户选择的是椭圆，则加入关键字让用户在命令行中进行选择来对椭圆进行投影或平移
            ////opts.Keywords.Add("Project");　　　　　　　　　　//加入关键字"Project"，它表示对椭圆进行投影	
            ////opts.Keywords.Add("Offset");　　　　　　　　　　//加入关键字"Offset"，它表示对椭圆进行平移	
            ////opts.Keywords.Default="Project";　　　　　　　　//设置缺省的关键字为"Project"，当用户直接按空格或回车的话，相当于在命令行中键入了"Project"		
            ////PromptResult resKey=ed.GetKeywords(opts);       //获取用户输入的关键字




        }

    }



    public class CreateCircle
    {
        [CommandMethod("foreach")]
        public void StepthroughBlockrecord()
        {
            AcadApp.Document doc = AcadApp.Application.DocumentManager.MdiActiveDocument;
            Database d = doc.Database;

            using (Transaction t = d.TransactionManager.StartTransaction())
            {

                doc.Editor.WriteMessage(d.TransactionManager.NumberOfActiveTransactions.ToString());//活动事务的个数
                doc.Editor.WriteMessage("\n" + d.TransactionManager.TopTransaction.ToString()); //最上面的或者说最新的事务
                doc.Editor.WriteMessage("\n" + t.ToString()); //最上面的或者说最新的事务
                //在程序执行过程中，为了回滚所作的部分修改，可以将一个事务嵌套在另一个事务中

                BlockTable acbt = t.GetObject(d.BlockTableId, OpenMode.ForRead) as BlockTable;
                //BlockTableRecord acBlkTblRec = t.GetObject(acbt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                BlockTableRecord acBlkTblRec = t.GetObject(d.CurrentSpaceId, OpenMode.ForRead) as BlockTableRecord;

                // Step through the Block table record遍历块表记录
                foreach (ObjectId asObjId in acBlkTblRec)
                {
                    // doc.Editor.WriteMessage("\nDXF name: " + asObjId.ObjectClass.DxfName);
                    doc.Editor.WriteMessage("\nObjectID: " + asObjId.ToString());

                    doc.Editor.WriteMessage("\nHandle: " + asObjId.Handle.ToString());

                    doc.Editor.WriteMessage("\n");
                }
            }

            ////acbt.UpgradeOpen();升降级打开对象
            ////acbt.DowngradeOpen();

            //// 以读的方式打开图层表
            //LayerTable acLyrTbl = acTrans.GetObject(d.LayerTableId, OpenMode.ForRead) as LayerTable;
            ////遍历图层并将图层名以‘Door’开头的图层升级为写打开方式;
            //foreach (ObjectId acObjId in acLyrTbl)
            //{
            //    LayerTableRecord acLyrTblRec = acTrans.GetObject(acObjId, OpenMode.ForRead) as LayerTableRecord;

            //    //检查图层名是否以‘Door’开头

            //    if (acLyrTblRec.Name.StartsWith("Door", StringComparison.OrdinalIgnoreCase) == true)
            //    {

            //        //检查是否为当前层，是则不冻结  // Check to see if the layer is current, if so then do not freeze it

            //        if (acLyrTblRec.ObjectId != d.Clayer)
            //        {
            //            acLyrTblRec.UpgradeOpen();  // Change from read to write mode升级打开方式
            //            acLyrTblRec.IsFrozen = true;    // Freeze the layer冻结图层
            //        }
            //    }
            //}

            //acTrans.Commit(); //提交修改并关闭事务




        }  //遍历块表记录 ++ 升降级打开对象

        //创建一个层
        public ObjectId CreateLayer()
        {


            ObjectId layerId; //它返回函数的值

            Database db = HostApplicationServices.WorkingDatabase;

            Transaction trans = db.TransactionManager.StartTransaction();

            //首先取得层表……

            LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);

            //检查EmployeeLayer层是否存在……


            if (lt.Has("EmployeeLayer"))
            {

                layerId = lt["EmployeeLayer"];
            }
            else
            {

                //如果EmployeeLayer层不存在，就创建它

                LayerTableRecord ltr = new LayerTableRecord();

                ltr.Name = "EmployeeLayer"; //设置层的名字

                ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, 2);

                layerId = lt.Add(ltr);

                trans.AddNewlyCreatedDBObject(ltr, true);

            }

            trans.Commit();

            trans.Dispose();

            return layerId;

        }

        [CommandMethod("trannest")]
        public void transNest()
        {
            #region 事务嵌套

            //创建对事务管理器的引用  
            AcadApp.Document doc = AcadApp.Application.DocumentManager.MdiActiveDocument;
            Database d = AcadApp.Application.DocumentManager.MdiActiveDocument.Database;

            TransactionManager acTransMgr = d.TransactionManager;
            using (Transaction acTrans1 = acTransMgr.StartTransaction())
            {
                //打印当前活动事务的个数     
                doc.Editor.WriteMessage("\nNumber of transactions active: " + acTransMgr.NumberOfActiveTransactions.ToString());

                BlockTable acBlkTbl = acTrans1.GetObject(d.BlockTableId, OpenMode.ForRead) as BlockTable;

                BlockTableRecord acBlkTblRec = acTrans1.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                Circle acCirc = new Circle(); acCirc.Center = new Point3d(5, 5, 0); acCirc.Radius = 3;

                acBlkTblRec.AppendEntity(acCirc);

                acTrans1.AddNewlyCreatedDBObject(acCirc, true);

                //创建第二个事务

                using (Transaction acTrans2 = acTransMgr.StartTransaction())
                {

                    doc.Editor.WriteMessage("\nNumber of transactions active: " + acTransMgr.NumberOfActiveTransactions.ToString());

                    acCirc.ColorIndex = 5;          // Change the circle's color修改圆的颜色

                    Line acLine = new Line(new Point3d(2, 5, 0), new Point3d(10, 7, 0));

                    acLine.ColorIndex = 3;

                    acBlkTblRec.AppendEntity(acLine);

                    acTrans2.AddNewlyCreatedDBObject(acLine, true);

                    //创建第三个事务

                    using (Transaction acTrans3 = acTransMgr.StartTransaction())
                    {

                        doc.Editor.WriteMessage("\nNumber of transactions active: " + acTransMgr.NumberOfActiveTransactions.ToString());

                        acCirc.ColorIndex = 3;

                        // Update the display of the drawing更新图形显示

                        doc.Editor.WriteMessage("\n");

                        // doc.Editor.Regen();

                        //询问保持还是取消第三个事务中的修改   

                        PromptKeywordOptions pKeyOpts = new PromptKeywordOptions("");

                        pKeyOpts.Message = "\nKeep color change ";
                        pKeyOpts.Keywords.Add("Yes"); pKeyOpts.Keywords.Add("No");
                        pKeyOpts.Keywords.Default = "No"; pKeyOpts.AllowNone = true;

                        PromptResult pKeyRes = doc.Editor.GetKeywords(pKeyOpts);

                        if (pKeyRes.StringResult == "No")
                        {
                            //   t.Abort(); // 如果程序出错，我们可以使用Abort方法回滚事务中所作的修改

                            acTrans3.Abort(); //取消事务3中的修改
                        }
                        else
                        {
                            acTrans3.Commit(); //保存事务3中的修改
                        }
                    }

                    doc.Editor.WriteMessage("\nNumber of transactions active: " + acTransMgr.NumberOfActiveTransactions.ToString());

                    acTrans2.Commit();
                }

                doc.Editor.WriteMessage("\nNumber of transactions active: " + acTransMgr.NumberOfActiveTransactions.ToString());

                acTrans1.Commit();      // Keep the changes to transaction 1保留事务1中的修改


            }
            #endregion
        }



        //访问模型空间、图纸空间或当前空间  获得对块表记录的引用后，往里添加一条新直线。
        [CommandMethod("as")]
        public static void AccessSpace()
        {

            AcadApp.Document acDoc = AcadApp.Application.DocumentManager.MdiActiveDocument;

            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                BlockTableRecord acBlkTblRec;

                // Request which table record to open询问打开哪条表记录（空间）

                PromptKeywordOptions pKeyOpts = new PromptKeywordOptions("");

                pKeyOpts.Message = "\nEnter whichspace to create the line in ";

                pKeyOpts.Keywords.Add("Model");

                pKeyOpts.Keywords.Add("Paper");

                pKeyOpts.Keywords.Add("Current");

                pKeyOpts.AllowNone = false;

                pKeyOpts.AppendKeywordsToMessage = true;

                PromptResult pKeyRes = acDoc.Editor.GetKeywords(pKeyOpts);

                if (pKeyRes.StringResult == "Model")                    //从Block表获取Model空间的ObjectID
                {

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                }

                else if (pKeyRes.StringResult == "Paper")              //从Block表获取Paper空间的ObjectID
                {

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.PaperSpace], OpenMode.ForWrite) as BlockTableRecord;
                }
                else
                {
                    //从数据库获取当前空间的ObjectID
                    acBlkTblRec = acTrans.GetObject(acCurDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                }


                ////设置图形中所有点对象的样式

                acCurDb.Pdmode = 34;

                acCurDb.Pdsize = 1;

                //******************************
                // Create an in memory circle在内存创建一个圆

                using (Circle acCirc = new Circle())
                {

                    acCirc.Center = new Point3d(2, 2, 0);

                    acCirc.Radius = 5;

                    // Adds the circle to an object array将圆添加到对象数组

                    DBObjectCollection acDBObjColl = new DBObjectCollection();

                    acDBObjColl.Add(acCirc);

                    // Calculate the regions based oneach closed loop

                    //基于每个闭环计算面域

                    DBObjectCollection myRegionColl = new DBObjectCollection();

                    myRegionColl = Region.CreateFromCurves(acDBObjColl);

                    Region acRegion = myRegionColl[0] as Region;

                    acBlkTblRec.AppendEntity(acRegion);

                    acTrans.AddNewlyCreatedDBObject(acRegion, true);

                    acCirc.ColorIndex = 1;
                    acBlkTblRec.AppendEntity(acCirc);

                    acTrans.AddNewlyCreatedDBObject(acCirc, true);

                    // Dispose of the in memory circlenot appended to the database

                    //处置内存中的圆，不添加到数据库;

                }

                acTrans.Commit();

            }

        }


        #region  选择集操作
        //选择集
        [CommandMethod("sel", CommandFlags.UsePickSet)]
        public static void CheckForPickfirstSelection()
        {

            AcadApp.Document acDoc = AcadApp.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            Editor acDocEd = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;

            //// Get the PickFirst selection set获取PickFirst选择集

            //PromptSelectionResult acSSPrompt;
            //acSSPrompt = acDocEd.SelectImplied();
            //SelectionSet acSSet;

            //if (acSSPrompt.Status == PromptStatus.OK) // 如果提示状态OK，说明启动命令前选择了对象;
            //{
            //    acSSet = acSSPrompt.Value;
            //    AcadApp.Application.ShowAlertDialog("Number of objects in Pickfirst selection: " + acSSet.Count.ToString());
            //}
            //else
            //{
            //    AcadApp.Application.ShowAlertDialog("Number of objects in Pickfirst selection: 0");
            //}

            // 清空选择集????
            //  //提示选择屏幕上的对象并遍历选择集
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                PromptSelectionResult acSSPrompt = acDoc.Editor.GetSelection();// 请求从图形区域选择对象

                // 如果提示状态OK，表示已选择对象
                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    SelectionSet acSSet = acSSPrompt.Value;

                    // 遍历选择集内的对象
                    foreach (SelectedObject acSSObj in acSSet)
                    {
                        // 确认返回的是合法的SelectedObject对象

                        if (acSSObj != null)
                        {
                            // 以写打开所选对象

                            Entity acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForWrite) as Entity;

                            if (acEnt != null)
                            {
                                acEnt.ColorIndex = 3; // 将对象颜色修改为绿色
                            }
                        }
                    } acTrans.Commit();
                }
            }


        }



        //合并选择集
        [CommandMethod("ms")]
        public static void MergeSelectionSets()
        {

            Editor acDocEd = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptSelectionResult acSSPrompt;

            acSSPrompt = acDocEd.GetSelection();

            SelectionSet acSSet1;

            ObjectIdCollection acObjIdColl = new ObjectIdCollection();

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                acSSet1 = acSSPrompt.Value; // 获取所选对象
                acObjIdColl = new ObjectIdCollection(acSSet1.GetObjectIds());// 向ObjectIdCollection中追加所选对象
            }

            //可以合并多个选择集，方法是创建一个ObjectIdCollection集合对象然后从多个选择集中将对象id都添加到集合中。除了向ObjectIdCollection对象添加对象id，还可以从中删除对象id。所有对象id都添加到ObjectIdCollection集合对象后，可以遍历该集合并根据需要操作每个对象。

            acSSPrompt = acDocEd.GetSelection();
            SelectionSet acSSet2;

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                acSSet2 = acSSPrompt.Value;

                if (acObjIdColl.Count == 0)  // 检查ObjectIdCollection集合大小，如果为0就对其初始化
                {

                    acObjIdColl = new ObjectIdCollection(acSSet2.GetObjectIds());
                }

                else
                {
                    foreach (ObjectId acObjId in acSSet2.GetObjectIds())// Step through the second selection set遍历第二个选择集
                    {
                        acObjIdColl.Add(acObjId); // 将第二个选择集中的每个对象id添加到集合内

                    }
                }

            }

            foreach (ObjectId acObjId in acObjIdColl)
            {

                AcadApp.Application.ShowAlertDialog("example : " + acObjId.ToString() + "," + acObjId.Handle.Value);
            }

            AcadApp.Application.ShowAlertDialog("Number of objects selected: " + acObjIdColl.Count.ToString());

        }



        //过滤选择集
        [CommandMethod("FilterSelectionSet")]
        public static void FilterSelectionSet()
        {

            Editor acDocEd = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;

            //使用选择过滤器定义选择集规则

            //选择过滤器由TypedValue形式的一对参数构成。TypedValue的第一个参数表明过滤器的类型（例如对象），第二个参数为要过滤的值（例如圆）。过滤器类型是一个DXF组码，用来指定使用哪种过滤器。一些常用过滤器类型列表如下。


            //选择过滤器可以包含过滤多个属性或对象的条件。可以通过声明一个包含足够数量元素的数组来定义总的过滤条件，数组的每个元素代表一个过滤条件。

            TypedValue[] acTypValAr = new TypedValue[1]; // 创建一个TypedValue数组来定义过滤器条件

            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "CIRCLE"), 0);

            //**************
            // 创建TypedValue数组定义过滤条件

            //TypedValue[] acTypValAr = new TypedValue[3];

            //acTypValAr.SetValue(new TypedValue((int)DxfCode.Color, 5), 0);

            //acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "CIRCLE"), 1);

            //acTypValAr.SetValue(new TypedValue((int)DxfCode.LayerName, "0"), 2);
            //**************


            // 将过滤器条件赋值给SelectionFilter对象

            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

            PromptSelectionResult acSSPrompt;

            acSSPrompt = acDocEd.GetSelection(acSelFtr);

            // 提示状态OK，表示用户已选完

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = acSSPrompt.Value;

                AcadApp.Application.ShowAlertDialog("Number of objects selected: " + acSSet.Count.ToString());
            }

            else
            {

                AcadApp.Application.ShowAlertDialog("Numberof objects selected: 0");

            }

        }


        #endregion


        //启动进程外CAD
        //  [CommandMethod("0810")]
        public static void bb1109()
        {
            //增加了COM引用后，程序就可以使用许多VBA中的功能了


            //引用 using Autodesk.AutoCAD.Interop;// '使用COM的Interop来访问AutoCAD中的菜单API  
            //引用com下个AutoCAD 2006 Type Library和AutoCAD/ObjectDBX Common 16.0 Type Library
            //using Autodesk.AutoCAD.Interop.Common;//include acadobject;

            //AcadApplication acAppComObj = null;
            //const string strProgId = "AutoCAD.Application.16.2";// "AutoCAD.Application.18";

            //// Get a running instance of AutoCAD获取正在运行的AutoCAD实例；
            //try
            //{
            //    acAppComObj = (AcadApplication)Marshal.GetActiveObject(strProgId);
            //}
            //catch // An error occurs if no instance is running没有正在运行的实例时出错；
            //{
            //    try
            //    {
            //        // Create a new instance of AutoCAD创建新的AutoCAD实例；
            //        acAppComObj = (AcadApplication)Activator.CreateInstance(Type.GetTypeFromProgID(strProgId), true);
            //    }
            //    catch
            //    {
            //        // If an instance of AutoCAD is not created then message and exit创建新实例不成功就显示信息并退出；
            //        System.Diagnostics.Trace.WriteLine("Instance of 'AutoCAD.Application'" + " could not be created.");

            //        return;
            //    }
            //}

            //// Display the application and return the name and version显示获得的应用程序实例并返回名称、版本；
            //acAppComObj.Visible = true;
            //System.Diagnostics.Trace.WriteLine("Now running " + acAppComObj.Name + " version " + acAppComObj.Version);

            //// Get the active document
            //AcadDocument acDocComObj;
            //acDocComObj = acAppComObj.ActiveDocument;

            ////// Optionally, load your assembly and start your command or if your assembly
            ////// is demand-loaded, simply start the command of your in-process assembly.
            //////可选的，加载程序集并启动命令，如果进程内程序集已被加载，直接启动命令即可；
            ////acDocComObj.SendCommand("(command " + (char)34 + "NETLOAD" + (char)34 + " " +
            ////                        (char)34 + "c:/myapps/mycommands.dll" + (char)34 + ") ");

            ////acDocComObj.SendCommand("MyCommand ");


            //another
            //AcadMenuGroups mnus = (AcadMenuGroups)app.MenuGroups;
            //AcadPopupMenus pmnus = mnus.Item(1).Menus;
            //int count = 0;
            //foreach (AcadPopupMenu mnu in pmnus)
            //{
            //    if (mnu.OnMenuBar == true) count++;
            //}
            //AcadPopupMenu Menu_SModel = pmnus.Add("&Module");
            //string macro = Convert.ToChar(System.Windows.Forms.Keys.Escape).ToString();
            //AcadPopupMenuItem MenuItem_MainForm = Menu_SModel.AddMenuItem(Menu_SModel.Count, "&MainForm", macro + "SMF ");
            //MenuItem_MainForm.HelpString = "Show main window";
            //AcadPopupMenuItem MenuItem_SetBoard = Menu_SModel.AddMenuItem(Menu_SModel.Count, "Set &Board", macro + "mBoardW ");
            //MenuItem_SetBoard.HelpString = "Set Board Width";

            //if (count == 0)//下拉菜单不同时间的装载情况会不一样
            //    pmnus.InsertMenuInMenuBar("&Module", count + 12);//AutoCAD 2006 有13个下拉菜单项
            //else
            //    pmnus.InsertMenuInMenuBar("&Module", ++count);

        }


        //打开指定文件
        [CommandMethod("open")]
        public static void bn11009()
        {

            string strFileName = "e:\\grid.dwg";
            AcadApp.DocumentCollection acDocMgr = AcadApp.Application.DocumentManager;

            if (System.IO.File.Exists(strFileName))
            {
                acDocMgr.Open(strFileName, false);
                acDocMgr.MdiActiveDocument.Editor.WriteMessage("File " + strFileName + "  exist.");
            }

            else
            {
                acDocMgr.MdiActiveDocument.Editor.WriteMessage("File " + strFileName + " does not exist.");
            }

        }


        #region //菜单
        Autodesk.AutoCAD.Windows.ContextMenuExtension m_ContextMenu;
        [CommandMethod("ctest")]
        public void AddM()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                m_ContextMenu = new Autodesk.AutoCAD.Windows.ContextMenuExtension();
                m_ContextMenu.Title = "工程图系统";
                Autodesk.AutoCAD.Windows.MenuItem mi;
                mi = new Autodesk.AutoCAD.Windows.MenuItem("用户管理");
                mi.Click += new EventHandler(MenuUserM_OnClick);
                //mi.Click += MenuUserM_OnClick;
                m_ContextMenu.MenuItems.Add(mi);

                Autodesk.AutoCAD.ApplicationServices.Application.AddDefaultContextMenuExtension(m_ContextMenu);
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nError: " + ex.Message + "\n");
            }
        }

        //void RemoveContextMenu()
        //{
        //    try
        //    {
        //        if (m_ContextMenu != null)
        //        {
        //            Autodesk.AutoCAD.ApplicationServices.Application.RemoveDefaultContextMenuExtension(m_ContextMenu);
        //            m_ContextMenu = null;
        //        }
        //    }
        //    catch
        //    { }
        //}

        private void MenuUserM_OnClick(object Sender, EventArgs e)
        {

            //System.Windows.Forms.MessageBox.Show("用户管理");
            AcadApp.DocumentLock docLock = AcadApp.Application.DocumentManager.MdiActiveDocument.LockDocument();

            // Create();

            docLock.Dispose();
        }

        //    PaletteSet ps = null;
        //    [CommandMethod("CreatePalette")]
        //    public void CreatePalette()
        //    {
        //        ps = new PaletteSet("My Test Palette Set");
        //        ps.MinimumSize = new System.Drawing.Size(300, 300);
        //        System.Windows.Forms.UserControl myctrl = new CadLoad.MyCrl();
        //        ps.Add("test", myctrl);
        //        ps.Style = PaletteSetStyles.ShowTabForSingle;
        //        ps.Opacity = 60;
        //        ps.Visible = true;
        //    }

        #endregion

        //插入块
        [CommandMethod("IB")]
        public void ImportBlocks()
        {
            
            AcadApp.DocumentCollection dm = AcadApp.Application.DocumentManager;
            Editor ed = dm.MdiActiveDocument.Editor;
            Database destDb = dm.MdiActiveDocument.Database;
            Database sourceDb = new Database(false, true);
            PromptResult sourceFileName;
            try
            {
                //从命令行要求用户输入以得到要导入的块所在的源DWG文件的名字
                sourceFileName = ed.GetString("\nEnter the name of the source drawing: ");
                //把源DWG读入辅助数据库
                sourceDb.ReadDwgFile(sourceFileName.StringResult, System.IO.FileShare.Write, true, "");
                //用集合变量来存储块ID的列表
                ObjectIdCollection blockIds = new ObjectIdCollection();
                Autodesk.AutoCAD.DatabaseServices.TransactionManager tm =
                sourceDb.TransactionManager;
                using (Transaction myT = tm.StartTransaction())
                {
                    //打开块表
                    BlockTable bt = (BlockTable)tm.GetObject(sourceDb.BlockTableId,
                    OpenMode.ForRead, false);
                    //在块表中检查每个块
                    foreach (ObjectId btrId in bt)
                    {
                        BlockTableRecord btr = (BlockTableRecord)tm.GetObject(btrId,
                        OpenMode.ForRead, false);
                        //只添加有名块和非layout块(layout块是非MS和非PS的块) 
                        if (!btr.IsAnonymous && !btr.IsLayout)
                            blockIds.Add(btrId);
                        btr.Dispose();	//释放块表记录引用变量所占用的资源
                    }
                    bt.Dispose();//释放块表引用变量所占用的资源
                    //没有作改变，不需要提交事务
                    myT.Dispose();
                }
                
                IdMapping mapping = new IdMapping();
                mapping = sourceDb.WblockCloneObjects(blockIds, destDb.BlockTableId, DuplicateRecordCloning.Replace, false);

                ed.WriteMessage("\nCopied " + blockIds.Count.ToString() + " block definitions from " + sourceFileName.StringResult + " to the current drawing.");
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                ed.WriteMessage("\nError during copy: " + ex.Message);
            }
            sourceDb.Dispose();
        }
    }

    #region  返回一Image
    class ViewDWG
    {
        //struct BITMAPFILEHEADER
        //{
        //    public short bfType;
        //    public int bfSize;
        //    public short bfReserved1;
        //    public short bfReserved2;
        //    public int bfOffBits;
        //}



        //public static System.Drawing.Image GetDwgImage(string FileName)
        //{
        //    if (!(File.Exists(FileName)))
        //    {
        //        throw new FileNotFoundException("文件没有被找到");
        //    }
        //    FileStream DwgF;  //文件流
        //    int PosSentinel;  //文件描述块的位置
        //    BinaryReader br;  //读取二进制文件
        //    int TypePreview;  //缩略图格式
        //    int PosBMP;       //缩略图位置 
        //    int LenBMP;       //缩略图大小
        //    short biBitCount; //缩略图比特深度 
        //    BITMAPFILEHEADER biH; //BMP文件头，DWG文件中不包含位图文件头，要自行加上去
        //    byte[] BMPInfo;       //包含在DWG文件中的BMP文件体
        //    MemoryStream BMPF = new MemoryStream(); //保存位图的内存文件流
        //    BinaryWriter bmpr = new BinaryWriter(BMPF); //写二进制文件类

        //    System.Drawing.Image myImg = null;
        //    try
        //    {
        //        DwgF = new FileStream(FileName, FileMode.Open, FileAccess.Read);   //文件流
        //        br = new BinaryReader(DwgF);
        //        DwgF.Seek(13, SeekOrigin.Begin); //从第十三字节开始读取
        //        PosSentinel = br.ReadInt32();  //第13到17字节指示缩略图描述块的位置
        //        DwgF.Seek(PosSentinel + 30, SeekOrigin.Begin);  //将指针移到缩略图描述块的第31字节
        //        TypePreview = br.ReadByte();  //第31字节为缩略图格式信息，2 为BMP格式，3为WMF格式
        //        if (TypePreview == 1)
        //        {
        //        }
        //        else if (TypePreview == 2 || TypePreview == 3)
        //        {
        //            PosBMP = br.ReadInt32(); //DWG文件保存的位图所在位置
        //            LenBMP = br.ReadInt32(); //位图的大小
        //            DwgF.Seek(PosBMP + 14, SeekOrigin.Begin); //移动指针到位图块
        //            biBitCount = br.ReadInt16(); //读取比特深度
        //            DwgF.Seek(PosBMP, SeekOrigin.Begin); //从位图块开始处读取全部位图内容备用
        //            BMPInfo = br.ReadBytes(LenBMP); //不包含文件头的位图信息
        //            br.Close();
        //            DwgF.Close();
        //            biH.bfType = 19778; //建立位图文件头
        //            if (biBitCount < 9)
        //            {
        //                biH.bfSize = 54 + 4 * (int)(Math.Pow(2, biBitCount)) + LenBMP;
        //            }
        //            else
        //            {
        //                biH.bfSize = 54 + LenBMP;
        //            }
        //            biH.bfReserved1 = 0; //保留字节
        //            biH.bfReserved2 = 0; //保留字节
        //            biH.bfOffBits = 14 + 40 + 1024; //图像数据偏移
        //            //以下开始写入位图文件头
        //            bmpr.Write(biH.bfType); //文件类型
        //            bmpr.Write(biH.bfSize);  //文件大小
        //            bmpr.Write(biH.bfReserved1); //0
        //            bmpr.Write(biH.bfReserved2); //0
        //            bmpr.Write(biH.bfOffBits); //图像数据偏移
        //            bmpr.Write(BMPInfo); //写入位图
        //            BMPF.Seek(0, SeekOrigin.Begin); //指针移到文件开始处
        //            myImg = System.Drawing.Image.FromStream(BMPF); //创建位图文件对象
        //            bmpr.Close();
        //            BMPF.Close();
        //        }
        //        return myImg;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw new System.Exception(ex.Message);
        //    }
        //}
    }
}

    #endregion


/*  


























 
 
 
 
 */