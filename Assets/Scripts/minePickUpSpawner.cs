using UnityEngine;
using System.Collections;

public class minePickUpSpawner : MonoBehaviour {
	public float respawnTime;
	public Transform minePickup;

	float timer;
	public bool mineUp;

	// Use this for initialization
	void Start () {
		mineUp = false;
		resetTimer();
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0 && mineUp == false){
			Transform newMine = (Transform) Instantiate(minePickup, transform.position, transform.rotation);
			newMine.GetComponent<minePickupSimple>().spawner = transform.gameObject;
			mineUp = true;
			resetTimer();
		}
	}

	public void resetTimer(){
		timer = respawnTime;
	}
}
