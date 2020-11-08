using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
	void Awake()
	{
		this.type = "Pistol";
		pelletHoleManager = new PelletHoleManager();
	}
	protected override Vector3 WeaponSpread()
    {     
        return base.WeaponSpread();
    }

}