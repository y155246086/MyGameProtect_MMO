using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    public class CorruptedFrameException : Exception
    {
        public CorruptedFrameException(string msg):base(msg)
        {
        }
    }
}
