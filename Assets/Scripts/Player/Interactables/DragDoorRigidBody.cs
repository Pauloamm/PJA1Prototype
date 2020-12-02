using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDoorRigidBody : MonoBehaviour
{
    public GameObject playerCam;
    
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private FlashlightOrientation flashlightOrientationScript;

    // HingeJoint doorJoint;
    // Rigidbody doorBody;

    [SerializeField] private Transform bodyToRotate;
    
    public float interactDistance = 0.3f;
    [SerializeField]private float minAngle, maxAngle, defaultAngle, nextAngle;

    private Ray playerAim;


    public bool isObjectHeld;

    private bool IsInRange =>
        Vector3.Distance(this.transform.position, playerCam.transform.position) > interactDistance;
    


    private void Awake()
    {
        isObjectHeld = false;
        // doorJoint = this.GetComponent<HingeJoint>();
        // doorBody = this.GetComponent<Rigidbody>();


        defaultAngle = this.transform.eulerAngles.y;
        maxAngle = defaultAngle + 1f;
        minAngle = defaultAngle - 1f;
        nextAngle = defaultAngle;
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
        // doorBody.useGravity = true;
        // doorBody.freezeRotation = false;
        
        playerMovementScript.enabled = false;
        flashlightOrientationScript.enabled = false;

        Transform temp = this.transform;
        
        nextAngle +=  Input.GetAxis("Mouse Y") * Time.deltaTime * 20f;

        nextAngle = Mathf.Clamp(nextAngle, minAngle, maxAngle);
        
        temp.transform.eulerAngles = new Vector3(temp.transform.eulerAngles.x, nextAngle, temp.transform.eulerAngles.z);
        // temp.Rotate(Vector3.up, (Input.GetAxis("Mouse Y") * Time.deltaTime) * 300f, Space.Self);

        Quaternion.Slerp(this.transform.rotation, temp.rotation,1f);

      
    }


    public void UnlockJoints()
    {
        minAngle = defaultAngle - 90f;
    }

    public void DropObject()
    {
        isObjectHeld = false;
        
        // doorBody.useGravity = true;
        // doorBody.freezeRotation = false;

        playerMovementScript.enabled = true;
        flashlightOrientationScript.enabled = true;
    }
}