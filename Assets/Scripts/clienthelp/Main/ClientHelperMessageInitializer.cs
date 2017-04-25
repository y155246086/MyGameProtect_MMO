using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Messages;
using com.kz.protocol;

namespace ClientHelper
{
	public class ClientHelperMessageInitializer : AbstractMessageInitializer
	{
		private ClientNetHelper clientHelper;
        public ClientHelperMessageInitializer(ClientNetHelper clientHelper)
		{
			this.clientHelper = clientHelper;
            logger = clientHelper.getDefaultLogger();
		}
		public override void Initilial()
		{
			//Auto generate this code according to the attribute of Message
			//AddMessage(1, typeof(TestMessage));
			AddMessage(typeof(AC_LoginAuthMessage));
			AddMessage(typeof(AC_ConnectMessage));
			AddMessage(typeof(AC_CreateAccountMessage));
			AddMessage(typeof(AC_BindAccountMessage));
			AddMessage(typeof(AC_ChangePasswordMessage));
            AddMessage(typeof(GC_RecastMessage));
            AddMessage(typeof(GC_SpritePosMessage));
			
		}
	}
}
