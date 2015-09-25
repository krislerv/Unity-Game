using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerExperiencebar : MonoBehaviour {

	public Transform purpleBar;
	public Text experienceText;

	void Start() {
		purpleBar.localScale = new Vector3 (0, 1, 1);
	}
	
	public void updateExperiencebar(int currentExperiencePoints, int maxExperiencePoints) {
		purpleBar.localScale = new Vector3 (((float)currentExperiencePoints)/((float)maxExperiencePoints), 1, 1);
		experienceText.text = currentExperiencePoints + " / " + maxExperiencePoints;
	}
}
