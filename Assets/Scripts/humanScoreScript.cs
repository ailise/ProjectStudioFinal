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
	// Use this for initialization
	void Start ()
	{
	
//		humanScore = 0;
//		UpdateScore ();
	
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
	
	
//	void UpdateScore ()
//	{
//	
//	
//	}
	
	

}
