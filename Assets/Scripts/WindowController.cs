using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour {

	private GameObject player; 
	private bool questionAsked = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(questionAsked){
			// if opt in, play the sound and cue the transition 
			if(Input.GetKeyDown (KeyCode.Return)){
				questionAsked = false; 
				GetComponent <AudioSource>().Play ();

				// disable the nurse's collider
				transform.parent.Find ("Nurse").GetComponent <NurseChase>().endChase ();
				transform.parent.Find ("Nurse").GetComponent <Collider2D>().enabled = false; 

				// fade out 
				GetComponent <SlowDoor>().transitionRooms (); 
			}
		}
	}

	void OnGUI(){
		if(questionAsked){
			// display the current question

			Vector3 playerScreenPos = Camera.main.WorldToScreenPoint (player.transform.position + new Vector3(0,1,0));
			GUI.Button (new Rect (playerScreenPos.x, playerScreenPos.y, 100, 50), "Break? <Enter>");

		}
	}

	// if trigger entered, pose a question to jump 

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			questionAsked = true; 
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			questionAsked = false; 
		}
	}

}
