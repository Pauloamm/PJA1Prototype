using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IStorable
{
    // List of actions for the item
    [SerializeField]
    public List<Action> slotActions;

    public string actionSlot;
    
    // Item for inspect item menu and inventory icon
    public GameObject itemGameObjectForInspect;
    public Sprite icon;
	public int quantity = 1;

    // Probably delete later if image static
    public bool empty;
    public int iD;
    public string type;
    public string description;

	public List<Action> ItemActions => slotActions;

	public GameObject ItemGameObjectForInspect => itemGameObjectForInspect;

	public Sprite Icon => icon;

	public string Type { get => this.type; set => this.type = value; }
	int IStorable.Quantity { get => this.quantity; set => this.quantity = value; }

	public void StoreItem()
	{
		throw new System.NotImplementedException();
	}
}
