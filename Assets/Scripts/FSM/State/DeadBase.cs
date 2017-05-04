using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBase : FSMState
{

    public DeadBase(EntityParent owner)
    {
        this.theOwner = owner;
        animator = owner.animator;
    }
    public override void Enter(params System.Object[] args)
    {
        Debuger.Log("进入死亡状态");
        theOwner.SetAction(ActionConstants.DIE);
        theOwner.Actor.AddCallbackInFrames<int>(theOwner.SetAction, 0);
        ParticleSystem[] s = theOwner.gameObject.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < s.Length; i++)
        {
            s[i].gameObject.SetActive(false);
        }

        Mogo.Util.EventDispatcher.TriggerEvent<uint>(ActorEvent.ACTOR_DEAD, theOwner.ID);
    }
    public override void Exit()
    {
        Debuger.Log("退出死亡状态");
    }
    public override void OnUpdate()
    {
        
    }
}
