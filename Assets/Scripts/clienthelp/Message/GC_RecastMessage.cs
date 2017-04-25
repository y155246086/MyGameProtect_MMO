using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.message.proto;
using com.finger.common.net.proto;


namespace Messages
{
    [Network.Message(ProtocolId = 2, IsRequest = false)]
	public class GC_RecastMessage : ProtocolMessage<RecastRes>
	{
		public override int Result { get { return protocol.result; } }
	}
}
