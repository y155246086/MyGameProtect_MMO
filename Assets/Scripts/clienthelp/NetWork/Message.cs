using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Network
{
	public abstract class Message
	{
		public int ProtocolId
		{
			get { return GetProtocolId(this.GetType()); }
		}

		public virtual int Result
		{
			get { return 0; }
		}

		internal NetworkBuffer EncodeWithProtocolID()
		{
			NetworkBuffer outBuffer = new NetworkBuffer(NetworkParameters._MAX_SEND_BUFFER_SIZE, true);
			outBuffer.WriteInt32(this.ProtocolId);
			EncodeBody(outBuffer);
			return outBuffer;
		}

		public virtual Message DecodeBody(NetworkBuffer inBuffer, int offset, int count)
		{
			return this;
		}

		public virtual void EncodeBody(NetworkBuffer outBuffer)
		{
		}

		public static int GetProtocolId(Type messageType)
		{
			var attributes = messageType.GetCustomAttributes(typeof(MessageAttribute), false);
			if (attributes.Length != 0)
				return (attributes[0] as MessageAttribute).ProtocolId;
			throw new Exception("MessageAttribute in " + messageType);
		}
	}
}
