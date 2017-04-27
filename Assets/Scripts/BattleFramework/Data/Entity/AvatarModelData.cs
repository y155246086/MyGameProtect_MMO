using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class AvatarModelData {
        public static string csvFilePath = "Configs/AvatarModelData";
        public static string[] columnNameArray = new string[11];
        public static List<AvatarModelData> dataList;
        public static Dictionary<int, AvatarModelData> dataMap;
        public static List<AvatarModelData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<AvatarModelData>();
            dataMap = new Dictionary<int, AvatarModelData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[11];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                AvatarModelData data = new AvatarModelData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.bornFx= new List<int>();
                strs = csvFile.mapData[i].data[1].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.bornFx.Add(int.Parse(strs[j]));
                }
                columnNameArray [1] = "bornFx";
                int.TryParse(csvFile.mapData[i].data[2],out data.bornTime);
                columnNameArray [2] = "bornTime";
                int.TryParse(csvFile.mapData[i].data[3],out data.deadTime);
                columnNameArray [3] = "deadTime";
                data.dieFx= new List<int>();
                strs = csvFile.mapData[i].data[4].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.dieFx.Add(int.Parse(strs[j]));
                }
                columnNameArray [4] = "dieFx";
                data.nakedEquipList= new List<int>();
                strs = csvFile.mapData[i].data[5].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.nakedEquipList.Add(int.Parse(strs[j]));
                }
                columnNameArray [5] = "nakedEquipList";
                data.prefabName = csvFile.mapData[i].data[6];
                columnNameArray [6] = "prefabName";
                float.TryParse(csvFile.mapData[i].data[7],out data.scale);
                columnNameArray [7] = "scale";
                float.TryParse(csvFile.mapData[i].data[8],out data.scaleRadius);
                columnNameArray [8] = "scaleRadius";
                float.TryParse(csvFile.mapData[i].data[9],out data.speed);
                columnNameArray [9] = "speed";
                data.vocation = csvFile.mapData[i].data[10];
                columnNameArray [10] = "vocation";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static AvatarModelData GetByID (int id,List<AvatarModelData> data)
        {
            foreach (AvatarModelData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static AvatarModelData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public List<int> bornFx;//出生特效
        public int bornTime;//出生时间
        public int deadTime;//死亡时间
        public List<int> dieFx;//死亡特效
        public List<int> nakedEquipList;//初始装备id
        public string prefabName;//预设名称
        public float scale;//缩放系数
        public float scaleRadius;//半径
        public float speed;//速度
        public string vocation;//职业
    }
}
