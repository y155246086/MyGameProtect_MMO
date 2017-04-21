using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Network
{
    public class LengthFieldBasedFrameDecoder : FrameDecoder
    {
        private int maxFrameLength;
        private int lengthFieldOffset;
        private int lengthFieldLength;
        private int lengthFieldEndOffset;
        private int lengthAdjustment;
        private int initialBytesToStrip;
		//private bool discardingTooLongFrame;
        private long tooLongFrameLength;
        private long bytesToDiscard;

        public LengthFieldBasedFrameDecoder(
        int maxFrameLength,
        int lengthFieldOffset, int lengthFieldLength,
        int lengthAdjustment, int initialBytesToStrip)
        {
            if (maxFrameLength <= 0)
            {
                throw new ArgumentException(
                        "maxFrameLength must be a positive integer: " +
                        maxFrameLength);
            }

            if (lengthFieldOffset < 0)
            {
                throw new ArgumentException(
                        "lengthFieldOffset must be a non-negative integer: " +
                        lengthFieldOffset);
            }

            if (initialBytesToStrip < 0)
            {
                throw new ArgumentException(
                        "initialBytesToStrip must be a non-negative integer: " +
                        initialBytesToStrip);
            }

            if (lengthFieldLength != 1 && lengthFieldLength != 2 && lengthFieldLength != 4)
            {
                throw new ArgumentException(
                        "lengthFieldLength must be either 1, 2, or 4: " +
                        lengthFieldLength);
            }

            if (lengthFieldOffset > maxFrameLength - lengthFieldLength)
            {
                throw new ArgumentException(
                        "maxFrameLength (" + maxFrameLength + ") " +
                        "must be equal to or greater than " +
                        "lengthFieldOffset (" + lengthFieldOffset + ") + " +
                        "lengthFieldLength (" + lengthFieldLength + ").");
            }

            this.maxFrameLength = maxFrameLength;
            this.lengthFieldOffset = lengthFieldOffset;
            this.lengthFieldLength = lengthFieldLength;
            this.lengthAdjustment = lengthAdjustment;
            lengthFieldEndOffset = lengthFieldOffset + lengthFieldLength;
            this.initialBytesToStrip = initialBytesToStrip;
        }

        public override bool Decode(Common.NetworkBuffer msgBuffer, ref int msgCount)
        {
            if (msgBuffer.ReadableBytes < lengthFieldEndOffset)
            {
                return false;
            }

            int actualLengthFieldOffset = msgBuffer.ReadOffset + lengthFieldOffset;
            int frameLength;
            switch (lengthFieldLength)
            {
                case 1:
                    frameLength = msgBuffer.GetByte(actualLengthFieldOffset);
                    break;
                case 2:
                    frameLength = msgBuffer.GetUInt16(actualLengthFieldOffset);
                    break;
                case 4:
                    frameLength = msgBuffer.GetInt32(actualLengthFieldOffset);
                    break;
                default:
                    throw new CorruptedFrameException("should not reach here");
            }

            if (frameLength < 0)
            {
                msgBuffer.SkipBytes(lengthFieldEndOffset);
                throw new CorruptedFrameException(
                        "negative pre-adjustment length field: " + frameLength);
            }

            frameLength += lengthAdjustment + lengthFieldEndOffset;
            if (frameLength < lengthFieldEndOffset)
            {
                msgBuffer.SkipBytes(lengthFieldEndOffset);
                throw new CorruptedFrameException(
                        "Adjusted frame length (" + frameLength + ") is less " +
                        "than lengthFieldEndOffset: " + lengthFieldEndOffset);
            }

            if (frameLength > maxFrameLength)
            {
                // Enter the discard mode and discard everything received so far.
				//discardingTooLongFrame = true;
                tooLongFrameLength = frameLength;
                bytesToDiscard = frameLength - msgBuffer.ReadableBytes;
                msgBuffer.SkipBytes(msgBuffer.ReadableBytes);
                throw new CorruptedFrameException(String.Format("too long frame, frame length:{0}", tooLongFrameLength));
            }

            // never overflows because it's less than maxFrameLength
            int frameLengthInt = (int)frameLength;
            if (msgBuffer.ReadableBytes < frameLengthInt)
            {
                return false;
            }

            if (initialBytesToStrip > frameLengthInt)
            {
                msgBuffer.SkipBytes(frameLengthInt);
                throw new CorruptedFrameException(
                        "Adjusted frame length (" + frameLength + ") is less " +
                        "than initialBytesToStrip: " + initialBytesToStrip);
            }
            msgBuffer.SkipBytes(initialBytesToStrip);

            msgCount = frameLengthInt - initialBytesToStrip;

            return true;
        }

    }
}
