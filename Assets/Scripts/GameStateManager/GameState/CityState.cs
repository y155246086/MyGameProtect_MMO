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
    }
}
