using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Weapon
{


    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject bat;
    public void Awake()
    {
        this.type = "Bat";
    }
    public override void Attacking()
    {
        if (nextShotCooldown > 0) return;
        
            anim.SetTrigger("OnAttacking");

            nextShotCooldown = defaultShotCooldown;

            rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, gunRange))
            {
                // Get a reference to an Enemy script attached to the collider we hit
                Enemy enemy = hit.collider.GetComponent<Enemy>();

                float impactPercentageWithDistance = WeaponForceCalculation(hit);

                DoDamage(enemy, impactPercentageWithDistance);

            }
        
    
    }


    public override void ChangeMagazine()
    {
      //pra nao dar erro ao fazer reload no bastao
    }

}
