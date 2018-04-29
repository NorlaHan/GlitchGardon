using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefault : MonoBehaviour {
	void Awake () {
		PlayerPrefsManager.SetMasterVolume(0.8f);
		PlayerPrefsManager.SetDifficulty(2);
	}

}
