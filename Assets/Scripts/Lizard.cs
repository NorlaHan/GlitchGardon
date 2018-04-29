using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour {

	private Attacker attacker;
	private Animator animator;

	// Use this for initialization
	void Start () {
		attacker = gameObject.GetComponent<Attacker> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		GameObject obj = collider.gameObject;
		// Leave the method if not colliding with defender
		if (!obj.GetComponent<Defenders> ()) {return;}
		//Debug.Log (name + " trigger enter");
		animator.SetBool ("isAttacking", true);
		attacker.Attack (obj);
	}

	//void OnTriggerExit2D(Collider2D collider){
	//	GameObject obj = collider.gameObject;
	//	if (!obj.GetComponent<Defenders> ()) {return;}
	//	Debug.Log (name + " trigger exit");
	//	animator.SetBool ("isAttacking", false);
	//	attacker.Attack (null);
	//}

}
