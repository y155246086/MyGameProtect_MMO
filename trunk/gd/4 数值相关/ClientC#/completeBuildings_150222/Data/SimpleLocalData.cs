//CopyRight YingYuGang(232871714@qq.com) 2014-2015
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BattleFramework.Data{
    //this class is used to Simulation simple local data;
	public static class SimpleLocalData{
       
        public static UserData user;
        public static UserData GetUserData()
        {
            if (user == null)
                user = new UserData();
            user.name = "根本停不下来";
            user.coin = 1000;
            user.crystal = 100;
            user.dollor = 10;
            user.exp = 20;
            user.upgradeExp = 50;
            user.level = 1;
            user.heros = new List<HeroData>();

            HeroData hero = new HeroData();
            hero.attack = 10;
            hero.defence = 4;
            hero.health = 100;
            hero.star = 1;
            hero.id = 10001;
            hero.card = "CleopatraVII_s";
            hero.fight = hero.attack * hero.defence * hero.health;
            user.heros.Add(hero);

            hero = new HeroData();
            hero.attack = 10;
            hero.defence = 4;
            hero.health = 100;
            hero.star = 1;
            hero.id = 10002;
            hero.card = "KingArthur_s";
            hero.fight = hero.attack * hero.defence * hero.health;
            user.heros.Add(hero);

            hero = new HeroData();
            hero.attack = 10;
            hero.defence = 4;
            hero.health = 100;
            hero.star = 1;
            hero.id = 10003;
            hero.card = "Richard_s";
            hero.fight = hero.attack * hero.defence * hero.health;
            user.heros.Add(hero);

            hero = new HeroData();
            hero.attack = 10;
            hero.defence = 4;
            hero.health = 100;
            hero.star = 1;
            hero.id = 10004;
            hero.card = "RobinHood_s";
            hero.fight = hero.attack * hero.defence * hero.health;
            user.heros.Add(hero);

            hero = new HeroData();
            hero.attack = 10;
            hero.defence = 4;
            hero.health = 100;
            hero.star = 1;
            hero.id = 10005;
            hero.card = "Sparta_s";
            hero.fight = hero.attack * hero.defence * hero.health;
            user.heros.Add(hero);

            return user;
        }

		
		public static List<BattleFramework.Data.HeroData> InitLocalPlayerHeros()
		{
			List<BattleFramework.Data.HeroData> playerHeros = new List<BattleFramework.Data.HeroData> ();
			BattleFramework.Data.HeroData hero = new BattleFramework.Data.HeroData ();
			hero.id = 10102;
			hero.name = "RobinHood";
			hero.icon = "RobinHood_Portrait";
			hero.locationIndex = 1;
			hero.soldierLevel = 1;
			hero.soldierNum = 16;
			hero.soldierType = 1;
			playerHeros.Add (hero);

			hero = new BattleFramework.Data.HeroData ();
			hero.id = 10103;
			hero.name = "Richard";
			hero.icon = "Richard_Portrait";
			hero.locationIndex = 2;
			hero.soldierLevel = 1;
			hero.soldierNum = 16;
			hero.soldierType = 1;
			playerHeros.Add (hero);

			hero = new BattleFramework.Data.HeroData ();
			hero.id = 10104;
			hero.name = "Sparta";
			hero.icon = "Sparta_Portrait";
			hero.locationIndex = 3;
			hero.soldierLevel = 1;
			hero.soldierNum = 10;
			hero.soldierType = 1;
			playerHeros.Add (hero);	

			hero = new BattleFramework.Data.HeroData ();
			hero.id = 10105;
			hero.name = "KingArthur";
			hero.icon = "KingArthur_Portrait";
			hero.locationIndex = 4;
			hero.soldierLevel = 1;
			hero.soldierNum = 13;
			hero.soldierType = 1;
			playerHeros.Add (hero);	

			hero = new BattleFramework.Data.HeroData ();
			hero.id = 10111;
			hero.name = "KingArthur";
			hero.icon = "KingArthur_Portrait";
			hero.locationIndex = 4;
			hero.soldierLevel = 1;
			hero.soldierNum = 13;
			hero.soldierType = 5;
			playerHeros.Add (hero);	

			return playerHeros;

		}

		public static List<BattleFramework.Data.HeroData> InitLocalEnemyHeros()
		{
			return null;
		}

	}
}
