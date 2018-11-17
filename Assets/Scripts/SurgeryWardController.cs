using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurgeryWardController : Room {


	public Sprite playerLyingDown; 
	private bool once = true;


	// Use this for initialization
	void Start () {

		base.Start ();
		player.setSprite (playerLyingDown);
		
	}
	
	// Update is called once per frame
	void Update () {

		if(once){
			player.pause ();
			once = false;
		}
		
	}
}
