using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SkillAction {
        public static string csvFilePath = "Configs/SkillAction";
        public static string[] columnNameArray = new string[33];
        public static List<SkillAction> dataList;
        public static Dictionary<int, SkillAction> dataMap;
        public static List<SkillAction> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<SkillAction>();
            dataMap = new Dictionary<int, SkillAction>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[33];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                SkillAction data = new SkillAction();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                int.TryParse(csvFile.mapData[i].data[1],out data.type);
                columnNameArray [1] = "type";
                int.TryParse(csvFile.mapData[i].data[2],out data.targetType);
                columnNameArray [2] = "targetType";
                int.TryParse(csvFile.mapData[i].data[3],out data.targetRangeType);
                columnNameArray [3] = "targetRangeType";
                data.targetRangeParam= new List<float>();
                strs = csvFile.mapData[i].data[4].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.targetRangeParam.Add(float.Parse(strs[j]));
                }
                columnNameArray [4] = "targetRangeParam";
                float.TryParse(csvFile.mapData[i].data[5],out data.hitXoffset);
                columnNameArray [5] = "hitXoffset";
                float.TryParse(csvFile.mapData[i].data[6],out data.hitYoffset);
                columnNameArray [6] = "hitYoffset";
                int.TryParse(csvFile.mapData[i].data[7],out data.castPosType);
                columnNameArray [7] = "castPosType";
                float.TryParse(csvFile.mapData[i].data[8],out data.damageMul);
                columnNameArray [8] = "damageMul";
                int.TryParse(csvFile.mapData[i].data[9],out data.damageAdd);
                columnNameArray [9] = "damageAdd";
                int.TryParse(csvFile.mapData[i].data[10],out data.maxTargetCount);
                columnNameArray [10] = "maxTargetCount";
                int.TryParse(csvFile.mapData[i].data[11],out data.actionTime);
                columnNameArray [11] = "actionTime";
                int.TryParse(csvFile.mapData[i].data[12],out data.nextHitTime);
                columnNameArray [12] = "nextHitTime";
                int.TryParse(csvFile.mapData[i].data[13],out data.minAttackValue);
                columnNameArray [13] = "minAttackValue";
                int.TryParse(csvFile.mapData[i].data[14],out data.maxAttackValue);
                columnNameArray [14] = "maxAttackValue";
                int.TryParse(csvFile.mapData[i].data[15],out data.action);
                columnNameArray [15] = "action";
                int.TryParse(csvFile.mapData[i].data[16],out data.actionBeginDuration);
                columnNameArray [16] = "actionBeginDuration";
                int.TryParse(csvFile.mapData[i].data[17],out data.duration);
                columnNameArray [17] = "duration";
                int.TryParse(csvFile.mapData[i].data[18],out data.cameraTweenId);
                columnNameArray [18] = "cameraTweenId";
                int.TryParse(csvFile.mapData[i].data[19],out data.cameraTweenSL);
                columnNameArray [19] = "cameraTweenSL";
                int.TryParse(csvFile.mapData[i].data[20],out data.cameraTweenST);
                columnNameArray [20] = "cameraTweenST";
                int.TryParse(csvFile.mapData[i].data[21],out data.hitFxID);
                columnNameArray [21] = "hitFxID";
                data.hitAction= new List<int>();
                strs = csvFile.mapData[i].data[22].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.hitAction.Add(int.Parse(strs[j]));
                }
                columnNameArray [22] = "hitAction";
                data.sfx= new Dictionary<int, float>();
                strs = csvFile.mapData[i].data[23].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    strsTwo = strs[j].Split(new char[1]{':'});
                    if (strsTwo.Length == 2)
                        data.sfx.Add(int.Parse(strsTwo[0]),float.Parse(strsTwo[1]));
                }
                columnNameArray [23] = "sfx";
                data.hitSfx= new List<int>();
                strs = csvFile.mapData[i].data[24].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.hitSfx.Add(int.Parse(strs[j]));
                }
                columnNameArray [24] = "hitSfx";
                float.TryParse(csvFile.mapData[i].data[25],out data.extraSpeed);
                columnNameArray [25] = "extraSpeed";
                int.TryParse(csvFile.mapData[i].data[26],out data.extraSt);
                columnNameArray [26] = "extraSt";
                int.TryParse(csvFile.mapData[i].data[27],out data.extraSl);
                columnNameArray [27] = "extraSl";
                float.TryParse(csvFile.mapData[i].data[28],out data.teleportDistance);
                columnNameArray [28] = "teleportDistance";
                float.TryParse(csvFile.mapData[i].data[29],out data.hitExtraSpeed);
                columnNameArray [29] = "hitExtraSpeed";
                int.TryParse(csvFile.mapData[i].data[30],out data.hitExtraSt);
                columnNameArray [30] = "hitExtraSt";
                int.TryParse(csvFile.mapData[i].data[31],out data.hitExtraSl);
                columnNameArray [31] = "hitExtraSl";
                int.TryParse(csvFile.mapData[i].data[32],out data.damageFlag);
                columnNameArray [32] = "damageFlag";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static SkillAction GetByID (int id,List<SkillAction> data)
        {
            foreach (SkillAction item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static SkillAction GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public int type;//类型
        public int targetType;//目标类型
        public int targetRangeType;//攻击范围类型
        public List<float> targetRangeParam;//范围类型参数
        public float hitXoffset;//hitXoffset
        public float hitYoffset;//hitYoffset
        public int castPosType;//castPosType
        public float damageMul;//damageMul
        public int damageAdd;//damageAdd
        public int maxTargetCount;//目标最多个数
        public int actionTime;//触发时间
        public int nextHitTime;//下次触发时间
        public int minAttackValue;//最小攻击力
        public int maxAttackValue;//最大攻击力
        public int action;//动作值
        public int actionBeginDuration;//动画触发帧数
        public int duration;//技能持续时间
        public int cameraTweenId;//相机特效数据ID
        public int cameraTweenSL;//相机特效延时
        public int cameraTweenST;//相机特效持续
        public int hitFxID;//击中特效
        public List<int> hitAction;//击中行为
        public Dictionary<int, float> sfx;//特效
        public List<int> hitSfx;//受击特效
        public float extraSpeed;//附加速度
        public int extraSt;//延时后位移
        public int extraSl;//持续时间
        public float teleportDistance;//传送距离
        public float hitExtraSpeed;//受击后位移速度
        public int hitExtraSt;//受击延时位移
        public int hitExtraSl;//受击位移时间
        public int damageFlag;//是否产生伤害标志
    }
}
