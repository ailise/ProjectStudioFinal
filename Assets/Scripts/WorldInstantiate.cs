using UnityEngine;
using System.Collections;

public class WorldInstantiate : MonoBehaviour {

	public GameObject cube;
	public GameObject sphere;
	public int numberOfCubes = 5;
	public int numberOfSpheres = 5;

	// Use this for initialization
	void Start () {

		PlaceCubes();
		PlaceSpheres();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlaceCubes() {

		for (int i = 0; i < numberOfCubes; i++) {
			Vector3 pos = new Vector3 (Random.Range (-10f, 10f), 0, Random.Range (-10f, 10f)) + transform.position;

			Instantiate (cube, pos, Quaternion.identity); 
		}


	}

	void PlaceSpheres() {
		
		for (int j = 0; j < numberOfSpheres; j++) {
			Vector3 pos = new Vector3 (Random.Range (-10f, 10f), 0, Random.Range (-10f, 10f)) + transform.position;
			
			Instantiate (sphere, pos, Quaternion.identity); 
		}
		
		
	}


}

