using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateMachine/Transition/ToDeadState")]
public class ToDeadState : Transition
{
    [SerializeField] private PlayerHealthManager playerHealthManager;

    private const float healthLimit = 0;
  

    public override bool CanTransition()
    {
        // return playerHealthManager.GetPlayerHP <= healthLimit;
        return true;

    }
    
    
}
