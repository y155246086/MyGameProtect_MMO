using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginState : GameState {

    protected override void OnStart()
    {
        Debuger.Log("LoginState-->OnStart");
    }
    protected override void OnStop()
    {
        Debuger.Log("LoginState-->OnStop");
    }
    protected override void OnLoadComplete(params object[] args)
    {
        Debuger.Log("LoginState-->OnLoadComplete");
    }
}
