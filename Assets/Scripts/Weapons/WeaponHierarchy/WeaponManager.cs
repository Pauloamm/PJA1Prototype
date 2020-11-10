using System;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<WeaponInfo> ownedGuns; // SERIALIZED FOR DEBUGGING

    /*[SerializeField]*/ private WeaponInfo equipedWeapon; // SERIALIZED FOR DEBUGGING

    [SerializeField] private GameObject previousGun;

    // Input keycodes 
    [SerializeField] private readonly KeyCode shootKeycode = KeyCode.Mouse0;
    [SerializeField] private readonly KeyCode reloadKeycode = KeyCode.R;

    // ACESSORS
    public WeaponInfo EquippedWeapon => equipedWeapon;

    // Events for UI change
    public delegate void WeaponAction(WeaponInfo weaponInfo);
    public event WeaponAction WeaponSwitched;
    public event WeaponAction WeaponReloaded;
    public event WeaponAction WeaponShot;
    
    private void Awake()
    {
        ownedGuns = new List<WeaponInfo>();
    }

    private void Update()
    {
        WeaponSelectionInput();

        WeaponActionInput();
    }

    private void WeaponSelectionInput()
    {
        // Check for input if u pressed any of the weapons associated keys
        foreach (WeaponInfo weaponInfo in ownedGuns)
        {
            if (Input.GetKeyDown(weaponInfo.weapon.weaponKeyCode))
            {
                // If so switch for respective weapon
                SwitchWeapon(weaponInfo);
            }
        }
    }

    private void WeaponActionInput()
    {
        // Guard clause
        if (equipedWeapon == null) return;


        if (Input.GetKeyDown(shootKeycode))
        {
            if (equipedWeapon.weapon.Attack())
                WeaponShot?.Invoke(equipedWeapon);
        }

        if (Input.GetKeyDown(reloadKeycode) && equipedWeapon.remainingMagazines > 0)
        {
            if (equipedWeapon.weapon.ChangeMagazine())
                WeaponReloaded?.Invoke(equipedWeapon);
        }
    }


    private void SwitchWeapon(WeaponInfo weaponToEquip)
    {
        if(equipedWeapon == null 
        || equipedWeapon.weapon.weaponState != WeaponState.Idle) return;
        
        GameObject weaponToEquipObject = weaponToEquip.weapon.gameObject;

        try
        {
            // Gives exception in the first weapon picked up
            previousGun.gameObject.SetActive(false);
        }
        catch (Exception)
        {
            // Exception happening in the first weapon picked up

        }
        finally
        {
            // Equips new weapon and deactivates previous equiped one
            weaponToEquipObject.SetActive(true);
            equipedWeapon = weaponToEquip;
            previousGun = weaponToEquipObject;
            
            WeaponSwitched?.Invoke(equipedWeapon);

            // Resets localPosition and localRotation to default values
            equipedWeapon.weapon.ResetLocalPosAndRot(true);
            equipedWeapon.weapon.weaponState = WeaponState.Idle;
        }

        /*
        if(equipedWeapon == null 
        || equipedWeapon.weapon.weaponState != WeaponState.Idle) return;

        equipedWeapon.weapon.gameObject.SetActive(false);
        weaponToEquip.weapon.gameObject.SetActive(true);
        equipedWeapon = weaponToEquip;
        
        WeaponSwitched?.Invoke(equipedWeapon);

        // Resets localPosition and localRotation to default values
        equipedWeapon.weapon.ResetLocalPosAndRot(true);
        */
    }

    public void AddWeapon(GameObject newWeapon, Transform parentCamera)
    {
        WeaponInfo weaponInfo = new WeaponInfo
        {
            remainingMagazines = 50,
            weapon = newWeapon.GetComponent<Weapon>(),
        };

        ownedGuns.Add(weaponInfo);

        // Initiates default localPosition and localRotation to parent
		weaponInfo.weapon.transform.SetParent(parentCamera);
        weaponInfo.weapon.ResetLocalPosAndRot(true);
        weaponInfo.weapon.weaponState = WeaponState.Idle;

        //Switches for new weapon
        equipedWeapon = weaponInfo;
        SwitchWeapon(weaponInfo);
    }
}