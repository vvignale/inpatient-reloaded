using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftShopCashier : Interactable {

	private GameObject interactionController; 
	private bool questionAsked;
	private string options; 
	private string[] texts; 
	private int textIndex;

	void Start () {

		textIndex = 0; 
		texts = new string[3]{ "Text/GiftShopCashier1", "Text/GiftShopCashier2", "Text/GiftShopCashier3"}; 

		options = "(1) Balloon -- $3\n No thanks";

		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, texts[textIndex], this); 
		base.Start ();
	}

	void OnGUI () {

		// display questions to ask if button pressed 
		if(questionAsked){

			Vector2 size = new Vector2 (150, 100);
			Vector2 screenPos = base.placeBox (player.transform.position + new Vector3(2, 0, 0), size);	

			GUI.Box (new Rect (screenPos.x, screenPos.y, size.x, size.y), options);
		}
	}

	void Update(){

		if (questionAsked){

			if(Input.GetKeyDown (KeyCode.Alpha1)){
				// check if can buy balloon 
				if(!(player.GetComponent <PlayerController>().hasInventory ("balloon"))){

					player.GetComponent <PlayerController>().addInventory ("balloon", 1);
					Vector3 playerHand = new Vector3 (.5f, 0, 0);
					GameObject balloon = Instantiate (Resources.Load ("Prefabs/Balloon"), player.transform.position + playerHand, player.transform.rotation) as GameObject;
					// set the player as the rigid body component 
					balloon.transform.Find ("StringBase").GetComponent <FixedJoint2D>().connectedBody = player.GetComponent <Rigidbody2D>();
					balloon.transform.parent = player.transform; 

					// display a thank you message 
					textIndex = 2;
					interactionController.GetComponent<InteractionCollider>().updateText (texts[textIndex]); 
					interactionController.GetComponent <InteractionCollider>().startInteraction ();
				}
				else{
					// display message that you can't buy another at this time 
					textIndex = 1; 
					interactionController.GetComponent<InteractionCollider>().updateText (texts[textIndex]); 
					interactionController.GetComponent <InteractionCollider>().startInteraction ();
				}
				questionAsked = false; 
			}

			else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				questionAsked = false; 
			}


		}

	}
		

	public override void handleInteractionEnd(){

		// display question for what to buy 
		if(textIndex == 0)
			questionAsked = true; 

	}
}
