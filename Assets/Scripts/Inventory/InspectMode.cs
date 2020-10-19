using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InspectMode : MonoBehaviour
{
    public delegate void ChangeUI();
    public event ChangeUI InspectOpened;
    public event ChangeUI InspectClosed;

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
    }

    private void CloseInspectMode()
    {
        InspectClosed?.Invoke();
    }
}
