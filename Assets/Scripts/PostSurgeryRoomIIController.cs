using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSurgeryRoomIIController : Room {

	public Sprite playerSprite; 
	private bool once; 

	// Use this for initialization
	void Start () {
		base.Start ();
		once = true; 
	}
	
	// Update is called once per frame
	void Update () {

		if(once){
			// change player sprite and pause
			player.setSprite (playerSprite);
			player.pause ();
			once = false; 
		}

		GameObject.FindGameObjectWithTag ("Player").GetComponent <Rigidbody2D>().Sleep ();

	}
}
