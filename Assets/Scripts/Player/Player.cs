using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICanAttacked {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetHurt(int value)
    {
        Debuger.Log("产生伤害：" + value);
    }
}
