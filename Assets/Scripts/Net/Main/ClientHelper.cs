using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Network;
using Messages;
using Common;
using LuaFramework;


namespace ClientHelper
{
    public enum NetType
    {
        NETTYPE_TCP = 1,
        //NETTYPE_UDP = 2,
    };

    public enum NetStatus
    {
        NETSTATUS_INIT = 0,                     //网络初始状态
        NETSTATUS_SOCKCONNECTING = 1,           //Sock正在连接    
        NETSTATUS_SOCKCONNECTED,                //socket连接完毕,需要AuthToken
        NETSTATUS_AUTHTOKEN,                    //AuthToken发送完毕，等待应答
        NETSTATUS_NEED_QUERYINITINFO,           //需要queryInitInfo，此时连接成功
        NETSTATUS_CONNECTED,                    //连接成功，AuthToken成功后进入，queryInitInfo过程是在connect成功之后，不影响连接
        NETSTATUS_DISCONNECTED,                 //连接断开，重新connectis
        NETSTATUS_CLOSED,                       //连接关闭，需要重新login
    };

    //网络层对逻辑层的通知类型
    public enum InformType
    {
        INFORM_CONNECT = 0,
        INFORM_DISCONNECT = 1,
    };
    public partial class ClientConfigDataUpdateHelper
    {
        private const String NEW_VERSION_FILE_NAME = "new_version.properties";
        private const String VERSION_FILE_NAME = "version.properties";
        private String configDataPath;
        private String versionFilePath;
        private String downloadWwwUrl;

        private Dictionary<String, Int32> versionMap = new Dictionary<string, Int32>();

        public void Initialize(String configDataPath, String versionFilePath, String downloadWwwUrl)
        {
            this.configDataPath = configDataPath;
            this.versionFilePath = versionFilePath;
            this.downloadWwwUrl = downloadWwwUrl;
        }

        //加载本地版本文件
        private void loadCurrentVersionFile()
        {
            if (!File.Exists(versionFilePath + VERSION_FILE_NAME))
                return;
            StreamReader sr = new StreamReader(versionFilePath + VERSION_FILE_NAME, Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                String[] tmp = line.Split('=');
                versionMap[tmp[0]] = System.Int32.Parse(tmp[1]);
            }
            sr.Close();
        }

        private void LoadWwwVersionFileAndUpdate()
        {
            //下载最新的版本文件
            NetUtil.WwwDownload(downloadWwwUrl + VERSION_FILE_NAME + "?" + GetTimeStamp(), versionFilePath + NEW_VERSION_FILE_NAME);

            if (!File.Exists(versionFilePath + NEW_VERSION_FILE_NAME))
                return;

            //版本文件对比和配置文件下载
            StreamReader sr = new StreamReader(versionFilePath + NEW_VERSION_FILE_NAME, Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                String[] tmp = line.Split('=');
                String fileName = tmp[0];
                Int32 newVersion = System.Int32.Parse(tmp[1]);

                if (versionMap.ContainsKey(fileName))
                {
                    Int32 oldVersion = versionMap[fileName];
                    if (oldVersion != newVersion)
                    {
                        NetUtil.WwwDownload(downloadWwwUrl + fileName + "?" + GetTimeStamp(), configDataPath + fileName);
                    }
                }
                else
                    NetUtil.WwwDownload(downloadWwwUrl + fileName + "?" + GetTimeStamp(), configDataPath + fileName);

                versionMap[tmp[0]] = newVersion;
            }
            sr.Close();

            //替换新的本地版本文件
            File.Delete(versionFilePath + VERSION_FILE_NAME);
            File.Move(versionFilePath + NEW_VERSION_FILE_NAME, versionFilePath + VERSION_FILE_NAME);
        }

        public void CheckUpdate()
        {
            this.loadCurrentVersionFile();
            this.LoadWwwVersionFileAndUpdate();
        }

        public static Int64 GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
    public partial class ClientNetHelper
    {
        private MessageDelegateInitializer msgDelegateInitializer;
        private MessageDelegateProcessor msgDelegateProcessor;
        private AbstractMessageInitializer messageInitializer;
        public Action<string> logger;

        public MessageDelegateProcessor MsgDelegateProcessor
        {
            get { return msgDelegateProcessor; }
        }



        public void Initialize(Action<string> logger)
        {
            this.logger = logger;
            MySerializer.GetInstance().Initialize(false);
            msgDelegateInitializer = new MessageDelegateInitializer();
            msgDelegateProcessor = new MessageDelegateProcessor(msgDelegateInitializer);
            messageInitializer = new ClientHelperMessageInitializer(this);
            //SetOnConnectAuthRes();
        }

        public void ProcessMessage(NetworkBuffer buffer, int length)
        {
            UnityEngine.Debug.Log(":::" + buffer.GetBuffer().Length);
            //for (int i = 0; i < buffer.GetBuffer().Length; i++)
            //{
            //    UnityEngine.Debug.Log(i + ":" + buffer.GetBuffer()[i]);
            //}
            int protocolId = buffer.ReadInt32();
            int bodySize = length - 4;
            Type msgType = messageInitializer.getMessageType(protocolId);
            if (msgType == null)
            {
                UnityEngine.Debug.Log(":::此消息C#无处理，进入ulua检测处理,消息号：" + protocolId + "-消息长度：" + length);
                Util.CallMethod("Network", "OnSocket", protocolId,new ByteBuffer(buffer.GetBuffer()));
                return;
                OnProcessMessageFail(buffer, protocolId, bodySize);
                return;
            }
            MessageC msg = (MessageC)Activator.CreateInstance(msgType);
            if (msg == null)
            {
                OnProcessMessageFail(buffer, protocolId, bodySize);
                return;
            }
            msg.DecodeBody(buffer, buffer.ReadOffset, bodySize);
            if (AbstractMessageInitializer.getDefaultLogger() != null)
                AbstractMessageInitializer.getDefaultLogger()("message recv:" + msgType.FullName + ", messageId:" + protocolId);

            if (!msgDelegateProcessor.HandleMessage(msg))
            {
                //ReceviedFail(buffer, protocolId, bodySize);
                return;
                //messageHandler = messageInitializer.GetDefaultMessageHandler();
                //if (messageHandler != null)
                //    messageHandler.handleMessage(connection, msg);
            }
        }

        private void OnProcessMessageFail(NetworkBuffer buffer, int protocolId, int bodySize)
        {
            AbstractMessageInitializer.getDefaultLogger()("消息解析失败，messageID：" + protocolId);
            buffer.SkipBytes(bodySize);
        }

    }
}
