using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDoorTrigger : MonoBehaviour {


	private EscapeNurseController nurse; 

	// Use this for initialization
	void Start () {
		nurse = transform.parent.Find ("Nurse").GetComponent <EscapeNurseController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){

			if(!nurse.isAsleep ()){
				nurse.scoldPlayer ();
			}
		}

	}
}
