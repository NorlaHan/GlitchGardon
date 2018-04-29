using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlock_";

	public static void SetMasterVolume (float volume){
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Master volume out of range");
		}
	}

	public static float GetMasterVolume () {
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

	public static void UnlockLevel(int level){
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString(), 1); // int 1 stands for true.
			Debug.Log ("Unlock level "+ level +" current level count is "+ SceneManager.sceneCountInBuildSettings);
		} else {
			Debug.LogError ("Trying to unlock level not in build order, current level count is "+ SceneManager.sceneCountInBuildSettings);
		}
	}

	public static bool IsLevelUnlocked(int level){
		int levelValue = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString ());
		bool isLevelUnlocked = (levelValue == 1);

		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			return isLevelUnlocked;
			//Debug.Log ("isLevelUnlocked, level "+ level + isLevelUnlocked);    //will not execute?
		} else {
			Debug.LogError ("Level "+ level +", is not in scene build order");
			return false;
		}
	}

	public static void SetDifficulty(float difficulty){
		if (1f <= difficulty && difficulty <= 3f) {
			PlayerPrefs.SetFloat (DIFFICULTY_KEY, difficulty);
		} else {
			Debug.LogError ("Set difficulity "+ difficulty + " out of range");
		}
	}

	public static float GetDifficulty(){
		return PlayerPrefs.GetFloat (DIFFICULTY_KEY);
	}
}
