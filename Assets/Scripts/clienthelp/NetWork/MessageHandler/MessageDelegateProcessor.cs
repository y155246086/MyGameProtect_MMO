using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
	public class MessageDelegateProcessor
	{
		private MessageDelegateInitializer delegateInitializer;
		public MessageDelegateProcessor(MessageDelegateInitializer delegateInitializer)
		{
			this.delegateInitializer = delegateInitializer;

		}

		public bool HandleMessage(Message msg,IConnection connection)
		{
			MessageDelegateNode msgDelegate = delegateInitializer.getMessageDelegate(msg.GetType());
			if (msgDelegate == null)
				return false;
			msgDelegate.ReceiveMessage(msg);

			if (msgDelegate.isShortConnect)
				connection.Disconnect();
			return true;
		}
	}
}
