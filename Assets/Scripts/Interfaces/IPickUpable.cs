using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUpable
{
	// Acessers
	List<Action> ItemActions { get; }
    GameObject ItemGameObjectForInspect { get; }
    Sprite Icon { get; }
	string Type { get; }

	Inventory inventoryToStore { get; }
	
	
    // Methods
    void StoreItem();
    void UpdateItemQuantityUI(GameObject currentItem);
    void RemoveItem(Dictionary<string,GameObject> inventoryToRemove);
    

}