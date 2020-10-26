using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Behaviour/Align")]
public class Align : SteeringBehaviour
{
    [SerializeField]
    float maxAngAccel = Mathf.PI / 8f, // 22.5 graus
          targetRadius = 0.0001f,
          slowRadius = Mathf.PI/4f,  // 45 graus
          timeToTarget = 5f;
    [SerializeField]
    float maxAngSpeed = Mathf.PI / 4f; // 45 graus



    override public Steering GetSteering(MovementInfo npc, MovementInfo target) {
        Steering steering = new Steering();
        
        // rotation direction from current to target orientation
        float rotation = target.orientation - npc.orientation;
        rotation = AuxMethods.NormAngle(rotation);  // garantir valor entre -PI e PI

        float rotationSize = Mathf.Abs(rotation);
        
        // Are we there yet? (Shrek, 2001)
        if (rotationSize < targetRadius) return steering;
   
        float targetSpeed;
        if (rotationSize > slowRadius) targetSpeed = maxAngSpeed;
        //    slowRadius --- maxSpeed
        //    distance   --- targetSpeed
        else targetSpeed = maxAngSpeed * rotationSize / slowRadius;

        targetSpeed *= Mathf.Sign(rotation);

        steering.angular = targetSpeed - npc.rotation;
        steering.angular /= timeToTarget;

        steering.angular = Mathf.Clamp(steering.angular, -maxAngAccel, maxAngAccel);

        return steering;
    }
}







