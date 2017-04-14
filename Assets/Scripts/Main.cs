using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    private PlayerSkillManager skillManager;
    void Awake()
    {
        this.gameObject.AddComponent<MonsterManager>();
        BattleFramework.Data.DataCenter.Instance();
        Application.targetFrameRate = 60;
    }
    // Use this for initialization
    void Start()
    {
        MonsterManager.Instance.CreateMonster(1, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        Mogo.Util.TimerHeap.Tick();
        Mogo.Util.FrameTimerHeap.Tick();
    }
}
