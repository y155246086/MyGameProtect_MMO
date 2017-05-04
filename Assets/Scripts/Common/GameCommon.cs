using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///  玩家状态
/// </summary>
public enum PlayerState
{
    Battle = 0,
    UNBattle = 1
}
/// <summary>
/// 职业
/// </summary>
public enum Vocation
{
    Warrior = 1,         // 战士
    Assassin = 2,       // 刺客
    Archer = 3,         // 弓箭手
    Mage = 4,           // 法师
    Others = 5           // 其他
}
/// <summary>
///  角色属性
/// </summary>
public enum CharactorProperty
{
    HP          = 0,//血量
    SPEED       = 1,//速度
    TURN_SPEED  = 2,//转速
    MAX_HP      = 3//最大血量
}
/// <summary>
/// 精灵类型
/// </summary>
public enum SpriteType
{
    NONE = 0,//无
    Myself = 1,//玩家
    Player = 3,//玩家
    Monster =  4,//怪
}
public enum TargetSelectType
{
    Enemy = 0,          // 敌人
    Myself = 1,         // 自己
    TeamMember = 2,     // 队友
    Ally = 3            // 友方
}
/// <summary>
/// 目标类型
/// </summary>
public enum AttackTargetType
{
    Enemy = 0,          // 敌人
    Myself = 1,         // 自己
    TeamMember = 2,     // 队友
    Ally = 3            // 友方
}
/// <summary>
/// 选择范围类型  0  扇形范围 1  圆形范围， 2， 单体。 3  直线范围 4 前方范围
/// </summary>
public enum TargetRangeType
{
    SectorRange = 0,
    CircleRange = 1,
    SingeTarget = 2,
    LineRange = 3,
    WorldRange = 6
}

public class GameCommon
{
    //npc活动范围的半径
    public static readonly int NPC_MOVEAREA_RAD = 10;
    //城市场景ID
    public static int SCENE_CITY_ID = 1;
    //野外场景ID
    public static int SCENE_YEWAI_ID = 2;
    //登陆场景ID
    public static int SCENE_LOGIN_ID = 3;

    /// <summary>
    /// 地面层的索引
    /// </summary>
    public static int GROUND_LAYER_INDEX = 8;
    //怪物的层
    public static int ENEMY_LAYER = 12;
}
/// <summary>
/// 物品类型
/// </summary>
public enum GoodsType
{
    RECOVERY = 0 //恢复类型
}
/// <summary>
/// 恢复类型
/// </summary>
public enum RecoveryType
{
    HP = 0
}

public class ActionConstants
{
    public readonly static int CITY_IDLE = -1; //主城的站立
    public readonly static int COPY_IDLE = 0; //副本的站立
    public readonly static int HIT = 11; //受击
    public readonly static int HIT_AIR = 12; //浮空
    public readonly static int HIT_GROUND = 13; //倒地受击
    public readonly static int KNOCK_DOWN = 14; //击飞
    public readonly static int PUSH = 15; //后退
    public readonly static int STUN = 16;
    public readonly static int DIE = 17; //死亡
    public readonly static int REVIVE = 19; //复活
    public readonly static int DIE_KNOCK_DOWN = 37; //击飞死亡
    public readonly static int DIE_AIR = 38; //浮空死亡
}

public class PlayerActionNames
{//角色动作ID对照名字表,用于技能结束判断
    public readonly static Dictionary<int, string> names = new Dictionary<int, string>()
        {
            {-1, "idle"},
            {0, "ready"},
            {1, "attack_1"},
            {2, "attack_2"},
            {3, "attack_3"},
            {4, "powercharge"},
            {5, "powerattack_1"},
            {6, "powerattack_2"},
            {7, "powerattack_3"},
            {8, "skill_1"},
            {9, "skill_2"},
            {10, "rush"},
            {11, "hit"},
            {12, "hitair"},
            {13, "hitground"},
            {14, "knockdown"},
            {15, "push"},
            {16, "stun"},
            {17, "die"},
        };

    public readonly static string IDLE = "idle";
    public readonly static string READY = "ready";
    public readonly static string ATTACK_1 = "attack_1";
    public readonly static string ATTACK_2 = "attack_2";
    public readonly static string ATTACK_3 = "attack_3";
    public readonly static string POWERCHARGE = "powercharge";
    public readonly static string POWERATTACK_1 = "powerattack_1";
    public readonly static string POWERATTACK_2 = "powerattack_2";
    public readonly static string POWERATTACK_3 = "powerattack_3";
    public readonly static string SKILL_1 = "skill_1";
    public readonly static string SKILL_2 = "skill_2";
    public readonly static string RUSH = "rush";
    public readonly static string HIT = "hit";
    public readonly static string HITAIR = "hitair";
    public readonly static string HITGROUND = "hitground";
    public readonly static string KNOCKDOWN = "knockdown";
    public readonly static string PUSH = "push";
    public readonly static string STUN = "stun";
    public readonly static string DIE = "die";
    public readonly static string DIE_HITAIR = "dir_hitair";
    public readonly static string DIE_KNOCKDOWN = "dir_knockdown";
    public readonly static string GETUP = "getup";
}

public class ActionTime
{//动作时间，毫秒
    public readonly static uint HIT = 600;
    public readonly static uint HIT_AIR = 3500;
    public readonly static uint KNOCK_DOWN = 3500;
    public readonly static uint PUSH = 1000;
    public readonly static uint HIT_GROUND = 3000;
    public readonly static uint REVIVE = 2500;
}

public class StateCfg
{
    public readonly static int DEATH_STATE = 0;             //死亡状态       
    public readonly static int DIZZY_STATE = 1;             //眩晕状态       
    public readonly static int POSSESS_STATE = 2;             //魅惑状态       
    public readonly static int IMMOBILIZE_STATE = 3;             //定身状态       
    public readonly static int SILENT_STATE = 4;             //沉默状态       
    public readonly static int STIFF_STATE = 5;             //僵直状态       
    public readonly static int FLOAT_STATE = 6;             //浮空状态       
    public readonly static int DOWN_STATE = 7;             //击倒状态       
    public readonly static int BACK_STATE = 8;             //击退状态       
    public readonly static int UP_STATE = 9;             //击飞状态       
    public readonly static int IMMUNITY_STATE = 10;            //免疫状态       
    public readonly static int NO_HIT_STATE = 11;            //无法被击中状态 
    public readonly static int SLOW_DOWN_STATE = 12;            //无法被击中状态 
    public readonly static int BATI_STATE = 13;            //霸体状态 
}
public enum HitColorType
{
    HCT_WHITE,
    HCT_RED
}

