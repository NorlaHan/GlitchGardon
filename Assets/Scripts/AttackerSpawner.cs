using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour {

	[Tooltip ("The attacker prefabs which are to spawn")]
	public GameObject[] attackerPrefabArray;

	[Tooltip ("is attacker in the array can spawn?")]
	public bool[] AttackerCanSpawn;

	[Tooltip ("How many attackers a lane can have at once")]
	public int maxAttackerInLane = 5; 

	[Tooltip ("How many Lane is active?")]
	public int numberOfLane = 5;

	[Tooltip ("Larger spawn slower")]
	public float spawnAdjust = 8;

	[Tooltip ("How often (relatively) will attack spawn")]
	public float[] attackerSpawnFactor;

	float total = 0,spawnSpread;
	bool useFactor, canSpawn = true;
	int index = 0 ;

	// Use this for initialization
	void Start () {
		int length = attackerSpawnFactor.Length;
		SetDifficulity ();
		CaculateFactor (length); 
	}
	
	// Update is called once per frame
	void Update () {
		if (!canSpawn) {return;}
		if (!(transform.childCount < maxAttackerInLane)) {return;}
		foreach (GameObject element in attackerPrefabArray) {
			index = System.Array.IndexOf (attackerPrefabArray, element);
			if (AttackerCanSpawn[index]){
				//Caculate the index only when the factor is used.
				//print (element + " index is "  + index);
				if (isTimeToSpawn(element, index)) {
				Spawn (element);
				//Debug.Log ("Spawn" + element.name);
				}
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (transform.position, 0.33f );
	}

	public void SetDifficulity ()
	{
		if (PlayerPrefsManager.GetDifficulty () == 1) {spawnSpread = numberOfLane * spawnAdjust * 1.5f;
		}else if (PlayerPrefsManager.GetDifficulty () == 2) {spawnSpread = numberOfLane * spawnAdjust;
			}else if (PlayerPrefsManager.GetDifficulty () == 3) {spawnSpread = numberOfLane * spawnAdjust * 0.7f;
				}
		Debug.Log ("spawnSpread is " + spawnSpread);
	}

	void CaculateFactor (int length){
		for (int i = 0; i < length; i++) {total += attackerSpawnFactor [i];}
		print ("Total factor is " + total);
		useFactor = total != 0;
	}

	void Spawn (GameObject obj){
		GameObject newAttacker;
		newAttacker = Instantiate (obj);
		newAttacker.transform.parent = transform;
		newAttacker.transform.position = transform.position;
	}
		

	public void StopSpawn(){
		canSpawn = false;
	}

	bool isTimeToSpawn(GameObject obj ,int index){
		float factor, meanSpawnDelay, spawnPerSecond;
		meanSpawnDelay =obj.GetComponent<Attacker> ().spawnFrequency;
		if (useFactor) {
			factor = attackerSpawnFactor [index] / total;
			print (obj.name + " factor / total is " + factor);
			spawnPerSecond = 1 / meanSpawnDelay * factor;
		} else {
			spawnPerSecond = 1 / meanSpawnDelay;
		}
		if (Time.deltaTime > meanSpawnDelay) {
			Debug.LogWarning ("Spawn rate capped by frame rate");
		}
		float threshold = spawnPerSecond * Time.deltaTime / spawnSpread;
		return (threshold > Random.value);
	}


}
