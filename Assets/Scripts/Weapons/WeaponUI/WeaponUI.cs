using System.Collections.Generic;
using UnityEngine;


public class WeaponUI : MonoBehaviour
{
    [SerializeField] private WeaponInfo correspondentWeapon;
    [SerializeField] private List<GameObject> bulletsUI;

    //ACESSORS
    public WeaponInfo CorrespondentWeapon => correspondentWeapon;

  
    
    public void WeaponReloadedUIChange()
    {
        foreach (GameObject obj in bulletsUI)
        {
            obj.SetActive(true);
        }
    }

    public void WeaponShotUIChange()
    {
        bulletsUI[bulletsUI.Count - (correspondentWeapon.weapon.BulletsInCurrentMagazine + 1)].SetActive(false);
    }
}