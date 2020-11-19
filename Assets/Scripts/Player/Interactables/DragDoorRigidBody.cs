using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDoorRigidBody : MonoBehaviour
{

    public GameObject playerCam;

    HingeJoint doorJoint;
    Rigidbody doorBody;

    //private float PickupRange = 0.3f;
    //private float ThrowStrength = 50f;
    //private float distance = 0.3f;
    //private float maxDistanceGrab = 0.3f;

    public float m_DoorMaxGrab = 0.3f;

    private Ray playerAim;
    public bool isObjectHeld;
    private bool tryPickupObject;

    //TESTE
    [SerializeField]
    PlayerMovement playerMovement;

    private void Awake()
    {
        isObjectHeld = false;
        doorJoint = this.GetComponent<HingeJoint>();
        doorBody = this.GetComponent<Rigidbody>();
    }



    public void IsBeingDragged()
    {
        Debug.Log(isObjectHeld);
        if (!isObjectHeld)
        {
            Debug.Log("entrou");
            TryPickObject();
        }
        else
        {
            HoldObject();
        }
    }

    public void TryPickObject()
    {
        isObjectHeld = true;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().freezeRotation = false;
    }

    public void HoldObject()
    {
        Transform Temp;

        Temp = this.transform;

        Temp.Rotate(Vector3.up, (Input.GetAxis("Mouse Y") * Time.deltaTime) * 300f, Space.Self);

        Quaternion.Slerp(this.transform.rotation, Temp.rotation, 1f);

        if (Vector3.Distance(this.transform.position, playerCam.transform.position) > m_DoorMaxGrab)
        {
            DropObject();
        }
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
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().freezeRotation = false;
    }

    public void OnSelect()
    {
        playerMovement.enabled = false;
        IsBeingDragged();
    }

    public void OnDiselect()
    {
        playerMovement.enabled = true;
        DropObject();
    }
}
