using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    UiStateManager uiStateManager;
    [SerializeField]
    Camera playerCamera, inspectCamera;
    [SerializeField]
    DisplayPanel displayPanel;
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject backButton;

    [SerializeField]
    InspectMode inspectMode;

    void Awake()
    {
        uiStateManager.InventoryOpened += ToInventory;
        uiStateManager.InventoryClosed += ToIngame;
        inspectMode.InspectOpened += ToInspect;
        inspectMode.InspectClosed += ToInventory;
    }

    // Inicial State do caralho que o foda
    void Start()
    {
        ToIngame();
    }

    public void ToInspect()
    {
        EnableMouse(); // Enables mouse cursor

        inspectCamera.depth = 2; // Changes Camera to Inspect Camera
        inventory.SetActive(false); // Turns OFF Inventory 
        backButton.SetActive(true); // Turns ON Back Button 
    }
    public void ToInventory()
    {
        EnableMouse(); // Enables mouse cursor

        inspectCamera.depth = 0; // Changes Camera to Main Camera
        inventory.gameObject.SetActive(true);   // Turns ON Inventory 
        backButton.gameObject.SetActive(false); // Turns OFF Back Button 
    }
    public void ToIngame()
    {
        DisableMouse(); // Disables mouse cursor

        inspectCamera.depth = 0; // Changes Camera to Main Camera
        inventory.gameObject.SetActive(false);  // Turns OFF Inventory 
        backButton.gameObject.SetActive(false); // Turns ON Back Button 
    }

    private void EnableMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void DisableMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

}
