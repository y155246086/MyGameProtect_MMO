using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

public class BillboardLogicManager{

    private static BillboardLogicManager instance;
    private Text bloodText;
    public static BillboardLogicManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new BillboardLogicManager();
            }
            return instance;
        }
    }
    internal void AddSplitBattleBillboard(Vector3 vector3, int hp, SplitBattleBillboardType type)
    {
        if(bloodText == null)
        {
            bloodText = BloodText.Instance.GetComponent<Text>();
        }
        GameObject t = GetObj();
        Text text = t.GetComponent<Text>();
        text.gameObject.SetActive(true);
        text.text = "暴击 " + hp.ToString();
        Camera uicamera = GameObject.Find("UICamera").GetComponent<Camera>();
        Vector3 pos = Camera.main.WorldToViewportPoint(vector3);
        pos = uicamera.ViewportToWorldPoint(pos);
        text.transform.position = pos;
        text.rectTransform.DOLocalMoveY(text.rectTransform.anchoredPosition3D.y + 100, 0.3f);
        text.rectTransform.DOScale(2, 0.3f).OnComplete(() => { OnComplete(text.gameObject); });
        //AppFacade.Instance.GetManager<LuaFramework.ResourceManager>(LuaFramework.ManagerName.Resource).LoadPrefab("Fx", "particle_1058_attack1_1", LoadComplete1);
    }
    private void LoadComplete1(Object[] obj)
    {
        GameObject o = GameObject.Instantiate(obj[0]) as GameObject;

       // ResetShader(o);
    }
    private List<GameObject> textList = new List<GameObject>();
    private GameObject GetObj()
    {
        GameObject textobj = null;
        if(textList.Count>0)
        {
            textobj = textList[0];
            textList.RemoveAt(0);
            return textobj;
        }
        else
        {
            textobj = GameObject.Instantiate<GameObject>(bloodText.gameObject);
            GameObject.Destroy(textobj.GetComponent<BloodText>());
            textobj.transform.SetParent(bloodText.transform.parent);
            textobj.transform.Reset();
            return textobj;
        }
    }
    private void OnComplete(GameObject obj)
    {
        obj.transform.localScale = Vector3.one;
        obj.gameObject.SetActive(false);
        textList.Add(obj);
    }
}
