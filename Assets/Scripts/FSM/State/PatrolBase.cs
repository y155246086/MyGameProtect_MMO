using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBase : FSMState {

    public PatrolBase(Transform owner)
    {
        curRotSpeed = 6;
        curSpeed = 1;
        this.owner = owner;
        destPos = owner.position;
        animator = owner.GetComponent<Animator>();
        aiPath = owner.GetComponent<AIPath>();
        aiController = owner.GetComponent<FSM.AIController>();
    }
    public override void Enter(params Object[] args)
    {
        Debuger.Log("进入巡逻状态");
        animator.applyRootMotion = false;
        StartMove();
    }
    public override void Exit()
    {
        Debuger.Log("退出巡逻状态");
        Animator ani = owner.GetComponent<Animator>();
        StopMove();
    }
    public override void OnUpdate(Transform target)
    {
        
    }
    protected override bool OnUpdateState(Transform target)
    {
        if (Vector3.Distance(owner.position, target.position) <= aiController.ChaseDistance)
        {
            Debuger.Log("转换为追逐状态");
            owner.GetComponent<FSM.AIController>().ChangeState(FSMStateType.Chasing);
            return true;
        }
        if (GetDistanceXZ(owner.position, destPos) <= aiController.ArriveDistance)
        {
            if (aiController.data.patrolRadius <=0)
            {
                destPos = owner.position;
            }
            else
            {
                destPos = new Vector3(aiController.BornPoint.x + UnityEngine.Random.RandomRange(-aiController.data.patrolRadius, aiController.data.patrolRadius), aiController.BornPoint.y, aiController.BornPoint.z + UnityEngine.Random.RandomRange(-aiController.data.patrolRadius, aiController.data.patrolRadius));
            }
            AIPathTarget.position = destPos;
        }
        return false;
    }
    
    protected override void OnUpdateAction(Transform target)
    {
        //到达目标点就停止移动
        if (GetDistanceXZ(owner.position, destPos) <= aiController.ArriveDistance)
        {
            StopMove();
            //animator.SetBool("IsWalk", false);
            return;
        }
        //角色移动
        //destPos = target.position;
        //AIPathTarget.position = destPos;
        //StartMove();
        //animator.Play(Animator.StringToHash("Base Layer.walk"));
    }
    
}
