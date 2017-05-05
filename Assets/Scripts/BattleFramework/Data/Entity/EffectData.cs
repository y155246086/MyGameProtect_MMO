using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class EffectData {
        public static string csvFilePath = "Configs/EffectData";
        public static string[] columnNameArray = new string[14];
        public static List<EffectData> dataList;
        public static Dictionary<int, EffectData> dataMap;
        public static List<EffectData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<EffectData>();
            dataMap = new Dictionary<int, EffectData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[14];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                EffectData data = new EffectData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                float.TryParse(csvFile.mapData[i].data[1],out data.duration);
                columnNameArray [1] = "duration";
                int.TryParse(csvFile.mapData[i].data[2],out data.isConflict);
                columnNameArray [2] = "isConflict";
                int.TryParse(csvFile.mapData[i].data[3],out data.isStatic);
                columnNameArray [3] = "isStatic";
                int.TryParse(csvFile.mapData[i].data[4],out data.group);
                columnNameArray [4] = "group";
                int.TryParse(csvFile.mapData[i].data[5],out data.locationType);
                columnNameArray [5] = "locationType";
                data.slot = csvFile.mapData[i].data[6];
                columnNameArray [6] = "slot";
                data.location= new Vector3();
                strs = csvFile.mapData[i].data[7].Split(new char[1]{','});
                    data.location.x = (float.Parse(strs[0]));
                    data.location.y = (float.Parse(strs[1]));
                    data.location.z = (float.Parse(strs[2]));
                columnNameArray [7] = "location";
                data.player = csvFile.mapData[i].data[8];
                columnNameArray [8] = "player";
                data.resourceName = csvFile.mapData[i].data[9];
                columnNameArray [9] = "resourceName";
                float.TryParse(csvFile.mapData[i].data[10],out data.delay);
                columnNameArray [10] = "delay";
                int.TryParse(csvFile.mapData[i].data[11],out data.effectType);
                columnNameArray [11] = "effectType";
                data.anim = csvFile.mapData[i].data[12];
                columnNameArray [12] = "anim";
                float.TryParse(csvFile.mapData[i].data[13],out data.soundDelay);
                columnNameArray [13] = "soundDelay";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static EffectData GetByID (int id,List<EffectData> data)
        {
            foreach (EffectData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static EffectData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public float duration;//持续时间
        public int isConflict;//是否有冲突id
        public int isStatic;//是否静态
        public int group;//组
        public int locationType;//坐标类型
        public string slot;//位置名称
        public Vector3 location;//坐标
        public string player;//玩家
        public string resourceName;//资源路径
        public float delay;//延迟时间
        public int effectType;//特效类型
        public string anim;//动画名称
        public float soundDelay;//声音延迟
    }
}
