using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRechargeable
{
    int RemainingCharges { get; set; }
    bool canBeUsed { get; set; }
    void Recharge();
   
}
