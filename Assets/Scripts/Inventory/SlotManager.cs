using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{

    List<GameObject> slots;

    [SerializeField] GameObject referenceSlot;

    private void Awake()
    {
        slots = new List<GameObject>();
    }


    public void AddSlot(GameObject item)
    {
        Item itemItem = item.GetComponent<Item>();
        GameObject temp = Instantiate(referenceSlot, this.transform);
        Slot tempSlot = temp.GetComponent<Slot>();

        tempSlot.item = item;
        tempSlot.iD = itemItem.iD;
        tempSlot.type = itemItem.type;
        tempSlot.description = itemItem.description;
        tempSlot.icon = itemItem.icon;

        slots.Add(temp);

        DisplaySlotUpdate(temp, tempSlot);
        Debug.Log("slot Added");
    }


    private void DisplaySlotUpdate(GameObject currentSlot, Slot tempSlot)
    {
        currentSlot.GetComponent<Image>().sprite = tempSlot.icon;
    }
}
