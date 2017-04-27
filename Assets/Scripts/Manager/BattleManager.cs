using UnityEngine;
using System.Collections;

public class BattleManager{
    protected EntityParent theOwner;
    protected SkillManager skillManager;

    public BattleManager(EntityParent _owner)
    {
        this.theOwner = _owner;
    }

    public BattleManager(EntityParent _owner, SkillManager _skillManager)
    {
        // TODO: Complete member initialization
        this.theOwner = _owner;
        this.skillManager = _skillManager;
    }



    public virtual void Clean()
    {

    }

    // 主动释放技能。直接进入PREPARING放技能
    virtual public void CastSkill(int actionID)
    {
        //theOwner.ChangeMotionState(MotionState.ATTACKING, actionID);
        skillManager.UseSkill(actionID);
    }
}
