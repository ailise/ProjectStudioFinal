using UnityEngine;
using System.Collections;

public class minePickup : MonoBehaviour {


	public float respawnTimer;
	public float spin = 10f;

	void OnTriggerEnter (Collider other )
	{
		if ( other.gameObject.tag == "humanPlayerOne" ) {
			GameObject.FindWithTag ("humanPlayerOne").GetComponent<runnerOneControls> ().hasMine = true;
			DestroyObject(gameObject);
		} else if ( other.gameObject.tag == "humanPlayerTwo" ) {
			GameObject.FindWithTag ("humanPlayerTwo").GetComponent<runnerTwoControls> ().hasMine = true;
			DestroyObject(gameObject);
		}
	}


	//Old code, all players collect mines, both players get mine if one collect a mine

	//	if (//GameObject.FindWithTag ("Player").GetComponent<runnerOneControls> ().hasMine) {
	//		GameObject.FindWithTag ("humanPlayerOne").GetComponent<runnerOneControls> ().hasMine = true;
	//		GameObject.FindWithTag ("humanPlayerTwo").GetComponent<runnerTwoControls> ().hasMine = true;
	//		DestroyObject(gameObject);
		
		//} else {
		//	Debug.Log ( "Already has a mine" );
		//}
	//}
	//
			
	//	}
		//Debug.Log ("We have trigged");
	//}

	//End old code


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		transform.Rotate (Vector3.right, spin * Time.deltaTime);


	}
}
