using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
//public class GunInfo
//{
//    public int spareMagazines;
//    public RaycastShoot gunScript;

//}

    

public class GunManager : MonoBehaviour
{

    public struct WeaponInfo
    {
       public  int remainingMagazines;
       public  Weapon weapon;
    };

    //[SerializeField]
    //public GunInfo[] ownedGuns = new GunInfo[2];
    [SerializeField]
    public List<WeaponInfo> ownedGuns;

    [SerializeField]
    GameObject test;

    private Vector3 DefaultOffSet= new Vector3(0.47f,-0.12f,0.7f);

    [SerializeField]
    private WeaponInfo equipedGun;

    private GameObject previousGun;

    private void Awake()
    {
        ownedGuns = new List<WeaponInfo>();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            equipedGun = ownedGuns[0];          
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            equipedGun = ownedGuns[1];
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            equipedGun = ownedGuns[2];
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            equipedGun = ownedGuns[3];
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            equipedGun = ownedGuns[4];
        }

        if(Input.GetMouseButton(0))
        {         
            equipedGun.weapon.shooting();
        }

        if(Input.GetKey(KeyCode.R))
        {
            equipedGun.weapon.ChangeMagazine(equipedGun.remainingMagazines);
            if(equipedGun.remainingMagazines>0)
            equipedGun.remainingMagazines--;
        }
    }

    private void SwicthGun(GameObject WeaponToEquip)
    {
        previousGun.gameObject.SetActive(false);
        WeaponToEquip.gameObject.SetActive(true);
        previousGun = WeaponToEquip;
    }

    public void AddWeapon(GameObject NewWeapon, Transform Parent)
    { 
        WeaponInfo weaponInfo = new WeaponInfo
        {
            remainingMagazines = 1,
            weapon = NewWeapon.GetComponent<Weapon>(),
        };

        ownedGuns.Add(weaponInfo);
        NewWeapon.transform.position = Vector3.zero;
        NewWeapon.transform.SetParent(Parent);
        NewWeapon.transform.localRotation = Quaternion.identity;
        NewWeapon.transform.localPosition = DefaultOffSet;
        NewWeapon.transform.LookAt(Parent.position + (Parent.forward * 50f));
        weaponInfo.weapon.OnPickUpDefaultInit(Quaternion.identity,DefaultOffSet);
        equipedGun = weaponInfo;
       
    }



    //#region DELETE LATER
    //public RaycastShoot[] deleteLater;




    //void Start()
    //{
    //    //ownedGuns[0] = new GunInfo();
    //    //ownedGuns[1] = new GunInfo();
    //    //ownedGuns[0].gunScript = deleteLater[0];
    //    ownedGuns[0].spareMagazines = 7;


    //    //ownedGuns[1].gunScript = deleteLater[1];
    //    ownedGuns[1].spareMagazines = 9;


    //}
    //#endregion



    //void Update()
    //{

    //    // Switching between guns
    //    if (Input.GetKeyDown(KeyCode.Alpha1) && ownedGuns[0] != null && equipedGun != ownedGuns[0])
    //    {
    //        Debug.Log("ENTERING WEAPON 1");
    //        SwitchEquipedGun(0);
    //    }


    //    if (Input.GetKeyDown(KeyCode.Alpha2) && ownedGuns[1] != null && equipedGun != ownedGuns[1])
    //    {
    //        SwitchEquipedGun(1);
    //    }

    //    if (equipedGun != null)
    //    {

    //        if (Input.GetKeyDown(KeyCode.Mouse0))
    //        {
    //            equipedGun.gunScript.Shooting();
    //        }
    //        else if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            if (equipedGun.spareMagazines > 0)
    //            {
    //                // Change equiped gun magazine with a spare one with a random number of bullets
    //                equipedGun.gunScript.ChangeMagazine(Mathf.RoundToInt(equipedGun.gunScript.magazineSize * RandomNonLinearProbabilityPercentage()));
    //                equipedGun.spareMagazines--;
    //                Debug.Log($"RELOADED! - new rand magazine: {equipedGun.gunScript.bullets} | spare magazines: {equipedGun.spareMagazines}");
    //            }
    //            else
    //            {
    //                Debug.Log($"NO SPARE MAGAZINES AVAILABLE!");
    //            }
    //        }

    //        if (Input.GetKey(KeyCode.Mouse1))
    //        {
    //            equipedGun.gunScript.ZoomingIn(Time.deltaTime * 10f);
    //        }
    //        else
    //        {
    //            equipedGun.gunScript.ZoomingOut(Time.deltaTime * 10f);
    //        }
    //    }
    //}

    //private void SwitchEquipedGun(int gunIndex)
    //{


    //    // NOT WORKING 
    //    // Disable all guns ...
    //    foreach (GunInfo gun in ownedGuns)
    //    {
    //        gun.gunScript.gameObject.SetActive(false);
    //        Debug.Log(gun.gunScript.gameObject.name);

    //    }


    //    ownedGuns[gunIndex].gunScript.gameObject.SetActive(true); // ... but this one   NOT ACTIVATING



    //    equipedGun = ownedGuns[gunIndex]; // update equiped gun

    //    Debug.Log($"Gun {gunIndex + 1} ready!");



    //}

   
}
