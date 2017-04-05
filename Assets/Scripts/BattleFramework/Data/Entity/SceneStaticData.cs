using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SceneStaticData {
        public static string csvFilePath = "Configs/SceneStaticData";
        public static string[] columnNameArray = new string[2];
        public static List<SceneStaticData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<SceneStaticData> dataList = new List<SceneStaticData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[2];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                SceneStaticData data = new SceneStaticData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.clipGroup = csvFile.mapData[i].data[1];
                columnNameArray [1] = "clipGroup";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static SceneStaticData GetByID (int id,List<SceneStaticData> data)
        {
            foreach (SceneStaticData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public string clipGroup;//场景单元数据ID组
    }
}
