using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState {
    protected Vector3 destPos;//目标点
    protected float curRotSpeed;//旋转速度
    protected float curSpeed;//移动速度


    protected EntityParent owner;
    protected Animator animator;
    protected AIPath aiPath;
    private Transform pathTarget;
    protected Transform AIPathTarget
    {
        get
        {
            if(pathTarget == null)
            {
                GameObject o = new GameObject(owner + "_AIPathTarget");
                pathTarget = o.transform;
            }
            return pathTarget;
        }
    }
    /// <summary>
    /// 进入状态
    /// </summary>
    public abstract void Enter(params Object[] args);
    /// <summary>
    /// 离开状态
    /// </summary>
    public abstract void Exit();
    /// <summary>
    /// 更新状态
    /// </summary>
    public abstract void OnUpdate(Transform target);
    public float GetDistanceXZ(Vector3 source, Vector3 target)
    {
        return Vector2.Distance(new Vector2(source.x, source.z), new Vector2(target.x, target.z));
    }
   
    /// <summary>
    /// 是否处于某个状态下
    /// </summary>
    /// <param name="animatorStateName"></param>
    /// <returns></returns>
    protected bool IsAnimatorName(string animatorStateName)
    {
        AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animatorInfo.IsName(animatorStateName) && !animator.IsInTransition(0);
    }
    /// <summary>
    /// 开始移动
    /// </summary>
    protected void StartMove()
    {
        if (aiPath != null)
        {
            //AIPathTarget.position = destPos;
            aiPath.target = AIPathTarget;
            aiPath.speed = curSpeed;
            aiPath.canSearch = true;
            aiPath.canMove = true;
            animator.SetFloat("Speed", 1f);
        }
    }
    /// <summary>
    /// 停止移动
    /// </summary>
    protected void StopMove()
    {
        if (aiPath != null)
        {
            aiPath.target = null;
            aiPath.canSearch = false;
            aiPath.canMove = false;
            animator.SetFloat("Speed", 0f);
        }
    }
}
