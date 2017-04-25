using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Messages;
using com.kz.message.proto;
using System;
using System.Collections.Generic;

public class TestUISend : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button denglu = this.transform.Find("Denglu").GetComponent<Button>();
        denglu.onClick.AddListener(OnDenglu);
        Button Fasong = this.transform.Find("Fasong").GetComponent<Button>();
        Fasong.onClick.AddListener(OnFasong);
        NetworkManagerProxy.Instance.Client.SetOnLoginAuthRes(OnAuthenticationHandle);
        NetworkManagerProxy.Instance.Client.SetOnLoginGameRes(OnLoginGameServerHandle);
        NetworkManagerProxy.Instance.Client.SetOnRecastRes(OnRecastRes);
        NetworkManagerProxy.Instance.Client.SetOnSpritePosRes(OnSpritePosRes);
    }
    public GameObject targetPrefab;
    private void OnSpritePosRes(GC_SpritePosMessage obj)
    {
        string s = "";
        for (int i = 0; i < obj.Protocol.curPos.Count; i++)
        {
            s += obj.Protocol.curPos[i] + ",";
        }
        Debug.LogError("--------------OnSpritePosRes||||||" + s);
        Target.position = new Vector3(obj.Protocol.curPos[0], obj.Protocol.curPos[1], obj.Protocol.curPos[2]);
        if(targetPrefab != null)
        {
            GameObject p = GameObject.Instantiate<GameObject>(targetPrefab);
            p.transform.Reset();
            p.transform.position = Target.position;
        }
    }

    private void OnRecastRes(GC_RecastMessage obj)
    {
        Debug.LogError("--------------OnRecastRes" + obj.Protocol.id);
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
    public Transform StartP;
    public Transform EndP;
    public Transform Target;
    private void OnFasong()
    {
        CG_RecastMessage messgae = new CG_RecastMessage();
        messgae.Protocol.start.Add(StartP.position.x);
        messgae.Protocol.start.Add(StartP.position.y);
        messgae.Protocol.start.Add(StartP.position.z);

        messgae.Protocol.end.Add(EndP.position.x);
        messgae.Protocol.end.Add(EndP.position.y);
        messgae.Protocol.end.Add(EndP.position.z);
        NetworkManagerProxy.Instance.Client.SendMessage(messgae);
    }

    private void OnDenglu()
    {
        OnLoginGameHandler();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
