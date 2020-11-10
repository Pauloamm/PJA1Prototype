using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int pelletsPerBulletShot = 5;  // Number of pellets per bullet shoot

	void Awake()
	{
		this.type = "Shotgun";
		pelletHoleManager = new PelletHoleManager();
        bulletsInCurrentMagazine = defaultMagazineSize;
	}

	public override bool Attack()
    {
        // Check if enough time has elapsed since they last fired and if there is at least 1 bullet available
        if (weaponState != WeaponState.Idle || bulletsInCurrentMagazine <= 0) return false;

        weaponState = WeaponState.Attacking;
		// Trigger lerp with initial conditions
		lerpInitialTime = Time.time;
		lerpCompletedPercentage = 0f;

        // Update the time when our player can fire next
        nextShotCooldown = defaultShotCooldown;

        // Create a vector at the center of our camera's viewport
        rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        // Reduce the bullets available
        bulletsInCurrentMagazine--;

        for (int i = 0; i < pelletsPerBulletShot; i++)
        {
            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            Vector3 pelletDirection = WeaponSpread();

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, pelletDirection, out hit, gunRange))
            {
                // Get a reference to an Enemy script attached to the collider we hit
                Enemy enemy = hit.collider.GetComponent<Enemy>();

                float impactPercentageWithDistance = WeaponForceCalculation(hit);

                base.DoDamage(enemy, impactPercentageWithDistance);

                base.ApplyWeaponForce(hit, impactPercentageWithDistance);

                base.HoleCreation(hit);
            }
        }

        Debug.Log($"bullets: {bulletsInCurrentMagazine}");
        base.ShotEvent();

        base.PlayShootingSound();
        base.muzzleFlash.Play();
        
        base.WeaponRecoil();

		return true;
    }


    protected override Vector3 WeaponSpread()
    {
		// spread pellet according to the maximum gun recoil
        float recoilSpreadFactor = recoil;
        
        float pelletSpreadRadius = Random.Range(0.0f, recoilSpreadFactor / 400.0f) * pelletSpreadRadiusMultiplier;  // Pellet spread radius increases with the current weapon X rotation, due to recoil
        Vector3 pelletSpreadAngle = this.transform.TransformDirection(Random.insideUnitCircle.normalized);  // TransformDirection from local space to world space | .normalized turns it into .onUnitCircle
        Vector3 pelletDirection = this.transform.forward - (this.transform.forward.y - playerCamera.transform.forward.y) * 0.5f * Vector3.up + pelletSpreadAngle * pelletSpreadRadius;
        
        return pelletDirection;
    }
}
