using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ballScoreScript : MonoBehaviour
{

	// Use this for initialization
	public float ballScore = 0;
	public Text ballScoreText;
	
	// Use this for initialization
	void Start ()
	{
		
		ballScoreText.text = "Ball: " + ballScore.ToString ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
