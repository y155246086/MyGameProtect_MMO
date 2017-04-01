using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    public class MessageDelegateInitializer
    {
        private Dictionary<Type, MessageDelegateNode> msgReceiveDelegates = new Dictionary<Type, MessageDelegateNode>();

        public void AddMessageReceiveDelegate(Type msgType, MessageDelegateNode msgDelegate)
        {
            msgReceiveDelegates.Remove(msgType);
            msgReceiveDelegates.Add(msgType, msgDelegate);
        }

        public MessageDelegateNode getMessageDelegate(Type msgType)
        {
            MessageDelegateNode msgDelegate = null;
            msgReceiveDelegates.TryGetValue(msgType, out msgDelegate);
            return msgDelegate;
        }
    }
}
