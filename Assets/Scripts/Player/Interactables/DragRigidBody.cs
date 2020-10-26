using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorGrabClass
{
    public float m_DoorPickupRange = 2f;
    public float m_DoorThrow = 10f;
    public float m_DoorDistance = 2f;
    public float m_DoorMaxGrab = 3f;
}

public class DragRigidBody : MonoBehaviour
{


    public GameObject playerCam;
    public DoorGrabClass DoorGrab = new DoorGrabClass();

    private float PickupRange = 3f;
    private float ThrowStrength = 50f;
    private float distance = 3f;
    private float maxDistanceGrab = 4f;

    private Ray playerAim;
    private GameObject objectHeld;
    public bool isObjectHeld;
    private bool tryPickupObject;

    

    //------LUis
    private bool isHolding2 = false;

    public void ChangeHolding() {
        isHolding2 = !isHolding2;
        
    }
    public bool IsHolding2 => isHolding2;
    //------LUis

    private void Awake()
    {
        isObjectHeld = false;
        tryPickupObject = false;
        objectHeld = null;
    }
    
    public  void DragBody(GameObject objectHit)
    {
        objectHeld = objectHit;

            if (!isObjectHeld)
            {
                TryPickObject();
                // tryPickupObject = true;
            }
            else
            {
                HoldObject();
            }
        
        

    }

    public void TryPickObject()
    {
        isObjectHeld = true;
        objectHeld.GetComponent<Rigidbody>().useGravity = true;
        objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
        
        
        PickupRange = DoorGrab.m_DoorPickupRange;
        ThrowStrength = DoorGrab.m_DoorThrow;
        
        
        distance = DoorGrab.m_DoorDistance;
        maxDistanceGrab = DoorGrab.m_DoorMaxGrab;
    }
    
    public void HoldObject()
    {
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 nextPos = playerCam.transform.position + playerAim.direction * distance;
        Vector3 currPos = objectHeld.transform.position;

        objectHeld.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10;

        if (Vector3.Distance(objectHeld.transform.position, playerCam.transform.position) > maxDistanceGrab)
        {
            DropObject();
            isHolding2 = !isHolding2; // false
            Debug.Log("ola");
        }

    }

    public void DropObject()
    {
        
        isObjectHeld = false;
        // tryPickupObject = false;
        objectHeld.GetComponent<Rigidbody>().useGravity = true;
        objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
        objectHeld = null;
    }
}
