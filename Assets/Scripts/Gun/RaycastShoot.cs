using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletHole
{
    public float existenceCountdown = 2.0f;
    public GameObject pelletHoleObj;

    public PelletHole(GameObject newPelletHole)
    {
        this.pelletHoleObj = newPelletHole;
    }
}

public class PelletHoleManager
{
    List<PelletHole> pelletHoles;
    GameObject pelletHolePrefab;
    Camera holeCam;
    
    public PelletHoleManager()
    {
        pelletHoles = new List<PelletHole>();
        
        pelletHolePrefab = Resources.Load("Prefabs/Pellet Hole") as GameObject;
        
        holeCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void NewPelletHole(Vector3 hitPoint, GameObject hitObject)
    {
        GameObject newPelletHole = Object.Instantiate(pelletHolePrefab, hitPoint, holeCam.transform.rotation);
        newPelletHole.transform.parent = hitObject.transform;
        
        pelletHoles.Add(new PelletHole(newPelletHole));
    }
    
    public void CheckPelletHoles()
    {
        for (int i = 0; i < pelletHoles.Count; i++)
        {
            pelletHoles[i].existenceCountdown -= Time.deltaTime;

            if(pelletHoles[i].existenceCountdown <= 0)
            {
                GameObject.Destroy(pelletHoles[i].pelletHoleObj);
                pelletHoles.RemoveAt(i);
            }
        }
    }
}

public class RaycastShoot : MonoBehaviour
{
    public int bullets = 30;        // Bullets available in this gun
    public int magazineSize = 30;   // Size of the magazine
    public int pelletDamage = 1;    // Set the number of hitpoints that each pellet will take away from shot objects with a health script
    public float fireRate = 0.25f;  // Number in seconds which controls how often the player can fire
    public float recoil = 20.0f;    // Recoil angle after shooting
    public float kickback = 0.15f;  // Kickback intensity after shooting
    public float pelletSpreadRadiusMultiplier = 1.0f;   // Maximum spread radius per pellet (multiplier)
    public int pelletsPerBulletShot = 5;  // Number of pellets per bullet shoot
    public float gunRange = 50f; // Distance in Unity units over which the player can fire
    public float hitForce = 100f;   // Amount of force which will be added to objects with a rigidbody shot by the player
    public AudioClip[] gunSounds;
    
    private float nextShotCooldown; // Float to store the time left until the player will be allowed to fire again, after firing

    //[SerializeField]
    private Camera fpsCam;          // Holds a reference to the first person camera


    private RectTransform crosshair;    // Holds a reference to the crosshair's transform
    private AudioSource gunAudio;   // Holds a reference to the audio source which will play our shooting and reloading sound effects
    private PelletHoleManager pelletHoleManager;
    
    private Vector3 defaultLocalPosition;
    private Quaternion defaultLocalRotation;

    void Awake() // has to be Awake() or at least OnEnable() so that the Components can be instantiated in time, avoiding null reference exception errors
    {
        fpsCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Debug.Log(fpsCam);

        crosshair = GameObject.Find("CrosshairDot").GetComponent<RectTransform>();

        gunAudio = this.GetComponent<AudioSource>();
        
        pelletHoleManager = new PelletHoleManager();
        
        defaultLocalPosition = this.transform.localPosition;
        defaultLocalRotation = this.transform.localRotation;
        
    }

 
    void Update()
    {
        // // Draws a debugging ray starting at the center of our camera's viewport and ending at the distance of weaponRangee
        // Debug.DrawRay(fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f)), fpsCam.transform.forward * gunRange, Color.green);
    
        // Gradually restore position and rotation after shooting kickback and recoil, respectively
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, defaultLocalPosition, Time.deltaTime * 4f);
        this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, defaultLocalRotation, Time.deltaTime * 4f);
        
        // If there is a delay for the next shot
        if(nextShotCooldown > 0)
        {
            // Keep decreasing the delay
            nextShotCooldown -= Time.deltaTime;
        }

        pelletHoleManager.CheckPelletHoles();
    }
    
    public void Shooting()
    {
        // Check if enough time has elapsed since they last fired and if there is at least 1 bullet available
        if (nextShotCooldown <= 0 && bullets > 0) 
        {
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
                if(pelletsPerBulletShot > 1)
                {
                    // If there are multiple pellets per bullet shot, then spread them according to the maximum gun recoil
                    recoilSpreadFactor = recoil;
                }
                else
                {
                    // Else, if there's only 1 pellet per bullet shot, spread it according to the current gun recoil
                    float currentRecoil = 360.0f - this.transform.localEulerAngles.x;
                    if(currentRecoil >= 360f) currentRecoil = 0.0f;
                    recoilSpreadFactor = currentRecoil;
                }
                float pelletSpreadRadius = Random.Range(0.0f, recoilSpreadFactor / 400.0f) * pelletSpreadRadiusMultiplier;  // Pellet spread radius increases with the current weapon X rotation, due to recoil
                Vector3 pelletSpreadAngle = this.transform.TransformDirection(Random.insideUnitCircle.normalized);  // TransformDirection from local space to world space | .normalized turns it into .onUnitCircle
                Vector3 pelletDirection = this.transform.forward - (this.transform.forward.y - fpsCam.transform.forward.y) * 0.5f * Vector3.up + pelletSpreadAngle * pelletSpreadRadius;
                #endregion
                
                // Check if our raycast has hit anything
                if (Physics.Raycast (rayOrigin, pelletDirection, out hit, gunRange))
                {
                    // Shows a new bullet hole sprite object during a few seconds
                    pelletHoleManager.NewPelletHole(hit.point, hit.collider.gameObject);
                    
                    #region Test if "hit" is an Enemy
                    // Get a reference to an Enemy script attached to the collider we hit
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    
                    // Calc pellet's impact percentage according to the distance of the hit object
                    // (more distance => less impact, and vice-versa)
                    float impactPercentageWithDistance = 1 - hit.distance/gunRange;

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
                        hit.rigidbody.AddForce (-hit.normal * hitForce * impactPercentageWithDistance);
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
        }
    }

    public void ChangeMagazine(int bulletsInSpareMagazine)
    {
        // Play the reloading sound effect
        gunAudio.PlayOneShot(gunSounds[1]);

        // Change magazine
        bullets = bulletsInSpareMagazine;
    }
    
    /*
    public int ReloadingFullMagazine(int spareBullets)
    {
        int usedBullets = 0;

        // Check if there is any spare bullets and if this gun's magazine isn't already full
        if(spareBullets > 0 && bullets < magazineSize)
        {
            // Play the reloading sound effect
            gunAudio.PlayOneShot(gunSounds[1]);

            // Calc bullets needed to fully reload
            int bulletsNeededToFullyReload = magazineSize - bullets;

            // If there are enough spare bullets to fully reload
            if(bulletsNeededToFullyReload <= spareBullets)
            {
                bullets += bulletsNeededToFullyReload; // Fully reload gun magazine
                usedBullets = bulletsNeededToFullyReload; // Update number of spare bullets used
            }
            else
            {
                bullets += spareBullets; // Use all spare bullets to reload gun magazine
                usedBullets = spareBullets; // Update number of spare bullets used
            }

            Debug.Log($"RELOADED! - new bullets: {bullets} | spare bullets: {spareBullets - usedBullets}");
        }
        else
        {
            Debug.Log($"NO SPARE BULLETS AVAILABLE!");
        }
        
        return usedBullets;
    }
    */
    
    public void ZoomingIn(float zoomingInVelocity)
    {
        // Gradually zoom in
        fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, 37.5f, zoomingInVelocity);
        
        // Gradually increase crosshair
        crosshair.sizeDelta = Vector2.Lerp(crosshair.sizeDelta, new Vector2(10f, 10f), zoomingInVelocity);
    }
    
    public void ZoomingOut(float zoomingOutVelocity)
    {

        if(fpsCam != null && crosshair != null)
        {

            // Gradually zoom out
            fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, 45.0f, zoomingOutVelocity); 

            // Gradually decrease crosshair
            crosshair.sizeDelta = Vector2.Lerp(crosshair.sizeDelta, new Vector2(5f, 5f), zoomingOutVelocity);
        }

       
    }
}
