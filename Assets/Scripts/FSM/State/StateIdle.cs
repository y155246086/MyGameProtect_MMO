using UnityEngine;
using System.Collections;

public class StateIdle : FSMState
{

    public StateIdle(EntityParent owner)
    {
        this.theOwner = owner;
        animator = owner.animator;
    }
    public override void Enter(params System.Object[] args)
    {
        Debuger.Log("进入休闲状态");
        
        if (theOwner is EntityPlayer && GameWorld.inCity)
        {
            theOwner.SetAction(ActionConstants.CITY_IDLE);
        }
        else
        {
            theOwner.SetAction(ActionConstants.COPY_IDLE);
        }
    }
    public override void Exit()
    {
        Debuger.Log("退出休闲状态");
    }
    public override void OnUpdate()
    {
        
    }
}
