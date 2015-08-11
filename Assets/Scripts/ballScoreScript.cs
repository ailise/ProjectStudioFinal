using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ballScoreScript : MonoBehaviour
{

	public static int ballScore;
	
	void Awake ()
	{
		
		text = GetComponent<Text> ();
		ballScore = 0;
	}
	// Use this for initialization
	
	public Text text;
	
	// Update is called once per frame
	void Update ()
	{
		text.text = "Ball: " + ballScore;
		//		Debug.Log (text);
		
	}
	
	public void ballScoreIncrease ()
	{
		
		ballScore ++;
		Debug.Log (ballScore);
		
	}

//	public void ballScoreIncrease(){
//
//	}
}
