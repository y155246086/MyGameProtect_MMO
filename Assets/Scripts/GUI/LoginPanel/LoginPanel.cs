using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.kz.protocol;
using com.kz.message.proto;
using System;
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

        //NetworkManagerProxy.Instance.Client.SetOnLoginAuthRes(OnAuthenticationHandle);
        //NetworkManagerProxy.Instance.Client.SetOnConnectGameRes(OnConnectionGameSeverHandle);
        //NetworkManagerProxy.Instance.Client.SetOnLoginGameRes(OnLoginGameServerHandle);
        //NetworkManagerProxy.Instance.Client.SetOnCreateRoleRes(OnCreateRoleHandle);
    }

    private void OnChooseServerHandler()
    {
        GUIManager.ShowView(PanelNameConst.ChooseServerPanel);
        GUIManager.HideView(PanelNameConst.LoginPanel);
    }

    private void OnLoginGameHandler()
    {
        GameStateManager.LoadScene(2);
    }

    private void OnClickHandler()
    {
        Debuger.Log("登陆游戏");
        GUIManager.ShowView(PanelNameConst.LoadingPanel);
    }
    public void LoginGame(string accountName, string password)
    {
        DeviceInfoPro device = new DeviceInfoPro();
        device.deviceName = "test";
        device.deviceType = 1;
        device.UDID = "test";

        System.Random random = new System.Random();
        int randomSeed = random.Next(Int32.MinValue, Int32.MaxValue);

        NetworkManagerProxy.Instance.Client.LoginAS(SysConfig.GetInstance().GetStringProperties(SysConfig.K_AUTH_SERVER_IP),
            SysConfig.GetInstance().GetIntProperties(SysConfig.K_AUTH_SERVER_PORT),
            accountName, password, "" + randomSeed,
            SysConfig.GetInstance().GetIntProperties(SysConfig.K_CHANNELID),
            SysConfig.GetInstance().GetStringProperties(SysConfig.K_VERSION), device);
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
