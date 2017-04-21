using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    public abstract class AbstractNetworkOutHandler 
    {
        protected object obj;

        private AbstractNetworkOutHandler nextOutHandler;
        public AbstractNetworkOutHandler NextOutHandler
        {
            get { return nextOutHandler; }
            set { nextOutHandler = value; }
        }

        protected void sendBuffDown(IConnection connection, byte[] buffer, int offset, int size)
        {
            if (NextOutHandler != null)
            {
                NextOutHandler.Send(connection, buffer, offset, size);
            }
            else
            {
                connection._Send(buffer, offset, size);
            }
        }

        protected void sendObjectDown(IConnection connection, Object msg)
        {
            if(NextOutHandler != null)
            {
                NextOutHandler.Send(connection, msg);
            }
            else
            {
                throw new NotSupportedException("The last ConnectionOutHandler can't be invoked Send(IConnection connection, Object msg)");
            }
        }

        public abstract void Send(IConnection connection, byte[] buffer, int offset, int size);
        public abstract void Send(IConnection connection, Object msg);
    }
}
