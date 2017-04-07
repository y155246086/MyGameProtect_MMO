using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
namespace BattleFramework.Data
{
    public class DataCenter : MonoBehaviour
    {
        static DataCenter instance;
  
        public List<AIData> list_AIData;
        public List<CameraEffectData> list_CameraEffectData;
        public List<CameraStateData> list_CameraStateData;
        public List<CreateAIData> list_CreateAIData;
        public List<EffectData> list_EffectData;
        public List<EventData> list_EventData;
        public List<GameBaseData> list_GameBaseData;
        public List<GameSceneData> list_GameSceneData;
        public List<GoodsData> list_GoodsData;
        public List<ImportAnimationAddClipData> list_ImportAnimationAddClipData;
        public List<ImportAnimationAddEventData> list_ImportAnimationAddEventData;
        public List<MonsterData> list_MonsterData;
        public List<NPCData> list_NPCData;
        public List<OperationData> list_OperationData;
        public List<PlayerData> list_PlayerData;
        public List<SceneClipData> list_SceneClipData;
        public List<SceneData> list_SceneData;
        public List<SceneLevelData> list_SceneLevelData;
        public List<SceneStaticData> list_SceneStaticData;
        public List<SkillData> list_SkillData;
        public List<StoryData> list_StoryData;
        public List<TriggerData> list_TriggerData;
        public List<WaveData> list_WaveData;
        public List<WeaponData> list_WeaponData;
  
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
            list_AIData = AIData.LoadDatas ();
            list_CameraEffectData = CameraEffectData.LoadDatas ();
            list_CameraStateData = CameraStateData.LoadDatas ();
            list_CreateAIData = CreateAIData.LoadDatas ();
            list_EffectData = EffectData.LoadDatas ();
            list_EventData = EventData.LoadDatas ();
            list_GameBaseData = GameBaseData.LoadDatas ();
            list_GameSceneData = GameSceneData.LoadDatas ();
            list_GoodsData = GoodsData.LoadDatas ();
            list_ImportAnimationAddClipData = ImportAnimationAddClipData.LoadDatas ();
            list_ImportAnimationAddEventData = ImportAnimationAddEventData.LoadDatas ();
            list_MonsterData = MonsterData.LoadDatas ();
            list_NPCData = NPCData.LoadDatas ();
            list_OperationData = OperationData.LoadDatas ();
            list_PlayerData = PlayerData.LoadDatas ();
            list_SceneClipData = SceneClipData.LoadDatas ();
            list_SceneData = SceneData.LoadDatas ();
            list_SceneLevelData = SceneLevelData.LoadDatas ();
            list_SceneStaticData = SceneStaticData.LoadDatas ();
            list_SkillData = SkillData.LoadDatas ();
            list_StoryData = StoryData.LoadDatas ();
            list_TriggerData = TriggerData.LoadDatas ();
            list_WaveData = WaveData.LoadDatas ();
            list_WeaponData = WeaponData.LoadDatas ();
        }
 
 
    }
}
