using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : DoorController {

	private GameObject interactionTriangle;
	protected bool inRange; 

	// Use this for initialization
	void Start () {

		inRange = false; 
		Vector3 trianglePos = new Vector3(transform.position.x, transform.position.y+2, transform.position.z-.01f);
		interactionTriangle = Instantiate(Resources.Load("Prefabs/InteractionTriangle"), trianglePos, transform.rotation) as GameObject;
		interactionTriangle.transform.SetParent (gameObject.transform); 
		interactionTriangle.GetComponent <SpriteRenderer>().enabled = false;	// can't see by default 
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Return) && inRange){
			base.transitionRooms ();
		}
	}

	public void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			interactionTriangle.GetComponent <SpriteRenderer> ().enabled = true;
			inRange = true;
		}
	}

	public void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			interactionTriangle.GetComponent <SpriteRenderer> ().enabled = false;
			inRange = false;
		}
	}
}
