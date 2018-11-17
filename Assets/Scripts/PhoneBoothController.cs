using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBoothController : MonoBehaviour {

	private GameObject player; 
	private bool playerPresent; 
	private float maxHoldTime;	
	private float callTime = 50f; 	// max time in seconds that player has to wait on hold (whole clip is 10 min) 
	private float holdTimer; 
	private AudioSource audio;

	private bool entering; 
	private bool exiting; 
	private bool onCall;

	private Camera camera; 

	// Use this for initialization
	void Start () {
		playerPresent = false;
		holdTimer = 0f; 

		audio = GetComponent<AudioSource>();

		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <Camera>();

		onCall = false; 
		player = GameObject.FindGameObjectWithTag ("Player"); 
	}

	void OnGUI(){

		if (entering && !onCall) {

			//print ("player pos in GUI: " + player.transform.position);

			Vector3 screenPos = camera.WorldToScreenPoint (player.transform.position);
			GUI.Box (new Rect (screenPos.x-100, screenPos.y, 200, 50), "Start Call? <Enter>");

		}

		if(exiting){
			GUI.Button (new Rect (player.transform.position.x, player.transform.position.y+200, 100, 20), "Leave Call? y/n");

		}
		
	}

	public void startNewCall(AudioSource newAudio, float length){

		audio = newAudio; 
		onCall = true; 
		entering = false; 
		holdTimer = 0;
		maxHoldTime = length; 
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.Return)) {
			if (entering) {
				startNewCall (GetComponent <AudioSource>(), callTime);
			}
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			if(exiting){
				onCall = false; 
				exiting = false; 
				player.GetComponent<PlayerController> ().unpause ();
				audio.Stop ();
				holdTimer = 0; 
				transform.parent.Find ("Operator").GetComponent <OperatorController> ().resetIndex (); 
			}
		}
		if(Input.GetKeyDown(KeyCode.N)){
			if(exiting){
				player.GetComponent<PlayerController> ().unpause ();
				exiting = false; 
			}
		}

		// TODO if player is on the phone, continue running the phone sequence:
		if(onCall){
			
			// while a certain amount of time hasn't passed, keep ringing and hold music
			if(holdTimer < maxHoldTime){	// could also maybe just get/make a 10 min audio clip....

				// if haven't started the music start it
				if(!audio.isPlaying){
					audio.Play();
				}

				holdTimer += Time.deltaTime;
			}

		// after certain time has passed, start phase two of text boxing
			else{
				audio.Stop ();
				holdTimer = 0; 
				onCall = false; 
				// trigger to begin a text animation for the operator

				// find the operator and have it start its thing
				transform.parent.Find("Operator").GetComponent <OperatorController>().startInteraction ();


				// good place to start with testing anim based text 

				// possibly need a new type of interactible. one that can trigger response 
				
			}


		// after that ends, offer options for how to deal 
		}
		
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){

			if (!onCall) {
				entering = true;
			}
				
		}	
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){

			if (!entering && onCall) {
				exiting = true; 
				player.GetComponent<PlayerController>().pause(); 
			}
			else{
				entering = false; 
			}
				

		}
		
	}
}
