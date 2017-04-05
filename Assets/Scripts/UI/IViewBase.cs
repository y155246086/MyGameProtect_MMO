using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIPanelLayers
{
    BackgroundLayer = 0,
    DefaultLayer = 10,
    NormalLayer = 20,
    MainLayer = 30,
    MaskLayer = 40,
    PopLayer = 50,
    TipsLayer = 60,
    PrompLayer = 70,
    LoadingLayer = 80,
}
public abstract class IViewBase
{

    public UIPanelLayers uiLayer = UIPanelLayers.NormalLayer;
	public void Start()
    {
        OnStart();
    }
    public void Destroy()
    {
        OnDestory();
    }
    public void Show()
    {
        OnShow();
    }
    public void Hide()
    {
        OnHide();
    }

    protected abstract void OnStart();
    protected abstract void OnShow();
    protected abstract void OnHide();
    protected abstract void OnDestory();
}
