using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health 
{

	public int health;

	public Health(int health)
	{
		this.health = health;
	}

	public int GetHealth() => health;	
}
