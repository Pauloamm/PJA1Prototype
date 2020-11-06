using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Weapon : Storable
{
<<<<<<< Updated upstream
    private int numberOfBullets;

    public int bullets = 30;        // Bullets available in this gun
    public int magazineSize = 30;   // Size of the magazine
    public int pelletDamage = 1;    // Set the number of hitpoints that each pellet will take away from shot objects with a health script
    public float fireRate = 0.25f;  // Number in seconds which controls how often the player can fire
=======
    //private int numberOfBullets;
    [SerializeField]
    public int BulletsinCurrentMagazine = 10;        // Bullets available in this gun
    [SerializeField]
    public int DefaultMagazineSize = 10;   // Size of the magazine
    [SerializeField]
    public int BulletDamage = 1;    // Set the number of hitpoints that each pellet will take away from shot objects with a health script
    [SerializeField]
    public float defaultShotCooldown = 0.25f;  // Number in seconds which controls how often the player can fire
    [SerializeField]
>>>>>>> Stashed changes
    public float recoil = 20.0f;    // Recoil angle after shooting
    [SerializeField]
    public float WeaponKickRecoil = 0.15f;  // Kickback intensity after shooting
    [SerializeField]
    public float pelletSpreadRadiusMultiplier = 1.0f;   // Maximum spread radius per pellet (multiplier)
    [SerializeField]
    public int pelletsPerBulletShot = 5;  // Number of pellets per bullet shoot
    [SerializeField]
    public float gunRange = 50f; // Distance in Unity units over which the player can fire
    [SerializeField]
    public float hitForce = 100f;   // Amount of force which will be added to objects with a rigidbody shot by the player
    [SerializeField]
    public AudioClip[] gunSounds;

    [SerializeField]
    public float nextShotCooldown; // Float to store the time left until the player will be allowed to fire again, after firing

    [SerializeField]
    protected Camera fpsCam;          // Holds a reference to the first person camera

    //RayCastOrigi
    public Vector3 rayOrigin;

    [SerializeField]
    private AudioSource gunAudio;   // Holds a reference to the audio source which will play our shooting and reloading sound effects
<<<<<<< Updated upstream
    
    
=======

    public delegate void OnShooting(int bullets);
    public event OnShooting shot;

    [SerializeField]
>>>>>>> Stashed changes
    private PelletHoleManager pelletHoleManager;

    // Default position and rotation for recoil animation
    private Vector3 defaultLocalPosition;
    private Quaternion defaultLocalRotation;


    // Keycode associated to weapon for fast equiping
    [SerializeField] public KeyCode weaponKeyCode;
    
    // Equiping manager
    [SerializeField] private WeaponManager weaponManager;
<<<<<<< Updated upstream
    
=======

    private void Awake()
    {
        pelletHoleManager = new PelletHoleManager();
    }
>>>>>>> Stashed changes
    private void Update()
    {
        // Gradually restore position and rotation after shooting kickback and recoil, respectively
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, defaultLocalPosition, Time.deltaTime * 4f);
        this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, defaultLocalRotation, Time.deltaTime * 4f);

        // If there is a delay for the next shot
        if (nextShotCooldown > 0)
        {
            // Keep decreasing the delay
            nextShotCooldown -= Time.deltaTime;
        }
    }
    public virtual void Attacking()
    {
<<<<<<< Updated upstream
        
=======
>>>>>>> Stashed changes
        // Check if enough time has elapsed since they last fired and if there is at least 1 bullet available
        if (nextShotCooldown > 0 || BulletsinCurrentMagazine <= 0) return;

        // Update the time when our player can fire next
        nextShotCooldown = defaultShotCooldown;

        PlayShootingSound();

        // Create a vector at the center of our camera's viewport
        rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        // Reduce the bullets available
        BulletsinCurrentMagazine--;

        // Declare a raycast hit to store information about what our raycast has hit
        RaycastHit hit;

        Vector3 pelletDirection = WeaponSpread();

        // Check if our raycast has hit anything
        if (Physics.Raycast(rayOrigin, pelletDirection, out hit, gunRange))
        {
<<<<<<< Updated upstream
            // Update the time when our player can fire next
            nextShotCooldown = fireRate;

            // Play the shooting sound effect
            gunAudio.PlayOneShot(gunSounds[0]);

            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // Reduce the bullets available
            bullets--;

            for (int i = 0; i < pelletsPerBulletShot; i++)
            {
                // Declare a raycast hit to store information about what our raycast has hit
                RaycastHit hit;

                #region Calc random pellet spread
                // Calculate a random direction spread for this pellet
                float recoilSpreadFactor;
                if (pelletsPerBulletShot > 1)
                {
                    // If there are multiple pellets per bullet shot, then spread them according to the maximum gun recoil
                    recoilSpreadFactor = recoil;
                }
                else
                {
                    // Else, if there's only 1 pellet per bullet shot, spread it according to the current gun recoil
                    float currentRecoil = 360.0f - this.transform.localEulerAngles.x;
                    if (currentRecoil >= 360f) currentRecoil = 0.0f;
                    recoilSpreadFactor = currentRecoil;
                }
                float pelletSpreadRadius = Random.Range(0.0f, recoilSpreadFactor / 400.0f) * pelletSpreadRadiusMultiplier;  // Pellet spread radius increases with the current weapon X rotation, due to recoil
                Vector3 pelletSpreadAngle = this.transform.TransformDirection(Random.insideUnitCircle.normalized);  // TransformDirection from local space to world space | .normalized turns it into .onUnitCircle
                Vector3 pelletDirection = this.transform.forward - (this.transform.forward.y - fpsCam.transform.forward.y) * 0.5f * Vector3.up + pelletSpreadAngle * pelletSpreadRadius;
                #endregion

                // Check if our raycast has hit anything
                if (Physics.Raycast(rayOrigin, pelletDirection, out hit, gunRange))
                {
                    // Shows a new bullet hole sprite object during a few seconds
                    //pelletHoleManager.NewPelletHole(hit.point, hit.collider.gameObject);

                    #region Test if "hit" is an Enemy
                    // Get a reference to an Enemy script attached to the collider we hit
                    Enemy enemy = hit.collider.GetComponent<Enemy>();

                    // Calc pellet's impact percentage according to the distance of the hit object
                    // (more distance => less impact, and vice-versa)
                    float impactPercentageWithDistance = 1 - hit.distance / gunRange;

                    // If there was an Enemy script attached
                    if (enemy != null && !enemy.isDying())
                    {
                        // Call the damage function of the Enemy script, passing in our pelletDamage
                        enemy.Damage(pelletDamage * impactPercentageWithDistance);
                    }

                    // Check if the object we hit has a rigidbody attached
                    if (hit.rigidbody != null)
                    {
                        // Add force to the rigidbody we hit, in the direction from which it was hit
                        hit.rigidbody.AddForce(-hit.normal * hitForce * impactPercentageWithDistance);
                    }
                    #endregion
                }
            }

            // Apply shooting kickback and recoil (after reseting position and rotation to default values)
            this.transform.localRotation = defaultLocalRotation;
            this.transform.localPosition = defaultLocalPosition;
            this.transform.localPosition -= this.transform.forward * kickback;
            this.transform.Rotate(-recoil, 0f, 0f);

            //fpsCam.fieldOfView = 61.0f;

            Debug.Log($"bullets: {bullets}");
=======
            // Get a reference to an Enemy script attached to the collider we hit
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            float impactPercentageWithDistance = WeaponForceCalculation(hit);

            DoDamage(enemy, impactPercentageWithDistance);

            ApplyWeaponForce(hit, impactPercentageWithDistance);

            HoleCreation(hit);
>>>>>>> Stashed changes
        }

        WeaponRecoil();

        //fpsCam.fieldOfView = 61.0f;

        Debug.Log($"bullets: {BulletsinCurrentMagazine}");
        ShotEvent();
    }

<<<<<<< Updated upstream
    
    
=======
    //CReates the holes
    protected void HoleCreation(RaycastHit hit) => pelletHoleManager.NewPelletHole(hit.point, hit.collider.gameObject);

    //Invokes the shot Event
    protected void ShotEvent()
    {
        shot?.Invoke(BulletsinCurrentMagazine);
    }

    //Responsable for the weaponRecoil
    protected void WeaponRecoil()
    {
        this.transform.localRotation = defaultLocalRotation;
        this.transform.localPosition = defaultLocalPosition;
        this.transform.localPosition -= this.transform.forward * WeaponKickRecoil;
        this.transform.Rotate(-recoil, 0f, 0f);
    }

    //OnPickUp overrrides the default values
>>>>>>> Stashed changes
    public void OnPickUpDefaultInit(Quaternion localRotation, Vector3 localPosition)
    {
        defaultLocalRotation = localRotation;
        defaultLocalPosition = localPosition;
    }

<<<<<<< Updated upstream
   
=======
    //Changes the magazine for the current weapon with a magazine thats has random bullets in her
>>>>>>> Stashed changes
    public void ChangeMagazine(int spareMagazines)
    {
        if(spareMagazines>0)
        {
            float fillPercentage = RandomNonLinearProbabilityPercentage();
            BulletsinCurrentMagazine = (int)(DefaultMagazineSize * fillPercentage);
            // Play the reloading sound effect
            gunAudio.PlayOneShot(gunSounds[1]);
        }

    }

    //Calculates the % of next magazines bullets 
    private float RandomNonLinearProbabilityPercentage()
    {
        float randNonLinearProbabilityPercentage = 0.00f;

        for (int i = 1; i <= 4; i++)
        {
            randNonLinearProbabilityPercentage = UnityEngine.Random.Range(randNonLinearProbabilityPercentage, 0.25f * i);
        }

        return randNonLinearProbabilityPercentage;
    }

<<<<<<< Updated upstream
    
    
=======

    //Stores the weapon on the inventory
>>>>>>> Stashed changes
    public override void StoreItem()
    {
        slotManager.AddSlot(this.gameObject);
        weaponManager.AddWeapon(this.gameObject, fpsCam.transform);
<<<<<<< Updated upstream
        
=======
    }

    //Plays the sound 
    // Play the shooting sound effect
    protected void PlayShootingSound()=> gunAudio.PlayOneShot(gunSounds[0]);
  

    //Responsable for doing the math for the weapon Spread
    protected virtual Vector3 WeaponSpread()
    {
        float recoilSpreadFactor;
        // Else, if there's only 1 pellet per bullet shot, spread it according to the current gun recoil
        float currentRecoil = 360.0f - this.transform.localEulerAngles.x;
        if (currentRecoil >= 360f) currentRecoil = 0.0f;
        recoilSpreadFactor = currentRecoil;

        float pelletSpreadRadius = Random.Range(0.0f, recoilSpreadFactor / 400.0f) * pelletSpreadRadiusMultiplier;  // Pellet spread radius increases with the current weapon X rotation, due to recoil
        Vector3 pelletSpreadAngle = this.transform.TransformDirection(Random.insideUnitCircle.normalized);  // TransformDirection from local space to world space | .normalized turns it into .onUnitCircle
        Vector3 pelletDirection = this.transform.forward - (this.transform.forward.y - fpsCam.transform.forward.y) * 0.5f * Vector3.up + pelletSpreadAngle * pelletSpreadRadius;

        return pelletDirection;
    }

    //Calculates the force apllied from the current gun
    // Calc pellet's impact percentage according to the distance of the hit object
    // (more distance => less impact, and vice-versa)
    protected float WeaponForceCalculation(RaycastHit hit) =>  1 - hit.distance / gunRange;
   


    protected void DoDamage(Enemy enemy, float impactPercentageWithDistance)
    {
        // If there was an Enemy script attached
        if (enemy != null && !enemy.isDying())
        {
            // Call the damage function of the Enemy script, passing in our pelletDamage
            enemy.Damage(BulletDamage * impactPercentageWithDistance);
        }
    }


    protected void ApplyWeaponForce(RaycastHit hit, float impactPercentageWithDistance)
    {
        // Check if the object we hit has a rigidbody attached
        if (hit.rigidbody != null)
        {
            // Add force to the rigidbody we hit, in the direction from which it was hit
            hit.rigidbody.AddForce(-hit.normal * hitForce * impactPercentageWithDistance);
        }
>>>>>>> Stashed changes
    }
}

