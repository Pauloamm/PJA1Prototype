using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectItemRender : MonoBehaviour
{


    // Current slot for inspect
    public Slot slotForInspect;

    // Script for the event to load the gameObject from current slot
    [SerializeField] private DisplayPanel displayPanel;
    
    // Item from slot to inspect 
    GameObject instantiatedItem;


    // Event for leaving inspect mode
    public delegate void ChangeUI();
    public event ChangeUI InspectClosed;
    
    
    void Awake()
    {
        displayPanel.inspectOpen += ItemLoad;
        InspectClosed += ItemDestroy;

    }

    public void ItemLoad()
    {
        GameObject itemGameObjectForInspect = slotForInspect.itemGameObjectForInspect;

        Debug.Log("Spawn");
        instantiatedItem = Instantiate(itemGameObjectForInspect, this.transform.position, Quaternion.identity);
    }

    void Update()
    {
        // Used for rotation of item
        slotForInspect?.slotActions[0].RespectiveAction(instantiatedItem);

        // Check for Esc key to leave (destroys item and changes to InventoryUI)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InspectClosed?.Invoke();
            
        }
    }

    void ItemDestroy()
    {
        GameObject.Destroy(instantiatedItem);
    }
}