using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Common;
using System.Net.Sockets;

namespace Network
{
	public abstract class AbstractConnectionHandler : IMessageHandler
	{
		public virtual void handleMessage(IConnection connection, Message message)
		{

		}

		public virtual void handleConnectionActive(IConnection connection, SocketError result)
		{

		}

		public abstract void handleConnectionInactive(IConnection connection, SocketError result);
		public abstract void handleRequestTimeout(IConnection connection, int userData);
	}
}
