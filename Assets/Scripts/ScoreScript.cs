using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

	public float score = 0;
	public Text scoreText;

	// Use this for initialization
	void Start ()
	{
	
		scoreText.text = score.ToString ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
