using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追逐
/// </summary>
public class ChaseBase : FSMState {

    public ChaseBase(Transform owner)
    {
        curRotSpeed = 6;
        curSpeed = 4;
        this.owner = owner;
        animator = owner.GetComponent<Animator>();
        aiPath = owner.GetComponent<AIPath>();
        aiController = owner.GetComponent<FSM.AIController>();
        //FindNextPoint();
    }
    public override void Enter()
    {
        Debuger.Log("进入追逐状态");
        animator.applyRootMotion = false;
        //StartMove();
    }
    public override void Exit()
    {
        Debuger.Log("退出追逐状态");
        StopMove();
    }
    public override void OnUpdate(Transform target)
    {
        
    }
    protected override bool OnUpdateState(Transform target)
    {
        destPos = target.position;
        if (GetDistanceXZ(owner.position, destPos) <= aiController.AttackDistance)
        {
            Debuger.Log("转换为攻击状态");
            owner.GetComponent<FSM.AIController>().ChangeState(FSMStateType.Attacking);
            return true;
        }
        else if (GetDistanceXZ(owner.position, destPos) >= aiController.ChaseDistance)
        {
            Debuger.Log("转变到巡逻状态");
            owner.GetComponent<FSM.AIController>().ChangeState(FSMStateType.Patroling);
            return true;
        }
        return false;
    }
    protected override void OnUpdateAction(Transform target)
    {
        //到达目标点就停止移动
        if (GetDistanceXZ(owner.position, destPos) <= aiController.ArriveDistance)
        {
            StopMove();
            return;
        }
        if(animator.GetInteger("Action") == 0)
        {
            destPos = target.position;
            AIPathTarget.position = destPos;
            StartMove();
        }
        
        //StartMove();
        return;
        AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animatorInfo.IsName("Base Layer.run"))//注意这里指的不是动画的名字而是动画状态的名字
        {
            //角色移动
            StartMove();
        }
        else
        {
            destPos = target.position;

            Vector3 dir = new Vector3(destPos.x, 0, destPos.z) - new Vector3(owner.position.x, 0, owner.position.z);
            //确定我当前角色的方向
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            owner.rotation = Quaternion.Slerp(owner.rotation, targetRotation, Time.deltaTime + curRotSpeed);
        }

        
    }
}
