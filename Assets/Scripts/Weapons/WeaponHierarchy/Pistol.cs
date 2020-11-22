using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
	void Awake()
	{
		this.type = "Pistol";
		pelletHoleManager = new PelletHoleManager();
		
		defaultLocalPosition = this.transform.position;
		defaultLocalRotation = this.transform.rotation;
	}
	

}