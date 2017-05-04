using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : FSMState
{

    public AttackBase(EntityParent owner)
    {
        curRotSpeed = 6;
        curSpeed = 80;
        this.theOwner = owner;
        animator = owner.animator;
    }
    public override void Enter(params System.Object[] args)
    {
        Debuger.Log("进入攻击状态");
    }
    public override void Exit()
    {
        Debuger.Log("退出攻击状态");
    }
    public override void OnUpdate()
    {
        if (theOwner.skillManager!= null && theOwner.skillManager.IsSkillPlaying == true)
        {
            return;
        }
        float dist = GetDistanceXZ(theOwner.transform.position, GameWorld.thePlayer.transform.position);

        if (dist >= theOwner.propertyManager.GetPropertyValue(PropertyType.Attack_Dis) && dist < theOwner.propertyManager.GetPropertyValue(PropertyType.Chase_Dis))
        {
            theOwner.ChangeState(FSMStateType.Chasing);
            return;
        }
        else if (dist >= theOwner.propertyManager.GetPropertyValue(PropertyType.Chase_Dis))
        {
            theOwner.ChangeState(FSMStateType.Patroling);
            return;
        }
        if (theOwner.skillManager != null && theOwner.stiff == false)
        {
            theOwner.skillManager.Attack();
        }
        destPos = GameWorld.thePlayer.transform.position;

        Vector3 dir = new Vector3(destPos.x, 0, destPos.z) - new Vector3(theOwner.transform.position.x, 0, theOwner.transform.position.z);
        //确定我当前角色的方向
        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            theOwner.transform.rotation = Quaternion.Slerp(theOwner.transform.rotation, targetRotation, Time.deltaTime + curRotSpeed);
        }
        return;
    }
}
