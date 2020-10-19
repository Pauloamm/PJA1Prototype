using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPanel : MonoBehaviour
{
    public GameObject panel;

    public delegate GameObject OnClick();
    public event OnClick OnMouseClick;
    GameObject slotSelected;

    public delegate void Inspect();
    public event Inspect Inspected;

    bool isOpen = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isOpen)
            {
                slotSelected = OnMouseClick?.Invoke();
                if (slotSelected != null)
                {
                    panel.transform.position = Input.mousePosition;
                    panel.SetActive(true);
                    slotSelected.GetComponent<Slot>();
                    isOpen = !isOpen;

                    //Inspected?.Invoke();
                }
            }
            else
            {
                panel.SetActive(false);
                isOpen = !isOpen;
            }

        }
    }
}
