using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleFramework.Data;
using System;
using Mogo.Util;
public class SkillMapping
{
    public int normalAttack = 5001;
    public int spellOne = 6101;
    public int spellTwo = 0;
    public int spellThree = 0;
    public int powerupAttack = 0;
    public int maxPowerupAttack = 0;
    public List<int> powers = new List<int>();

    public void clean()
    {
        normalAttack = 3;
        spellOne = 0;
        spellTwo = 0;
        spellThree = 0;
        powerupAttack = 0;
        maxPowerupAttack = 0;
        powers.Clear();
    }

    public bool hadStudy(int id)
    {
        return false;
    }
}
public class PlayerSkillManager : SkillManager {

    // 公共cd 为 0.2 秒
    public int CommonCD = 200;
    private float lastAttackTime = 0.0f;
    private int lastSkillID = 0;
    private int currentSpellID = 0;    // 当前正在使用的技能配置数据(未使用技能时为0)
    private int currentHitSpellID = 0; // 受击技能
    private int lastPowerSkillID = 0;
    public bool isAnger = false;

    // 技能cd 相关
    private Dictionary<int, float> skilllastCastTime = new Dictionary<int, float>();
    private Dictionary<int, int> skillCoolTime = new Dictionary<int, int>();//技能cd

    private Dictionary<int, int> dependenceSkill = new Dictionary<int, int>();
    private Dictionary<int, int> comboSkillPeriodStart = new Dictionary<int, int>();//记录连续技开始范围
    private Dictionary<int, int> comboSkillPeriod = new Dictionary<int, int>();// 记录连续技最大有效时间 结束范围
    private Dictionary<int, int> commonCD = new Dictionary<int, int>();//最小攻击间隔CD
    private EntityMyself owner;
    private SkillMapping skillMapping = new SkillMapping();
    public PlayerSkillManager(EntityParent owner) : base(owner)
    {
        this.owner = owner as EntityMyself;
        try
        {
            InitData();
        }
        catch (Exception ex)
        {
            Debuger.LogError(ex);
        }
    }
    // 初始化 技能数据
    public void InitData()
    {
        // 初始化 技能冷却时间： 技能cd， 连续技cd，连续技最大有效时间，组技能cd, 
        for (int i = 0; i < DataCenter.Instance().list_SkillData.Count; i++)
        {
            SkillData skillData = DataCenter.Instance().list_SkillData[i];
            if (skillData.cd.Count == 4)
            {
                // 技能 cd
                skillCoolTime[skillData.id] = skillData.cd[0];
                if (skillData.cd[2] > 0)
                {
                    // 记录连续技最大有效时间
                    comboSkillPeriod[skillData.id] = skillData.cd[2];
                }
                comboSkillPeriodStart[skillData.id] = skillData.cd[1];
                commonCD[skillData.id] = skillData.cd[3];
            }
            else
            {
                Debuger.LogError("format error: spell cool time:" + skillData.id);
            }
        }
        

        // 依赖技能： 前置技能， 前置次数， 后置技能
        
        foreach (KeyValuePair<int, SkillData> pair in SkillData.dataMap)
        {
            if (pair.Value.dependSkill > 0)
            {
                dependenceSkill[pair.Key] = pair.Value.dependSkill;
            }
        }
        lastSkillID = skillMapping.normalAttack;
    }
    public bool IsCommonCooldown()
    {
        int attackInterval = (int)((Time.realtimeSinceStartup - lastAttackTime) * 1000);

        if (attackInterval < CommonCD)
        {
            Debuger.Log("common cool down time");
            return true;
        }
        return false;
    }
    public bool IsSkillCooldown(int skillID)
    {
        if (!SkillData.dataMap.ContainsKey(skillID))
        {
            return true;
        }
        if (!this.skilllastCastTime.ContainsKey(skillID))
        {
            skilllastCastTime[skillID] = 0;
        }
        int skillInterval = (int)((Time.realtimeSinceStartup - this.skilllastCastTime[skillID]) * 1000);
        if (!this.skillCoolTime.ContainsKey(skillID))
        {
            skillCoolTime[skillID] = 0;
        }
        if (skillInterval < this.skillCoolTime[skillID])
        {
            Debuger.Log("skill cool down time");
            return true;
        }
        return false;

    }
    public void ResetCoolTime(int skillID)
    {
        CommonCD = commonCD[skillID];
        lastAttackTime = Time.realtimeSinceStartup;
        skilllastCastTime[skillID] = lastAttackTime;
    }
    public void Compensation(float t)
    {
        lastAttackTime += t;
        List<int> key = new List<int>();
        foreach (var item in skilllastCastTime)
        {
            key.Add(item.Key);
        }
        for (int i = 0; i < key.Count; i++)
        {
            skilllastCastTime[key[i]] += t;
        }
    }
    public void ClearComboSkill()
    {
        lastSkillID = 0;
        lastPowerSkillID = 0;
    }
    public int GetCommonCD(int id)
    {
        if (!commonCD.ContainsKey(id))
        {
            return 0;
        }
        return commonCD[id];
    }
    public int GetNormalOne()
    {
        return skillMapping.normalAttack;
    }
    // 获取普通攻击，技能id
    public int GetNormalAttackID()
    {
        if (isAnger)
        {//怒气
            return GetSuperAttackID();
        }
        int interval = (int)((Time.realtimeSinceStartup - lastAttackTime) * 1000);

        if (dependenceSkill.ContainsKey(lastSkillID) && this.comboSkillPeriod.ContainsKey(lastSkillID))
        {
            int nextSkill = dependenceSkill[lastSkillID];
            int cd = comboSkillPeriodStart[lastSkillID];
            if (commonCD[lastSkillID] > cd)
            {
                cd = commonCD[lastSkillID];
            }
            if (nextSkill > 0 && interval > cd && interval < this.comboSkillPeriod[lastSkillID])
            {
                lastSkillID = nextSkill;
                return nextSkill;
            }
        }
        lastSkillID = skillMapping.normalAttack;
        return skillMapping.normalAttack;
    }
    public bool HasDependence(int skillId)
    {
        if (dependenceSkill.ContainsKey(skillId) && dependenceSkill[skillId] > 0)
        {
            return true;
        }
        return false;
    }

    public int GetSuperAttackID()
    {
        int interval = (int)((Time.realtimeSinceStartup - lastAttackTime) * 1000);

        if (dependenceSkill.ContainsKey(lastSkillID) && this.comboSkillPeriod.ContainsKey(lastSkillID))
        {
            int nextSkill = dependenceSkill[lastSkillID];
            int cd = comboSkillPeriodStart[lastSkillID];
            if (commonCD[lastSkillID] > cd)
            {
                cd = commonCD[lastSkillID];
            }
            if (nextSkill > 0 && interval > cd && interval < this.comboSkillPeriod[lastSkillID])
            {
                lastSkillID = nextSkill;
                return nextSkill;
            }
        }
        lastSkillID = skillMapping.powerupAttack;
        return skillMapping.powerupAttack;
    }
    public int GetSpellOneID()
    {
        return skillMapping.spellOne;
    }

    public int GetSpellTwoID()
    {
        return skillMapping.spellTwo;
    }

    public int GetSpellThreeID()
    {
        return skillMapping.spellThree;
    }

    public bool ChargeAble()
    {
        return skillMapping.powerupAttack > 0;
    }
    #region Combo Part

    protected static readonly uint resetComboTime = 5000;
    protected bool hasResetCombo = false;
    protected uint resetComboTimerID = uint.MaxValue;

    protected int comboNumber = 0;
    protected int maxCombo = 0;

    protected void AddCombo(int num)
    {
        if (num > 0)
        {
            if (!hasResetCombo)
            {
                TimerHeap.DelTimer(resetComboTimerID);
            }
            else
            {
                hasResetCombo = false;
            }

            comboNumber += num;
            if (comboNumber > maxCombo)
            {
                maxCombo = comboNumber;
                // EventDispatcher.TriggerEvent(Events.InstanceEvent.UploadMaxCombo, maxCombo);
            }

            resetComboTimerID = TimerHeap.AddTimer(resetComboTime, 0, ResetCombo);
            //MainUIViewManager.Instance.SetComboAttackNum(comboNumber);
        }
    }

    protected void ResetCombo()
    {
        hasResetCombo = true;
        comboNumber = 0;
        //MainUIViewManager.Instance.SetComboAttackNum(comboNumber);
    }

    protected void ForceResetCombo()
    {
        TimerHeap.DelTimer(resetComboTimerID);
        ResetCombo();
    }

    public int GetMaxCombo()
    {
        return maxCombo;
    }

    public void ResetMaxCombo()
    {
        maxCombo = 0;
    }

    #endregion
}
