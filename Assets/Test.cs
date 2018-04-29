using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//PlayerPrefsManager.SetMasterVolume (0f);
		//print ("SetMasterVolume" + PlayerPrefsManager.GetMasterVolume ());
		//PlayerPrefsManager.SetMasterVolume (0.3f);
		print ("MasterVolume is " + PlayerPrefsManager.GetMasterVolume());
		//PlayerPrefsManager.SetMasterVolume (1.1f);

		//PlayerPrefsManager.UnlockLevel (2);
		//print ("PlayerPrefsManager.UnlockLevel (2)");
		//print ("Level 1 unlock is " + PlayerPrefsManager.IsLevelUnlocked (1));
		//print ("Level 2 unlock is " + PlayerPrefsManager.IsLevelUnlocked (2));

		//print ("Current difficulty is " + PlayerPrefsManager.GetDifficulty());
		//PlayerPrefsManager.SetDifficulty (0.5f);
		//print ("Current difficulty is " + PlayerPrefsManager.GetDifficulty());
		//PlayerPrefsManager.SetDifficulty (1.1f);
		print ("Current difficulty is " + PlayerPrefsManager.GetDifficulty());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
