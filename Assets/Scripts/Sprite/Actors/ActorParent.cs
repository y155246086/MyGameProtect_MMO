using UnityEngine;
using System.Collections;
using System;

public class ActorParent<T> : ActorParent where T : EntityParent
{
    private T m_theEntity;
    public T theEntity
    {
        get { return m_theEntity; }
        set { m_theEntity = value; }
    }
    public override EntityParent GetEntity()
    {
        return m_theEntity;
    }
}
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Pathfinding.RVO.RVOSimulator))]
[RequireComponent(typeof(Pathfinding.FunnelModifier))]
public class ActorParent : MonoBehaviour,ICanAttacked {
    
    public virtual EntityParent GetEntity()
    {
        return null;
    }
    void Awake()
    {
        OnAwake();
    }
    void Start()
    {
        OnStart();
    }
    void Update()
    {
        OnUpdate();
    }
    void FixedUpdate()
    {
        OnFixedUpdate();
    }
    protected virtual void OnAwake()
    {

    }
    protected virtual void OnStart()
    {

    }
    protected virtual void OnUpdate()
    {
        this.GetEntity().OnUpdate();
    }
    protected virtual void OnFixedUpdate()
    {
        this.GetEntity().OnFixedUpdate();
    }
    public void SetHurt(int value)
    {
        if(!(this is ActorMyself))
        {
            GetEntity().propertyManager.ChangeProperty(PropertyType.HP, -value);
            if (GetEntity().propertyManager.GetPropertyValue(PropertyType.HP)>0)
            {
                GetEntity().SetAction(ActionConstants.HIT);
                AddCallbackInFrames<int>(GetEntity().SetAction, 0);
            }
            
            FloatBlood(value);
        }
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
    // 基于帧的回调函数。 用于处理必须在异帧 完成的事情。
    public void AddCallbackInFrames(Action callback, int inFrames = 3)
    {
        StartCoroutine(CallBackInFrames(callback, inFrames));
    }

    public void AddCallbackInFrames<U>(Action<U> callback, U arg1, int inFrames = 3)
    {
        StartCoroutine(CallBackInFrames(callback, arg1, inFrames));
    }

    public void AddCallbackInFrames<U, V>(Action<U, V> callback, U arg1, V arg2, int inFrames = 3)
    {
        StartCoroutine(CallBackInFrames(callback, arg1, arg2, inFrames));
    }

    public void AddCallbackInFrames<U, V, T>(Action<U, V, T> callback, U arg1, V arg2, T arg3, int inFrames = 3)
    {
        StartCoroutine(CallBackInFrames(callback, arg1, arg2, arg3, inFrames));
    }

    public void AddCallbackInFrames<U, V, T, W>(Action<U, V, T, W> callback, U arg1, V arg2, T arg3, W arg4, int inFrames = 3)
    {
        StartCoroutine(CallBackInFrames(callback, arg1, arg2, arg3, arg4, inFrames));
    }

    IEnumerator CallBackInFrames(Action callback, int inFrames)
    {
        int n = 0;
        while (n < inFrames)
        {
            yield return new WaitForFixedUpdate();
            n += 1;
        }
        callback();
    }

    IEnumerator CallBackInFrames<U>(Action<U> callback, U arg1, int inFrames)
    {
        int n = 0;
        while (n < inFrames)
        {
            yield return new WaitForFixedUpdate();
            n += 1;
        }
        callback(arg1);
    }

    IEnumerator CallBackInFrames<U, V>(Action<U, V> callback, U arg1, V arg2, int inFrames)
    {
        int n = 0;
        while (n < inFrames)
        {
            yield return new WaitForFixedUpdate();
            n += 1;
        }
        callback(arg1, arg2);
    }

    IEnumerator CallBackInFrames<U, V, T>(Action<U, V, T> callback, U arg1, V arg2, T arg3, int inFrames)
    {
        int n = 0;
        while (n < inFrames)
        {
            yield return new WaitForFixedUpdate();
            n += 1;
        }
        callback(arg1, arg2, arg3);
    }

    IEnumerator CallBackInFrames<U, V, T, W>(Action<U, V, T, W> callback, U arg1, V arg2, T arg3, W arg4, int inFrames)
    {
        int n = 0;
        while (n < inFrames)
        {
            yield return new WaitForFixedUpdate();
            n += 1;
        }
        callback(arg1, arg2, arg3, arg4);
    }

    internal bool IsDead()
    {
        return false;
    }
    public void FloatBlood(int hp, SplitBattleBillboardType type = SplitBattleBillboardType.CriticalPlayer)
    {
        if (GameCommonUtils.GetChild(transform, "slot_billboard"))
            BillboardLogicManager.Instance.AddSplitBattleBillboard(GameCommonUtils.GetChild(transform, "slot_billboard").position, hp, type);
    }
    virtual protected void FloatBlood(int type, int num)
    {
        switch (type)
        {
            case 1:
                {//闪避
                    FloatBlood(num, SplitBattleBillboardType.Miss);
                    break;
                }
            case 2:
                {//怪物普通受击
                    FloatBlood(num, SplitBattleBillboardType.NormalMonster);
                    break;
                }
            case 3:
                {//破击
                    FloatBlood(num, SplitBattleBillboardType.BrokenAttack);
                    break;
                }
            case 4:
                {//暴击
                    FloatBlood(num, SplitBattleBillboardType.CriticalMonster);
                    break;
                }
            case 5:
                {//破击加暴击
                    FloatBlood(num, SplitBattleBillboardType.BrokenAttack);
                    break;
                }
        }
    }
}
