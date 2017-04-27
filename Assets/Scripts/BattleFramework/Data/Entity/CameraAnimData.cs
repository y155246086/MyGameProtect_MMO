using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class CameraAnimData {
        public static string csvFilePath = "Configs/CameraAnimData";
        public static string[] columnNameArray = new string[8];
        public static List<CameraAnimData> dataList;
        public static Dictionary<int, CameraAnimData> dataMap;
        public static List<CameraAnimData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<CameraAnimData>();
            dataMap = new Dictionary<int, CameraAnimData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[8];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                CameraAnimData data = new CameraAnimData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                int.TryParse(csvFile.mapData[i].data[2],out data.xRate);
                columnNameArray [2] = "xRate";
                float.TryParse(csvFile.mapData[i].data[3],out data.xSwing);
                columnNameArray [3] = "xSwing";
                int.TryParse(csvFile.mapData[i].data[4],out data.yRate);
                columnNameArray [4] = "yRate";
                float.TryParse(csvFile.mapData[i].data[5],out data.ySwing);
                columnNameArray [5] = "ySwing";
                int.TryParse(csvFile.mapData[i].data[6],out data.zRate);
                columnNameArray [6] = "zRate";
                float.TryParse(csvFile.mapData[i].data[7],out data.zSwing);
                columnNameArray [7] = "zSwing";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static CameraAnimData GetByID (int id,List<CameraAnimData> data)
        {
            foreach (CameraAnimData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static CameraAnimData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string name;//特效名称
        public int xRate;//x旋转
        public float xSwing;//x摆动
        public int yRate;//y旋转
        public float ySwing;//y摆动
        public int zRate;//z旋转
        public float zSwing;//z摆动
    }
}
