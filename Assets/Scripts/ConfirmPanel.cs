using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPanel : MonoBehaviour {


	LevelManager levelManager;

	// Use this for initialization
	void Start () {

		if (GameObject.FindObjectOfType<LevelManager> ()) {
			levelManager = GameObject.FindObjectOfType<LevelManager> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
