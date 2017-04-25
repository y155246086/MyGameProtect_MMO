using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.message.proto;
using com.finger.common.net.proto;


namespace Messages
{
    [Network.Message(ProtocolId = 3, IsRequest = false)]
	public class GC_SpritePosMessage : ProtocolMessage<SpritePosRes>
	{
		public override int Result { get { return protocol.id; } }
	}
}
