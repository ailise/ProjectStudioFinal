using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class humanScoreScript : MonoBehaviour
{

	public static int humanScore;
	
	void Awake ()
	{
	
		text = GetComponent<Text> ();
		humanScore = 0;
	}
	
	public Text text;
	
	// Update is called once per frame
	void Update ()
	{
		text.text = "Humans: " + humanScore;
//		Debug.Log (text);
		
	}
	
	public void humanScoreIncrease ()
	{
	
		humanScore ++;
		Debug.Log (humanScore);
	
	}
	
	public void humanScorePrint ()
	{
	
		humanScore.ToString ();
	
	}
	
	
//	void UpdateScore ()
//	{
//	
//	
//	}
	
	

}
