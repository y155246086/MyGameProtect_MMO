using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Network
{
	public abstract class AbstractMessageInitializer
	{
		private Dictionary<int, IMessageHandler> msgHandlers = new Dictionary<int, IMessageHandler>();
		private Dictionary<int, Type> msgs = new Dictionary<int, Type>();
		private IMessageHandler defaultMsgHandler;
		private IMessageHandler connectionActiveHandler;
		private IMessageHandler connectionInactiveHandler;
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

		public IMessageHandler GetMessageHandler(int protocolId)
		{
			IMessageHandler msgHandler = null;
			msgHandlers.TryGetValue(protocolId, out msgHandler);
			return msgHandler;
		}

		public IMessageHandler GetConnectionActiveHandler()
		{
			return connectionActiveHandler;
		}

		public IMessageHandler GetConnectionInactiveHandler()
		{
			return connectionInactiveHandler;
		}

		public IMessageHandler GetDefaultMessageHandler()
		{
			return defaultMsgHandler;
		}

		public void AddMessageHanlder(Type msg, IMessageHandler handler)
		{
			msgHandlers.Add(Message.GetProtocolId(msg), handler);
		}

		public void AddMessage(Type msg)
		{
			msgs.Add(Message.GetProtocolId(msg), msg);
		}

		public void RemoveMessage(Type msg)
		{
			msgs.Remove(Message.GetProtocolId(msg));
		}

		public void SetConnectionActiveHandler(IMessageHandler handler)
		{
			connectionActiveHandler = handler;
		}

		public void SetConnectionInactiveHandler(IMessageHandler handler)
		{
			connectionInactiveHandler = handler;
		}

		public void SetDefaultMsgHandler(IMessageHandler handler)
		{
			defaultMsgHandler = handler;
		}

        public static Action<String> getDefaultLogger()
        {
            return logger;
        }
	}
}
