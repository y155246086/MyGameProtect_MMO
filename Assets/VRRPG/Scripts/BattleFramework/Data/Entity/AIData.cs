using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class AIData {
        public static string csvFilePath = "Configs/AIData";
        public static string[] columnNameArray = new string[9];
        public static List<AIData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<AIData> dataList = new List<AIData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[9];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                AIData data = new AIData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                float.TryParse(csvFile.mapData[i].data[2],out data.evasionDistance);
                columnNameArray [2] = "evasionDistance";
                float.TryParse(csvFile.mapData[i].data[3],out data.attackDistance);
                columnNameArray [3] = "attackDistance";
                float.TryParse(csvFile.mapData[i].data[4],out data.warningDistance);
                columnNameArray [4] = "warningDistance";
                float.TryParse(csvFile.mapData[i].data[5],out data.fleeHpPercent);
                columnNameArray [5] = "fleeHpPercent";
                int.TryParse(csvFile.mapData[i].data[6],out data.fleeTeammateNum0);
                columnNameArray [6] = "fleeTeammateNum0";
                int.TryParse(csvFile.mapData[i].data[7],out data.fleeTeammateNum1);
                columnNameArray [7] = "fleeTeammateNum1";
                int.TryParse(csvFile.mapData[i].data[8],out data.btId);
                columnNameArray [8] = "btId";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static AIData GetByID (int id,List<AIData> data)
        {
            foreach (AIData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public string name;//名字
        public float evasionDistance;//闪避距离
        public float attackDistance;//攻击距离
        public float warningDistance;//警戒距离
        public float fleeHpPercent;//逃跑血量
        public int fleeTeammateNum0;//群体攻击逃跑时同伴的最多数量
        public int fleeTeammateNum1;//枪声响起逃跑时同伴的最多数量
        public int btId;//逻辑资源ID
    }
}
