using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using IDisaposableObjects;

namespace TestConsole
{
    class Program
    {
        private static readonly TimeSpan oneSeconds = new TimeSpan(0, 0, 1);
        private static readonly TimeSpan tenSeconds = new TimeSpan(0, 0, 10);
        private static readonly TimeSpan thritySecnods = new TimeSpan(0, 0, 30);

        static void Main(string[] args)
        {
            OpenFileWithGC();

            OpenFileWithtUsing();
            OpenFileWithoutUsing();

            ApplyManagedResourceWithGC();



            Console.WriteLine("End of the application");
        }

        #region Destructor Sample
        private static void OpenFileWithGC()
        {
            OpenFile_Destructor();
            MonitorFileStatus();

            Wait(tenSeconds);
            CallGC();
            Wait(tenSeconds);
        }

        private static void OpenFile_Destructor()
        {
            UnmanagedResourceHolder obj = new UnmanagedResourceHolder();
            obj.OpenFile();
            Console.WriteLine("End of test method.");
        }
        #endregion Destructor Sample

        #region IDispose samples

        private static void OpenFileWithtUsing()
        {
            MonitorFileStatus();
            Wait(tenSeconds);

            using (DisposableResourceHolder obj = new DisposableResourceHolder())
            {
                obj.OpenFile();
                Wait(tenSeconds);
                Console.WriteLine("End of using statement.");
            }
            Wait(tenSeconds);
        }

        private static void OpenFileWithoutUsing()
        {
            MonitorFileStatus();
            OpenFile_IDisposable();

            Wait(tenSeconds);
            CallGC();
            Wait(tenSeconds);
        }

        private static void OpenFile_IDisposable()
        {
            DisposableResourceHolder obj = new DisposableResourceHolder();
            obj.OpenFile();
            Console.WriteLine("End of test method.");
        }
        #endregion IDispose samples

        #region Pure managed resource sample
        private static void ApplyManagedResourceWithGC()
        {
            ApplyManagedResource();

            Wait(tenSeconds);
            CallGC();
            Wait(tenSeconds);

            CallGC();
            Wait(tenSeconds);
        }

        private static void ApplyManagedResource()
        {
            ManagedResourceHolder obj = new ManagedResourceHolder();
            obj.ApplyResource();
            Console.WriteLine("End of test method.");
        }
        #endregion Pure managed resource sample

        #region helper
        private static void Wait(TimeSpan time)
        {
            Console.WriteLine("Wait for {0} seconds...", time.TotalSeconds);
            Thread.Sleep(time);
        }

        private static void CallGC()
        {
            Console.WriteLine("Call GC.Collect...");
            GC.Collect();
        }

        private static void MonitorFileStatus()
        {
            Console.WriteLine("Start to monitor file: {0}", FileHolder.TestFileName);
            Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    bool isInUse = FileHolder.IsFileInUse(FileHolder.TestFileName);

                    string message = isInUse ? "File is in use." : "File is released.";
                    Console.WriteLine(message);
                    Thread.Sleep(oneSeconds);
                }
            });
        }
        #endregion helper
    }
}
