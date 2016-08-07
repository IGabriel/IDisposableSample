using System;
using System.IO;

namespace IDisaposableObjects
{
    public class NondisposableHolder : FileHolder
    {
        ~NondisposableHolder()
        {
            StreamInUse.Dispose();
            Console.WriteLine("Destructor, released file stream.");
        }
    }
}
