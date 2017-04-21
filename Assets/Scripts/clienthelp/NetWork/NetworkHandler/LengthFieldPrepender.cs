using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Network
{
    public class LengthFieldPrepender : AbstractNetworkOutHandler
    {
        private int lengthFieldLength;
        private int lengthAdjustment;
        private bool lengthIncludesLengthFieldLength;

        /**
     * Creates a new instance.
     *
     * @param lengthFieldLength the length of the prepended length field.
     *                          Only 1, 2, 4 are allowed.
     * @param lengthIncludesLengthFieldLength
     *                          if {@code true}, the length of the prepended
     *                          length field is added to the value of the
     *                          prepended length field.
     *
     * @throws IllegalArgumentException
     *         if {@code lengthFieldLength} is not 1, 2, 4
     */
        public LengthFieldPrepender(
                int lengthFieldLength, int lengthAdjustment, bool lengthIncludesLengthFieldLength)
        {
            if (lengthFieldLength != 1 && lengthFieldLength != 2 &&
                lengthFieldLength != 4)
            {
                throw new ArgumentException(
                        "maxFrameLength must be either 1, 2, 4: " +
                         lengthFieldLength);
            }

            this.lengthFieldLength = lengthFieldLength;
            this.lengthAdjustment = lengthAdjustment;
            this.lengthIncludesLengthFieldLength = lengthIncludesLengthFieldLength;
        }

        public override void Send(IConnection connection, byte[] buffer, int offset, int size)
        {
            int length = size + lengthAdjustment;
            if (lengthIncludesLengthFieldLength)
            {
                length += lengthFieldLength;
            }

            if (length < 0)
            {
                throw new ArgumentException(
                        "Adjusted frame length (" + length + ") is less than zero");
            }
            NetworkBuffer outBuffer = new NetworkBuffer(size + lengthFieldLength, true);

            switch (lengthFieldLength)
            {
                case 1:
                    if (length >= 256)
                    {
                        throw new ArgumentException(
                                "length does not fit into a byte: " + length);
                    }
                    outBuffer.WriteByte((byte)length);
                    break;
                case 2:
                    if (length >= 65536)
                    {
                        throw new ArgumentException(
                                "length does not fit into a short integer: " + length);
                    }
                    outBuffer.WriteInt16((short)length);
                    break;
                case 4:
                    outBuffer.WriteInt32(length);
                    break;
                default:
                    throw new ArgumentException("should not reach here");
            }
            outBuffer.WriteBytes(buffer, offset, size);

            sendBuffDown(connection, outBuffer.GetBuffer(), outBuffer.ReadOffset, outBuffer.ReadableBytes);

        }
        public override void Send(IConnection connection, Object msg)
        {
            if (msg is Message)
            {
                Message outMsg = (Message)msg;
                NetworkBuffer outBuffer = outMsg.EncodeWithProtocolID();
                Send(connection, outBuffer.GetBuffer(), outBuffer.ReadOffset, outBuffer.ReadableBytes);
            }
        }

    }
}
