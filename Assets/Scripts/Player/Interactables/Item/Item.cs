using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName ="Interactable/Item")]

public class Item : MonoBehaviour, IStorable
{
	// -------------------------------------IStorable---------------------------------------------//

	// Slot manager
	[SerializeField] private SlotManager slotManager;

	// List of actions for the item
	[SerializeField] private List<Action> itemActions;

	// Item for inspect item menu and inventory icon
	[SerializeField] private GameObject itemGameObjectForInspect;
	[SerializeField] private Sprite icon;
	[SerializeField] private int quantity = 1;
	public string type = "Item";

	public SlotManager SlotManager => slotManager;
	public List<Action> ItemActions => itemActions;
	public GameObject ItemGameObjectForInspect => itemGameObjectForInspect;
	public Sprite Icon => icon;
	public int Quantity { get { return this.quantity; } set { quantity = value; } }
	public string Type { get { return this.type; } set { type = value; } }

    // -------------------------------------------------------------------------------------------//
    public void StoreItem()
    {
        slotManager.AddSlot(this.gameObject);
        Destroy(this.gameObject);
    }
}