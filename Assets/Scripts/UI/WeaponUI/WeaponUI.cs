﻿using System.Collections.Generic;
using UnityEngine;


public class WeaponUI : MonoBehaviour
{
    [SerializeField] private WeaponInfo correspondentWeapon;
    [SerializeField] private List<GameObject> bulletsUI;

    //ACESSORS
    public WeaponInfo CorrespondentWeapon => correspondentWeapon;


    void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            bulletsUI.Add(transform.GetChild(i).gameObject);
        }
    }
    
    public void WeaponReloadedUIChange()
    {
        int reloadedBullets = correspondentWeapon.weapon.BulletsInCurrentMagazine;
        int magazineSize = correspondentWeapon.weapon.DefaultMagazineSize;

        for (int i = magazineSize - 1; i >= magazineSize - reloadedBullets; i--)
        {
            bulletsUI[i].SetActive(true);
        }
            
    }

    public void WeaponShotUIChange()
    {
        bulletsUI[bulletsUI.Count - (correspondentWeapon.weapon.BulletsInCurrentMagazine + 1)].SetActive(false);
    }
}