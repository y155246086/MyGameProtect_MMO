using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    public class FixedLengthFrameDecoder : FrameDecoder
    {
        private int length;
        public FixedLengthFrameDecoder(int length)
        {
            this.length = length;
        }

        public override bool Decode(Common.NetworkBuffer msgBuffer, ref int msgCount)
        {
            if (msgBuffer.ReadableBytes < length)
            {
                return false;
            }
            else
            {
                msgCount = length;
                return true;
            }
        }
    }
}
