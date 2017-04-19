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
    protected override void AddEventListener()
    {
        Mogo.Util.EventDispatcher.AddEventListener<string>(GUIEvent.RESOURCE_UPDATE_MESSAGE, OnResUpdateMessageHandler);
    }
    protected override void RemoveEventListener()
    {
        Mogo.Util.EventDispatcher.RemoveEventListener<string>(GUIEvent.RESOURCE_UPDATE_MESSAGE, OnResUpdateMessageHandler);
    }
    private void OnResUpdateMessageHandler(string message)
    {
        descText.text = message;
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
