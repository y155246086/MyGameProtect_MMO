using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class TriggerData {
        public static string csvFilePath = "Configs/TriggerData";
        public static string[] columnNameArray = new string[4];
        public static List<TriggerData> dataList;
        public static List<TriggerData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<TriggerData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[4];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                TriggerData data = new TriggerData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                int.TryParse(csvFile.mapData[i].data[1],out data.type);
                columnNameArray [1] = "type";
                data.name = csvFile.mapData[i].data[2];
                columnNameArray [2] = "name";
                float.TryParse(csvFile.mapData[i].data[3],out data.value);
                columnNameArray [3] = "value";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static TriggerData GetByID (int id,List<TriggerData> data)
        {
            foreach (TriggerData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static TriggerData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public int type;//触发类型
        public string name;//名称
        public float value;//数值
    }
}
