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
//		humanScore = GetComponent<humanScoreScript> ().humanScorePrint ();
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		humanScoreScript humanScoreScript = GetComponent<humanScoreScript> ();
		ballScoreScript ballScoreScript = GetComponent<ballScoreScript> ();
//		humanScoreResult = GetComponent<humanScoreScript> ().humanScorePrint ();
		if (humanScoreScript.humanScore > ballScoreScript.ballScore) {
		
			text.text = "Human Players Win!";
	
		} else {
		
			text.text = "Ball Player Wins!";
		
		}
//		text.text = "Humans: " + humanScoreScript.humanScore;
//		text.text = "Test";
		Debug.Log (text.text);
		restartGame ();
		
	
	}
	
	void restartGame ()
	{
	
		if (Input.GetKeyDown (KeyCode.R) && timer.GetComponent<TimerScript> ().end == true) {
			
			Application.LoadLevel ("mainGroupPrototype");	// this is crashing the game
			
		}
	
	}
}
