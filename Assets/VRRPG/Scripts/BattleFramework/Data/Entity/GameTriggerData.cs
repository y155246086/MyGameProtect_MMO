using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class GameTriggerData {
        public static string csvFilePath = "Configs/GameTriggerData";
        public static string[] columnNameArray = new string[4];
        public static List<GameTriggerData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<GameTriggerData> dataList = new List<GameTriggerData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[4];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                GameTriggerData data = new GameTriggerData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                int.TryParse(csvFile.mapData[i].data[1],out data.goodsID);
                columnNameArray [1] = "goodsID";
                data.content = csvFile.mapData[i].data[2];
                columnNameArray [2] = "content";
                data.sound = csvFile.mapData[i].data[3];
                columnNameArray [3] = "sound";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static GameTriggerData GetByID (int id,List<GameTriggerData> data)
        {
            foreach (GameTriggerData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public int goodsID;//触发类型 0一次 1重复
        public string content;//内容
        public string sound;//音效资源
    }
}
