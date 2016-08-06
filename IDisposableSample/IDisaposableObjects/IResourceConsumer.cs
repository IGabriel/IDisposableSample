using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDisaposableObjects
{
    interface IResourceConsumer
    {
        void ApplyResource();
    }
}
