﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    List<GameObject> slots;
    
    // Prefab slot for instantiate
    [SerializeField] GameObject referenceSlot;


	private void Awake()
    {
        slots = new List<GameObject>();
    }

    public void AddSlot(GameObject item)
    {
        // Gets script from pickUp object
        Storable itemInfo = item.GetComponent<Storable>();
        
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
    
    

    private void StoreSlotProperties(Slot newSlotScript, Storable itemInfo, GameObject newSlotObject)
    {

        // Item for inspect item menu and inventory icon
        newSlotScript.itemGameObjectForInspect = itemInfo.ItemGameObjectForInspect;
        newSlotScript.icon = itemInfo.Icon;

        // // Probably delete later if image static
        // newSlotScript.iD = itemInfo.iD;
        // newSlotScript.type = itemInfo.type;
        // newSlotScript.description = itemInfo.description;

        newSlotScript.slotActions = itemInfo.ItemActions;
        

        // Adds slot script to the list
        slots.Add(newSlotObject);
    }
    
    private void StoreImageInSlotObject(GameObject currentSlot, Slot tempSlot)
    {
        currentSlot.GetComponent<Image>().sprite = tempSlot.icon;

    }

	public void RemoveFromList(GameObject currentSlot) => slots.Remove(currentSlot);

}
