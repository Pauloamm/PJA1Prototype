using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, IRechargeable
{
    //--------------------------------------------------

    // Action variables
    private float holdKeyDelay = 1f, currentTimer = 0f;
    private bool isRecharging;
    public bool canBeUsed { get; set; }


    [SerializeField]
    private Light flashlightLight;


    // TEST
    [SerializeField]
    Transform player, playerCamera;
    GameObject playerObject;

    private Vector3 offset;
    private Vector3 initialOffset;

    public MovementInfo info;

    float angularDrag = 0.95f;

    [SerializeField]
    private int remainingCharges;
    public int RemainingCharges
    {
        get { return remainingCharges; }

        set
        {
            if (value < 0)
            {
                remainingCharges = 0;
            }
            else remainingCharges = value;
        }
    }

    
    
    int chargeDuration;
    float currentChargeDurationRemaining;
    public int CurrentChargePercentageRemaining => (int) ((currentChargeDurationRemaining / chargeDuration) * 100);

    //--------------------------------------------------

    void Awake()
    {
        if (flashlightLight == null) flashlightLight = this.GetComponentInChildren<Light>();

        playerObject = GameObject.FindGameObjectWithTag("Player");

        player = playerObject.transform;
        playerCamera = playerObject.GetComponentInChildren<Camera>().transform;

        offset = playerCamera.position - this.transform.position;

        initialOffset = player.transform.localPosition - this.transform.position;
        initialOffset.Normalize();


        RemainingCharges = 0;
        currentChargeDurationRemaining = 300;
        chargeDuration = 300;
        isRecharging = false;
        canBeUsed = true;

    }

    void Update()
    {
        // TEST    small rotation upwards and down with camera movement
        //UpdatePosition();

        // Timer counting battery time left
        if (flashlightLight.enabled)
            BatteryTimer();

        // Input
        FlashLightAction();

    }


    private void FlashLightAction()
    {

        // If it is only a click and has battery
        if (Input.GetKeyUp(KeyCode.F) && currentChargeDurationRemaining > 0)
        {
            //Resets timer for click/hold separation
            currentTimer = 0f;

            //Turn On/Off
            if (canBeUsed && currentChargeDurationRemaining > 0)
            {
                flashlightLight.enabled = !flashlightLight.enabled;
            }
            else canBeUsed = true;


        }

        // Checks if it is held down(1sec held)
        else if (Input.GetKey(KeyCode.F))
        {

            if (currentTimer >= holdKeyDelay && canBeUsed && RemainingCharges > 0)
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

    private void BatteryTimer()
    {
        // Counts down
        if (currentChargeDurationRemaining > 0)
            currentChargeDurationRemaining -= Time.deltaTime;

        // Clamps when goes below 0 and turns off lantern
        else
        {
            currentChargeDurationRemaining = 0;
            flashlightLight.enabled = false;

        }
        
    }

    public void Recharge()
    {
        if (RemainingCharges > 0)
        {
            RemainingCharges--;
            currentChargeDurationRemaining = chargeDuration;
        }


    }


  
    public void UpdatePosition()
    {

        GameObject lookCube = GameObject.Find("InvisibleTarget");

        Vector3 newForward = lookCube.transform.position - this.transform.position;
        
        this.transform.forward = newForward;


    }





}
