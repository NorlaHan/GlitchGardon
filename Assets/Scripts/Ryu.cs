using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryu : MonoBehaviour {

	[Tooltip ("How often the Ryu can dodge.")]
	[Range (0,1)] 
	public float dodgeRate = 0.4f;

	private Attacker attacker;
	private Animator animator;
	GraveStone graveStone;
	// Use this for initialization
	void Start () {
		attacker = gameObject.GetComponent<Attacker> ();
		animator = gameObject.GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {
		if (animator.GetBool ("isCruching")) {
			if (graveStone == null) {
				animator.SetBool ("isCruching", false);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		GameObject obj = collider.gameObject;
		// Leave the method if not colliding with defender
		if (!obj.GetComponent<Defenders> ()) {return;}
		if (obj.GetComponent<GraveStone> ()) {
			graveStone = obj.GetComponent<GraveStone> ();
			RyuCruch ();
			RyuAttack();
			attacker.Attack (obj);
		} else {
			RyuAttack();
			attacker.Attack (obj);
		}
	}

	//void OnTriggerExit2D (Collider2D collider){
	//	GameObject obj = collider.gameObject;
	//	if (!obj.GetComponent<Defenders> ()) {return;}
	//	Debug.Log (name + " trigger exit");
	//	animator.SetBool ("isAttacking",false);
	//	FoxNotAttack();
	//	attacker.Attack (null);
	//}

	void RyuCruch(){
		animator.SetBool ("isCruching",true);
	}

	public bool RyuDodge(){
		if (Random.value < dodgeRate) {
			animator.SetTrigger ("DodgeTrigger");
			return true;
			//Debug.LogWarning ("Fox dodge success!");
		}return false;
	}

	void RyuAttack(){
		animator.SetBool ("isAttacking", true);
	}

	void RyuNotAttack(){
		animator.SetBool ("isAttacking",false);
	}
}