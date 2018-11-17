using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseTrigger : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){

			// initialize the chase
			transform.parent.Find("Nurse").GetComponent <NurseChase>().startChase (); 
		}
	}
}
