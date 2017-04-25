using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class PlayerData {
        public static string csvFilePath = "Configs/PlayerData";
        public static string[] columnNameArray = new string[4];
        public static List<PlayerData> dataList;
        public static List<PlayerData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<PlayerData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[4];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                PlayerData data = new PlayerData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                data.department = csvFile.mapData[i].data[2];
                columnNameArray [2] = "department";
                data.image = csvFile.mapData[i].data[3];
                columnNameArray [3] = "image";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static PlayerData GetByID (int id,List<PlayerData> data)
        {
            foreach (PlayerData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static PlayerData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string name;//姓名
        public string department;//部门
        public string image;//照片资源路径
    }
}
