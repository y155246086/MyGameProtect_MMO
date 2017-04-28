using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SkillAction {
        public static string csvFilePath = "Configs/SkillAction";
        public static string[] columnNameArray = new string[19];
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
            columnNameArray = new string[19];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                SkillAction data = new SkillAction();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                int.TryParse(csvFile.mapData[i].data[2],out data.type);
                columnNameArray [2] = "type";
                int.TryParse(csvFile.mapData[i].data[3],out data.targetType);
                columnNameArray [3] = "targetType";
                int.TryParse(csvFile.mapData[i].data[4],out data.targetRangeType);
                columnNameArray [4] = "targetRangeType";
                data.targetRangeParam = csvFile.mapData[i].data[5];
                columnNameArray [5] = "targetRangeParam";
                int.TryParse(csvFile.mapData[i].data[6],out data.actionTime);
                columnNameArray [6] = "actionTime";
                int.TryParse(csvFile.mapData[i].data[7],out data.nextHitTime);
                columnNameArray [7] = "nextHitTime";
                int.TryParse(csvFile.mapData[i].data[8],out data.minAttackValue);
                columnNameArray [8] = "minAttackValue";
                int.TryParse(csvFile.mapData[i].data[9],out data.maxAttackValue);
                columnNameArray [9] = "maxAttackValue";
                int.TryParse(csvFile.mapData[i].data[10],out data.action);
                columnNameArray [10] = "action";
                int.TryParse(csvFile.mapData[i].data[11],out data.triggerTime);
                columnNameArray [11] = "triggerTime";
                int.TryParse(csvFile.mapData[i].data[12],out data.duration);
                columnNameArray [12] = "duration";
                int.TryParse(csvFile.mapData[i].data[13],out data.cameraTweenId);
                columnNameArray [13] = "cameraTweenId";
                int.TryParse(csvFile.mapData[i].data[14],out data.cameraTweenSL);
                columnNameArray [14] = "cameraTweenSL";
                int.TryParse(csvFile.mapData[i].data[15],out data.cameraTweenST);
                columnNameArray [15] = "cameraTweenST";
                int.TryParse(csvFile.mapData[i].data[16],out data.hitFxID);
                columnNameArray [16] = "hitFxID";
                int.TryParse(csvFile.mapData[i].data[17],out data.hitAction);
                columnNameArray [17] = "hitAction";
                data.sfx= new Dictionary<int, float>();
                strs = csvFile.mapData[i].data[18].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    strsTwo = strs[j].Split(new char[1]{':'});
                    if (strsTwo.Length == 2)
                        data.sfx.Add(int.Parse(strsTwo[0]),float.Parse(strsTwo[1]));
                }
                columnNameArray [18] = "sfx";
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
        public string name;//名字
        public int type;//类型
        public int targetType;//目标类型
        public int targetRangeType;//攻击范围类型
        public string targetRangeParam;//范围类型参数
        public int actionTime;//触发时间
        public int nextHitTime;//下次触发时间
        public int minAttackValue;//最小攻击力
        public int maxAttackValue;//最大攻击力
        public int action;//动作值
        public int triggerTime;//动画触发帧数
        public int duration;//技能持续时间
        public int cameraTweenId;//相机特效数据ID
        public int cameraTweenSL;//相机特效延时
        public int cameraTweenST;//相机特效持续
        public int hitFxID;//击中特效
        public int hitAction;//击中行为
        public Dictionary<int, float> sfx;//特效
    }
}
