﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, IRechargeable
{
    private float holdKeyDelay = 1f, currentTimer = 0f;
    private bool isRecharging;

    [SerializeField]
    private Light flashlightLight;

    [SerializeField]
    Transform player, playerCamera;

    private Vector3 offset;

    public MovementInfo info;


    float angularDrag = 0.95f;

    public int RemainingCharges
    {
        get { return RemainingCharges; }

        set { if (value < 0) value = 0; }
    }
    public bool canBeUsed { get; set; }


    // Start is called before the first frame update
    void Awake()
    {
        if (flashlightLight == null) flashlightLight = this.GetComponentInChildren<Light>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        player = playerObject.transform;
        playerCamera = playerObject.GetComponentInChildren<Camera>().transform;

        offset = playerCamera.position - this.transform.position;


        RemainingCharges = 0;
        isRecharging = false;

    }

    // Update is called once per frame
    void Update()
    {

        UpdatePosition();
        //MouseFollow();


        // If it is only a click
        if (Input.GetKeyUp(KeyCode.F))
        {
            currentTimer = 0f;

            //Turn On/Off
            if (canBeUsed)
            {
                flashlightLight.enabled = !flashlightLight.enabled;
            }
            else canBeUsed = true;


        }

        // Checks if it is held down(1sec held)
        else if (Input.GetKey(KeyCode.F))
        {

            if (currentTimer >= holdKeyDelay && canBeUsed)
            {
                flashlightLight.enabled = false;
                Recharge();
                currentTimer = 0f;
                canBeUsed = false;

            }

            else
                currentTimer += Time.deltaTime;
        }






    }

    public void Recharge()
    {

    }

    public void MouseFollow()
    {
        // Update our position according to current rotation vector 
        info.orientationV2 += info.rotationV2 * Time.deltaTime;

        // Add drag
        info.rotationV2 *= angularDrag;


        // Read Mouse Movement
        Vector2 mouseXY = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        float nextAngular;

        float mousesens = 20f;
        // Gets mouse inputs
        if (Mathf.Abs(mouseXY.x) != 0 || Mathf.Abs(mouseXY.y) != 0)
        {
            // Adds angular X
            nextAngular = mouseXY.x * Time.deltaTime * mousesens;
            info.rotationV2.x += nextAngular;

            //Adds angular Y
            nextAngular = mouseXY.y * Time.deltaTime * mousesens;
            info.rotationV2.y -= nextAngular;
        }

        //Normalize orientation
        info.orientationV2 = AuxMethods.NormAngle(info.orientationV2);

        //Y axis clamp
        info.orientationV2.y = Mathf.Clamp(info.orientationV2.y, -Mathf.PI / 2, Mathf.PI / 2);

        // Resets rotations for next frame(values are additive and odnt reset after update)
        /*flashlightLight.*/transform.rotation = Quaternion.identity;

        // Rotates flashlight right-left
        /*flashlightLight.*/transform.Rotate(flashlightLight.transform.up, info.orientationV2.x * Mathf.Rad2Deg,Space.World);
        /*flashlightLight.*/transform.Rotate(flashlightLight.transform.right, info.orientationV2.y * Mathf.Rad2Deg, Space.World);

    }

    public void UpdatePosition()
    {
        //this.transform.position = player.position + offset;


        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, playerCamera.transform.rotation + offset, 3.0f * Time.deltaTime);

        
        this.transform.forward = Vector3.Lerp(player.transform.forward , player.transform.forward + offset, 3.0f * Time.deltaTime);
        
        
    }

    //IEnumerator Offset()
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    this.transform.
    //}

}
