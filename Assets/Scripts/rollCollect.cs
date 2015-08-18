using UnityEngine;
using System.Collections;

public class rollCollect : MonoBehaviour
{
	public float speed = 15f;
	Bounds combinedBounds;	// bounds of player + objects collected
	Component[] childRenderers;	// renderers in player children

	GameObject text;	// reference ball score

	// roll sounds
	AudioSource[] rollSounds;
	AudioSource light;
	AudioSource medium;
	AudioSource large;

	float waitTime; // time inbetween sound play
	float lastCheck;

	public int fractionRemove = 2;	// denominator of fraction of objects removed when ball hits mine

	// Use this for initialization
	void Start ()
	{
		text = GameObject.FindWithTag ("ballScore");
		rollSounds = GetComponents<AudioSource> ();	// get array of audio sources on object
		large = rollSounds [0];
		medium = rollSounds [1];
		light = rollSounds [2];

		lastCheck = 0f;
		waitTime = 1f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		gameOverText gameOverText = GetComponent<gameOverText> ();
		
		combinedBounds = GetComponent<Renderer> ().bounds;

		if (transform.childCount > 0) {	
			childRenderers = GetComponentsInChildren<Renderer> ();	// find renderers of children objects
			foreach (Renderer childRenderer in childRenderers) {
				combinedBounds.Encapsulate (childRenderer.bounds);	// expand combined bounds to fit children bounds
			}
		}

		// play sounds
		if (Input.GetAxis ("ballerHorizontal") != 0 || Input.GetAxis ("ballerVertical") != 0) {
			if (!medium.isPlaying && Time.time - lastCheck > waitTime) {
				lastCheck = Time.time;
				medium.Play ();
			}
		} else {
			medium.Stop ();
		}

		// change pitch of rolling sound depending on number of children
		if (transform.childCount >= 0) {
			medium.pitch = 1f;
		} else if (transform.childCount > 2) {
			medium.pitch = 0.8f;
		} else if (transform.childCount > 5) {
			medium.pitch = 0.6f;
		} else if (transform.childCount > 10) {
			medium.pitch = 0.4f;
		} else {
			medium.pitch = 0.2f;
		}

		//Debug.Log(combinedBounds.size);
	}

	void FixedUpdate ()
	{
		// player input
		float x = Input.GetAxis ("ballerHorizontal");
		float y = Input.GetAxis ("ballerVertical");

		// add force to sphere
		Rigidbody rbody = GetComponent<Rigidbody> ();
		rbody.AddForce (x * speed * Time.deltaTime, 0f, y * speed * Time.deltaTime, ForceMode.VelocityChange);
		waitTime = Mathf.Lerp (2f, 0.8f, rbody.velocity.magnitude);
		//Debug.Log(waitTime);
	}

	void OnCollisionEnter (Collision collision)
	{
		// check if collided with pick-up-able object or human player
		// if object's size is less than half of current player size
		// if min and max points of bound box are in player bound box
		if ((collision.gameObject.tag == "objects" 
			|| collision.gameObject.tag == "humanPlayerOne" || collision.gameObject.tag == "humanPlayerTwo" || collision.gameObject.tag == "npc")
			&& collision.gameObject.GetComponent<Renderer> ().bounds.size.sqrMagnitude < combinedBounds.extents.sqrMagnitude
			&& combinedBounds.Contains (collision.gameObject.GetComponent<Renderer> ().bounds.max)
			&& combinedBounds.Contains (collision.gameObject.GetComponent<Renderer> ().bounds.min)) {
			// set collided object as child
			collision.gameObject.transform.parent = this.transform;

			//destroy rigidbody if there is one
			if (collision.gameObject.GetComponent<Rigidbody> () != null) {	
				Destroy (collision.gameObject.GetComponent<Rigidbody> ());
			}

			// disable player controls if picked up
			if (collision.gameObject.tag == "humanPlayerOne" || collision.gameObject.tag == "humanPlayerTwo") {
				collision.gameObject.GetComponent<mainRunnerControls> ().enabled = false;
				text.GetComponent<ballScoreScript> ().ballScoreIncrease (); //increments score for Ball player
				large.Play ();
			} else if (collision.gameObject.tag == "npc") {
				collision.gameObject.GetComponent<NPC> ().enabled = false;
				light.Play ();
			} else {
				light.Play ();	// play pickup sound
			}
			
		}
	}
		
	

	// remove objects if hit mine
	// this function is called from mineTrigger
	public void removeObjects ()
	{
		//	Debug.Log("boom");
		if (transform.childCount > 0) {	// only do stuff if there are children

			int numObjsRemove = transform.childCount / fractionRemove; // remove 1/fractionRemove of children (tune this value)
			// unparent last child numObjsRemove times
			while (numObjsRemove >= 0) {
				Transform child = transform.GetChild (transform.childCount - 1);

				child.parent = null;	// unparent child

				Rigidbody cRbody = child.gameObject.GetComponent<Rigidbody> ();
				if (cRbody == null) {
					cRbody = child.gameObject.AddComponent<Rigidbody> ();	// add rigidbody to removed child if doesn't have one
				}

				// reenable player controls if removing a player
				if (child.tag == "humanPlayerOne" || child.tag == "humanPlayerTwo") {
					child.gameObject.GetComponent<mainRunnerControls> ().enabled = true;
					// upright player
					child.transform.eulerAngles = new Vector3 (0f, child.transform.eulerAngles.y, 0f);
					// reapply rotation contraints
					cRbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
				} else if (child.tag == "npc") {	// renable movement if npc
					child.gameObject.GetComponent<NPC> ().enabled = true;
					// upright player
					child.transform.eulerAngles = new Vector3 (0f, child.transform.eulerAngles.y, 0f);
					// reapply rotation contraints
					cRbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
				}
				numObjsRemove--;
				
			}
		}
	}

	// helper function to choose which sound to play depending on size of ball
	AudioSource chooseSound ()
	{
		if (transform.childCount <= 10) {
			if (large.isPlaying) {
				large.Stop ();
			}
			return medium;
		} else {
			if (medium.isPlaying) {
				medium.Stop ();
			}
			return large;
		}
	}

	// draw bounding box
	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (transform.position, combinedBounds.size);
		Gizmos.DrawWireCube (transform.position, combinedBounds.extents);
	}
}