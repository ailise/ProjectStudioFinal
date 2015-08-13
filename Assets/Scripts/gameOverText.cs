using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameOverText : MonoBehaviour
{
	public Text text;

	public Text timer;

	// Use this for initialization
	void Start ()
	{
		text = GetComponent<Text> ();
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		humanScoreScript humanScoreScript = GetComponent<humanScoreScript> ();
		ballScoreScript ballScoreScript = GetComponent<ballScoreScript> ();
		if (humanScoreScript.humanScore > ballScoreScript.ballScore) {
		
			text.text = "Human Players Win!\nPress 'R' to Restart!";
	
		} 
		
		if (humanScoreScript.humanScore < ballScoreScript.ballScore) {
		
			text.text = "Ball Player Wins!";
		
		}
		
		if (humanScoreScript.humanScore == ballScoreScript.ballScore) {
		
			text.text = "It's a Tie!\nPress 'R' to Restart!";
		
		}

//		Debug.Log (text.text);
		restartGame ();
		
	
	}
	
	void restartGame ()
	{
	
		if (Input.GetKeyDown (KeyCode.R) && timer.GetComponent<TimerScript> ().end == true) {
			
			Application.LoadLevel ("mainGroupPrototype");	// this is crashing the game
			
		}
	
	}
}
