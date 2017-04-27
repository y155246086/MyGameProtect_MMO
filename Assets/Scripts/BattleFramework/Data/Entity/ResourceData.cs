using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class ResourceData {
        public static string csvFilePath = "Configs/ResourceData";
        public static string[] columnNameArray = new string[5];
        public static List<ResourceData> dataList;
        public static Dictionary<int, ResourceData> dataMap;
        public static List<ResourceData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<ResourceData>();
            dataMap = new Dictionary<int, ResourceData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[5];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                ResourceData data = new ResourceData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.resourceName = csvFile.mapData[i].data[1];
                columnNameArray [1] = "resourceName";
                data.resourcePath = csvFile.mapData[i].data[2];
                columnNameArray [2] = "resourcePath";
                data.abName = csvFile.mapData[i].data[3];
                columnNameArray [3] = "abName";
                data.assetName = csvFile.mapData[i].data[4];
                columnNameArray [4] = "assetName";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static ResourceData GetByID (int id,List<ResourceData> data)
        {
            foreach (ResourceData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static ResourceData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string resourceName;//资源名称
        public string resourcePath;//资源路径
        public string abName;//资源包名称
        public string assetName;//资源名-用于加载
    }
}
