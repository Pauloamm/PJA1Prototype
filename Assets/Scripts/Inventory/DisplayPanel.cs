﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPanel : MonoBehaviour
{
    public GameObject panel;

    // Returns the slot in which the mouse is hovering
    public delegate GameObject OnClick();
    public event OnClick OnMouseClick;
    
    // Slot selected
    GameObject slotSelected;
    Slot currentSlot;
    private List<Action> currentSlotActions;

    
    [SerializeField]
    InspectMode inspectMode;

    [SerializeField] private GameObject buttonPrefab;

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
                // Gets slot where mouse is selected 
                slotSelected = OnMouseClick?.Invoke();
                if (slotSelected != null)
                {
                    
                    currentSlot = slotSelected.GetComponent<Slot>(); // Get Slot
                    currentSlotActions = currentSlot.slotActions;
                    CreatePanelWithActions();
                    
                    panel.transform.position = Input.mousePosition; // Mete o o painel junto ao rato
                    panel.SetActive(true); // ativa painel

                    isOpen = !isOpen; // Altera o bool do painel 

                }
            }
            else
            {
                panel.SetActive(false);
                Transform childPannel = transform.GetChild(0);

                for (int i = 0; i < childPannel.childCount; i++)
                {
                    Destroy(childPannel.GetChild(i).gameObject);
                }
                
                
                isOpen = !isOpen;
            }

        }
    }


    private Slot ReturnSlot() => currentSlot;

    private void CreatePanelWithActions()
    {
        foreach (Action action in currentSlotActions)
        {
            // CREATE BUTTON
            Button newButton = Instantiate(buttonPrefab, transform.GetChild(0)).GetComponent<Button>();
            
            // CHANGES CHARECTERISTICS
            newButton.GetComponentInChildren<Text>().text = action.name;
            
            
            // button.onclick(action.RespectiveAction)
            newButton.onClick.AddListener(
                delegate { action.RespectiveAction(slotSelected); });


        }
    }

    
    
}
