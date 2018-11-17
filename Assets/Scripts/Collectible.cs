using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to model a collectible item 
// Common behaviors include disappearing when "picked up" by player and being added to inventory under a tag name

public class Collectible : MonoBehaviour {

	public string tag; 			// set via unity editor :/
	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		
		// if item is collected by player, deactivate and add to inventory
		if (collider.tag == "Player") {
			GetComponent <AudioSource>().Play ();
			player.addInventory (tag, 1);
			// can't completely disable yet or sound won't play
			gameObject.GetComponent <SpriteRenderer>().enabled = false;
			gameObject.GetComponent <Collider2D>().enabled = false;
		}

	}
}
