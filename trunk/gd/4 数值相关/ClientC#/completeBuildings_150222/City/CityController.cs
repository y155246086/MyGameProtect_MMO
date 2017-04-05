
//CopyRight YingYuGang(232871714@qq.com) 2014-2015
using UnityEngine;
using System.Collections;
using DataMgr;
using BattleFramework.Data;

namespace BattleFramework.City
{

	public class CityController : MonoBehaviour
	{

		public MainPanel mainPanel;
		public HeroPanel heroPanel;

		public BuildingPanel buildingPanel;
		public bool isBuildingPanel = false;

		public bool isMoving = false;
		private float beginTime = 0f;
		private float overTime = 0f;
		private const float jiange = 1.0f;
		public bool isTimeing = true;
		private Vector3 getpoint;
		private Vector3 beforePosition;

		private RaycastHit hitinfoTerr;
		private Transform nowMovingObject;
				
		public BattleFramework.Data.UserData user;
				
		public Ray myray ;

		public Ray terrainRay;

		public GameObject clickGround;

		public static bool isDataCenterInited;

		private static CityController instance;

		public static CityController SingleTon()
		{
			return instance;
		}

		void Awake()
		{
			if(instance==null)
				instance = this;
			//用这种方式来初始化DontDestroyOnLoad物体和脚本
			if(!isDataCenterInited)
			{
				isDataCenterInited = true;
				GameObject go = new GameObject("DataCenter");
				DontDestroyOnLoad(go);
				go.AddComponent<DataCenter>();
			}
		}

		void Start ()
		{
			user = GetUserData ();
		}

		BattleFramework.Data.UserData GetUserData ()
		{
			return SimpleLocalData.GetUserData ();
		}

		public void BattleBegin ()
		{
			Debug.Log ("BattleBegin");
			Application.LoadLevel ("Battlefield_Gobi01");
		}
    
		public void ShowHero ()
		{
			heroPanel.gameObject.SetActive (true);
		}

		public void HideHero ()
		{
			heroPanel.gameObject.SetActive (false);
		}

		void Update ()
		{
			GetCityBuilding ();
			//开始检测  获取需要拖动游戏物体
			getpoint = Input.mousePosition;
			myray = Camera.main.ScreenPointToRay (getpoint);

			if (Physics.Raycast (myray, out hitinfoTerr, Mathf.Infinity, 1 << 16) && isTimeing) {
				//停留在这个地方开始计算时间
				if (Input.GetMouseButtonDown (0)) {   //只有检测射线在面板上才可以触发事件吧
					beginTime = Time.time;	
				}
				if (Input.GetMouseButton (0)) {
					overTime = Time.time;
				}
				if (overTime - beginTime >= jiange) {
					nowMovingObject = hitinfoTerr.transform.GetChild (0);

					if (nowMovingObject.childCount > 0) { 
						nowMovingObject = hitinfoTerr.transform.GetChild (0).GetChild (0);//获取到可以移动的对象
						beforePosition = nowMovingObject.position;
						isMoving = true;
					}
				}
			}

			if (isMoving) {  //时间间隔满足  选中物体
				isTimeing = false;
				if (Physics.Raycast (myray, out hitinfoTerr, Mathf.Infinity, 1 << 9 | 1 << 16)) {
					nowMovingObject.position = hitinfoTerr.point;
					if (Input.GetMouseButtonUp (0)) { //terr layer
						overTime = beginTime = 0.0f;
						int nowLayer = hitinfoTerr.transform.gameObject.layer;
						if (nowLayer == 16) {  //可以添加交换程序
							Transform transfomFater = hitinfoTerr.transform.GetChild (0);
							if (transfomFater.childCount == 0) {
								nowMovingObject.parent = transfomFater.transform;
								nowMovingObject.position = transfomFater.position;
							} else {
								Transform childChange = transfomFater.GetChild (0);
								childChange.parent = nowMovingObject.parent;
								childChange.position = beforePosition;
								nowMovingObject.parent = transfomFater.transform;
								nowMovingObject.position = transfomFater.position;
							}

						} else {   
							nowMovingObject.position = beforePosition;
						}
						isTimeing = true;
						isMoving = false;
					}
				}

			}

					
		}
				
		public void GetCityBuilding ()
		{
			if (Input.GetMouseButtonDown (0) && isBuildingPanel == false) {


				Vector3 getpoint = Input.mousePosition;
				RaycastHit hitinfo;
				myray = Camera.main.ScreenPointToRay (getpoint);
				
				if (Physics.Raycast (myray, out hitinfo, Mathf.Infinity, 1 << 16)) {
					clickGround = hitinfo.transform.gameObject; //get present obejct
					//判断地板的AreaCenter是否有子物体

					Transform child = clickGround.transform.GetChild (0);
					if (child.transform.childCount > 0) {
						//判断有子物体    什么时候开始交换呢

					} else {
						//得到当前点击的层物体   激活游戏面板
						buildingPanel.gameObject.SetActive (true);
						isBuildingPanel = true;

					}


				}

			}


				
		}

	}
}