using UnityEngine;
using System.Collections;
using Mogo.Util;
using BattleFramework.Data;
using System.Collections.Generic;

public class BattleManager{
    protected EntityParent theOwner;
    protected SkillManager skillManager;

    public BattleManager(EntityParent _owner, SkillManager _skillManager)
    {
        this.theOwner = _owner;
        this.skillManager = _skillManager;
        EventDispatcher.AddEventListener<int, uint, uint, List<int>>(Events.FSMMotionEvent.OnHit, OnHit);
    }
    virtual public void OnHit(int _actionID, uint _attackerID, uint woundId, List<int> harm)
    {
        if(woundId != theOwner.ID)
        {
            return;
        }
        //TODO如果死了return
       
        if (!theOwner.immuneShift)
        {
            theOwner.ClearSkill(true);
        }
        SkillAction action = SkillAction.dataMap[_actionID];
        if (action.hitSfx != null && action.hitSfx[0] > 0)
        {
            theOwner.PlayFX((int)action.hitSfx[0]);
        }
        //HitBuff(action);
        List<int> hitAction = action.hitAction;
        if (hitAction == null || hitAction.Count == 0 || theOwner.immuneShift)
        {
            return;
        }
        int _act = Utils.Choice<int>(hitAction);
        bool cfgShow = true;
        //if (((theOwner is EntityMonster) && (theOwner as EntityMonster).ShowHitAct != 0) ||
        //    ((theOwner is EntityDummy) && (theOwner as EntityDummy).ShowHitAct != 0) ||
        //    ((theOwner is EntityMercenary) && (theOwner as EntityMercenary).ShowHitAct != 0))
        //{
        //    cfgShow = false;
        //}
        string actName = theOwner.CurrActStateName();
        if (GameWorld.showHitAction && _act > 0 && theOwner.curHp > 0 &&
            !actName.EndsWith("getup") &&
            !actName.EndsWith("knockout") &&
            !actName.EndsWith(PlayerActionNames.names[ActionConstants.HIT_GROUND]) &&
            //!theOwner.IsInTransition() &&
            cfgShow)
        {//如果没填就不做受击表现
            //TODO切换被击状态
            theOwner.SetAction(_act);
            theOwner.Actor.AddCallbackInFrames<int>(theOwner.SetAction, 0);
            theOwner.ChangeState(FSMStateType.Hit, _actionID, _attackerID);
        }
        if (GameWorld.showHitEM && !(theOwner is EntityMonster) && action.hitExtraSpeed != 0)
        {
            theOwner.Motor.SetExrtaSpeed(-action.hitExtraSpeed);
            theOwner.Motor.SetMoveDirection(theOwner.transform.forward);
            TimerHeap.AddTimer<EntityParent>((uint)(action.hitExtraSl * 1000), 0, (e) =>
            {
                if (e == null || e.Motor == null)
                {
                    return;
                }
                e.Motor.SetExrtaSpeed(0);
            }, theOwner);
        }
    }

    virtual protected void FloatBlood(int type, int num)
    {
        switch (type)
        {
            case 1:
                {//闪避
                    theOwner.Actor.FloatBlood(num, SplitBattleBillboardType.Miss);
                    break;
                }
            case 2:
                {//怪物普通受击
                    theOwner.Actor.FloatBlood(num, SplitBattleBillboardType.NormalMonster);
                    break;
                }
            case 3:
                {//破击
                    theOwner.Actor.FloatBlood(num, SplitBattleBillboardType.BrokenAttack);
                    break;
                }
            case 4:
                {//暴击
                    theOwner.Actor.FloatBlood(num, SplitBattleBillboardType.CriticalMonster);
                    break;
                }
            case 5:
                {//破击加暴击
                    theOwner.Actor.FloatBlood(num, SplitBattleBillboardType.BrokenAttack);
                    break;
                }
        }
    }

    public virtual void Clean()
    {

    }

    // 主动释放技能。直接进入PREPARING放技能
    virtual public void CastSkill(int actionID)
    {
        skillManager.UseSkill(actionID);
    }
}
