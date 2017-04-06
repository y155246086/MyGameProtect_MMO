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
    }
    /// <summary>
    /// 技能触发特效，一个技能对应多个特效
    /// </summary>
    /// <param name="actionID"></param>
    public void PlaySfx(int actionID)
    {
        SkillData skillData = SkillData.GetByID(actionID, DataCenter.Instance().skillDataList);
        if (skillData == null)
        {
            LoggerHelper.Error("not exist spell data:" + actionID);
            return;
        }

        // 从技能表中获取 sfx 配置
        // 逐个， 按序， 按时触发特效
        //key  特效id value 延时启动时间
        Dictionary<int, float> sfx = new Dictionary<int, float>(); ;

        List<EffectData> effectList = DataCenter.Instance().effectDataList;
        for (int i = 0; i < effectList.Count; i++)
        {
            if(effectList[i].skillId == actionID)
            {
                sfx.Add(effectList[i].id, effectList[i].delay);
            }
        }

        SfxHandler cueHandler = theOwner.sfxHandler;

        if (sfx != null && sfx.Count > 0)
        {
            if (!sfxTimerIDDic.ContainsKey(actionID))
                sfxTimerIDDic.Add(actionID, new List<uint>());
            foreach (var pair in sfx)
            {
                if (pair.Key < 1000)
                {
                    sfxTimerIDDic[actionID].Add(FrameTimerHeap.AddTimer((uint)(1000 * pair.Value), 0, PlayUIFx, pair.Key));
                }
                else
                {
                    sfxTimerIDDic[actionID].Add(FrameTimerHeap.AddTimer((uint)(1000 * pair.Value), 0, TriggerCues, cueHandler, pair.Key));
                }
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
