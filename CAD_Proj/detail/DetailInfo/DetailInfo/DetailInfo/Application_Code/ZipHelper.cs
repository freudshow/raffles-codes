using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections; 
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Core;
using System.Runtime.Serialization.Formatters.Binary;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams; 

namespace DetailInfo
{
    class ZipHelper
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationZipFilePath"></param>
        public static void CreateZip(string sourceFilePath, string destinationZipFilePath)
        {
            if (sourceFilePath[sourceFilePath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                sourceFilePath += System.IO.Path.DirectorySeparatorChar;
            ZipOutputStream zipStream = new ZipOutputStream(File.Create(destinationZipFilePath + ".zip"));
            zipStream.SetLevel(6); // 压缩级别 0-9
            CreateZipFiles(sourceFilePath, zipStream, sourceFilePath);
            zipStream.Finish();
            zipStream.Close();
        }

        /// <summary>
        /// 递归压缩文件
        /// </summary>
        /// <param name="sourceFilePath">待压缩的文件或文件夹路径</param>
        /// <param name="zipStream">打包结果的zip文件路径（类似 D:/WorkSpace/a.zip）,全路径包括文件名和.zip扩展名</param>
        /// <param name="staticFile"></param>
        public static void CreateZipFiles(string sourceFilePath, ZipOutputStream zipStream, string staticFile)
        {
            string[] filesArray = Directory.GetFileSystemEntries(sourceFilePath);
            foreach (string file in filesArray)
            {
                if (Directory.Exists(file)) //如果当前是文件夹，递归
                {
                    CreateZipFiles(file, zipStream, staticFile);
                }
                else //如果是文件，开始压缩
                {
                    FileStream fileStream = File.OpenRead(file);
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    string tempFile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempFile);
                    entry.DateTime = DateTime.Now;
                    entry.Size = fileStream.Length;
                    fileStream.Close();
                    zipStream.PutNextEntry(entry);
                    zipStream.Write(buffer, 0, buffer.Length);

                }
            }
        }
    }


    ///    
    /// Zip文件压缩、解压   
    ///    
    public class ZipFile
    {
        ///    
        /// 解压类型   
        ///    
        public enum UnzipType
        {
            ///    
            /// 解压到当前压缩文件所在的目录   
            ///    
            ToCurrentDirctory,
            ///    
            /// 解压到与压缩文件名相同的新的目录,如果有多个文件,将为每个文件创建一个目录   
            ///    
            ToNewDirctory
        }

        #region 压缩
        ///    
        /// 压缩文件,默认目录为当前目录,文件名为当前目录名,压缩级别为6   
        ///    
        /// 要压缩的文件或文件夹   
        public void Zip(params string[] fileOrDirectory)
        {
            Zip(6, fileOrDirectory);
        }

        ///    
        /// 压缩文件,默认目录为当前目录,文件名为当前目录名   
        ///    
        /// 压缩的级别   
        /// 要压缩的文件或文件夹   
        public void Zip(int zipLevel, params string[] fileOrDirectory)
        {
            if (fileOrDirectory == null)
                return;
            else if (fileOrDirectory.Length < 1)
                return;
            else
            {
                string str = fileOrDirectory[0];
                if (str.EndsWith("\\"))
                    str = str.Substring(0, str.Length - 1);
                str += ".zip";
                Zip(str, zipLevel, fileOrDirectory);
            }
        }


        ///    
        /// 压缩文件,默认目录为当前目录   
        ///    
        /// 压缩后的文件   
        /// 压缩的级别   
        /// 要压缩的文件或文件夹   
        public void Zip(string zipedFileName, int zipLevel, params string[] fileOrDirectory)
        {
            if (fileOrDirectory == null)
                return;
            else if (fileOrDirectory.Length < 1)
                return;
            else
            {
                string str = fileOrDirectory[0];
                if (str.EndsWith("\\"))
                    str = str.Substring(0, str.Length - 1);
                str = str.Substring(0, str.LastIndexOf("\\"));
                Zip(zipedFileName, str, zipLevel, fileOrDirectory);
            }
        }

        ///    
        /// 压缩文件   
        ///    
        /// 压缩后的文件   
        /// 压缩的级别   
        /// 当前所处目录   
        /// 要压缩的文件或文件夹   
        public void Zip(string zipedFileName, string currentDirectory, int zipLevel, params string[] fileOrDirectory)
        {
            ArrayList AllFiles = new ArrayList();

            //获取所有文件   
            if (fileOrDirectory != null)
            {
                for (int i = 0; i < fileOrDirectory.Length; i++)
                {
                    if (File.Exists(fileOrDirectory[i]))
                        AllFiles.Add(fileOrDirectory[i]);
                    else if (Directory.Exists(fileOrDirectory[i]))
                        GetDirectoryFile(fileOrDirectory[i], AllFiles);
                }
            }

            if (AllFiles.Count < 1)
                return;

            ZipOutputStream zipedStream = new ZipOutputStream(File.Create(zipedFileName));
            zipedStream.SetLevel(zipLevel);

            for (int i = 0; i < AllFiles.Count; i++)
            {
                string file = AllFiles[i].ToString();
                FileStream fs;

                //打开要压缩的文件   
                try
                {
                    fs = File.OpenRead(file);
                }
                catch
                {
                    continue;
                }

                try
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);

                    //新建一个entry   
                    string fileName = file.Replace(currentDirectory, "");
                    if (fileName.StartsWith("\\"))
                        fileName = fileName.Substring(1);
                    ZipEntry entry = new ZipEntry(fileName);
                    entry.DateTime = DateTime.Now;

                    //保存到zip流   
                    fs.Close();
                    zipedStream.PutNextEntry(entry);
                    zipedStream.Write(buffer, 0, buffer.Length);
                }
                catch
                {
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

            zipedStream.Finish();
            zipedStream.Close();
        }


        ///    
        /// 压缩文件夹   
        ///    
        /// 当前所在的文件夹   
        public void ZipDirectory(string curretnDirectory)
        {
            if (curretnDirectory == null)
                return;

            string dir = curretnDirectory;
            if (dir.EndsWith("\\"))
                dir = dir.Substring(0, dir.Length - 1);
            string file = dir.Substring(dir.LastIndexOf("\\") + 1) + ".zip";
            dir += "\\" + file;

            Zip(dir, 6, curretnDirectory);
        }


        ///    
        /// 压缩文件夹   
        ///    
        /// 当前所在的文件夹   
        public void ZipDirectory(string curretnDirectory, int zipLevel)
        {
            if (curretnDirectory == null)
                return;

            string dir = curretnDirectory;
            if (dir.EndsWith("\\"))
                dir = dir.Substring(0, dir.Length - 1);
            dir += ".zip";

            Zip(dir, zipLevel, curretnDirectory);
        }


        //递归获取一个目录下的所有文件   
        private void GetDirectoryFile(string parentDirectory, ArrayList toStore)
        {
            string[] files = Directory.GetFiles(parentDirectory);
            for (int i = 0; i < files.Length; i++)
                toStore.Add(files[i]);
            string[] directorys = Directory.GetDirectories(parentDirectory);
            for (int i = 0; i < directorys.Length; i++)
                GetDirectoryFile(directorys[i], toStore);
        }
        #endregion


        #region 解压
        ///    
        /// 解压文件   
        /// 解压类型   
        /// 要解压的文件   
        public void UnZip(UnzipType type, params string[] zipFile)
        {
            ZipInputStream zipStream;
            ZipEntry entry;
            for (int i = 0; i < zipFile.Length; i++)
            {
                zipStream = new ZipInputStream(File.OpenRead(zipFile[i]));

                //获取目录名,并创建该目录   
                string directoryName = "";
                if (type == UnzipType.ToNewDirctory)
                    directoryName = zipFile[i].Substring(0, zipFile[i].LastIndexOf("."));
                else
                    directoryName = zipFile[i].Substring(0, zipFile[i].LastIndexOf("\\"));

                if (!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);

                //读出每一个文件   
                while ((entry = zipStream.GetNextEntry()) != null)
                {
                    //获取文件名   
                    string fileName = entry.Name;
                    if (directoryName.EndsWith("\\"))
                        fileName = directoryName + fileName;
                    else
                        fileName = directoryName + "\\" + fileName;
                    if (fileName == String.Empty)
                        continue;

                    //创建一个文件   
                    if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                    FileStream streamWriter = File.Create(fileName);

                    //写入文件   
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = zipStream.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }

                    streamWriter.Close();

                }
                zipStream.Close();
            }
        }

        ///    
        /// 解压文件   
        /// 要解压的文件,默认解压到新文件夹,每个文件对应生成一个文件夹   
        public void UnZip(params string[] zipFile)
        {
            UnZip(UnzipType.ToNewDirctory, zipFile);
        }
        #endregion
    }
}
