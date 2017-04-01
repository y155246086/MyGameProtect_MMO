using UnityEngine;
using System.Collections;
using Network;
//using UnityEngine.Networking;
using Common;
using ClientHelper;
using UnityEngine.Experimental.Networking;
using LuaFramework;

public class NetworkManagerC : MonoBehaviour
{
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        _instance = this;
        clientHellper = new ClientNetHelper();
        clientHellper.Initialize(NetLog);
        DontDestroyOnLoad(_instance.gameObject);
    }
    private static NetworkManagerC _instance;
   // public static readonly string IP = "http://192.168.99.153";
    //public static readonly string PORT = "29300";

    public static readonly string IP = "http://192.168.99.130";
    public static readonly string PORT = "21020";
    public long PLAYERID = 100;
    public long CLIENTID = 101;
    public ClientNetHelper clientHellper;

    public static NetworkManagerC Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("NetworkManagerC").AddComponent<NetworkManagerC>();
            }
            return _instance;
        }
    }


    public void SendMessage(MessageC msg)
    {
        if(msg == null)
        {
            return;
        }
        StartCoroutine(_SendMessage(msg));
    }
    public void ProcessReciveveMessage(NetworkBuffer buffer,int length)
    {
        if(clientHellper != null)
        {
            clientHellper.ProcessMessage(buffer, length);
        }
    }

    private IEnumerator _SendMessage(MessageC msg)
    {
        string http = IP + ":" + PORT;
        UnityWebRequest request = new UnityWebRequest(http, UnityWebRequest.kHttpVerbPUT);
        var uploadHandler = CreateUploadHandle(msg, PLAYERID, CLIENTID);
        var downloadHandler = new DownLoadHandler();
        request.uploadHandler = uploadHandler;
        request.downloadHandler = downloadHandler;
        yield return request.Send();
        while(!downloadHandler.IsComplete)
        {
            yield return 0;
        }
        uploadHandler.Dispose();
        downloadHandler.Clear();
    }

    public void NetLog(string msg)
    {
        Debug.Log("Message:" + msg);
    }


    private UploadHandlerRaw CreateUploadHandle(MessageC msg, long playerId, long clientId)
    {
        NetworkBuffer outBuffer = msg.EncodeWithProtocolID(playerId,clientId);
        int readableByteLength = outBuffer.ReadableBytes;
        byte[] readableBytes = outBuffer.ReadBytes(0,readableByteLength);
        UploadHandlerRaw uploader = new UploadHandlerRaw(readableBytes);
        return uploader;
    }


    public void SendMessageForHttp(ByteBuffer buffer, int ProtocolId)
    {
        if (buffer == null)
        {
            return;
        }
        StartCoroutine(_SendMessageForHttp(buffer, ProtocolId));
    }

    private IEnumerator _SendMessageForHttp(ByteBuffer buffer, int ProtocolId)
    {
        string http = IP + ":" + PORT;
        UnityWebRequest request = new UnityWebRequest(http, UnityWebRequest.kHttpVerbPUT);

        NetworkBuffer outBuffer = new NetworkBuffer(NetworkParameters._MAX_SEND_BUFFER_SIZE, true);
        outBuffer.WriteInt64(PLAYERID);
        outBuffer.WriteInt64(CLIENTID);
        outBuffer.WriteInt32(ProtocolId);

        ByteBuffer buf = new ByteBuffer(buffer.ToBytes());
        
        byte[] bs = buf.ReadBytes();//这种方式会吧头部多余的一个Int值省略掉
        outBuffer.WriteBytes(bs, 0, bs.Length);

        int readableByteLength = outBuffer.ReadableBytes;
        byte[] readableBytes = outBuffer.ReadBytes(0, readableByteLength);

        UploadHandlerRaw uploadHandler = new UploadHandlerRaw(readableBytes);

        var downloadHandler = new DownLoadHandler();
        request.uploadHandler = uploadHandler;
        request.downloadHandler = downloadHandler;
        yield return request.Send();
        while (!downloadHandler.IsComplete)
        {
            yield return 0;
        }
        uploadHandler.Dispose();
        downloadHandler.Clear();
        buffer.Close();
        buf.Close();
    }
}

public class DownLoadHandler: DownloadHandlerScript
{
    private NetworkBuffer buffer = null;
    private int dataLength = 0;
    public bool IsComplete { get; set; }
    protected override byte[] GetData()
    {
        return null;
    }

    protected override bool ReceiveData(byte[] data, int dataLength)
    {
        if(data == null || dataLength == 0)
        {
            return false;
        }
        buffer.WriteBytes(data, 0, dataLength);
        return true;
    }

    protected override void ReceiveContentLength(int contentLength)
    {
        dataLength = contentLength;
        buffer = new NetworkBuffer(contentLength, true); 
    }

    protected override void CompleteContent()
    {
        NetworkManagerC.Instance.ProcessReciveveMessage(buffer, dataLength);
        IsComplete = true;
    }

    protected override float GetProgress()
    {
        return 0;
    }

    public void Clear()
    {
        base.Dispose();
    }
}

