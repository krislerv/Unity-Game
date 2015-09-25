using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Player controls
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
	private float normalizedHorizontalSpeed = 0;
	private bool goingLeft;

	// Player knockback
	public float damageKnockback = 8f;
	public float knockbackDuration = 0.15f;
	private float knockbackTime;
	private Vector3 knockback;

	// Weapons
	public GameObject[] weaponPrefabs;

	// Action bar
	public GameObject actionBar;

	// Attacking
	private bool attacking = false;	// If player is currently attacking
	private float attackTime;		// Countdown from beginning of attack to end of attack
	private float attackLength;		// Length of attack

	// Player health
	public GameObject healthbar;
	public int maxHealth;
	private int currentHealth;

	// Player experience
	public GameObject experiencebar;
	public int maxExperiencePoints;
	private int currentExperiencePoints;

	// Player damage invincibility
	public float damageInvincibilityLength = 2f;
	private float damageInvincibilityTime;
	private bool invincible;

	// Character controller
	private CharacterController2D controller;

	void Awake() {
		controller = GetComponent<CharacterController2D> ();
		currentHealth = maxHealth;
		actionBar.GetComponent<ActionBar> ().fillActionBar (new Sprite[] {weaponPrefabs[0].GetComponent<Weapon>().icon, weaponPrefabs[1].GetComponent<Weapon>().icon, weaponPrefabs[2].GetComponent<Weapon>().icon} );
		controller.onTriggerEnterEvent += onTriggerEnter;
	}

	void onTriggerEnter( Collider2D coll)
	{
		if (coll.gameObject.name.StartsWith ("blob")) {
			if (!invincible) {
				currentHealth--;
				healthbar.GetComponent<PlayerHealthbar> ().updateHealthbar (currentHealth, maxHealth);
				knockback = new Vector3 (transform.position.x - coll.gameObject.transform.position.x, 0, 0).normalized;
				invincible = true;
			}
		}
	}

	public void gainExperience(int experience) {
		currentExperiencePoints += experience;
		experiencebar.GetComponent<PlayerExperiencebar> ().updateExperiencebar (currentExperiencePoints, maxExperiencePoints);
	}

	void Update() {
		handleWeapons ();
		handleInvincibility ();

		// grab our current velocity to use as a base for all calculations
		Vector3 velocity = controller.velocity;

		if (controller.isGrounded) {
			velocity.y = 0;
		}


		//
		// Handle x velocity
		//

		if (knockback == Vector3.zero) {
			// horizontal input
			if (Input.GetKey (KeyCode.D)) {
				normalizedHorizontalSpeed = 1;
				goRight ();
			} else if (Input.GetKey (KeyCode.A)) {
				normalizedHorizontalSpeed = -1;
				goLeft ();
			} else {
				normalizedHorizontalSpeed = 0;
			}

			// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
			float smoothedMovementFactor = controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
			velocity.x = Mathf.Lerp (velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);
		} else {
			velocity.x = knockback.x * damageKnockback;
			knockbackTime += Time.deltaTime;
			if (knockbackTime >= knockbackDuration) {
				knockback = Vector3.zero;
				knockbackTime = 0;
			}
		}


		//
		// Handle y velocity
		//
		if (controller.isGrounded && Input.GetKeyDown (KeyCode.W)) {
			velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
		}


		//
		// apply gravity
		//
		velocity.y += gravity * Time.deltaTime;


		//
		// Move the player
		//
		controller.move (velocity * Time.deltaTime);
	}

	private void goLeft() {
		if (transform.localScale.x > 0f) {
			goingLeft = true;
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}

	private void goRight() {
		if (transform.localScale.x < 0f) {
			goingLeft = false;
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}

	private void handleWeapons() {
		attackTime -= Time.deltaTime;
		// Check if it's time to end sword slash
		if (attacking) {
			if (attackTime <= 0) {
				attacking = false;
				Destroy (GameObject.Find ("sword(Clone)"));
			} else {
				return;
			}
		}

		int weapon = -1;
		if (Input.GetMouseButtonDown (0)) {
			weapon = 0;
		} else if (Input.GetMouseButtonDown (1)) {
			weapon = 1;
		} else if (Input.GetMouseButtonDown (2)) {
			weapon = 2;
		} 

		if (weapon != -1) {
			Vector3 positionOffset = weaponPrefabs [weapon].GetComponent<Weapon> ().positionOffset;
			positionOffset.x = goingLeft ? positionOffset.x * -1 : positionOffset.x;
			GameObject weaponUsed = (GameObject)Instantiate (weaponPrefabs [weapon], 
		                                      transform.position + positionOffset, 
		                                      Quaternion.identity);
			if (weaponUsed.GetComponent<MeleeWeapon> () != null) {
				weaponUsed.transform.parent = this.transform;
			}
			if (goingLeft) {
				weaponUsed.transform.localScale = new Vector3 (-weaponUsed.transform.localScale.x, weaponUsed.transform.localScale.y, weaponUsed.transform.localScale.z);
			}
			actionBar.GetComponent<ActionBar> ().actionButtons [weapon].GetComponent<ActionButton> ().setCooldown (weaponPrefabs [weapon].GetComponent<Weapon> ().cooldown);
			attacking = true;
			attackTime = weaponUsed.GetComponent<Weapon> ().attackLength;
		}
	}

	private void handleInvincibility() {
		damageInvincibilityTime += Time.deltaTime;
		if (damageInvincibilityTime >= damageInvincibilityLength) {
			invincible = false;
			damageInvincibilityTime = 0;
		}
	}
}
