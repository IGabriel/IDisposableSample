﻿using System;
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
        private const string TestFileName = @"TestFile.txt";
        private const string Hybrid1 = @"Hybrid_1.txt";
        private const string Hybrid2 = @"Hybrid_2.txt";

        private static readonly TimeSpan oneSeconds = new TimeSpan(0, 0, 1);
        private static readonly TimeSpan fiveSeconds = new TimeSpan(0, 0, 5);
        private static readonly TimeSpan thritySecnods = new TimeSpan(0, 0, 30);

        static void Main(string[] args)
        {
            //MemoryTest();
            //FileTest();

            //OpenFileWithManagedCodeWithUsing();
            //OpenFileWithManagedCodeWithoutUsing();

            //OpenFileWithUnmanagedCodeWithUsing();
            //OpenFileWithUnmanagedCodeWithoutUsing();

            //HybridTestWithUsing();
            //HybridTestWithoutUsing();

            Console.WriteLine("End of the application");
        }

        #region Managed sample
        private static void OpenFileWithManagedCodeWithUsing()
        {
            OpenFileWithUsing(HolderType.Managed, new string[] { TestFileName });
        }

        private static void OpenFileWithManagedCodeWithoutUsing()
        {
            OpenFileWithoutUsing(HolderType.Managed, new string[] { TestFileName });
        }
        #endregion Managed sample

        #region Unmanaged sample
        private static void OpenFileWithUnmanagedCodeWithUsing()
        {
            OpenFileWithUsing(HolderType.Unmanaged, new string[] { TestFileName });
        }

        private static void OpenFileWithUnmanagedCodeWithoutUsing()
        {
            OpenFileWithoutUsing(HolderType.Unmanaged, new string[] { TestFileName });
        }
        #endregion Unmanaged sample

        #region Test
        private static void MemoryTest()
        {
            AllocateMemory();

            CallGC();
            Wait(fiveSeconds);
        }

        private static void AllocateMemory()
        {
            Console.WriteLine("Applying memory...");

            const int size = 1024 * 1024 * 1024;
            byte[] buffer = new byte[size];

            Random r = new Random();
            r.NextBytes(buffer);

            MemoryStream stream = new MemoryStream();
            stream.Write(buffer, 0, size);

            Console.WriteLine("Applied memory.");
            Wait(fiveSeconds);
        }

        private static void FileTest()
        {
            MonitorFileStatus(TestFileName);

            OpenFile();

            CallGC();
            Wait(fiveSeconds);
        }

        private static void OpenFile()
        {
            FileStream stream  = File.Open(TestFileName, FileMode.Append, FileAccess.Write);
            Wait(fiveSeconds);
        }
        #endregion Test

        #region Hybrid sample
        private static void HybridTestWithUsing()
        {
            OpenFileWithUsing(HolderType.Hybrid,
                new string[] { Hybrid1, Hybrid2 });
        }

        private static void HybridTestWithoutUsing()
        {
            OpenFileWithoutUsing(HolderType.Hybrid,
                new string[] { Hybrid1, Hybrid2 });
        }
        #endregion Hybrid sample

        #region helper
        private static void OpenFileWithUsing(HolderType type, string[] files)
        {
            Array.ForEach(files, file => MonitorFileStatus(file));
            using (IFileHolder holder = CreateHolder(type))
            {
                holder.OpenFile();
                Console.WriteLine("File is opened.");

                Wait(fiveSeconds);
            }

            Console.WriteLine("Out of using statement.");
            Wait(fiveSeconds);
        }

        private static void OpenFileWithoutUsing(HolderType type, string[] files)
        {
            Array.ForEach(files, file => MonitorFileStatus(file));
            OpenFile(type);

            Wait(fiveSeconds);
            CallGC();
            Wait(fiveSeconds);
        }

        private static void OpenFile(HolderType type)
        {
            IFileHolder holder = CreateHolder(type);
            holder.OpenFile();
            Console.WriteLine("End of test method.");
        }

        private static IFileHolder CreateHolder(HolderType type)
        {
            IFileHolder holder = null;
            switch(type)
            {
                case HolderType.Managed:
                    holder = new ManagedFileHolder(TestFileName);
                    break;
                case HolderType.Unmanaged:
                    holder = new UnmanagedFileHolder(TestFileName);
                    break;
                case HolderType.Hybrid:
                    holder = new HybridHolder(Hybrid1, Hybrid2);
                    break;
                default:
                    throw new NotSupportedException(string.Format("Not support type: {0}", type));
            }
            return holder;
        }

        private static void Wait(TimeSpan time)
        {
            Console.WriteLine("Wait for {0} seconds...", time.TotalSeconds);
            Thread.Sleep(time);
        }

        private static void CallGC()
        {
            Console.WriteLine("Call GC.Collect...");
            GC.Collect();

            //For Hybrid, without using
            //GC.Collect(0);
        }

        private static void MonitorFileStatus(string fileName)
        {
            Console.WriteLine("Start to monitor file: {0}", fileName);
            Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    bool isInUse = IsFileInUse(fileName);

                    string messageFormat = isInUse ? "File {0} is in use." : "File {0} is released.";
                    Console.WriteLine(messageFormat, fileName);
                    Thread.Sleep(oneSeconds);
                }
            });
        }


        private static bool IsFileInUse(string fileName)
        {
            bool isInUse = true;
            FileStream stream = null;
            try
            {
                stream = File.Open(fileName, FileMode.Append, FileAccess.Write);
                isInUse = false;
            }
            catch
            {
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
            return isInUse;
        }
        #endregion helper
    }
}
