using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiStateManager : MonoBehaviour
{
    public GameObject inventory;
    private bool inventoryEnabled;


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
