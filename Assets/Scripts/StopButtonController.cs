using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButtonController : Interactable {

	private MovablePlatform platform; 

	// Use this for initialization
	void Start () {
		inRange = false; 
		platform = transform.parent.Find ("Platform").GetComponent <MovablePlatform> (); 
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) && inRange){
			// toggle on or off the platform's motion 
			if (platform.getFrozen ())
				platform.toggleFrozen (false);
			else
				platform.toggleFrozen (true);

		}
	}


	public override void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player")
			inRange = true; 
		base.OnTriggerEnter2D (other);
	}

	public override void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player")
			inRange = false; 
		base.OnTriggerExit2D (other);
	}
}
