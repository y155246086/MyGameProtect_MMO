using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Common;

namespace Network
{
	public abstract class IConnection
	{
		protected NetworkHandlerPipeline handlerPipeline = new NetworkHandlerPipeline();

        protected int channelId = 0;

		public abstract void ConnectAsync(IPEndPoint localAddress, IPEndPoint remoteAddress);

		public abstract void Disconnect();

        public bool Send(byte[] buffer, int offset, int count, int protocolId, int callback, int channelId = 1)
		{
			if (!isConnected())
			{
				return false;
			}

            this.channelId = channelId;

			NetworkBuffer outBuffer = new NetworkBuffer(count + 4, true);
			outBuffer.WriteInt32(protocolId);
			outBuffer.WriteBytes(buffer, offset, count);

			if (handlerPipeline.OutHeader != null)
			{
				handlerPipeline.OutHeader.Send(this, outBuffer.GetBuffer(), outBuffer.ReadOffset, outBuffer.ReadableBytes);
				return true;
			}
			else
			{
				return _Send(outBuffer.GetBuffer(), outBuffer.ReadOffset, outBuffer.ReadableBytes);
			}
		}

		public bool Send(Network.Message obj, int channelId = 1)
		{
			Logger.Error("send message:" + obj.ProtocolId);
			if (!isConnected())
			{
				return false;
			}

            this.channelId = channelId;

			if (handlerPipeline.OutHeader != null)
			{
				handlerPipeline.OutHeader.Send(this, obj);
				return true;
			}
			else
			{
				return false;
			}
		}

		internal abstract bool _Send(byte[] buffer, int offset, int count);

		public abstract bool isConnected();

		public abstract void Update();

		public void SetNetworkInitializer(AbstractNetworkInitializer networkInitilializer)
		{
			networkInitilializer.Initial(handlerPipeline);
		}

		public void AddNetworkHandler(AbstractNetworkInHandler handler)
		{
			handlerPipeline.AddHandler(handler);
		}
		public void AddNetworkHandler(AbstractNetworkOutHandler handler)
		{
			handlerPipeline.AddHandler(handler);
		}
	}
}
