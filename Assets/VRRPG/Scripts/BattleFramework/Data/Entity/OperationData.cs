using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class OperationData {
        public static string csvFilePath = "Configs/OperationData";
        public static string[] columnNameArray = new string[11];
        public static List<OperationData> dataList;
        public static List<OperationData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<OperationData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[11];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                OperationData data = new OperationData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                int.TryParse(csvFile.mapData[i].data[1],out data.type);
                columnNameArray [1] = "type";
                data.name = csvFile.mapData[i].data[2];
                columnNameArray [2] = "name";
                int.TryParse(csvFile.mapData[i].data[3],out data.preConditionType);
                columnNameArray [3] = "preConditionType";
                data.preConditionValue = csvFile.mapData[i].data[4];
                columnNameArray [4] = "preConditionValue";
                data.result = csvFile.mapData[i].data[5];
                columnNameArray [5] = "result";
                int.TryParse(csvFile.mapData[i].data[6],out data.resultValue);
                columnNameArray [6] = "resultValue";
                float.TryParse(csvFile.mapData[i].data[7],out data.time);
                columnNameArray [7] = "time";
                float.TryParse(csvFile.mapData[i].data[8],out data.dis);
                columnNameArray [8] = "dis";
                int.TryParse(csvFile.mapData[i].data[9],out data.eventId);
                columnNameArray [9] = "eventId";
                int.TryParse(csvFile.mapData[i].data[10],out data.isLoop);
                columnNameArray [10] = "isLoop";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static OperationData GetByID (int id,List<OperationData> data)
        {
            foreach (OperationData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static OperationData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public int type;//交互类型
        public string name;//名称
        public int preConditionType;//交互前置条件类型
        public string preConditionValue;//交互前置条件类数据
        public string result;//交互结果
        public int resultValue;//结果数量
        public float time;//交互时间
        public float dis;//交互距离
        public int eventId;//事件数据ID
        public int isLoop;//是否可重复交互
    }
}
