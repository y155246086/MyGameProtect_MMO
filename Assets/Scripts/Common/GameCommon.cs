using UnityEngine;
using System.Collections;

/// <summary>
///  玩家状态
/// </summary>
public enum PlayerState
{
    Battle = 0,
    UNBattle = 1
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

public enum SpriteType
{
    Player = 0,
    Monster
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

//delegate ==
//玩家属性变化
//public delegate void CharactorPropertyChange(float changeValue);


