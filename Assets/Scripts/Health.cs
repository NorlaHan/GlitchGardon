using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float health = 3f;
	public bool specialMoveOnDeath = false;

	private GameObject healthBar, healthBase;
	private float fullHealth;
	private bool isDefender = false, showHealthBar, barSwitch =true ;
	// Use this for initialization
	void Start () {
		if (GetComponent<Defenders> ()) {
			isDefender = true;
			healthBar = gameObject.transform.Find ("HealthBase/HealthBar").gameObject;
			healthBase = gameObject.transform.Find ("HealthBase").gameObject;
			healthBar.transform.localScale = new Vector3 (1f, 1f, 1f);
			fullHealth = health;
			CheckHealthBar();
		} else {isDefender = false;}
	}
	
	// Update is called once per frame
	void Update () {
		if (isDefender) {
			if (showHealthBar && barSwitch) {
				barSwitch = false;
				Invoke ("HideHealthBar", 2f);
			}

		}
	}

	public void TakeDamage(float damage){
		float remainHealth;
		health -= damage;
		remainHealth = health;
		if (isDefender){
			ShowHealthBar ();
			barSwitch = true;
			healthBar.transform.localScale = new Vector3 (remainHealth / fullHealth, 1f, 1f);
		}
		if (health <= 0) {
			if (specialMoveOnDeath) {return;}
			// Optionally triggers animation Die state.
			DestroyObject();
		}
	}

	public void DestroyObject(){
		Destroy (gameObject);
	}

	void CheckHealthBar(){
		showHealthBar = healthBar.GetComponent<SpriteRenderer>().enabled;
	}

	void ShowHealthBar(){
		healthBar.GetComponent<SpriteRenderer> ().enabled = true;
		healthBase.GetComponent<SpriteRenderer> ().enabled = true;
		CheckHealthBar();
	}

	void HideHealthBar(){
		healthBar.GetComponent<SpriteRenderer> ().enabled = false;
		healthBase.GetComponent<SpriteRenderer> ().enabled = false;
		CheckHealthBar();
	}
}
