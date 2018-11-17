using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSurgery2Doctor : Interactable {

	private GameObject interactionController; 
	private bool questionAsked; 
	private string[] questions; 
	private string[] texts; 
	private int textIndex; 
	private int questionIndex; 

	private float cryingTimer;
	private float cryTime; 

	// Use this for initialization
	void Start () {
		base.Start ();
		player.GetComponent <PlayerController>().setReboot (true);

		cryingTimer = 0;
		cryTime = -1f; 

		textIndex = 0; 
		questionIndex = 0; 
		// should add a final word on the matter 
		texts = new string[11]{"Text/PostTrachDoctor1", "Text/PostTrachDoctor2", "Text/PostTrachDoctor3", "Text/PostTrachDoctor4", "Text/PostTrachDoctor5", 
			"Text/PostTrachDoctor6", "Text/PostTrachDoctor7", "Text/PostTrachDoctor8", "Text/PostTrachDoctor9", "Text/PostTrachDoctor10", "Text/PostTrachDoctor11"};

		questions = new string[5]{ "(1) What the hell happened? \n(2) Why is my throat so sore?", "(1) How did I get an infection? \n(2) What is a tracheostomy?", 
			"(1) Are they allowed to do that? \n(2) Does that mean I'm well enough to leave?", "(1) I don't want to stay. \n(2) I want to go home.", 
			"(1) ...my mom could look after me \n(2) Why did you give me the trach. I didn't want it."};

		text = texts[textIndex];
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this); 

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

				// choose where to jump text to based on part of conversation (will either be 1, 2 or maybe 3 ahead)
				if(textIndex == 0 || textIndex == 5 || textIndex == 6){		// some texts lead to only one answer
					textIndex += 1; 
				}
				else if(textIndex == 1 || textIndex == 7){	// jump ahead variable amount depending on question 
					
					if(Input.GetKeyDown (KeyCode.Alpha1)){
						textIndex += 1;
					}
					else if (Input.GetKeyDown (KeyCode.Alpha2)){
						textIndex += 2; 
					}
				}



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

		if(cryTime != -1){
			cryingTimer += Time.deltaTime; 

			if(cryingTimer >= cryTime){
				cryTime = -1; 
				interact = true;
				// start the next interaction 
				interactionController.GetComponent <InteractionCollider> ().startInteraction ();
			}
		}
			

	}


	public override void handleInteractionEnd(){

		// make sure player is paused after each turn since text manager screws this up
		player.GetComponent <PlayerController>().pause ();

		// not every interaction has a question after it. if not, increment the text or whatever and start that 
		if(textIndex == 2 || textIndex == 3){
			textIndex = 4;
			interactionController.GetComponent<InteractionCollider> ().updateText (texts [textIndex]);
			interactionController.GetComponent <InteractionCollider> ().startInteraction ();
		}
		else if(textIndex == 4){
			// pause for a second while the player cries. will start next piece shortly 
			textIndex += 1;
			interact = false;
			cryTime = 3f; 
			interactionController.GetComponent<InteractionCollider> ().updateText (texts [textIndex]);
		}
		else if(textIndex == 8 || textIndex == 9){
			textIndex = 10; 
			interactionController.GetComponent<InteractionCollider> ().updateText (texts [textIndex]);
			interactionController.GetComponent <InteractionCollider> ().startInteraction ();
		}
		else if(textIndex == 10){
			//transition out after finishing the full conversation 
			interact = false; 
			transform.parent.Find("Exit").GetComponent <SlowDoor>().transitionRooms (); 
		}
		// otherwise, increment for the next question 
		else if(questionIndex < questions.Length){
			interact = false;
			questionAsked = true;
		}


	}


}
