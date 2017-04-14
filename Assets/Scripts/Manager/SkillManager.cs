using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using System.Collections.Generic;
using Mogo.Util;


public class SkillManager : MonoBehaviour
{
    private List<SkillData> skillList = new List<SkillData>();
    private SpriteBase owner;
    private Dictionary<int, float> cdDict = new Dictionary<int, float>();
    private SkillData curSkillData = null;
    private bool isCanSkill = true;
    private bool isSkillPlaying = false;
    public SkillManager()
    {

    }

    public void SetOwenr(SpriteBase owner)
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
    private bool IsCding(int skillID)
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

        curSkillData = data;
        //设置cd
        cdDict[data.id] = Time.time;
        //播放动作
        owner.GetComponent<Animator>().SetInteger("Action", data.action);
        isCanSkill = false;
        isSkillPlaying = true;
        AttackingFx(data);
        TimerHeap.AddTimer((uint)(data.triggerTime * 1000f), 0, DelayAttack);
        TimerHeap.AddTimer((uint)(data.duration * 1000f), 0, EndAttackAction);
        //GameObjectUtils.Instance.CheckAttaceTrigger("Base Layer." + data.stateName, data.triggerTime, owner.GetComponent<Animator>(), AttackTrigger, EndAttackAction);
    }
    private void EndAttackAction()
    {
        isCanSkill = true;
        isSkillPlaying = false;
        owner.GetComponent<Animator>().SetInteger("Action", 0);
    }
    private void DelayAttack()
    {
        AttackTrigger();
    }
    protected void AttackTrigger()
    {
        Transform target = GetHitSprite(curSkillData.id);
        ICanAttacked att = target.GetComponent<ICanAttacked>();
        if(att != null)
        {
            att.SetHurt(curSkillData.maxAttackValue);
        }
    }
    private Transform GetHitSprite(int skillid)
    {
        SkillData skill = SkillData.GetByID(skillid);
        if(owner is Player)
        {
            return MonsterManager.Instance.monsterList[0].transform;
        }
        else
        {
            return GameWorld.player.transform;
        }
        switch ((TargetRangeType)skill.targetRangeType)
        {
            case TargetRangeType.CircleRange:
                if(owner is Player)
                {
                    return MonsterManager.Instance.monsterList[0].transform;
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
    private void AttackingFx(SkillData skillData)
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
