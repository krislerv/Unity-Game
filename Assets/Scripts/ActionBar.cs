using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionBar : MonoBehaviour {

	public GameObject[] actionButtons;
	

	public void fillActionBar(Sprite[] weapons) {
		int i = 0;
		foreach (GameObject button in actionButtons) {
			button.GetComponent<Image>().sprite = weapons[i];
			i++;
		}
	}

	public void setCooldown(int ability, float time) {
		actionButtons [ability].GetComponent<ActionButton> ().setCooldown (time);
	}
}
