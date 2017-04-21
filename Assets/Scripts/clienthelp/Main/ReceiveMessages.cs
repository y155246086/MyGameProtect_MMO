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


        public void SetOnConnectAuthRes()
        {

            Debuger.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA,暂时删除了的");
        }


    }
}
