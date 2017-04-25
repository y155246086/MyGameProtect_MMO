using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SkillData {
        public static string csvFilePath = "Configs/SkillData";
        public static string[] columnNameArray = new string[23];
        public static List<SkillData> dataList;
        public static List<SkillData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<SkillData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[23];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                SkillData data = new SkillData();
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
                data.shootPrefabs = csvFile.mapData[i].data[6];
                columnNameArray [6] = "shootPrefabs";
                int.TryParse(csvFile.mapData[i].data[7],out data.minAttackValue);
                columnNameArray [7] = "minAttackValue";
                int.TryParse(csvFile.mapData[i].data[8],out data.maxAttackValue);
                columnNameArray [8] = "maxAttackValue";
                float.TryParse(csvFile.mapData[i].data[9],out data.speed);
                columnNameArray [9] = "speed";
                float.TryParse(csvFile.mapData[i].data[10],out data.angle);
                columnNameArray [10] = "angle";
                data.attackedEffect = csvFile.mapData[i].data[11];
                columnNameArray [11] = "attackedEffect";
                data.attackedSound = csvFile.mapData[i].data[12];
                columnNameArray [12] = "attackedSound";
                float.TryParse(csvFile.mapData[i].data[13],out data.cd);
                columnNameArray [13] = "cd";
                int.TryParse(csvFile.mapData[i].data[14],out data.action);
                columnNameArray [14] = "action";
                float.TryParse(csvFile.mapData[i].data[15],out data.triggerTime);
                columnNameArray [15] = "triggerTime";
                float.TryParse(csvFile.mapData[i].data[16],out data.duration);
                columnNameArray [16] = "duration";
                data.skillSound = csvFile.mapData[i].data[17];
                columnNameArray [17] = "skillSound";
                int.TryParse(csvFile.mapData[i].data[18],out data.cameraTweenId);
                columnNameArray [18] = "cameraTweenId";
                float.TryParse(csvFile.mapData[i].data[19],out data.cameraTweenSL);
                columnNameArray [19] = "cameraTweenSL";
                float.TryParse(csvFile.mapData[i].data[20],out data.cameraTweenST);
                columnNameArray [20] = "cameraTweenST";
                int.TryParse(csvFile.mapData[i].data[21],out data.hitFxID);
                columnNameArray [21] = "hitFxID";
                int.TryParse(csvFile.mapData[i].data[22],out data.hitAction);
                columnNameArray [22] = "hitAction";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static SkillData GetByID (int id,List<SkillData> data)
        {
            foreach (SkillData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static SkillData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string name;//名字
        public int type;//类型
        public int targetType;//目标类型
        public int targetRangeType;//攻击范围类型
        public string targetRangeParam;//范围类型参数
        public string shootPrefabs;//弹道资源
        public int minAttackValue;//最小攻击力
        public int maxAttackValue;//最大攻击力
        public float speed;//弹道速度
        public float angle;//弹道角度
        public string attackedEffect;//攻击特效
        public string attackedSound;//击中声音
        public float cd;//技能CD
        public int action;//动作值
        public float triggerTime;//动画触发帧数
        public float duration;//技能持续时间
        public string skillSound;//技能声音
        public int cameraTweenId;//相机特效数据ID
        public float cameraTweenSL;//相机特效延时
        public float cameraTweenST;//相机特效持续
        public int hitFxID;//击中特效
        public int hitAction;//击中行为
    }
}
