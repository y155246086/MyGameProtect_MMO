using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class EquipData {
        public static string csvFilePath = "Configs/EquipData";
        public static string[] columnNameArray = new string[13];
        public static List<EquipData> dataList;
        public static Dictionary<int, EquipData> dataMap;
        public static List<EquipData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<EquipData>();
            dataMap = new Dictionary<int, EquipData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[13];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                EquipData data = new EquipData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.type= new List<int>();
                strs = csvFile.mapData[i].data[1].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.type.Add(int.Parse(strs[j]));
                }
                columnNameArray [1] = "type";
                data.prefabPath= new List<string>();
                strs = csvFile.mapData[i].data[2].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.prefabPath.Add(strs[j]);
                }
                columnNameArray [2] = "prefabPath";
                data.mesh = csvFile.mapData[i].data[3];
                columnNameArray [3] = "mesh";
                data.material = csvFile.mapData[i].data[4];
                columnNameArray [4] = "material";
                data.slot= new List<string>();
                strs = csvFile.mapData[i].data[5].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.slot.Add(strs[j]);
                }
                columnNameArray [5] = "slot";
                data.slotInCity= new List<string>();
                strs = csvFile.mapData[i].data[6].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.slotInCity.Add(strs[j]);
                }
                columnNameArray [6] = "slotInCity";
                int.TryParse(csvFile.mapData[i].data[7],out data.priority);
                columnNameArray [7] = "priority";
                int.TryParse(csvFile.mapData[i].data[8],out data.putOnMethod);
                columnNameArray [8] = "putOnMethod";
                int.TryParse(csvFile.mapData[i].data[9],out data.suit);
                columnNameArray [9] = "suit";
                int.TryParse(csvFile.mapData[i].data[10],out data.suitCount);
                columnNameArray [10] = "suitCount";
                int.TryParse(csvFile.mapData[i].data[11],out data.isWeapon);
                columnNameArray [11] = "isWeapon";
                data.subEquip= new List<int>();
                strs = csvFile.mapData[i].data[12].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.subEquip.Add(int.Parse(strs[j]));
                }
                columnNameArray [12] = "subEquip";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static EquipData GetByID (int id,List<EquipData> data)
        {
            foreach (EquipData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static EquipData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public List<int> type;//类型
        public List<string> prefabPath;//预设资源路径
        public string mesh;//模型
        public string material;//材质
        public List<string> slot;//插口
        public List<string> slotInCity;//在主城是的插口
        public int priority;//优先级
        public int putOnMethod;//0
        public int suit;//适合
        public int suitCount;//适合个数
        public int isWeapon;//是不是武器
        public List<int> subEquip;//子装备
    }
}
