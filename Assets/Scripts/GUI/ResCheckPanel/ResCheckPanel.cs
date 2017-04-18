using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResCheckPanel : IViewBase
{
    private Text descText = null;
    protected override void OnStart()
    {
        uiLayer = UIPanelLayers.DefaultLayer;
        descText = panelObj.transform.Find("DescText").GetComponent<Text>();
        descText.text = "";
    }

    private void OnResUpdateMessageHandler(string message)
    {
        descText.text = message;
    }

    protected override void OnShow()
    {
        Mogo.Util.EventDispatcher.AddEventListener<string>(GUIEvent.RESOURCE_UPDATE_MESSAGE, OnResUpdateMessageHandler);
        panelObj.gameObject.SetActive(true);
    }
    protected override void OnHide()
    {
        panelObj.gameObject.SetActive(false);
        Mogo.Util.EventDispatcher.RemoveEventListener<string>(GUIEvent.RESOURCE_UPDATE_MESSAGE, OnResUpdateMessageHandler);
    }
    protected override void OnDestory()
    {

    }
}
