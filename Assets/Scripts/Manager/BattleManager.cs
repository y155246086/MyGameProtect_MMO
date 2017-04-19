using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {

    private Animator anim;
    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }
	void Start () {
	}
	
	void Update () {
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
        Player ai = GameObject.FindObjectOfType<Player>();
        if(ai != null && ai.skillManager != null)
        {
            ai.skillManager.UseSkill(id);
        }
    }
    public void SpellOneAttack()
    {

    }
    public void SpellTwoAttack()
    {

    }
    
}
