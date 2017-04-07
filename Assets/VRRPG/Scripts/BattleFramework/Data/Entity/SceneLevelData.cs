using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SceneLevelData {
        public static string csvFilePath = "Configs/SceneLevelData";
        public static string[] columnNameArray = new string[7];
        public static List<SceneLevelData> dataList;
        public static List<SceneLevelData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<SceneLevelData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[7];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                SceneLevelData data = new SceneLevelData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                float.TryParse(csvFile.mapData[i].data[1],out data.timeInterval);
                columnNameArray [1] = "timeInterval";
                int.TryParse(csvFile.mapData[i].data[2],out data.fromWave);
                columnNameArray [2] = "fromWave";
                int.TryParse(csvFile.mapData[i].data[3],out data.toWave);
                columnNameArray [3] = "toWave";
                int.TryParse(csvFile.mapData[i].data[4],out data.isMove);
                columnNameArray [4] = "isMove";
                int.TryParse(csvFile.mapData[i].data[5],out data.StartTriggerStoryId);
                columnNameArray [5] = "StartTriggerStoryId";
                int.TryParse(csvFile.mapData[i].data[6],out data.EndTriggerStoryId);
                columnNameArray [6] = "EndTriggerStoryId";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static SceneLevelData GetByID (int id,List<SceneLevelData> data)
        {
            foreach (SceneLevelData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static SceneLevelData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public float timeInterval;//刷怪间隔
        public int fromWave;//第几批怪
        public int toWave;//到第几批怪
        public int isMove;//角色是否可以移动
        public int StartTriggerStoryId;//刷怪开始触发剧情ID
        public int EndTriggerStoryId;//战斗结束触发剧情ID
    }
}
