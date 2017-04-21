using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using Mogo.Util;

public abstract class EntityParent {

    public uint ID;
    public Animator animator;
    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public Vector3 bornPosition = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public Vector3 scale = new Vector3(1, 1, 1);
    protected EntityServerInfo serverInfo;
    protected AdvanceFSM fsm;
    public PropertyManager propertyManager;
    private SfxManager sfxManager;
    public SfxHandler sfxHandler;
    public SkillManager skillManager;
    public SpriteType spriteType;
    public ActorParent Actor { get; set; }
    /// <summary>
    /// 状态机初始化
    /// </summary>
    private void ConstructFSM()
    {
        fsm.AddFSMState(FSMStateType.Patroling, new PatrolBase(this));
        fsm.AddFSMState(FSMStateType.Chasing, new ChaseBase(this));
        fsm.AddFSMState(FSMStateType.Attacking, new AttackBase(this));
        fsm.AddFSMState(FSMStateType.Dead, new DeadBase(this));
    }
    public void OnUpdate()
    {
        fsm.FSMUpdate();
    }
    public void OnFixedUpdate()
    {
        if (propertyManager.GetPropertyValue(PropertyType.HP) <= 0)
        {
            fsm.ChangeState(FSMStateType.Dead);
            return;
        }
        fsm.FSMFixedUpdate();
    }
    public void ChangeState(FSMStateType fSMStateType)
    {
        fsm.ChangeState(fSMStateType);
    }
    public void CreateModel()
    {
        OnCreateModel();
    }
    public void EnterWorld()
    {
        propertyManager = new PropertyManager();
        sfxManager = new SfxManager(this);
        sfxHandler = this.gameObject.AddComponent<SfxHandler>();
        if(this is EntityMyself)
        {
            skillManager = new PlayerSkillManager(this);
        }
        else
        {
            skillManager = new SkillManager(this);
        }
        
        fsm = new AdvanceFSM(this);
        if (this is EntityMonster)
            ConstructFSM();
        OnEnterWorld();
    }
    public void LeaveWorld()
    {
        OnLeaveWorld();
    }
    /// <summary>
    /// 创建模型
    /// </summary>
    protected abstract void OnCreateModel();
    /// <summary>
    /// 进入场景
    /// </summary>
    protected abstract void OnEnterWorld();
    /// <summary>
    /// 离开场景
    /// </summary>
    protected abstract void OnLeaveWorld();
   
    /// <summary>
    /// 动画是否在融合中
    /// </summary>
    /// <returns></returns>
    public bool IsInTransition()
    {
        return animator.IsInTransition(0);
    }

    public void SetEntityServerInfo(EntityServerInfo info)
    {
        serverInfo = info;
        ID = info.id;
        bornPosition = info.position;
    }
    public virtual void UpdatePosition()
    {
        Vector3 point;
        if (transform)
        {
            if (GameCommonUtils.GetPointInTerrain(bornPosition.x, bornPosition.z, out point))
            {
                transform.position = new Vector3(point.x, point.y + 0.3f, point.z);
                if (rotation != Vector3.zero)
                    transform.eulerAngles = new Vector3(0, rotation.y, 0);
            }
            else
            {
                var map = GameStateManager.CurrentSceneData;
                var myself = this as EntityMyself;
                if (myself != null)
                {
                    Vector3 bornPoint;
                    if (GameCommonUtils.GetPointInTerrain((float)(map.enterPoint.x), (float)(map.enterPoint.z), out bornPoint))
                    {
                        transform.position = new Vector3(bornPoint.x, bornPoint.y + 0.5f, bornPoint.z);
                    }
                    else
                    {
                        transform.position = new Vector3(bornPoint.x, bornPoint.y, bornPoint.z);
                    }
                }
                else
                {
                    transform.position = new Vector3(point.x, point.y + 1, point.z);
                }
                
            }
        }

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
    internal bool IsDead()
    {
        return propertyManager.GetPropertyValue(PropertyType.HP)<=0;
    }
    virtual public void SetAction(int act)
    {
        if (animator == null)
        {
            return;
        }
        animator.SetInteger("Action", act);
        if (act == ActionConstants.HIT_AIR)
        {

        }
        else if (act == ActionConstants.KNOCK_DOWN)
        {

        }
        else if (act == ActionConstants.HIT)
        {

        }
        else if (act == ActionConstants.PUSH)
        {

        }
        else if (act == ActionConstants.HIT_GROUND)
        {

        }
    }
    /// <summary>
    /// 分身
    /// </summary>
    public void CreateDuplication()
    {
        GameObject duplication = (GameObject)UnityEngine.Object.Instantiate(Actor.gameObject, Vector3.zero, Quaternion.identity);

        MonoBehaviour[] scripts = duplication.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script.GetType() != typeof(Transform))
            {
                UnityEngine.Object.Destroy(script);
            }
        }

        Animator anima = duplication.GetComponent<Animator>();
        if (anima != null)
            UnityEngine.Object.Destroy(anima);

        CharacterController chaController = duplication.GetComponent<CharacterController>();
        if (chaController != null)
            UnityEngine.Object.Destroy(chaController);

        AIPath AIPath = duplication.GetComponent<AIPath>();
        if (AIPath != null)
            UnityEngine.Object.Destroy(AIPath);

        duplication.transform.position = transform.position;
        duplication.transform.rotation = transform.rotation;
        TimerHeap.AddTimer<GameObject>(1000, 0, (copy) => { UnityEngine.Object.Destroy(copy); }, duplication);
    }
}
