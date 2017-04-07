using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SceneData {
        public static string csvFilePath = "Configs/SceneData";
        public static string[] columnNameArray = new string[10];
        public static List<SceneData> dataList;
        public static List<SceneData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<SceneData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[10];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                SceneData data = new SceneData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                int.TryParse(csvFile.mapData[i].data[1],out data.sceneType1);
                columnNameArray [1] = "sceneType1";
                data.sizeType1 = csvFile.mapData[i].data[2];
                columnNameArray [2] = "sizeType1";
                data.sizeTypeNum1 = csvFile.mapData[i].data[3];
                columnNameArray [3] = "sizeTypeNum1";
                int.TryParse(csvFile.mapData[i].data[4],out data.sceneType2);
                columnNameArray [4] = "sceneType2";
                data.sizeType2 = csvFile.mapData[i].data[5];
                columnNameArray [5] = "sizeType2";
                data.sizeTypeNum2 = csvFile.mapData[i].data[6];
                columnNameArray [6] = "sizeTypeNum2";
                int.TryParse(csvFile.mapData[i].data[7],out data.sceneType3);
                columnNameArray [7] = "sceneType3";
                data.sizeType3 = csvFile.mapData[i].data[8];
                columnNameArray [8] = "sizeType3";
                data.sizeTypeNum3 = csvFile.mapData[i].data[9];
                columnNameArray [9] = "sizeTypeNum3";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static SceneData GetByID (int id,List<SceneData> data)
        {
            foreach (SceneData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static SceneData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public int sceneType1;//地形1
        public string sizeType1;//尺寸类型1
        public string sizeTypeNum1;//尺寸类型1数量
        public int sceneType2;//地形1
        public string sizeType2;//尺寸类型1
        public string sizeTypeNum2;//尺寸类型1数量
        public int sceneType3;//地形1
        public string sizeType3;//尺寸类型1
        public string sizeTypeNum3;//尺寸类型1数量
    }
}
