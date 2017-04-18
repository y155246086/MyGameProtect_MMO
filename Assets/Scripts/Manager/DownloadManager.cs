using BattleFramework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加载管理器
/// </summary>
public class DownloadManager : MonoBehaviour {

    private static DownloadManager _Instance;

    public static DownloadManager Instance
    {
        get 
        {
            if(_Instance == null)
            {
                _Instance = FindObjectOfType(typeof(DownloadManager)) as DownloadManager;
            }
            return _Instance;
        }
    }
    public delegate void LoadCallBack(params object[] args);
    
    //
    //======================================================================================
    /// <summary>
    /// 加载普通的场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="startPercent"></param>
    /// <returns></returns>
    public void LoadNormalScene(int sceneId,LoadCallBack loadHandler, int loadingType = 1,params object[] args)
    {
        GameSceneData data = GameSceneData.GetByID(sceneId);
        if (data == null)
        {
            Debuger.LogError("场景ID错误-" + sceneId);
        }
        LoadNormalScene(data, loadHandler, loadingType, args);
    }
    public void LoadNormalScene(GameSceneData data, LoadCallBack loadHandler,int loadingType = 1, params object[] args)
    {
        StartCoroutine(_LoadNormalScene(data, loadHandler,  loadingType, args));
    }
    IEnumerator _LoadNormalScene(GameSceneData data, LoadCallBack loadHandler, int loadingType = 1, params object[] args)
    {
        float startPercent = 0;
        yield return new WaitForSeconds(0.1f);
        //GameObject[] sceneObject = GameObject.FindGameObjectsWithTag("scene");
        //for (int i = 0; i < sceneObject.Length; i++)
        //{
        //    sceneObject[i].SetActive(false);
        //}
        yield return new WaitForSeconds(0.1f);
        int startProgress = (int)(startPercent * 100);
        int displayProgress = startProgress;
        int toProgress = startProgress;
        AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(data.levelName);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = startProgress + (int)(op.progress * (1.0f - startPercent) * 100);
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetProgress(displayProgress);
                yield return null;
            }
            yield return null;
        }

        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetProgress(displayProgress);
            yield return null;
        }
        
        op.allowSceneActivation = true;
        yield return new WaitForSeconds(0.1f);

        if(data.ResRefreshPointsPath.Length>1)
        {
            GameObject point = Res.ResourceManager.Instance.Instantiate<GameObject>(data.ResRefreshPointsPath);
            point.transform.parent = null;
            point.transform.Reset();
        }

        yield return new WaitForSeconds(1f);


        if(loadHandler != null)
        {
            loadHandler(args);
        }
        
    }


    /// <summary>
    /// 加载场景打包的
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator LoadBundleScene(string sceneName)
    {
        string path = "";//BundleManager.GetBundleLoadPath(BundleManager.PathSceneData, sceneName + ".data");
        WWW www = new WWW(path);
        float m_BundlePercent = 0;
        UnityEngine.Object m_LastSceneBundle = null;
        //loadingText.text = "加载资源包中...";
        int displayProgress = 0;
        int toProgress = 0;
        while (!www.isDone)
        {
            toProgress = (int)(www.progress * m_BundlePercent * 100);
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetProgress(displayProgress);
                yield return null;
            }
            yield return null;
        }

        toProgress = (int)(m_BundlePercent * 100);
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetProgress(displayProgress);
            yield return null;
        }

        yield return www;
        if (null != www.assetBundle)
        {
            m_LastSceneBundle = www.assetBundle;
            //yield return StartCoroutine(_LoadNormalScene(sceneName, m_BundlePercent));//暂时先删除
        }
    }

    void SetProgress(int progress)
    {
        Mogo.Util.EventDispatcher.TriggerEvent<int>(GUIEvent.LOAD_SCENE_PROGRESS, progress);
    } 
}
