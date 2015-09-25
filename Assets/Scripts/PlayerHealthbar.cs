using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthbar : MonoBehaviour {

	public Transform greenBar;
	public Text healthText;
	
	public void updateHealthbar(int currentHealth, int maxHealth) {
		greenBar.localScale = new Vector3 (((float)currentHealth)/((float)maxHealth), 1, 1);
		if (greenBar.localScale.x < 0) {
			greenBar.localScale = new Vector3(0, 1, 1);
		}
		healthText.text = currentHealth + " / " + maxHealth;
	}
}
