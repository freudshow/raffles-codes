using System;
using System.Runtime.InteropServices;
using System.IO;


namespace LxFile
{
    /// <summary>
    /// 文件操作代理，该类提供类似于Windows的文件操作体验
    /// </summary>
    public class FileOperateProxy
    {
        #region 【内部类型定义】
        private struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;         //父窗口句柄 
            public wFunc wFunc;         //要执行的动作 
            public string pFrom;        //源文件路径，可以是多个文件，以结尾符号"\0"结束
            public string pTo;          //目标路径，可以是路径或文件名 
            public FILEOP_FLAGS fFlags;             //标志，附加选项 
            public bool fAnyOperationsAborted;      //是否可被中断 
            public IntPtr hNameMappings;            //文件映射名字，可在其它 Shell 函数中使用 
            public string lpszProgressTitle;        // 只在 FOF_SIMPLEPROGRESS 时，指定对话框的标题。
        }

        private enum wFunc
        {
            FO_MOVE = 0x0001,   //移动文件
            FO_COPY = 0x0002,   //复制文件
            FO_DELETE = 0x0003, //删除文件，只是用pFrom
            FO_RENAME = 0x0004  //文件重命名
        }

        private enum FILEOP_FLAGS
        {
            FOF_MULTIDESTFILES = 0x0001,    //pTo 指定了多个目标文件，而不是单个目录
            FOF_CONFIRMMOUSE = 0x0002,
            FOF_SILENT = 0x0044,            // 不显示一个进度对话框
            FOF_RENAMEONCOLLISION = 0x0008, // 碰到有抵触的名字时，自动分配前缀
            FOF_NOCONFIRMATION = 0x10,      // 不对用户显示提示
            FOF_WANTMAPPINGHANDLE = 0x0020, // 填充 hNameMappings 字段，必须使用 SHFreeNameMappings 释放
            FOF_ALLOWUNDO = 0x40,           // 允许撤销
            FOF_FILESONLY = 0x0080,         // 使用 *.* 时, 只对文件操作
            FOF_SIMPLEPROGRESS = 0x0100,    // 简单进度条，意味者不显示文件名。
            FOF_NOCONFIRMMKDIR = 0x0200,    // 建新目录时不需要用户确定
            FOF_NOERRORUI = 0x0400,         // 不显示出错用户界面
            FOF_NOCOPYSECURITYATTRIBS = 0x0800,     // 不复制 NT 文件的安全属性
            FOF_NORECURSION = 0x1000        // 不递归目录
        }
        #endregion 【内部类型定义】

        #region 【DllImport】

        [DllImport("shell32.dll")]
        private static extern int SHFileOperation(ref SHFILEOPSTRUCT lpFileOp);

        #endregion 【DllImport】

        #region 【删除文件操作】
        /// <summary>
        /// 删除单个文件。
        /// </summary>
        /// <param name="fileName">删除的文件名</param>
        /// <param name="toRecycle">指示是将文件放入回收站还是永久删除，true-放入回收站，false-永久删除</param>
        /// <param name="showDialog">指示是否显示确认对话框，true-显示确认删除对话框，false-不显示确认删除对话框</param>
        /// <param name="showProgress">指示是否显示进度对话框，true-显示，false-不显示。该参数当指定永久删除文件时有效</param>
        /// <param name="errorMsg">反馈错误消息的字符串</param>
        /// <returns>操作执行结果标识，删除文件成功返回0，否则，返回错误代码</returns>
        public static int DeleteFile(string fileName, bool toRecycle, bool showDialog, bool showProgress, ref string errorMsg)
        {
            try
            {
                string fName = GetFullName(fileName);
                return ToDelete(fName, toRecycle, showDialog, showProgress, ref errorMsg);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return -200;
            }
        }

        /// <summary>
        /// 删除一组文件。
        /// </summary>
        /// <param name="fileNames">字符串数组，表示一组文件名</param>
        /// <param name="toRecycle">指示是将文件放入回收站还是永久删除，true-放入回收站，false-永久删除</param>
        /// <param name="showDialog">指示是否显示确认对话框，true-显示确认删除对话框，false-不显示确认删除对话框</param>
        /// <param name="showProgress">指示是否显示进度对话框，true-显示，false-不显示。该参数当指定永久删除文件时有效</param>
        /// <param name="errorMsg">反馈错误消息的字符串</param>
        /// <returns>操作执行结果标识，删除文件成功返回0，否则，返回错误代码</returns>
        public static int DeleteFiles(string[] fileNames, bool toRecycle, bool showDialog, bool showProgress, ref string errorMsg)
        {
            try
            {
                string fName = "";
                foreach (string str in fileNames)
                {
                    fName += GetFullName(str) + "\0";     //组件文件组字符串
                }

                return ToDelete(fName, toRecycle, showDialog, showProgress, ref errorMsg);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return -200;
            }
        }
        #endregion 【删除文件操作】

        #region 【移动文件操作】
        /// <summary>
        /// 移动一个文件到指定路径下
        /// </summary>
        /// <param name="sourceFileName">要移动的文件名</param>
        /// <param name="destinationPath">移动到的目的路径</param>
        /// <param name="showDialog">指示是否显示确认对话框，true-显示确认对话框，false-不显示确认对话框</param>
        /// <param name="showProgress">指示是否显示进度对话框</param>
        /// <param name="autoRename">指示当文件名重复时，是否自动为新文件加上后缀名</param>
        /// <param name="errorMsg">反馈错误消息的字符串</param>
        /// <returns>返回移动操作是否成功的标识，成功返回0，失败返回错误代码</returns>
        public static int MoveFile(string sourceFileName, string destinationPath, bool showDialog, bool showProgress, bool autoRename, ref string errorMsg)
        {
            try
            {
                string sfName = GetFullName(sourceFileName);
                string dfName = GetFullName(destinationPath);

                return ToMoveOrCopy(wFunc.FO_MOVE, sfName, dfName, showDialog, showProgress, autoRename, ref errorMsg);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return -200;
            }
        }

        /// <summary>
        /// 移动一组文件到指定的路径下
        /// </summary>
        /// <param name="sourceFileNames">要移动的文件名数组</param>
        /// <param name="destinationPath">移动到的目的路径</param>
        /// <param name="showDialog">指示是否显示确认对话框，true-显示确认对话框，false-不显示确认对话框</param>
        /// <param name="showProgress">指示是否显示进度对话框</param>
        /// <param name="autoRename">指示当文件名重复时，是否自动为新文件加上后缀名</param>
        /// <param name="errorMsg">反馈错误消息的字符串</param>
        /// <returns>返回移动操作是否成功的标识，成功返回0，失败返回错误代码,-200:表示其他异常</returns>
        public static int MoveFiles(string[] sourceFileNames, string destinationPath, bool showDialog, bool showProgress, bool autoRename, ref string errorMsg)
        {
            try
            {
                string sfName = "";
                foreach (string str in sourceFileNames)
                {
                    sfName += GetFullName(str) + "\0";   //组件文件组字符串
                }
                string dfName = GetFullName(destinationPath);

                return ToMoveOrCopy(wFunc.FO_MOVE, sfName, dfName, showDialog, showProgress, autoRename, ref errorMsg);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return -200;
            }
        }
        #endregion 【移动文件操作】

        #region 【复制文件操作】
        /// <summary>
        /// 复制一个文件到指定的文件名或路径
        /// </summary>
        /// <param name="sourceFileName">要复制的文件名</param>
        /// <param name="destinationFileName">复制到的目的文件名或路径</param>
        /// <param name="showDialog">指示是否显示确认对话框，true-显示确认对话框，false-不显示确认对话框</param>
        /// <param name="showProgress">指示是否显示进度对话框</param>
        /// <param name="autoRename">指示当文件名重复时，是否自动为新文件加上后缀名</param>
        /// <returns>返回移动操作是否成功的标识，成功返回0，失败返回错误代码,-200:表示其他异常</returns>
        public static int CopyFile(string sourceFileName, string destinationFileName, bool showDialog, bool showProgress, bool autoRename, ref string errorMsg)
        {
            try
            {
                string sfName = GetFullName(sourceFileName);
                string dfName = GetFullName(destinationFileName);

                return ToMoveOrCopy(wFunc.FO_COPY, sfName, dfName, showDialog, showProgress, autoRename, ref errorMsg);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return -200;
            }
        }

        /// <summary>
        /// 复制一组文件到指定的路径
        /// </summary>
        /// <param name="sourceFileNames">要复制的文件名数组</param>
        /// <param name="destinationPath">复制到的目的路径</param>
        /// <param name="showDialog">指示是否显示确认对话框，true-显示确认对话框，false-不显示确认对话框</param>
        /// <param name="showProgress">指示是否显示进度对话框</param>
        /// <param name="autoRename">指示当文件名重复时，是否自动为新文件加上后缀名</param>
        /// <returns>返回移动操作是否成功的标识，成功返回0，失败返回错误代码,-200:表示其他异常</returns>
        public static int CopyFiles(string[] sourceFileNames, string destinationPath, bool showDialog, bool showProgress, bool autoRename, ref string errorMsg)
        {
            try
            {
                string sfName = "";
                foreach (string str in sourceFileNames)
                {
                    sfName += GetFullName(str) + "\0";     //组件文件组字符串
                }
                string dfName = GetFullName(destinationPath);

                return ToMoveOrCopy(wFunc.FO_COPY, sfName, dfName, showDialog, showProgress, autoRename, ref errorMsg);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return -200;
            }
        }
        #endregion 【复制文件操作】

        #region 【重命名文件】
        /// <summary>
        /// 重命名一个文件为新名称，建议您使用更方便的Microsoft.VisualBasic.FileSystem.ReName();替换该方法
        /// </summary>
        /// <param name="sourceFileName">要复制的文件名</param>
        /// <param name="destinationFileName">复制到的目的文件名或路径</param>
        /// <param name="showDialog">指示是否显示确认对话框，true-显示确认对话框，false-不显示确认对话框</param>
        /// <returns>返回移动操作是否成功的标识，成功返回0，失败返回错误代码,-200:表示其他异常</returns>
        [Obsolete("建议使用 Microsoft.VisualBasic.FileSystem.ReName()方法")]
        public static int ReNameFile(string sourceFileName, string destinationFileName, bool showDialog, ref string errorMsg)
        {
            
            try
            {
                SHFILEOPSTRUCT lpFileOp = new SHFILEOPSTRUCT();
                lpFileOp.wFunc = wFunc.FO_RENAME;
                lpFileOp.pFrom = GetFullName(sourceFileName) + "\0\0";         //将文件名以结尾字符"\0\0"结束
                lpFileOp.pTo = GetFullName(destinationFileName) + "\0\0";

                lpFileOp.fFlags = FILEOP_FLAGS.FOF_NOERRORUI;
                if (!showDialog)
                    lpFileOp.fFlags |= FILEOP_FLAGS.FOF_NOCONFIRMATION;     //设定不显示提示对话框


                lpFileOp.fAnyOperationsAborted = true;

                int n = SHFileOperation(ref lpFileOp);
                if (n == 0)
                    return 0;

                string tmp = GetErrorString(n);

                errorMsg = string.Format("{0}({1})", tmp, sourceFileName);

                return n;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return -200;
            }
        }

        /// <summary>
        /// 利用Microsoft.VisualBasic.FileSystem.ReName()方法实现
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newFileName"></param>
        public static void ReNameFile(string filePath, string newFileName)
        {
            try
            {
                string extensName = Path.GetExtension(filePath);
                string newName = newFileName + extensName;
                Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(filePath, newName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        #endregion 【重命名文件】

        /// <summary>
        /// 删除单个或多个文件
        /// </summary>
        /// <param name="fileName">删除的文件名，如果是多个文件，文件名之间以字符串结尾符'\0'隔开</param>
        /// <param name="toRecycle">指示是将文件放入回收站还是永久删除，true-放入回收站，false-永久删除</param>
        /// <param name="showDialog">指示是否显示确认对话框，true-显示确认删除对话框，false-不显示确认删除对话框</param>
        /// <param name="showProgress">指示是否显示进度对话框，true-显示，false-不显示。该参数当指定永久删除文件时有效</param>
        /// <param name="errorMsg">反馈错误消息的字符串</param>
        /// <returns>操作执行结果标识，删除文件成功返回0，否则，返回错误代码</returns>
        private static int ToDelete(string fileName, bool toRecycle, bool showDialog, bool showProgress, ref string errorMsg)
        {
            SHFILEOPSTRUCT lpFileOp = new SHFILEOPSTRUCT();
            lpFileOp.wFunc = wFunc.FO_DELETE;
            lpFileOp.pFrom = fileName + "\0";       //将文件名以结尾字符"\0"结束

            lpFileOp.fFlags = FILEOP_FLAGS.FOF_NOERRORUI;
            if (toRecycle)
                lpFileOp.fFlags |= FILEOP_FLAGS.FOF_ALLOWUNDO;  //设定删除到回收站
            if (!showDialog)
                lpFileOp.fFlags |= FILEOP_FLAGS.FOF_NOCONFIRMATION;     //设定不显示提示对话框
            if (!showProgress)
                lpFileOp.fFlags |= FILEOP_FLAGS.FOF_SILENT;     //设定不显示进度对话框

            lpFileOp.fAnyOperationsAborted = true;

            int n = SHFileOperation(ref lpFileOp);
            if (n == 0)
                return 0;

            string tmp = GetErrorString(n);

            //.av 文件正常删除了但也提示 402 错误，不知道为什么。屏蔽之。
            if ((fileName.ToLower().EndsWith(".av") && n.ToString("X") == "402"))
                return 0;

            errorMsg = string.Format("{0}({1})", tmp, fileName);

            return n;
        }

        /// <summary>
        /// 移动或复制一个或多个文件到指定路径下
        /// </summary>
        /// <param name="flag">操作类型，是移动操作还是复制操作</param>
        /// <param name="sourceFileName">要移动或复制的文件名，如果是多个文件，文件名之间以字符串结尾符'\0'隔开</param>
        /// <param name="destinationFileName">移动到的目的位置</param>
        /// <param name="showDialog">指示是否显示确认对话框，true-显示确认对话框，false-不显示确认对话框</param>
        /// <param name="showProgress">指示是否显示进度对话框</param>
        /// <param name="autoRename">指示当文件名重复时，是否自动为新文件加上后缀名</param>
        /// <param name="errorMsg">反馈错误消息的字符串</param>
        /// <returns>返回移动操作是否成功的标识，成功返回0，失败返回错误代码</returns>
        private static int ToMoveOrCopy(wFunc flag, string sourceFileName, string destinationFileName, bool showDialog, bool showProgress, bool autoRename, ref string errorMsg)
        {
            SHFILEOPSTRUCT lpFileOp = new SHFILEOPSTRUCT();
            lpFileOp.wFunc = flag;
            lpFileOp.pFrom = sourceFileName + "\0";         //将文件名以结尾字符"\0\0"结束
            lpFileOp.pTo = destinationFileName + "\0\0";

            lpFileOp.fFlags = FILEOP_FLAGS.FOF_NOERRORUI;
            lpFileOp.fFlags |= FILEOP_FLAGS.FOF_NOCONFIRMMKDIR; //指定在需要时可以直接创建路径
            if (!showDialog)
                lpFileOp.fFlags |= FILEOP_FLAGS.FOF_NOCONFIRMATION;     //设定不显示提示对话框
            if (!showProgress)
                lpFileOp.fFlags |= FILEOP_FLAGS.FOF_SILENT;     //设定不显示进度对话框
            if (autoRename)
                lpFileOp.fFlags |= FILEOP_FLAGS.FOF_RENAMEONCOLLISION;  //自动为重名文件添加名称后缀

            lpFileOp.fAnyOperationsAborted = true;

            int n = SHFileOperation(ref lpFileOp);
            if (n == 0)
                return 0;

            string tmp = GetErrorString(n);

            errorMsg = string.Format("{0}({1})", tmp, sourceFileName);

            return n;
        }

        /// <summary>
        /// 获取一个文件的全名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回生成文件的完整路径名</returns>
        private static string GetFullName(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            return fi.FullName;
        }

        /// <summary>
        /// 解释错误代码
        /// </summary>
        /// <param name="n">代码号</param>
        /// <returns>返回关于错误代码的文字描述</returns>
        private static string GetErrorString(int n)
        {
            if (n == 0) return string.Empty;

            switch (n)
            {
                case 2:
                    return "系统找不到指定的文件。";
                case 7:
                    return "存储控制块被销毁。您是否选择的“取消”操作？";
                case 113:
                    return "文件已存在！";
                case 115:
                    return "重命名文件操作,原始文件和目标文件必须具有相同的路径名。不能使用相对路径。";
                case 117:
                    return "I/O控制错误";
                case 123:
                    return "指定了重复的文件名";
                case 116:
                    return "The source is a root directory, which cannot be moved or renamed.";
                case 118:
                    return "Security settings denied access to the source.";
                case 124:
                    return "The path in the source or destination or both was invalid.";
                case 65536:
                    return "An unspecified error occurred on the destination.";
                case 1026:
                    return "在试图移动或拷贝一个不存在的文件.";
                case 1223:
                    return "操作被取消！";
                default:
                    return "未识别的错误代码：" + n;
            }
        }
    }
}

