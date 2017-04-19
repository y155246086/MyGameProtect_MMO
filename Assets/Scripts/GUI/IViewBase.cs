using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIPanelLayers
{
    BackgroundLayer = 0,
    DefaultLayer = 100,
    NormalLayer = 200,
    MainLayer = 300,
    MaskLayer = 400,
    PopLayer = 500,
    TipsLayer = 600,
    PrompLayer = 700,
    LoadingLayer = 800,
}
public abstract class IViewBase
{

    public UIPanelLayers uiLayer = UIPanelLayers.NormalLayer;
    public GameObject panelObj = null;
	public void Start()
    {
        panelObj.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        panelObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        panelObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        panelObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        panelObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        OnStart();
    }
    public void Destroy()
    {
        OnDestory();
    }
    public void Show(params object[] args)
    {
        panelObj.gameObject.SetActive(true);
        OnShow(args);
        AddEventListener();
    }
    public void Hide()
    {
        RemoveEventListener();
        panelObj.gameObject.SetActive(false);
        OnHide();
    }
    /// <summary>
    /// OnShow 后调用添加事件
    /// </summary>
    protected virtual void AddEventListener()
    {

    }
    /// <summary>
    /// OnHide 前移除事件
    /// </summary>
    protected virtual void RemoveEventListener()
    {

    }

    protected abstract void OnStart();

    protected abstract void OnShow(params object[] args);
    
    protected abstract void OnHide();
    protected abstract void OnDestory();
    protected T Find<T>(string namePath)
    {
        return panelObj.transform.Find(namePath).GetComponent<T>();
    }
}
