using UnityEngine;
using System.Collections;
using BattleFramework.Data;

public class EntityMonster :  EntityParent {

    private MonsterData data = null;
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
        UpdatePosition();
        if (data.scale > 0)
            gameObject.transform.localScale = new Vector3(data.scale, data.scale, data.scale);
        animator.applyRootMotion = false;
    }
    
    protected override void OnEnterWorld()
    {
        SetOwenr(data);
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
    }
}
