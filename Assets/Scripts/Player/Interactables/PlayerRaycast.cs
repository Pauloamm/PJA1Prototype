using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    static public Ray playerRay;

    private IRaycastResponse lastObject;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private Inventory inventory;

    private bool isHolding = true;

    [SerializeField]
    private Vector3 MouseP;

    [SerializeField]
    private WeaponManager weaponManager;


    private float range = 3f;
    private float pickUpRange, dragRange;



    // Events
    public delegate void OnInteract(GameObject objectHit);

    public event OnInteract OnDrag;
    public event OnInteract OnPickUp;




    void FixedUpdate()
    {
        Vector2 Mouse2D = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MouseP = new Vector3(Mouse2D.x, Mouse2D.y, 0f);

        playerRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;

        bool isHitting = Physics.Raycast(playerRay, out hit);
        bool hasCollider = hit.collider != null;
        bool hasInput = Input.GetKeyDown(KeyCode.E);


        if (isHitting && hasCollider && hasInput)
        {
            GameObject objectHit = hit.collider.gameObject; //.GetComponent<IRaycastable>();
            float currentDistance = (hit.point - this.transform.position).magnitude;

            if (currentDistance < range)
            {
                IRaycastResponse temp = objectHit.GetComponent<IRaycastResponse>();

                temp.OnRaycastSelect();
            }
           
        }
       
    }




}