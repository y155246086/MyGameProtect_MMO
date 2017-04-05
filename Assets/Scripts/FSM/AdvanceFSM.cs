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
    Dead//死亡
}
public class AdvanceFSM : FSMBase {
    private Dictionary<FSMStateType, FSMState> map;

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
    public AdvanceFSM()
    {
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
    public void ChangeState(FSMStateType type)
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
        currentState.Enter();
    }
    public void Play(string parameterName, bool value)
    {
        var ar = this.GetComponent<UnityEngine.Animator>();
        ar.SetBool(parameterName, value);

    }
    public void Play(string parameterName, float value)
    {
        var ar = this.GetComponent<UnityEngine.Animator>();
        ar.SetFloat(parameterName, value);
    }
    public void Play(string parameterName, int value)
    {
        var ar = this.GetComponent<UnityEngine.Animator>();
        ar.SetInteger(parameterName, value);
    }
    public void Play(string parameterName)
    {
        var ar = this.GetComponent<UnityEngine.Animator>();
        ar.SetTrigger(parameterName);
    }
    public bool GetBool(string parameterName)
    {
        var ar = this.GetComponent<UnityEngine.Animator>();
        return ar.GetBool(parameterName);
    }
    public int GetInt(string parameterName)
    {
        var ar = this.GetComponent<UnityEngine.Animator>();
        return ar.GetInteger(parameterName);
    }
    public float GetFloat(string parameterName)
    {
        var ar = this.GetComponent<UnityEngine.Animator>();
        return ar.GetFloat(parameterName);
    }
}
