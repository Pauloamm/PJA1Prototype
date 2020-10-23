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
    private bool isObjectHeld;
    private bool tryPickupObject;

    private void Start()
    {
        isObjectHeld = false;
        tryPickupObject = false;
        objectHeld = null;
    }


    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isObjectHeld)
            {
                tryPickObject();
                tryPickupObject = true;
            }
            else
            {
                holdObject();
            }
        }
        else if (isObjectHeld)
        {
            DropObject();
        }


    }

    private void tryPickObject()
    {
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(playerAim, out hit, PickupRange))
            objectHeld = hit.collider.gameObject;
        {
            if (hit.collider != null &&  hit.collider.CompareTag("Dragable") && tryPickupObject )
            {

                isObjectHeld = true;
                objectHeld.GetComponent<Rigidbody>().useGravity = true;
                objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
                PickupRange = DoorGrab.m_DoorPickupRange;
                ThrowStrength = DoorGrab.m_DoorThrow;
                distance = DoorGrab.m_DoorDistance;
                maxDistanceGrab = DoorGrab.m_DoorMaxGrab;
               
            }


        }
    }


    private void holdObject()
    {
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 nextPos = playerCam.transform.position + playerAim.direction * distance;
        Vector3 currPos = objectHeld.transform.position;

        objectHeld.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10;

        if (Vector3.Distance(objectHeld.transform.position, playerCam.transform.position) > maxDistanceGrab)
        {
            DropObject();
            Debug.Log("ola");
        }

    }

    private void DropObject()
    {
        
        isObjectHeld = false;
        tryPickupObject = false;
        objectHeld.GetComponent<Rigidbody>().useGravity = true;
        objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
        objectHeld = null;
    }
}
