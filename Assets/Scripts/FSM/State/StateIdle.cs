using UnityEngine;
using System.Collections;
using Mogo.Util;

public class StateIdle : FSMState
{
    private uint tempTime = 0;
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
        if(theOwner is EntityMonster)
        {
            if (tempTime>0)
            {
                TimerHeap.DelTimer(tempTime);
            }
            tempTime = TimerHeap.AddTimer<EntityParent>((uint)1500, 0, (e) =>
            {
                if (e == null || e.Motor == null)
                {
                    return;
                }
                e.ChangeState(FSMStateType.Attacking);
            }, theOwner);
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
