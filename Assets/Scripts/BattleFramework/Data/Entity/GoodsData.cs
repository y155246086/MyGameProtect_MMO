using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class GoodsData {
        public static string csvFilePath = "Configs/GoodsData";
        public static string[] columnNameArray = new string[12];
        public static List<GoodsData> dataList;
        public static Dictionary<int, GoodsData> dataMap;
        public static List<GoodsData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<GoodsData>();
            dataMap = new Dictionary<int, GoodsData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[12];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                GoodsData data = new GoodsData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.goodsName = csvFile.mapData[i].data[1];
                columnNameArray [1] = "goodsName";
                data.resourceName = csvFile.mapData[i].data[2];
                columnNameArray [2] = "resourceName";
                int.TryParse(csvFile.mapData[i].data[3],out data.goodsType);
                columnNameArray [3] = "goodsType";
                int.TryParse(csvFile.mapData[i].data[4],out data.effectType);
                columnNameArray [4] = "effectType";
                int.TryParse(csvFile.mapData[i].data[5],out data.effectValue);
                columnNameArray [5] = "effectValue";
                data.info = csvFile.mapData[i].data[6];
                columnNameArray [6] = "info";
                float.TryParse(csvFile.mapData[i].data[7],out data.dis);
                columnNameArray [7] = "dis";
                data.sound = csvFile.mapData[i].data[8];
                columnNameArray [8] = "sound";
                data.position= new Vector3();
                strs = csvFile.mapData[i].data[9].Split(new char[1]{','});
                    data.position.x = (float.Parse(strs[0]));
                    data.position.y = (float.Parse(strs[1]));
                    data.position.z = (float.Parse(strs[2]));
                columnNameArray [9] = "position";
                data.scale= new Vector3();
                strs = csvFile.mapData[i].data[10].Split(new char[1]{','});
                    data.scale.x = (float.Parse(strs[0]));
                    data.scale.y = (float.Parse(strs[1]));
                    data.scale.z = (float.Parse(strs[2]));
                columnNameArray [10] = "scale";
                data.rotion= new Vector3();
                strs = csvFile.mapData[i].data[11].Split(new char[1]{','});
                    data.rotion.x = (float.Parse(strs[0]));
                    data.rotion.y = (float.Parse(strs[1]));
                    data.rotion.z = (float.Parse(strs[2]));
                columnNameArray [11] = "rotion";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static GoodsData GetByID (int id,List<GoodsData> data)
        {
            foreach (GoodsData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static GoodsData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//物品ID
        public string goodsName;//物品名称
        public string resourceName;//资源名称
        public int goodsType;//物品类别
        public int effectType;//道具效果
        public int effectValue;//效果值
        public string info;//物品介绍
        public float dis;//有效距离
        public string sound;//使用音效
        public Vector3 position;//UI的展示坐标
        public Vector3 scale;//UI的展示坐标
        public Vector3 rotion;//UI的展示坐标
    }
}
