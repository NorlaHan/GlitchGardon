using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButton : MonoBehaviour {

	static public GameObject selectedDefender;

	public enum defenderType { cactus, gnome, graveStone, starTrophy}

	[Tooltip ("Select the defender which this button represents.")]
	public defenderType defenderToSpawn;
	[Tooltip ("Please drag in the prefabs as the list above")]
	public GameObject[] defenderList; 

	public bool isClicked = false, isClickable = false;


	private MyButton[] buttonArray;
	//StarDisplay starDisplay;
	//DefenderSpawner defenderSpawner;

	private int defenderIndex;
	Text costText;

	//static Button currentClicked = null;

	void Start(){
		buttonArray =  FindObjectsOfType<MyButton> ();
		//starDisplay = GameObject.FindObjectOfType<StarDisplay> ();
		//defenderSpawner = GameObject.FindObjectOfType<DefenderSpawner> ();
		defenderIndex = defenderToSpawn.GetHashCode ();
		if (GetComponentInChildren<Text> ()) {
			costText = GetComponentInChildren<Text> ();
			costText.text = defenderList [defenderIndex].GetComponent<Defenders> ().starCost.ToString();
		} else {
			Debug.LogWarning (name + "Missing Text component in children.");
			costText.text = ("--");
		}
	}


	void OnMouseDown(){
		// Dim the button and clear the selectedDefender if the button is already been selected.
		if (isClicked) {
			GetComponent<SpriteRenderer> ().color = Color.gray;
			selectedDefender = null;
			isClicked = false;
		} else {
			// If the button has not been selected, deselected all button first.
			foreach (MyButton thisButton in buttonArray) {
				thisButton.GetComponent<SpriteRenderer> ().color = Color.gray;
				thisButton.isClicked = false;

				GetComponent<SpriteRenderer> ().color = Color.white;
				//Set selectedDefender according to the enum.
				selectedDefender = defenderList [defenderIndex];
				isClicked = true;
			}
			//Debug.Log (name + " pressed, defender to build is " + selectedDefender.name);
		}
	}

	public void DefenderAvaliable(){
		// Button is black when the starAmount is lesser than the cost.

		//if (!isClickable) {selectedDefender = null;}
			// Not enough stars, turn black. unbuiltable ,unclickable.

	
	}	

	//static void SetColorAll(){
		//if (isClicked == false) {
		//	Button.		GetComponent<SpriteRenderer> ().color = new Vector4 (0.5f, 0.5f, 0.5f, 1f);
		//}
	//}
}
