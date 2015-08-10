﻿using UnityEngine;
using System.Collections;

public class mineTrigger : MonoBehaviour
{
	float upForce = 10f;
	GameObject text;	// reference human score

	void Start(){
		text = GameObject.FindWithTag("humanScore");
	}

	void OnTriggerEnter (Collider ball)
	{
		
		// check if collider is ball
		if (ball.gameObject.tag == "ballerPlayer") {	// if ball player object enters
			ball.gameObject.GetComponent<Rigidbody> ().AddForce (0f, upForce, 0f, ForceMode.Impulse);
			ball.gameObject.GetComponent<rollCollect> ().removeObjects (); // call function to remove objects
			text.GetComponent<humanScoreScript> ().humanScoreIncrease ();
			GameObject.FindWithTag("explosionAudio").GetComponent<AudioSource>().Play();

			Destroy (this.gameObject);	// remove self
			
		} else if (ball.gameObject.tag == "objects" && ball.gameObject.transform.parent != null) {	// if ball player's children enter
			Debug.Log (ball.gameObject.transform.parent);
			ball.gameObject.transform.parent.GetComponent<Rigidbody> ().AddForce (0f, upForce, 0f, ForceMode.Impulse);
			// get parent (ball player) and call function in parent's script
			ball.gameObject.transform.parent.GetComponent<rollCollect> ().removeObjects ();
			text.GetComponent<humanScoreScript> ().humanScoreIncrease ();
			GameObject.FindWithTag("explosionAudio").GetComponent<AudioSource>().Play();

			Destroy (this.gameObject);	// remove self
		}
	}
	
}
