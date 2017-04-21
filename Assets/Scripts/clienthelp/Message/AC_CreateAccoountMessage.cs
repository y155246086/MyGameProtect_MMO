using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.message.proto;


namespace Messages
{
    [Network.Message(ProtocolId = Protocols.P_AC_AUTH_CREATE_ACCOUNT, IsRequest = false)]
	public class AC_CreateAccountMessage : ProtocolMessage<CreateAccountRes>
	{
		public override int Result { get { return protocol.result; } }
	}
}
