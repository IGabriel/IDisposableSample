using System;

namespace IDisaposableObjects
{
    public class DisposableResourceHolder : FileHolder, IDisposable
    {
        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                Console.WriteLine("IDisposableForUnmanaged: object have been disposed.");
                return;
            }
            StreamInUse.Dispose();
            StreamInUse = null;
            Console.WriteLine("IDisposableForUnmanaged: object disposed.");
            disposed = true;
        }

        ~DisposableResourceHolder()
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
