using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	public GameObject projectile, projectileParent,gun ;

	private Animator animator;
	private AttackerSpawner myLaneSpawner;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("projectilesParent")) {
			projectileParent = GameObject.Find ("projectilesParent");
		} else {
			projectileParent = new GameObject ("projectilesParent");
		}
		if (gameObject.GetComponent<Animator> ()) {
			animator = gameObject.GetComponent<Animator> ();
		}
		SetMyLaneSpawner ();
	}

	void Update(){
		if (IsAttackerAheadInLane()) {
			animator.SetBool ("isAttacking", true);
		} else {
			animator.SetBool ("isAttacking", false);
		}
	}

	void FireGun(){
		GameObject newProjectile = Instantiate (projectile);
		newProjectile.transform.parent = projectileParent.transform;
		newProjectile.transform.position = gun.transform.position;
	}

	void SetMyLaneSpawner(){
		AttackerSpawner[] allSpawner =  GameObject.FindObjectsOfType<AttackerSpawner> ();
		foreach (AttackerSpawner element in allSpawner) {
			if (element.transform.position.y == transform.position.y) {
				myLaneSpawner = element;
				//Debug.Log (name + "'s lane is set to "+ myLaneSpawner.name);
				return;
			}
		}
		Debug.LogWarning ("Can't find AttackerSpawner for " + name);
	}

	bool IsAttackerAheadInLane(){
		if (myLaneSpawner.transform.childCount <= 0) {
			return false; // No attackers in lane.
		}
		foreach (Transform attacker in myLaneSpawner.transform) {
			if (attacker.transform.position.x >= transform.position.x) {
				return true; // True if enemy is ahead.
			}
		}return false; // False when there are enemies but not ahead.

	}
}
