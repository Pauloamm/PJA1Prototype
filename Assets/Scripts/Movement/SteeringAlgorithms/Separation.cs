using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName="Behaviour/Separation")]

public class Separation : SteeringBehaviour
{
    private List<MovementInfo> targetList;
   
    public float threshold = 3, k, maxAccell;
    
    private void OnEnable()
    {
        targetList = GameObject
            .FindGameObjectsWithTag("NPC")
            .Select(x => x.GetComponent<MovementManager>().GetInfo).ToList();
    }

    public override Steering GetSteering(MovementInfo npc, MovementInfo ignoredTarget)
    {
        Steering steering = new Steering();
        foreach (MovementInfo target in targetList)
        {
            Vector3 direction = npc.position - target.position;
            float distanceSqr = direction.sqrMagnitude;
            if (distanceSqr < threshold * threshold)
            {
                float strength = Mathf.Min(maxAccell, k / distanceSqr);
                steering.linear += direction.normalized * strength;
            }
        }
        return steering;
    }
}
