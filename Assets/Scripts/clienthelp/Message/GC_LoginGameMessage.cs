using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.message.proto;


namespace Messages
{
    [Network.Message(ProtocolId = Protocols.P_GC_GAME_LOGIN, IsRequest = false)]
	public class GC_LoginGameMessage : ProtocolMessage<LoginGameRes>
	{
		public override int Result { get { return protocol.result; } }
	}
}
