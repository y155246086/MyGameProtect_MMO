using UnityEngine;
using System.Collections;

public class ActorMyself : ActorPlayer<EntityMyself>
{
    [System.NonSerialized]
    public bool enableStick = true;
    protected override void OnUpdate()
    {
        base.OnUpdate();
        ActChange();
    }
    void LateUpdate()
    {
        float speed = 0;
        if ((ETCInput.GetAxis("Vertical") != 0 || ETCInput.GetAxis("Horizontal") != 0))
        {
            speed = Mathf.Max(Mathf.Abs(ETCInput.GetAxis("Vertical")), Mathf.Abs(ETCInput.GetAxis("Horizontal")));
        }
        if(enableStick == true)
        {
            this.theEntity.animator.SetFloat("Speed", speed);
            if (this.theEntity.character)
            {
                this.theEntity.character.SimpleMove(this.transform.forward * Mathf.Abs(speed) * 4);
            }
        }
        else
        {
            this.theEntity.animator.SetFloat("Speed", 0);
        }
        
        if (ETCInput.GetButtonDown("ButtonAttack"))
        {
            NormalAttack(3);
        }
        if (ETCInput.GetButtonDown("ButtonAttack1"))
        {
            (GetEntity().battleManager as PlayerBattleManager).SpellOneAttack();
        }
        if (ETCInput.GetButtonDown("ButtonAttack2"))
        {
            (GetEntity().battleManager as PlayerBattleManager).SpellTwoAttack();
        }
    }
    public void NormalAttack(int id)
    {
       // GetEntity().skillManager.UseSkill(id);
        (GetEntity().battleManager as PlayerBattleManager).NormalAttack();
    }
}
