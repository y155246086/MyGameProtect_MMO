using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : FSMState
{

    public AttackBase(EntityParent owner)
    {
        curRotSpeed = 6;
        curSpeed = 80;
        this.owner = owner;
        animator = owner.animator;
    }
    public override void Enter(params Object[] args)
    {
        Debuger.Log("进入攻击状态");
    }
    public override void Exit()
    {
        Debuger.Log("退出攻击状态");
    }
    public override void OnUpdate(Transform target)
    {
        if (owner.skillManager!= null && owner.skillManager.IsSkillPlaying == true)
        {
            return;
        }
        float dist = GetDistanceXZ(owner.transform.position, target.position);

        if (dist >= owner.propertyManager.GetPropertyValue(PropertyType.Attack_Dis) && dist < owner.propertyManager.GetPropertyValue(PropertyType.Chase_Dis))
        {
            owner.ChangeState(FSMStateType.Chasing);
            return;
        }
        else if (dist >= owner.propertyManager.GetPropertyValue(PropertyType.Chase_Dis))
        {
            owner.ChangeState(FSMStateType.Patroling);
            return;
        }
        if (owner.skillManager != null)
        {
            owner.skillManager.Attack();
        }
        destPos = target.position;

        Vector3 dir = new Vector3(destPos.x, 0, destPos.z) - new Vector3(owner.transform.position.x, 0, owner.transform.position.z);
        //确定我当前角色的方向
        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRotation, Time.deltaTime + curRotSpeed);
        }
        return;
    }
}
