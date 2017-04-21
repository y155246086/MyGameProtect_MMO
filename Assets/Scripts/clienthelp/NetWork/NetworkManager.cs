using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Network
{
    public class NetworkManager : INetworkManager
    {
        private static NetworkManager instance = new NetworkManager();

        public NetworkManager()
        {
        }

        public static INetworkManager Instance
        {
            get { return instance; }
        }

        public static INetworkManager GetInstance()
        {
            return instance;
        }

        public IConnection CreateConnection( ProtocolType type)
        {
            if (type == ProtocolType.Tcp)
            {
                return new TCPConnection();
            }
            return null;
        }

        public IConnection SearchConnectoin(int connectionID)
        {
            return null;
        }

        public bool RemoveConnection(int connectionID)
        {
            return true;
        }
    }
}
