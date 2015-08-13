using UnityEngine;
using System.Collections;

public class mineTrigger : MonoBehaviour
{
	float upForce = 10f;
	GameObject text;	// reference human score
	float destroyTimer = 10f;

	public GameObject explosion;	// referenced in mainRunnerControls

	void Start(){


		text = GameObject.FindWithTag("humanScore");
		StartCoroutine ( mineDeathTimer () );
	}

	IEnumerator mineDeathTimer () {
		Debug.Log ("Started death time");
		yield return new WaitForSeconds (10);
		Destroy (this.gameObject);
		Debug.Log ("Destroyed self, 10 seconds up");
		GameObject.FindWithTag("mineTimedOut").GetComponent<AudioSource>().Play ();

	}

	void OnTriggerEnter (Collider ball)
	{
		
		// check if collider is ball
		if (ball.gameObject.tag == "ballerPlayer") {	// if ball player object enters
			ball.gameObject.GetComponent<Rigidbody> ().AddForce (0f, upForce, 0f, ForceMode.Impulse);
			ball.gameObject.GetComponent<rollCollect> ().removeObjects (); // call function to remove objects
			text.GetComponent<humanScoreScript> ().humanScoreIncrease ();
			GameObject.FindWithTag("explosionAudio").GetComponent<AudioSource>().Play();


			explosion.GetComponent<ParticleSystem>().Play (); //new explosion system, gives null pointer
		
			Destroy (this.gameObject);	// remove self
			
		} else if ((ball.gameObject.tag == "objects" || ball.gameObject.tag == "humanPlayerOne" || ball.gameObject.tag == "humanPlayerTwo")
				 && ball.gameObject.transform.parent != null) {	// if ball player's children enter
			Debug.Log (ball.gameObject.transform.parent);
			ball.gameObject.transform.parent.GetComponent<Rigidbody> ().AddForce (0f, upForce, 0f, ForceMode.Impulse);
			// get parent (ball player) and call function in parent's script
			ball.gameObject.transform.parent.GetComponent<rollCollect>().removeObjects ();
			text.GetComponent<humanScoreScript> ().humanScoreIncrease ();
			GameObject.FindWithTag("explosionAudio").GetComponent<AudioSource>().Play();

			explosion.GetComponent<ParticleSystem>().Play (); //new explosion system attempt, gives null pointer

			Destroy (this.gameObject);	// remove self
		}
	}
	
}
