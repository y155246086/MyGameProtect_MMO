using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using System.Collections.Generic;
using Mogo.Util;


public class SkillManager
{
    private List<SkillData> skillList = new List<SkillData>();
    private EntityParent owner;
    private Dictionary<int, float> cdDict = new Dictionary<int, float>();
    private SkillAction curSkillData = null;
    private bool isCanSkill = true;
    private bool isSkillPlaying = false;
    private uint delayAttackTimerID = 0;
    private uint EndAttackTimerID = 0;
    public SkillManager(EntityParent owner)
    {
        this.owner = owner;
    }
    /// <summary>
    /// 添加技能 将owner的所有技能都添加
    /// </summary>
    /// <param name="skillId"></param>
    public void AddSkill(int skillId)
    {
        if (HasSkill(skillId))
        {
            return;
        }
        SkillData data = SkillData.GetByID(skillId);
        if(data != null)
        {
            skillList.Add(data);
            cdDict[data.id] = Time.time*1000 - data.cd[0];
        }
        skillList.Sort(SortList);
    }

    private int SortList(SkillData x, SkillData y)
    {
        if(x.cd[0] - y.cd[0]>0)
        {
            return -1;
        }else if(x.cd[0] - y.cd[0] <0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    private bool HasSkill(int skillId)
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if(skillList[i].id == skillId)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 攻击
    /// </summary>
    public void Attack()
    {
        if (isCanSkill == false) return;
        if(owner.IsDead())
        {
            return;
        }
        for (int i = 0; i < skillList.Count; i++)
        {
            SkillData data = skillList[i];
            if(cdDict.ContainsKey(data.id))
            {
                if (Time.time * 1000 - cdDict[data.id] >= data.cd[0] && isCanSkill == true)//cd时间到
                {
                    UseSkill(data.id);
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                UseSkill(data.id);
                break;
            }
        }
    }
    /// <summary>
    /// 技能是不是在cd 中
    /// </summary>
    /// <returns></returns>
    protected bool IsCding(int skillID)
    {
        SkillData data = SkillData.GetByID(skillID);
        if (data!= null && cdDict.ContainsKey(data.id))
        {
            if (Time.time * 1000 - cdDict[data.id] >= data.cd[0])//cd时间到
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 技能是否在释放中
    /// </summary>
    public bool IsSkillPlaying
    {
        get {
            return isSkillPlaying;
        }
    }
    public void Process(EntityParent theOwner, int skillDataId)
    {
        int spellID = skillDataId;
        SkillData s = SkillData.dataMap[spellID];
        //theOwner.motor.speed = 0;
        //theOwner.motor.targetSpeed = 0;
        int baseTime = 0;
        for (int i = 0; i < s.skillAction.Count; i++)
        {
            SkillAction action = SkillAction.dataMap[s.skillAction[i]];
            List<object> args1 = new List<object>();
            args1.Add(s.skillAction[0]);
            args1.Add(theOwner.transform.localToWorldMatrix);
            args1.Add(theOwner.transform.rotation);
            args1.Add(theOwner.transform.forward);
            args1.Add(theOwner.transform.position);
            if (i == 0)
            {
                ProcessHit(theOwner, spellID, args1);
                //if (theOwner is EntityMyself)
                //{
                //    theOwner.Motor.enableStick = action.enableStick > 0;
                //}
            }
            if (i + 1 == s.skillAction.Count)
            {
                break;
            }
            uint tid = 0;
            List<object> args2 = new List<object>();
            args2.Add(s.skillAction[i + 1]);
            args2.Add(theOwner.transform.localToWorldMatrix);
            args2.Add(theOwner.transform.rotation);
            args2.Add(theOwner.transform.forward);
            args2.Add(theOwner.transform.position);
            if (action.actionTime > 0)
            {
                tid = TimerHeap.AddTimer((uint)((baseTime + action.actionTime) / theOwner.aiRate), 0, ProcessHit, theOwner, spellID, args2);
                baseTime += action.actionTime;
            }
            if (action.nextHitTime > 0)
            {
                tid = TimerHeap.AddTimer((uint)((baseTime + action.nextHitTime) / theOwner.aiRate), 0, ProcessHit, theOwner, spellID, args2);
                baseTime += action.nextHitTime;
            }
            theOwner.hitTimer.Add(tid);
        }
    }

    private void ProcessHit(EntityParent theOwner, int spellID, List<object> args)
    {
        int actionID = (int)args[0];
        UnityEngine.Matrix4x4 ltwm = (UnityEngine.Matrix4x4)args[1];
        UnityEngine.Quaternion rotation = (UnityEngine.Quaternion)args[2];
        UnityEngine.Vector3 forward = (UnityEngine.Vector3)args[3];
        UnityEngine.Vector3 position = (UnityEngine.Vector3)args[4];

        SkillAction action = SkillAction.dataMap[actionID];
        SkillData s = SkillData.dataMap[spellID];

        // 回调，基于计时器。 在duration 后切换回 idle 状态
        int duration = action.duration;
        if (duration <= 0 && s.skillAction.Count > 1)// && theOwner.hitActionIdx >= (s.skillAction.Count - 1))
        {
            if (SkillAction.dataMap[s.skillAction[0]].duration <= 0)
            {
                theOwner.Actor.AddCallbackInFrames<int, EntityParent>(
                    (_actionID, _theOwner) =>
                    {
                        //_theOwner.TriggerUniqEvent<int>(Events.FSMMotionEvent.OnAttackingEnd, _actionID);
                    },
                    actionID,
                    theOwner);
            }
        }
        else if (duration > 0 && action.action > 0)
        {
            TimerHeap.AddTimer<int, EntityParent>(
                (uint)duration,
                0,
                (_actionID, _theOwner) =>
                {
                    //_theOwner.TriggerUniqEvent<int>(Events.FSMMotionEvent.OnAttackingEnd, _actionID);
                    //_theOwner.ChangeMotionState(MotionState.IDLE);
                },
                actionID,
                theOwner);
        }
        // 回调，基于计时器。 在removeSfxTime 后关闭持久的sfx
        if (action.duration > 0)
        {
            TimerHeap.AddTimer<int, EntityParent>(
                (uint)action.duration,
                0,
                (_actionID, _theOwner) =>
                {
                    _theOwner.RemoveSfx(_actionID);
                },
                actionID,
                theOwner);
        }
        UseSkill(SkillAction.GetByID(actionID));
    }
    public void UseSkill(int skillDataID)
    {
        Process(owner, skillDataID);
        //UseSkill(SkillData.GetByID(skillid));
    }
    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="data"></param>
    private void UseSkill(SkillAction data)
    {
        if (owner is EntityMyself)
        {
            Mogo.Util.EventDispatcher.TriggerEvent(GUIEvent.STOP_JOYSTICK_TURN);
        }
        curSkillData = data;
        //设置cd
        cdDict[data.id] = Time.time * 1000;
        //播放动作
        SkillAction skillAction = data;
        owner.SetAction(skillAction.action);
        owner.Actor.AddCallbackInFrames<int>(owner.SetAction,0);
        //面向敌人
        Transform target = GetHitSprite(curSkillData.id);
        if (target != null)
        {
            owner.transform.LookAt(new Vector3(target.position.x, owner.transform.position.y, target.position.z));
        }
        isCanSkill = false;
        isSkillPlaying = true;
        AttackingFx(skillAction);
        delayAttackTimerID = TimerHeap.AddTimer<SkillAction>((uint)(skillAction.triggerTime), 0, DelayAttack,skillAction);
        TimerHeap.DelTimer(EndAttackTimerID);
        EndAttackTimerID = TimerHeap.AddTimer<SkillAction>((uint)(skillAction.duration), 0, EndAttackAction, skillAction);
        //GameObjectUtils.Instance.CheckAttaceTrigger("Base Layer." + data.stateName, data.triggerTime, owner.GetComponent<Animator>(), AttackTrigger, EndAttackAction);
    }
    
    public void Clear()
    {
        TimerHeap.DelTimer(delayAttackTimerID);
        TimerHeap.DelTimer(EndAttackTimerID);
    }
    protected void EndAttackAction(SkillAction skillAction)
    {
        isCanSkill = true;
        isSkillPlaying = false;
        owner.SetAction(0);
        owner.ClearSkill();
        if(owner is EntityMyself)
        {
            Mogo.Util.EventDispatcher.TriggerEvent(GUIEvent.START_JOYSTICK_TURN);
        }
    }
    protected void DelayAttack(SkillAction skillAction)
    {
        List<EntityParent> list = GetHitEntities(curSkillData.id);
        for (int i = 0; i < list.Count; i++)
        {
            ICanAttacked att = list[i].Actor.GetComponent<ICanAttacked>();
            if (att != null)
            {
                att.SetHurt(Random.Range(skillAction.minAttackValue, skillAction.maxAttackValue) * 100);
            }
        }
    }
    protected List<EntityParent> GetHitEntities(int skillid)
    {
        List<EntityParent> list = new List<EntityParent>();
        if (owner is EntityMyself)
        {
            foreach (var item in GameWorld.SpriteList)
            {
                if(item.Value != owner)
                {
                    list.Add(item.Value);
                }
            }

        }
        return list;
    }
    protected Transform GetHitSprite(int skillid)
    {
        SkillAction skill = SkillAction.GetByID(skillid);
        if(owner is EntityMyself)
        {
            if (GameWorld.GetEntity(SpriteType.Monster) != null)
            {
                return GameWorld.GetEntity(SpriteType.Monster).transform;
            }
            else
            {
                return null;
            }
            
        }
        else
        {
            return GameWorld.player.transform;
        }
        switch ((TargetRangeType)skill.targetRangeType)
        {
            case TargetRangeType.CircleRange:
                if (owner is EntityMyself)
                {
                    return GameWorld.GetEntity(3).transform;
                }
                else
                {
                    return GameWorld.player.transform;
                }
                
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 播放特效
    /// </summary>
    protected void AttackingFx(SkillAction skillData)
    {
        owner.PlaySfx(skillData.id);
        if(skillData.cameraTweenId>0)
        {
            ////有震屏,调用震屏接口
            TimerHeap.AddTimer<int, float>((uint)(skillData.cameraTweenSL), 0, MogoMainCamera.Instance.Shake, skillData.cameraTweenId, (float)skillData.cameraTweenST/1000f);
        }
    }
}
