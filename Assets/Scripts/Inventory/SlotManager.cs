using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
	//List<GameObject> slots;

	Dictionary<string, GameObject> inventory;

	// Prefab slot for instantiate
	[SerializeField] GameObject referenceSlot;

	private void Awake()
	{
		inventory = new Dictionary<string, GameObject>();

		//slots = new List<GameObject>();
	}

	public void AddSlot(GameObject item)
	{
		// Gets script from pickUp object
		IStorable itemInfo = item.GetComponent<IStorable>();


		// Creates new slot object with slot script for info storage
		if (IsStored(itemInfo.Type))
		{
			IStorable temp = inventory[itemInfo.Type].GetComponent<IStorable>();
			Debug.Log(temp);
			temp.Quantity++;
			UpdateQuantity(inventory[itemInfo.Type], temp);
			return;
		}


		// Creates new slot object with slot script for info storage
		GameObject newSlotObject = Instantiate(referenceSlot, this.transform);


		Slot newSlotScript = newSlotObject.GetComponent<Slot>();

		// Stores data in slot script
		StoreSlotProperties(newSlotScript, itemInfo, newSlotObject);

		// Update object image 
		StoreImageInSlotObject(newSlotObject, newSlotScript);


		// DEBUG
		Debug.Log("slot Added");


	}

    public bool IsStored(string key)
    {
        return inventory.ContainsKey(key);
    }


	private void StoreSlotProperties(Slot newSlotScript, IStorable itemInfo, GameObject newSlotObject)
	{

		// Item for inspect item menu and inventory icon
		newSlotScript.itemGameObjectForInspect = itemInfo.ItemGameObjectForInspect;
		newSlotScript.icon = itemInfo.Icon;

		// // Probably delete later if image static
		// newSlotScript.iD = itemInfo.iD;
		newSlotScript.type = itemInfo.Type;
		// newSlotScript.description = itemInfo.description;

		newSlotScript.slotActions = itemInfo.ItemActions;
		Debug.Log(newSlotScript.type);
		inventory.Add(newSlotScript.type, newSlotObject);
		// Adds slot script to the list
		//slots.Add(newSlotObject);
	}

	public void UpdateQuantity(GameObject currentSlot, IStorable inventorySlot)
	{
		currentSlot.GetComponentInChildren<Text>().text = "x" + inventorySlot.Quantity.ToString();
	}


	private void StoreImageInSlotObject(GameObject currentSlot, Slot tempSlot)
	{
		currentSlot.GetComponent<Image>().sprite = tempSlot.icon;

	}
}
