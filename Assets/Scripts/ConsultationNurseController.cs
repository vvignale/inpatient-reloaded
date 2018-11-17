using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultationNurseController : MonoBehaviour {

	private float xTarget;
	private bool walk; 
	private float speed; 

	// Use this for initialization
	void Start () {
		walk = false; 
		gameObject.GetComponent <SpriteRenderer> ().enabled = false;
		speed = 2f; 
		xTarget = transform.parent.Find ("Doctor").transform.position.x + 2;	// come to rest beside doctor 
	}
	
	// Update is called once per frame
	void Update () {
		if(walk){

			float distance = xTarget - transform.position.x; 
			int dir; 
			if (distance < 0)
				dir = -1;
			else
				dir = 1; 

			if (Mathf.Abs (distance) < .01) {
				transform.position = new Vector3 (xTarget, transform.position.y, transform.position.z);
				walk = false; 
			}

			transform.Translate (new Vector3 (Time.deltaTime * dir * 2f, 0, 0));
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		// disappear out the door. may handle talking, etc in other room 
		if(other.tag == "Door")
			gameObject.GetComponent <SpriteRenderer> ().enabled = false;
		
	}

	public void leavingWalk(){
		// change the target and start walking
		xTarget = transform.parent.Find("ConsultExitDoor").transform.position.x; 
		walk = true; 
	}

	public void startWalk(){
		// starts the nurse walking up to the doctor
		gameObject.GetComponent <SpriteRenderer> ().enabled = true;
		walk = true;
	}


}
