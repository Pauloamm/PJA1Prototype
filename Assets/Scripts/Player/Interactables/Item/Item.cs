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


    public SlotManager SlotManager => slotManager;
    public List<Action> ItemActions => itemActions;
    public GameObject ItemGameObjectForInspect => itemGameObjectForInspect;
    public Sprite Icon => icon;

    // -------------------------------------------------------------------------------------------//
    public void StoreItem()
    {
        slotManager.AddSlot(this.gameObject);
        Destroy(this.gameObject);
    }
}