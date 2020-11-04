using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Heal")]

public class HealAction : Action
{
	public override void RespectiveAction(GameObject itemObject)
	{
		Destroy(itemObject);
	}
}
