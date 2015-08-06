using UnityEngine;
using System.Collections;

public class minePickupSimple : MonoBehaviour {


	public float respawnTimer;
	public float spin = 10f;

	void OnTriggerEnter (Collider other )
	

	{
		if ( (other.gameObject.tag == "humanPlayerOne" || other.gameObject.tag == "humanPlayerTwo") 
				&& other.gameObject.transform.parent == null ) {
			other.gameObject.GetComponent<mainRunnerControls> ().hasMine = true;
			DestroyObject(gameObject);
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		transform.Rotate (Vector3.right, spin * Time.deltaTime);


	}
}
