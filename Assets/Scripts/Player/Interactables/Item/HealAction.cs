using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Action/Heal")]

public class HealAction : Action
{
	public override void RespectiveAction(GameObject itemObject)
	{
		if (itemObject.GetComponent<IStorable>().Quantity-- <= 1)
			Destroy(itemObject);
		else
			itemObject.GetComponentInChildren<Text>().text = "x" + 
				itemObject.GetComponent<IStorable>().Quantity.ToString();
	}
}
