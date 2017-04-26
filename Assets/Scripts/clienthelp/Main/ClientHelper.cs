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
        private bool pollingMode;
        private ProtocolType type;
        private bool userTypeMode;
        private IConnection connection;
        private MessageDelegateInitializer msgDelegateInitializer;
        private MessageDelegateProcessor msgDelegateProcessor;
        private Action<String> logger;

        public MessageDelegateProcessor MsgDelegateProcessor
        {
            get { return msgDelegateProcessor; }
        }


        private AbstractMessageInitializer messageInitializer;
        private AbstractNetworkInitializer networkInitializer;

        public void Initialize(bool pollingMode, bool userTypeMode, ProtocolType type, Action<String> logger, IMessageHandler disconnectHandler)
        {
            this.pollingMode = pollingMode;
            this.userTypeMode = userTypeMode;
            this.type = type;
            this.logger = logger;

            MySerializer.GetInstance().Initialize(userTypeMode);
            msgDelegateInitializer = new MessageDelegateInitializer();
            msgDelegateProcessor = new MessageDelegateProcessor(msgDelegateInitializer);
            messageInitializer = new ClientHelperMessageInitializer(this);
            messageInitializer.SetConnectionInactiveHandler(disconnectHandler);
            networkInitializer = new ClientHelperNetworkInitializer(messageInitializer, msgDelegateProcessor);

            SetOnConnectAuthRes();
        }

        private bool _SendMessage(Network.Message message)
        {
            if (connection != null)
            {
                return connection.Send(message);
            }

            return false;
        }
        public bool SendMessage(Network.Message message)
        {
            return _SendMessage(message);
        }
        public void SendMessageForLua(ByteBuffer buffer, int ProtocolId)
        {
            if (buffer == null)
            {
                return;
            }
            NetworkBuffer outBuffer = new NetworkBuffer(NetworkParameters._MAX_SEND_BUFFER_SIZE, true);
            outBuffer.WriteInt32(ProtocolId);

            ByteBuffer buf = new ByteBuffer(buffer.ToBytes());

            byte[] bs = buf.ReadBytes();//这种方式会吧头部多余的一个Int值省略掉
            outBuffer.WriteBytes(bs, 0, bs.Length);

            int readableByteLength = outBuffer.ReadableBytes;
            byte[] readableBytes = outBuffer.ReadBytes(0, readableByteLength);

            connection._Send(readableBytes, 0, readableByteLength);
        }

        public void Update()
        {
            if (connection != null)
            {
                connection.Update();
            }
        }

        public bool IsConnected()
        {
            if (connection == null)
                return false;
            return connection.isConnected();
        }

        public void Disconnect()
        {
            if (connection != null)
                connection.Disconnect();
        }

        public Action<String> getDefaultLogger()
        {
            return logger;
        }
    }
}
