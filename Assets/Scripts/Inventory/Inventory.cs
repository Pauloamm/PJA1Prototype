using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
