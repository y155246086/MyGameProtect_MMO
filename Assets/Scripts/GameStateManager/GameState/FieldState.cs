using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 野外状态
/// </summary>
public class FieldState : GameState
{

    protected override void OnStart()
    {
        Debuger.Log("FieldState-->OnStart");
    }
    protected override void OnStop()
    {
        Debuger.Log("FieldState-->OnStop");
    }
    protected override void OnLoadComplete(params object[] args)
    {
        Debuger.Log("FieldState-->OnLoadComplete");
        MonsterManager.Instance.CreateMonster(1, Vector3.zero);
        GUIManager.ShowView(PanelNameConst.EasyTouchControlsPanel);
        GUIManager.ShowView(PanelNameConst.FunctionButtonPanel,0);
    }
}

