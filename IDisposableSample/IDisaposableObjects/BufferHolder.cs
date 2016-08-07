using System;
using System.Collections.Generic;
using System.Linq;

namespace IDisaposableObjects
{
    public class BufferHolder : IBufferHolder
    {
        internal const int BlockSize = 1024 * 1024;
        internal const int CycleTimes = 1024;

        private List<byte[]> _list = new List<byte[]>();
        private Random _r = new Random();

        public void ApplyResource()
        {
            for (int i = 0; i < CycleTimes; i++)
            {
                byte[] buffer = new byte[BlockSize];
                _r.NextBytes(buffer);
                _list.Add(new byte[BlockSize]);
                Console.WriteLine("apply new block to memory stream, count of list: {0}", _list.Count);
            }
        }

        //~ManagedResourceHolder()
        //{
        //    _list.Select(array => array = null);
        //    _list = null;
        //    _r = null;
        //}
    }
}
