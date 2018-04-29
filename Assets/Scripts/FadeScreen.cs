using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {

	public float fadeInTime = 0.8f,  fadeOutTime = 0.8f, textDelay = 1f;
	public bool useStartText = false, useEndText= false;
	public string startText, endText;

	private Image fadePanel;
	private Text text;
	private Color currentColor = Color.black;
	private float sceneTime, startFadeOutTime;
	private bool isAutoNext = false, isManualNext = false,switchA=true, switchB=true,switchC=true, switchD=true; 

	// Use this for initialization
	void Start () {
		fadePanel = GetComponent<Image> ();
		// If the level manager exist and autoLoadNext >0, the scene is a splash screen, thus fade out should set automatically.
		if (GameObject.FindObjectOfType<LevelManager> () && GameObject.FindObjectOfType<LevelManager> ().autoLoadNextLevel>0) {
			sceneTime = GameObject.FindObjectOfType<LevelManager> ().autoLoadNextLevel;
			startFadeOutTime = sceneTime-fadeOutTime;
			isAutoNext = true;
			Debug.Log("Auto next activated, starting fade out after " + startFadeOutTime + isAutoNext);
			}
		if (useStartText && GameObject.Find("LevelInfo")){
			text = GameObject.Find("LevelInfo").GetComponent<Text>();
			text.text = startText;
			text.enabled = true;
		}else {Debug.Log(name + ": LevelInfo disable."); }
	}
	
	// Update is called once per frame
	void Update (){
		Invoke ("FadeIn", textDelay);
		//FadeIn ();
		if (isAutoNext) {AutoFadeOut ();}
		if (isManualNext) {ManualFadrOut();}
	}

	//Public void
	public void ManualFadrOut(){
		if (!isManualNext) {isManualNext = true;}
		FadeOut ();
	}
		
	// Private void
	void FadeIn ()
	{	
		if (useStartText && text.enabled && switchA) {
			switchA = false;
			text.enabled = false;
		}
		if (Time.timeSinceLevelLoad < (fadeInTime+textDelay)) {
			float alphaChange = Time.deltaTime / fadeInTime;
			currentColor.a -= alphaChange;
			fadePanel.color = currentColor;
		} else if (Time.timeSinceLevelLoad > (fadeInTime+textDelay) && switchB) {
			fadePanel.enabled = false ;
			switchB = false;
			Debug.Log ("Deactive switch Closed");
		}
	} 

	void AutoFadeOut (){
		if (Time.timeSinceLevelLoad >= startFadeOutTime) {
			FadeOut();	
			Debug.Log ("Auto fade out");
		} 
	}


	void FadeOut ()
	{
		if (switchC) {
			switchC = false;
			fadePanel.enabled = true;
			Debug.Log("Active switch Closed");
		}
		if (currentColor.a < 1f) {
			float alphaChange = Time.deltaTime / fadeOutTime;
			currentColor.a += alphaChange;
			fadePanel.color = currentColor;
		} else if(useEndText && switchD){
			switchD = false;
			text.text = endText;
			text.enabled = true;
		}
	}
}
