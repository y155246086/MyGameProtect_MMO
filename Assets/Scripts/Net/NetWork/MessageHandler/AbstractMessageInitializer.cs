using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Network
{
	public abstract class AbstractMessageInitializer
	{
		private Dictionary<int, Type> msgs = new Dictionary<int, Type>();
        protected static Action<String> logger;

		public AbstractMessageInitializer()
		{
			Initilial();
		}

		public abstract void Initilial();

		public Type getMessageType(int protocolId)
		{
			Type msgType = null;
			msgs.TryGetValue(protocolId, out msgType);
			return msgType;
		}

		public void AddMessage(Type msg)
		{
            msgs.Add(MessageC.GetProtocolId(msg), msg);
		}

		public void RemoveMessage(Type msg)
		{
            msgs.Remove(MessageC.GetProtocolId(msg));
		}

        public static Action<String> getDefaultLogger()
        {
            return logger;
        }
	}
}
