using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追逐
/// </summary>
public class ChaseBase : FSMState {

    public ChaseBase(EntityParent owner)
    {
        curRotSpeed = 6;
        curSpeed = 4;
        this.theOwner = owner;
        animator = owner.animator;
        aiPath = owner.gameObject.GetComponent<AIPath>();
    }
    public override void Enter(params System.Object[] args)
    {
        Debuger.Log("进入追逐状态");
        animator.applyRootMotion = false;
    }
    public override void Exit()
    {
        Debuger.Log("退出追逐状态");
        StopMove();
    }
    public override void OnUpdate()
    {
        destPos = GameWorld.thePlayer.transform.position;
        if (GetDistanceXZ(theOwner.transform.position, destPos) <= theOwner.propertyManager.GetPropertyValue(PropertyType.Attack_Dis))
        {
            Debuger.Log("转换为攻击状态");
            theOwner.ChangeState(FSMStateType.Attacking);
            return;
        }
        else if (GetDistanceXZ(theOwner.transform.position, destPos) >= theOwner.propertyManager.GetPropertyValue(PropertyType.Chase_Dis))
        {
            Debuger.Log("转变到巡逻状态");
            theOwner.ChangeState(FSMStateType.Patroling);
            return;
        }
        OnUpdateAction();
    }
    protected void OnUpdateAction()
    {
        //到达目标点就停止移动

        if (GetDistanceXZ(theOwner.transform.position, destPos) <= theOwner.propertyManager.GetPropertyValue(PropertyType.Arrive_Dis))
        {
            StopMove();
            return;
        }
        if(animator.GetInteger("Action") == 0)
        {
            destPos = GameWorld.thePlayer.transform.position;
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
            destPos = GameWorld.thePlayer.transform.position;

            Vector3 dir = new Vector3(destPos.x, 0, destPos.z) - new Vector3(theOwner.transform.position.x, 0, theOwner.transform.position.z);
            //确定我当前角色的方向
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            theOwner.transform.rotation = Quaternion.Slerp(theOwner.transform.rotation, targetRotation, Time.deltaTime + curRotSpeed);
        }

        
    }
}
