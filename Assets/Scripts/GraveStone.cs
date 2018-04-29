using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveStone : MonoBehaviour {

	public float dieDamage = 10f;

	Animator animator;
	Health health;
	//Defenders defender;
	GameObject attacker;
	//GameObject[] attackerArray = new GameObject[100];

	//bool dieAttack = false;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		health = GetComponent<Health> ();
		//defender = GetComponent<Defenders> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Return from damaged state
		//if (animator.GetBool ("isAttacked") == true) {
		//	if (!attacker || attacker.transform.position.x < transform.position.x) {StopDamaging ();}
		//}
		// Special die attack
		if (health.health <= 0) {SpecialDieAttack ();}
	}


	void OnTriggerStay2D(Collider2D collider){
		if (!collider.GetComponent<Attacker> ()) {
			return;
		} else {
			//for (int i=0; attacker != obj.gameObject ; i++){
			//	attackerArray[i] = obj.gameObject;
			//	Debug.Log ("Add "+obj.gameObject.name + " to array");
			//}
			StartDamaging();
			if (attacker != collider.gameObject) {
				attacker = collider.gameObject;
			}
		}
	}

	void StartDamaging(){
		animator.SetTrigger("underAttackTrigger");
	}

	//void StopDamaging(){
	//	animator.SetBool ("isAttacked", false);
	//}

	void SpecialDieAttack (){
		if (attacker) {
			animator.SetTrigger ("dieTrigger");
			Invoke ("DieAttack", 2f);
		}
		else {
			Debug.LogWarning ("Missing target, self-destructed");
			health.DestroyObject ();
		}
	}

	void DieAttack(){
		//TODO DieAttack damage all attackers that were attacking it. 
		//dieAttack = true;
		//foreach (GameObject attackerInArray in attackerArray) {
		//	attackerInArray.GetComponent<Health> ().TakeDamage (dieDamage);
		//	Debug.Log ("Kamakaze attack " + attackerInArray.name);
		//}
		attacker.GetComponent<Health> ().TakeDamage (dieDamage);
		health.DestroyObject ();
	}

}
