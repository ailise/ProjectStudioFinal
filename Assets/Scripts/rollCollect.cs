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
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		// add force to sphere
		Rigidbody rbody = GetComponent<Rigidbody>();
		rbody.AddForce(x * speed * Time.deltaTime, 0f, y * speed * Time.deltaTime, ForceMode.VelocityChange);
	}

	void OnCollisionEnter(Collision collision){
		// check if collided with pick-up-able object
		// if object's size is less than half of current player size
		// if min and max points of bound box are in player bound box
		if(collision.gameObject.tag == "objects" 
			&& collision.gameObject.GetComponent<Renderer>().bounds.size.sqrMagnitude < combinedBounds.extents.sqrMagnitude
			&& combinedBounds.Contains(collision.gameObject.GetComponent<Renderer>().bounds.max)
			&& combinedBounds.Contains(collision.gameObject.GetComponent<Renderer>().bounds.min)){
			// set collided object as child
			collision.gameObject.transform.parent = this.transform;
		}
		
	}

	// draw bounding box
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, combinedBounds.size);
		Gizmos.DrawWireCube(transform.position, combinedBounds.extents);
	}
}
