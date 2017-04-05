using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : FSMState
{

    public AttackBase(Transform owner)
    {
        curRotSpeed = 6;
        curSpeed = 80;
        this.owner = owner;
        animator = owner.GetComponent<Animator>();
        aiController = owner.GetComponent<FSM.AIController>();
    }
    public override void Enter()
    {
        Debuger.Log("进入攻击状态");
        //animator.SetTrigger("TriggerAttack01");
    }
    public override void Exit()
    {
        Debuger.Log("退出攻击状态");
        //nimator.SetBool("TriggerAttack01",false);
    }
    public override void OnUpdate(Transform target)
    {
        FSM.AIController ai = owner.GetComponent<FSM.AIController>();
        if (ai != null && ai.skillManager)
        {
            ai.skillManager.Attack();
        }
    }
    protected override bool OnUpdateState(Transform target)
    {
        float dist = GetDistanceXZ(owner.position, target.position);
        if (dist >= aiController.AttackDistance && dist < aiController.ChaseDistance)
        {
            Debug.LogError("发现玩家");
            owner.GetComponent<FSM.AIController>().ChangeState(FSMStateType.Chasing);
            return true;
        }
        else if (dist >= aiController.ChaseDistance)
        {
            Debug.LogError("丢是玩家");
            owner.GetComponent<FSM.AIController>().ChangeState(FSMStateType.Patroling);
            return true;
        }
        return false;
    }
    protected override void OnUpdateAction(Transform target)
    {
        destPos = target.position;

        Vector3 dir = new Vector3(destPos.x, 0, destPos.z) - new Vector3(owner.position.x, 0, owner.position.z);
        //确定我当前角色的方向
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        owner.rotation = Quaternion.Slerp(owner.rotation, targetRotation, Time.deltaTime + curRotSpeed);
    }
}
