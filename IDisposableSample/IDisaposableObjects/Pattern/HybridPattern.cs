using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDisaposableObjects
{
    // From StackOverflow: http://stackoverflow.com/questions/16601929/dispose-for-cleaning-up-managed-resources
    class HybridPattern : IDisposable
    {
        private bool _disposed = false;

        ~HybridPattern()
        {
            Dispose(false);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Code to dispose the managed resources of the class
                // internalComponent1.Dispose();
            }

            // Code to dispose the un-managed resources of the class
            // CloseHandle(handle);
            // handle = IntPtr.Zero;

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
