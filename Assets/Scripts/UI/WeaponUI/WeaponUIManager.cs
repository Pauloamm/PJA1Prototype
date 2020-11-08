using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUIManager : MonoBehaviour
{
    [SerializeField] private List<WeaponUI> weaponsUI;
    [SerializeField] private WeaponManager weaponManager;

    private WeaponUI currentWeaponUI;
    private WeaponUI previousWeaponUI;


    private void Awake()
    {
        weaponManager.WeaponSwitched += OnWeaponSwap;
        weaponManager.WeaponReloaded += OnWeaponReload;
        weaponManager.WeaponShot += OnWeaponShot;
        
        foreach (WeaponUI weaponUI in weaponsUI) weaponUI.gameObject.SetActive(false);
    }
    
    

    private void OnWeaponSwap(WeaponInfo weaponInfo)
    {
        foreach (WeaponUI weaponUI in weaponsUI)
        {
            if (weaponUI.CorrespondentWeapon.weapon == weaponInfo.weapon)
            {
                previousWeaponUI?.gameObject.SetActive(false);
                weaponUI.gameObject.SetActive(true);
                
                currentWeaponUI = weaponUI;
                previousWeaponUI = weaponUI;
            }
        }
    }

    private void OnWeaponShot(WeaponInfo weaponInfo) => currentWeaponUI.WeaponShotUIChange();

    private void OnWeaponReload(WeaponInfo weaponInfo) => currentWeaponUI.WeaponReloadedUIChange();
    
}