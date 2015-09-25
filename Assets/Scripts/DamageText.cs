using UnityEngine;
using System.Collections;
using System;

public class DamageText : MonoBehaviour {

	float timeToLive = 0.5f;

	// Use this for initialization
	void Start() {
		Invoke ("remove", timeToLive);
	}

	private void remove() {
		Destroy(gameObject);
	}

	public void launch (int damage) {
		GetComponent<TextMesh> ().text = damage.ToString();
		System.Random r = new System.Random ();
		float x = (float)(r.NextDouble() - 0.5);
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (x, 1) * 250);
	}
}
