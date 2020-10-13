using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    private bool inventoryEnabled;

    private int slotsCount;
    private int enabledSlots;
    public GameObject slotHolder;
    SlotManager slotManager;

    void Start()
    {
        slotManager = GameObject.Find("ListContent").GetComponent<SlotManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            inventoryEnabled = !inventoryEnabled;

        if (inventoryEnabled)
            inventory.SetActive(true);
        else
            inventory.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;

            slotManager.AddSlot(itemPickedUp);
        }
    }
}
