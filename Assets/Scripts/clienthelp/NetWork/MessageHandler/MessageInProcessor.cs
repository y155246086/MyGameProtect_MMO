using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Common;
using System.Net.Sockets;
using LuaFramework;

namespace Network
{
    public class MessageInProcessor : AbstractNetworkInHandler
    {
        private AbstractMessageInitializer messageInitializer;
        private MessageDelegateProcessor msgDelegateProcessor;
        public MessageInProcessor(AbstractMessageInitializer messageInitializer, MessageDelegateProcessor msgDelegateProcessor)
        {
            this.messageInitializer = messageInitializer;
            this.msgDelegateProcessor = msgDelegateProcessor;
        }

        public override void OnReceived(IConnection connection, NetworkBuffer buffer, int offset, int size)
        {
            int protocolId = buffer.ReadInt32();
            int bodySize = size - 4;
            Type msgType = messageInitializer.getMessageType(protocolId);
            if (msgType == null)
            {
                UnityEngine.Debug.Log(":::此消息C#无处理，进入ulua检测处理,消息号：" + protocolId + "-消息长度：" + size);
                LuaFramework.Util.CallMethod("Network", "OnSocket", protocolId, new ByteBuffer(buffer.GetBuffer()));
                return;
            }
            if (msgType == null)
            {
                this.ReceviedFail(buffer,protocolId,bodySize);
                return;
            }
            Message msg = (Message)Activator.CreateInstance(msgType);
            if (msg == null)
            {
                this.ReceviedFail(buffer, protocolId, bodySize);
                return;
            }
            msg.DecodeBody(buffer, buffer.ReadOffset, bodySize);
            IMessageHandler messageHandler = messageInitializer.GetMessageHandler(protocolId);

            if (AbstractMessageInitializer.getDefaultLogger() != null)
                AbstractMessageInitializer.getDefaultLogger()("message recv:" + msgType.FullName + ", messageId:" + protocolId);

            if (messageHandler != null)
            {
                messageHandler.handleMessage(connection, msg);
            }
            else
            {
                if (!msgDelegateProcessor.HandleMessage(msg, connection))
                {
                    //ReceviedFail(buffer, protocolId, bodySize);
                    return;
                    //messageHandler = messageInitializer.GetDefaultMessageHandler();
                    //if (messageHandler != null)
                    //    messageHandler.handleMessage(connection, msg);
                }
            }
        }

        private void ReceviedFail(NetworkBuffer buffer,int protocolId,int bodySize)
        {
            AbstractMessageInitializer.getDefaultLogger()("消息解析失败，messageID：" + protocolId);
            buffer.SkipBytes(bodySize);

        }
        public override void OnReceived(IConnection connection, Object msg)
        {

        }

        public override void OnRequestTimeout(IConnection connection, int userData)
        {
            //通过MessageHandler处理超时，还是直接通过Disconnect处理？
            //TODO:参数如何传递，protocolId？callback？
            int protocolId = userData;

            IMessageHandler messageHandler = messageInitializer.GetMessageHandler(protocolId);
            if (messageHandler != null)
            {
                messageHandler.handleRequestTimeout(connection, userData);
            }

            base.OnRequestTimeout(connection, userData);
        }

        public override void OnConnected(IConnection connection, SocketError result)
        {
            FireConnected(connection, result);
            if (messageInitializer.GetConnectionActiveHandler() != null)
            {
                messageInitializer.GetConnectionActiveHandler().handleConnectionActive(connection, result);
            }
        }

        // after connection disconnect(local, remote)
        public override void OnDisconnected(IConnection connection, SocketError error)
        {
            if (messageInitializer.GetConnectionInactiveHandler() != null)
            {
                messageInitializer.GetConnectionInactiveHandler().handleConnectionInactive(connection, error);
            }
        }
    }
}
