using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏状态基础类
/// </summary>
public abstract class GameState {

	public void Start()
    {
        //TODO显示加载UI
        GUIManager.ShowView(PanelNameConst.LoadingPanel);
        OnStart();
    }
    public void Stop()
    {
        OnStop();
    }
    public void LoadComplete(params object[] args)
    {
        //TODO隐藏加载UI
        OnLoadComplete(args);
        GUIManager.HideView(PanelNameConst.LoadingPanel);
    }
    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract void OnLoadComplete(params object[] args);
}
