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
            logger = clientHelper.logger;
		}
		public override void Initilial()
		{
			//Auto generate this code according to the attribute of Message
			//AddMessage(1, typeof(TestMessage));

			AddMessage(typeof(GC_LoginGameMessage));
			//AddMessage(typeof(GC_CreateRoleMessage));
			
			AddMessage(typeof(GC_ACKMessage));

		}
	}
}
