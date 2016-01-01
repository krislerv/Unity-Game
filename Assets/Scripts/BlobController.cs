using UnityEngine;
using System.Collections;

public class BlobController : Enemy {

	public float gravity = -25f;
	public float jumpHeight = 2f;
	public float jumpDelay = 2f;
	private float jumpTime;
	private int knockback;

	private CharacterController2D controller;

	protected override void Start() {
		base.Start ();
		controller = GetComponent<CharacterController2D> ();
		controller.onTriggerEnterEvent += onTriggerEnterEvent;
		jumpTime = jumpDelay;
	}

	void onTriggerEnterEvent(Collider2D coll) {
		if (coll.name != "player") {
			damage (coll.GetComponent<Weapon> ().damage);
			knockback = coll.GetComponent<Weapon> ().knockback;
		}
	}

	void Update() {
		Vector3 velocity = controller.velocity;

		if (controller.isGrounded) {
			velocity.x = 0;
		}

		if (jumpTime <= 0) {
			if (controller.isGrounded) {
				if (GameObject.Find ("player").transform.position.x > transform.position.x) {
					velocity.x = 2f;
					goRight();
				} else {
					velocity.x = -2f;
					goLeft();
				}
				velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
				jumpTime = jumpDelay + jumpTime; // ex. if jumpdelay is 2 and jumpTime is -0.2, the new jumpTime should be 2 - 0.2 = 1.8
			}
		} else {
			jumpTime -= Time.deltaTime;
		}

		if (knockback != 0) {
			velocity.x += GameObject.Find("player").transform.position.x > transform.position.x ? -3f : 3f;
			if (velocity.y > 0) {
				velocity.y = Mathf.Sqrt(knockback * jumpHeight * -gravity);
			} else {
				velocity.y += Mathf.Sqrt(knockback * jumpHeight * -gravity);
			}
			knockback = 0;
		}

		// apply gravity
		velocity.y += gravity * Time.deltaTime;

		controller.move (velocity * Time.deltaTime);
	}

	private void goLeft() {
		if (transform.localScale.x > 0f) {
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}
	
	private void goRight() {
		if (transform.localScale.x < 0f) {
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}
}
