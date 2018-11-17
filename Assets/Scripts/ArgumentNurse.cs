using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgumentNurse : Interactable {

	private GameObject interactionController; 
	private bool questionAsked; 
	private string[] questions; 
	private string[] texts; 
	private int textIndex; 
	private int questionIndex; 


	private float cryTimer;
	private float cryingTime; 

	// Use this for initialization
	void Start () {

		base.Start ();

		// restore transition
		GameObject.FindGameObjectWithTag ("GameMaster").GetComponent <GameController>().setFadeSpeed (.8f, 2f);

		textIndex = 0; 
		questionIndex = 0; 
		// should add a final word on the matter 
		texts = new string[11]{"Text/ArgumentNurse1", "Text/ArgumentNurse2", "Text/ArgumentNurse3", "Text/ArgumentNurse4", "Text/ArgumentNurse5", 
			"Text/ArgumentNurse6", "Text/ArgumentNurse7", "Text/ArgumentNurse8", "Text/ArgumentNurse9", "Text/ArgumentNurse10", "Text/ArgumentNurse11"};

		questions = new string[9]{ "(1) I thought I heard something. \n(2) I just wanted to get out of this room.", "(1) What is it? \n(2) I can go home?", 
			"(1) I suppose so. \n(2) I don't want the surgery.", "(1) What if it fails?\n(2) I'm not going to get better, am I?", "(1) Forget it; I don't want to talk about this.\n (2) I want to go home.",
			"(1) Pasta \n(2) Fuck you.", "(1) Call her. \n(2) I want to go home", "(1) I don't want morphine. \n(2) I'm going crazy in here.", 
			"(1) I'm not getting in bed.\n (2) I want to go outside."};

		text = texts[textIndex];
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this); 

		cryingTime = 5f;
		cryTimer = -1f; 
		
	}

	void OnGUI(){
		if(questionAsked){
			// display the current question
			Vector3 screenPos = Camera.main.WorldToScreenPoint (player.transform.position);
			GUIStyle style = new GUIStyle ("button");
			style.fontSize = 16; 
			GUI.Box (new Rect (screenPos.x-100, screenPos.y+200, 400, 50), questions[questionIndex], style);

		}

	}
	
	// Update is called once per frame
	void Update () {

		if (questionAsked) {
			if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.Alpha2)) {
				textIndex += 1; 

				// all the stuff we have to do every time 
				interact = true; 
				questionAsked = false; 
				interactionController.GetComponent<InteractionCollider> ().updateText (texts [textIndex]);
				if(textIndex < texts.Length){
					interactionController.GetComponent <InteractionCollider> ().startInteraction ();
				}
				questionIndex += 1; 

			}
		}
		else if(cryTimer != -1){
			cryTimer += Time.deltaTime; 
			if(cryTimer >= cryingTime){
				textIndex += 1; 
				interactionController.GetComponent<InteractionCollider> ().updateText (texts [textIndex]);
				interactionController.GetComponent <InteractionCollider> ().startInteraction ();
				cryTimer = -1; 
				interact = true; 
			}
		}
		
	}


	public override void handleInteractionEnd(){

		// make sure player is paused after each turn since text manager screws this up
		player.GetComponent <PlayerController>().pause ();


		if(textIndex == 7){
			// start a wait period and then launch 8
			cryTimer = 0; 
			interact = false; 
		}

		else if(textIndex == texts.Length-1){
			interact = false; 
			transform.parent.Find ("Exit").GetComponent <DoorController> ().transitionRooms ();
		}

		// otherwise, increment for the next question 
		else if(questionIndex < questions.Length){
			interact = false;
			questionAsked = true;
		}


	}
}
