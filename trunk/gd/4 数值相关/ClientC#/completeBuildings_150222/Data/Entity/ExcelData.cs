using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public static class ExcelData {
//--------------------CastleBuildDesign---------------------------//
        public class CastleBuildDesign {
            public static string csvFilePath = "Configs/CastleBuildDesign";
            public  static List<CastleBuildDesign> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<CastleBuildDesign> dataList = new List<CastleBuildDesign>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        CastleBuildDesign data = new CastleBuildDesign();
                    int.TryParse(csvFile.mapData[i].data[0],out data.id);
                    data.cName = csvFile.mapData[i].data[1];
                    data.eName = csvFile.mapData[i].data[2];
                    int.TryParse(csvFile.mapData[i].data[3],out data.maxBuildingNUM);
                    int.TryParse(csvFile.mapData[i].data[4],out data.maxLevel);
                    int.TryParse(csvFile.mapData[i].data[5],out data.castleBuildingBeginID);
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public int id;//
                public string cName;//建筑物编号
                public string eName;//建筑物名字
                public int maxBuildingNUM;//建筑物名字
                public int maxLevel;//最大建造数量
                public int castleBuildingBeginID;//建筑物最大等级
            }
 
//--------------------CastleBuildingItems---------------------------//
        public class CastleBuildingItems {
            public static string csvFilePath = "Configs/CastleBuildingItems";
            public  static List<CastleBuildingItems> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<CastleBuildingItems> dataList = new List<CastleBuildingItems>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        CastleBuildingItems data = new CastleBuildingItems();
                    int.TryParse(csvFile.mapData[i].data[0],out data.id);
                    data.name = csvFile.mapData[i].data[1];
                    int.TryParse(csvFile.mapData[i].data[2],out data.level);
                    int.TryParse(csvFile.mapData[i].data[3],out data.goldCost);
                    int.TryParse(csvFile.mapData[i].data[4],out data.magicCost);
                    int.TryParse(csvFile.mapData[i].data[5],out data.timeCost);
                    int.TryParse(csvFile.mapData[i].data[6],out data.numericalAttributes);
                    int.TryParse(csvFile.mapData[i].data[7],out data.getPlayeExp);
                    data.buildingART = csvFile.mapData[i].data[8];
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public int id;//
                public string name;//城堡建筑物id
                public int level;//建筑物名字
                public int goldCost;//建筑物等级
                public int magicCost;//需要金币
                public int timeCost;//需要泉水
                public int numericalAttributes;//需要时间
                public int getPlayeExp;//数值属性
                public string buildingART;//获得玩家经验表
            }
 
//--------------------CastleLimits---------------------------//
        public class CastleLimits {
            public static string csvFilePath = "Configs/CastleLimits";
            public  static List<CastleLimits> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<CastleLimits> dataList = new List<CastleLimits>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        CastleLimits data = new CastleLimits();
                    data.id = csvFile.mapData[i].data[0];
                    bool.TryParse(csvFile.mapData[i].data[1],out data.isOpen);
                    int.TryParse(csvFile.mapData[i].data[2],out data.barrackID);
                    int.TryParse(csvFile.mapData[i].data[3],out data.barrackNUM);
                    int.TryParse(csvFile.mapData[i].data[4],out data.barrackMaxLEvel);
                    int.TryParse(csvFile.mapData[i].data[5],out data.campsiteID);
                    int.TryParse(csvFile.mapData[i].data[6],out data.campsiteNUM);
                    int.TryParse(csvFile.mapData[i].data[7],out data.campsiteMaxLEvel);
                    int.TryParse(csvFile.mapData[i].data[8],out data.goldmineID);
                    int.TryParse(csvFile.mapData[i].data[9],out data.goldmineNUM);
                    int.TryParse(csvFile.mapData[i].data[10],out data.goldmineMaxLEvel);
                    int.TryParse(csvFile.mapData[i].data[11],out data.magicSpringID);
                    int.TryParse(csvFile.mapData[i].data[12],out data.magicSpringNUM);
                    int.TryParse(csvFile.mapData[i].data[13],out data.magicSpringMaxLEvel);
                    int.TryParse(csvFile.mapData[i].data[14],out data.savingsPotID);
                    int.TryParse(csvFile.mapData[i].data[15],out data.savingsPotNUM);
                    int.TryParse(csvFile.mapData[i].data[16],out data.savingsPotMaxLEvel);
                    int.TryParse(csvFile.mapData[i].data[17],out data.magicBottleID);
                    int.TryParse(csvFile.mapData[i].data[18],out data.magicBottleNUM);
                    int.TryParse(csvFile.mapData[i].data[19],out data.magicBottleMaxLEvel);
                    int.TryParse(csvFile.mapData[i].data[20],out data.heroCampsiteID);
                    int.TryParse(csvFile.mapData[i].data[21],out data.heroCampsiteNUM);
                    int.TryParse(csvFile.mapData[i].data[22],out data.heroCampsiteMaxLEvel);
                    int.TryParse(csvFile.mapData[i].data[23],out data.laboratoryID);
                    int.TryParse(csvFile.mapData[i].data[24],out data.laboratoryNUM);
                    int.TryParse(csvFile.mapData[i].data[25],out data.laboratoryMaxLEvel);
                    int.TryParse(csvFile.mapData[i].data[26],out data.spellFactoryID);
                    int.TryParse(csvFile.mapData[i].data[27],out data.spellFactoryNUM);
                    int.TryParse(csvFile.mapData[i].data[28],out data.spellFactoryMaxLEvel);
                    int.TryParse(csvFile.mapData[i].data[29],out data.workerHouseID);
                    int.TryParse(csvFile.mapData[i].data[30],out data.workerHouseNUM);
                    int.TryParse(csvFile.mapData[i].data[31],out data.workerHouseMaxLEvel);
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public string id;//
                public bool isOpen;//城堡等级
                public int barrackID;//对应等级是否开放
                public int barrackNUM;//兵营编号
                public int barrackMaxLEvel;//兵营限制数量
                public int campsiteID;//兵营限制等级
                public int campsiteNUM;//营地编号
                public int campsiteMaxLEvel;//营地限制数量
                public int goldmineID;//营地限制等级
                public int goldmineNUM;//金矿编号
                public int goldmineMaxLEvel;//金矿限制数量
                public int magicSpringID;//金矿限制等级
                public int magicSpringNUM;//魔法泉编号
                public int magicSpringMaxLEvel;//魔法泉限制数量
                public int savingsPotID;//魔法泉限制等级
                public int savingsPotNUM;//储金罐编号
                public int savingsPotMaxLEvel;//储金罐限制数量
                public int magicBottleID;//储金罐限制等级
                public int magicBottleNUM;//魔法瓶编号
                public int magicBottleMaxLEvel;//魔法瓶限制数量
                public int heroCampsiteID;//魔法瓶限制等级
                public int heroCampsiteNUM;//英雄营地编号
                public int heroCampsiteMaxLEvel;//英雄营地限制数量
                public int laboratoryID;//英雄营地限制等级
                public int laboratoryNUM;//实验室编号
                public int laboratoryMaxLEvel;//实验室限制数量
                public int spellFactoryID;//实验室限制等级
                public int spellFactoryNUM;//法术工厂编号
                public int spellFactoryMaxLEvel;//法术工厂限制数量
                public int workerHouseID;//法术工厂限制等级
                public int workerHouseNUM;//工人木屋编号
                public int workerHouseMaxLEvel;//工人木屋限制数量
            }
 
//--------------------HeroLevelGrowup---------------------------//
        public class HeroLevelGrowup {
            public static string csvFilePath = "Configs/HeroLevelGrowup";
            public  static List<HeroLevelGrowup> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<HeroLevelGrowup> dataList = new List<HeroLevelGrowup>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        HeroLevelGrowup data = new HeroLevelGrowup();
                    int.TryParse(csvFile.mapData[i].data[0],out data.id);
                    int.TryParse(csvFile.mapData[i].data[1],out data.life);
                    int.TryParse(csvFile.mapData[i].data[2],out data.attack);
                    int.TryParse(csvFile.mapData[i].data[3],out data.defence);
                    float.TryParse(csvFile.mapData[i].data[4],out data.lifeGrowup);
                    float.TryParse(csvFile.mapData[i].data[5],out data.attackGrowup);
                    float.TryParse(csvFile.mapData[i].data[6],out data.defenceGrowup);
                    float.TryParse(csvFile.mapData[i].data[7],out data.reduceDamage);
                    float.TryParse(csvFile.mapData[i].data[8],out data.moveSpeedAdd);
                    float.TryParse(csvFile.mapData[i].data[9],out data.attackSpeedAdd);
                    float.TryParse(csvFile.mapData[i].data[10],out data.reduceSkillCD);
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public int id;//
                public int life;//序号
                public int attack;//初始生命
                public int defence;//初始攻击
                public float lifeGrowup;//初始防御
                public float attackGrowup;//生命成长
                public float defenceGrowup;//攻击成长
                public float reduceDamage;//防御成长
                public float moveSpeedAdd;//伤害免伤
                public float attackSpeedAdd;//移动速度加成（百化）
                public float reduceSkillCD;//攻击速度加成（百化）
            }
 
//--------------------Heros---------------------------//
        public class Heros {
            public static string csvFilePath = "Configs/Heros";
            public  static List<Heros> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<Heros> dataList = new List<Heros>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        Heros data = new Heros();
                    int.TryParse(csvFile.mapData[i].data[0],out data.id);
                    bool.TryParse(csvFile.mapData[i].data[1],out data.isOpen);
                    data.showName = csvFile.mapData[i].data[2];
                    int.TryParse(csvFile.mapData[i].data[3],out data.unitType);
                    int.TryParse(csvFile.mapData[i].data[4],out data.lifeType);
                    int.TryParse(csvFile.mapData[i].data[5],out data.attackType);
                    int.TryParse(csvFile.mapData[i].data[6],out data.damageType);
                    int.TryParse(csvFile.mapData[i].data[7],out data.armorType);
                    int.TryParse(csvFile.mapData[i].data[8],out data.moveSpeed);
                    int.TryParse(csvFile.mapData[i].data[9],out data.attackDistance);
                    float.TryParse(csvFile.mapData[i].data[10],out data.attackSpeed);
                    int.TryParse(csvFile.mapData[i].data[11],out data.maxLevel);
                    int.TryParse(csvFile.mapData[i].data[12],out data.growupID);
                    data.itemIcon = csvFile.mapData[i].data[13];
                    data.descrption = csvFile.mapData[i].data[14];
                    data.prefab = csvFile.mapData[i].data[15];
                    int.TryParse(csvFile.mapData[i].data[16],out data.beginSkill);
                    int.TryParse(csvFile.mapData[i].data[17],out data.passivitySkill);
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public int id;//
                public bool isOpen;//序号
                public string showName;//是否投放
                public int unitType;//屏幕显示名称
                public int lifeType;//单位类型
                public int attackType;//类型
                public int damageType;//攻击方式对空对地
                public int armorType;//伤害类型
                public int moveSpeed;//护甲类型
                public int attackDistance;//移动速度
                public float attackSpeed;//攻击距离
                public int maxLevel;//攻击速度
                public int growupID;//最大等级
                public string itemIcon;//成长
                public string descrption;//头像
                public string prefab;//描述
                public int beginSkill;//prefab资源
                public int passivitySkill;//主动技能
            }
 
//--------------------PlayerExp---------------------------//
        public class PlayerExp {
            public static string csvFilePath = "Configs/PlayerExp";
            public  static List<PlayerExp> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<PlayerExp> dataList = new List<PlayerExp>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        PlayerExp data = new PlayerExp();
                    int.TryParse(csvFile.mapData[i].data[0],out data.id);
                    int.TryParse(csvFile.mapData[i].data[1],out data.playerExp);
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public int id;//
                public int playerExp;//玩家等级
            }
 
//--------------------Soldier---------------------------//
        public class Soldier {
            public static string csvFilePath = "Configs/Soldier";
            public  static List<Soldier> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<Soldier> dataList = new List<Soldier>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        Soldier data = new Soldier();
                    int.TryParse(csvFile.mapData[i].data[0],out data.id);
                    data.name = csvFile.mapData[i].data[1];
                    int.TryParse(csvFile.mapData[i].data[2],out data.accountPopulation);
                    int.TryParse(csvFile.mapData[i].data[3],out data.needBarrackLevel);
                    int.TryParse(csvFile.mapData[i].data[4],out data.unitType);
                    int.TryParse(csvFile.mapData[i].data[5],out data.lifeType);
                    int.TryParse(csvFile.mapData[i].data[6],out data.attackType);
                    int.TryParse(csvFile.mapData[i].data[7],out data.damageType);
                    int.TryParse(csvFile.mapData[i].data[8],out data.armorType);
                    int.TryParse(csvFile.mapData[i].data[9],out data.moveSpeed);
                    int.TryParse(csvFile.mapData[i].data[10],out data.TrainingTime);
                    int.TryParse(csvFile.mapData[i].data[11],out data.attackDistance);
                    float.TryParse(csvFile.mapData[i].data[12],out data.attackSpeed);
                    int.TryParse(csvFile.mapData[i].data[13],out data.maxLevel);
                    int.TryParse(csvFile.mapData[i].data[14],out data.soldierBeginID);
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public int id;//
                public string name;//编号
                public int accountPopulation;//名字
                public int needBarrackLevel;//所占人口
                public int unitType;//所需兵营等级
                public int lifeType;//单位类型
                public int attackType;//分类
                public int damageType;//攻击类型
                public int armorType;//伤害类型
                public int moveSpeed;//护甲类型
                public int TrainingTime;//移动速度
                public int attackDistance;//训练时间
                public float attackSpeed;//攻击距离
                public int maxLevel;//攻击速度
                public int soldierBeginID;//最大等级
            }
 
//--------------------SoldierProperty---------------------------//
        public class SoldierProperty {
            public static string csvFilePath = "Configs/SoldierProperty";
            public  static List<SoldierProperty> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<SoldierProperty> dataList = new List<SoldierProperty>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        SoldierProperty data = new SoldierProperty();
                    int.TryParse(csvFile.mapData[i].data[0],out data.id);
                    data.name = csvFile.mapData[i].data[1];
                    data.soldier = csvFile.mapData[i].data[2];
                    data.needLaboratoryLevel = csvFile.mapData[i].data[3];
                    int.TryParse(csvFile.mapData[i].data[4],out data.trainMagicCost);
                    data.healthProperty = csvFile.mapData[i].data[5];
                    data.attackValue = csvFile.mapData[i].data[6];
                    int.TryParse(csvFile.mapData[i].data[7],out data.magicCost);
                    int.TryParse(csvFile.mapData[i].data[8],out data.oilCost);
                    int.TryParse(csvFile.mapData[i].data[9],out data.timeCost);
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public int id;//
                public string name;//士兵编号ID
                public string soldier;//名字
                public string needLaboratoryLevel;//士兵等级
                public int trainMagicCost;//需要实验室等级
                public string healthProperty;//训练消耗泉水
                public string attackValue;//生命值
                public int magicCost;//攻击力
                public int oilCost;//升级需要泉水
                public int timeCost;//升级需要黑油
            }
 
//--------------------SpellSolution---------------------------//
        public class SpellSolution {
            public static string csvFilePath = "Configs/SpellSolution";
            public  static List<SpellSolution> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<SpellSolution> dataList = new List<SpellSolution>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        SpellSolution data = new SpellSolution();
                    int.TryParse(csvFile.mapData[i].data[0],out data.id);
                    data.name = csvFile.mapData[i].data[1];
                    int.TryParse(csvFile.mapData[i].data[2],out data.needSpellFactoryLevel);
                    float.TryParse(csvFile.mapData[i].data[3],out data.radius);
                    float.TryParse(csvFile.mapData[i].data[4],out data.randomRadius);
                    int.TryParse(csvFile.mapData[i].data[5],out data.NUM);
                    float.TryParse(csvFile.mapData[i].data[6],out data.interval);
                    int.TryParse(csvFile.mapData[i].data[7],out data.TrainingTime);
                    int.TryParse(csvFile.mapData[i].data[8],out data.maxLevel);
                    int.TryParse(csvFile.mapData[i].data[9],out data.spellSolutionBeginID);
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public int id;//
                public string name;//法术药水编号
                public int needSpellFactoryLevel;//名称
                public float radius;//需要法术工厂等级
                public float randomRadius;//半径
                public int NUM;//随机半径
                public float interval;//数量
                public int TrainingTime;//释放间隔时间
                public int maxLevel;//训练时间
                public int spellSolutionBeginID;//最大等级
            }
 
//--------------------SpellSolutionProperty---------------------------//
        public class SpellSolutionProperty {
            public static string csvFilePath = "Configs/SpellSolutionProperty";
            public  static List<SpellSolutionProperty> LoadDatas(){
                  CSVFile csvFile = new CSVFile();
                  csvFile.Open (csvFilePath);
                  List<SpellSolutionProperty> dataList = new List<SpellSolutionProperty>();
                  for(int i = 0;i < csvFile.mapData.Count;i ++){
                        SpellSolutionProperty data = new SpellSolutionProperty();
                    int.TryParse(csvFile.mapData[i].data[0],out data.id);
                    data.name = csvFile.mapData[i].data[1];
                    int.TryParse(csvFile.mapData[i].data[2],out data.trainGoldCost);
                    float.TryParse(csvFile.mapData[i].data[3],out data.totalValue);
                    int.TryParse(csvFile.mapData[i].data[4],out data.singleValue);
                    int.TryParse(csvFile.mapData[i].data[5],out data.magicCost);
                    int.TryParse(csvFile.mapData[i].data[6],out data.timeCost);
                    dataList.Add(data);
                }
                return dataList;
               }
 
                public int id;//
                public string name;//法术编号ID
                public int trainGoldCost;//名称
                public float totalValue;//训练消耗金币
                public int singleValue;//总值（伤害/治疗/冰冻时间）
                public int magicCost;//单次值（伤害/治疗）
                public int timeCost;//升级需要泉水
            }
 
    }
}
