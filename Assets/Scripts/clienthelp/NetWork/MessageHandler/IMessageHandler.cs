using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Common;
using System.Net.Sockets;

namespace Network
{
	public interface IMessageHandler
	{
		void handleMessage(IConnection connection, Network.Message message);
		void handleConnectionActive(IConnection connection, SocketError result);
		void handleConnectionInactive(IConnection connection, SocketError result);
		void handleRequestTimeout(IConnection connection, int userData);
	}
}
