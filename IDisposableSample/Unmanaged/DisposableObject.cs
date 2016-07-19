using System;
using System.IO;

namespace Unmanaged
{
    class DisposableObject
    {
        private MemoryStream _stream;

        public DisposableObject()
        {
            Console.WriteLine("DisposableObject: constructor, creating memory stream...");
            _stream = new MemoryStream();
        }

        public void SayMorning()
        {
            Console.WriteLine("Good morning!");
        }

        #region IDisposable Support
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                Console.WriteLine("DisposableObject: object have been disposed.");
                return;
            }

            if (disposing)
            {
                Console.WriteLine("DisposableObject: releasing unmanaged resource...");
                _stream.Close();
            }
            Console.WriteLine("DisposableObject: object disposed.");
            disposed = true;
        }

        ~DisposableObject()
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
