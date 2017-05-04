using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using System;
using System.Collections.Generic;
using Mogo.Util;

public class SfxHandler : MonoBehaviour {

    //private static Dictionary<int, GameObject> m_resourceDic = new Dictionary<int, GameObject>();
    private Dictionary<string, Transform> m_locationDic = new Dictionary<string, Transform>();
    private Dictionary<int, Dictionary<string, GameObject>> m_fxDic = new Dictionary<int, Dictionary<string, GameObject>>();
    private Dictionary<int, List<int>> m_groupFXList = new Dictionary<int, List<int>>();
    //private MeleeWeaponTrail[] m_weaponTrails;
    private Dictionary<string, MeleeWeaponTrail> m_weaponTrailsDic = new Dictionary<string, MeleeWeaponTrail>();
    private static Dictionary<string, Material> m_weaponTrailMaterial = new Dictionary<string, Material>();
    private static Dictionary<string, AnimationClip> m_animationClip = new Dictionary<string, AnimationClip>();
    private Renderer[] m_randerer;
    private Material[] m_mat;

    private static HashSet<string> m_loadedFX = new HashSet<string>();

    // 记录SlotCueHandler
    SlotCueHandler slotCueHandler;
    void Awake()
    {
        slotCueHandler = gameObject.GetComponent<SlotCueHandler>();
        GetMaterials();
        EventDispatcher.AddEventListener<GameObject>(Events.OtherEvent.OnChangeWeapon, OnChangeWeapon);
        EventDispatcher.AddEventListener(ActorParent.ON_EQUIP_DONE, OnEquitDone);
    }

    void OnDestroy()
    {
        EventDispatcher.RemoveEventListener<GameObject>(Events.OtherEvent.OnChangeWeapon, OnChangeWeapon);
        EventDispatcher.RemoveEventListener(ActorParent.ON_EQUIP_DONE, OnEquitDone);
        Clear();
    }
    private void OnChangeWeapon(GameObject go)
    {
        if (this && go)
        {
            var wt = go.GetComponent<MeleeWeaponTrail>();
            if (wt)
            {
                m_weaponTrailsDic[wt.transform.parent.name] = wt;
            }
            else
            {
                
            }
        }
    }

    private void OnEquitDone()
    {
        if (this)
            GetMaterials();
    }

    private void GetMaterials()
    {
        var randers = new List<Renderer>();
        var smr = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        var mr = GetComponentsInChildren<MeshRenderer>(true);
        randers.AddRange(smr);
        randers.AddRange(mr);
        if (randers.Count != 0)
        {
            m_randerer = randers.ToArray();
            m_mat = new Material[m_randerer.Length];
            for (int i = 0; i < m_randerer.Length; i++)
            {
                m_mat[i] = m_randerer[i].material;
            }
        }
        else
        {
            m_randerer = null;
            m_mat = null;
        }
    }
    public static void AddloadedFX(String fxResourceName)
    {
        m_loadedFX.Add(fxResourceName);
    }

    public static void UnloadAllFXs()
    {
        m_loadedFX.Clear();
    }
    public void Clear()
    {
        RemoveAllFX();
        m_mat = null;
        m_randerer = null;
        m_weaponTrailsDic.Clear();
        m_locationDic.Clear();
    }
    public void RemoveAllFX()
    {
        List<int> list = new List<int>();
        foreach (var fxs in m_fxDic)
        {
            list.Add(fxs.Key);
        }
        foreach (var item in list)
        {
            RemoveFXs(item);
        }
        m_fxDic.Clear();
    }
    public void RemoveFXs(int id)
    {
        if (id == 0)
            return;
        var fx = EffectData.GetByID(id);
        RemoveFXs(id, fx.group);
    }
    public void RemoveFXs(int id, int group)
    {
        if (m_fxDic.ContainsKey(id))
        {
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            foreach (var item in m_fxDic[id])
            {
                list.Add(new KeyValuePair<int, string>(id, item.Key));
            }
            foreach (var item in list)
            {
                RemoveFX(item.Key, item.Value, group);
            }
            m_fxDic.Remove(id);
        }
    }
    private void RemoveFX(int id, string guid, int group)
    {
        RemoveFX(id, guid);
        if (m_groupFXList.ContainsKey(group))
            m_groupFXList[group].Remove(id);
    }
    /// <summary>
    /// 发射弓箭或火球等
    /// </summary>
    /// <param name="shootSfxId">飞行物</param>
    /// <param name="boomSfxId">碰撞后特效,-1代表无</param>
    /// <param name="target">目标</param>
    /// <param name="speed">速度</param>
    /// <param name="distance">如果目标为null时，到前方一定距离后就消失</param>
    public void Shoot(EffectData fx, Transform target, float speed = 10, float distance = 30)
    {
        PlayFX(fx.id, fx, (go, guid) =>
        {
            //加上bullet脚本,设置参数
            var bullet = go.AddComponent<ActorBullet>();
            bullet.OnDestroy = () =>
            {
                RemoveFX(fx.id, guid);
            };
            Vector3 targetPosition = Vector3.zero;

            if (target == null)
                targetPosition = go.transform.position + transform.forward * distance;

            bullet.Setup(target, speed, targetPosition);
        });
    }

    /// <summary>
    /// 插入特效
    /// </summary>
    /// <param name="id">FXData id</param>
    /// <param name="action">加载对象回调</param>
    public void HandleFx(int id, Transform target = null, System.Action<GameObject, string> action = null, string bone_path = "")
    {
        EffectData fxData = EffectData.GetByID(id);
        if (fxData != null)
        {
            if (fxData.effectType == (int)EffectType.Flying)
                Shoot(fxData, target);
            else
                PlayFX(id, fxData, action, bone_path);
        }
        else
        {
            Debuger.LogWarning(string.Format("Can not find fxData {0}", id));
        }
    }
    private void PlayFX(int id, EffectData fx, System.Action<GameObject, string> action = null, string bone_path = "")
    {
        //HandleWeaponTrial(fx);
        HandleAnim(fx);
        HandleFade(fx);


        
        string path = GameCommonUtils.GetResourceData(fx.resourceName).resourcePath;
        GameObject obj = Res.ResourceManager.Instance.Instantiate<GameObject>(path);
        string guid = path;
        m_loadedFX.Add(path);

        var go = obj as GameObject;

        if (!m_fxDic.ContainsKey(id))
        {
            m_fxDic[id] = new Dictionary<string, GameObject>();
        }
        if (!m_fxDic[id].ContainsKey(guid))
        {
            m_fxDic[id].Add(guid, go);
        }
        else
        {
            m_fxDic[id][guid] = go;
        }
        
        //处理音效
        var audio = go.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.volume = 1f;

            FrameTimerHeap.AddTimer((uint)fx.soundDelay, 0, () =>
            {
                if (audio != null)
                    audio.enabled = true;
            });
        }
        switch ((FXLocationType)fx.locationType)
        {
            case FXLocationType.SelfSlot:

                // 判断输入路径是否指定，若未指定则绑在预设的骨上
                if (bone_path == "")
                {
                    if (!m_locationDic.ContainsKey(fx.slot))
                    {
                        m_locationDic.Add(fx.slot, GetBone(transform, fx.slot));
                    }
                    go.transform.parent = m_locationDic[fx.slot];
                }

                // 若已经指定则绑在指定的骨上
                else
                {
                    if (!m_locationDic.ContainsKey(bone_path))
                    {
                        m_locationDic.Add(bone_path, GetBone(transform, bone_path));
                    }
                    go.transform.parent = m_locationDic[bone_path];

                    // 记下index，以便删除
                    slotCueHandler.SetFxList(id, bone_path, guid);
                }

                go.transform.localPosition = go.transform.position;
                go.transform.localRotation = go.transform.rotation;
                break;
            case FXLocationType.World:
                go.transform.position = fx.location;
                break;
            case FXLocationType.SelfLocal:
                go.transform.parent = transform;
                //LoggerHelper.Debug(go.transform.position.x + " " + go.transform.position.y + " " + go.transform.position.z);
                go.transform.localPosition = go.transform.position;
                go.transform.localRotation = go.transform.rotation;
                go.transform.forward = transform.forward;
                break;
            case FXLocationType.SelfWorld:
                go.transform.localPosition = transform.localPosition;
                go.transform.localRotation = go.transform.rotation;
                go.transform.position = transform.position;
                go.transform.forward = transform.forward;
                break;
            case FXLocationType.SlotWorld:

                if (!m_locationDic.ContainsKey(fx.slot))
                {
                    m_locationDic.Add(fx.slot, GetBone(transform, fx.slot));
                }

                var solt = m_locationDic[fx.slot];
                go.transform.localPosition = solt.transform.localPosition;
                go.transform.position = solt.transform.position;
                go.transform.localRotation = solt.transform.rotation;
                break;
            default:
                break;
        }
        if (fx.isStatic == (int)FXStatic.NotStatic)
        {
            Action actRemove = () =>
            {
                if (this)
                    RemoveFX(id, guid);
            };
            if (fx.duration > 0)
                FrameTimerHeap.AddTimer((uint)(fx.duration * 1000), 0, actRemove);
            else
                FrameTimerHeap.AddTimer(3000, 0, actRemove);
        }
        else
        {
            //如果技能为静态技能，分组标记非0，而且特效绑定到自身
                
        }
        if (action != null)
            action(go, guid);
    }
    /// <summary>
    /// 处理动画播放
    /// </summary>
    /// <param name="fx"></param>
    private void HandleAnim(EffectData fx)
    {
        if (!String.IsNullOrEmpty(fx.anim))
        {
            if (!gameObject.GetComponent<Animation>())
                gameObject.AddComponent<Animation>();
            if (m_animationClip.ContainsKey(fx.anim))
            {
                gameObject.GetComponent<Animation>().AddClip(m_animationClip[fx.anim], fx.anim);
                gameObject.GetComponent<Animation>().Play(fx.anim);
            }
            else
            {
                //to do: 资源释放
               
            }
        }
    }

    /// <summary>
    /// 处理淡入淡出
    /// </summary>
    /// <param name="fx"></param>
    private void HandleFade(EffectData fx)
    {
        
    }
    public void RemoveFX(int id, string guid)
    {
        if (m_fxDic.ContainsKey(id) && m_fxDic[id].ContainsKey(guid))
        {
            GameObject.Destroy(m_fxDic[id][guid]);
            //AssetCacheMgr.ReleaseInstance(guid);
            m_fxDic[id].Remove(guid);
        }
    }
    /// <summary>
    /// 获取身体的位置
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="boneName"></param>
    /// <returns></returns>
    private Transform GetBone(Transform transform, string boneName)
    {
        Transform bone = transform.FindChild(boneName);
        if (bone == null)
        {
            foreach (Transform child in transform)
            {
                bone = GetBone(child, boneName);
                if (bone != null) return bone;
            }
        }
        return bone;
    }
    public void RemoveSlotCue(int id, string index)
    {
        RemoveFX(id, index);
    }
    private int currentShaderIndex = 0;
    private int nextShaderIndex = 1;
    private int curFrame = 0;
    private bool updatingShader = false;
    private int currentShaderFx = 0;
    private Shader orgShader;
    
    private void SetMatShader(Shader shader)
    {
        if (m_mat != null && shader)
            foreach (var item in m_mat)
            {
                //LoggerHelper.Error("SetMatShader item: " + item.name);
                item.shader = shader;
            }
    }

    private Color GetMatColor(string prop)
    {
        if (m_mat != null && m_mat.Length != 0)
            return m_mat[0].GetColor(prop);
        else
            return Color.clear;
    }

    private void SetMatColor(string prop, Color color)
    {
        if (m_mat != null)
            foreach (var item in m_mat)
            {
                item.SetColor(prop, color);
            }
    }

    private void SetMatFloat(string prop, float value)
    {
        if (m_mat != null)
            foreach (var item in m_mat)
            {
                item.SetFloat(prop, value);
            }
    }

    private void SetMatTexture(string prop, Texture texture)
    {
        foreach (var item in m_mat)
        {
            item.SetTexture(prop, texture);
        }
    }
}
