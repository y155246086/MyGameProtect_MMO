using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBase : FSMState {

    public PatrolBase(EntityParent owner)
    {
        curRotSpeed = 6;
        curSpeed = 1;
        this.owner = owner;
        destPos = owner.transform.position;
        animator = owner.animator;
        aiPath = owner.gameObject.GetComponent<AIPath>();
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
        StopMove();
    }
    public override void OnUpdate(Transform target)
    {
        
    }
    protected override bool OnUpdateState(Transform target)
    {
        if (target != null && Vector3.Distance(owner.transform.position, target.position) <= owner.propertyManager.GetPropertyValue(PropertyType.Chase_Dis))
        {
            Debuger.Log("转换为追逐状态");
            owner.ChangeState(FSMStateType.Chasing);
            return true;
        }
        if (GetDistanceXZ(owner.transform.position, destPos) <= owner.propertyManager.GetPropertyValue(PropertyType.Arrive_Dis))
        {
            float patrolRadius = owner.propertyManager.GetPropertyValue(PropertyType.Patrol_Radius);
            if (patrolRadius <= 0)
            {
                destPos = owner.bornPosition;
            }
            else
            {
                destPos = new Vector3(owner.bornPosition.x + UnityEngine.Random.RandomRange(-patrolRadius, patrolRadius), owner.bornPosition.y, owner.bornPosition.z + UnityEngine.Random.RandomRange(-patrolRadius, patrolRadius));
            }
            AIPathTarget.position = destPos;
        }
        return false;
    }
    
    protected override void OnUpdateAction(Transform target)
    {
        //到达目标点就停止移动
        if (GetDistanceXZ(owner.transform.position, destPos) <= owner.propertyManager.GetPropertyValue(PropertyType.Arrive_Dis))
        {
            StopMove();
            return;
        }
    }
    
}
