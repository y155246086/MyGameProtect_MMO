using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
//if you want to update this,please go to CreateClassFromCSV.cs
namespace BattleFramework.Data
{
    public class DataCenter : MonoBehaviour
    {
        static DataCenter instance;
  
        public List<AvatarModelData> list_AvatarModelData;
        public List<CameraAnimData> list_CameraAnimData;
        public List<EffectData> list_EffectData;
        public List<EquipData> list_EquipData;
        public List<GameBaseData> list_GameBaseData;
        public List<GameSceneData> list_GameSceneData;
        public List<GoodsData> list_GoodsData;
        public List<MonsterData> list_MonsterData;
        public List<ResourceData> list_ResourceData;
        public List<SkillAction> list_SkillAction;
        public List<SkillData> list_SkillData;
  
        public static DataCenter Instance ()
        {
            if (instance == null) {
                Debug.Log ("new _DataCenter");
                GameObject go = new GameObject ("_DataCenter");
                DataCenter dataCenter = go.AddComponent<DataCenter> ();
                dataCenter.LoadCSV ();
                DontDestroyOnLoad (go);
                instance = dataCenter;
            }
            return instance;
        }
   
   
        public void LoadCSV ()
        {
            list_AvatarModelData = AvatarModelData.LoadDatas ();
            list_CameraAnimData = CameraAnimData.LoadDatas ();
            list_EffectData = EffectData.LoadDatas ();
            list_EquipData = EquipData.LoadDatas ();
            list_GameBaseData = GameBaseData.LoadDatas ();
            list_GameSceneData = GameSceneData.LoadDatas ();
            list_GoodsData = GoodsData.LoadDatas ();
            list_MonsterData = MonsterData.LoadDatas ();
            list_ResourceData = ResourceData.LoadDatas ();
            list_SkillAction = SkillAction.LoadDatas ();
            list_SkillData = SkillData.LoadDatas ();
        }
 
 
    }
}
