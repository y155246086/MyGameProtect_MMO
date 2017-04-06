using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class GameSceneData {
        public static string csvFilePath = "Configs/GameSceneData";
        public static string[] columnNameArray = new string[7];
        public static List<GameSceneData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<GameSceneData> dataList = new List<GameSceneData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[7];
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
                dataList.Add(data);
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
  
        public int id;//数据ID
        public string sceneName;//场景名称
        public string levelName;//用于加载的场景名称
        public string ResRefreshPointsPath;//刷新点预设路径
        public string bgsound;//背景音乐
        public string randomSound;//随机播放音效
        public string stateName;//状态名称
    }
}
