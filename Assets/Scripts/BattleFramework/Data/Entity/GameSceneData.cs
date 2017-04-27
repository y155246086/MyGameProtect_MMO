using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class GameSceneData {
        public static string csvFilePath = "Configs/GameSceneData";
        public static string[] columnNameArray = new string[8];
        public static List<GameSceneData> dataList;
        public static Dictionary<int, GameSceneData> dataMap;
        public static List<GameSceneData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<GameSceneData>();
            dataMap = new Dictionary<int, GameSceneData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[8];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                GameSceneData data = new GameSceneData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.sceneName = csvFile.mapData[i].data[1];
                columnNameArray [1] = "sceneName";
                data.levelName = csvFile.mapData[i].data[2];
                columnNameArray [2] = "levelName";
                data.ResRefreshPointsPath = csvFile.mapData[i].data[3];
                columnNameArray [3] = "ResRefreshPointsPath";
                data.bgsound = csvFile.mapData[i].data[4];
                columnNameArray [4] = "bgsound";
                data.randomSound = csvFile.mapData[i].data[5];
                columnNameArray [5] = "randomSound";
                data.stateName = csvFile.mapData[i].data[6];
                columnNameArray [6] = "stateName";
                data.enterPoint= new Vector3();
                strs = csvFile.mapData[i].data[7].Split(new char[1]{','});
                    data.enterPoint.x = (float.Parse(strs[0]));
                    data.enterPoint.y = (float.Parse(strs[1]));
                    data.enterPoint.z = (float.Parse(strs[2]));
                columnNameArray [7] = "enterPoint";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static GameSceneData GetByID (int id,List<GameSceneData> data)
        {
            foreach (GameSceneData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static GameSceneData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string sceneName;//场景名称
        public string levelName;//用于加载的场景名称
        public string ResRefreshPointsPath;//刷新点预设路径
        public string bgsound;//背景音乐
        public string randomSound;//随机播放音效
        public string stateName;//状态名称
        public Vector3 enterPoint;//初始坐标
    }
}
