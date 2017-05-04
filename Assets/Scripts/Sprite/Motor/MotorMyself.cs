using UnityEngine;
using System.Collections;

public class MotorMyself : MotorParent {

    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (extraSpeed != 0 || verticalSpeed != 0)
            collisionFlags = charcater.Move((extraSpeed * moveDirection + new Vector3(0, verticalSpeed, 0)) * Time.deltaTime);
    }
    public bool CanMoveTo(Vector3 position)
    {
        Vector3 save = transform.position;
        Vector3 movement = position - save;

        Physics.IgnoreLayerCollision(8, 11, true);
        charcater.Move(movement);


        Vector3 d = transform.position - position;
        transform.position = save;
        Physics.IgnoreLayerCollision(8, 11, false);

        if (d.magnitude > 0.3f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public override void TeleportTo(Vector3 destination)
    {
        Vector3 movement = destination - transform.position;
        collisionFlags = charcater.Move(movement);
    }
}
