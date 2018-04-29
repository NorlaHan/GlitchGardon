using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrophy : MonoBehaviour {

	StarDisplay starDisplay;

	// Use this for initialization
	void Start () {
		if (GameObject.FindObjectOfType<StarDisplay> ()) {
			starDisplay = GameObject.FindObjectOfType<StarDisplay> ();
		} else {
			Debug.LogWarning ("StarDisplay missing!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AddStars (int amount){
		//print ("Add " + amount + " stars");
		if (starDisplay) {
			starDisplay.AddStars (amount);
		} else {
			Debug.LogWarning ("StarDisplay missing!");
		}
	}
}
