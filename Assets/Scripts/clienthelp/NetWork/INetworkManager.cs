using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Network
{
    public interface INetworkManager
    {
        IConnection CreateConnection(ProtocolType type);

        IConnection SearchConnectoin(int connectionID);

        bool RemoveConnection(int connectionID);

    }
}
