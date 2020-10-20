using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    static public Ray playerRay;

    [SerializeField]
    private Camera playerCamera;

    private float range = 10f;
    private float pickUpRange, dragRange;

    // Events
    public delegate void OnInteract(GameObject objectHit);
    public event OnInteract OnDrag;
    public event OnInteract OnPickUp;



    void FixedUpdate()
    {
        playerRay = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        RaycastHit hit;

        if (Physics.Raycast(playerRay, out hit))
        {
            if (hit.collider != null)
            {

                if (Input.GetMouseButton(0))
                {
                    GameObject objectHit = hit.collider.gameObject;
                    float currentDistance = (hit.point - this.transform.position).magnitude;

                    switch (objectHit.tag)
                    {
                        case "Dragable":

                            if (currentDistance < dragRange)
                                OnDrag?.Invoke(objectHit);
                            break;

                        case "PickUp":

                            if (currentDistance < pickUpRange)
                                OnPickUp?.Invoke(objectHit);
                            break;

                    }

                }
               

            }
                
            
        }
    }

}
