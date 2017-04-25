using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Messages;
using com.kz.message.proto;
using System;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button denglu = this.transform.Find("Denglu").GetComponent<Button>();
        denglu.onClick.AddListener(OnDenglu);
        Button fasong = this.transform.Find("Fasong").GetComponent<Button>();
        denglu.onClick.AddListener(OnFasong);
        NetworkManagerProxy.Instance.Client.SetOnLoginAuthRes(OnAuthenticationHandle);
        //NetworkManagerProxy.Instance.Client.SetOnConnectGameRes(OnConnectionGameSeverHandle);
        NetworkManagerProxy.Instance.Client.SetOnLoginGameRes(OnLoginGameServerHandle);
        //NetworkManagerProxy.Instance.Client.SetOnCreateRoleRes(OnCreateRoleHandle);
    }

    private void OnLoginGameServerHandle(GC_LoginGameMessage obj)
    {
        Debug.LogError("--------------LoginGame");
    }

    private void OnAuthenticationHandle(Messages.AC_LoginAuthMessage obj)
    {
        Debug.LogError("ABCDDDFSDKFJSDFJLSJDFKJSD");
    }


    private void OnLoginGameHandler()
    {
        LoginGame();
        //GameStateManager.LoadScene(2);
    }
    public void LoginGame(string accountName = "aa", string password = "bb")
    {
        DeviceInfoPro device = new DeviceInfoPro();
        device.deviceName = "test";
        device.deviceType = 1;
        device.UDID = "test";

        System.Random random = new System.Random();
        int randomSeed = random.Next(Int32.MinValue, Int32.MaxValue);

        NetworkManagerProxy.Instance.Client.LoginAS("192.168.99.123",
            889,
            accountName, password, "" + randomSeed,
            1,
            "1", device);


    }

    private void OnDenglu()
    {
        OnLoginGameHandler();
    }
    private void OnFasong()
    {
        CG_LoginGameMessage messgae = new CG_LoginGameMessage();
        messgae.Protocol.accountId = (long)111;
        messgae.Protocol.token = "123";

        messgae.Protocol.areaId = 2;
        NetworkManagerProxy.Instance.Client.SendMessage(messgae);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
