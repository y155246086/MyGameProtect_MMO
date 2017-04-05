using UnityEngine;
using UnityEngine.UI;  			//UI命名空间
using UnityEngine.EventSystems;	//事件系统命名空间
using System.Collections;
using System.Collections.Generic;
using BattleFramework.Data;

public static class ExcelData
{
	/*
	public static  CastleBuildDesign GetExcelData (int id)
	{
		CastleBuildDesign excel_CastleBuildDesign = new CastleBuildDesign ();
		List<CastleBuildDesign> excel_CastleBuildDesignList = excel_CastleBuildDesign.LoadDatas ();
		foreach (CastleBuildDesign cbd in excel_CastleBuildDesignList) {
			if (cbd.id == id) {
				excel_CastleBuildDesign = cbd;
				return excel_CastleBuildDesign;
			}
		}
		return excel_CastleBuildDesign;
	}

	public static  List<CastleBuildDesign> GetExcelData ()
	{
		CastleBuildDesign excel_CastleBuildDesign = new CastleBuildDesign ();
		return excel_CastleBuildDesign.LoadDatas ();
	}

	public static class test(){

		void mytest(){


		}

	}

	public static class CastleBuildDesign {
		public static string csvFilePath = "Configs/CastleBuildDesign";
		public static List<CastleBuildDesign> LoadDatas(){
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
	*/
}
