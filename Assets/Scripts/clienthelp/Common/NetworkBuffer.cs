using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common
{
    public class NetworkBuffer
    {
        MemoryStream ms;
        BinaryReader br;
        BinaryWriter bw;
        bool isBigEndian;
        int readOffset;

        public int ReadOffset
        {
            get { return readOffset; }
            set
            {
                if (value > ms.Length || value > writeOffset)
                {
                    throw new IndexOutOfRangeException();                  
                }
                else
                {
                    readOffset = value;
                }
            }
        }
        int writeOffset;

        public int WriteOffset
        {
            get { return writeOffset; }
            set
            {
                if (value > ms.Capacity || value < readOffset)
                {
                    throw new IndexOutOfRangeException();                    
                }
                else
                {
                    writeOffset = value;
                }
            }
        }

        public int Capacity
        {
            get { return ms.Capacity; }
        }

        public int ReadableBytes
        {
            get { return writeOffset - readOffset; }
        }

        public bool Readable
        {
            get { return readOffset < writeOffset; }
        }

        public int WritableBytes
        {
            get { return ms.Capacity - writeOffset; }
        }

        public void ResetReadOffset()
        {
            readOffset = 0;
        }

        public void ResetWriteOffset()
        {
            readOffset = 0;
            writeOffset = 0;
        }

        public NetworkBuffer(int capacity, bool isBigEndian)
        {
            ms = new MemoryStream(capacity);
            br = new BinaryReader(ms);
            bw = new BinaryWriter(ms);
            this.isBigEndian = isBigEndian;
        }

        public NetworkBuffer(byte[] buffer, int offset, int count, bool isBigEndian)
        {
            ms = new MemoryStream(buffer, offset, count);
            br = new BinaryReader(ms);
            bw = new BinaryWriter(ms);
            readOffset = 0;
            writeOffset = count;
            this.isBigEndian = isBigEndian;
        }

        public void DiscardReadBytes()
        {
            int _length = writeOffset - readOffset;
            if (readOffset > 0)
            {
                if (_length > 0)
                {
                    byte[] _buffer = ReadBytes(readOffset, _length);
                    ms.Position = 0;
                    bw.Write(_buffer, 0, _length);
                    writeOffset = _length;
                    readOffset = 0;
                }
                else
                {
                    ms.Position = 0;
                    ResetWriteOffset();
                }
            }
        }

        public void SkipBytes(int length)
        {
            if ((readOffset + length) > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                readOffset += length;
            }
        }

        public bool EnsureWritableBytes(int writableBytes)
        {
            if (writableBytes > (ms.Capacity - writeOffset))
            {
                return false;
            }
            return true;
        }

        public byte ReadByte(int offset)
        {
            if (offset + 1 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 1;
            return br.ReadByte();
        }

        public byte ReadByte()
        {
            return ReadByte(readOffset);
        }

        public Int16 ReadInt16(int offset)
        {
            if (offset + 2 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 2;
            if (isBigEndian && BitConverter.IsLittleEndian)
            {
                return BigEndianTransfer.ToInt16(br.ReadBytes(2));
            }
            else
            {
                return br.ReadInt16();
            }
        }

        public Int16 ReadInt16()
        {
            return ReadInt16(readOffset);
        }


        public UInt16 ReadUInt16(int offset)
        {
            if (offset + 2 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 2;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                return BigEndianTransfer.ToUInt16(br.ReadBytes(2));
            }
            else
            {
                return br.ReadUInt16();
            }
        }
        public UInt16 ReadUInt16()
        {
            return ReadUInt16(readOffset);
        }

        public Int32 ReadInt32(int offset)
        {
            if (offset + 4 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 4;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                return BigEndianTransfer.ToInt32(br.ReadBytes(4));
            }
            return br.ReadInt32();
        }
        public Int32 ReadInt32()
        {
            return ReadInt32(readOffset);
        }

        public Int64 ReadInt64(int offset)
        {
            if (offset + 8 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 8;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                return BigEndianTransfer.ToInt64(br.ReadBytes(8));
            }
            return br.ReadInt64();
        }
        public Int64 ReadInt64()
        {
            return ReadInt64(readOffset);
        }

        public String ReadString(int offset)
        {
            Int16 size = ReadInt16(offset);
            if (size > 0)
            {
                return Encoding.UTF8.GetString(ReadBytes(size));
            }
            return "";
        }

        public String ReadString()
        {
            return ReadString(readOffset);
        }

        public UInt32 ReadUInt32(int offset)
        {
            if (offset + 4 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 4;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                return BigEndianTransfer.ToUInt32(br.ReadBytes(4));
            }

            return br.ReadUInt32();
        }

        public UInt32 ReadUInt32()
        {
            return ReadUInt32(readOffset);
        }

        public byte[] ReadBytes(int offset, int count)
        {
            if (offset + count > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }

            ms.Position = offset;
            readOffset = offset + count;
            return br.ReadBytes(count);
        }
        public byte[] ReadBytes(int count)
        {
            return ReadBytes(readOffset, count);
        }

        public byte GetByte(int offset)
        {
            if (offset + 1 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            return br.ReadByte();
        }
        public byte GetByte()
        {
            return GetByte(readOffset);
        }

        public Int16 GetInt16(int offset)
        {
            if (offset + 2 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                return BigEndianTransfer.ToInt16(br.ReadBytes(2));
            }
            return br.ReadInt16();
        }
        public Int16 GetInt16()
        {
            return GetInt16(readOffset);
        }

        public UInt16 GetUInt16(int offset)
        {
            if (offset + 2 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                return BigEndianTransfer.ToUInt16(br.ReadBytes(2));
            }
            return br.ReadUInt16();
        }

        public UInt16 GetUInt16()
        {
            return GetUInt16(readOffset);
        }

        public Int32 GetInt32(int offset)
        {
            if (offset + 4 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                return BigEndianTransfer.ToInt32(br.ReadBytes(4));
            }
            return br.ReadInt32();
        }
        public Int32 GetInt32()
        {
            return GetInt32(readOffset);
        }

        public UInt32 GetUInt32(int offset)
        {
            if (offset + 4 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                return BigEndianTransfer.ToUInt32(br.ReadBytes(4));
            }
            return br.ReadUInt32();
        }
        public UInt32 GetUInt32()
        {
            return GetUInt32(readOffset);
        }

        public void WriteByte(int offset, byte value)
        {
            if (offset + 1 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 1;
            bw.Write(value);
        }
        public void WriteByte(byte value)
        {
            WriteByte(writeOffset, value);
        }

        public void PutInt16(int offset, Int16 value)
        {
            ms.Position = offset;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }

        public void WriteInt16(int offset, Int16 value)
        {
            if (offset + 2 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 2;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }
        public void WriteInt16(Int16 value)
        {
            WriteInt16(writeOffset, value);
        }

        public void WriteUInt16(int offset, UInt16 value)
        {
            if (offset + 2 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 2;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }
        public void WriteUInt16(UInt16 value)
        {
            WriteUInt16(writeOffset, value);
        }

        public void WriteInt32(int offset, Int32 value)
        {
            if (offset + 4 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 4;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }
        public void WriteInt32(Int32 value)
        {
            WriteInt32(writeOffset, value);
        }

        public void WriteUInt32(int offset, UInt32 value)
        {
            if (offset + 4 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 4;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }
        public void WriteUInt32(UInt32 value)
        {
            WriteUInt32(writeOffset, value);
        }

        public void WriteInt64(int offset, Int64 value)
        {
            if (offset + 8 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 8;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }
        public void WriteInt64(Int64 value)
        {
            WriteInt64(writeOffset, value);
        }

        public void WriteString(int offset, String text)
        {
            if (text == null || text.Length <= 0)
            { 
                WriteInt16(0);
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(text);

            if (offset + 2 + bytes.Length > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }

            WriteInt16((short)bytes.Length);

            WriteBytes(bytes, 0, bytes.Length);
        }
        public void WriteString(String text)
        {
            WriteString(writeOffset, text);
        }

        public void WriteBytes(int offset, byte[] value, int index, int count)
        {
            if (offset + count > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + count;
            bw.Write(value, index, count);
        }
        public void WriteBytes(byte[] value, int index, int count)
        {
            WriteBytes(writeOffset, value, index, count);
        }

        public byte[] GetContent()
        {
            return ms.ToArray();
        }

        public MemoryStream GetStream()
        {
            return ms;
        }

        public byte[] GetBuffer()
        {
            return ms.GetBuffer();
        }
      
    }

  		
}
