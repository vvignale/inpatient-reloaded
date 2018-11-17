using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaintTriggerController : MonoBehaviour {

	private PlayerController player;
	private bool faintSequence;

	private float shortTimer;
	private float shortTime; 

	// Use this for initialization
	void Start () {
		faintSequence = false; 
		player = GameObject.FindObjectOfType<PlayerController> ();
		shortTime = 6f;
		shortTimer = -1; 
	}
	
	// Update is called once per frame
	void Update () {
		if(faintSequence){
			
			if(shortTimer != -1){
				shortTimer += Time.deltaTime; 
				if(shortTimer > shortTime){
					player.makeSleepy ();
					shortTimer = -1; 
				}
			}
				
			// reduce player energy 
			player.addInventory ("Energy: ", -.5f);

			if(player.getInventory ("Energy: ") < 2f){
				// slow transition 
				// turn down volume of everything 
				// interlude 2 room 
				transform.parent.Find("RadRoom4_Exit").GetComponent <SlowDoor>().transitionRooms ();
			}

		}
	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player"){
			faintSequence = true;
			// temporarily turn off player's ability to reboot 
			player.setReboot (false);
			shortTimer = 0; 
		} 
	}
}
