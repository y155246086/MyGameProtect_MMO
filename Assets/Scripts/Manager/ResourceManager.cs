using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Res
{
    public class ResourceManager : MonoBehaviour
    {

        private static ResourceManager instance;
        private bool isInited = false;

        public static ResourceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("ResouceManager").AddComponent<ResourceManager>();
                }
                return instance;
            }
        }

        void Awake()
        {
            if (isInited)
            {
                return;
            }
            isInited = true;
            if (instance != null)
            {
                Destroy(instance.gameObject);
                instance = null;
            }
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }



        public T Instantiate<T>(string resourcePath) where T : UnityEngine.Object
        {
            if (resourcePath.Length == 1)
            {
                Debug.LogError("BBBB");
            }
            return (GameObject.Instantiate<UnityEngine.Object>(Resources.Load<UnityEngine.Object>(resourcePath)) as T);
        }
        /// <summary>
        /// 实例化资源
        /// </summary>
        /// <param name="assetName">资源名称</param>
        public void InstantiateAsset(string assetName)
        {
            //GameObject.Instantiate(resourceDic[assetName]);
        }
        private string uiPanelPath = "GUI/Panel";
        public GameObject GetUIPrefab(string name)
        {
            return LoadPrefab(name, uiPanelPath);
        }
        public GameObject LoadPrefab(string name, string path)
        {
            string loadPath = path + "/" + name;
            GameObject prefab = Resources.Load<GameObject>(loadPath);

            if (prefab == null)
            {
                Debuger.LogError("加载的资源没有找到:" + loadPath);
            }
            return prefab;
        }
        //AppFacade.Instance.GetManager<LuaFramework.ResourceManager>(LuaFramework.ManagerName.Resource).LoadPrefab("Fx", "fx_die_x_1", LoadComplete1);
    }
}
