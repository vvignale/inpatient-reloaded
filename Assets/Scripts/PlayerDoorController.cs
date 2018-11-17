using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorController : InteractableDoor {

	void Update () {
		if(Input.GetKeyDown (KeyCode.Return) && inRange){
			transform.parent.Find ("PatientRoomHall_Exit").GetComponent <DoorController> ().setLock (false);
			base.transitionRooms ();
		}
	}
}
