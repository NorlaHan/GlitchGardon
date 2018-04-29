using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Attacker))]
public class Fox : MonoBehaviour {

	[Tooltip ("How often the fox can dodge.")]
	[Range (0,1)] 
	public float dodgeRate = 0.2f;

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

	void OnTriggerEnter2D (Collider2D collider) {
		GameObject obj = collider.gameObject;
		// Leave the method if not colliding with defender
		if (!obj.GetComponent<Defenders> ()) {return;}
		if (obj.GetComponent<GraveStone> ()) {
			FoxJumpOver ();
		} else 
			//if (obj.GetComponent<DefenderProjectile> ()) {
			//Debug.LogWarning ("Fox dodge roll");
			//FoxDodge ();
			//}else
			{
			FoxAttack();
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

	void FoxJumpOver(){
		animator.SetTrigger ("JumpOverTrigger");
	}

	public bool FoxDodge(){
		if (Random.value < dodgeRate) {
			animator.SetTrigger ("DodgeTrigger");
			return true;
			//Debug.LogWarning ("Fox dodge success!");
		}return false;
	}

	void FoxAttack(){
		animator.SetBool ("isAttacking", true);
	}

	void FoxNotAttack(){
		animator.SetBool ("isAttacking",false);
	}
}
