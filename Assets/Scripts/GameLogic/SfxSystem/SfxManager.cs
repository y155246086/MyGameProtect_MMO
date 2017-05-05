using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mogo.Util;
using BattleFramework.Data;

public class SfxManager{
    private EntityParent theOwner;
    public Dictionary<int, List<uint>> sfxTimerIDDic { get; protected set; }
    public SfxManager(EntityParent owenr)
    {
        this.theOwner = owenr;
        sfxTimerIDDic = new Dictionary<int, List<uint>>();
    }
    /// <summary>
    /// 技能触发特效，一个技能对应多个特效
    /// </summary>
    /// <param name="skillID"></param>
    public void PlaySfx(int skillID)
    {
        SkillAction skillData = SkillAction.GetByID(skillID);
        if (skillData == null)
        {
            Debuger.LogError("not exist spell data:" + skillID);
            return;
        }

        // 从技能表中获取 sfx 配置
        // 逐个， 按序， 按时触发特效
        //key  特效id value 延时启动时间
        Dictionary<int, float> sfx = skillData.sfx;

        SfxHandler cueHandler = theOwner.sfxHandler;

        if (sfx != null && sfx.Count > 0)
        {
            if (!sfxTimerIDDic.ContainsKey(skillID))
                sfxTimerIDDic.Add(skillID, new List<uint>());
            foreach (var pair in sfx)
            {
                sfxTimerIDDic[skillID].Add(FrameTimerHeap.AddTimer((uint)(1000 * pair.Value), 0, TriggerCues, cueHandler, pair.Key));
            }
        }
        return;
    }
    public void RemoveSfx(int actionID)
    {
        List<uint> sfxs = null;
        sfxTimerIDDic.TryGetValue(actionID, out sfxs);
        if (sfxs == null)
        {
            return;
        }
        foreach (var item in sfxs)
        {
            FrameTimerHeap.DelTimer(item);
        }
        Dictionary<int, float> sfx = SkillAction.dataMap[actionID].sfx;
        if (sfx == null)
        {
            return;
        }
        SfxHandler cueHandler = theOwner.sfxHandler;
        foreach (var item in sfx)
        {
            if (cueHandler)
                cueHandler.RemoveFXs(item.Key);
        }
        sfxs.Clear();
    }
    public void StopUIFx(int id)
    {
        
    }
    public void PlayUIFx(int id)
    {
    }
    private void TriggerCues(SfxHandler cueHandler, int cuesID)
    {
        if (cueHandler)
            cueHandler.HandleFx(cuesID);
    }
    public void Clear()
    {

    }
}
