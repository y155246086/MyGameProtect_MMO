using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChooseServerPanel : IViewBase {
    private Button server01Button = null;
    private Button server02Button = null;
    protected override void OnStart()
    {
        uiLayer = UIPanelLayers.NormalLayer;
        server01Button = panelObj.transform.Find("Server01").GetComponent<Button>();
        server02Button = panelObj.transform.Find("Server02").GetComponent<Button>();

        server01Button.onClick.AddListener(OnButtonClickHandler);
        server02Button.onClick.AddListener(OnButtonClickHandler);
    }

    private void OnButtonClickHandler()
    {
        GUIManager.ShowView(PanelNameConst.LoginPanel);
        GUIManager.HideView(PanelNameConst.ChooseServerPanel);
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
