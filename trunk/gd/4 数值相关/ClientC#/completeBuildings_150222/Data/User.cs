//CopyRight YingYuGang(232871714@qq.com) 2014-2015
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BattleFramework.Data{

    [System.Serializable]
	public class UserData
    {
        public string name;
        public int level;
        public int exp;
        public int upgradeExp;
        public int coin;
        public int crystal;
        public int dollor;

        public List<HeroData> heros;
        public CityData city;
	}
    [System.Serializable]
    public class HeroData
    {
        public int id;
        public string name;
        public int attack;
        public int defence;
        public int fight;
        public int health;
        public string card;
        public int star;

		public int locationIndex;
		public int soldierType;
		public int soldierLevel;
		public int soldierNum;//Max is 16

		public string icon;

    }
    [System.Serializable]
    public class CityData
    {
        public string name;
        public int level;
        public List<BuildingData> buildings;
    }
    [System.Serializable]
    public class BuildingData
    {
        public int id;
        public string name;
        public int level;
    }

}
