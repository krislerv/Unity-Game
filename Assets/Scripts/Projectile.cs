using UnityEngine;
using System.Collections;

public class Projectile : Weapon {
	
	public float movementSpeed;
	
	void Start () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Converting to Vector2 to stop z-direction from messing up the normalization
		GetComponent<Rigidbody2D> ().velocity = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized * movementSpeed;
	}

	void Update() {
		Vector2 dir = GetComponent<Rigidbody2D> ().velocity;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		Destroy (gameObject);
	}
}
