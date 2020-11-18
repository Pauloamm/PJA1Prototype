using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoBehaviour, IRaycastResponse
{
    HingeJoint doorJoint;
    Rigidbody doorBody;
    [SerializeField]
    string requiredKey;

    [SerializeField]
    SlotManager slotManager;

    public void OnRaycastSelect()
    {
        if (!slotManager.IsStored(requiredKey)) return;

        JointLimits limits = doorJoint.limits;
        limits.min = -90;
        limits.bounciness = 0;
        limits.bounceMinVelocity = 0;
        limits.max = 1;
        doorJoint.limits = limits;
        doorJoint.useLimits = true;
        
        doorBody.constraints = RigidbodyConstraints.None;
        Destroy(this);
    }

    public void OnRaycastDiselect()
    {
       
    }


    private void Awake()
    {
        doorJoint = this.GetComponent<HingeJoint>();
        doorBody = this.GetComponent<Rigidbody>();
    }


}
