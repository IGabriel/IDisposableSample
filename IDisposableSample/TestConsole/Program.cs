using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Text;
using IDisaposableObjects;

namespace TestConsole
{
    class Program
    {
        private static readonly TimeSpan _waitShortTime = new TimeSpan(0, 0, 10);
        private static readonly TimeSpan _waitLongTime = new TimeSpan(0, 0, 30);

        static void Main(string[] args)
        {
            //DestructorForUnmanagedSimpleWithGC();
            //DestructorForUnmanagedSimpleWithoutGC();

            DestructorForManagedSimpleWithGC();


            Console.WriteLine("End of the application");
        }

        #region Destructor for unmanaged ressource simple
        private static void DestructorForUnmanagedSimpleWithGC()
        {
            DestructorForUnmanagedSimple();
            GC.Collect();
            Thread.Sleep(_waitShortTime);
        }

        private static void DestructorForUnmanagedSimpleWithoutGC()
        {
            DestructorForUnmanagedSimple();
            Thread.Sleep(_waitShortTime);
        }

        private static void DestructorForUnmanagedSimple()
        {
            DestructorForUnmanaged obj = new DestructorForUnmanaged();
            obj.ApplyResource();
        }
        #endregion Destructor for unmanaged ressource simple

        #region Destructor for managed ressource simple
        private static void DestructorForManagedSimpleWithGC()
        {
            DestructorForManagedSimple();
            Console.WriteLine("Force garbage collect.");
            GC.Collect();
            Thread.Sleep(_waitShortTime);

            //Console.WriteLine("Try to collect the first time...");
            //Thread.Sleep(_waitShortTime);
            //GC.Collect();

            //Console.WriteLine("Try to collect the second time...");
            //Thread.Sleep(_waitShortTime);
            //GC.Collect();

            //Console.WriteLine("Try to collect the thrid time...");
            //Thread.Sleep(_waitShortTime);
            //GC.Collect();

            //Console.WriteLine("Try to collect the fourth time...");
            //Thread.Sleep(_waitShortTime);
            //GC.Collect();
        }

        private static void DestructorForManagedSimple()
        {
            DestructorForManaged obj = new DestructorForManaged();
            obj.ApplyResource();
        }
        #endregion Destructor for unmanaged ressource simple
    }
}
