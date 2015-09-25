using UnityEngine;
using System.Collections;

public class Sword : MeleeWeapon {

	void Start() {
		transform.Rotate (0, 0, 10);
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, -4);
	}
}
