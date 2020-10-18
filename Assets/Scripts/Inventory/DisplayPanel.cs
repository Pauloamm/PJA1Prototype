using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPanel : MonoBehaviour
{
    public GameObject panel;
    public delegate GameObject OnClick();

    public event OnClick OnMouseClick;
    GameObject ze;

    void Awake()
    {
    }
    bool isOpen = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isOpen)
            {
                ze = OnMouseClick?.Invoke();
                if (ze != null)
                {
                    Debug.Log("a tua mae");
                    panel.transform.position = Input.mousePosition;
                    panel.SetActive(true);
                    ze.GetComponent<Slot>();
                    isOpen = !isOpen;
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
