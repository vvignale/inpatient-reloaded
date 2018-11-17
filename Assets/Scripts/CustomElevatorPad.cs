using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomElevatorPad : ElevatorPadController {

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Return) && playerOn) {
			// call back to the nurse to continue chasing 
			transform.parent.transform.parent.Find("Nurse").GetComponent <NurseChase>().jumpIndex ();
		}

		base.Update ();
	}

}
