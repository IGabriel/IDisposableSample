using System;
using System.IO;

namespace Destructor
{
    class DestructorObject
    {
        public virtual void SayHello()
        {
            Console.WriteLine("Hello, I am 'DestructorSample'");
        }

        private MemoryStream _stream;
        public DestructorObject()
        {
            Console.WriteLine("Constructor for class 'DestructorSample', creating memory stream...");
            _stream = new MemoryStream(1024);
        }

        ~DestructorObject()
        {
            Console.WriteLine("Destructor for class 'DestructorSample', closing memory stream...");
            _stream.Close();
        }
    }
}
