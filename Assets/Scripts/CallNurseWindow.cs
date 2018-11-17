using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallNurseWindow : MonoBehaviour {

	private float callTimer;
	private float maxTime; 

	// Use this for initialization
	void Start () {

		maxTime = 3f; 
		callTimer = -1;
	}
	
	// Update is called once per frame
	void Update () {

		if(callTimer != -1){
			callTimer += Time.deltaTime; 
			if(callTimer >= maxTime){
				transform.parent.Find ("Nurse").GetComponent <NightCollectionNurse> ().intervene ();
				callTimer = -1; 
			}
		}

	}

	// when this window is approached, start a timer before nurse comes
	// later will require you to have visited the friend first 
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player")
			callTimer = 0; 
	}
}
