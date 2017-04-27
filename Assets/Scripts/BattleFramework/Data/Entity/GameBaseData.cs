using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class GameBaseData {
        public static string csvFilePath = "Configs/GameBaseData";
        public static string[] columnNameArray = new string[12];
        public static List<GameBaseData> dataList;
        public static Dictionary<int, GameBaseData> dataMap;
        public static List<GameBaseData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<GameBaseData>();
            dataMap = new Dictionary<int, GameBaseData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[12];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                GameBaseData data = new GameBaseData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                float.TryParse(csvFile.mapData[i].data[1],out data.forwardSpeed);
                columnNameArray [1] = "forwardSpeed";
                float.TryParse(csvFile.mapData[i].data[2],out data.backwardsSpeed);
                columnNameArray [2] = "backwardsSpeed";
                float.TryParse(csvFile.mapData[i].data[3],out data.sidewaysSpeed);
                columnNameArray [3] = "sidewaysSpeed";
                int.TryParse(csvFile.mapData[i].data[4],out data.hp);
                columnNameArray [4] = "hp";
                float.TryParse(csvFile.mapData[i].data[5],out data.zhunxingMinValue);
                columnNameArray [5] = "zhunxingMinValue";
                float.TryParse(csvFile.mapData[i].data[6],out data.zhunxingMaxValue);
                columnNameArray [6] = "zhunxingMaxValue";
                float.TryParse(csvFile.mapData[i].data[7],out data.zhunxingStepValue);
                columnNameArray [7] = "zhunxingStepValue";
                data.heartSound = csvFile.mapData[i].data[8];
                columnNameArray [8] = "heartSound";
                float.TryParse(csvFile.mapData[i].data[9],out data.luopanMaxDis);
                columnNameArray [9] = "luopanMaxDis";
                float.TryParse(csvFile.mapData[i].data[10],out data.luopanMidDis);
                columnNameArray [10] = "luopanMidDis";
                float.TryParse(csvFile.mapData[i].data[11],out data.luopanMinDis);
                columnNameArray [11] = "luopanMinDis";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static GameBaseData GetByID (int id,List<GameBaseData> data)
        {
            foreach (GameBaseData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static GameBaseData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public float forwardSpeed;//人物前移速度
        public float backwardsSpeed;//后移速度
        public float sidewaysSpeed;//两边移动速度
        public int hp;//人物初始化血量
        public float zhunxingMinValue;//白准星的最小值
        public float zhunxingMaxValue;//白准星的最大值
        public float zhunxingStepValue;//白准星的移动步长
        public string heartSound;//心跳声音
        public float luopanMaxDis;//罗盘探测红色距离
        public float luopanMidDis;//罗盘探测黄色距离
        public float luopanMinDis;//罗盘探测绿色距离
    }
}
