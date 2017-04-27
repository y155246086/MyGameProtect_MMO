using UnityEngine;
using System.Collections;
using Mogo.Util;
using System.Collections.Generic;

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
}
