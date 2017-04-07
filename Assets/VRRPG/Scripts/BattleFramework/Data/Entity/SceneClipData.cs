using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SceneClipData {
        public static string csvFilePath = "Configs/SceneClipData";
        public static string[] columnNameArray = new string[13];
        public static List<SceneClipData> dataList;
        public static List<SceneClipData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<SceneClipData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[13];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                SceneClipData data = new SceneClipData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.resourcePath = csvFile.mapData[i].data[1];
                columnNameArray [1] = "resourcePath";
                int.TryParse(csvFile.mapData[i].data[2],out data.sceneType);
                columnNameArray [2] = "sceneType";
                int.TryParse(csvFile.mapData[i].data[3],out data.sizeType);
                columnNameArray [3] = "sizeType";
                data.ExitPath1 = csvFile.mapData[i].data[4];
                columnNameArray [4] = "ExitPath1";
                int.TryParse(csvFile.mapData[i].data[5],out data.ExitType1);
                columnNameArray [5] = "ExitType1";
                data.ExitPath2 = csvFile.mapData[i].data[6];
                columnNameArray [6] = "ExitPath2";
                int.TryParse(csvFile.mapData[i].data[7],out data.ExitType2);
                columnNameArray [7] = "ExitType2";
                data.ExitPath3 = csvFile.mapData[i].data[8];
                columnNameArray [8] = "ExitPath3";
                int.TryParse(csvFile.mapData[i].data[9],out data.ExitType3);
                columnNameArray [9] = "ExitType3";
                data.ExitPath4 = csvFile.mapData[i].data[10];
                columnNameArray [10] = "ExitPath4";
                int.TryParse(csvFile.mapData[i].data[11],out data.ExitType4);
                columnNameArray [11] = "ExitType4";
                data.sceneContentPath = csvFile.mapData[i].data[12];
                columnNameArray [12] = "sceneContentPath";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static SceneClipData GetByID (int id,List<SceneClipData> data)
        {
            foreach (SceneClipData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static SceneClipData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string resourcePath;//资源路径
        public int sceneType;//场景类型
        public int sizeType;//大小类型
        public string ExitPath1;//出口1
        public int ExitType1;//出口1类型
        public string ExitPath2;//出口2
        public int ExitType2;//出口2类型
        public string ExitPath3;//出口3
        public int ExitType3;//出口3类型
        public string ExitPath4;//出口4
        public int ExitType4;//出口4类型
        public string sceneContentPath;//场景动态内容预制体
    }
}
