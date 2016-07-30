using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IDisaposableObjects
{
    public class DestructorObject : IResourceConsumer
    {
        private MemoryStream _stream;

        public DestructorObject()
        {
            _stream = new MemoryStream();
            Console.WriteLine("Constructor, created memory stream...");
        }

        ~DestructorObject()
        {
            _stream.Close();
            Console.WriteLine("Destructor, released memory stream...");
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
