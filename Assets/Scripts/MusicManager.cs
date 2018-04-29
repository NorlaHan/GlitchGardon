using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
	public bool[] levelMusicLoopArray;
	public float[] levelMusicVolumeArray;

	private float masterVolume;

	AudioSource music;

	void Awake(){
		GameObject.DontDestroyOnLoad (gameObject);
		music = GetComponent<AudioSource> ();
		// Adjust master volume only when the first time which is set to 0.
		if (PlayerPrefsManager.GetMasterVolume () == 0) {PlayerPrefsManager.SetMasterVolume (0.8f);}
		Debug.Log ("Don't destroy on load : " + name);
	}

	
	public void OnLevelLoadMusic (int levelIndex){
		Debug.Log("Playing Clip : " + levelMusicChangeArray[levelIndex].name);
		masterVolume = PlayerPrefsManager.GetMasterVolume ();
		if (levelMusicChangeArray [levelIndex]) {
			music.clip = levelMusicChangeArray [levelIndex];
			music.loop = levelMusicLoopArray[levelIndex];
			music.volume = levelMusicVolumeArray[levelIndex]*masterVolume ;
			music.Play();
		}
	}

	public void ChangeVolume(float volume){
		int levelIndex = LevelManager.nowPlayingLevel; //SceneManager.GetActiveScene ().buildIndex ;
		music.volume = levelMusicVolumeArray[levelIndex]*volume;
	}
}
