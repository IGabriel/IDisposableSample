using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDisaposableObjects
{
    public class HybridHolder : IBufferHolder
    {
        #region IManagedResourceHolder
        private List<byte[]> _list = new List<byte[]>();
        private Random _r = new Random();

        public void ApplyResource()
        {
            for (int i = 0; i < BufferHolder.CycleTimes; i++)
            {
                byte[] buffer = new byte[BufferHolder.BlockSize];
                _r.NextBytes(buffer);
                _list.Add(new byte[BufferHolder.BlockSize]);
                Console.WriteLine("apply new block to memory stream, count of list: {0}", _list.Count);
            }
        }
        #endregion IManagedResourceHolder
    }
}
