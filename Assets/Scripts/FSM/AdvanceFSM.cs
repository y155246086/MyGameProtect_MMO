using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态类型
/// </summary>
public enum FSMStateType
{
    Patroling,//巡逻
    Chasing,//追逐
    Attacking,//攻击
    Dead,//死亡
    Hit,//受击
    Idle,//休闲
}
public class AdvanceFSM : FSMBase {
    private Dictionary<FSMStateType, FSMState> map;

    private EntityParent owner;
    protected Vector3 destPos;

    private FSMState currentState;
    public FSMState CurrentState
    {
        get { return currentState; }
    }
    private FSMStateType currentType;
    public FSMStateType CurrentType
    {
        get { return currentType; }
    }
    public AdvanceFSM(EntityParent owner)
    {
        this.owner = owner;
        map = new Dictionary<FSMStateType, FSMState>();
    }
    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="type"></param>
    /// <param name="fsmState"></param>
    public void AddFSMState(FSMStateType type,FSMState fsmState)
    {
        if (map.Count == 0)
        {
            map.Add(type,fsmState);
            ChangeState(type);
            return;
        }
        if(map.ContainsKey(type))
        {
            Debuger.LogError("添加的状态已经存在:" + type);
            return;
        }
        map.Add(type, fsmState);
    }
    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="type"></param>
    public void DeleteFSMState(FSMStateType type)
    {
        if(!map.ContainsKey(type))
        {
            Debuger.LogError("当前列表中不存在这个状态:" + type);
        }
        map.Remove(type); 
    }
    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="type"></param>
    public void ChangeState(FSMStateType type, params System.Object[] args)
    {
        if(!map.ContainsKey(type))
        {
            Debuger.LogError("不包含此状态" + type);
        }

        FSMState state = null;
        map.TryGetValue(type, out state);
        if (state == null)
        {
            Debuger.LogError("获取到的状态为空:" + type);
            return;
        }

        if (state == currentState)
        {
            return;
        }
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = state;
        currentType = type;
        currentState.Enter(args);
    }
    public override void FSMUpdate()
    {
        base.FSMUpdate();
        if (CurrentState != null)
            CurrentState.OnUpdate();
    }
    /// <summary>
    /// 状态组里是否有这个状态
    /// </summary>
    /// <param name="fSMStateType"></param>
    /// <returns></returns>
    public bool HasState(FSMStateType fSMStateType)
    {
        return map.ContainsKey(fSMStateType);
    }
}
