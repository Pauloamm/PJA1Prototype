using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(menuName ="Interactable/Item")]

public class Item : MonoBehaviour, IPickUpable, IRaycastResponse
{
	// -------------------------------------IPickUpable---------------------------------------------//

	// InventorySlot manager
	[SerializeField] private Inventory inventory;

	// List of actions for the item
	[SerializeField] private List<Action> itemActions;

	// Item for inspect item menu and inventory icon
	[SerializeField] private GameObject itemGameObjectForInspect;
	[SerializeField] private Sprite icon;
	public string type = "Item";

	public Inventory Inventory => inventory;
	public List<Action> ItemActions => itemActions;
	public GameObject ItemGameObjectForInspect => itemGameObjectForInspect;
	public Sprite Icon => icon;
	public string Type { get { return this.type; } set { type = value; } }
	
	public Inventory inventoryToStore { get; }

	// -------------------------------------------------------------------------------------------//
    public void StoreItem()
    {
        inventory.AddItemSlot(this.gameObject);
        Destroy(this.gameObject);
    }

 


    public void OnRaycastSelect()
    {
        StoreItem();
    }

    public void OnRaycastDiselect()
    {
        //pra nao dar erro XD
    }

    public void UpdateItemQuantityUI(GameObject currentItem)
    {
	    ISlot currentSlot = currentItem.GetComponent<ISlot>();
	    currentItem.GetComponentInChildren<Text>().text = "x" + currentSlot.Quantity;
    }

    public void RemoveItem(Dictionary<string, GameObject> inventoryToRemove)
    {
	    InventorySlot currentSlot =(InventorySlot) inventoryToRemove[type].GetComponent<ISlot>();
	    
	    if (currentSlot.Quantity > 1)
	    {
		    currentSlot.Quantity--;
		    UpdateItemQuantityUI(inventoryToRemove[type]);
	    }
	    else
	    {
		    inventoryToRemove.Remove(type);
		    Destroy(currentSlot.gameObject);
	    } 
    }


    
    
}