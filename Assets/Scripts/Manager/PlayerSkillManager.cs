using UnityEngine;
using System.Collections;

public class PlayerSkillManager : SkillManager {

    private float lastAttackTime = 0.0f;
    //// 公共cd 为 0.2 秒
    public int CommonCD = 200;
    private EntityMyself owner;

    public PlayerSkillManager(EntityParent owner) : base(owner)
    {
        this.owner = owner as EntityMyself;
    }
    public bool IsCommonCooldown()
    {
        int attackInterval = (int)((Time.realtimeSinceStartup - lastAttackTime) * 1000);

        if (attackInterval < CommonCD)
        {
            Debuger.Log("common cool down time");
            return true;
        }
        return false;
    }
}
