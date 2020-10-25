using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject bat;

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            bat.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            bat.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (bat.activeSelf==true)
        {
           
            if (Input.GetMouseButtonDown(0))
                anim.SetBool("Attacking", true);
            if (Input.GetMouseButtonUp(0))
                anim.SetBool("Attacking", false);
        }
        
    }
}
