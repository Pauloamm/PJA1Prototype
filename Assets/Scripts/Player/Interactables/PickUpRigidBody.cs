using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRigidBody : MonoBehaviour, IRaycastResponse
{
    public GameObject playerCam;

    private float ThrowStrength = 50f;
    private float distance = 2.5f;
    private float maxDistanceGrab = 4f;

    private Ray playerAim;
    public bool isObjectHeld;
    private bool tryPickupObject;

    //TESTE
    [SerializeField]
    PlayerMovement playerMovement;

    private void Awake()
    {
        isObjectHeld = false;
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
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 nextPos = playerCam.transform.position + playerAim.direction * distance;
        Vector3 currPos = this.transform.position;

        this.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 20;

        
        
        if (Vector3.Distance(this.transform.position, playerCam.transform.position) > maxDistanceGrab)
        {
            DropObject();
        }
    }

    public void DropObject()
    {
        isObjectHeld = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().freezeRotation = false;
    }

    public void OnRaycastSelect()
    {
        IsBeingDragged();
    }

    public void OnRaycastDiselect()
    {
        DropObject();
    }
}
