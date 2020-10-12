using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    static public Ray playerRay ;
    // Update is called once per frame

    private float range = 10f;

    void Update()
    {
        playerRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(transform.position, transform.forward * range);
    }
}
