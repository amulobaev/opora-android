using System;
using System.Collections.Generic;
using System.Text;

namespace Opora
{
    public interface ISave
    {
        void Save(string filename, byte[] data);
    }
}