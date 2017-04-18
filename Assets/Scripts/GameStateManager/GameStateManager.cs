using BattleFramework.Data;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 游戏状态管理
/// </summary>
public class GameStateManager : MonoBehaviour {

    private static GameState currentState;
    private static Dictionary<string, GameState> map;
    private static GameSceneData currentSceneData;
    void Start()
    {
        map = new Dictionary<string, GameState>();
        currentState = null;
        //LoadScene(0);//游戏启动 INIT_SCENE_ID
    }
	/// <summary>
	/// 当前状态
	/// </summary>
    public static GameState CurrentState
    {
        get
        {
            return currentState;
        }
    }
    public static GameSceneData CurrentSceneData
    {
        get
        {
            return currentSceneData;
        }
    }
    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="state"></param>
    public static void SetState(GameState state)
    {
        if (state == null)
            return;
        if(state != currentState && currentState != null)
        {
            currentState.Stop();
        }
        currentState = state;
        currentState.Start();
    }
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneID"></param>
    public static void LoadScene(int sceneID)
    {
        GUIManager.HideAllView();
        GameSceneData sceneData = GameSceneData.GetByID(sceneID);
        if (sceneData == null)
        {
            Debuger.LogError("场景ID错误-" + sceneID);
            return;
        }
        string stateName = sceneData.stateName;
        GameState state = null;
        if (!map.TryGetValue(stateName, out state))
        {
            state = Assembly.GetExecutingAssembly().CreateInstance(stateName) as GameState;
            if(state == null)
            {
                Debuger.LogError("游戏状态为空" + stateName);
                return;
            }
            map.Add(stateName, state);
        }
        currentSceneData = sceneData;
        SetState(state);

        DownloadManager.Instance.LoadNormalScene(sceneData, state.LoadComplete);
    }

}
