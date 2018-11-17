using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CallNurseButton : Interactable { 

	private GameObject nurse;
	private PermanentRoomController room; 

	void OnEnable(){
		// have nurse waiting invisible by the door 
		nurse = transform.parent.transform.Find("PermanentRoomNurse").gameObject;
		nurse.GetComponent <SpriteRenderer>().enabled = false;

		interact = true; 
	}

	// Use this for initialization
	void Start () {
		base.Start ();
		room = transform.parent.gameObject.GetComponent <PermanentRoomController> ();
		OnEnable ();
	}
	
	// Update is called once per frame
	void Update(){
		
		if (Input.GetKeyDown (KeyCode.Return) && inRange && canInteract () && !room.getTimerPause ()) {
			//questionAsked = true;

			room.setTimerPause (true);
			// have the nurse walk over and see what you need 
			nurse.GetComponent <PermanentNurseController>().greetPatient ();
			interact = false;	// can't keep calling while they are doing their thing (reset later)

		}
		
	}



}
