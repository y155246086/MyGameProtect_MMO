using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class TriggerData {
        public static string csvFilePath = "Configs/TriggerData";
        public static string[] columnNameArray = new string[11];
        public static List<TriggerData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<TriggerData> dataList = new List<TriggerData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[11];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                TriggerData data = new TriggerData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                int.TryParse(csvFile.mapData[i].data[1],out data.goodsID);
                columnNameArray [1] = "goodsID";
                data.content = csvFile.mapData[i].data[2];
                columnNameArray [2] = "content";
                int.TryParse(csvFile.mapData[i].data[3],out data.resultGoodsId);
                columnNameArray [3] = "resultGoodsId";
                data.cameraName = csvFile.mapData[i].data[4];
                columnNameArray [4] = "cameraName";
                data.sound = csvFile.mapData[i].data[5];
                columnNameArray [5] = "sound";
                data.monsterPoint = csvFile.mapData[i].data[6];
                columnNameArray [6] = "monsterPoint";
                data.monsterId = csvFile.mapData[i].data[7];
                columnNameArray [7] = "monsterId";
                data.ObsNames = csvFile.mapData[i].data[8];
                columnNameArray [8] = "ObsNames";
                data.gameTriggerPosint = csvFile.mapData[i].data[9];
                columnNameArray [9] = "gameTriggerPosint";
                data.triggerId = csvFile.mapData[i].data[10];
                columnNameArray [10] = "triggerId";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static TriggerData GetByID (int id,List<TriggerData> data)
        {
            foreach (TriggerData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public int goodsID;//触发条件物品id
        public string content;//内容
        public int resultGoodsId;//触发后获得物品ID
        public string cameraName;//触发镜头ID
        public string sound;//触发音效资源
        public string monsterPoint;//刷新怪物的点
        public string monsterId;//刷新怪物的ID
        public string ObsNames;//消失障碍点
        public string gameTriggerPosint;//消失对话触发点
        public string triggerId;//刷新可交互点
    }
}
