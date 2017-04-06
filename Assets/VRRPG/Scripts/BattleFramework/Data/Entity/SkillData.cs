using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SkillData {
        public static string csvFilePath = "Configs/SkillData";
        public static string[] columnNameArray = new string[16];
        public static List<SkillData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<SkillData> dataList = new List<SkillData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[16];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                SkillData data = new SkillData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                int.TryParse(csvFile.mapData[i].data[2],out data.type);
                columnNameArray [2] = "type";
                data.shootPrefabs = csvFile.mapData[i].data[3];
                columnNameArray [3] = "shootPrefabs";
                int.TryParse(csvFile.mapData[i].data[4],out data.minAttackValue);
                columnNameArray [4] = "minAttackValue";
                int.TryParse(csvFile.mapData[i].data[5],out data.maxAttackValue);
                columnNameArray [5] = "maxAttackValue";
                float.TryParse(csvFile.mapData[i].data[6],out data.speed);
                columnNameArray [6] = "speed";
                float.TryParse(csvFile.mapData[i].data[7],out data.angle);
                columnNameArray [7] = "angle";
                data.attackedEffect = csvFile.mapData[i].data[8];
                columnNameArray [8] = "attackedEffect";
                data.attackedSound = csvFile.mapData[i].data[9];
                columnNameArray [9] = "attackedSound";
                float.TryParse(csvFile.mapData[i].data[10],out data.cd);
                columnNameArray [10] = "cd";
                data.triggerName = csvFile.mapData[i].data[11];
                columnNameArray [11] = "triggerName";
                data.stateName = csvFile.mapData[i].data[12];
                columnNameArray [12] = "stateName";
                float.TryParse(csvFile.mapData[i].data[13],out data.triggerTime);
                columnNameArray [13] = "triggerTime";
                int.TryParse(csvFile.mapData[i].data[14],out data.zhaohuanId);
                columnNameArray [14] = "zhaohuanId";
                data.skillSound = csvFile.mapData[i].data[15];
                columnNameArray [15] = "skillSound";
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
  
        public int id;//数据ID
        public string name;//名字
        public int type;//类型
        public string shootPrefabs;//弹道资源
        public int minAttackValue;//最小攻击力
        public int maxAttackValue;//最大攻击力
        public float speed;//弹道速度
        public float angle;//弹道角度
        public string attackedEffect;//攻击特效
        public string attackedSound;//击中声音
        public float cd;//技能CD
        public string triggerName;//动画触发名称
        public string stateName;//动画状态机状态名称
        public float triggerTime;//动画触发时间
        public int zhaohuanId;//召唤关卡ID
        public string skillSound;//技能声音
    }
}
