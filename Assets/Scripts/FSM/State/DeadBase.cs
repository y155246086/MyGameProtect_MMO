using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBase : FSMState
{

    public DeadBase(EntityParent owner)
    {
        this.owner = owner;
        animator = owner.animator;
    }
    public override void Enter(params Object[] args)
    {
        Debuger.Log("进入死亡状态");
        owner.SetAction(ActionConstants.DIE);
        owner.Actor.AddCallbackInFrames<int>(owner.SetAction, 0);
        ParticleSystem[] s = owner.gameObject.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < s.Length; i++)
        {
            s[i].gameObject.SetActive(false);
        }

        Mogo.Util.EventDispatcher.TriggerEvent<uint>(ActorEvent.ACTOR_DEAD, owner.ID);
    }
    public override void Exit()
    {
        Debuger.Log("退出死亡状态");
    }
    public override void OnUpdate(Transform target)
    {
        
    }
    protected override bool OnUpdateState(Transform target)
    {
        return false;
    }
    protected override void OnUpdateAction(Transform target)
    {
        
    }
}
