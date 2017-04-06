using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class EffectData {
        public static string csvFilePath = "Configs/EffectData";
        public static string[] columnNameArray = new string[11];
        public static List<EffectData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<EffectData> dataList = new List<EffectData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[11];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                EffectData data = new EffectData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                int.TryParse(csvFile.mapData[i].data[1],out data.skillId);
                columnNameArray [1] = "skillId";
                float.TryParse(csvFile.mapData[i].data[2],out data.duration);
                columnNameArray [2] = "duration";
                int.TryParse(csvFile.mapData[i].data[3],out data.isConflict);
                columnNameArray [3] = "isConflict";
                int.TryParse(csvFile.mapData[i].data[4],out data.isStatic);
                columnNameArray [4] = "isStatic";
                int.TryParse(csvFile.mapData[i].data[5],out data.locationType);
                columnNameArray [5] = "locationType";
                data.location= new Vector3();
                strs = csvFile.mapData[i].data[6].Split(new char[1]{','});
                    data.location.x = (float.Parse(strs[0]));
                    data.location.y = (float.Parse(strs[1]));
                    data.location.z = (float.Parse(strs[2]));
                columnNameArray [6] = "location";
                data.player = csvFile.mapData[i].data[7];
                columnNameArray [7] = "player";
                data.resourcePath = csvFile.mapData[i].data[8];
                columnNameArray [8] = "resourcePath";
                float.TryParse(csvFile.mapData[i].data[9],out data.delay);
                columnNameArray [9] = "delay";
                int.TryParse(csvFile.mapData[i].data[10],out data.effectType);
                columnNameArray [10] = "effectType";
                dataList.Add(data);
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
  
        public int id;//数据ID
        public int skillId;//技能ID
        public float duration;//持续时间
        public int isConflict;//是否有冲突
        public int isStatic;//是否静态
        public int locationType;//坐标类型
        public Vector3 location;//坐标
        public string player;//玩家
        public string resourcePath;//资源路径
        public float delay;//延迟时间
        public int effectType;//特效类型
        public string anim;
        public string slot;
        public int soundDelay;
    }
}
