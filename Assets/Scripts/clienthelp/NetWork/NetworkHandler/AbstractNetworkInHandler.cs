using System;
using System.Net.Sockets;
using Common;

namespace Network
{
    public abstract class AbstractNetworkInHandler
    {
        public abstract void OnReceived(IConnection connection, NetworkBuffer buffer, int offset, int size);
        public abstract void OnReceived(IConnection connection, Object msg);

        private AbstractNetworkInHandler nextInHandler;

        protected object obj;

        public AbstractNetworkInHandler NextInHandler
        {
            get { return nextInHandler; }
            set { nextInHandler = value; }
        }

        public Boolean FireBuffReceived(IConnection connection, NetworkBuffer buffer, int offset, int size)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnReceived(connection, buffer, offset, size);
                return true;
            }
            return false;
        }

        public Boolean FireObjectReceived(IConnection connection, Object msg)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnReceived(connection, msg);
                return true;
            }
            return false;
        }

        public Boolean FireConnected(IConnection connection, SocketError result)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnConnected(connection, result);
                return true;
            }
            return false;
        }

        public Boolean FireDisconnected(IConnection connection, SocketError error)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnDisconnected(connection, error);
                return true;
            }
            return false;
        }

        public Boolean FireRequestTimeout(IConnection connection, int userData)
        {
            if (NextInHandler != null)
            {
                NextInHandler.OnRequestTimeout(connection, userData);
                return true;
            }

            return false;
        }
        
        // after connected to remote
        public virtual void OnConnected(IConnection connection, SocketError result)
        {
            FireConnected(connection, result);
        }

        // after connection disconnect(local, remote)
        public virtual void OnDisconnected(IConnection connection, SocketError error)
        {
            FireDisconnected(connection, error);
        }

        public virtual void OnRequestTimeout(IConnection connection, int userData)
        {
            FireRequestTimeout(connection, userData);
        }
    }
}
