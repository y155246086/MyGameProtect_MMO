using BattleFramework.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : FSM.AIController,ICanAttacked {
    /// <summary>
    /// 动画的播放速率
    /// </summary>
    public float aiRate;
    
    public override float ChaseDistance
    {
        get
        {
            return data.floowDis;
        }
    }
    public override float AttackDistance
    {
        get
        {
            return data.attackDis;
        }
    }
    public override float ArriveDistance
    {
        get
        {
            return base.ArriveDistance;
        }
    }
    public void SetHurt(int value)
    {
        Debuger.Log("产生伤害：" + value);
        Play("Action", 11);
        AddCallbackInFrames<string,int>(Play, "Action", 0);
    }
    /// <summary>
    /// 设置怪物数据
    /// </summary>
    /// <param name="data"></param>
    public void SetData(MonsterData data)
    {
        this.data = data;
    }
    /// <summary>
    /// 设置怪物数据
    /// </summary>
    /// <param name="monsterId">怪物数据ID</param>
    public void SetData(int monsterId)
    {
        
        data = MonsterData.GetByID(monsterId);
    }
    protected override void Initialize()
    {
        base.Initialize();
        propertyManager.AddProperty(PropertyType.HP, data.currentHP);
        propertyManager.AddProperty(PropertyType.MAX_HP, data.maxHP);

        skillManager = this.gameObject.AddComponent<SkillManager>();
        skillManager.SetOwenr(this);
        SetOwenr(this);
    }
    public void SetOwenr(MonsterAI owner)
    {
        if (owner.data.skillID1 > 0)
            skillManager.AddSkill(owner.data.skillID1);
        if (owner.data.skillID2 > 0)
            skillManager.AddSkill(owner.data.skillID2);
        if (owner.data.skillID3 > 0)
            skillManager.AddSkill(owner.data.skillID3);
    }
    public Transform AttackTarget
    {
        get
        {
            return playerTransfrom;
        }
    }

    public bool TargetIsDead()
    {
        return false;
    }

    public bool IsDead()
    {
        return propertyManager.GetPropertyValue(PropertyType.HP)<=0;
    }
}
