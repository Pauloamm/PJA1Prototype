using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public delegate void OnPickUp(GameObject item);

    public event OnPickUp OnPickUpEvent;
    //[SerializeField]
    //Camera playerCamera;

    private void Update()
    {
        RaycastHit raycast;
        Ray ray = this.GetComponentInChildren<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        //Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out raycast, 5f))
        {
            GameObject objectHit = raycast.collider.gameObject;

            if (objectHit != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (objectHit.CompareTag("Item"))
                    {

                        OnPickUpEvent?.Invoke(objectHit);
                    }
                }
            }
        }
    }
}
