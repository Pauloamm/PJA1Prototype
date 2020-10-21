using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPanel : MonoBehaviour
{
    public GameObject panel;

    public delegate GameObject OnClick();
    public event OnClick OnMouseClick;
    GameObject slotSelected;
    Slot currentSlot;

    [SerializeField]
    InspectMode inspectMode;

    public delegate void Inspect();
    public event Inspect Inspected;

    bool isOpen = false;
    void Awake()
    {
        inspectMode.SlotSelected += ReturnSlot;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isOpen)
            {
                slotSelected = OnMouseClick?.Invoke();
                if (slotSelected != null)
                {
                    panel.transform.position = Input.mousePosition; // Mete o o painel junto ao rato
                    panel.SetActive(true); // ativa painel
                    currentSlot = slotSelected.GetComponent<Slot>(); // Get Slot

                    isOpen = !isOpen; // Altera o bool do painel 

                }
            }
            else
            {
                panel.SetActive(false);
                isOpen = !isOpen;
            }

        }
    }


    private Slot ReturnSlot() => currentSlot;
    
}
