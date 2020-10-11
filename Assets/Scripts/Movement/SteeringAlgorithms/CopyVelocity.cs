using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyVelocity : SteeringBehaviour
{
    [SerializeField]
    public float maxAcceleration = 3f;
    [SerializeField]
    public float timeToTarget = 2f;
    [SerializeField]
    public float epsilon = 0.00001f;



    public override Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        //Se o valor de cumprimento do vetor velocidade for mais pequeno nao se faz nada
        Steering steering = new Steering();
        if (Mathf.Abs(target.orientationV2.magnitude) < epsilon)
            return steering;

        //calcula se a diferenca entre velocidades e dividi se pelo tempo pra nao ser instantaneo
        steering.linear = target.velocity - npc.velocity;
        steering.linear /= timeToTarget;


        if (steering.linear.magnitude > maxAcceleration)
        {

            steering.linear = steering.linear.normalized * maxAcceleration;
        }

        steering.angular = 0f;
        return steering;


    }
}
