using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public Vector3 destination;
	public float speed = 100f;

	bool grounded = false;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("PickRandomDestination", 0f, 2f);
	}

	void PickRandomDestination () {
		destination = new Vector3 (Random.Range (30f, 105f), 0f, Random.Range (-10f, 65f));
	}


	void OnDrawGizmos () {
		Gizmos.color = Color.black;
		Gizmos.DrawLine (transform.position, destination);
		Gizmos.DrawWireSphere (destination, 0.5f);
	}
	void Update () {
		Ray ray = new Ray (transform.position, new Vector3 (0, -1, 0));

		if (Physics.Raycast (ray, 1.1f)) {
			grounded = true;
		} else {
			grounded = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 moveDirection = destination - transform.position;
		moveDirection = Vector3.Normalize (moveDirection);

		Ray ray = new Ray (transform.position + moveDirection, Vector3.down);
		bool isThereGroundInFrontOfMe = Physics.Raycast (ray, 1.1f);

		if ( Vector3.Distance (transform.position, destination) > 1f && grounded
		    && isThereGroundInFrontOfMe) {
			GetComponent<Rigidbody> ().AddForce (moveDirection * speed);
		} else {
			GetComponent<Rigidbody>().velocity = Physics.gravity;
			destination = Vector3.zero;
		}
	}
}
