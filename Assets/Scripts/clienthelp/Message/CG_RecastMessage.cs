using System;
using System.Collections.Generic;
using Network;
using com.kz.protocol;
using Common;
using com.kz.message.proto;
using com.finger.common.net.proto;

namespace Messages
{
    [Network.Message(ProtocolId = 1, IsRequest = true)]
    public class CG_RecastMessage : ProtocolMessage<RecastReq>
	{
	}
}
