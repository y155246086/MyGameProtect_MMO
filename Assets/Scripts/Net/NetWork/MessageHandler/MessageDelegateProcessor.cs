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

        public bool HandleMessage(MessageC msg)
		{
			MessageDelegateNode msgDelegate = delegateInitializer.getMessageDelegate(msg.GetType());
			if (msgDelegate == null)
				return false;
			msgDelegate.ReceiveMessage(msg);
			return true;
		}
	}
}
