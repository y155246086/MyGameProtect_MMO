using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class WaveData {
        public static string csvFilePath = "Configs/WaveData";
        public static string[] columnNameArray = new string[4];
        public static List<WaveData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<WaveData> dataList = new List<WaveData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[4];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                WaveData data = new WaveData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.monsterIDS = csvFile.mapData[i].data[1];
                columnNameArray [1] = "monsterIDS";
                data.monsterNums = csvFile.mapData[i].data[2];
                columnNameArray [2] = "monsterNums";
                int.TryParse(csvFile.mapData[i].data[3],out data.teshu);
                columnNameArray [3] = "teshu";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static WaveData GetByID (int id,List<WaveData> data)
        {
            foreach (WaveData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public string monsterIDS;//怪物ID
        public string monsterNums;//怪物数量
        public int teshu;//出生在特殊位置
    }
}
