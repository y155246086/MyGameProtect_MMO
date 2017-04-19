using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingPanel : IViewBase {

    private Text contentText = null;
    private Image face = null;
    protected override void OnStart()
    {
        uiLayer = UIPanelLayers.LoadingLayer;
        contentText = Find<Text>("ProgressBar/ContentText");
        face = Find<Image>("ProgressBar/Face");
        face.rectTransform.sizeDelta = new Vector2(0, face.rectTransform.sizeDelta.y);
    }
    
    private void OnLoadProgressHandler(int progress)
    {
        progress = Mathf.Min(progress, 100);
        contentText.text = progress  + "%";
        face.rectTransform.sizeDelta = new Vector2(950f * ((float)progress / 100f), face.rectTransform.sizeDelta.y);
    }
    protected override void AddEventListener()
    {
        Mogo.Util.EventDispatcher.AddEventListener<int>(GUIEvent.LOAD_SCENE_PROGRESS, OnLoadProgressHandler);
    }
    protected override void RemoveEventListener()
    {
        Mogo.Util.EventDispatcher.RemoveEventListener<int>(GUIEvent.LOAD_SCENE_PROGRESS, OnLoadProgressHandler);
    }
    protected override void OnShow(params object[] args)
    {
        
    }
    protected override void OnHide()
    {
        face.rectTransform.sizeDelta = new Vector2(0, face.rectTransform.sizeDelta.y);
    }
    protected override void OnDestory()
    {

    }
}
