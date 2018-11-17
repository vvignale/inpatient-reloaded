using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){

		if (transform.parent.Find("Nurse").GetComponent <RestingNurseController>().getReadyForBed ()){
			if(other.tag == "Player"){
				transform.parent.Find ("Exit").GetComponent <DoorController> ().transitionRooms ();
			}
		}
	}
}
