using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentNurseController : Interactable {

	PermanentRoomController room; 
	private GameObject interactionController;
	private Vector3 target; 
	private bool questionAsked;
	private string options; 
	private string[] texts; 
	private int textIndex;
	private Vector3 startingPos = Vector3.zero; 

	private bool inBed; 
	private bool medicationGiven; 

	void OnEnable(){
		textIndex = 0; 
		texts = new string[4]{ "Text/PermanentNurse1", "Text/PermanentNurse2", "Text/PermanentNurse3", "Text/PermanentNurse4" }; 
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform);
		interactionController.GetComponent<InteractionCollider>().doSetup(1, 1, texts[textIndex], this);

		target = Vector3.zero; 
		questionAsked = false;
		if(startingPos == Vector3.zero)
			startingPos = transform.position;
		transform.position = startingPos;

		room = transform.parent.gameObject.GetComponent <PermanentRoomController>();
		inBed = room.getBedState ();
		if(inBed){
			options = "(1)Turn\n(2)Get in Chair\n(3)Medication\n(4)TV\n(5)What will happen to me?\n(6)Nothing";
		}

		else{
			options = "(1) Get in Bed";
		}
			
	}

	// Use this for initialization
	void Start () {
		base.Start ();
		startingPos = transform.position;

		medicationGiven = false; 
		OnEnable ();
	}
		
	void OnGUI () {

		// display questions to ask if button pressed 
		if(questionAsked){

			Vector2 size = new Vector2 (150, 100);
			Vector2 screenPos = base.placeBox (player.transform.position + new Vector3(2, 0, 0), size);	

			GUI.Box (new Rect (screenPos.x, screenPos.y, size.x, size.y), options);
		}
	}


	
	// Update is called once per frame
	void Update () {


		if(target != Vector3.zero){
			// while not at the target, move towards the target 
			Vector3 dir = target - transform.position; 
			float mag = dir.sqrMagnitude;

			if (mag <= .001) {
				transform.position = target;

				// do something when get to target 
				// in first instance, pose the question

				interactionController.GetComponent <InteractionCollider> ().startInteraction ();
				target = Vector3.zero;

			} else {
				transform.Translate (dir.normalized * Time.deltaTime * 2f);
			}
		}


		// handle answers to questions if posed
		if(questionAsked){
			if (inBed) {
				if(Input.GetKeyDown (KeyCode.Alpha1)){
					// turn player 
					room.turnPlayer ();
				}
				else if (Input.GetKeyDown (KeyCode.Alpha2)) {

					// put into the wheelchair  or bed as needed 
						// TODO add condition to refuse
					room.movePlayer ();
					questionAsked = false;
				}
				else if(Input.GetKeyDown (KeyCode.Alpha3)){

					if(!medicationGiven){
						textIndex = 1; 
						medicationGiven = true; 
					}
					else{
						textIndex = 2;
					}
					interactionController.GetComponent<InteractionCollider>().doSetup(1, 1, texts[textIndex], this);
					interactionController.GetComponent<InteractionCollider> ().startInteraction ();
					
				}else if (Input.GetKeyDown (KeyCode.Alpha4)) {
					// change the tv (first have an interaction)
					room.toggleTV (!room.getTVOn ());
					questionAsked = false;
				}
				else if(Input.GetKeyDown (KeyCode.Alpha5)){
					textIndex = 3;
					interactionController.GetComponent<InteractionCollider>().doSetup(1, 1, texts[textIndex], this);
					interactionController.GetComponent<InteractionCollider> ().startInteraction ();
				}

				else if(Input.GetKeyDown (KeyCode.Alpha6)){
					room.resetRoom (); 
				}
			}

			else{
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					room.movePlayer ();
					questionAsked = false;
				}
			}
				
		}

	}

	public override void OnTriggerEnter2D(Collider2D other){}

	public override void OnTriggerExit2D(Collider2D other){}


	public void greetPatient(){

		gameObject.GetComponent <SpriteRenderer> ().enabled = true;
		target = new Vector3(player.transform.position.x +3, transform.position.y, transform.position.z);
	}


	public override void handleInteractionEnd(){

		// make sure player is still paused
		player.GetComponent <PlayerController>().pause ();

		if(textIndex == 1 || textIndex == 2 || textIndex == 3){
			if(textIndex == 1)
				room.resetPain ();
			room.resetRoom (); 
		}
		else if(textIndex == 0){
			questionAsked = true; 
		}



	}
}
