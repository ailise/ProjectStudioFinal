using UnityEngine;
using System.Collections;

public class rollCollect : MonoBehaviour {
	public float speed = 20f;
	Bounds combinedBounds;	// bounds of player + objects collected
	Component[] childRenderers;	// renderers in player children

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		combinedBounds = GetComponent<Renderer>().bounds;

		if(transform.childCount > 0){	
			childRenderers = GetComponentsInChildren<Renderer>();	// find renderers of children objects
			foreach(Renderer childRenderer in childRenderers){
				combinedBounds.Encapsulate(childRenderer.bounds);	// expand combined bounds to fit children bounds
			}
		}

		//Debug.Log(combinedBounds.size);
	}

	void FixedUpdate(){
		// player input
		float x = Input.GetAxis("ballerHorizontal");
		float y = Input.GetAxis("ballerVertical");

		// add force to sphere
		Rigidbody rbody = GetComponent<Rigidbody>();
		rbody.AddForce(x * speed * Time.deltaTime, 0f, y * speed * Time.deltaTime, ForceMode.VelocityChange);
	}

	void OnCollisionEnter(Collision collision){
		// check if collided with pick-up-able object or human player
		// if object's size is less than half of current player size
		// if min and max points of bound box are in player bound box
		if((collision.gameObject.tag == "objects" 
			|| collision.gameObject.tag == "humanPlayerOne" || collision.gameObject.tag == "humanPlayerTwo")
			&& collision.gameObject.GetComponent<Renderer>().bounds.size.sqrMagnitude < combinedBounds.extents.sqrMagnitude
			&& combinedBounds.Contains(collision.gameObject.GetComponent<Renderer>().bounds.max)
			&& combinedBounds.Contains(collision.gameObject.GetComponent<Renderer>().bounds.min)){
			// set collided object as child
			collision.gameObject.transform.parent = this.transform;

			//destroy rigidbody if there is one
			if(collision.gameObject.GetComponent<Rigidbody>() != null){	
				Destroy(collision.gameObject.GetComponent<Rigidbody>());
			}

			// disable player controls if picked up
			if(collision.gameObject.tag == "humanPlayerOne"){
				collision.gameObject.GetComponent<runnerOneControls>().enabled = false;
			}
			else if(collision.gameObject.tag == "humanPlayerTwo"){
				collision.gameObject.GetComponent<runnerTwoControls>().enabled = false;
			}
		}
	}

	// remove objects if hit mine
	// this function is called from mineTrigger
	public void removeObjects(){
	//	Debug.Log("boom");
		if(transform.childCount > 0){	// only do stuff if there are children

			int numObjsRemove = transform.childCount / 3; // remove 1/3 of children (tune this value)
			// unparent last child numObjsRemove times
			while(numObjsRemove >= 0){
				Transform child = transform.GetChild(transform.childCount-1);

				child.parent = null;	// unparent child

				Rigidbody cRbody = child.gameObject.GetComponent<Rigidbody>();
				if(cRbody == null){
					cRbody = child.gameObject.AddComponent<Rigidbody>();	// add rigidbody to removed child if doesn't have one
				}

				// reenable player controls if removing a player
				if(child.tag == "humanPlayerOne"){
					child.gameObject.GetComponent<runnerOneControls>().enabled = true;
					// upright player
					child.transform.eulerAngles = new Vector3(0f, child.transform.eulerAngles.y, 0f);
					// reapply rotation contraints
					cRbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
				}
				else if(child.tag == "humanPlayerTwo"){
					child.gameObject.GetComponent<runnerTwoControls>().enabled = true;
					// upright player
					child.transform.eulerAngles = new Vector3(0f, child.transform.eulerAngles.y, 0f);
					// reapply rotation contraints
					cRbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
				}
				numObjsRemove--;
			}
		}
	}

	// draw bounding box
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, combinedBounds.size);
		Gizmos.DrawWireCube(transform.position, combinedBounds.extents);
	}
}