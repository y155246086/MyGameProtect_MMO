using BattleFramework.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : FSM.AIController {
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

    internal void SetHurt(int p)
    {
        propertyManager.AddProperty(PropertyType.HP,propertyManager.GetPropertyValue(PropertyType.HP) - p);
    }
}
