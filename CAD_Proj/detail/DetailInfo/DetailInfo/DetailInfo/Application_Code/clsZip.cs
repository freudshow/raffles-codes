using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace DetailInfo
{
    class clsZip
    {
        public static void CompressFile(string sourceFile, string destinationFile)
        {
            // make sure the source file is there
            if ( Directory.Exists(sourceFile) == false)
                throw new FileNotFoundException();

            // Create the streams and byte arrays needed
            byte[] buffer = null;
            FileStream sourceStream = null;
            FileStream destinationStream = null;
            GZipStream compressedStream = null;

            try
            {
                // Read the bytes from the source file into a byte array
                string[] filesArray = Directory.GetFileSystemEntries(sourceFile);
                foreach (string file in filesArray)
                {
                    sourceStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);

                    // Read the source stream values into the buffer
                    buffer = new byte[sourceStream.Length];
                    int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);

                    if (checkCounter != buffer.Length)
                    {
                        throw new ApplicationException();
                    }

                    // Open the FileStream to write to
                    destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write);
                    // Create a compression stream pointing to the destiantion stream
                    compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true);

                    // Now write the compressed data to the destination file
                    compressedStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "压缩文件时发生错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Make sure we allways close all streams
                if (sourceStream != null)
                    sourceStream.Close();

                if (compressedStream != null)
                    compressedStream.Close();

                if (destinationStream != null)
                    destinationStream.Close();
            }
        }
    }
}
