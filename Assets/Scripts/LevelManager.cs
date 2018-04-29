using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


	public static int nowPlayingLevel, totalSceneCount;
	[Tooltip ("Drag MusicManager in here in case doessn't have one.")]
	public GameObject musicManagerPrefab;
	[Tooltip ("How many seconds before changing to next level. 0 means never")]
	public float autoLoadNextLevel = 0f;
	[Tooltip("Confirm panel prefab")]
	public GameObject confirmPanelCanvas, confirmPanel;
	[Tooltip("The Start Level name when next level is call at the last level")]
	public string startLevelName = "01a_StartMenu";

	private GameObject calledConfirmPanel;
	//public GameObject frontUICanvas;

	void Awake(){
		GetLevel ();
		GetMusic ();
		// set the diff
		if (PlayerPrefsManager.GetDifficulty() == 0) {PlayerPrefsManager.SetDifficulty (2f);}
	}

	void Start(){
		totalSceneCount = SceneManager.sceneCountInBuildSettings;
		//Debug.Log ("totalSceneCount " + totalSceneCount);
		if (autoLoadNextLevel == 0) {
			Debug.Log ("Auto load next level disable");
		}else{
			Debug.Log ("Load next level in " + autoLoadNextLevel + " seconds");
			Invoke ("NextLevel", autoLoadNextLevel);
		}
		// Paused game will always unpause at new level.
		if (Time.timeScale == 0f) {Time.timeScale = 1f;}
		//if (GameObject.Find("FrontUICanvas")){
		//	frontUICanvas = GameObject.Find("FrontUICanvas");
		//	Debug.Log ("FrontUICanvas found");
		//}
	}
		
	void GetLevel(){
		nowPlayingLevel = SceneManager.GetActiveScene().buildIndex;
	}

	void GetMusic(){
		MusicManager musicManager;
		// If the musicManager alreadi exist. grab the instance.
		if (GameObject.FindObjectOfType<MusicManager> ()) {
			musicManager = GameObject.FindObjectOfType<MusicManager> ();
		} else {	//If it doesn't exist. Make a new one.
			Debug.LogWarning ("No MusicManager was found, create from Prefab");
			musicManager = Instantiate (musicManagerPrefab).GetComponent<MusicManager>();
		}
		musicManager.OnLevelLoadMusic (nowPlayingLevel);
	}

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		SceneManager.LoadScene (name, LoadSceneMode.Single);
	}

	public void BackLevel(){
		int backLevel = nowPlayingLevel - 1;
		Debug.Log ("Load Back Level : " + backLevel);
		SceneManager.LoadScene (backLevel);
	}

	public void NextLevel(){
		int nextLevel = nowPlayingLevel + 1;
		if (nextLevel <= totalSceneCount - 1) {
			Debug.Log ("Load next Level : " + nextLevel);
			SceneManager.LoadScene (nextLevel);
		} else {Debug.LogWarning ("Last level reach, back to start menu.");
			LoadLevel (startLevelName);
		}
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		string activeScene = SceneManager.GetActiveScene().name ;
		Debug.Log ("Try to unload : "+activeScene);
		SceneManager.UnloadSceneAsync (activeScene);
	}

	public void Continue(){
		SceneManager.LoadScene(LevelSaver.lastPlayedLevel);
	}

	public void SurrenderCall(){
		if (!calledConfirmPanel) {
			calledConfirmPanel = Instantiate (confirmPanel);
			Time.timeScale = 0f;
		} else {Debug.LogWarning ("Confirm panel already called, action denial.");}
		//calledConfirmPanel.transform.parent = confirmPanelCanvas.transform;

	}

	public void SurrenderCancel(){
		Time.timeScale = 1f;
		Destroy (calledConfirmPanel);
	}

	public void SurrenderConfirm(){
		Continue ();
	}

}
