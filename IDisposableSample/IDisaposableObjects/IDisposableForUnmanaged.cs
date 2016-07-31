using System;
using System.IO;

namespace IDisaposableObjects
{
    public class IDisposableForUnmanaged : IResourceConsumer, IDisposable
    {
        private MemoryStream _stream;

        public IDisposableForUnmanaged()
        {
            _stream = new MemoryStream();
            Console.WriteLine("IDisposableForUnmanaged: constructor, created memory stream.");
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                Console.WriteLine("IDisposableForUnmanaged: object have been disposed.");
                return;
            }
            _stream.Dispose();
            Console.WriteLine("IDisposableForUnmanaged: object disposed.");
            disposed = true;
        }

        ~IDisposableForUnmanaged()
        {
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void ApplyResource()
        {
            for (int i = 0; i < ConstValues.CycleTimes; i++)
            {
                Console.WriteLine("apply new block to memory stream, id: {0}", i);
                Byte[] buffer = new byte[ConstValues.BlockSize];
                _stream.Write(buffer, 0, ConstValues.BlockSize);
            }
        }
        #endregion
    }
}
