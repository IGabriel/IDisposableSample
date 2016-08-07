using System;
using System.IO;

namespace IDisaposableObjects
{
    public class ManagedFileHolder : IFileHolder, IDisposable
    {
        private string _fileName;
        private FileStream _stream;

        public ManagedFileHolder(string fileName)
        {
            _fileName = fileName;
        }

        public void OpenFile()
        {
            Console.WriteLine("Open file with .Net libray.");
            _stream = File.Open(_fileName, FileMode.Append, FileAccess.Write);
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && _stream != null)
                {
                    _stream.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
