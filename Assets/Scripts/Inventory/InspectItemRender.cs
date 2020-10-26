using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectItemRender : MonoBehaviour
{
    Slot slot;
    GameObject itemGameObjectForInspect;
    GameObject instantiatedItem;

    [SerializeField]
    InspectMode inspectMode;

    void Awake()
    {
        //inspectMode.InspectClosedPaulo += ItemDestroy;
        inspectMode.InspectOpenedForLoadItem += ItemLoad;
        inspectMode.InspectClosed += ItemDestroy;
    }

    public void ItemLoad(Slot slot)
    {
        this.slot = slot;
        itemGameObjectForInspect = slot.itemGameObjectForInspect;

        Debug.Log("Spawn");
        instantiatedItem = Instantiate(itemGameObjectForInspect, this.transform.position, Quaternion.identity);
        //instantiatedItem.layer = 5;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ItemDestroy();
        }
    }

     void ItemDestroy()
    {
        GameObject.Destroy(instantiatedItem);
    }
}
