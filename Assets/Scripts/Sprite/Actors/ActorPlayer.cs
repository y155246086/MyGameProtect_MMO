using UnityEngine;
using System.Collections;

public class ActorPlayer : ActorPlayer<EntityPlayer> { }

public class ActorPlayer<T> : ActorParent<T> where T : EntityPlayer{

}
