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
        bloodText.gameObject.SetActive(true);
        bloodText.text = "暴击 " + hp.ToString();
        Camera uicamera = GameObject.Find("UICamera").GetComponent<Camera>();
        Vector3 pos = Camera.main.WorldToViewportPoint(vector3);
        pos = uicamera.ViewportToWorldPoint(pos);
        bloodText.transform.position = pos;
        bloodText.rectTransform.DOLocalMoveY(bloodText.rectTransform.anchoredPosition3D.y + 100, 0.3f);
        bloodText.rectTransform.DOScale(2, 0.3f).OnComplete(() => { OnComplete(bloodText.gameObject); });
        //AppFacade.Instance.GetManager<LuaFramework.ResourceManager>(LuaFramework.ManagerName.Resource).LoadPrefab("Fx", "particle_1058_attack1_1", LoadComplete1);
    }
    private void LoadComplete1(Object[] obj)
    {
        GameObject o = GameObject.Instantiate(obj[0]) as GameObject;

       // ResetShader(o);
    }
    
    private void OnComplete(GameObject obj)
    {
        bloodText.transform.localScale = Vector3.one;
        bloodText.gameObject.SetActive(false);
    }
}
