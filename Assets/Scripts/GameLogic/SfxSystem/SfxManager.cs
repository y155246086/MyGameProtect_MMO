using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mogo.Util;
using BattleFramework.Data;

public class SfxManager{
    private FSM.AIController theOwner;
    public Dictionary<int, List<uint>> sfxTimerIDDic { get; protected set; }
    public SfxManager(FSM.AIController owenr)
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
        SkillData skillData = SkillData.GetByID(skillID);
        if (skillData == null)
        {
            LoggerHelper.Error("not exist spell data:" + skillID);
            return;
        }

        // 从技能表中获取 sfx 配置
        // 逐个， 按序， 按时触发特效
        //key  特效id value 延时启动时间
        Dictionary<int, float> sfx = new Dictionary<int, float>(); ;

        List<EffectData> effectList = EffectData.dataList;
        for (int i = 0; i < effectList.Count; i++)
        {
            if(effectList[i].skillId == skillID)
            {
                sfx.Add(effectList[i].id, effectList[i].delay);
            }
        }

        SfxHandler cueHandler = theOwner.sfxHandler;

        if (sfx != null && sfx.Count > 0)
        {
            if (!sfxTimerIDDic.ContainsKey(skillID))
                sfxTimerIDDic.Add(skillID, new List<uint>());
            foreach (var pair in sfx)
            {
                sfxTimerIDDic[skillID].Add(FrameTimerHeap.AddTimer((uint)(1000 * pair.Value), 0, TriggerCues, cueHandler, pair.Key));
                //if (pair.Key < 1000)
                //{
                //    sfxTimerIDDic[skillID].Add(FrameTimerHeap.AddTimer((uint)(1000 * pair.Value), 0, PlayUIFx, pair.Key));
                //}
                //else
                //{
                //    sfxTimerIDDic[skillID].Add(FrameTimerHeap.AddTimer((uint)(1000 * pair.Value), 0, TriggerCues, cueHandler, pair.Key));
                //}
            }
        }
        return;
    }
    public void RemoveSfx()
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
}
