using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using Mogo.Util;
using System.Collections.Generic;

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
        AvatarModelData modelData = AvatarModelData.GetByID(data.modelId);
        gameObject = Res.ResourceManager.Instance.Instantiate<GameObject>(GameCommonUtils.GetResourceData(modelData.prefabName).resourcePath);
        
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
        if (modelData != null && modelData.scale > 0)
        {
            gameObject.transform.localScale = new Vector3(modelData.scale, modelData.scale, modelData.scale);
            if (modelData.scaleRadius > 0 && ap.GetComponent<CharacterController>()!= null)
                ap.GetComponent<CharacterController>().radius = modelData.scaleRadius;
        }
            
        animator.applyRootMotion = false;
    }
    
    protected override void OnEnterWorld()
    {
        SetOwenr(data);
        battleManager = new MonsterBattleManager(this, skillManager);
        Mogo.Util.EventDispatcher.AddEventListener<uint>(ActorEvent.ACTOR_DEAD, OnDead);
    }
    private uint deadTimeID = 0;
    public List<int> HitShader
    {
        get
        {
            return data.hitShader;
        }
    }
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
        for (int i = 0; i < data.skillIDs.Count; i++)
        {
            skillManager.AddSkill(data.skillIDs[i]);
        }
    }
    protected override void OnLeaveWorld()
    {
        skillManager.Clear();
        Mogo.Util.EventDispatcher.RemoveEventListener<uint>(ActorEvent.ACTOR_DEAD, OnDead);
        Mogo.Util.TimerHeap.DelTimer(deadTimeID);
        gameObject.SetActive(false);
    }
}
