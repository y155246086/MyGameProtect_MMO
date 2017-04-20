using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    void Awake()
    {
        BattleFramework.Data.DataCenter.Instance();
        Application.targetFrameRate = 60;
    }
    // Use this for initialization
    void Start()
    {
        AppFacade.Instance.StartUp();   //启动游戏
        this.gameObject.AddComponent<GameStateManager>();
        this.gameObject.AddComponent<DownloadManager>();
        //
    }

    // Update is called once per frame
    void Update()
    {
        Mogo.Util.TimerHeap.Tick();
        Mogo.Util.FrameTimerHeap.Tick();
    }
}
