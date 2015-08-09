using UnityEngine;
using System.Collections;

public class WorldInstantiate : MonoBehaviour {

	public Transform floorPrefab;

	public Transform bushPrefab;
	public Transform buildingPrefab;
	public Transform hydrantPrefab;
	public Transform lightPrefab;
	public Transform sTreePrefab;
	public Transform lTreePrefab;

	public Color[] groundColors;

/*	public GameObject cube;
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
*/



	void Start(){

		// create 15 x 25 map made up of 5x5 grids
		for(int i = 0; i < 25; i += 5){
			for(int j = 0; j < 15; j += 5){
				transform.position = new Vector3(i * 5f, 0f, j * 5);
				gridInstantiate();
			}
		}
		
	}

	// helper function to generate 5x5 grid with objects
	void gridInstantiate(){

		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				Vector3 pos = new Vector3(i * 5f, -1f, j * 5) + transform.position;

				// instantiate floor
				Transform newClone = (Transform) Instantiate(floorPrefab, pos, transform.rotation);
				
				if(i == 2 || j == 2){ // if street
					newClone.GetComponent<Renderer>().material.color = groundColors[0];
				}
				else{
					float rand = Random.Range(0f,1f);	// type of area
					float rand2 = Random.Range(0f,1f);	// options for type of area

					if(rand < 0.5f){	// grassy terrain
						if(rand2 < 0.4){	// a lot of bushes
							int numObjs = Random.Range(2,5);
							placePrefabs(bushPrefab, pos, numObjs);
						}
						else if(rand2 < 0.7){	// balance of bush, small trees and large trees
							int numObjs = Random.Range(1, 3);
							placePrefabs(bushPrefab, pos, numObjs);
							numObjs = Random.Range(1, 3);
							placePrefabs(sTreePrefab, pos, numObjs);
							numObjs = Random.Range(0, 2);
							placePrefabs(lTreePrefab, pos, numObjs);
						}
						else if(rand2 <= 0.1){	// only trees
							int numObjs = Random.Range(1, 4);
							placePrefabs(sTreePrefab, pos, numObjs);
							numObjs = Random.Range(0, 4);
							placePrefabs(lTreePrefab, pos, numObjs);
						}
						newClone.GetComponent<Renderer>().material.color = groundColors[1];
					}
					else{	// concrete terrain
						if(rand2 < 0.7){	// building with hyrant and light
							placePrefabs(buildingPrefab, pos, 1);
							placePrefabs(hydrantPrefab, pos, 1);
							int numObjs = Random.Range(0, 3);
							placePrefabs(lightPrefab, pos, numObjs);
						}
						else{	// hyrant and light
							int numObjs = Random.Range(0, 3);
							placePrefabs(lightPrefab, pos, numObjs);
							placePrefabs(hydrantPrefab, pos, 1);
						}
						newClone.GetComponent<Renderer>().material.color = groundColors[2];
					}
						
				}
			}
		}
	}

	// helper function to place prefabs in square
	void placePrefabs(Transform obj, Vector3 currPos, int numObjects){
		Vector3 pos;
		Quaternion rot = Quaternion.identity;

		for(int i = 0; i < numObjects; i++){
			if(obj == buildingPrefab){	// place building in center of square
				pos = new Vector3(0f, 1f, 0f) + currPos;
				float rand = Random.Range(0f,1f);
				if(rand < 0.5f){	// building either normal or rotated 90 degrees
					rot.eulerAngles = new Vector3(0f, 90f, 0f);
				}
			}
			else if(obj == hydrantPrefab){	// place hyrant along east/west sides of square
				float rand = Random.Range(0f,1f);
				if(rand < 0.5f){
					pos = new Vector3(-2f, 1f, Random.Range(-2f, 2f)) + currPos;
				}else{
					pos = new Vector3(2f, 1f, Random.Range(-2f, 2f)) + currPos;
				}
				rot.eulerAngles = new Vector3(0f, Random.Range(0f,365f), 0f);
			}
			else if(obj == lightPrefab){	// place light post along north/south sides of square
				float rand = Random.Range(0f,1f);
				if(rand < 0.5f){
					pos = new Vector3(Random.Range(-2f, 2f), 1f, -2f) + currPos;
				}else{
					pos = new Vector3(Random.Range(-2f, 2f), 1f, 2f) + currPos;
				}
				rot.eulerAngles = new Vector3(0f, Random.Range(0f,365f), 0f);
			}
			else{	// other object positions randomly in square
				pos = new Vector3(Random.Range (-2f, 2f), 1f, Random.Range(-2f, 2f)) + currPos;
				rot.eulerAngles = new Vector3(0f, Random.Range(0f,365f), 0f);
			}
					
			Instantiate (obj, pos, rot); 
		}	
	}


}

