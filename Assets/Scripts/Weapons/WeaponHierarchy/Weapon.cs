
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
	NotOwned,
	Idle,
	Attacking,
	Reloading
}

public class Weapon : MonoBehaviour, IStorable
{

	// -------------------------------------IStorable---------------------------------------------//

	// Slot manager
	[SerializeField] protected SlotManager slotManager;

	// List of actions for the item
	[SerializeField] protected List<Action> itemActions;

	// Item for inspect item menu and inventory icon
	[SerializeField] protected GameObject itemGameObjectForInspect;
	[SerializeField] protected Sprite icon;
	[SerializeField] protected int remainingMagazines;
	public string type = "Weapon";

	public SlotManager SlotManager => slotManager;
	public List<Action> ItemActions => itemActions;
	public GameObject ItemGameObjectForInspect => itemGameObjectForInspect;
	public Sprite Icon => icon;
	public int Quantity { get { return this.remainingMagazines; } set { remainingMagazines = value; } }
	public string Type { get { return this.type; } set { type = value; } }



	// -------------------------------------------------------------------------------------------//


	// Magazine/bullets variables
	[SerializeField] protected int bulletsInCurrentMagazine;
	[SerializeField] protected int defaultMagazineSize = 10;
	[SerializeField] protected int bulletDamage = 10;

	// Variables for shooting
	[SerializeField] protected float defaultShotCooldown = 0.25f;  // Number in seconds which controls how often the player can fire
	[SerializeField] public float nextShotCooldown; // Float to store the time left until the player will be allowed to fire again, after firing

	// Bullet spread variables
	[SerializeField] protected float pelletSpreadRadiusMultiplier = 1.0f;   // Maximum spread radius per pellet (multiplier)

	// Weapon recoil effect variables
	[SerializeField] protected float recoil = 20.0f;    // Recoil angle after shooting
	[SerializeField] protected float weaponKickRecoil = 0.15f;  // Kickback intensity after shooting

	private readonly Vector3 defaultLocalPosition = new Vector3(0.47f, -0.12f, 0.7f);
	private readonly Quaternion defaultLocalRotation = Quaternion.identity;


	// Gun specific values
	[SerializeField] protected float gunRange = 50f; // Distance in Unity units over which the player can fire
	[SerializeField] protected float hitForce = 100f;   // Amount of force which will be added to objects with a rigidbody shot by the player

	// Sound effects for weapon
	[SerializeField] protected AudioClip[] gunSounds;
	[SerializeField] protected AudioSource gunAudio;   // Holds a reference to the audio source which will play our shooting and reloading sound effects


	// Camera variables
	[SerializeField] protected Camera playerCamera;          // Holds a reference to the first person camera
	public Vector3 rayOrigin;

	// Weapon Events
	public delegate void OnShooting(int bullets);
	public event OnShooting Shot; // ESTÁ A SER USADO ???

	// Visual shot hit effect manager
	protected PelletHoleManager pelletHoleManager;
	[SerializeField] protected ParticleSystem muzzleFlash;
	[SerializeField] public WeaponState weaponState = WeaponState.NotOwned; // SERIALIZE FOR DEBUGGING
	[SerializeField] private Animation weaponAnimations;

	// Keycode associated to weapon for fast equiping
	[SerializeField] public KeyCode weaponKeyCode;

	// Weapon manager
	[SerializeField] private WeaponManager weaponManager;


	//ACESSORS
	public int BulletsInCurrentMagazine => bulletsInCurrentMagazine;

	public int DefaultMagazineSize => defaultMagazineSize;

	// Weapon localPosition and localRotation Lerping
	protected float lerpInitialTime;
	protected float lerpCompletedPercentage;


	private void Awake()
	{
		pelletHoleManager = new PelletHoleManager();
		bulletsInCurrentMagazine = defaultMagazineSize;
	}


	private void Update()
	{
		switch(weaponState)
		{
			case WeaponState.Idle:
				if(lerpCompletedPercentage < 1.0f)
				{
					ResetLocalPosAndRot(false);

					/*Debug.Log(Vector3.Distance(this.transform.localPosition, defaultLocalPosition)
					+ " | " + Quaternion.Angle(this.transform.localRotation, defaultLocalRotation));
					Debug.Log(lerpCompletedPercentage);*/
				}

				break;
			
			case WeaponState.Attacking:
				// If there is a delay for the next shot
				if (nextShotCooldown > 0)
				{
					// Keep decreasing the delay
					nextShotCooldown -= Time.deltaTime;
				}
				else
				{
					weaponState = WeaponState.Idle;
				}
				
				ResetLocalPosAndRot(false);
				
				break;

			case WeaponState.Reloading:
				if (!weaponAnimations.isPlaying)
				{
					weaponState = WeaponState.Idle;
				}

				break;
		}
	}
	public virtual bool Attack()
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

		// Declare a raycast hit to store information about what our raycast has hit
		RaycastHit hit;

		Vector3 pelletDirection = WeaponSpread();

		// Check if our raycast has hit anything
		if (Physics.Raycast(rayOrigin, pelletDirection, out hit, gunRange))
		{

			// Get a reference to an Enemy script attached to the collider we hit
			Enemy enemy = hit.collider.GetComponent<Enemy>();

			float impactPercentageWithDistance = WeaponForceCalculation(hit);

			DoDamage(enemy, impactPercentageWithDistance);

			ApplyWeaponForce(hit, impactPercentageWithDistance);

			HoleCreation(hit);
		}
		
		Debug.Log($"bullets: {bulletsInCurrentMagazine}");
		ShotEvent();
		
		PlayShootingSound();
		muzzleFlash.Play();

		WeaponRecoil();

		return true;
	}



	//CReates the holes
	//protected void HoleCreation(RaycastHit hit) => pelletHoleManager.NewPelletHole(hit.point, hit.collider.gameObject);

	protected void HoleCreation(RaycastHit hit)
	{
		GameObject pelletHolePrefab = Resources.Load("Prefabs/Pellet Hole") as GameObject;
		
		GameObject newPelletHole = Object.Instantiate(pelletHolePrefab, hit.point - playerCamera.transform.forward * 0.01f, playerCamera.transform.rotation);
        
        newPelletHole.transform.parent = hit.collider.gameObject.transform;

		Destroy(newPelletHole, 2.0f);
	}
	
	//Invokes the shot Event
	protected void ShotEvent()
	{
		Shot?.Invoke(bulletsInCurrentMagazine); // ESTÁ A SER USADO ???
	}

	//Responsable for the weaponRecoil
	protected void WeaponRecoil()
	{
		ResetLocalPosAndRot(true);
		this.transform.position -= this.transform.forward * weaponKickRecoil;
		this.transform.Rotate(-recoil, 0f, 0f);
	}

	//Resets localPosition and localRotation to default values
	public void ResetLocalPosAndRot(bool instantReset)
	{
		if (instantReset)
		{
			// Instant reset
			this.transform.localPosition = defaultLocalPosition;
			this.transform.localRotation = defaultLocalRotation;
		}
		else
		{
			float lerpMaxTime = defaultShotCooldown * 5;
			float lerpElapsedTime = Time.time - lerpInitialTime;
            lerpCompletedPercentage = lerpElapsedTime / lerpMaxTime;
			
			// Gradual reset (to use after weapon attacked)
			this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, defaultLocalPosition, lerpCompletedPercentage);
			this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, defaultLocalRotation, lerpCompletedPercentage);
		}
	}


	//Changes the magazine for the current weapon with a magazine thats has random bullets in her
	public bool ChangeMagazine()
	{
		if (weaponState != WeaponState.Idle) return false;

		weaponState = WeaponState.Reloading;
		ResetLocalPosAndRot(true);
		weaponAnimations.Play("ReloadAnimation");

		float fillPercentage = RandomNonLinearProbabilityPercentage();
		bulletsInCurrentMagazine = (int)(defaultMagazineSize * fillPercentage);
		remainingMagazines--;

		// Play the reloading sound effect
		gunAudio.PlayOneShot(gunSounds[1]);

		return true;
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


	//Stores the weapon on the inventory
	public void StoreItem()
	{
		slotManager.AddSlot(this.gameObject);
		weaponManager.AddWeapon(this.gameObject, playerCamera.transform);

	}

	// Play the shooting sound effect
	protected void PlayShootingSound() => gunAudio.PlayOneShot(gunSounds[0]);


	//Responsable for doing the math for the weapon Spread
	protected virtual Vector3 WeaponSpread()
	{
		// spread pellet according to the current gun recoil
		float recoilSpreadFactor;
		float currentRecoil = 360.0f - this.transform.localEulerAngles.x;
		if (currentRecoil >= 360f) currentRecoil = 0.0f;
		recoilSpreadFactor = currentRecoil;

		float pelletSpreadRadius = Random.Range(0.0f, recoilSpreadFactor / 400.0f) * pelletSpreadRadiusMultiplier;  // Pellet spread radius increases with the current weapon X rotation, due to recoil
		Vector3 pelletSpreadAngle = this.transform.TransformDirection(Random.insideUnitCircle.normalized);  // TransformDirection from local space to world space | .normalized turns it into .onUnitCircle
		Vector3 pelletDirection = this.transform.forward - (this.transform.forward.y - playerCamera.transform.forward.y) * 0.5f * Vector3.up + pelletSpreadAngle * pelletSpreadRadius;

		return pelletDirection;
	}


	protected float WeaponForceCalculation(RaycastHit hit) => 1 - hit.distance / gunRange;



	protected void DoDamage(Enemy enemy, float impactPercentageWithDistance)
	{
		// If there was an Enemy script attached
		if (enemy != null && !enemy.isDying())
		{
			// Call the damage function of the Enemy script, passing in our pelletDamage
			enemy.Damage(bulletDamage * impactPercentageWithDistance);
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
	}
}

