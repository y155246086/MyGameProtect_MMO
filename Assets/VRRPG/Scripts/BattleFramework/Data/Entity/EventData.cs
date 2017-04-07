using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class EventData {
        public static string csvFilePath = "Configs/EventData";
        public static string[] columnNameArray = new string[5];
        public static List<EventData> dataList;
        public static List<EventData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<EventData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[5];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                EventData data = new EventData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                int.TryParse(csvFile.mapData[i].data[1],out data.type);
                columnNameArray [1] = "type";
                data.desc = csvFile.mapData[i].data[2];
                columnNameArray [2] = "desc";
                int.TryParse(csvFile.mapData[i].data[3],out data.eventValue);
                columnNameArray [3] = "eventValue";
                int.TryParse(csvFile.mapData[i].data[4],out data.nextEventId);
                columnNameArray [4] = "nextEventId";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static EventData GetByID (int id,List<EventData> data)
        {
            foreach (EventData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static EventData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public int type;//事件类型
        public string desc;//事件描述
        public int eventValue;//事件数据
        public int nextEventId;//触发事件ID
    }
}
