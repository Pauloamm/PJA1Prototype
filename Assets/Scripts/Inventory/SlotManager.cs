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
		//slots = new List<GameObject>();
		inventory = new Dictionary<string, GameObject>();
	}

	public void AddSlot(GameObject item)
	{
		// Gets script from pickUp object
		Storable itemInfo = item.GetComponent<Storable>();


		// Creates new slot object with slot script for info storage
		if (inventory.ContainsKey(itemInfo.type))
		{
			InventorySlot temp = inventory[itemInfo.type].GetComponent<InventorySlot>();
			temp.quantity++;
			UpdateQuantity(inventory[itemInfo.type], temp);

			return;
		}
		GameObject newSlotObject = Instantiate(referenceSlot, this.transform);
		//Slot newSlotScript = newSlotObject.GetComponent<Slot>();


		Slot newSlotScript = new Slot();

		// Stores data in slot script
		StoreSlotProperties(newSlotScript, itemInfo, newSlotObject);

		// Update object image 
		StoreImageInSlotObject(newSlotObject, newSlotScript);


		// DEBUG
		Debug.Log("slot Added");
	}



	private void StoreSlotProperties(Slot newSlotScript, Storable itemInfo, GameObject newSlotObject)
	{
		// Item for inspect item menu and inventory icon
		newSlotScript.itemGameObjectForInspect = itemInfo.ItemGameObjectForInspect;
		newSlotScript.icon = itemInfo.Icon;
		newSlotScript.type = itemInfo.type;
		newSlotScript.slotActions = itemInfo.ItemActions;

		// // Probably delete later if image static
		// newSlotScript.iD = itemInfo.iD;
		// newSlotScript.type = itemInfo.type;
		// newSlotScript.description = itemInfo.description;



		InventorySlot newInventorySlot = new InventorySlot
		{
			slot = newSlotScript,
			quantity = 1
		};

		newSlotObject.GetComponent<InventorySlot>().slot = newInventorySlot.slot;
		newSlotObject.GetComponent<InventorySlot>().quantity = newInventorySlot.quantity;

		inventory.Add(newSlotScript.type, newSlotObject);

		// Adds slot script to the list
		//slots.Add(newSlotObject);
	}



	public void UpdateQuantity(GameObject currentSlot, InventorySlot inventorySlot)
	{
		currentSlot.GetComponentInChildren<Text>().text = "x" + inventorySlot.quantity.ToString();
	}


	private void StoreImageInSlotObject(GameObject currentSlot, Slot tempSlot)
	{
		currentSlot.GetComponent<Image>().sprite = tempSlot.icon;
	}

	//public void RemoveFromList(GameObject currentSlot) => slots.Remove(currentSlot);

}
