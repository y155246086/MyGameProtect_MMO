using System;
using System.Collections.Generic;
using Common;
using Messages;
using Network;

namespace ClientHelper
{
    public partial class ClientNetHelper
	{
		public void SetOnLoginGameRes(Action<GC_LoginGameMessage> onLoginGameRes)
		{
			MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(MessageC msg) { onLoginGameRes((GC_LoginGameMessage)msg); };
			msgDelegateInitializer.AddMessageReceiveDelegate(typeof(GC_LoginGameMessage), delegateNode);
		}

		

		public void SetOnCreateRoleRes(Action<GC_CreateRoleMessage> onCreateRoleRes)
		{
			MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(MessageC msg) { onCreateRoleRes((GC_CreateRoleMessage)msg); };
			msgDelegateInitializer.AddMessageReceiveDelegate(typeof(GC_CreateRoleMessage), delegateNode);
		}

		public void SetOnACKRes(Action<GC_ACKMessage> onACKRes)
		{
			MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(MessageC msg) { onACKRes((GC_ACKMessage)msg); };
			msgDelegateInitializer.AddMessageReceiveDelegate(typeof(GC_ACKMessage), delegateNode);
		}


       
    }
}
