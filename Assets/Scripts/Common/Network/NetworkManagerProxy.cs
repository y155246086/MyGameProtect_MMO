using UnityEngine;
using System.Collections;
using ClientHelper;
using com.kz.message.proto;
using com.kz.protocol;
using Common;
using Network;
using System.Net;
using System.Net.Sockets;

public class NetworkManagerProxy : MonoBehaviour, IMessageHandler
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        clientHelper.Update();
	}



	void Awake(){
		if (isInit) {
			return;
		}
		isInit = true;
		if (instance != null) {
			instance = null;
			Destroy(gameObject);
		}
		instance = this;
        //netLoger = new NetManagerLog();
		clientHelper = new ClientNetHelper ();
#if UNITY_IPHONE
        clientHelper.Initialize(true,true, System.Net.Sockets.ProtocolType.Tcp, OnLogHandle,this);
#else 
        clientHelper.Initialize(true,false, System.Net.Sockets.ProtocolType.Tcp, OnLogHandle,this);
#endif
        //clientHelper.Initialize(true, System.Net.Sockets.ProtocolType.Tcp, OnLogHandle);
        /*StartCoroutine(ClientUpdate());*/
        Init();
		DontDestroyOnLoad (instance.gameObject);
	}

    private static NetworkManagerProxy instance;
	private bool isInit = false;
	private ClientNetHelper clientHelper;
    //private NetManagerLog netLoger;

    private bool startLog = true;

    public bool StartLog
    {
        get { return startLog; }
        set { startLog = value; }
    }



    private long currentTime;



    public void OnLogHandle(string content)
    {
        if (startLog)
        {
            Debug.Log("OnLogHandle:" + content);
        }
    }


	public ClientNetHelper Client{
		get{
			return clientHelper;
		}
	}
    public static NetworkManagerProxy Instance
    {
		get{
			if(instance == null){
                instance = new GameObject("NetworkManager").AddComponent<NetworkManagerProxy>();
			}
			return instance;
		}
	}

    public void Init()
    {
        //Client.SetOnShipListRes(GameBagManager.Instance.OnShipBagMessageHandle);
        //Client.SetOnBagListRes(GameBagManager.Instance.OnGoodsBagMessageHandle);
        //Client.SetOnProduceInfo(CommonServerDataManager.Instance.OnProductInfoHandle);
        //Client.SetOnCommanderListRes(GameBagManager.Instance.OnCaptainBagMessageHandle);
        //Client.SetOnBattleInfoRes(BattleSelectManager.Instance.OnBattleInfoMessageHandle);
        //Client.SetOnTacticsInfoPro(ZhanShuDataManager.Instance.OnZhanShuListMessageHandle);
        //Client.SetOnUpdateTacticsInfo(ZhanShuDataManager.Instance.OnTacticsUpdateInfoMessageHandle);
        //Client.SetOnUpdateTacticsNum(ZhanShuDataManager.Instance.OnTacticsOpenNumMessageHandle);
        //Client.SetOnBagUnstackItemUpdatetRes(GameBagManager.Instance.OnUnstackItemUpdatetMessageHandle);
        //Client.SetOnCurrentAreaRecordRes(InstanceManager.Instance.ProcessCurrentAreaRecordMessage);
    }

    
   
    void OnApplicationQuit()
    {
        this.Disconnect();
    }

    public void Disconnect()
    {
        Debug.Log("disconnect client!!!");
        if (Client != null)
        {
            Client.Disconnect();
        }
    }

    public void handleMessage(IConnection connection, Network.Message message){ }
    public void handleConnectionActive(IConnection connection, SocketError result) { }
    public void handleConnectionInactive(IConnection connection, SocketError result)
    {
        Debug.Log("=========");
    }
    public void handleRequestTimeout(IConnection connection, int userData) { }
}
