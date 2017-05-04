using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using BattleFramework.Data;
using UnityEngine.Rendering;
using Mogo.Util;

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
    protected Animator m_animator;
    protected string preActName = "";
    public Action<string, string> ActChangeHandle;

    private Action<String, Boolean> m_animatorStateChanged;
    public Action<String, Boolean> AnimatorStateChanged
    {
        get
        {
            return m_animatorStateChanged;
        }
        set
        {
            m_animatorStateChanged = value;
        }
    }

    private Action<String, Boolean> m_hitStateChanged;
    public Action<String, Boolean> HitStateChanged
    {
        get
        {
            return m_hitStateChanged;
        }
        set
        {
            m_hitStateChanged = value;
        }
    }
    void Awake()
    {
        m_animator = GetComponent<Animator>();
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
    public void SetHurt(int value)
    {
        if(!(this is ActorMyself))
        {
            //GetEntity().propertyManager.ChangeProperty(PropertyType.HP, -value);
            if (GetEntity().propertyManager.GetPropertyValue(PropertyType.HP)>0)
            {
                GetEntity().SetAction(ActionConstants.HIT_AIR);
                AddCallbackInFrames<int>(GetEntity().SetAction, 0);
            }
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
    private int idleCnt = 0;
    virtual public void ActChange()
    {
        if (m_animator == null)
        {
            return;
        }
        if (m_animator.IsInTransition(0))
        {//融合期间
            return;
        }
        AnimatorClipInfo[] state = m_animator.GetCurrentAnimatorClipInfo(0);
        if (state.Length == 0)
        {
            return;
        }
        string currName = state[0].clip.name;
        if (currName != preActName)
        {//动作变换
            if (ActChangeHandle != null)
            {
                ActChangeHandle(preActName, currName);
            }
            if (!currName.EndsWith("ready") && !currName.EndsWith("run") &&
                !currName.EndsWith("idle") && !currName.EndsWith("powercharge") &&
                !currName.EndsWith("powercharge_loop") && !currName.EndsWith("powercharge_left") &&
                !currName.EndsWith("roll"))
            {
                SkillAction a = null;
                if (GetEntity().currSpellID != -1 &&
                    SkillAction.dataMap.TryGetValue(SkillData.dataMap[GetEntity().currSpellID].skillAction[0], out a))
                {//只为当前版本所用，新版本中动作不一样了要去掉
                    if (a.duration <= 0)
                    {
                        m_animator.SetInteger("Action", 0);
                    }
                    else
                    {//旋风斩之类技能使用
                        m_animator.SetInteger("Action", -3);
                    }
                }
                else
                {
                    m_animator.SetInteger("Action", 0);
                }
            }
            preActName = currName;
        }
        if ((currName.EndsWith("hit") && preActName.EndsWith("hit")) ||
            (currName.EndsWith("push") && preActName.EndsWith("push")) ||
            (currName.EndsWith("hitair") && preActName.EndsWith("hitair")) ||
            (currName.EndsWith("knockdown") && preActName.EndsWith("knockdown")))
        {
            int act = m_animator.GetInteger("Action");
            if (act != 0 && act != -1)
            {
                m_animator.SetInteger("Action", 0);
            }
        }
        if (GetEntity() != null &&
            currName != null &&
            GetEntity().stiff &&
            (currName.EndsWith("ready") ||
            currName.EndsWith("run") ||
            currName.EndsWith("run_left") ||
            currName.EndsWith("run_right") ||
            currName.EndsWith("run_back")))
        {
            idleCnt++;
            if (idleCnt > 5)
            {
                GetEntity().ClearHitAct();
                idleCnt = 0;
            }
        }
        else
        {
            idleCnt = 0;
        }
    }
    #region 换装
    private static object m_equipWeaponLock = new object();

    public bool m_isChangingWeapon = false;
    //身上已穿装备<部位类型，装备id>
    private Dictionary<int, int> m_equipOnDic = new Dictionary<int, int>();
    ////裸体时的“装备”（外形显示）
    private List<int> m_nakedEquipList = new List<int>();
    //已装备中的用挂载方法装备的装备
    private List<GameObject> m_equipList = new List<GameObject>();
    //已装备中的Mesh或Material
    private List<UnityEngine.Object> m_equipMeshOrMaterialList = new List<UnityEngine.Object>();
    private List<GameObject> weaponObj = new List<GameObject>();
    public List<GameObject> WeaponObj { get { return weaponObj; } }
    private EquipData m_weaponData = null;
    private List<SkinnedMeshRenderer> m_smrList = new List<SkinnedMeshRenderer>();
    public List<SkinnedMeshRenderer> SmrList { get { return m_smrList; } }
    private List<SkinnedMeshRenderer> m_smrAllList = new List<SkinnedMeshRenderer>();

    private Dictionary<int, EquipObjectData> m_equipGoDic = new Dictionary<int, EquipObjectData>();

    public EquipData WeaponData
    {
        get
        {
            return m_weaponData;
        }
    }

    public Dictionary<int, int> EquipOnDic
    {
        get
        {
            return m_equipOnDic;
        }
    }
    private AvatarModelData GetAvatarModelData(int vocation)
    {
        for (int i = 0; i < DataCenter.Instance().list_AvatarModelData.Count; i++)
        {
            return DataCenter.Instance().list_AvatarModelData[i];
        }
        
        return null;
    }
    /// <summary>
    /// 初始化装备外形
    /// </summary>
    public void InitEquipment()
    {
        Debuger.Log("name:" + transform.name + ",vocation:" + (int)GetEntity().vocation);
        AvatarModelData data = GetAvatarModelData((int)GetEntity().vocation);
        if (data == null)
            return;
        if (data.nakedEquipList == null || data.nakedEquipList.Count <= 0) return;
        SetNakedEquid(data.nakedEquipList);
        InitNakedEquid();
    }

    public void InitEquipment(int vocation)
    {
        AvatarModelData data = GetAvatarModelData(vocation);
        if (data == null)
            return;
        if (data.nakedEquipList == null || data.nakedEquipList.Count <= 0) return;
        SetNakedEquid(data.nakedEquipList);
        InitNakedEquid();
    }

    /// <summary>
    /// 初始显示裸装
    /// </summary>
    public void InitNakedEquid()
    {
        //Debuger.Log("InitNakedEquid");
        ClearOriginalModel();
        PutOnNakedEquip();
        ReEquidAll(m_equipOnDic);
    }
    private void ClearOriginalModel()
    {
        if (m_smrAllList.Count <= 0)
        {
            foreach (Transform t in transform)
            {
                SkinnedMeshRenderer smr = t.GetComponent<SkinnedMeshRenderer>();
                if (smr == null) continue;
                smr.gameObject.SetActive(true);
                smr.sharedMesh = null;
                m_smrAllList.Add(smr);
            }
        }
        else
        {
            foreach (SkinnedMeshRenderer smr in m_smrAllList)
            {
                smr.gameObject.SetActive(true);
                smr.sharedMesh = null;
            }
        }

    }

    /// <summary>
    /// 设置裸装，用于卸装时恢复裸装
    /// </summary>
    /// <param name="ids"></param>
    public void SetNakedEquid(List<int> _nakedEquipList)
    {
        m_nakedEquipList = _nakedEquipList;
    }
    public void Equip(List<int> idList, Action onDone = null)
    {
        List<EquipData> equipDataList = new List<EquipData>();
        for (int i = 0; i < idList.Count; i++)
        {
            int equidID = idList[i];
            EquipData equipData = EquipData.GetByID(equidID);
            if (equipData == null)
                return;

            if (m_equipOnDic.ContainsValue(equipData.id)) continue;


            equipDataList.Add(equipData);
        }

        if (equipDataList.Count == 0)
        {
            if (onDone != null) onDone();
            return;
        }
        GetEquipObjectList(equipDataList, (equipGoDic, equipOnDic) =>
        {
            m_equipGoDic = equipGoDic;

            RemoveOld();

            //根据优先级等重装所有装备
            ReEquidAll(equipOnDic);

            if (onDone != null)
            {
                onDone();
            }
        });
    }

    /// <summary>
    /// 刷新身上装备，有时候因为模型没创建好却调用装备接口引起装备失败
    /// </summary>
    public void RefreshEquip()
    {
        RemoveOld();
        //穿上裸装
        PutOnNakedEquip();


        //根据优先级等重装所有装备
        ReEquidAll(m_equipOnDic);
    }

    /// <summary>
    /// 穿上装备
    /// </summary>
    /// <param name="_equidID"></param>
    public void Equip(int _equidID, Action onDone = null)
    {
        //Debuger.Log("Equid:" + _equidID);

        EquipData equipData = EquipData.GetByID(_equidID);
        if (equipData == null)
            return;

        foreach (int id in m_equipOnDic.Values)
        {
            if (id == equipData.id)
            {
                if (onDone != null) onDone();
                return;
            }
        }

        List<EquipData> equipDataList = new List<EquipData>();
        equipDataList.Add(equipData);
        if (equipDataList.Count == 0)
        {
            if (onDone != null) onDone();
            return;
        }
        GetEquipObjectList(equipDataList, (equipGoDic, equipOnDic) =>
        {
            if (!this) return;
            if (transform == null) return;
            m_equipGoDic = equipGoDic;

            RemoveOld();

            //根据优先级等重装所有装备
            ReEquidAll(equipOnDic);

            if (onDone != null)
            {
                onDone();

            }
           
        });
    }
    protected void PutOnNakedEquip()
    {
        if (m_nakedEquipList == null) return;
        foreach (int id in m_nakedEquipList)
        {
            if (EquipData.GetByID(id) == null)
            {
                Debuger.Log("cannot find equipData:" + id);
            }
            EquipData equip = EquipData.GetByID(id);
            if (m_equipOnDic.ContainsKey(equip.type[0])) continue;
            m_equipOnDic[equip.type[0]] = id;
        }
    }

    private void UncombineEquip(EquipData equipData, int type)
    {
        EquipData old = EquipData.GetByID(m_equipOnDic[type]);
        if (old.subEquip == null) return;
        if (old.subEquip.Count > 0)
        {
            foreach (int id in old.subEquip)
            {
                EquipData equip = EquipData.GetByID(id);

                //子装备只允许为一个type
                m_equipOnDic[equip.type[0]] = equip.id;
            }
        }
    }

    private void CombineEquip(EquipData newEquip)
    {
        if (newEquip.suit <= 0) return;
        int count = 0;
        foreach (int id in m_equipOnDic.Values)
        {
            EquipData equip = EquipData.GetByID(id);
            if (equip.type[0] == newEquip.type[0])
            {
                count++;
                continue;
            }
            if (equip.suit == newEquip.suit) count++;
        }
        if (count != newEquip.suitCount) return;

        EquipData suit = EquipData.GetByID(newEquip.suit);
        foreach (int id in suit.subEquip)
        {
            EquipData equip = EquipData.GetByID(id);
            m_equipOnDic[equip.type[0]] = suit.id;
        }
    }

    /// <summary>
    /// 按优先级顺序重新穿上所有装备
    /// </summary>
    protected void ReEquidAll(Dictionary<int, int> equipOnDic)
    {
        List<int> equipIds = new List<int>();
        foreach (int id in equipOnDic.Values)
        {
            equipIds.Add(id);
        }

        equipIds.Sort(delegate(int a, int b)
        {
            if (EquipData.GetByID(a).priority >= EquipData.GetByID(b).priority) return 1;
            else return -1;
        });

        HashSet<int> suitHasPuton = new HashSet<int>();


        for (int i = 0; i < equipIds.Count; i++)
        {
            if (suitHasPuton.Contains(equipIds[i])) continue;
            EquipData equip = EquipData.GetByID(equipIds[i]);
            AddEquid(equip);
            if ((equip.subEquip != null && equip.subEquip.Count > 0) || equip.type.Count > 1)
                suitHasPuton.Add(equip.id);
        }
    }

    /// <summary>
    /// 卸下所有装备只剩裸装
    /// </summary>
    public void RemoveAll()
    {
        m_equipOnDic.Clear();
        RemoveOld();
        PutOnNakedEquip();
        ReEquidAll(m_equipOnDic);
    }

    public void RemoveOld()
    {

        for (int i = 0; i < m_equipList.Count; i++)
        {
            Debuger.Log("m_equipList装备资源释放");
            m_equipList[i] = null;
        }
        for (int i = 0; i < m_equipMeshOrMaterialList.Count; i++)
        {
            Debuger.Log("m_equipMeshOrMaterialList装备资源释放");
            m_equipMeshOrMaterialList[i] = null;
        }
        for (int i = 0; i < m_smrList.Count; i++)
        {
            m_smrList[i].sharedMaterial = null;
            m_smrList[i].sharedMesh = null;
            m_smrList[i] = null;
        }

        //m_smr.sharedMaterial = null;
        //m_smr.sharedMesh = null;

        m_smrList.Clear();
        m_equipList.Clear();
        m_equipMeshOrMaterialList.Clear();
    }

    private void AddEquid(EquipData equip)
    {
        if (equip.putOnMethod == 1)
        {
            AddEquidMethod1(equip);
        }
        else
        {
            AddEquidMethod2(equip);
        }
    }

    /// <summary>
    /// 替换mesh和material
    /// </summary>
    /// <param name="equipData"></param>
    private void AddEquidMethod2(EquipData equipData)
    {
        if (!m_equipGoDic.ContainsKey(equipData.id))
        {
            Debuger.LogWarning("!m_equipGoDic.ContainsKey(equipData.id):" + equipData.id);
            return;
        }
        Material material = m_equipGoDic[equipData.id].mat;
        GameObject instance = m_equipGoDic[equipData.id].goList[0];
        if (transform == null) return;
        Transform equipPart = GameCommonUtils.GetChild(transform, equipData.slot[0]);

        if (equipPart == null)
        {
            Debuger.Log("can not find slot:" + equipData.slot[0] + ",equipId:" + equipData.id);
            Debuger.Log("gameObject:" + name);
            Debuger.LogError("can not find slot:" + equipData.slot[0] + ",equipId:" + equipData.id + ",vocation:" + GetEntity().vocation);
            return;
        }

        SkinnedMeshRenderer smr = equipPart.GetComponent<SkinnedMeshRenderer>();
        if (!smr)//安全检查
            return;
        m_smrList.Add(smr);
        m_equipMeshOrMaterialList.Add(material);
        smr.sharedMaterial = material;
        smr.shadowCastingMode = ShadowCastingMode.Off;
        smr.receiveShadows = false;
        smr.useLightProbes = true;
        SkinnedMeshRenderer smrTemp = m_equipGoDic[equipData.id].smr;
        UVAnim tempUV = smrTemp.gameObject.GetComponent<UVAnim>();
        UVAnim uv = smr.GetComponent<UVAnim>();
        if(tempUV != null && uv == null)
        {
            uv = smr.gameObject.AddComponent<UVAnim>();
            uv.Direction = tempUV.Direction;
            uv.speed = tempUV.speed;
        }
        if(uv == null)
        {
            uv = smr.gameObject.AddComponent<UVAnim>();
            uv.Direction = new Vector2(0.5f,0.13f);
            uv.speed = 1.5f;
        }
        if (equipData.type.Count > 1) ClearOriginalModel();

        smr.sharedMesh = smrTemp.sharedMesh;
        //CombineInstance ci = new CombineInstance();
        //ci.mesh = smrTemp.sharedMesh;
        //m_combineInstances.Add(ci);

        List<Transform> bones = new List<Transform>();
        for (int i = 0; i < smrTemp.bones.Length; i++)
        {
            bones.Add(GameCommonUtils.GetChild(this.transform, smrTemp.bones[i].name));
        }
        //m_bones.AddRange(bones);
        smr.bones = bones.ToArray();
        m_equipMeshOrMaterialList.Add(instance);

    }

    EquipData currentEquip;
    const string EQUIP_TAP = "equip";
    public static string ON_EQUIP_DONE = "ON_EQUIP_DONE";

    /// <summary>
    /// 装备挂载在某个gameObject下
    /// </summary>
    /// <param name="equipData"></param>
    private void AddEquidMethod1(EquipData equipData)
    {
        if (transform == null) return;
        if (!m_equipGoDic.ContainsKey(equipData.id))
        {
            Debuger.LogWarning("!m_equipGoDic.ContainsKey(equipData.id):" + equipData.id);
            return;
        }
        int ccount = 0;

        currentEquip = equipData;

        weaponObj.Clear();
        m_weaponData = equipData;


        EquipObjectData eod = m_equipGoDic[equipData.id];
        for (int i = 0; i < equipData.prefabPath.Count; i++)
        {
            int index = i;
            GameObject equipGo = null;


            equipGo = eod.goList[index];
            if (equipGo == null)
            {
                Debuger.LogError("equip load fail!");
                ccount++;
                return;
            }

            Transform equipPart = null;
            if (equipGo == null) return;
            if (transform == null) return;

            if (GameWorld.inCity)
            {
                equipPart = GameCommonUtils.GetChild(transform, equipData.slotInCity[ccount]);
            }
            else
            {
                equipPart = GameCommonUtils.GetChild(transform, equipData.slot[ccount]);
            }


            if (equipPart == null)
            {
                Debuger.LogError("cannot find equip slot!");
                ccount++;
                return;
            }

            Vector3 scale = equipGo.transform.localScale;
            equipGo.transform.parent = equipPart;
            equipGo.transform.localPosition = Vector3.zero;
            equipGo.transform.localEulerAngles = Vector3.zero;
            equipGo.transform.localScale = scale;

            if (equipData.isWeapon > 0)
            {
                weaponObj.Add(equipGo);
            }
            //保存已穿装备，方便替换时卸载
            m_equipList.Add(equipGo);

            ccount++;

            if (m_isChangingWeapon)
            {
                m_isChangingWeapon = false;
                if (GetEntity() != null)
                    GetEntity().weaponAnimator = equipGo.GetComponent<Animator>();
            }

        }

    }

    /// <summary>
    /// 装备换位，一些动作变换需要用到
    /// </summary>
    /// <param name="equipName"></param>
    /// <param name="partName"></param>
    public void ChangeEquipPosition(string equipName, string partName)
    {
        Transform t = GameCommonUtils.GetChild(transform, equipName);
        t.parent = GameCommonUtils.GetChild(transform, partName);
        t.localPosition = Vector3.zero;
        t.localEulerAngles = Vector3.zero;
    }

    public void ChangeEquipPosition(GameObject weapon, string partName)
    {
        if (weapon == null) return;
        Transform t = weapon.transform;
        t.parent = GameCommonUtils.GetChild(transform, partName);
        t.localPosition = Vector3.zero;
        t.localEulerAngles = Vector3.zero;
    }
    //暂时清空这两个函数-fred
    private void InstanceLoaded(int copyId, bool isInCopy)
    {
       
    }

    public IEnumerator SwitchWeaponPos(bool isInCopy)
    {
        yield return 0;
    }

    private void GetEquipObjectList(List<EquipData> equipDataList, Action<Dictionary<int, EquipObjectData>, Dictionary<int, int>> onLoad)
    {
        HashSet<int> equipIdSet = new HashSet<int>();
        for (int i = 0; i < equipDataList.Count; i++)
        {
            equipIdSet = GetEquipIdSet(equipDataList[i]);
        }

        Dictionary<int, int> tempEquipOnDic = new Dictionary<int, int>();
        foreach (KeyValuePair<int, int> pair in m_equipOnDic)
        {
            tempEquipOnDic[pair.Key] = pair.Value;
        }

        Dictionary<int, EquipObjectData> eo = new Dictionary<int, EquipObjectData>();
        int count = 0;
        foreach (int id in equipIdSet)
        {
            eo[id] = new EquipObjectData();
            var index = id;
            EquipData equip = EquipData.GetByID(id);
            if (equip.putOnMethod == 1)
            {
                GetInstanceList(equip.prefabPath, (goList) =>
                {
                    eo[index].type = 1;
                    eo[index].goList = goList;
                    count++;
                    if (count == equipIdSet.Count) onLoad(eo, tempEquipOnDic);
                });
            }
            else if (equip.putOnMethod == 2)
            {
                eo[index].type = 2;
                eo[index].mat = Res.ResourceManager.Instance.Instantiate<Material>(equip.material);
                GameObject go = Res.ResourceManager.Instance.Instantiate<GameObject>(equip.mesh);
                eo[index].goList = new List<GameObject>();
                eo[index].goList.Add(go);
                eo[index].smr = go.GetComponentInChildren<SkinnedMeshRenderer>();
                go.SetActive(false);
                //eo[index].smr = go.transform.FindChild(equip.mesh).GetComponent<SkinnedMeshRenderer>();

                count++;
                if (count == equipIdSet.Count) onLoad(eo, tempEquipOnDic);
            }
        }
    }

    private HashSet<int> GetEquipIdSet(EquipData equipData)
    {

        HashSet<int> equipIdSet = new HashSet<int>();

        //判断是否需要解除套装
        foreach (int t in equipData.type)
        {
            if (m_equipOnDic.ContainsKey(t))
            {
                UncombineEquip(equipData, t);
            }
        }

        //去陈推新
        foreach (int t in equipData.type)
        {

            if (m_equipOnDic.ContainsKey(t))
            {
                EquipData old = EquipData.GetByID(m_equipOnDic[t]);
                if (old != null && old.type.Count > 0)
                {
                    foreach (int temp in old.type)
                    {
                        m_equipOnDic.Remove(temp);
                    }
                }
            }
            m_equipOnDic[t] = equipData.id;
        }

        //判断是否需要装备“合体”
        if (equipData.type.Count == 1)
        {
            CombineEquip(equipData);
        }

        //穿上裸装
        PutOnNakedEquip();


        foreach (int id in m_equipOnDic.Values)
        {
            if (!equipIdSet.Contains(id)) equipIdSet.Add(id);
        }

        return equipIdSet;
    }

    private void GetInstanceList(List<string> prefabNameList, Action<List<GameObject>> onLoad)
    {
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < prefabNameList.Count; i++)
        {
            goList.Add(Res.ResourceManager.Instance.Instantiate<GameObject>(prefabNameList[i]));
            
        }
        onLoad(goList);
    }
    #endregion
}
class EquipObjectData
{
    public int type;//1挂载，2mesh
    public List<GameObject> goList;
    public Material mat;
    public SkinnedMeshRenderer smr;
}
