using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class MonsterData {
        public static string csvFilePath = "Configs/MonsterData";
        public static string[] columnNameArray = new string[30];
        public static List<MonsterData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<MonsterData> dataList = new List<MonsterData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[30];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                MonsterData data = new MonsterData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.PrefabsPath = csvFile.mapData[i].data[1];
                columnNameArray [1] = "PrefabsPath";
                int.TryParse(csvFile.mapData[i].data[2],out data.type);
                columnNameArray [2] = "type";
                data.name = csvFile.mapData[i].data[3];
                columnNameArray [3] = "name";
                int.TryParse(csvFile.mapData[i].data[4],out data.currentHP);
                columnNameArray [4] = "currentHP";
                float.TryParse(csvFile.mapData[i].data[5],out data.moveSpeed);
                columnNameArray [5] = "moveSpeed";
                int.TryParse(csvFile.mapData[i].data[6],out data.maxHP);
                columnNameArray [6] = "maxHP";
                float.TryParse(csvFile.mapData[i].data[7],out data.inductionDis);
                columnNameArray [7] = "inductionDis";
                float.TryParse(csvFile.mapData[i].data[8],out data.patrolRadius);
                columnNameArray [8] = "patrolRadius";
                float.TryParse(csvFile.mapData[i].data[9],out data.PatrolCD);
                columnNameArray [9] = "PatrolCD";
                float.TryParse(csvFile.mapData[i].data[10],out data.floowDis);
                columnNameArray [10] = "floowDis";
                float.TryParse(csvFile.mapData[i].data[11],out data.attackDis);
                columnNameArray [11] = "attackDis";
                float.TryParse(csvFile.mapData[i].data[12],out data.attackCD);
                columnNameArray [12] = "attackCD";
                int.TryParse(csvFile.mapData[i].data[13],out data.attackValue);
                columnNameArray [13] = "attackValue";
                data.bornSound = csvFile.mapData[i].data[14];
                columnNameArray [14] = "bornSound";
                data.attackSound = csvFile.mapData[i].data[15];
                columnNameArray [15] = "attackSound";
                data.deadSound = csvFile.mapData[i].data[16];
                columnNameArray [16] = "deadSound";
                data.attackedSound = csvFile.mapData[i].data[17];
                columnNameArray [17] = "attackedSound";
                data.footSound = csvFile.mapData[i].data[18];
                columnNameArray [18] = "footSound";
                int.TryParse(csvFile.mapData[i].data[19],out data.skillID1);
                columnNameArray [19] = "skillID1";
                int.TryParse(csvFile.mapData[i].data[20],out data.skillID2);
                columnNameArray [20] = "skillID2";
                int.TryParse(csvFile.mapData[i].data[21],out data.skillID3);
                columnNameArray [21] = "skillID3";
                float.TryParse(csvFile.mapData[i].data[22],out data.scale);
                columnNameArray [22] = "scale";
                data.bornEffect = csvFile.mapData[i].data[23];
                columnNameArray [23] = "bornEffect";
                data.attackedEffect = csvFile.mapData[i].data[24];
                columnNameArray [24] = "attackedEffect";
                data.attackedEffect2 = csvFile.mapData[i].data[25];
                columnNameArray [25] = "attackedEffect2";
                data.jinzhanAttackedEffect2 = csvFile.mapData[i].data[26];
                columnNameArray [26] = "jinzhanAttackedEffect2";
                data.cameraEffect = csvFile.mapData[i].data[27];
                columnNameArray [27] = "cameraEffect";
                data.deadEffect = csvFile.mapData[i].data[28];
                columnNameArray [28] = "deadEffect";
                int.TryParse(csvFile.mapData[i].data[29],out data.isRemove);
                columnNameArray [29] = "isRemove";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static MonsterData GetByID (int id,List<MonsterData> data)
        {
            foreach (MonsterData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public string PrefabsPath;//资源路径
        public int type;//类型
        public string name;//名字
        public int currentHP;//当前血量
        public float moveSpeed;//移动速度
        public int maxHP;//最大血量
        public float inductionDis;//警惕范围
        public float patrolRadius;//巡逻半径
        public float PatrolCD;//巡逻CD
        public float floowDis;//追击范围
        public float attackDis;//攻击距离
        public float attackCD;//攻击cd
        public int attackValue;//攻击力
        public string bornSound;//出生声音
        public string attackSound;//攻击声音
        public string deadSound;//死亡声音
        public string attackedSound;//被打声音
        public string footSound;//跑步声音
        public int skillID1;//技能1
        public int skillID2;//技能2
        public int skillID3;//技能3
        public float scale;//缩放系数
        public string bornEffect;//出生特效
        public string attackedEffect;//被打特效
        public string attackedEffect2;//被打特效2
        public string jinzhanAttackedEffect2;//近战被打特效
        public string cameraEffect;//被打时相机特效
        public string deadEffect;//死亡特效
        public int isRemove;//死亡是否清除尸体
    }
}
