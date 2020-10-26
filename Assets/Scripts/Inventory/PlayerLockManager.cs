using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockManager : MonoBehaviour
{
    [SerializeField] private UiStateManager uiStateManager;

    [SerializeField] private PlayerMovement playerMovement;


    void Awake()
    {
        uiStateManager.InventoryOpened += LockPlayerMovement;
        uiStateManager.InventoryClosed += UnlockPlayerMovement;
    }


    private void LockPlayerMovement()
    {
        playerMovement.enabled = false;
    }

    private void UnlockPlayerMovement()
    {
        playerMovement.enabled = true;
    }
}