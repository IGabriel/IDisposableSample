﻿using System;

namespace IDisaposableObjects
{
    public class UnmanagedFileHolder : IFileHolder, IDisposable
    {
        private IntPtr _handle;
        private string _fileName;

        public UnmanagedFileHolder(string fileName)
        {
            _fileName = fileName;
        }

        public void OpenFile()
        {
            Console.WriteLine("Open file with windows api.");
            OFSTRUCT info;
            _handle = WindowsApi.OpenFile(_fileName, out info, OpenFileStyle.OF_READWRITE);
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // no managed resource
                }
                WindowsApi.CloseHandle(_handle);
                _handle = IntPtr.Zero;

                disposed = true;
            }
        }

        ~UnmanagedFileHolder()
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
