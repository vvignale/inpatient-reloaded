using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend1Controller : Interactable {

	private GameObject interactionController; 
	private AudioSource[] sounds; 

	private bool startedPlaying = false;
	private bool pt3 = false; 

	// Use this for initialization
	void Start () {
		base.Start ();
		text = "Text/Friend1";	 
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this); 
		sounds = GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.Return) && interactionController.GetComponent <InteractionCollider>().isInRange () && !pt3){
			sounds [0].Stop ();
		}

		if(startedPlaying && !sounds[1].isPlaying && !pt3){
			// start the next interaction and when that one ends, will set to simply we should play some time
			interact = true;  
			interactionController.GetComponent <InteractionCollider>().startInteraction ();
			pt3 = true; 	// I hate this
		}
		
	}

	public override void handleInteractionEnd(){

		if (!startedPlaying) {
			// play the second sound and then reset the thing
			sounds [1].Play ();
			interact = false; 
			startedPlaying = true; 
			interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, "Text/Friend1b", this); 
		}

		else {
			// have already played second clip
			// change the message to be simple and start the music again 
			interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, "Text/Friend1c", this); 
			sounds [0].Play ();
		}

	}
}
