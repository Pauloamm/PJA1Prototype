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
    SlotManager slotManager;

    public void OnRaycastSelect()
    {
        if (slotManager.IsStored(requiredKey))
            dragDoor.UnlockJoints();

            dragDoor.OnSelect();
    }

    public void OnRaycastDiselect()
    {
        dragDoor.OnDiselect();
    }
}
