using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBase : FSMState
{

    public DeadBase(Transform owner)
    {
        curRotSpeed = 6;
        curSpeed = 80;
        this.owner = owner;
        animator = owner.GetComponent<Animator>();
        aiController = owner.GetComponent<FSM.AIController>();
    }
    public override void Enter()
    {
        Debuger.Log("进入死亡状态");
        animator.SetTrigger("TriggerDead");
        if (aiController.data.deadSound != null && aiController.data.deadSound.Length > 1)
        {
            Mogo.SoundManager.GameObjectPlaySound(aiController.data.deadSound, owner.gameObject, false, true);
        }

        ParticleSystem[] s = owner.gameObject.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < s.Length; i++)
        {
            s[i].gameObject.SetActive(false);
        }
        //if (aiController.data.deadEffect.Length > 1)
        //{
        //    BaofengCommon.PlayEffect(aiController.data.deadEffect, owner.position - new Vector3(0, 0.5f, 0), 4f);//死亡特效
        //}
        if (aiController.data.isRemove == 1)
        {
            GameObject.Destroy(owner.gameObject, 10f);
        }
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
