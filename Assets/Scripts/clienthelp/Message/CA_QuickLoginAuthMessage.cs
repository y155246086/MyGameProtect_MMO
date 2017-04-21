using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.message.proto;


namespace Messages
{
    [Network.Message(ProtocolId = Protocols.P_CA_AUTH_QUICK_LOGIN, IsRequest = true)]
	public class CA_QuickLoginAuthMessage : ProtocolMessage<LocalLoginReq>
	{
	}
}
