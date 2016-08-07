using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IDisaposableObjects
{
    public class HybridHolder : IFileHolder, IDisposable
    {
        private string _unmanagedFile;
        private string _managedFile;

        private IntPtr _handle;
        private FileStream _stream;

        public HybridHolder(string unmanagedFile, string managedFile)
        {
            _unmanagedFile = unmanagedFile;
            _managedFile = managedFile;
        }

        public void OpenFile()
        {
            Console.WriteLine("Open file with windows api.");
            OFSTRUCT info;
            _handle = WindowsApi.OpenFile(_unmanagedFile, out info, OpenFileStyle.OF_READWRITE);

            Console.WriteLine("Open file with .Net libray.");
            _stream = File.Open(_managedFile, FileMode.Append, FileAccess.Write);
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                //Console.WriteLine("string is null? {0}", _stream == null);
                if (disposing && _stream != null)
                {
                    Console.WriteLine("Clean up managed resource.");
                    _stream.Dispose();
                }

                Console.WriteLine("Clean up unmanaged resource.");
                WindowsApi.CloseHandle(_handle);
                _handle = IntPtr.Zero;

                disposed = true;
            }
        }

        ~HybridHolder()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
