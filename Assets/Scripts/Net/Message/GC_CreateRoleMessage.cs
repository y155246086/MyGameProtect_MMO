using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.game.message.proto;


namespace Messages
{
	[Message(ProtocolId = Protocols.P_GC_GAME_CREATE_ROLE, IsRequest = false)]
	public class GC_CreateRoleMessage : ProtocolMessage<CreateRoleRes>
	{
		public override int Result { get { return protocol.result; } }
	}
}
