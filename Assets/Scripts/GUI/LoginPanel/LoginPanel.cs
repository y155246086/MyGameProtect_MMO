using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.kz.protocol;
using com.kz.message.proto;
using System;
using Messages;
public class LoginPanel : IViewBase
{
    private Button loginGameButton = null;
    private Button chooseServerButton = null;
    protected override void OnStart()
    {
        uiLayer = UIPanelLayers.NormalLayer;
        loginGameButton = panelObj.transform.Find("LoginGameButton").GetComponent<Button>();
        chooseServerButton = panelObj.transform.Find("ChooseServerButton").GetComponent<Button>();

        loginGameButton.onClick.AddListener(OnLoginGameHandler);
        chooseServerButton.onClick.AddListener(OnChooseServerHandler);

        NetworkManagerProxy.Instance.Client.SetCallBackRes<GC_LoginGameMessage>(OnLoginGameServerHandle);
        NetworkManagerProxy.Instance.Client.SetOnLoginAuthRes(OnAuthenticationHandle);
        NetworkManagerProxy.Instance.Client.SetOnLoginGameRes(OnLoginGameServerHandle);
    }

    private void OnLoginGameServerHandle(GC_LoginGameMessage obj)
    {
        Debug.LogError("--------------LoginGame");
    }

    private void OnAuthenticationHandle(Messages.AC_LoginAuthMessage obj)
    {
        Debug.LogError("ABCDDDFSDKFJSDFJLSJDFKJSD");
    }

    private void OnChooseServerHandler()
    {
        GUIManager.ShowView(PanelNameConst.ChooseServerPanel);
        GUIManager.HideView(PanelNameConst.LoginPanel);
    }

    private void OnLoginGameHandler()
    {
        //LoginGame();
        GameStateManager.LoadScene(2);
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

    protected override void OnShow(params object[] args)
    {

    }
    protected override void OnHide()
    {

    }
    protected override void OnDestory()
    {
        
    }

}
