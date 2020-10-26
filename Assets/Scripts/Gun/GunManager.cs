using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GunInfo
{
    public int spareMagazines;
    public RaycastShoot gunScript;

}

public class GunManager : MonoBehaviour
{
    [SerializeField]
    public GunInfo[] ownedGuns = new GunInfo[2];

    [SerializeField]
    GameObject test;
    private GunInfo equipedGun;

    #region DELETE LATER
    public RaycastShoot[] deleteLater;

    void Start()
    {
        //ownedGuns[0] = new GunInfo();
        //ownedGuns[1] = new GunInfo();
        //ownedGuns[0].gunScript = deleteLater[0];
        ownedGuns[0].spareMagazines = 7;


        //ownedGuns[1].gunScript = deleteLater[1];
        ownedGuns[1].spareMagazines = 9;


    }
    #endregion

    void Update()
    {
        
        // Switching between guns
        if (Input.GetKeyDown(KeyCode.Alpha1) && ownedGuns[0] != null && equipedGun != ownedGuns[0])
        {
            Debug.Log("ENTERING WEAPON 1");
            SwitchEquipedGun(0);
        }


        if (Input.GetKeyDown(KeyCode.Alpha2) && ownedGuns[1] != null && equipedGun != ownedGuns[1])
        {
            SwitchEquipedGun(1);
        }

        if (equipedGun != null)
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                equipedGun.gunScript.Shooting();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (equipedGun.spareMagazines > 0)
                {
                    // Change equiped gun magazine with a spare one with a random number of bullets
                    equipedGun.gunScript.ChangeMagazine(Mathf.RoundToInt(equipedGun.gunScript.magazineSize * RandomNonLinearProbabilityPercentage()));
                    equipedGun.spareMagazines--;
                    Debug.Log($"RELOADED! - new rand magazine: {equipedGun.gunScript.bullets} | spare magazines: {equipedGun.spareMagazines}");
                }
                else
                {
                    Debug.Log($"NO SPARE MAGAZINES AVAILABLE!");
                }
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                equipedGun.gunScript.ZoomingIn(Time.deltaTime * 10f);
            }
            else
            {
                equipedGun.gunScript.ZoomingOut(Time.deltaTime * 10f);
            }
        }
    }

    private void SwitchEquipedGun(int gunIndex)
    {


        // NOT WORKING 
        // Disable all guns ...
        foreach (GunInfo gun in ownedGuns)
        {
            gun.gunScript.gameObject.SetActive(false);
            Debug.Log(gun.gunScript.gameObject.name);

        }

        
        ownedGuns[gunIndex].gunScript.gameObject.SetActive(true); // ... but this one   NOT ACTIVATING



        equipedGun = ownedGuns[gunIndex]; // update equiped gun

        Debug.Log($"Gun {gunIndex + 1} ready!");



    }

    private float RandomNonLinearProbabilityPercentage()
    {
        float randNonLinearProbabilityPercentage = 0.00f;

        for (int i = 1; i <= 4; i++)
        {
            randNonLinearProbabilityPercentage = UnityEngine.Random.Range(randNonLinearProbabilityPercentage, 0.25f * i);
        }

        return randNonLinearProbabilityPercentage;
    }
}
