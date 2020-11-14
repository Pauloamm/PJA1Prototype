using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorGrabClass
{
    public float m_DoorPickupRange = 0.3f;
    public float m_DoorThrow = 10f;
    public float m_DoorDistance = 0.3f;
    public float m_DoorMaxGrab = 0.3f;
}

public class DragRigidBody : MonoBehaviour, IRaycastResponse
{

    public GameObject playerCam;
    public DoorGrabClass DoorGrab = new DoorGrabClass();

    private float PickupRange = 0.3f;
    private float ThrowStrength = 50f;
    private float distance = 0.3f;
    private float maxDistanceGrab = 0.3f;

    private Ray playerAim;
    public bool isObjectHeld;
    private bool tryPickupObject;

    //TESTE
    [SerializeField]
    PlayerMovement playerMovement;

    private void Awake()
    {
        isObjectHeld = false;
        tryPickupObject = false;
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

        //if is door  use door values
        //PickupRange = DoorGrab.m_DoorPickupRange;
        //ThrowStrength = DoorGrab.m_DoorThrow;
        //distance = DoorGrab.m_DoorDistance;
        //maxDistanceGrab = DoorGrab.m_DoorMaxGrab;
    }

    public void HoldObject()
    {
        Transform Temp;

        Temp = this.transform;

        Temp.Rotate(Vector3.up, (Input.GetAxis("Mouse Y") * Time.deltaTime) * 300f, Space.Self);

        Quaternion.Slerp(this.transform.rotation, Temp.rotation, 1f);

        if (Vector3.Distance(this.transform.position, playerCam.transform.position) > maxDistanceGrab)
        {
            DropObject();
        }
    }

    public void DropObject()
    {
        isObjectHeld = false;
        // tryPickupObject = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().freezeRotation = false;
    }

    public void OnRaycastSelect()
    {
        playerMovement.enabled = false;
        IsBeingDragged();
    }

    public void OnRaycastDiselect()
    {
        playerMovement.enabled = true;
        DropObject();
    }
}
