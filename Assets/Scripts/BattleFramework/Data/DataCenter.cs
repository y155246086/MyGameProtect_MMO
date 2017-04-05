using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleFramework.Data;

public class DataCenter : SingleTon<DataCenter>
{

    //static DataCenter instance;

    
    private bool isInit = false;
    public List<GoodsData> goodsDatas;
    public List<TriggerData> triggerDataList;
    public List<MonsterData> monsterDataList;
    public List<SkillData> skillDataList;
    public List<WaveData> waveDataList;
    public List<SceneLevelData> sceneLevelDataList;
    public GameBaseData baseData;
    public List<StoryData> storyDataList;
    public List<SceneClipData> sceneClipDataList;
    public List<SceneData> sceneDataList;
    public List<SceneStaticData> sceneStaticDataList;
    public List<OperationData> operationDataList;
    public List<NPCData> npcDataList;
    public List<EventData> eventDataList;
	public List<AIData> aiDataList;
    public List<CreateAIData> createAIDataList;
    public List<CameraStateData> cameraStateList;
    public List<CameraEffectData> cameraEffectList;
    public List<WeaponData> weaponDataList;
    public List<GameSceneData> gameSceneDataList;
    public void Init()
    {
        if (isInit == true)
        {
            return;
        }
        Debug.Log("DataCenter:初始化数据开始");
        goodsDatas = GoodsData.LoadDatas();
        triggerDataList = TriggerData.LoadDatas();
        monsterDataList = MonsterData.LoadDatas();
        skillDataList = SkillData.LoadDatas();
        sceneLevelDataList = SceneLevelData.LoadDatas();
        waveDataList = WaveData.LoadDatas();
        baseData = GameBaseData.LoadDatas()[0];
        storyDataList = StoryData.LoadDatas();
        sceneClipDataList = SceneClipData.LoadDatas();
        sceneDataList = SceneData.LoadDatas();
        sceneStaticDataList = SceneStaticData.LoadDatas();
        operationDataList = OperationData.LoadDatas();
        eventDataList = EventData.LoadDatas();
		npcDataList = NPCData.LoadDatas();
        aiDataList = AIData.LoadDatas();
        createAIDataList = CreateAIData.LoadDatas();
        cameraStateList = CameraStateData.LoadDatas();
        cameraEffectList = CameraEffectData.LoadDatas();
        weaponDataList = WeaponData.LoadDatas();
        gameSceneDataList = GameSceneData.LoadDatas();
        isInit = true;
        Debug.Log("DataCenter:初始化数据结束");
    }
}

