using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.game.message.proto;

namespace Messages
{
	[Message(ProtocolId = Protocols.P_CG_GAME_LOGIN, IsRequest = true)]
	public class CG_LoginGameMessage : ProtocolMessage<LoginGameReq>
	{
	}
}
