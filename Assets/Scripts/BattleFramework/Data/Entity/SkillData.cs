using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SkillData {
        public static string csvFilePath = "Configs/SkillData";
        public static string[] columnNameArray = new string[8];
        public static List<SkillData> dataList;
        public static Dictionary<int, SkillData> dataMap;
        public static List<SkillData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            dataList = new List<SkillData>();
            dataMap = new Dictionary<int, SkillData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[8];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                SkillData data = new SkillData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.name = csvFile.mapData[i].data[1];
                columnNameArray [1] = "name";
                data.desc = csvFile.mapData[i].data[2];
                columnNameArray [2] = "desc";
                int.TryParse(csvFile.mapData[i].data[3],out data.icon);
                columnNameArray [3] = "icon";
                data.cd= new List<int>();
                strs = csvFile.mapData[i].data[4].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.cd.Add(int.Parse(strs[j]));
                }
                columnNameArray [4] = "cd";
                int.TryParse(csvFile.mapData[i].data[5],out data.dependSkill);
                columnNameArray [5] = "dependSkill";
                data.skillAction= new List<int>();
                strs = csvFile.mapData[i].data[6].Split(new char[1]{','});
                for(int j=0;j<strs.Length;j++){
                    data.skillAction.Add(int.Parse(strs[j]));
                }
                columnNameArray [6] = "skillAction";
                int.TryParse(csvFile.mapData[i].data[7],out data.castRange);
                columnNameArray [7] = "castRange";
                dataList.Add(data);
                if (!dataMap.ContainsKey(data.id))
                    dataMap.Add(data.id,data);
            }
            return dataList;
        }
  
        public static SkillData GetByID (int id,List<SkillData> data)
        {
            foreach (SkillData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
  
        public static SkillData GetByID (int id)
        {
            return GetByID(id,dataList);
        }
  
        public int id;//数据ID
        public string name;//名字
        public string desc;//描述
        public int icon;//图标
        public List<int> cd;//cd
        public int dependSkill;//关联skillDataID
        public List<int> skillAction;//skillActionIDList
        public int castRange;//施法范围
    }
}
