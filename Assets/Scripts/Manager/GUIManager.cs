using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

/// <summary>
/// UI管理器
/// </summary>
public class GUIManager{

    private static Dictionary<string, KeyValuePair<GameObject, IViewBase>> viewMap = new Dictionary<string, KeyValuePair<GameObject, IViewBase>>();
    private static List<IViewBase> layerList = new List<IViewBase>();
    private static GameObject InstantiatePanel(string prefabName)
    {
        GameObject prefab = Res.ResourceManager.Instance.GetUIPrefab(prefabName);
        if (prefab == null)
        {
            Debuger.LogError("prefab is Null-" + prefabName);
            return null;
        }

        GameObject UIPrefab = GameObject.Instantiate<GameObject>(prefab);
        return UIPrefab;
    }
    private static Transform panelParent;
    /// <summary>
    /// 显示UI
    /// </summary>
    /// <param name="name"></param>
    public static void ShowView(string name, params object[] args)
    {
        Debuger.Log("GUIManager.ShowView---" + name);
        IViewBase view = null;
        GameObject panel = null;
        KeyValuePair<GameObject, IViewBase> found;
        if(!viewMap.TryGetValue(name,out found))
        {
            System.Object o = Assembly.GetExecutingAssembly().CreateInstance(name);
            view = o as IViewBase;
            if(o != null && view == null)
            {
                Debuger.LogError("View is must extends IViewBase");
                return;
            }
            panel = InstantiatePanel(name);
            if (view == null || panel == null)
            {
                Debuger.LogError("View or Panel is null-" + name);
                return;
            }
            if(panelParent == null)
            {
                panelParent = GameObject.Find("GUICanvas").transform.Find("Panels");
            }
            if(panelParent != null)
            {
                panel.transform.SetParent(panelParent);
                panel.transform.Reset();
            }
            viewMap.Add(name, new KeyValuePair<GameObject, IViewBase>(panel, view));
            view.panelObj = panel;
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
        UpdateIViewLayer(view);
        view.Show(args);

    }
    private static void UpdateIViewLayer(IViewBase view)
    {
        if(layerList.IndexOf(view)>-1)
        {
            layerList.Remove(view);
        }
        if(layerList.Count == 0)
        {
            layerList.Add(view);
        }
        else
        {
            for (int i = 0; i < layerList.Count; i++)
            {
                if(view.uiLayer<layerList[i].uiLayer)
                {
                    layerList.Insert(i, view);
                    break;
                }
            }
        }
        for (int i = 0; i < layerList.Count; i++)
        {
            layerList[i].panelObj.transform.SetSiblingIndex(i);
        }
    }
    public static void HideView(string name)
    {
        Debuger.Log("GUIManager.HideView---" + name);
        KeyValuePair<GameObject, IViewBase> found;
        if(viewMap.TryGetValue(name, out found))
        {
            found.Value.Hide();
        }
    }
    public static void DestoryAllView(string name)
    {

    }
    public static IViewBase FindView(string name)
    {
        KeyValuePair<GameObject, IViewBase> found;
        if (viewMap.TryGetValue(name, out found))
        {
            return found.Value;
        }
        return null;
    }

    public static void HideAllView()
    {
        foreach (var item in viewMap)
        {
            item.Value.Value.Hide();
        }
    }
}
