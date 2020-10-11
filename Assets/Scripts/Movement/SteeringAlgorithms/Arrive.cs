using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Behaviour/Arrive")]
public class Arrive : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f, slowRadius = 1.5f, targetRadius = 0.1f, timeToTarget = 5f;
    [SerializeField]
    float maxSpeed = 10f;

    override public Steering GetSteering(MovementInfo npc, MovementInfo target) {
        Steering steering = new Steering();
        // vector from npc.position to target.position
        Vector3 direction = target.position - npc.position;
        // distance to target
        float distance = direction.magnitude;
        
        // Are we there yet? (Shrek, 2001)
        if (distance < targetRadius) return steering;
        float targetSpeed;
        if (distance > slowRadius) targetSpeed = maxSpeed;
        //    slowRadius --- maxSpeed
        //    distance   --- targetSpeed
        else targetSpeed = maxSpeed * distance / slowRadius;

        Vector3 targetVelocity = direction.normalized;
        targetVelocity *= targetSpeed;
        steering.linear = targetVelocity - npc.velocity;
        steering.linear /= timeToTarget;

        steering.linear = Vector3.ClampMagnitude(steering.linear, maxAccel);

        return steering;
    }
}







