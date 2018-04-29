using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver : MonoBehaviour {

	LevelManager levelManager;

	public static int lastPlayedLevel;

	// Use this for initialization
	void Start () {

		if (GameObject.FindObjectOfType<LevelManager> ()) {
			levelManager = GameObject.FindObjectOfType<LevelManager> ();
		} else {Debug.LogWarning (name + " can't find LevelManager ");}
		lastPlayedLevel = LevelManager.nowPlayingLevel;
	}
}
