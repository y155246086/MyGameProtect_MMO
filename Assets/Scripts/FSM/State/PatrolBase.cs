using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBase : FSMState {

    //巡逻到达的最后时间
    private float lastPatrolTime = 0;
    private float patrolCD = 3;
    private bool isPatroling = false;
    public PatrolBase(EntityParent owner)
    {
        curRotSpeed = 6;
        curSpeed = 1;
        this.theOwner = owner;
        destPos = owner.transform.position;
        animator = owner.animator;
        aiPath = owner.gameObject.GetComponent<AIPath>();
    }
    public override void Enter(params System.Object[] args)
    {
        Debuger.Log("进入巡逻状态");
        animator.applyRootMotion = false;
        StartPatrol();
    }
    public override void Exit()
    {
        Debuger.Log("退出巡逻状态");
        StopMove();
    }
    public override void OnUpdate()
    {
        if (GameWorld.thePlayer.transform != null && Vector3.Distance(theOwner.transform.position, GameWorld.thePlayer.transform.position) <= theOwner.propertyManager.GetPropertyValue(PropertyType.Chase_Dis))
        {
            Debuger.Log("转换为追逐状态");
            theOwner.ChangeState(FSMStateType.Chasing);
            return;
        }
        if (isPatroling == false && Time.time - lastPatrolTime > patrolCD)
        {
            StartPatrol();
        }
        //到达目标点就停止移动
        if (isPatroling == true && GetDistanceXZ(theOwner.transform.position, destPos) <= theOwner.propertyManager.GetPropertyValue(PropertyType.Arrive_Dis))
        {
            StopMove();
            lastPatrolTime = Time.time;
            isPatroling = false;
            return;
        }
    }
    private Vector3 GetNextPatrolPoint()
    {
        float patrolRadius = theOwner.propertyManager.GetPropertyValue(PropertyType.Patrol_Radius);
        return new Vector3(theOwner.bornPosition.x + UnityEngine.Random.RandomRange(-patrolRadius, patrolRadius), theOwner.bornPosition.y, theOwner.bornPosition.z + UnityEngine.Random.RandomRange(-patrolRadius, patrolRadius));
    }
    private void StartPatrol()
    {
        destPos = GetNextPatrolPoint();
        AIPathTarget.position = destPos;
        isPatroling = true;
        StartMove();
    }
}
