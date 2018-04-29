using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : MonoBehaviour {

	AttackerSpawner myLaneSpawner;
	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		SetMyLaneSpawner ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		spriteRenderer.enabled = AttackerAhead();

	}

	void SetMyLaneSpawner(){
		AttackerSpawner[] allSpawner = GameObject.FindObjectsOfType<AttackerSpawner> ();
		foreach (AttackerSpawner element in allSpawner) {
			if (element.transform.position.y == transform.position.y) {
				myLaneSpawner = element;
				return;
			}
		}
		Debug.LogWarning (name + ", find No Spawner.");
	}

	bool AttackerAhead(){
		if (myLaneSpawner.transform.childCount <= 0) {return false;}
		foreach (Transform child in myLaneSpawner.transform) {
			if (child.transform.position.x < (transform.position.x + 0.5f)) {return false;}
		} 
		return true;
	}
}
