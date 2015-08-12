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

	public Transform spider;

	public Transform minePickUpSpawner;
	public Color[] groundColors;

	public int numSpiders;	// number spiders spawned per grid

	void Start(){

		// create 15 x 25 map made up of 5x5 grids
		for(int i = 0; i < 28; i += 4){
			for(int j = 0; j < 16; j += 4){
				transform.position = new Vector3(i * 5f, 0f, j * 5f);

				if(i == 0 || i == 20 || j == 12 || i == 4 || i == 24){
					// do nothing
				}
				else{
					gridInstantiate();
				}
				
			}
		}
		
	}

	// helper function to generate 5x5 grid with objects
	void gridInstantiate(){

		for(int i = 0; i < 4; i++){
			for(int j = 0; j < 4; j++){
				Vector3 pos = new Vector3(i * 5f, -1f, j * 5) + transform.position;
				// instantiate floor
					Transform newClone = (Transform) Instantiate(floorPrefab, pos, transform.rotation);
					if(i == 3 || j == 3){ // if street
						newClone.GetComponent<Renderer>().material.color = groundColors[0];

						if((i == 3 && j == 2) || (i == 2 && j == 3)){
							int count = numSpiders;
							while(count > 0){
								Instantiate(spider, pos + new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-2.5f, 2.5f)), Quaternion.Euler(0f, Random.Range(0f,360f), 0f));
								count--;
							}
							count = numSpiders;
						}

						if(i == 3 && j == 3){
							Instantiate(minePickUpSpawner, pos + new Vector3(0f, 2f, 0f), transform.rotation);
						}
						// instantiate AI people
					}
					else{
						float rand = Random.Range(0f,1f);	// type of area
						float rand2 = Random.Range(0f,1f);	// options for type of area

						if(rand < 0.85f){	// grassy terrain
							if(rand2 < 0.4){	// a lot of bushes
								int numObjs = Random.Range(2,5);
								placePrefabs(bushPrefab, pos, numObjs);
							}
							else if(rand2 < 0.7){	// balance of bush, small trees and large trees
								int numObjs = Random.Range(1, 3);
								placePrefabs(bushPrefab, pos, numObjs);
								numObjs = Random.Range(1, 3);
								placePrefabs(sTreePrefab, pos, numObjs);
								numObjs = Random.Range(1, 3);
								placePrefabs(lTreePrefab, pos, numObjs);
							}
							else{	// only trees
								int numObjs = Random.Range(1, 4);
								placePrefabs(sTreePrefab, pos, numObjs);
								numObjs = Random.Range(1, 4);
								placePrefabs(lTreePrefab, pos, numObjs);
							}
							newClone.GetComponent<Renderer>().material.color = groundColors[1];
						}
						else{	// concrete terrain
								// building with hyrant and light
							placePrefabs(buildingPrefab, pos, 1);
							placePrefabs(hydrantPrefab, pos, 1);
							int numObjs = Random.Range(0, 3);
							placePrefabs(lightPrefab, pos, numObjs);
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
					rot.eulerAngles = new Vector3(0f, 90f, 0f);
				}else{
					pos = new Vector3(Random.Range(-2f, 2f), 1f, 2f) + currPos;
					rot.eulerAngles = new Vector3(0f, -90f, 0f);
				}
				
			}
			else{	// other object positions randomly in square
				pos = new Vector3(Random.Range (-2f, 2f), 1f, Random.Range(-2f, 2f)) + currPos;
				rot.eulerAngles = new Vector3(0f, Random.Range(0f,365f), 0f);
			}
					
			Instantiate (obj, pos, rot); 
		}	
	}


}

