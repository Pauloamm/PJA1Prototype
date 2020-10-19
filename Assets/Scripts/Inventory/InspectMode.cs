using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InspectMode : MonoBehaviour
{
    public delegate void ChangeUI();
    public event ChangeUI InspectOpened;
    public event ChangeUI InspectClosed;

    //---------------
    public delegate void InspectOpenForLoadItem(Slot currentSlot);
    public event InspectOpenForLoadItem InspectOpenedForLoadItem;
    //---------------

    public delegate Slot SlotSelect();
    public event SlotSelect SlotSelected;

    [SerializeField]
    InspectItemRender inspectItemRender;

    [SerializeField]
    Slot currentSlot;

    [SerializeField]
    Button openInspectButton, leaveInspectButton;

    void OnEnable()
    {
        openInspectButton.onClick.AddListener(OpenInspectMode);
        leaveInspectButton.onClick.AddListener(CloseInspectMode);
    }

    private void OpenInspectMode()
    {
        InspectOpened?.Invoke();

        currentSlot = SlotSelected?.Invoke();

        InspectOpenedForLoadItem?.Invoke(currentSlot);

        //inspectItemRender.ItemLoad(currentSlot);

    }

    private void CloseInspectMode()
    {
        InspectClosed?.Invoke();
        //InspectClosedPaulo?.Invoke(null);
    }
}
