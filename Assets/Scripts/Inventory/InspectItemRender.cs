using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectItemRender : MonoBehaviour
{
    Slot slot;
    GameObject itemGameObjectForInspectPaulo;
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
        itemGameObjectForInspectPaulo = slot.itemGameObjectForInspectPaulo;

        Debug.Log("Spawn");
        instantiatedItem = Instantiate(itemGameObjectForInspectPaulo, this.transform.position, Quaternion.identity);
        //instantiatedItem.layer = 5;
    }

    public void ItemDestroy()
    {
        Debug.Log(instantiatedItem);
        GameObject.Destroy(instantiatedItem);
    }
}
