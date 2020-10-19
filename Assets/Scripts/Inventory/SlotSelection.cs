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
        displayPanel.OnMouseClick += GameObjectReturns;
        Debug.Log("entrou");
    }
    public void OnPointerExit()
    {
        displayPanel.OnMouseClick -= GameObjectReturns;
        Debug.Log("saiu");

    }

    GameObject GameObjectReturns() => this.gameObject;
    

}
