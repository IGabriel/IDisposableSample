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
        private static readonly TimeSpan _waitTime = new TimeSpan(0, 0, 10);

        static void Main(string[] args)
        {
            //DestructorSimpleWithGC();
            DestructorSimpleWithoutGC();


            Console.WriteLine("End of the application");
        }

        #region DestructorSimple
        private static void DestructorSimpleWithGC()
        {
            DestructorSimple();
            GC.Collect();
            Thread.Sleep(_waitTime);
        }

        private static void DestructorSimpleWithoutGC()
        {
            DestructorSimple();
            Thread.Sleep(_waitTime);
        }

        private static void DestructorSimple()
        {
            DestructorObject obj = new DestructorObject();
            obj.ApplyResource();
        }
        #endregion DestructorSimple

        //static void Temp()
        //{
        //    Console.WriteLine("Creating a big memory stream...");
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        const int BlockSize = 1024 * 1024;
        //        for (int i = 0; i < 1024; i++)
        //        {
        //            byte[] buffer = new byte[BlockSize];
        //            stream.Write(buffer, 0, BlockSize);
        //            Console.WriteLine("Created block: {0}", i);
        //        }
        //    }
        //    Console.WriteLine("Stream disposed.");
        //}
    }
}
