using UnityEngine;
using System.Collections;
using DG.Tweening;
public class MotorParent : MonoBehaviour {
    public EntityParent theEntity;
    public CharacterController charcater;
	// Use this for initialization
	void Start () {
        charcater = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void TurnToControlStickDir()
    {
    }

    internal void Move()
    {
        
    }
}
