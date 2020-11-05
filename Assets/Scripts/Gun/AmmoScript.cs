using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoScript : MonoBehaviour
{
    //Aceder a classes
    [SerializeField]
    WeaponManager weaponManager;

    [SerializeField]
    Weapon weapon;

    [SerializeField]
    GameObject pistolMagazine;

    Transform bullet;

    [SerializeField]
    Transform[] bullets;

    private void Awake()
    {
        weaponManager.swaping += AmmoTypeChange;

    }
    // Start is called before the first frame update
    void Start()
    {
        
        bullets = new Transform[10];
        bullet = pistolMagazine.transform.GetChild(0);
        for (int i = 0; i < 10; i++)
        {
            bullets[i] = bullet.transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    void AmmoTypeChange(WeaponInfo weaponInfo)
    {
        switch (weaponInfo.weapon.type)
        {

            case "Pistol":
                Pistol(weaponInfo);
                break;

            case "Shotgun":
                Shotgun(weaponInfo);
                break;

            default:
                break;
        }
    }
    void Pistol(WeaponInfo weaponInfo)
    {
        pistolMagazine.SetActive(true);
    }

    void Shotgun(WeaponInfo weaponInfo)
    {

    }
}
