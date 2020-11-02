using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    static public Ray playerRay;


    [SerializeField] private Camera playerCamera;

    [SerializeField] private SlotManager slotManager;

    [SerializeField] private DragRigidBody dragRb;
    private bool isHolding = true;

    [SerializeField]
    private WeaponManager weaponManager;


    private float range = 10f;
    private float pickUpRange, dragRange;



    // Events
    public delegate void OnInteract(GameObject objectHit);

    public event OnInteract OnDrag;
    public event OnInteract OnPickUp;




    void FixedUpdate()
    {
        playerRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;

        if (Physics.Raycast(playerRay, out hit))
        {
            if (hit.collider != null)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    GameObject objectHit = hit.collider.gameObject; //.GetComponent<IRaycastable>();
                    float currentDistance = (hit.point - this.transform.position).magnitude;

                  

                    if (currentDistance < range)
                    {
                        if (objectHit.CompareTag("Dragable"))
                        {
                            // isHolding = true;    
                            dragRb.ChangeHolding();
                            // DragBody();
                            dragRb.DragBody(objectHit);
                        }
                        else
                        {
                            Storable storableObject = objectHit.GetComponent<Storable>();
                            storableObject?.StoreItem();
                        }
                        
                    }

                }
                else if (dragRb.IsHolding2)
                {
                    dragRb.DropObject();
                    dragRb.ChangeHolding();
                }
            }
        }
    }




}