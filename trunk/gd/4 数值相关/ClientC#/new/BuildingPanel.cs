using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleFramework.Data;

namespace BattleFramework.City
{
	public class BuildingPanel : BasePanel
	{

		public UIButton BuildingUIreturn;
		public UIGrid buildingGrid;
		public BuildingItem buildingItemPrebfa;
		public List<BuildingItem> buildingItems;
	
		public CastleBuildDesign castleBuildDesign;
		public List<CastleBuildDesign> castleBuildDesignList = castleBuildDesign.LoadDatas () ;

		private string[] buildingsList = {  //public 属性会加入到缓存中去
			"AllianceTower_Level03",
			"AoruoMiracle_Level03",
			"Barracks_Level03",
			"Billboard_Level03",
			"Cellar_Level03",
			"Gate_Level03",
			"Mint_Level03",
			"Monument_Level03",
			"OleStone_Level03",
			"Smithy_Level03",
			"Tavern_Level03",
			"Vault_Level03"};
		private void InitBuildingsList ()
		{
			foreach (CastleBuildDesign cbd in castleBuildDesignList) {


			}
		}


		protected override void Init ()
		{
			Debug.Log ("Init");
			BuildingUIreturn.onClick.Add (new EventDelegate (CloseBuildingUIreturn)); //传入方法执行点击事件

			BuildingItem item;
			for (int i=0; i<buildingsList.Length; i++) {
				GameObject go = Instantiate (buildingItemPrebfa.gameObject) as GameObject;
				go.transform.parent = buildingGrid.transform;
				go.transform.localScale = Vector3.one;
				item = go.GetComponent<BuildingItem> ();
				item.buildingPanel = this;
				item.buildName = buildingsList [i];
				item.buildImage.spriteName = item.buildName + "_Render";
				buildingItems.Add (item);
			}
			buildingGrid.Reposition ();
		}

		private void CloseBuildingUIreturn ()
		{
			this.gameObject.SetActive (false);
			mCity.isBuildingPanel = false;
		}

		public  void CreateBuilding (string name)
		{
			//begin building 
			Transform AreaCenter = mCity.clickGround.transform.GetChild (0);

			if (AreaCenter.childCount == 0) {
				Vector3 GroundPosition = AreaCenter.position; //得到ground世界坐标 
				int index = Random.Range (0, buildingsList.Length);
				string source = "Prefabs/Buildings/" + name;
				Object ogo = Resources.Load (source, typeof(Object));
				GameObject go = Instantiate (ogo) as GameObject;  //实例化
				
				go.transform.position = GroundPosition;
				go.transform.parent = AreaCenter;

				CloseBuildingUIreturn ();//关闭面板

			} else {
				print ("this is having a child");
			}

		}

	}
}