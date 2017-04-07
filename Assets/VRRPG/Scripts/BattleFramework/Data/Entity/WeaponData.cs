using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class WeaponData {
        public static string csvFilePath = "Configs/WeaponData";
        public static string[] columnNameArray = new string[5];
        public static List<WeaponData> dataList;
        public static List<WeaponData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<WeaponData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[5];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                WeaponData data = new WeaponData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                float.TryParse(csvFile.mapData[i].data[2],out data.attackDis);
                columnNameArray [2] = "attackDis";
                int.TryParse(csvFile.mapData[i].data[3],out data.attackValue);
                columnNameArray [3] = "attackValue";
                data.attackSound = csvFile.mapData[i].data[4];
                columnNameArray [4] = "attackSound";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static WeaponData GetByID (int id,List<WeaponData> data)
        {
            foreach (WeaponData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static WeaponData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string name;//名字
        public float attackDis;//攻击距离
        public int attackValue;//攻击力
        public string attackSound;//攻击声音
    }
}
