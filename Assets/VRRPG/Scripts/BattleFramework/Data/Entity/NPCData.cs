using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class NPCData {
        public static string csvFilePath = "Configs/NPCData";
        public static string[] columnNameArray = new string[6];
        public static List<NPCData> dataList;
        public static List<NPCData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<NPCData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[6];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                NPCData data = new NPCData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.PrefabsPath = csvFile.mapData[i].data[1];
                columnNameArray [1] = "PrefabsPath";
                data.name = csvFile.mapData[i].data[2];
                columnNameArray [2] = "name";
                int.TryParse(csvFile.mapData[i].data[3],out data.currentHP);
                columnNameArray [3] = "currentHP";
                float.TryParse(csvFile.mapData[i].data[4],out data.moveSpeed);
                columnNameArray [4] = "moveSpeed";
                int.TryParse(csvFile.mapData[i].data[5],out data.aiLogicId);
                columnNameArray [5] = "aiLogicId";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static NPCData GetByID (int id,List<NPCData> data)
        {
            foreach (NPCData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static NPCData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string PrefabsPath;//资源路径
        public string name;//名字
        public int currentHP;//当前血量
        public float moveSpeed;//移动速度
        public int aiLogicId;//AI逻辑
    }
}
