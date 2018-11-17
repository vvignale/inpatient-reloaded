using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HankerchiefController : Interactable {

	private GameObject interactionController; 
	private bool questionAsked; 

	// Use this for initialization
	void Start () {

		base.Start ();
		text = "Text/Handkerchief";
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
			GUI.Box (new Rect (screenPos.x-100, screenPos.y+200, 400, 50), "Take? (y/n)", style);

		}

	}
	
	// Update is called once per frame
	void Update () {

		if(questionAsked){

			if(Input.GetKeyDown(KeyCode.Y)){
				GetComponent<SpriteRenderer>().enabled = false;
				GetComponent <Collider2D>().enabled = false;

				//player.GetComponent <PlayerController>().addInventory ("Handkerchief", 1);
				questionAsked = false; 
				interactionTriangle.SetActive (false);
				player.GetComponent <PlayerController>().unpause ();

			}

			else if(Input.GetKeyDown (KeyCode.N)){
				questionAsked = false;
				interact = true; 
				player.GetComponent <PlayerController>().unpause ();

			}
		}
	}

	public override void handleInteractionEnd(){
		questionAsked = true; 
		interact = false; 
		player.GetComponent <PlayerController>().pause ();

	}
		
}
