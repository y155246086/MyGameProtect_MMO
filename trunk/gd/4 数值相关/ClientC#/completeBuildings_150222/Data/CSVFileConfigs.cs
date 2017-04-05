using UnityEngine;
using System.Collections;

public static class CSVFileConfigs  {

	public const string dict_hero = "Configs/dict_hero";

}


public enum DICT_HERO : int
{
	HERO_TYPEID = 0,
	NAME_ID,
	DESC_ID, //DESCRIPTION,
	
	ICON_FILE,          // UI图集 文件名
	ICON_SPRITE_NAME,   // UI ICON
	FBX_FILE,           // PREFABE_RED,        // 模型 文件名    FBX_FILE,
	//PREFABE_BLUE,       // 模型 文件名    FBX_FILE,
	
	ARMY_TYPE,          // 士兵类型
	PORTARAIT,          // 头像 ICON
	QUALITY,            // 卡牌星级
	CLASSIFY,           // 卡牌品质
	
	ATTACK_DISTANCE,
	ATTACK_SPEED,
	ATTACK_ANIMATORTIME,
	ATTACK_INTERVALj,
	
	DAMAGE_MODE,
	DAMAGE_RANGE,
	
	MOVE_SPEED,
	BASE_ATTACK,
	BASE_DEF,
	BASE_HP,
	BASE_VIOLENCE,
	BASE_LEADER,
	
	BASE_SKILL1_TYPEID, // 技能01
	BASE_SKILL2_TYPEID, // 技能02
	BASE_SKILL3_TYPEID, // 技能03
	
	LIBRARY,        // 图鉴
	
	// 基友数据
	GAY_ATK,
	GAY_DEF,
	GAY_HP,
	GAY_LEADER,
	//攻击模式
	NATURE
}
