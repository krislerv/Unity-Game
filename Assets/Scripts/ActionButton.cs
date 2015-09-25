using UnityEngine;
using System.Collections;

public class ActionButton : MonoBehaviour {

	float totalTime;
	float time;
	
	void Update() {
		if (time > 0) {
			time -= Time.deltaTime;
			foreach(Transform child in transform) {
				child.GetComponent<RectTransform> ().localScale = new Vector3 (1, time / totalTime, 1);
			}
		}
	}

	public void setCooldown(float time) {
		totalTime = time;
		this.time = time;
	}
}
