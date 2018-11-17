using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

	private Vector2 direction; 
	private PlayerController player;
	private float speed; 

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

		// move in direction until hit something
		transform.Translate (direction*Time.deltaTime*speed);
	}

	public void setDir(Vector2 dir, float s){
		direction = dir;
		speed = s; 
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){
			// player has to reset
			// have the player take a small amount of damage?
			transform.parent.Find("Resetter").GetComponent <DoorController>().transitionRooms ();
			player.addInventory ("Energy: ", -2);

		}
		else if(other.tag != "Laser"){	// ignore collisions with like types 
			// destroy the game object 
			Destroy (gameObject);
		}

	}

}
