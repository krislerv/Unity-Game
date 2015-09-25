using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int maxHealth;
	int health;

	public int experienceReward;
	
	public GameObject healthbarPrefab;
	public GameObject damageTextPrefab;

	Healthbar healthbar;

	protected virtual void Start () {
		healthbar = ((GameObject)Instantiate (healthbarPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity)).GetComponent<Healthbar>();
		healthbar.transform.parent = transform;
		health = maxHealth;
	}

	/*
	void OnTriggerEnter2D(Collider2D coll) {
		damage (coll.GetComponent<Weapon> ().damage);
		knockback (coll.GetComponent<Weapon>().knockback);
	}
	*/

	protected void damage(int amount) {
		health -= amount;
		healthbar.setScale(((float)health)/((float)maxHealth));
		GameObject damageText = (GameObject)Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
		damageText.GetComponent<DamageText>().launch(amount);
		if (health <= 0) {
			GameObject.Find("player").GetComponent<PlayerController>().gainExperience(experienceReward);
			Destroy(gameObject);
		}
	}

	/*
	protected void knockback(int force) {
		Vector2 dir = GameObject.Find ("player").GetComponent<Transform> ().rotation * Vector2.up;
		GetComponent<Rigidbody2D>().AddForce(dir.normalized * force);
	}
	*/
}
