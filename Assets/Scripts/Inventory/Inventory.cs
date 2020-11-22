using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	
	// Prefab slot for instantiate
	[SerializeField] GameObject referenceSlot;
	
	// Inventory storage
	Dictionary<string, GameObject> inventory;


	private void Awake()
	{
		inventory = new Dictionary<string, GameObject>();

	}

	public void AddItemSlot(GameObject item)
	{
		// Gets script from itemPickedUp object
		IPickUpable itemInfo = item.GetComponent<IPickUpable>();


		// If item of same type is already stored stacks it and returns
		if (IsStored(itemInfo.Type))
		{
			Debug.Log("JA TEM GUA<RDADO");
			ISlot storedItem = inventory[itemInfo.Type].GetComponent<ISlot>();
			
			Debug.Log(storedItem.Quantity);

			storedItem.Quantity++;
			Debug.Log(storedItem.Quantity);

			UpdateQuantity(inventory[itemInfo.Type]);

			return;
		}

		// Creates new slot
		CreateNewSlot(itemInfo);




	}

   

	private void CreateNewSlot(IPickUpable itemPickedUp)
	{

		GameObject newSlotObject = Instantiate(referenceSlot, this.transform);
		ISlot newSlot = newSlotObject.GetComponent<ISlot>();
		
		newSlot.StoredItem = itemPickedUp;
		
		inventory.Add(newSlot.GetType, newSlotObject);
		
		newSlotObject.GetComponent<Image>().sprite = newSlot.GetIcon;
		
		newSlot.StoredItem.UpdateItemQuantityUI(newSlotObject);


	}

	public void UpdateQuantity(GameObject currentSlot)
	{
		currentSlot.GetComponent<ISlot>().StoredItem.UpdateItemQuantityUI(currentSlot);
	}



	
	public bool IsStored(string key) => inventory.ContainsKey(key);

	public int GetQuantity(string key) => inventory[key].GetComponent<ISlot>().Quantity;
	
	public void RemoveSlot(string key)
	{
		inventory[key].GetComponent<ISlot>().StoredItem.RemoveItem(inventory);
	}
	
}
