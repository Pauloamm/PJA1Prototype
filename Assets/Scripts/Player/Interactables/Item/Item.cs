using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName ="Interactable/Item")]

public class Item : Storable
{
    public override void StoreItem()
    {
        slotManager.AddSlot(this.gameObject);
        Destroy(this.gameObject);
    }
}