using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Common;
using System.Net.Sockets;

namespace Network
{
	public abstract class AbstractMessageHandler : IMessageHandler
	{
		public abstract void handleMessage(IConnection connection, Message message);

		public abstract void handleConnectionActive(IConnection connection, SocketError result);

		public virtual void handleConnectionInactive(IConnection connection, SocketError result)
		{

		}

		public virtual void handleRequestTimeout(IConnection connection, int userData)
		{

		}
	}
}
