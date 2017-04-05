using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataMgr;

namespace BattleFramework.Data{

	public class CSVFileDemo : MonoBehaviour {

		CSVFile file = new CSVFile ();
		public List<HeroData> heroDatas = new List<HeroData>();
//		public List<Dict_army> achievenments ;

		public List<HeroData> LoadHeroDatas()
		{
			heroDatas = new List<HeroData>();
			file.Open(CSVFileConfigs.dict_hero);
			List<Row> data = file.mapData;
			foreach(Row row in data)
			{
				HeroData heroData = new HeroData();
				row.GetIntValue(DICT_HERO.HERO_TYPEID,out heroData.id);
				heroDatas.Add(heroData);
			}
			return heroDatas;
		}


		void Update()
		{
			if(Input.GetMouseButtonDown(0))
			{
//				Dict_army achievenment = new Dict_army();
//				achievenments = achievenment.LoadDatas();
			}
		}

	}
}
