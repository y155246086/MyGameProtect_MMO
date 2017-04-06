using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class ImportAnimationAddEventData {
        public static string csvFilePath = "Configs/ImportAnimationAddEventData";
        public static string[] columnNameArray = new string[8];
        public static List<ImportAnimationAddEventData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<ImportAnimationAddEventData> dataList = new List<ImportAnimationAddEventData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[8];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                ImportAnimationAddEventData data = new ImportAnimationAddEventData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                data.action = csvFile.mapData[i].data[2];
                columnNameArray [2] = "action";
                data.functionName = csvFile.mapData[i].data[3];
                columnNameArray [3] = "functionName";
                float.TryParse(csvFile.mapData[i].data[4],out data.time);
                columnNameArray [4] = "time";
                float.TryParse(csvFile.mapData[i].data[5],out data.floatParameter);
                columnNameArray [5] = "floatParameter";
                int.TryParse(csvFile.mapData[i].data[6],out data.intParameter);
                columnNameArray [6] = "intParameter";
                data.stringParameter = csvFile.mapData[i].data[7];
                columnNameArray [7] = "stringParameter";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static ImportAnimationAddEventData GetByID (int id,List<ImportAnimationAddEventData> data)
        {
            foreach (ImportAnimationAddEventData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public string name;//模型名称
        public string action;//动作名称
        public string functionName;//方法名称
        public float time;//添加的时间
        public float floatParameter;//浮点参数
        public int intParameter;//整数参数
        public string stringParameter;//字符串参数
    }
}
