using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	//Public variables
	public float gameTime = 90f ;

	[Tooltip ("Which % of total level time should the wave start? (0 = Wave do not initiate)")]
	[Range(0f,1f)] public float wave1 = 0.3f , wave2 = 0.6f ,wave3 = 0, stopSpawn = 0.97f;

	[Tooltip ("How much shorten between spawn in percentage (smaller is faster).")]
	[Range(0.0f,1f)] public float wave1Factor = 0.5f, wave2Factor = 0.5f,wave3Factor = 0.5f ;

	[Tooltip ("Whether the attack in the array can spawn of not. (Size 0 = do not change)")]
	public bool[] wave1CanSpawn, wave2CanSpawn, wave3CanSpawn;

	[Tooltip ("The Maximun attackers a lane can spawn at once. Size should equal to the lane. (Size 0 = do not change) ")]
	public int[] wave1MaxSpawn, wave2MaxSpawn, wave3MaxSpawn;

	[Tooltip ("The BGM to change when the wave start. (0 = do not change BGM).")]
	public int[] waveBGM;

	//Private component
	private Slider slider;
	private LevelManager levelManager;
	private AudioSource music;
	private Text completeMessage;
	private AttackerSpawner[] spawnerArray;
	private MusicManager musicmanager;
	private GameObject loseZone;
	private FadeScreen fadeScreen;
	bool haveCompleteMessage;

	//Private variables
	private bool isEndOfLevel = false, isWave1OfLevel = false, isWave2OfLevel = false, isWave3OfLevel = false, isStopSpawn= false, gameStart = false ;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		music = GetComponent<AudioSource> ();
		if (GameObject.FindObjectOfType<FadeScreen>()) {
			fadeScreen = GameObject.FindObjectOfType<FadeScreen>();
			Invoke ("GameStart", (fadeScreen.fadeInTime + fadeScreen.textDelay));
		} else {Debug.LogWarning ("FadeScreen missing, start now");GameStart ();} // Straight to GameStart if no fade screen was found.
		if (GameObject.Find ("CompleteMessage")) {
			completeMessage = GameObject.Find ("CompleteMessage").GetComponent<Text>();
		} else {Debug.LogWarning ("CompleteMessage missing");
			haveCompleteMessage = false;}
		if (GameObject.FindObjectOfType<MusicManager> ()){
			musicmanager = GameObject.FindObjectOfType<MusicManager> ();
		}else {Debug.LogWarning ("TimeSlider failed to find MusiManager");}
		if (GameObject.Find ("LoseZone")) {
			loseZone = GameObject.Find ("LoseZone");
		}else {Debug.LogWarning ("LoseZone missing");}
		spawnerArray = GameObject.FindObjectsOfType<AttackerSpawner> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStart) {return;}
		if (slider.value < 1) {
			slider.value += Time.deltaTime / gameTime;
			if (slider.value >= wave1 && !isWave1OfLevel && wave1 !=0f) {
				isWave1OfLevel = true;
				if (wave1CanSpawn.Length != 0) {ChangeSpawn (wave1CanSpawn);}
				if (wave1MaxSpawn.Length != 0) {ChangeMaxInLane (wave1MaxSpawn);}
				if (waveBGM [0] != 0) {ChangeBGM (waveBGM [0]);}
				ChangeDifficulty (wave1Factor);
			}
			if (slider.value >= wave2 && !isWave2OfLevel && wave2 !=0f){
				isWave2OfLevel = true;
				if (wave2CanSpawn.Length != 0) {ChangeSpawn (wave2CanSpawn);}
				if (wave2MaxSpawn.Length != 0) {ChangeMaxInLane (wave2MaxSpawn);}
				if (waveBGM [1] != 0) {ChangeBGM (waveBGM [1]);}
				ChangeDifficulty (wave2Factor);
			}
			if (slider.value >= wave3 && !isWave3OfLevel && wave3 != 0f) {
				isWave3OfLevel = true;
				if (wave3CanSpawn.Length != 0) {ChangeSpawn (wave3CanSpawn);}
				if (wave3MaxSpawn.Length != 0) {ChangeMaxInLane (wave3MaxSpawn);}
				if (waveBGM [2] != 0) {ChangeBGM (waveBGM [2]);}
				ChangeDifficulty (wave3Factor);
			}
			if (slider.value >= stopSpawn && !isStopSpawn) {
				isStopSpawn = true;
				StopSpawn ();
			}
		} else {
			if (!isEndOfLevel) {
				isEndOfLevel = true;
				loseZone.SetActive(false);
				DestroyOnWin ();
				LevelComplete ();
			}
		}
	}

	void GameStart(){
		gameStart = true;
	}
		
	void ChangeDifficulty(float factor){
		foreach (AttackerSpawner spawner in spawnerArray) {
			spawner.spawnAdjust = spawner.spawnAdjust* factor;
			spawner.SetDifficulity ();
		}
	}

	void ChangeSpawn(bool[] waveCanSpawn){
		foreach (AttackerSpawner spawner in spawnerArray) {
			int length = spawner.AttackerCanSpawn.Length ;
			for (int i = 0; i < length; i++) {
				spawner.AttackerCanSpawn [i] = waveCanSpawn [i];
			}
		}
	}

	void ChangeMaxInLane(int[] waveMaxSpawn){
		foreach (AttackerSpawner spawner in spawnerArray) {
			int length = spawnerArray.Length;
			for (int i = 0; i < length; i++) {
				spawner.maxAttackerInLane = waveMaxSpawn [i];
			}
		}
	}

	void ChangeBGM(int indexOfBGM){
		musicmanager.OnLevelLoadMusic (indexOfBGM);
	}

	void StopSpawn(){
		foreach (AttackerSpawner spawner in spawnerArray) {
			spawner.StopSpawn();
		}
	}

	void LevelComplete(){
		music.volume = music.volume*PlayerPrefsManager.GetMasterVolume();
		music.Play();
		Invoke ("NextLevel", music.clip.length);
		if (fadeScreen) {
			fadeScreen.ManualFadrOut ();
			if (haveCompleteMessage) {Invoke ("ShowCompleteMessage", fadeScreen.fadeOutTime);}
		}
		//levelManager.LoadLevel ("03a_Win");
	}

	void ShowCompleteMessage (){
		completeMessage.enabled = true;
	}

	void NextLevel(){
		levelManager.NextLevel ();
	}

	void DestroyOnWin(){
		GameObject[] taggedObjArray = GameObject.FindGameObjectsWithTag ("destroyOnWin");
		foreach (GameObject taggedObj in taggedObjArray) {
			Destroy (taggedObj);
		}
	}
}
