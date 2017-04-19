using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
