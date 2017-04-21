using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.message.proto;


namespace Messages
{
    [Network.Message(ProtocolId = Protocols.P_CA_AUTH_BIND_ACCOUNT, IsRequest = false)]
	public class AC_BindAccountMessage : ProtocolMessage<BindAccountRes>
    {
		public override int Result { get { return protocol.result; } }
    }
}
