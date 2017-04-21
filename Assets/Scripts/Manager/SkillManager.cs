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
    private SkillData curSkillData = null;
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
            cdDict[data.id] = Time.time - data.cd;
        }
        skillList.Sort(SortList);
    }

    private int SortList(SkillData x, SkillData y)
    {
        if(x.cd - y.cd>0)
        {
            return -1;
        }else if(x.cd - y.cd <0)
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
                if (Time.time - cdDict[data.id] >= data.cd && isCanSkill == true)//cd时间到
                {
                    UseSkill(data);
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                UseSkill(data);
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
            if (Time.time - cdDict[data.id] >= data.cd)//cd时间到
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
    public void UseSkill(int skillid)
    {
        if (isSkillPlaying == true) return;
        if (IsCding(skillid)) return;
        UseSkill(SkillData.GetByID(skillid));
    }
    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="data"></param>
    private void UseSkill(SkillData data)
    {
        //先判断当前状态可不可以使用技能，如：沉默，晕眩等限制状态

        if (owner is EntityMyself)
        {
            Mogo.Util.EventDispatcher.TriggerEvent(GUIEvent.STOP_JOYSTICK_TURN);
        }
        curSkillData = data;
        //设置cd
        cdDict[data.id] = Time.time;
        //播放动作
        owner.SetAction(data.action);

        //面向敌人
        Transform target = GetHitSprite(curSkillData.id);
        if (target != null)
        {
            owner.transform.LookAt(new Vector3(target.position.x, owner.transform.position.y, target.position.z));
        }
        isCanSkill = false;
        isSkillPlaying = true;
        AttackingFx(data);
        delayAttackTimerID = TimerHeap.AddTimer((uint)(data.triggerTime * 1000f), 0, DelayAttack);
        EndAttackTimerID = TimerHeap.AddTimer((uint)(data.duration * 1000f), 0, EndAttackAction);
        //GameObjectUtils.Instance.CheckAttaceTrigger("Base Layer." + data.stateName, data.triggerTime, owner.GetComponent<Animator>(), AttackTrigger, EndAttackAction);
    }
    
    public void Clear()
    {
        TimerHeap.DelTimer(delayAttackTimerID);
        TimerHeap.DelTimer(EndAttackTimerID);
    }
    protected void EndAttackAction()
    {
        isCanSkill = true;
        isSkillPlaying = false;
        owner.SetAction(0);
        if(owner is EntityMyself)
        {
            Mogo.Util.EventDispatcher.TriggerEvent(GUIEvent.START_JOYSTICK_TURN);
        }
    }
    protected void DelayAttack()
    {
        AttackTrigger();
    }
    protected void AttackTrigger()
    {
        Transform target = GetHitSprite(curSkillData.id);
        if(target != null)
        {
            ICanAttacked att = target.GetComponent<ICanAttacked>();
            if (att != null)
            {
                att.SetHurt(Random.Range(curSkillData.minAttackValue, curSkillData.maxAttackValue) * 100);
            }
        }
    }
    protected Transform GetHitSprite(int skillid)
    {
        SkillData skill = SkillData.GetByID(skillid);
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
    protected void AttackingFx(SkillData skillData)
    {
        owner.PlaySfx(skillData.id);
        if(skillData.cameraTweenId>0)
        {
            ////有震屏,调用震屏接口
            TimerHeap.AddTimer<int, float>((uint)(skillData.cameraTweenSL * 1000), 0, MogoMainCamera.Instance.Shake, skillData.cameraTweenId, skillData.cameraTweenST);
        }
        if (curSkillData.skillSound.Length > 1)
        {
            //播放声音
            //Mogo.SoundManager.GameObjectPlaySound(curSkillData.skillSound, this.gameObject, false, true);
        }
    }
}
