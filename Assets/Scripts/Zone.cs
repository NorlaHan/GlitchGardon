using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

	public enum functionType { projectile, all, lose}
	public functionType function;
	LevelManager levelManager;


	// This is a multi-functions Zone script.
	void Start(){
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
	}

	void OnTriggerEnter2D (Collider2D collider){
		GameObject obj = collider.gameObject;
		if (function == functionType.projectile) {ShredProjectile (obj);}
		if (function == functionType.all) {Destroy (obj.gameObject);}
		if (function == functionType.lose) {Lose (obj);}
		}
		
	void ShredProjectile(GameObject obj){
		if (obj.GetComponent<Projectile> ()) {
			Destroy (obj.gameObject);
			Debug.Log ("Projectile shreded");
		}
	}

	void Lose(GameObject obj){
		if (obj.GetComponent<Attacker> ()) {
			levelManager.LoadLevel ("03b_Lose");
		}
	}
}
