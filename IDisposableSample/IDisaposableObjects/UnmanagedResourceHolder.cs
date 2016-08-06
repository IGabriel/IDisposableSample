using System;
using System.IO;

namespace IDisaposableObjects
{
    public class UnmanagedResourceHolder : FileHolder
    {
        ~UnmanagedResourceHolder()
        {
            StreamInUse.Dispose();
            Console.WriteLine("Destructor, released file stream.");
        }
    }
}
