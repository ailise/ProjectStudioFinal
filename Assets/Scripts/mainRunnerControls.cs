using UnityEngine;
using System.Collections;

public class mainRunnerControls : MonoBehaviour {

	//Naming the axis of the player controls. Different from the name of the axis in the Project Setting Inputs.
	public string horizontalAxis;
	public string verticalAxis;


	public GameObject runner; //Assign in Inspector. Assign Runner one to runner one, runner two to runner two
	public GameObject mine; //Assign in Inspector, assign to Mine Prefab

	public bool hasMine = false;

	//Public keycodes for runner one and two.
	public KeyCode mineButtonPlayerOne; //Only assign in the inspector for runnerOne
	public KeyCode mineButtonPlayerTwo; //Only assign in the inspector for runnerTwo
	

	public GameObject explosionParticle;

	public float speed = 500f;
	//Used Speed + Drag
	//Mass 0.5f, Drag 1f, Speed 500f
	

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Debug, check for hasMine
		if (hasMine == true ) {
			Debug.Log ("Obtained Mine");
		}


		//Control Axis' for running players
		float runnerX = Input.GetAxis (horizontalAxis) * speed * Time.deltaTime;
		float runnerZ = Input.GetAxis (verticalAxis) * speed * Time.deltaTime;
	
		Rigidbody rbody = GetComponent<Rigidbody> ();


		//Implementing the controls

		//Left + Right
		rbody.AddForce (runnerX, 0f , 0f);
		//Up + Down
		rbody.AddForce (0f, 0f, runnerZ);
	
		//Mine placement, sets a mine if the player has one.
		if (Input.GetKeyDown (mineButtonPlayerOne )&& hasMine == true ) {
			GameObject placemine;
			placemine = (GameObject)Instantiate( mine, 
			                                    new Vector3 (runner.transform.localPosition.x,
			             									0f,
			             									runner.transform.localPosition.z),
			                                    Quaternion.Euler ( 0f, 0f, 0f ));
			hasMine = false;

			placemine.GetComponent<mineTrigger>().explosion = (GameObject) Instantiate(explosionParticle, new Vector3 (runner.transform.localPosition.x,
			        		0f,
			             	runner.transform.localPosition.z),
			                Quaternion.Euler ( 0f, 0f, 0f ));

			//Debug log, for coding purposed
			Debug.Log ("Placed Mine");

			                        }

		if (Input.GetKeyDown (mineButtonPlayerTwo )&& hasMine == true ) {
			GameObject placemine;
			placemine = (GameObject)Instantiate( mine, 
			                                    new Vector3 (runner.transform.localPosition.x, 0f, runner.transform.localPosition.z),
			                                    Quaternion.Euler ( 0f, 0f, 0f ));
			hasMine = false;
			placemine.GetComponent<mineTrigger>().explosion = (GameObject) Instantiate(explosionParticle, new Vector3 (runner.transform.localPosition.x,
			        		0f,
			             	runner.transform.localPosition.z),
			                Quaternion.Euler ( 0f, 0f, 0f ));
			//Debug log, for coding purposed
			Debug.Log ("Placed Mine");
			
		}



	}
}
