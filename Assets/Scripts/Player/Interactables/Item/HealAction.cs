using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Action/Heal")]

public class HealAction : Action
{
	public override void RespectiveAction(GameObject itemObject)
	{
		ISlot slot = itemObject.GetComponent<ISlot>();
		
		itemObject.transform.parent.GetComponent<Inventory>().RemoveSlot(slot.GetType);
		//HEAL ACTION

		Debug.Log("BIG BIG HEAL");

		
		
	}
}
