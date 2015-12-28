using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon {

	public int swingSpeed;

	void Start() {
		transform.Rotate (0, 0, 10);
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, -swingSpeed * Time.deltaTime);
	}

}
