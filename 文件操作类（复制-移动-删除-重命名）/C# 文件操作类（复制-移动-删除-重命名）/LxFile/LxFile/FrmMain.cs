using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LxFile
{
    public partial class FrmMain : Form
    {
        private string info = null;

        private string[] delFiles = null;
        private string[] copyFiles = null;
        private string[] moveFiles = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        #region 删除文件

        private void btnBrowser1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "D:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtDelFile.Text = openFileDialog.FileName;
            }
        }

        private void btnBrowser2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = "D:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                delFiles = openFileDialog.FileNames;
                foreach (string file in delFiles)
                {
                    txtDelFiles.Text += file + ";";
                }
            }
        }

        //删除文件
        private void btnDelFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDelFile.Text))
            {
                this.lblInfo.Text = "请选择要删除的文件";
                return;

            }
            int i = FileOperateProxy.DeleteFile(this.txtDelFile.Text, true, true, true, ref info);
            if (i != 0)
            {
                this.lblInfo.Text = info;
            }
            else
            {
                this.lblInfo.Text = "文件删除成功";
                this.txtDelFile.Text = string.Empty;
            }
        }

        //删除多个文件
        private void btnDelFiles_Click(object sender, EventArgs e)
        {
            if (delFiles == null || delFiles.Length == 0)
            {
                this.lblInfo.Text = "请选择要删除的文件";
                return;
            }
            int i = FileOperateProxy.DeleteFiles(delFiles, true, true, true, ref info);
            if (i != 0)
            {
                this.lblInfo.Text = info;
            }
            else
            {
                this.lblInfo.Text = "所有文件删除成功";
                delFiles = null;
                this.txtDelFiles.Text = string.Empty;
            }
        }

        #endregion

        #region 移动文件

        private void btnBrowser3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "D:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtMoveFile.Text = openFileDialog.FileName;
            }
        }

        private void btnBrowser4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = "D:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                moveFiles = openFileDialog.FileNames;
                foreach (string file in moveFiles)
                {
                    txtMoveFiles.Text += file + ";";
                }
            }
        }

        private void btnBrowser5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Desktop;
            folderBrowserDialog.SelectedPath = "C:";
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = "请选择移动目录";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtMoveDesPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        //移动文件
        private void btnMoveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtMoveFile.Text))
            {
                this.lblInfo.Text = "请选择要移动的文件";
                return;
            }
            if (string.IsNullOrEmpty(this.txtMoveDesPath.Text))
            {
                this.lblInfo.Text = "请选择目的路径";
                return;
            }
            int i = FileOperateProxy.MoveFile(txtMoveFile.Text, txtMoveDesPath.Text, true, true, true, ref info);
            if (i != 0)
            {
                this.lblInfo.Text = info;
            }
            else
            {
                this.lblInfo.Text = "文件移动成功";
                this.txtMoveDesPath.Text = string.Empty;
                this.txtMoveFile.Text = string.Empty;
            }
        }

        //移动多文件
        private void btnMoveFiles_Click(object sender, EventArgs e)
        {
            if (moveFiles == null || moveFiles.Length == 0)
            {
                this.lblInfo.Text = "请选择要移动的文件";
                return;
            }
            if (string.IsNullOrEmpty(this.txtMoveDesPath.Text))
            {
                this.lblInfo.Text = "请选择目的路径";
                return;
            }
            int i = FileOperateProxy.MoveFiles(moveFiles, txtMoveDesPath.Text, true, true, true, ref info);
            if (i != 0)
            {
                this.lblInfo.Text = info;
            }
            else
            {
                this.lblInfo.Text = "所有文件移动成功";
                moveFiles = null;
                this.txtMoveFiles.Text = string.Empty;
                this.txtMoveDesPath.Text = string.Empty;
            }
        }

        #endregion

        #region 复制文件

        private void btnBrowser6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "D:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtCopyFile.Text = openFileDialog.FileName;
            }
        }

        private void btnBrowser7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = "D:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                copyFiles = openFileDialog.FileNames;
                foreach (string file in copyFiles)
                {
                    txtCopyFiles.Text += file + ";";
                }
            }
        }

        private void btnBrowser8_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Desktop;
            folderBrowserDialog.SelectedPath = "C:";
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = "请选择复制目录";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtCopyDesPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        //复制文件
        private void btnCopyFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCopyFile.Text))
            {
                this.lblInfo.Text = "请选择要复制的文件";
                return;
            }
            if (string.IsNullOrEmpty(this.txtCopyDesPath.Text))
            {
                this.lblInfo.Text = "请选择目的路径";
                return;
            }
            int i = FileOperateProxy.CopyFile(txtCopyFile.Text, txtCopyDesPath.Text, true, true, true, ref info);
            if (i != 0)
            {
                this.lblInfo.Text = info;
            }
            else
            {
                this.lblInfo.Text = "文件复制成功";
                txtCopyFile.Text = string.Empty;
                txtCopyDesPath.Text = string.Empty;
            }
        }

        //复制多个文件
        private void btnCopyFiles_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCopyFiles.Text))
            {
                this.lblInfo.Text = "请选择要复制的文件";
                return;
            }

            if (string.IsNullOrEmpty(this.txtCopyDesPath.Text))
            {
                this.lblInfo.Text = "请选择目的路径";
                return;
            }
            int i = FileOperateProxy.CopyFiles(copyFiles, txtCopyDesPath.Text, true, true, true, ref info);
            if (i != 0)
            {
                this.lblInfo.Text = info;
            }
            else
            {
                this.lblInfo.Text = "所有文件复制成功";
                copyFiles = null;
                this.txtCopyFiles.Text = string.Empty;
                this.txtCopyDesPath.Text = string.Empty;
            }

        }

        #endregion


        #region 文件重命名

        private void btnBrowser9_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "D:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnReNameFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFilePath.Text))
            {
                this.lblInfo.Text = "请选择要重命名的文件";
                return;
            }

            if (string.IsNullOrEmpty(this.txtNewName.Text))
            {
                this.lblInfo.Text = "请输入新的名称";
                return;
            }


            string extensName = Path.GetExtension(txtFilePath.Text);
            string filDir = Path.GetDirectoryName(txtFilePath.Text);

            string newName = filDir + this.txtNewName.Text + extensName;
            int i = FileOperateProxy.ReNameFile(txtFilePath.Text, newName, true, ref info);
            if (i != 0)
            {
                this.lblInfo.Text = info;
            }
            else
            {
                this.lblInfo.Text = "文件重命名成功";
                this.txtFilePath.Text = string.Empty;
                this.txtNewName.Text = string.Empty;
            }
        }


        private void btnReNameFile2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFilePath.Text))
            {
                this.lblInfo.Text = "请选择要重命名的文件";
                return;
            }

            if (string.IsNullOrEmpty(this.txtNewName.Text))
            {
                this.lblInfo.Text = "请输入新的名称";
                return;
            }

            try
            {
                FileOperateProxy.ReNameFile(txtFilePath.Text, txtNewName.Text);
                
                this.lblInfo.Text = "文件重命名成功";
            }
            catch (Exception ex)
            {
                this.lblInfo.Text = ex.Message.ToString();
            }
        }

        #endregion
    }
}
