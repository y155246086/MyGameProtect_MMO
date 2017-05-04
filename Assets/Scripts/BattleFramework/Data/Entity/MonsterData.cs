using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class MonsterData {
        public static string csvFilePath = "Configs/MonsterData";
        public static string[] columnNameArray = new string[19];
        public static List<MonsterData> dataList;
        public static Dictionary<int, MonsterData> dataMap;
        public static List<MonsterData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<MonsterData>();
            dataMap = new Dictionary<int, MonsterData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[19];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                MonsterData data = new MonsterData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                int.TryParse(csvFile.mapData[i].data[1],out data.modelId);
                columnNameArray [1] = "modelId";
                data.resourceName = csvFile.mapData[i].data[2];
                columnNameArray [2] = "resourceName";
                int.TryParse(csvFile.mapData[i].data[3],out data.type);
                columnNameArray [3] = "type";
                data.name = csvFile.mapData[i].data[4];
                columnNameArray [4] = "name";
                int.TryParse(csvFile.mapData[i].data[5],out data.currentHP);
                columnNameArray [5] = "currentHP";
                int.TryParse(csvFile.mapData[i].data[6],out data.maxHP);
                columnNameArray [6] = "maxHP";
                float.TryParse(csvFile.mapData[i].data[7],out data.guardRadius);
                columnNameArray [7] = "guardRadius";
                int.TryParse(csvFile.mapData[i].data[8],out data.guardType);
                columnNameArray [8] = "guardType";
                float.TryParse(csvFile.mapData[i].data[9],out data.patrolRadius);
                columnNameArray [9] = "patrolRadius";
                int.TryParse(csvFile.mapData[i].data[10],out data.patrolType);
                columnNameArray [10] = "patrolType";
                float.TryParse(csvFile.mapData[i].data[11],out data.PatrolCD);
                columnNameArray [11] = "PatrolCD";
                float.TryParse(csvFile.mapData[i].data[12],out data.floowDis);
                columnNameArray [12] = "floowDis";
                float.TryParse(csvFile.mapData[i].data[13],out data.attackDis);
                columnNameArray [13] = "attackDis";
                int.TryParse(csvFile.mapData[i].data[14],out data.attackValue);
                columnNameArray [14] = "attackValue";
                data.skillIDs= new List<int>();
                strs = csvFile.mapData[i].data[15].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.skillIDs.Add(int.Parse(strs[j]));
                }
                columnNameArray [15] = "skillIDs";
                int.TryParse(csvFile.mapData[i].data[16],out data.isRemove);
                columnNameArray [16] = "isRemove";
                int.TryParse(csvFile.mapData[i].data[17],out data.exp);
                columnNameArray [17] = "exp";
                data.hitShader= new List<int>();
                strs = csvFile.mapData[i].data[18].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.hitShader.Add(int.Parse(strs[j]));
                }
                columnNameArray [18] = "hitShader";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static MonsterData GetByID (int id,List<MonsterData> data)
        {
            foreach (MonsterData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static MonsterData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public int modelId;//外形数据
        public string resourceName;//资源路径
        public int type;//类型
        public string name;//名字
        public int currentHP;//当前血量
        public int maxHP;//最大血量
        public float guardRadius;//警惕范围
        public int guardType;//警惕类型
        public float patrolRadius;//巡逻半径
        public int patrolType;//巡逻类型
        public float PatrolCD;//巡逻CD
        public float floowDis;//追击范围
        public float attackDis;//攻击距离
        public int attackValue;//攻击力
        public List<int> skillIDs;//技能1
        public int isRemove;//死亡是否清除尸体
        public int exp;//经验值
        public List<int> hitShader;//受击shader
    }
}
