using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using Mogo.Util;

public class EntityMonster :  EntityParent {

    private MonsterData data = null;
    public EntityMonster()
    {
        spriteType = SpriteType.Monster;
    }
    protected override void OnCreateModel()
    {
        data = MonsterData.GetByID(serverInfo.dataId);
        if(data == null)
        {
            Debuger.LogError("怪物数据Error null" + serverInfo.dataId);
            return;
        }
        gameObject = Res.ResourceManager.Instance.Instantiate<GameObject>(data.PrefabsPath);
        
        transform = gameObject.transform;
        transform.tag = "Monster";
        transform.gameObject.layer = 11;

        ActorMonster ap = gameObject.AddComponent<ActorMonster>();
        ap.theEntity = this;
        animator = gameObject.GetComponent<Animator>();
        this.Actor = ap;

        this.Motor = gameObject.AddComponent<MotorParent>();
        this.Motor.theEntity = this;
        UpdatePosition();
        if (data.scale > 0)
            gameObject.transform.localScale = new Vector3(data.scale, data.scale, data.scale);
        animator.applyRootMotion = false;
    }
    
    protected override void OnEnterWorld()
    {
        SetOwenr(data);
        Mogo.Util.EventDispatcher.AddEventListener<uint>(ActorEvent.ACTOR_DEAD, OnDead);
    }
    private uint deadTimeID = 0;
    private void OnDead(uint obj)
    {
        if(obj == ID)
        {
            deadTimeID = TimerHeap.AddTimer((uint)(10 * 1000f), 0, _OnDead);
        }
    }

    private void _OnDead()
    {
        GameWorld.RemoveEntity(ID);
    }
    public void SetOwenr(MonsterData data)
    {
        if (data.skillID1 > 0)
            skillManager.AddSkill(data.skillID1);
        if (data.skillID2 > 0)
            skillManager.AddSkill(data.skillID2);
        if (data.skillID3 > 0)
            skillManager.AddSkill(data.skillID3);
    }
    protected override void OnLeaveWorld()
    {
        skillManager.Clear();
        Mogo.Util.EventDispatcher.RemoveEventListener<uint>(ActorEvent.ACTOR_DEAD, OnDead);
        Mogo.Util.TimerHeap.DelTimer(deadTimeID);
        gameObject.SetActive(false);
    }
}
