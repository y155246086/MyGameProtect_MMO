using UnityEngine;
using System.Collections;
using Messages;

public class TestNetWork : MonoBehaviour {

	// Use this for initialization
    public static TestNetWork Instance = null;
    void Awake()
    {
        Instance = this;
    }
	void Start () {
        //CreateRole();
        //Login();
	}
    public void CreateRole()
    {
        //NetworkManagerC.Instance.clientHellper.SetOnCreateRoleRes(ProcessCreateRole);
        CG_CreateRoleMessage message = new CG_CreateRoleMessage();
        message.Protocol.username = "username111";
        message.Protocol.password = "4545";

        NetworkManagerC.Instance.SendMessage(message);
    }
	public void Login()
    {
        NetworkManagerC.Instance.clientHellper.SetOnLoginGameRes(ProcessLoginGmae);
        CG_LoginGameMessage message = new CG_LoginGameMessage();
        message.Protocol.username = "username";
        message.Protocol.password = "4545";
        NetworkManagerC.Instance.SendMessage(message);
    }

    private void ProcessLoginGmae(GC_LoginGameMessage msg)
    {
        if (msg != null)
        {
            Debug.Log(msg.Result);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void ProcessCreateRole(GC_CreateRoleMessage msg)
    {
        if (msg != null)
        {
            Debug.Log(msg.Result);
        }
    }
}
