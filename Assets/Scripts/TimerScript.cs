﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

	public float timeRemaining = 60f;
	public Text timer;

	public bool end;
	
	void Start ()
	{
		InvokeRepeating ("decreaseTimeRemaining", 1.0f, 1.0f);
		end = false;
		//	BroadcastMessage ("Start Timer", timeRemaining);
	}
	
	void Update ()
	{
//		if (timeRemaining == 0) {
//			sendMessageUpward ("timeElapsed");
//		}
		
		timer.text = timeRemaining.ToString ();
		
	}
	
	void decreaseTimeRemaining ()
	{
		if (timeRemaining > 0) {
		
			timeRemaining --;
		
		} else if (timeRemaining == 0) {
		
			//Destroy (this.gameObject);
			
		
		}
		
		if (timeRemaining == 0) {
		
			GameObject gameOverScreen = Instantiate (Resources.Load ("gameOverScreen")) as GameObject;
			gameOverScreen.transform.SetParent (GameObject.FindGameObjectWithTag ("gameOverCanvas").transform, false);
			gameOverScreen.transform.GetChild(0).GetComponent<gameOverText>().timer = timer;
			Debug.Log ("I did a thing!");
//			GameObject gameOverText = Instantiate (Resources.Load ("gameOverText")) as GameObject;
//			gameOverText.transform.SetParent (GameObject.FindGameObjectWithTag ("gameOverScreen").transform, false);
			
			end = true;
			Time.timeScale = 0f;	// pause game
		}
		
	}
}
