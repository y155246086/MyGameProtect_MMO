using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common;
using Network;
using ProtoBuf;
using com.kz.protocol;
//using ProtoBuffersMessage;

namespace Messages
{
	public class MySerializer
	{
		private IProtobufSerializer protobufSerializer;
        private static  MySerializer mySerializer = new MySerializer();
        public static int MAX_OUTMESSAGE_SIZE = 2048;

        private MySerializer()
        {
        }

        public static  MySerializer GetInstance()
        {
            return mySerializer;
        }
        public void Initialize(bool useTypeMode)
		{
            //if (useTypeMode)
            //    protobufSerializer = new TypeModelProtobufSerializer(new ProtobufSerializer());
         //   else
                protobufSerializer = new MetaDataProtobufSerializer();
		}

		public bool Serialize<T>(NetworkBuffer outBuffer, T instance)
		{
            outBuffer.GetStream().Position = outBuffer.WriteOffset;
            protobufSerializer.Serialize(outBuffer.GetStream(), instance);
            outBuffer.WriteOffset = (int)outBuffer.GetStream().Position;
            return true;
		}

        public Object DeserializeByteBuffer(Common.NetworkBuffer inBuffer, int offset, int size, Type protoType)
		{
            MemoryStream stream = new MemoryStream(inBuffer.GetBuffer(), offset, size);
			Object outObject = null;
			try
			{
				outObject = protobufSerializer.Deserialize(stream, protoType);
                inBuffer.ReadOffset += size;
			}
			catch (Exception ex)
			{
				Logger.Error(ex.StackTrace);
			}
			return outObject;
		}

	}
}
