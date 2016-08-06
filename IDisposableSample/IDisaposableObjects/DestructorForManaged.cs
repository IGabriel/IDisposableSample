using System;
using System.Collections.Generic;
using System.Linq;

namespace IDisaposableObjects
{
    public class DestructorForManaged
    {
        private static readonly TimeSpan wait = new TimeSpan(0, 0, 20);
        private List<byte[]> _list;
        private Random r;

        public DestructorForManaged()
        {
            _list = new List<byte[]>();
            r = new Random();
            Console.WriteLine("Constructor, created list.");
        }

        //~DestructorForManaged()
        //{
        //    _list.Select(item => item = null);
        //    _list.Clear();
        //    _list = null;
        //    Console.WriteLine("End of Destructor");
        //}

        public void ApplyResource()
        {
            for (int i = 0; i < ConstValues.CycleTimes; i++)
            {
                byte[] buffer = new byte[ConstValues.BlockSize];
                r.NextBytes(buffer);
                _list.Add(new byte[ConstValues.BlockSize]);
                Console.WriteLine("apply new block to memory stream, count of list: {0}", _list.Count);
            }
        }
    }
}
