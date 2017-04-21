using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
	public class ProtocolMessage<T> : Message where T : new()
	{
		protected T protocol = new T();
		public T Protocol { get { return protocol; } }

		public override Message DecodeBody(Common.NetworkBuffer inBuffer, int offset, int count)
		{
            protocol = (T)Messages.MySerializer.GetInstance().DeserializeByteBuffer(inBuffer, offset, count, typeof(T));
			return this;
		}

		public override void EncodeBody(Common.NetworkBuffer outBuffer)
		{
            Messages.MySerializer.GetInstance().Serialize<T>(outBuffer, protocol);
		}
	}
}
