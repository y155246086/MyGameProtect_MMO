using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class GameBaseData {
        public static string csvFilePath = "Configs/GameBaseData";
        public static string[] columnNameArray = new string[3];
        public static List<GameBaseData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<GameBaseData> dataList = new List<GameBaseData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[3];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                GameBaseData data = new GameBaseData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                float.TryParse(csvFile.mapData[i].data[1],out data.gapTime);
                columnNameArray [1] = "gapTime";
                float.TryParse(csvFile.mapData[i].data[2],out data.startButtonTime);
                columnNameArray [2] = "startButtonTime";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static GameBaseData GetByID (int id,List<GameBaseData> data)
        {
            foreach (GameBaseData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public float gapTime;//闪动间隔
        public float startButtonTime;//开始按钮响应时间
    }
}
