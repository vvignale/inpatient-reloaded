using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InterludeRoomController : Room {


	private bool once; 
	private float windTime = 25f; 
	private float timer; 

	// room that simply shows and plays an animation of a soccer field

	// Use this for initialization
	void Start () {

		base.Start ();
		GameObject.FindGameObjectWithTag ("Inventory").GetComponent <Text> ().enabled = false; 
		playerObj.GetComponent <SpriteRenderer>().enabled = false; 

		timer = 0; 
		once = true; 

		GetComponent <AudioSource>().Play ();
	}

	// Update is called once per frame
	void Update () {

		if(once){
			player.pause ();
			once = false; 
		}

		// play the audio source and the animations for the needed period of time 
		timer += Time.deltaTime; 
		if(timer >= windTime){
			// start the transition to the next room. music will turn itself off 
			transform.Find ("InterludeRoom_Exit").GetComponent <DoorController> ().transitionRooms ();
		}
			
	}
}
