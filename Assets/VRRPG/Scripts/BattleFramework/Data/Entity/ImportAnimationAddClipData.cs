using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class ImportAnimationAddClipData {
        public static string csvFilePath = "Configs/ImportAnimationAddClipData";
        public static string[] columnNameArray = new string[7];
        public static List<ImportAnimationAddClipData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<ImportAnimationAddClipData> dataList = new List<ImportAnimationAddClipData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[7];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                ImportAnimationAddClipData data = new ImportAnimationAddClipData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                data.clipName = csvFile.mapData[i].data[2];
                columnNameArray [2] = "clipName";
                int.TryParse(csvFile.mapData[i].data[3],out data.firstFrame);
                columnNameArray [3] = "firstFrame";
                int.TryParse(csvFile.mapData[i].data[4],out data. lastFrame);
                columnNameArray [4] = " lastFrame";
                int.TryParse(csvFile.mapData[i].data[5],out data.isLoop);
                columnNameArray [5] = "isLoop";
                data.eventDataId = csvFile.mapData[i].data[6];
                columnNameArray [6] = "eventDataId";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static ImportAnimationAddClipData GetByID (int id,List<ImportAnimationAddClipData> data)
        {
            foreach (ImportAnimationAddClipData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public string name;//文件名称
        public string clipName;//动作名称
        public int firstFrame;//起始帧
        public int  lastFrame;//结束帧
        public int isLoop;//是否循环播放
        public string eventDataId;//事件数据ID
    }
}
