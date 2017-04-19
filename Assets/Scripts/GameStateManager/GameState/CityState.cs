using BattleFramework.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 城市状态
/// </summary>
public class CityState : GameState
{

    protected override void OnStart()
    {
        Debuger.Log("CityState-->OnStart");
        
    }
    protected override void OnStop()
    {
        Debuger.Log("CityState-->OnStop");
    }
    protected override void OnLoadComplete(params object[] args)
    {
        Debuger.Log("CityState-->OnLoadComplete");
        GUIManager.ShowView(PanelNameConst.EasyTouchControlsPanel);
        GUIManager.ShowView(PanelNameConst.FunctionButtonPanel);
        if(GameWorld.player == null)
        {
            CreateRole();
        }
        GameObject bornPoint = GameObject.Find("BornPoint");
        if (bornPoint != null)
        {
            bornPoint.SetActive(false);
            GameWorld.player.transform.position = bornPoint.transform.position;
        }
        MonsterManager.Instance.CreateMonster(1, Vector3.zero);
    }
    private void CreateRole()
    {
        ResourceData data = ResourceData.GetByID(1);
        GameObject go = Res.ResourceManager.Instance.Instantiate<GameObject>(data.resourcePath);
        go.tag = "Player";
        GameWorld.player = go.AddComponent<Player>();
        
        
    }
}
