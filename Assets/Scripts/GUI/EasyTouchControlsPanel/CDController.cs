using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CDController : MonoBehaviour {
    private Image skillMask;
    private Text cdText;
    private int skillID;
    private PlayerSkillManager skillManager;
    private bool isRun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(isRun == false)
        {
            return;
        }
        if(skillMask == false)
        {
            return;
        }
        skillMask.gameObject.SetActive(true);
        float coolTime = (float)skillManager.GetCurrentCoolTime(skillID);
        float cd = (float)skillManager.GetSkillCD(skillID);
        float fillAmount = 0;
        if(cd<=0)
        {
            fillAmount = 0;
        }
        else
        {
            fillAmount = coolTime / cd;
        }
        skillMask.fillAmount = fillAmount;
        if (coolTime<=0)
        {
            cdText.text =  "";
        }
        else
        {
            cdText.text = (float)((int)(coolTime / 100f)) / 10f + "";
        }
        
	}

    private void Reset()
    {
        if (skillMask != null)
        {
            skillMask.gameObject.SetActive(false);
            cdText.text = "";
        }
    }
    public void SetCDContent(Image skillMask,Text cdText,int skillID,PlayerSkillManager skillManager)
    {
        this.skillMask = skillMask;
        this.cdText = cdText;
        this.skillID = skillID;
        this.skillManager = skillManager;
        Reset();
    }
    public bool IsRun
    {
        get
        {
            return isRun;
        }
        set
        {
            isRun = value;
            if(isRun == false)
            {
                Reset();
            }
        }
    }
}
