using UnityEngine;
using System.Collections;
using BattleFramework.Data;

public class EntityMyself : EntityPlayer
{
    private ResourceData data;
    private BattleManager battleManager;

    public EntityMyself()
    {
        spriteType = SpriteType.Myself;
    }
    protected override void OnCreateModel()
    {
        data = ResourceData.GetByID(serverInfo.dataId);
        if (data == null)
        {
            Debuger.LogError("数据Error null" + serverInfo.dataId);
            return;
        }
        gameObject = Res.ResourceManager.Instance.Instantiate<GameObject>(data.resourcePath);

        transform = gameObject.transform;
        transform.tag = "Player";
        transform.gameObject.layer = 11;

        ActorMyself ap = gameObject.AddComponent<ActorMyself>();
        ap.theEntity = this;
        animator = gameObject.GetComponent<Animator>();
        this.Actor = ap;
        UpdatePosition();
        animator.applyRootMotion = true;
        battleManager = gameObject.AddComponent<BattleManager>();
        gameObject.AddComponent<DontDestroyMe>();

    }
    protected override void OnEnterWorld()
    {
        GameWorld.player = this;
        skillManager.AddSkill(3);
        skillManager.AddSkill(4);
    }
    protected override void OnLeaveWorld()
    {
        skillManager.Clear();
    }
}
