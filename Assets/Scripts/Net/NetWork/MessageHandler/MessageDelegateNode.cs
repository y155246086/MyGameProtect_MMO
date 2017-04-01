using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Common;

namespace Network
{
    public class MessageDelegateNode
    {
        public Action<MessageC> receiveAction;
		public bool isShortConnect = false;

        public void ReceiveMessage(MessageC message)
        {
            receiveAction(message);
        }
    }
}
