using System;
using System.IO;

namespace IDisaposableObjects
{
    public class DestructorForUnmanaged : FileHolder
    {
        ~DestructorForUnmanaged()
        {
            StreamInUse.Dispose();
            Console.WriteLine("Destructor, released file stream.");
        }
    }
}
