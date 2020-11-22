using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoBehaviour, IRaycastResponse
{
    [SerializeField]
    string requiredKey;

    [SerializeField]
    DragDoorRigidBody dragDoor;

    [SerializeField]
    Inventory inventory;

    public void OnRaycastSelect()
    {
        if (inventory.IsStored(requiredKey))
            dragDoor.UnlockJoints();

            dragDoor.OnSelect();
    }

    public void OnRaycastDiselect()
    {
        dragDoor.OnDiselect();
    }
}
