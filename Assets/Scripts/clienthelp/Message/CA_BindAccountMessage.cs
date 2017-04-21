using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.message.proto;


namespace Messages
{
    [Network.Message(ProtocolId = Protocols.P_CA_AUTH_BIND_ACCOUNT, IsRequest = true)]
	public class CA_BindAccountMessage : ProtocolMessage<BindAccountReq>
	{

	}
}
