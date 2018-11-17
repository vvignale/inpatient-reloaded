using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSurgery2Doctors : Interactable {

	private GameObject interactionController;
	private float timer; 
	private float waitTime = 2f; 
	private bool interactionStarted = false; 
	private bool secondPhase = false; 
	private bool thirdPhase = false; 


	private float postTimer = 5f; 
	private float ceilingTimer = 7f; 
		
	public Sprite ceiling; 


	// Use this for initialization
	void Start () {
		text = "Text/OverheardDoctor1";	
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this);

		GameObject.FindGameObjectWithTag ("Player").GetComponent <SpriteRenderer>().enabled = false; 

		automatic = true; 
		timer = 0; 
		base.Start ();

	}
	
	// Update is called once per frame
	void Update () {

		// pause for a moment and then start the script
		timer += Time.deltaTime; 

		if(timer >= waitTime && !interactionStarted){
			interactionController.GetComponent <InteractionCollider>().startInteraction (); 
			interactionStarted = true; 
		}

		if(timer >= postTimer && secondPhase){
			secondPhase = false; 
			// change room sprite and play a sound. time that and then transition to the actual room 
			transform.parent.Find("Plate").GetComponent <SpriteRenderer>().sprite = ceiling;
			// turn on all the lights
			transform.parent.Find ("light1").GetComponent <Light> ().enabled = true;
			transform.parent.Find ("light2").GetComponent <Light> ().enabled = true;
			transform.parent.Find ("light3").GetComponent <Light> ().enabled = true;

			gameObject.GetComponent <AudioSource>().Play ();
			timer = 0; 
			thirdPhase = true;
		}

		if(timer >= ceilingTimer && thirdPhase){
			// transition to the post surgery room 
			transform.parent.Find ("PostSurgeryIRoom_Entrance").GetComponent <SlowDoor>().transitionRooms ();
		}
	}


	public override void handleInteractionEnd(){
		timer = 0; 
		secondPhase = true; 
		interact = false; 
	}
}
