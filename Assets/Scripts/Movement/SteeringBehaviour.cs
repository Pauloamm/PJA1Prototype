using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : ScriptableObject
{
    public abstract Steering GetSteering(MovementInfo npc, MovementInfo target);
}
