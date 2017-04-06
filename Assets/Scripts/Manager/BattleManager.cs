using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {

    void Awake()
    {
        this.gameObject.AddComponent<MonsterManager>();
        DataCenter.Instance().Init();
    }
	// Use this for initialization
	void Start () {
        MonsterManager.Instance.CreateMonster(1, Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {
        Mogo.Util.TimerHeap.Tick();
        Mogo.Util.FrameTimerHeap.Tick();
	}
}
