using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public delegate void OnAttack();
    public OnAttack Attacking;

    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject bat;

    private float AttackTimer;

    public void Start()
    {
         Attacking += Func; 
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bat.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            bat.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        AttackTimer += Time.deltaTime;
        Attacking?.Invoke();     
    }

    private void Func()
    {
        if (bat.activeSelf == true)
        {          
            if (Input.GetMouseButtonDown(0) && AttackTimer >= 2f)
            {
                anim.SetBool("Attacking", true);
                AttackTimer = 0f;
            }
            if (Input.GetMouseButtonUp(0))            
                anim.SetBool("Attacking", false);           
        }
    }
}
