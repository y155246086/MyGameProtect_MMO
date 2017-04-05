using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class CameraStateData {
        public static string csvFilePath = "Configs/CameraStateData";
        public static string[] columnNameArray = new string[8];
        public static List<CameraStateData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<CameraStateData> dataList = new List<CameraStateData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[8];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                CameraStateData data = new CameraStateData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                int.TryParse(csvFile.mapData[i].data[2],out data.type);
                columnNameArray [2] = "type";
                float.TryParse(csvFile.mapData[i].data[3],out data.rotaionAngle);
                columnNameArray [3] = "rotaionAngle";
                float.TryParse(csvFile.mapData[i].data[4],out data.elevation);
                columnNameArray [4] = "elevation";
                float.TryParse(csvFile.mapData[i].data[5],out data.hight);
                columnNameArray [5] = "hight";
                float.TryParse(csvFile.mapData[i].data[6],out data.heightChange);
                columnNameArray [6] = "heightChange";
                int.TryParse(csvFile.mapData[i].data[7],out data.isStatic);
                columnNameArray [7] = "isStatic";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static CameraStateData GetByID (int id,List<CameraStateData> data)
        {
            foreach (CameraStateData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public string name;//状态名称
        public int type;//状态类型名称
        public float rotaionAngle;//旋转角度
        public float elevation;//上下仰角
        public float hight;//高度
        public float heightChange;//高度浮动
        public int isStatic;//视角是否固定
    }
}
