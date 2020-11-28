using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDoorRigidBody : MonoBehaviour
{
    public GameObject playerCam;
    
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private FlashlightOrientation flashlightOrientationScript;

    HingeJoint doorJoint;
    Rigidbody doorBody;

    [SerializeField] private Transform bodyToRotate;
    
    public float interactDistance = 0.3f;

    private Ray playerAim;


    public bool isObjectHeld;

    private bool IsInRange =>
        Vector3.Distance(this.transform.position, playerCam.transform.position) > interactDistance;
    


    private void Awake()
    {
        isObjectHeld = false;
        doorJoint = this.GetComponent<HingeJoint>();
        doorBody = this.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if(!isObjectHeld) return;
        
        HoldObject();
        
        // Checks if stops interacting
        if (Input.GetKeyUp(KeyCode.E) || !IsInRange)
            DropObject();

    }


    public void HoldObject()
    {
        doorBody.useGravity = true;
        doorBody.freezeRotation = false;
        
        
        playerMovementScript.enabled = false;
        flashlightOrientationScript.enabled = false;

        Transform temp = this.transform;

        temp.Rotate(Vector3.up, (Input.GetAxis("Mouse Y") * Time.deltaTime) * 300f, Space.Self);

        Quaternion.Slerp(this.transform.rotation, temp.rotation, 1f);

      
    }


    public void UnlockJoints()
    {
        JointLimits limits = doorJoint.limits;
        limits.min = -90;
        limits.bounciness = 0;
        limits.bounceMinVelocity = 0;
        limits.max = 1;
        doorJoint.limits = limits;
        doorJoint.useLimits = true;
        doorBody.constraints = RigidbodyConstraints.None;
    }

    public void DropObject()
    {
        isObjectHeld = false;
        
        doorBody.useGravity = true;
        doorBody.freezeRotation = false;

        playerMovementScript.enabled = true;
        flashlightOrientationScript.enabled = true;
    }
}