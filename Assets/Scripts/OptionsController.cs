using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
	public Slider difficultySlider;
	public string startLevelName = "01a_StartMenu";

	private LevelManager levelManager;
	private MusicManager musicManager;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		musicManager = GameObject.FindObjectOfType<MusicManager> ();

		volumeSlider.value = PlayerPrefsManager.GetMasterVolume ();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty ();
		//Debug.Log (musicManager);
	}
	
	// Update is called once per frame
	void Update () {
		if (volumeSlider.value != PlayerPrefsManager.GetMasterVolume()) {
			if (musicManager) {
				musicManager.ChangeVolume (volumeSlider.value);
			} else {Debug.LogError ("Volume setting not apply due to no music manager");}
		}
	}

	public void SaveAndExit(){
		// Volume setting
		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
		// Diffculty setting
		PlayerPrefsManager.SetDifficulty (difficultySlider.value);
		// back to start menu
		levelManager.LoadLevel (startLevelName);
	}

	public void SetDefault(){
		volumeSlider.value = 0.8f;
		difficultySlider.value = 2f;
	}
}
