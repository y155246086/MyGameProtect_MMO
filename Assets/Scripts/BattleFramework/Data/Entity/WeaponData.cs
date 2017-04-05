using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class WeaponData {
        public static string csvFilePath = "Configs/WeaponData";
        public static string[] columnNameArray = new string[14];
        public static List<WeaponData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<WeaponData> dataList = new List<WeaponData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[14];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                WeaponData data = new WeaponData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.PrefabsPath = csvFile.mapData[i].data[1];
                columnNameArray [1] = "PrefabsPath";
                data.name = csvFile.mapData[i].data[2];
                columnNameArray [2] = "name";
                float.TryParse(csvFile.mapData[i].data[3],out data.inductionDis);
                columnNameArray [3] = "inductionDis";
                float.TryParse(csvFile.mapData[i].data[4],out data.patrolRadius);
                columnNameArray [4] = "patrolRadius";
                float.TryParse(csvFile.mapData[i].data[5],out data.PatrolCD);
                columnNameArray [5] = "PatrolCD";
                float.TryParse(csvFile.mapData[i].data[6],out data.floowDis);
                columnNameArray [6] = "floowDis";
                float.TryParse(csvFile.mapData[i].data[7],out data.attackDis);
                columnNameArray [7] = "attackDis";
                float.TryParse(csvFile.mapData[i].data[8],out data.attackCD);
                columnNameArray [8] = "attackCD";
                int.TryParse(csvFile.mapData[i].data[9],out data.attackValue);
                columnNameArray [9] = "attackValue";
                data.attackSound = csvFile.mapData[i].data[10];
                columnNameArray [10] = "attackSound";
                data.deadSound = csvFile.mapData[i].data[11];
                columnNameArray [11] = "deadSound";
                data.attackedSound = csvFile.mapData[i].data[12];
                columnNameArray [12] = "attackedSound";
                data.footSound = csvFile.mapData[i].data[13];
                columnNameArray [13] = "footSound";
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
  
        public int id;//数据ID
        public string PrefabsPath;//资源路径
        public string name;//名字
        public float inductionDis;//警惕范围
        public float patrolRadius;//巡逻半径
        public float PatrolCD;//巡逻CD
        public float floowDis;//追击范围
        public float attackDis;//攻击距离
        public float attackCD;//攻击cd
        public int attackValue;//攻击力
        public string attackSound;//攻击声音
        public string deadSound;//死亡声音
        public string attackedSound;//被打声音
        public string footSound;//跑步声音
    }
}
