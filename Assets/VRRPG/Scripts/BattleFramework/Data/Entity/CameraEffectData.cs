using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class CameraEffectData {
        public static string csvFilePath = "Configs/CameraEffectData";
        public static string[] columnNameArray = new string[4];
        public static List<CameraEffectData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<CameraEffectData> dataList = new List<CameraEffectData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[4];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                CameraEffectData data = new CameraEffectData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                int.TryParse(csvFile.mapData[i].data[2],out data.type);
                columnNameArray [2] = "type";
                float.TryParse(csvFile.mapData[i].data[3],out data.time);
                columnNameArray [3] = "time";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static CameraEffectData GetByID (int id,List<CameraEffectData> data)
        {
            foreach (CameraEffectData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public string name;//特效名称
        public int type;//特效类型名称
        public float time;//持续时间
    }
}
