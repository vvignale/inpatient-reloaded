using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurgeonController : Interactable {

	private GameObject interactionController;
	private float timer;
	private float waitTime; 
	private bool interactionStarted;

	private float fullTimer = 20f; 

	// Use this for initialization
	void Start () {
		waitTime = 5f; 
		timer = 0f; 
		interactionStarted = false;
		automatic = true; 
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider> ().doSetup (6, 6, "Text/Surgeon1", this, 2f);

		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= waitTime && !interactionStarted) {
			interactionController.GetComponent <InteractionCollider> ().startInteraction ();
			interactionStarted = true;
			timer = 0;
		}
			
		else if(timer >=fullTimer){
			// start transition to the next room
			transform.parent.transform.Find("WardExitDoor").GetComponent <SlowDoor>().transitionRooms ();
		}
		
	}
}
