using UnityEngine;
using System.Collections;
using Mogo.Util;
using System.Collections.Generic;
using BattleFramework.Data;

public class PlayerBattleManager : BattleManager {

    private List<int> preCmds = new List<int>();
    private uint timeOutId = 0;
    public PlayerBattleManager(EntityParent _owner, SkillManager _skillManager)
        : base(_owner, _skillManager)
    {
        skillManager = _skillManager;
    }
    // 析构函数， 移除监听的各种事件
    public override void Clean()
    {
        base.Clean();
    }
    // 普攻，先进入Entity的GoingToCastSpell
    public void NormalAttack()
    {
        if (CanUseSkill() == false)
        {
            return;
        }
        

        if ((skillManager as PlayerSkillManager).IsCommonCooldown())
        {
            preCmds.Add(0);
            return;
        }
        int nextskill = (skillManager as PlayerSkillManager).GetNormalAttackID();
        if (nextskill == theOwner.currSpellID && theOwner.currSpellID != -1)
        {
            preCmds.Add(0);
           return;
        }
        if ((skillManager as PlayerSkillManager).IsSkillCooldown(nextskill))
        {
            (skillManager as PlayerSkillManager).ClearComboSkill();
            preCmds.Add(0);
            return;
        }
        if (!(skillManager as PlayerSkillManager).HasDependence(nextskill))
        {
            CleanPreSkill();
        }
       
        theOwner.Motor.TurnToControlStickDir();
        //GameCommonUtils.LookAtTargetInRange(theOwner.Transform, 6, 360);
        (skillManager as PlayerSkillManager).ResetCoolTime(nextskill);
        EntityMyself.preSkillTime = Time.realtimeSinceStartup;
        theOwner.CastSkill(nextskill);
        TimerHeap.AddTimer((uint)((skillManager as PlayerSkillManager).GetCommonCD(nextskill)), 0, NextCmd);
    }
    /// <summary>
    /// 是否能使用技能，比如死亡等条件判断
    /// </summary>
    /// <returns></returns>
    private bool CanUseSkill()
    {
        if (theOwner.stiff)
        {
            return false;
        }
        return true;
    }
    public void SpellOneAttack()
    {
        CleanPreSkill();
  
        if(CanUseSkill() == false)
        {
            return;
        }

        if ((skillManager as PlayerSkillManager).IsCommonCooldown())
        {
            return;
        }
        int skillid = (skillManager as PlayerSkillManager).GetSpellOneID();
        if ((skillManager as PlayerSkillManager).IsSkillCooldown(skillid))
        {
            return;
        }
        
        (theOwner as EntityMyself).ClearSkill();
        (skillManager as PlayerSkillManager).ClearComboSkill();
        (skillManager as PlayerSkillManager).ResetCoolTime(skillid);
        theOwner.Motor.TurnToControlStickDir();
        EntityMyself.preSkillTime = Time.realtimeSinceStartup;
        theOwner.CastSkill(skillid);
        SkillData s = SkillData.dataMap[skillid];
        //设置UI CD
    }
    public void SpellTwoAttack()
    {
        CleanPreSkill();

        if (CanUseSkill() == false)
        {
            return;
        }

        if ((skillManager as PlayerSkillManager).IsCommonCooldown())
        {
            return;
        }
        int skillid = (skillManager as PlayerSkillManager).GetSpellTwoID();
        if ((skillManager as PlayerSkillManager).IsSkillCooldown(skillid))
        {
            return;
        }

        (theOwner as EntityMyself).ClearSkill();
        (skillManager as PlayerSkillManager).ClearComboSkill();
        (skillManager as PlayerSkillManager).ResetCoolTime(skillid);
        theOwner.Motor.TurnToControlStickDir();
        EntityMyself.preSkillTime = Time.realtimeSinceStartup;
        theOwner.CastSkill(skillid);
        SkillData s = SkillData.dataMap[skillid];
        //设置UI CD
    }
    public void NextCmd()
    {
        if (preCmds.Count == 0)
        {
            return;
        }
        preCmds.RemoveAt(0);
        NormalAttack();
    }
    public void CleanPreSkill()
    {
        preCmds.Clear();
    }
    // 当受击时的出来
    override public void OnHit(int _spellID, uint _attackerID, uint woundId, List<int> harm)
    {
        if (woundId != theOwner.ID)
        {
            return;
        }

        int hitType = harm[0];
        int hitNum = harm[1];
        if (GameWorld.showFloatBlood && SkillAction.dataMap[_spellID].damageFlag == 1)
        {
            FloatBlood(hitType, hitNum);
        }
        if (theOwner.curHp > 0)
        {
            base.OnHit(_spellID, _attackerID, woundId, harm);
        }

    }
}
