using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
	public class MessageAttribute : Attribute
	{
		private int protocolId;

		public int ProtocolId
		{
			get { return protocolId; }
			set { protocolId = value; }
		}

		private bool isRequest;

		public bool IsRequest
		{
			get { return isRequest; }
			set { isRequest = value; }
		}
	}
}
