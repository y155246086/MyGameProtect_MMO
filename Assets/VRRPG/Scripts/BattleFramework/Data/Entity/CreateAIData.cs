using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class CreateAIData {
        public static string csvFilePath = "Configs/CreateAIData";
        public static string[] columnNameArray = new string[3];
        public static List<CreateAIData> dataList;
        public static List<CreateAIData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<CreateAIData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[3];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                CreateAIData data = new CreateAIData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.monsterIDS = csvFile.mapData[i].data[1];
                columnNameArray [1] = "monsterIDS";
                data.monsterNums = csvFile.mapData[i].data[2];
                columnNameArray [2] = "monsterNums";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static CreateAIData GetByID (int id,List<CreateAIData> data)
        {
            foreach (CreateAIData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static CreateAIData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string monsterIDS;//AI ID
        public string monsterNums;//AI 数量
    }
}
