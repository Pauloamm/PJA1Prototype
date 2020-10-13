using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    private bool inventoryEnabled;

    private int enabledSlots;
    SlotManager slotManager;

    //void Start()
    //{
    //    slotManager = GameObject.Find("ListContent").GetComponent<SlotManager>();
    //}

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            inventoryEnabled = !inventoryEnabled;

        if (inventoryEnabled)
            inventory.SetActive(true);
        else
            inventory.SetActive(false);
    }
}
