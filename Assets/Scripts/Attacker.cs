using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

	[Tooltip ("How many seconds between each spawn.")]
	public float spawnFrequency;

	private float currentSpeed;
	private GameObject currentTarget;
	//private Health health;
	private Animator animator;


	// Use this for initialization
	void Start () {
		
		if (gameObject.GetComponent<Rigidbody2D> () == null) {
			Rigidbody2D rigidbody2D = gameObject.AddComponent<Rigidbody2D> ();
			rigidbody2D.isKinematic = true;
		}
		//health = gameObject.GetComponent<Health> ();
		animator = gameObject.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		// Attacker moving
		transform.Translate (Vector3.left * currentSpeed * Time.deltaTime);

		// Deactivate isAttacking when target is gone.
		if (animator.GetBool ("isAttacking") == true) { 
			if (currentTarget == null) {animator.SetBool ("isAttacking", false);}
		}
	}

	public void SetSpeed (float speed){
		currentSpeed = speed;
	}

	public void StrikeCurrentTarget(float damage){
		//Debug.Log (gameObject.name +  " deals "+ damage + " damage." );
		if (currentTarget) {
			if (currentTarget.GetComponent<Health> ()){
				currentTarget.GetComponent<Health> ().TakeDamage (damage);
			}
		}
	}

	public void Attack (GameObject obj){
		currentTarget = obj;
	}
}
