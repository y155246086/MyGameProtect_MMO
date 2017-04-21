using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Common;

namespace Network
{
    public class MessageOutProcessor : AbstractNetworkOutHandler
    {
        public override void Send(IConnection connection, byte[] buffer, int offset, int size)
        {
            sendBuffDown(connection, buffer, offset, size);
        }
        public override void Send(IConnection connection, Object msg)
        {
            sendObjectDown(connection, msg);
        }
    }
}
