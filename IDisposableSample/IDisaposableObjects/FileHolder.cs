using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Text;

namespace IDisaposableObjects
{
    public abstract class FileHolder : IFileHolder
    {
        public readonly static string TestFileName = @"TestFile.txt";

        public FileStream StreamInUse
        {
            get; protected set;
        }

        public void OpenFile()
        {
            StreamInUse = File.Open(TestFileName, FileMode.Append, FileAccess.Write);
            Console.WriteLine("Open file {0} and keep it in use.", TestFileName);
        }


        public bool IsFileInUse()
        {
            return IsFileInUse(TestFileName);
        }

        public static bool IsFileInUse(string fileName)
        {
            bool isInUse = true;
            FileStream stream = null;
            try
            {
                stream = File.Open(fileName, FileMode.Append, FileAccess.Write);
                isInUse = false;
            }
            catch
            {
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
            return isInUse;
        }
    }
}
