using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class StoryData {
        public static string csvFilePath = "Configs/StoryData";
        public static string[] columnNameArray = new string[40];
        public static List<StoryData> LoadDatas(){
            CSVFile csvFile = new CSVFile();
            csvFile.Open (csvFilePath);
            List<StoryData> dataList = new List<StoryData>();
            string[] strs;
            string[] strsTwo;
            List<int> listChild;
            columnNameArray = new string[40];
            for(int i = 0;i < csvFile.mapData.Count;i ++){
                StoryData data = new StoryData();
                int.TryParse(csvFile.mapData[i].data[0],out data.id);
                columnNameArray [0] = "id";
                data.desc = csvFile.mapData[i].data[1];
                columnNameArray [1] = "desc";
                int.TryParse(csvFile.mapData[i].data[2],out data.storyId);
                columnNameArray [2] = "storyId";
                int.TryParse(csvFile.mapData[i].data[3],out data.nextStoryId);
                columnNameArray [3] = "nextStoryId";
                int.TryParse(csvFile.mapData[i].data[4],out data.isSon);
                columnNameArray [4] = "isSon";
                int.TryParse(csvFile.mapData[i].data[5],out data.nextId);
                columnNameArray [5] = "nextId";
                int.TryParse(csvFile.mapData[i].data[6],out data.type);
                columnNameArray [6] = "type";
                int.TryParse(csvFile.mapData[i].data[7],out data.moveState);
                columnNameArray [7] = "moveState";
                float.TryParse(csvFile.mapData[i].data[8],out data.moveSpeed);
                columnNameArray [8] = "moveSpeed";
                int.TryParse(csvFile.mapData[i].data[9],out data.animationID);
                columnNameArray [9] = "animationID";
                int.TryParse(csvFile.mapData[i].data[10],out data.isDaobo);
                columnNameArray [10] = "isDaobo";
                data.animationName = csvFile.mapData[i].data[11];
                columnNameArray [11] = "animationName";
                data.sound = csvFile.mapData[i].data[12];
                columnNameArray [12] = "sound";
                data.soundContent = csvFile.mapData[i].data[13];
                columnNameArray [13] = "soundContent";
                int.TryParse(csvFile.mapData[i].data[14],out data.player);
                columnNameArray [14] = "player";
                int.TryParse(csvFile.mapData[i].data[15],out data.effect);
                columnNameArray [15] = "effect";
                int.TryParse(csvFile.mapData[i].data[16],out data.closeEffect);
                columnNameArray [16] = "closeEffect";
                data.targetPointName = csvFile.mapData[i].data[17];
                columnNameArray [17] = "targetPointName";
                float.TryParse(csvFile.mapData[i].data[18],out data.delay);
                columnNameArray [18] = "delay";
                float.TryParse(csvFile.mapData[i].data[19],out data.totalTime);
                columnNameArray [19] = "totalTime";
                float.TryParse(csvFile.mapData[i].data[20],out data.speed);
                columnNameArray [20] = "speed";
                float.TryParse(csvFile.mapData[i].data[21],out data.yRotion);
                columnNameArray [21] = "yRotion";
                int.TryParse(csvFile.mapData[i].data[22],out data.SceneLevelDataID);
                columnNameArray [22] = "SceneLevelDataID";
                int.TryParse(csvFile.mapData[i].data[23],out data.isMove);
                columnNameArray [23] = "isMove";
                int.TryParse(csvFile.mapData[i].data[24],out data.light);
                columnNameArray [24] = "light";
                int.TryParse(csvFile.mapData[i].data[25],out data.clearMonster);
                columnNameArray [25] = "clearMonster";
                int.TryParse(csvFile.mapData[i].data[26],out data.startSoundId);
                columnNameArray [26] = "startSoundId";
                int.TryParse(csvFile.mapData[i].data[27],out data.endSoundId);
                columnNameArray [27] = "endSoundId";
                int.TryParse(csvFile.mapData[i].data[28],out data.stopSoundId);
                columnNameArray [28] = "stopSoundId";
                int.TryParse(csvFile.mapData[i].data[29],out data.startObstacleId);
                columnNameArray [29] = "startObstacleId";
                int.TryParse(csvFile.mapData[i].data[30],out data.endObstacleId);
                columnNameArray [30] = "endObstacleId";
                float.TryParse(csvFile.mapData[i].data[31],out data.gameSpeed);
                columnNameArray [31] = "gameSpeed";
                int.TryParse(csvFile.mapData[i].data[32],out data.triggerBlur);
                columnNameArray [32] = "triggerBlur";
                data.huRotation= new Vector3();
                strs = csvFile.mapData[i].data[33].Split(new char[1]{','});
                    data.huRotation.x = (float.Parse(strs[0]));
                    data.huRotation.y = (float.Parse(strs[1]));
                    data.huRotation.z = (float.Parse(strs[2]));
                columnNameArray [33] = "huRotation";
                data.fogColor= new Vector3();
                strs = csvFile.mapData[i].data[34].Split(new char[1]{','});
                    data.fogColor.x = (float.Parse(strs[0]));
                    data.fogColor.y = (float.Parse(strs[1]));
                    data.fogColor.z = (float.Parse(strs[2]));
                columnNameArray [34] = "fogColor";
                data.fogDis= new Vector3();
                strs = csvFile.mapData[i].data[35].Split(new char[1]{','});
                    data.fogDis.x = (float.Parse(strs[0]));
                    data.fogDis.y = (float.Parse(strs[1]));
                    data.fogDis.z = (float.Parse(strs[2]));
                columnNameArray [35] = "fogDis";
                int.TryParse(csvFile.mapData[i].data[36],out data.triggerPanting);
                columnNameArray [36] = "triggerPanting";
                int.TryParse(csvFile.mapData[i].data[37],out data.triggerFire);
                columnNameArray [37] = "triggerFire";
                int.TryParse(csvFile.mapData[i].data[38],out data.isLookHu);
                columnNameArray [38] = "isLookHu";
                int.TryParse(csvFile.mapData[i].data[39],out data.isLockDir);
                columnNameArray [39] = "isLockDir";
                dataList.Add(data);
            }
            return dataList;
        }
  
        public static StoryData GetByID (int id,List<StoryData> data)
        {
            foreach (StoryData item in data) {
                if (id == item.id) {
                     return item;
                }
            }
            return null;
        }
  
        public int id;//数据ID
        public string desc;//描述
        public int storyId;//剧情ID
        public int nextStoryId;//连续的剧情ID
        public int isSon;//是否是子剧情
        public int nextId;//链接剧情数据ID
        public int type;//类型
        public int moveState;//移动的状态
        public float moveSpeed;//移动的速度
        public int animationID;//动画ID
        public int isDaobo;//是否倒着播放动画
        public string animationName;//播放动画名称
        public string sound;//声音名称
        public string soundContent;//声音内容
        public int player;//角色ID
        public int effect;//特效资源编号
        public int closeEffect;//剧情触发关闭特效编号
        public string targetPointName;//剧情目标点
        public float delay;//延时
        public float totalTime;//总时间
        public float speed;//轨道速度
        public float yRotion;//Y轴角度
        public int SceneLevelDataID;//完成后触发刷怪
        public int isMove;//剧情播放移动
        public int light;//剧情结束是否开灯
        public int clearMonster;//是否清除怪
        public int startSoundId;//剧情开始开启音效
        public int endSoundId;//剧情结束开启音效
        public int stopSoundId;//剧情结束关闭音效
        public int startObstacleId;//剧情开始开启障碍物
        public int endObstacleId;//剧情结束开启障碍物
        public float gameSpeed;//剧情开始游戏速率
        public int triggerBlur;//剧情开始触发虚幻效果
        public Vector3 huRotation;//胡八一朝向
        public Vector3 fogColor;//雾的颜色剧情开始触发
        public Vector3 fogDis;//雾的距离剧情开始触发
        public int triggerPanting;//剧情结束触发喘气
        public int triggerFire;//剧情结束触发开火
        public int isLookHu;//剧情触发朝向胡八一
        public int isLockDir;//剧情开始触发锁定朝向
    }
}
