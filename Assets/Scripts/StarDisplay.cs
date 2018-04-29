using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StarDisplay : MonoBehaviour {

	public enum Status {SUCCESS, FAILURE }
	public int startStarAmount = 100;

	private int starAmount;
	private Text displayText;

	//Button[] allButton;

	// Use this for initialization
	void Start () {
		displayText = GetComponent<Text>();
		starAmount = startStarAmount;
		//allButton = GameObject.FindObjectsOfType<Button> ();
		UpdateDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddStars(int amount){
		starAmount += amount;
		UpdateDisplay ();
	}

	public Status UseStars(int amount){
		if (starAmount >= amount) {
			starAmount -= amount;
			UpdateDisplay ();
			return Status.SUCCESS;
		}return Status.FAILURE;
	}

	public void ResetStar (){
		starAmount = 0;
		UpdateDisplay ();
	}

	void UpdateDisplay (){
		displayText.text = ("Stars " + starAmount.ToString());
	}
}
