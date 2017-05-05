using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using System.Collections.Generic;
using Mogo.Util;
using System;


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
            cdDict[data.id] = 0;
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
                    //设置cd
                    cdDict[data.id] = Time.time * 1000;
                    owner.CastSkill(data.id);
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                owner.CastSkill(data.id);
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
        OnAttacking(actionID, ltwm, rotation, forward, position);
    }
    public void UseSkill(int skillDataID)
    {
        Process(owner, skillDataID);
        //UseSkill(SkillData.GetByID(skillid));
    }
    public void OnAttacking(int hitActionID, Matrix4x4 ltwm, Quaternion rotation, Vector3 forward, Vector3 position)
    {
        UseSkill(SkillAction.GetByID(hitActionID), ltwm, rotation, forward, position);
    }
    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="data"></param>
    private void UseSkill(SkillAction data,Matrix4x4 ltwm, Quaternion rotation, Vector3 forward, Vector3 position)
    {
        if (owner is EntityMyself)
        {
            Mogo.Util.EventDispatcher.TriggerEvent(GUIEvent.STOP_JOYSTICK_TURN);
        }
        curSkillData = data;
        
        //播放动作
        SkillAction skillAction = data;
        if (skillAction.action>0)
        {
            if (PlayerActionNames.names.ContainsKey(skillAction.action))
            {
                owner.skillActName = PlayerActionNames.names[skillAction.action];
            }
            else
            {
                owner.skillActName = "";
            }
            owner.SetAction(skillAction.action);
            owner.Actor.AddCallbackInFrames<int>(owner.SetAction, 0);

            TimerHeap.DelTimer(EndAttackTimerID);
            EndAttackTimerID = TimerHeap.AddTimer<SkillAction>((uint)(skillAction.duration), 0, EndAttackAction, skillAction);
            isSkillPlaying = true;
            isCanSkill = false;
        }
        AttackingFx(skillAction);
        AttackingMove(skillAction);
        List<object> args = new List<object>();
        args.Add(ltwm);
        args.Add(rotation);
        args.Add(forward);
        args.Add(position);
        owner.delayAttackTimerID = TimerHeap.AddTimer<int, List<object>>((uint)(skillAction.actionBeginDuration), 0, DelayAttack, skillAction.id, args);
        //TODO 给服务器发送消息
    }
    
    public void Clear()
    {
        //TimerHeap.DelTimer(delayAttackTimerID);
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
    protected void DelayAttack(int actionID, List<object> args)
    {
        Matrix4x4 ltwm = (Matrix4x4)args[0];
        Quaternion rotation = (Quaternion)args[1];
        Vector3 forward = (Vector3)args[2];
        Vector3 position = (Vector3)args[3];
        AttackEffect(actionID, ltwm, rotation, forward, position);
        return;
        //List<EntityParent> list = GetHitEntities(curSkillData.id);
        //for (int i = 0; i < list.Count; i++)
        //{
        //    ICanAttacked att = list[i].Actor.GetComponent<ICanAttacked>();
        //    if (att != null)
        //    {
        //        att.SetHurt(Random.Range(skillAction.minAttackValue, skillAction.maxAttackValue) * 100);
        //    }
        //}
    }
    public void AttackEffect(int hitActionID, Matrix4x4 ltwm, Quaternion rotation, Vector3 forward, Vector3 position)
    {
       SkillAction s = SkillAction.dataMap[hitActionID];

        List<uint> list = GetHitEntities(hitActionID, ltwm, rotation, forward, position);
        List<uint> monsterList = new List<uint>();
        List<uint> playerList = new List<uint>();
        for (int i = 0; i < list.Count; i++)
        {
            if(GameWorld.Entities[list[i]] is EntityMonster)
            {
                monsterList.Add(list[i]);
            }
            if (GameWorld.Entities[list[i]] is EntityMyself)
            {
                playerList.Add(list[i]);
            }
        }
        if (owner is EntityMyself && monsterList.Count > 0)
        {
            AttackMonster(hitActionID, monsterList);
        }
        if (owner is EntityMonster && playerList.Count > 0)
        {
            //TODU怪攻击玩家
            AttackMonster(hitActionID, playerList);
        }
    }
    private void AttackMonster(int hitActionID, List<uint> dummys)
    {
        Dictionary<uint, List<int>> wounded = new Dictionary<uint, List<int>>();
        for (int i = 0; i < dummys.Count; i++)
        {
            List<int> harm = new List<int>();
            if (!GameWorld.Entities.ContainsKey(dummys[i]))
            {
                continue;
            }
            EntityParent e = GameWorld.Entities[dummys[i]];
            //if (Utils.BitTest(e.stateFlag, StateCfg.NO_HIT_STATE) == 1 || Utils.BitTest(e.stateFlag, StateCfg.DEATH_STATE) == 1)
            //{//不可击中状态
            //    continue;
            //}
            harm = CalculateDamage.CacuDamage(hitActionID, owner.ID, dummys[i]);
           
            wounded.Add(dummys[i], harm);
            int h = 0;
            h = (int)(e.curHp < harm[1] ? (uint)e.curHp : (uint)harm[1]);
            e.propertyManager.ChangeProperty(PropertyType.HP, -h);
        }
        TriggerDamage(hitActionID, wounded);
        for (int i = 0; i < dummys.Count; i++)
        {
            if (!GameWorld.Entities.ContainsKey(dummys[i]))
            {
                continue;
            }
            EntityParent e = GameWorld.Entities[dummys[i]];
            //AttackEnemyGenAnger(e);//计算怒气
            if (e.curHp <= 0)
            {//前端怪死亡
                //TODO发送消息
                e.OnDeath(hitActionID);
                (e as EntityParent).stateFlag = Mogo.Util.Utils.BitSet((e as EntityParent).stateFlag, StateCfg.DEATH_STATE);
            }
        }
        //TriggerCombo(dummys.Count);a
    }
    protected void TriggerDamage(int hitActionID, Dictionary<uint, List<int>> wounded)
    {
        foreach (var i in wounded)
        {
            EventDispatcher.TriggerEvent<int, uint, uint, List<int>>(Events.FSMMotionEvent.OnHit, hitActionID, owner.ID, i.Key, i.Value);
        }
    }
    private List<uint> GetHitEntities(int hitActionID, Matrix4x4 ltwm, Quaternion rotation, Vector3 forward, Vector3 position)
    {
        var spellData = SkillAction.dataMap[hitActionID];

        // 目标类型 0 敌人， 1 自己  2 队友  3  友方
        int targetType = spellData.targetType;
        // 攻击范围类型。  0  扇形范围 1  圆形范围， 2， 单体。 3  直线范围 4 前方范围
        int targetRangeType = spellData.targetRangeType;
        // 攻击范围参数。 针对不同类型，有不同意义。 浮点数列表
        List<float> targetRangeParam = spellData.targetRangeParam;
        float offsetX = spellData.hitXoffset;
        float offsetY = spellData.hitYoffset;
        float angleOffset = 180;
        // 最大攻击人数
        //int maxTargetCount = spellData.maxTargetCount;
        // 触发伤害特效帧数
        //int damageTriggerFrame = spellData.damageTriggerFrame;
        
        List<uint> entities = new List<uint>();

        if (targetType == (int)TargetSelectType.Myself)
        {
            entities.Add(owner.ID);
            return entities;
        }
        if (owner.transform == null)
        {
            return entities;
        }
        Matrix4x4 entityltwm = owner.transform.localToWorldMatrix;
        Quaternion entityrotation = owner.transform.rotation;
        Vector3 entityforward = owner.transform.forward;
        Vector3 entityposition = owner.transform.position;
        if (spellData.castPosType == 0)
        {
            entityltwm = ltwm;
            entityrotation = rotation;
            entityforward = forward;
            entityposition = position;
        }
        TargetRangeType rangeType = (TargetRangeType)targetRangeType;
        switch (rangeType)
        {
            case TargetRangeType.CircleRange:
                if (targetRangeParam.Count >= 1)
                {
                    float radius = targetRangeParam[0] * 0.01f;
                    
                    entities = GameCommonUtils.GetEntitiesInRange(entityltwm, entityrotation, entityforward, entityposition, radius, offsetX, offsetY, angleOffset);
                }
                break;
            case TargetRangeType.SectorRange:
                if (targetRangeParam.Count >= 2)
                {
                    float radius = targetRangeParam[0] * 0.01f;
                    float angle = targetRangeParam[1];
                    entities = GameCommonUtils.GetEntitiesInSector(entityltwm, entityrotation, entityforward, entityposition, radius, angle, offsetX, offsetY, angleOffset);
                    //entities = Utils.GetEntities(theOwner.Transform, radius, angle);
                }
                break;
            case TargetRangeType.SingeTarget:
                if (targetRangeParam.Count >= 1)
                {
                    float radius = targetRangeParam[0] * 0.01f;
                    float angle = 150;
                    entities = GameCommonUtils.GetEntitiesInSector(entityltwm, entityrotation, entityforward, entityposition, radius, angle, offsetX, offsetY, angleOffset);
                    GameCommonUtils.SortByDistance(owner.transform, entities);
                    if (entities.Count > 1)
                    {
                        for (int i = 1; i < entities.Count; i++)
                        {
                            entities.RemoveAt(i);
                        }
                    }
                }
                break;
            case TargetRangeType.WorldRange:
                if (targetRangeParam.Count >= 4)
                {
                    float x1 = targetRangeParam[0] * 0.01f;
                    float y1 = targetRangeParam[1] * 0.01f;
                    float x2 = targetRangeParam[2] * 0.01f;
                    float y2 = targetRangeParam[3] * 0.01f;
                    float minX = Math.Min(x1, x2);
                    float maxX = Math.Max(x1, x2);
                    float minY = Math.Min(y1, y2);
                    float maxY = Math.Max(y1, y2);
                    float radiusX = minX + (maxX - minX) * 0.5f;
                    float radiusY = minY + (maxY - minY) * 0.5f;
                    float radius = Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2)) * 0.5f;
                    entities = GameCommonUtils.GetEntitiesInRange(new Vector3(radiusX, 0, radiusY), radius);
                }
                break;
            case TargetRangeType.LineRange:
            default:
                if (targetRangeParam.Count >= 2)
                {
                    float length = targetRangeParam[0] * 0.01f;
                    float width = targetRangeParam[1] * 0.01f;
                    entities = GameCommonUtils.GetEntitiesFrontLineNew(entityltwm, entityrotation, entityforward, entityposition, length, entityforward, width, offsetX, offsetY, angleOffset);
                }
                break;
        }
        return entities;

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
            return GameWorld.thePlayer.transform;
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
                    return GameWorld.thePlayer.transform;
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
    private void AttackingMove(SkillAction action)
    {
        MotorParent theMotor = owner.Motor;
        if (theMotor == null)
        {
            return;
        }
        float extraSpeed = action.extraSpeed;
        if (extraSpeed != 0)
        {
            if (action.extraSt <= 0)
            {
                theMotor.SetExrtaSpeed(extraSpeed);
                theMotor.SetMoveDirection(owner.transform.forward);
                TimerHeap.AddTimer<MotorParent>((uint)action.extraSl, 0, (m) => { m.SetExrtaSpeed(0); }, theMotor);
            }
            else
            {
                TimerHeap.AddTimer<int>((uint)action.extraSt, 0, DelayExtraMove, action.id);
            }
        }
        else
        {
            theMotor.SetExrtaSpeed(0);
        }

        // 是否允许，在技能过程中使用 摇杆改变方向
        //if (theOwner is EntityMyself)
        //{
        //    theMotor.enableStick = action.enableStick > 0;
        //}

        if (action.teleportDistance > 0 && extraSpeed <= 0)
        {
            Vector3 dst = Vector3.zero;
            dst = owner.transform.position +owner.transform.forward * action.teleportDistance;
            theMotor.TeleportTo(dst);
        }
    }
    private void DelayExtraMove(int hitActionID)
    {
        SkillAction action = SkillAction.dataMap[hitActionID];
        MotorParent theMotor = owner.Motor;
        float extraSpeed = action.extraSpeed;
        theMotor.SetExrtaSpeed(extraSpeed);
        theMotor.SetMoveDirection(owner.transform.forward);
        TimerHeap.AddTimer<MotorParent>((uint)action.extraSl, 0, (m) => { m.SetExrtaSpeed(0); }, theMotor);
    }
}
