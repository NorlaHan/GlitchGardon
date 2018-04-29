using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour {

	//Vector3 leftDown, rightTop, screenGridSize;
	float cameraH, cameraW, clickedX, clickedY, zFromCamera;
	GameObject defendersParent;
	//Button[] allButton;
	StarDisplay starDisplay;

	// Use this for initialization
	void Start () {
		zFromCamera = transform.position.z - Camera.main.transform.position.z;
		if (GameObject.Find ("DefendersParent")) {
			defendersParent = GameObject.Find ("DefendersParent");
		} else {
			defendersParent = new GameObject ("DefendersParent");
		}

		//allButton = GameObject.FindObjectsOfType<Button> ();
		if (GameObject.FindObjectOfType<StarDisplay> ()) {
			starDisplay = GameObject.FindObjectOfType<StarDisplay> ();
		} else {
			Debug.LogWarning ("StarDisplay missing");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// refresh the screen and camera parameter
	//	if (cameraH != Camera.main.pixelHeight || cameraW != Camera.main.pixelWidth) {
			// Require delay for "ViewportToWorldPoint" method to pick up the new camera.
			// No delay will always picking up the old camera.
	//		Invoke ("GridSetting", 0.1f);
	//	}
	}

	//void GridSetting(){
	//	float zFromCamera = transform.position.z - Camera.main.transform.position.z;
	//	cameraW = Camera.main.pixelWidth;
	//	cameraH = Camera.main.pixelHeight;
	//	leftDown = Camera.main.ViewportToWorldPoint (new Vector3(0,0,zFromCamera));
	//	rightTop = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, zFromCamera));
	//	screenGridSize = rightTop - leftDown;
	//	Debug.LogWarning ("leftDown : " + leftDown +",rightTop : " + rightTop + ",screenGridSize : " + screenGridSize + ",camera : " + cameraW+ " x " + cameraH);
	//}

	void OnMouseDown(){
		if (MyButton.selectedDefender) {
			int cost = MyButton.selectedDefender.GetComponent<Defenders> ().starCost;
			if (starDisplay.UseStars (cost) == StarDisplay.Status.SUCCESS) {
				Vector2 rawPos = CaculateWorldPositionOfMouseClick ();
				Vector2 gridPos = SnapToGrid (rawPos);
				GameObject newDefender = Instantiate (MyButton.selectedDefender, gridPos, Quaternion.identity);
				newDefender.transform.parent = defendersParent.transform;
			} else {
				Debug.Log ("Insufficient stars");
			}

		}
	}

	Vector2 CaculateWorldPositionOfMouseClick(){
		//clickedX = leftDown.x + (Input.mousePosition.x / cameraW * screenGridSize.x) ;
		//clickedY = leftDown.y + (Input.mousePosition.y / cameraH * screenGridSize.y) ;
		//return new Vector2 (clickedX, clickedY);
		Vector2 worldPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, zFromCamera));
		return worldPos;
	}

	Vector2 SnapToGrid(Vector2 rawVector){
		Vector2 snapVector;
		snapVector.x = Mathf.RoundToInt (rawVector.x);
		snapVector.y = Mathf.RoundToInt (rawVector.y);
		return snapVector;
	}

	void PayDefender(){
		//Button.pay
	}
}
