using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storable : MonoBehaviour
{
    // Slot manager
    [SerializeField] protected SlotManager slotManager;

    // List of actions for the item
    [SerializeField] protected List<Action> itemActions;

    // Item for inspect item menu and inventory icon
    [SerializeField] protected GameObject itemGameObjectForInspect;
    [SerializeField] protected Sprite icon;

    
    // // Probably delete later if image static
    // public int iD;
    // public string type;
    // public string description;

    
    // Acessers
    [SerializeField] public List<Action> ItemActions => itemActions;
    [SerializeField] public GameObject ItemGameObjectForInspect => itemGameObjectForInspect;
    [SerializeField] public Sprite Icon => icon;


    public abstract void StoreItem();
}