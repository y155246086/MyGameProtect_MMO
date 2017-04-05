using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

/// <summary>
/// UI管理器
/// </summary>
public class GUIManager{

    private static Dictionary<string, KeyValuePair<GameObject, IViewBase>> viewMap = new Dictionary<string, KeyValuePair<GameObject, IViewBase>>();
    private static GameObject InstantiatePanel(string prefabName)
    {
        GameObject prefab = Res.ResourceManager.Instance.GetUIPrefab(prefabName);
        if (prefab == null)
        {
            Debuger.LogError("prefab is Null-" + prefabName);
            return null;
        }

        GameObject UIPrefab = GameObject.Instantiate<GameObject>(prefab);

        Camera uiCamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
        if (uiCamera == null)
        {
            Debuger.LogError("UICamera is null");
            return null;
        }
        UIPrefab.transform.parent = null;
        UIPrefab.transform.Reset();
        return UIPrefab;
    }
    /// <summary>
    /// 显示UI
    /// </summary>
    /// <param name="name"></param>
	public static void ShowView(string name)
    {
        IViewBase view = null;
        GameObject panel = null;
        KeyValuePair<GameObject, IViewBase> found;
        if(!viewMap.TryGetValue(name,out found))
        {
            view = Assembly.GetExecutingAssembly().CreateInstance(name) as IViewBase;

            panel = InstantiatePanel(name);
            if (view == null || panel == null)
            {
                Debuger.LogError("View or Panel is null-" + name);
                return;
            }
            //UIPanel[] childPanel = panel.GetComponentsInParent<UIPanel>();
            //foreach (var item in childPanel)
            //{
            //    item.depth += (int)view.uiLayer;
            //}
            viewMap.Add(name, new KeyValuePair<GameObject, IViewBase>(panel, view));
            view.Start();
        }
        else
        {
            view = found.Value;
            panel = found.Key;
        }
        if (view == null || panel == null)
        {
            Debuger.LogError("View or Panel is null-" + name);
            return;
        }
        //把层高的都关闭掉
        foreach (var item in viewMap)
        {
            if(view.uiLayer >= item.Value.Value.uiLayer)
            {
                continue;
            }
            if(!item.Value.Key.activeSelf)
            {
                continue;
            }
            HideView(item.Key);
        }

        //UIPanel uiPanel = panel.GetComponent<UIPanel>();
        //uiPanel.alpha = 1;

        panel.SetActive(true);
        view.Show();

    }
    public static void HideView(string name)
    {

    }
    public static void DestoryAllView(string name)
    {

    }
    public static IViewBase FindView(string name)
    {
        return null;
    }
}
