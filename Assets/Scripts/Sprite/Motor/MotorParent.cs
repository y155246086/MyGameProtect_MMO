using UnityEngine;
using System.Collections;
using DG.Tweening;
public class MotorParent : MonoBehaviour {
    public EntityParent theEntity;
    public CharacterController charcater;
    protected Vector3 moveDirection = new Vector3();
    public float extraSpeed = 0;//附加速度
    public float verticalSpeed = 0.0f;
    protected CollisionFlags collisionFlags;
	// Use this for initialization
	void Start () {
        charcater = this.GetComponent<CharacterController>();
	}
	
    public void SetExrtaSpeed(float _extraSpeed)
    {
        extraSpeed = _extraSpeed;
    }
    public void SetMoveDirection(Vector3 _direction, Space space = Space.World)
    {
        _direction = _direction.normalized;

        switch (space)
        {
            case Space.Self:
                this.moveDirection = transform.TransformDirection(_direction);
                break;
            case Space.World:
                this.moveDirection = _direction;
                break;
        }
    }
    internal void TurnToControlStickDir()
    {
    }

    internal void Move()
    {
        
    }

    virtual public void TeleportTo(Vector3 destination)
    {
        transform.position = destination;
    }
}
