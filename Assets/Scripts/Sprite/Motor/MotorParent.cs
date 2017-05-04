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
    public float speed = 0.0f;
	// Use this for initialization
	void Start () {
        charcater = this.GetComponent<CharacterController>();
	}
    public virtual void SetSpeed(float _speed)
    {
        this.speed = _speed;
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
    public bool isNeedRotation = true;
    protected Transform targetToLookAtTransform;
    protected bool isLookingAtTarget = false;
    protected Vector3 targetToLookAt;
    public bool enableStick = false;
    public void SetTargetToLookAt(Transform t)
    {
        isNeedRotation = true;
        targetToLookAtTransform = t;
        isLookingAtTarget = true;
    }
    
    public void SetTargetToLookAt(Vector3 _targetToLookAt)
    {
        targetToLookAt = _targetToLookAt;
        targetToLookAtTransform = null;
        isLookingAtTarget = true;
        
    }

    public void CancleLookAtTarget()
    {
        isLookingAtTarget = false;
    }
}
