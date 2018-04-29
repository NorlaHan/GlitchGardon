using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed, damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider){
		GameObject obj = collider.gameObject;
		// Leave the method if not colliding with attacker.
		if (!obj.GetComponent<Attacker> ()) {return;}
		// Roll Fox dodge
		if (obj.GetComponent<Fox> ()) {if (obj.GetComponent<Fox> ().FoxDodge ()) {return;}}
		if (obj.GetComponent<Ryu> ()) {if (obj.GetComponent<Ryu> ().RyuDodge ()) {return;}}
		Health health = obj.GetComponent<Health> ();
		health.TakeDamage (damage);
		//Debug.Log (obj.name + " damaged by " + name);
		Destroy (gameObject);
	}
}
