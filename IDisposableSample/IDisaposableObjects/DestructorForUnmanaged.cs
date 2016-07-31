using System;
using System.IO;

namespace IDisaposableObjects
{
    public class DestructorForUnmanaged : IResourceConsumer
    {
        private MemoryStream _stream;

        public DestructorForUnmanaged()
        {
            _stream = new MemoryStream();
            Console.WriteLine("Constructor, created memory stream.");
        }

        ~DestructorForUnmanaged()
        {
            _stream.Dispose();
            Console.WriteLine("Destructor, released memory stream.");
        }

        public void ApplyResource()
        {
            for(int i = 0; i < ConstValues.CycleTimes; i ++)
            {
                Console.WriteLine("apply new block to memory stream, id: {0}", i);
                Byte[] buffer = new byte[ConstValues.BlockSize];
                _stream.Write(buffer, 0, ConstValues.BlockSize);
            }
        }
    }
}
