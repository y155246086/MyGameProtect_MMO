using UnityEngine;
using System.Collections;

public class ActorMonster : ActorParent<EntityMonster> {
    protected override void OnUpdate()
    {
        base.OnUpdate();
        ActChange();
    }
}
