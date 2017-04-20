using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {

    void Awake()
    {
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

    }
    public void SpellOneAttack()
    {

    }
    public void SpellTwoAttack()
    {

    }
    
}
