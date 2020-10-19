using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockManager : MonoBehaviour
{
    [SerializeField]
    UiStateManager uiStateManager;
    
    void Awake()
    {
        uiStateManager.InventoryOpened += LockPlayerMovement;
        uiStateManager.InventoryClosed += UnlockPlayerMovement;
    }


    private void LockPlayerMovement()
    {
        this.GetComponent<PlayerMovement>().enabled = false;
    }

    private void UnlockPlayerMovement()
    {
        this.GetComponent<PlayerMovement>().enabled = true;
    }

}
