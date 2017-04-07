using UnityEngine;
using System.Collections;

public class MogoCameraLighting : MonoBehaviour
{
    //TweenAlpha m_ta;
    //UISprite m_spLighting;

    void Awake()
    {
       // m_ta = GameObject.Find("BillboardPanel").transform.FindChild("CGCameraLighting").GetComponentsInChildren<TweenAlpha>(true)[0];
       // m_spLighting = m_ta.transform.parent.GetComponentsInChildren<UISprite>(true)[0];
    }

    void OnTAEnd()
    {
        //m_ta.gameObject.SetActive(false);
    }


    public void SetCenter(Vector2 vec2)
    {

    }

    public void SetControll(float controll)
    {
    }

    public void SetLastTime(float time)
    {
        //m_ta.duration = time;
    }

    public void ResetToNoneWhite()
    {
        //m_spLighting.color = new Color(1, 1, 1, 0);
    }

    public void ResetToAllWhite()
    {
       // m_spLighting.color = new Color(1, 1, 1, 1);
    }

    public void SetFadeColor(Color color)
    {
        //m_spLighting.color = color;
    }

    public void StartFade(bool forward)
    {
        //m_ta.Reset();

        //m_ta.gameObject.SetActive(true);

        //if (forward)
        //{
        //    m_spLighting.color = new Color(1, 1, 1, 0);
        //    m_ta.from = 0;
        //    m_ta.to = 1;

        //    m_ta.eventReceiver = null;
        //}
        //else
        //{
        //    m_spLighting.color = new Color(1, 1, 1, 1);
        //    m_ta.from = 1;
        //    m_ta.to = 0;

        //    m_ta.eventReceiver = gameObject;
        //    m_ta.callWhenFinished = "OnTAEnd";
        //}

        //m_ta.Play(true);
    }

}
