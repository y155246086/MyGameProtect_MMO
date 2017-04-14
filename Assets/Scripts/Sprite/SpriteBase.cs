using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 精灵基类
/// </summary>
public class SpriteBase : MonoBehaviour {

    protected SfxManager sfxManager;// 特效管理器
    public SfxHandler sfxHandler;
    public PropertyManager propertyManager;//属性管理器
    public Vector3 BornPoint;//出生点
    protected SpriteType spriteType = SpriteType.NONE;
    
    /// <summary>
    /// 获取精灵类型
    /// </summary>
    public SpriteType SpriteType
    {
        get
        {
            return spriteType;
        }
    }
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        FSMUpdate();
    }
    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
    protected virtual void Initialize()
    {
        propertyManager = new PropertyManager();
        sfxManager = new SfxManager(this);
        sfxHandler = this.gameObject.AddComponent<SfxHandler>();
    }

    protected virtual void FSMUpdate()
    {

    }
    protected virtual void FSMFixedUpdate()
    {

    }
    /// <summary>
    /// 播放特效
    /// </summary>
    public void PlaySfx(int id)
    {
        if (sfxManager == null)
        {
            return;
        }
        sfxManager.PlaySfx(id);
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
