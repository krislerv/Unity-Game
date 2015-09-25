using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour {

	public Transform greenBar;

	public void setScale(float scale) {
		greenBar.localScale = new Vector3 (scale, 1, 1);
		if (greenBar.localScale.x <= 0) {
			Destroy(gameObject);
		}
	}
}
