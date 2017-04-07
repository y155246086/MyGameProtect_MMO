using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using System.Collections.Generic;
using Mogo.Util;


public class SkillManager : MonoBehaviour
{
    private List<SkillData> skillList = new List<SkillData>();
    private MonsterAI owner;
    private Dictionary<int, float> cdDict = new Dictionary<int, float>();
    private SkillData curSkillData = null;
    private bool isCanSkill = true;
    private bool isSkillPlaying = false;
    public SkillManager()
    {

    }

    public void SetOwenr(MonsterAI owner)
    {
        this.owner = owner;
        if (owner.data.skillID1>0)
            AddSkill(owner.data.skillID1);
        if (owner.data.skillID2 > 0)
            AddSkill(owner.data.skillID2);
        if (owner.data.skillID3 > 0)
            AddSkill(owner.data.skillID3);
    }
    /// <summary>
    /// 添加技能
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
        if(owner.AttackTarget == null || owner.IsDead())
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
    /// 技能是否在释放中
    /// </summary>
    public bool IsSkillPlaying
    {
        get {
            return isSkillPlaying;
        }
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
        //owner.Play(data.triggerName);
        owner.GetComponent<Animator>().SetInteger("Action", data.action);
        isCanSkill = false;
        isSkillPlaying = true;
        AttackingFx(data);
        //AnimatorClipInfo[] infoList = owner.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        //for (int i = 0; i < infoList.Length; i++)
        //{
        //    Debug.LogError("******|"+infoList[i].clip.name + "_" + infoList[i].clip.length);
        //}
        //Debuger.LogError(owner.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base Layer." + data.stateName));
        //float du = owner.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * 1000;
        //Mogo.Util.FrameTimerHeap.AddTimer((uint)du, 0, () =>
        //{
        //    isCanSkill = true;
        //});
        //Debuger.Log("等待攻击触发：AttackTrigger");
        GameObjectUtils.Instance.CheckAttaceTrigger("Base Layer." + data.stateName, data.triggerTime, owner.GetComponent<Animator>(), AttackTrigger, EndAttackAction);
    }
    private void EndAttackAction()
    {
        isCanSkill = true;
        isSkillPlaying = false;
    }
    private void DelayAttack()
    {
        AttackTrigger();
    }
    protected void AttackTrigger()
    {
        //Debuger.LogError(owner.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + "_______");
        owner.GetComponent<Animator>().SetInteger("Action", 0);
        
        if (owner.AttackTarget != null && owner.TargetIsDead() == false)
        {

            if (curSkillData.type == (int)SkillType.DAN_DAO)
            {
                //GameObject o = new GameObject();
                //o.transform.position = transform.position;
                //ShootObject shoot = o.gameObject.AddComponent<ShootObject>();
                //shoot.SetTarget(owner.AttackTarget);
                //shoot.Owner = owner as ActorMonster;
                //shoot.SkillData = curSkillData;
                //shoot.StartShoot();
                Debuger.Log("弹道技能");
            }
            else if (curSkillData.type == (int)SkillType.ZHAO_HUAN)
            {
                Debuger.Log("召唤技能");
            }
            else if (curSkillData.type == (int)SkillType.SHUN_YI)
            {
                Debuger.Log("瞬移技能");
            }
            else
            {
                Debuger.Log("产生伤害");
                ICanAttacked att = owner.AttackTarget.GetComponent<ICanAttacked>();
                if(att != null)
                {
                    att.SetHurt(curSkillData.maxAttackValue);
                }
            }
        }
        //isCanSkill = true;
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
