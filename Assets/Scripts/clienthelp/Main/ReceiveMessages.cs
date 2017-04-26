using System;
using System.Collections.Generic;
using Common;
using Messages;
using Network;

namespace ClientHelper
{
    public partial class ClientNetHelper
	{
		public void  SetOnLoginAuthRes(Action<AC_LoginAuthMessage> onLoginAuthRes)
		{
			MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(Network.Message msg) { onLoginAuthRes((AC_LoginAuthMessage)msg); };
			delegateNode.isShortConnect = true;
			msgDelegateInitializer.AddMessageReceiveDelegate(typeof(AC_LoginAuthMessage), delegateNode);  
		}

		public void SetOnCreateAccountAuthRes(Action<AC_CreateAccountMessage> onCreateAccountAuthRes)
		{
			MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(Network.Message msg) { onCreateAccountAuthRes((AC_CreateAccountMessage)msg); };
			delegateNode.isShortConnect = true;
			msgDelegateInitializer.AddMessageReceiveDelegate(typeof(AC_CreateAccountMessage), delegateNode);
		}

		public void SetOnBindAccountAuthRes(Action<AC_BindAccountMessage> onBindAccountAuthRes)
		{
			MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(Network.Message msg) { onBindAccountAuthRes((AC_BindAccountMessage)msg); };
			delegateNode.isShortConnect = true;
			msgDelegateInitializer.AddMessageReceiveDelegate(typeof(AC_BindAccountMessage), delegateNode);
		}

		public void SetOnChangePasswordAuthRes(Action<AC_ChangePasswordMessage> onChangePasswordAuthRes)
		{
			MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(Network.Message msg) { onChangePasswordAuthRes((AC_ChangePasswordMessage)msg); };
			delegateNode.isShortConnect = true;
			msgDelegateInitializer.AddMessageReceiveDelegate(typeof(AC_ChangePasswordMessage), delegateNode);
		}
        public void SetOnLoginGameRes(Action<GC_LoginGameMessage> onLoginGameRes)
        {
            MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(Network.Message msg) { onLoginGameRes((GC_LoginGameMessage)msg); };
            msgDelegateInitializer.AddMessageReceiveDelegate(typeof(GC_LoginGameMessage), delegateNode);
        }
        public void SetOnRecastRes(Action<GC_RecastMessage> onLoginGameRes)
        {
            MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(Network.Message msg) { onLoginGameRes((GC_RecastMessage)msg); };
            msgDelegateInitializer.AddMessageReceiveDelegate(typeof(GC_RecastMessage), delegateNode);
        }
        public void SetOnSpritePosRes(Action<GC_SpritePosMessage> onLoginGameRes)
        {
            MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(Network.Message msg) { onLoginGameRes((GC_SpritePosMessage)msg); };
            msgDelegateInitializer.AddMessageReceiveDelegate(typeof(GC_SpritePosMessage), delegateNode);
        }
        /// <summary>
        /// 设置消息回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onLoginGameRes"></param>
        public void SetCallBackRes<T>(Action<T> onCallBackRes) where T : Network.Message
        {
            MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(Network.Message msg) { onCallBackRes((T)msg); };
            msgDelegateInitializer.AddMessageReceiveDelegate(typeof(T), delegateNode);
        }
        public void SetOnConnectAuthRes()
        {

            MessageDelegateNode delegateNode = new MessageDelegateNode();
            delegateNode.receiveAction = delegate(Network.Message msg) { OnRecvConnectAuthRes((AC_ConnectMessage)msg); };
            msgDelegateInitializer.AddMessageReceiveDelegate(typeof(AC_ConnectMessage), delegateNode);
        }
        public void OnRecvConnectAuthRes(AC_ConnectMessage message)
        {
            if (waitSendAuthMsg != null)
            {
                connection.Send(waitSendAuthMsg);
                waitSendAuthMsg = null;
            }
        }

    }
}
