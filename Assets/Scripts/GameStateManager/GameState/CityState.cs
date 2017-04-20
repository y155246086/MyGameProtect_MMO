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
        GameWorld.AddNewEntity(SpriteType.Myself);
        GameObject[] arr = GameObject.FindGameObjectsWithTag("MonsterPoint");
        for (int i = 0; i < arr.Length; i++)
        {
            EntityServerInfo info = new EntityServerInfo();
            info.id = (uint)(i+1);
            info.dataId = 1;
            info.position = arr[i].transform.position;
            info.x = (short)info.position.x;
            info.y = (short)info.position.z;
            arr[i].SetActive(false);
            GameWorld.AddNewEntity(SpriteType.Monster, info);
        }

    }
}
