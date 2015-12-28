using UnityEngine;
using System.Collections;

public class Projectile : Weapon {
	
	public float movementSpeed;
	public float gravity;

	// Character controller
	private CharacterController2D controller;

	void Awake() {
		controller = GetComponent<CharacterController2D> ();
		controller.onTriggerEnterEvent += onTriggerEnter;
	}

	void Start () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Converting to Vector2 to stop z-direction from messing up the normalization
		Vector3 velocity = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized * movementSpeed;
		controller.velocity = velocity;
	}

	void Update() {
		Vector3 velocity = controller.velocity;
		velocity.y += gravity;
		float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
		controller.move (velocity * Time.deltaTime);
	}

	void onTriggerEnter( Collider2D coll) {
		Destroy (gameObject);
	}
}
