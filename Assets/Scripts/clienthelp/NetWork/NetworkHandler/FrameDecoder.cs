using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Network
{
    public abstract class  FrameDecoder : AbstractNetworkInHandler
    {
        public override void OnReceived(IConnection connection, NetworkBuffer buffer, int offset, int count)
        {
            NetworkBuffer msgBuffer = buffer;
            //NetworkBuffer msgBuffer = new NetworkBuffer(NetworkParameters._MAX_UNCOMPRESS_MESSAGE_SIZE, true);
            //msgBuffer.WriteBytes(buffer, offset, count);

            try
            {
                while (msgBuffer.Readable)
                {
                    int oldReaderOffset = msgBuffer.ReadOffset;
                    int msgCount = 0;
                    bool result = Decode(msgBuffer, ref msgCount);
                    if (result == false)
                    {
                        if (oldReaderOffset == msgBuffer.ReadOffset)
                        {
                            // Seems like more data is required.
                            // Let us wait for the next notification.
                            break;
                        }
                        else
                        {
                            // Previous data has been discarded.
                            // Probably it is reading on.
                            continue;
                        }
                    }
                    else if (msgCount == 0)
                    {
                        throw new InvalidOperationException(
                                "decode() method must read at least one byte " +
                                "if it returned a frame ");
                    }

                    try
                    {
                        FireBuffReceived(connection, msgBuffer, msgBuffer.ReadOffset, msgCount);
                    }
                    finally
                    {
                        msgBuffer.DiscardReadBytes();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Debug(e.Message);
                Logger.Debug(e.StackTrace);
            }
        }

        public override void OnReceived(IConnection connection, Object msg)
        {
            throw new NotSupportedException("FrameDecoder didn't implement OnReceived(IConnection connection, Object msg)");
        }

        public abstract bool Decode(Common.NetworkBuffer msgBuffer, ref int msgCount);
    }
}
