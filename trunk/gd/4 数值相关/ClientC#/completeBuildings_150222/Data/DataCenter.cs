using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BattleFramework.Data{
	public class DataCenter : MonoBehaviour {

	    public static DataCenter instance;

		public static bool isInited;

		public List<HeroData> heroDatas;


		void Awake()
		{
			if (instance == null)
				instance = this;
			else
				Debug.LogError ("There is multiple DataCenter in the scene!");
		}

		public void LoadCSV()
		{
			heroDatas = LoadCSVHeroDatas ();
		}

		public List<HeroData> LoadCSVHeroDatas()
		{
			CSVFile file = new CSVFile ();
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


	}
}
