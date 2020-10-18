using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SlotSelection : MonoBehaviour 
{

    DisplayPanel displayPanel;

    void Awake()
    {
        displayPanel = GameObject.Find("DisplayPanel").GetComponent<DisplayPanel>();
    }

    public void OnPointerEnter()
    {
        displayPanel.OnMouseClick += bacalhau;
        Debug.Log("entrou");
    }
    public void OnPointerExit()
    {
        displayPanel.OnMouseClick -= bacalhau;
        Debug.Log("saiu");

    }

    GameObject bacalhau() => this.gameObject;
    

    //[SerializeField] GameObject panelPrefeb;
    //GameObject currentPanel;
    //DisplayPanel displayPanel;
    //Button currentButton;
    //bool isOpen = false;
    //void Awake()
    //{
    //    currentButton = this.GetComponent<Button>();
    //    currentButton.onClick.AddListener(ze);
    //}

    //void ze()
    //{
    //    if (!isOpen)
    //    {
    //        currentPanel = Instantiate(panelPrefeb);
    //        currentPanel.transform.parent = GameObject.Find("UI_Inventory").transform;
    //        currentPanel.transform.position = Input.mousePosition;
    //        isOpen = !isOpen;
    //    }
    //    else
    //    {
    //        Destroy(currentPanel);
    //        isOpen = !isOpen;
    //    }
    //}

}
