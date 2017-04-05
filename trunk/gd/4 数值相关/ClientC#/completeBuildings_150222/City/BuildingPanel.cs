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
		/*
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
			"Vault_Level03"};*/

		private string[] GetBuildingsList ()
		{

			List<ExcelData.CastleBuildDesign> CBDList = ExcelData.CastleBuildDesign.LoadDatas ();
			List<ExcelData.CastleBuildingItems> CBDITEMList = ExcelData.CastleBuildingItems.LoadDatas ();
			int i = 0, j = 0;
			int CBDCOUNT = CBDList.Count, CBDITEMCOUNT = CBDITEMList.Count;
			int[] buildingsIDList = new int[CBDCOUNT];
			string[] buildingsListItem = new string[CBDCOUNT];

			if (CBDList == null) {
				Debug.Log ("fan hui zhi wei null");
			} else {
				Debug.Log ("get CBDLIST" + CBDCOUNT);
				foreach (ExcelData.CastleBuildDesign CBD in CBDList) {
					buildingsIDList [i] = CBD.castleBuildingBeginID;
					//Debug.Log (buildingsIDList [i] + "____222222222222222222222222222222222222222222222");
					i++;
				}
			}

			if (CBDList == null) {
				Debug.Log ("fan hui zhi wei null null null");
			} else {
				Debug.Log ("get CBDLISTITEM" + CBDITEMCOUNT);

				for (j=0; j<CBDCOUNT; j++) {
					foreach (ExcelData.CastleBuildingItems CBDITEM in CBDITEMList) {
						if (buildingsIDList [j] == CBDITEM.id) {
							buildingsListItem [j] = CBDITEM.buildingART;
							//Debug.Log (buildingsListItem [j] + "____222222222222222222222222222222222222222222222");
						}
					}
				}


			}
			return buildingsListItem;
		}

		protected override void Init ()
		{
			Debug.Log ("Init buildingsList");
			string[] buildingsList = GetBuildingsList ();

			Debug.Log ("Init -- data complete");
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
			string[] buildingsList = GetBuildingsList ();
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