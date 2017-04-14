using UnityEngine;
using System.Collections;

public class Player : SpriteBase, ICanAttacked {

    public SkillManager skillManager;
    protected override void Initialize()
    {
        base.Initialize();
        skillManager = this.gameObject.AddComponent<SkillManager>();
        skillManager.SetOwenr(this);
        skillManager.AddSkill(3);
        skillManager.AddSkill(4);
        GameWorld.player = this;
    }
    public void SetHurt(int value)
    {
        Debuger.Log("产生伤害：" + value);
    }
}
