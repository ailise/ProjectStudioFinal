﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameOverText : MonoBehaviour
{
	public Text text;
	GameObject humanScore;
	GameObject ballScore;

	// Use this for initialization
	void Start ()
	{
		text = GetComponent<Text> ();
//		humanScore = GetComponent<humanScoreScript> ().humanScorePrint ();
	}
	
	
	// Update is called once per frame
	void Update ()
	{
	
		text.text = "Time's Up!\nPress 'R' to \nRestart!" + humanScore;
		restartGame ();
	
	}
	
	void restartGame ()
	{
	
		if (Input.GetKeyDown (KeyCode.R)) {
			
			Application.LoadLevel ("mainGroupPrototype");
			
		}
	
	}
}
