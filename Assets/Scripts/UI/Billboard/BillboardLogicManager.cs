using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

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
    Vector3 pos;
    internal void AddSplitBattleBillboard(Vector3 vector3, int hp, SplitBattleBillboardType type)
    {
        if(bloodText == null)
        {
            bloodText = BloodText.Instance.GetComponent<Text>();
        }
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, vector3); 
        bloodText.rectTransform.anchoredPosition3D = RectTransformUtility.WorldToScreenPoint(Camera.main, vector3);
        bloodText.rectTransform.DOMoveY(pos.y + 100, 0.3f);
    }
    private void OnComplete()
    {

    }
}
