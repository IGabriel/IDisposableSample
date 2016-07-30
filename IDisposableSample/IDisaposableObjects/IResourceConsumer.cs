using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDisaposableObjects
{
    public interface IResourceConsumer
    {
        void ApplyResource();
    }
}
