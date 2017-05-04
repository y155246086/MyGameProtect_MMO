using BattleFramework.Data;
using Mogo.Util;
using System;
using System.Collections;
using System.Collections.Generic;

public class StateHit : FSMState
{

    public StateHit(EntityParent owner)
    {
        this.theOwner = owner;
        animator = owner.animator;
    }
    public override void Enter(params System.Object[] args)
    {

        if (theOwner == null || theOwner.transform == null)
            return;

        if (theOwner.IsDead())
            return;

        if (args.Length < 2)
        {
            Debuger.LogError("error skill id");
            return;
        }
        int hitActionID = (int)(args[0]);
        uint attackID = (uint)(args[1]);
        EntityParent attacker = null;
        if (GameWorld.Entities.ContainsKey(attackID))
        {
            attacker = GameWorld.Entities[attackID];
        }
        if (attackID == GameWorld.thePlayer.ID)
        {
            attacker = GameWorld.thePlayer;
        }
        if (theOwner.Motor != null)
        {
            theOwner.Motor.enableStick = false;
        }
        //if (attacker == null)
        //{//没有受击者
        //    return;
        //}
        List<int> hitAction = SkillAction.dataMap[hitActionID].hitAction;
        if (hitAction == null || hitAction.Count == 0)
        {
            return;
        }
        int action = Utils.Choice<int>(hitAction);
        if (action == ActionConstants.HIT)
        {
            HitActionRule(theOwner, action, hitActionID);
            return;
        }
        if (!(theOwner is EntityPlayer))
        {
            if (((theOwner is EntityMonster) && theOwner.GetIntAttr("notTurn") == 0))
            {
                if (theOwner.Motor == null)
                {
                    return;
                }
                if (attacker != null && attacker.transform != null)
                {
                    theOwner.Motor.SetTargetToLookAt(attacker.transform.position);
                }
            }
        }
        else
        {
            if (theOwner.transform == null)
            {
                return;
            }
            theOwner.preQuaternion = theOwner.transform.localRotation;
            if (attacker != null && attacker.transform != null)
                theOwner.Motor.SetTargetToLookAt(attacker.transform.position);
        }
        HitActionRule(theOwner, action, hitActionID);
    }
    private uint changeIdleTime = 0;
    private void HitActionRule(EntityParent theOwner, int act, int hitActionID)
    {
        int action = act;
        string actName = theOwner.CurrActStateName();
        if (actName.EndsWith(PlayerActionNames.names[ActionConstants.KNOCK_DOWN]))
        {//击飞状态，受非浮空打击，一直倒地
            action = ActionConstants.KNOCK_DOWN;
            return;
        }
        if (actName.EndsWith(PlayerActionNames.names[ActionConstants.HIT_AIR]))
        {//浮空受击
            action = ActionConstants.HIT_AIR;
        }
        else if (actName.EndsWith(PlayerActionNames.names[ActionConstants.HIT_GROUND]) || actName.EndsWith("knockout"))
        {
            action = ActionConstants.HIT_GROUND;
        }
        else if ((theOwner is EntityMyself) && (action == ActionConstants.HIT_AIR || action == ActionConstants.KNOCK_DOWN))
        {
            int random = Mogo.Util.RandomHelper.GetRandomInt(0, 100);
            if (random <= 70)
            {//主角受到倒地状态影响时，只有30%机会成功，否则变成普通受击 By.铭
                action = ActionConstants.HIT;
            }
        }

        theOwner.SetAction(action);

        // 回调函数， 切换到 idle
        if (changeIdleTime>0)
            TimerHeap.DelTimer(changeIdleTime);
        changeIdleTime = TimerHeap.AddTimer<EntityParent, int>(100, 0,
            (_theOwner, _hitAct) =>
            {
                if (_theOwner == null)
                {
                    return;
                }
                if (_theOwner.Motor != null)
                {
                    _theOwner.Motor.CancleLookAtTarget();
                }
                //_theOwner.TriggerUniqEvent<int>(Events.FSMMotionEvent.OnHitAnimEnd, _hitAct);
                if (_theOwner is EntityMyself)
                {
                    _theOwner.SetSpeed(0);
                    _theOwner.Motor.SetSpeed(0);
                }
                if(_theOwner is EntityMonster)
                {
                    _theOwner.ChangeState(FSMStateType.Attacking);
                }
            },
            theOwner, hitActionID
        );
    }
    public override void Exit()
    {
    }
    public override void OnUpdate()
    {
        
    }
}
