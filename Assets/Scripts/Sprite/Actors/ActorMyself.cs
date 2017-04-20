using UnityEngine;
using System.Collections;

public class ActorMyself : ActorPlayer<EntityMyself>
{
    void LateUpdate()
    {
        float speed = 0;
        if ((ETCInput.GetAxis("Vertical") != 0 || ETCInput.GetAxis("Horizontal") != 0))
        {
            speed = Mathf.Max(Mathf.Abs(ETCInput.GetAxis("Vertical")), Mathf.Abs(ETCInput.GetAxis("Horizontal")));
        }
        this.theEntity.animator.SetFloat("Speed", speed);
        if (ETCInput.GetButtonDown("ButtonAttack"))
        {
            NormalAttack(3);
        }
        if (ETCInput.GetButtonDown("ButtonAttack1"))
        {
            NormalAttack(4);
        }
        if (ETCInput.GetButtonDown("ButtonAttack2"))
        {
            NormalAttack(3);
        }
    }
    public void NormalAttack(int id)
    {
        GetEntity().skillManager.UseSkill(id);
    }
}
