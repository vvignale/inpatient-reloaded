using UnityEngine;
using System.Collections;

public class ReceptionistController : Interactable {

	private GameObject interactionController; 
	private bool gotForm;

	void Start () {

		text = "Text/Receptionist_MainLobby/BeforeForm";	//starter text 
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this); 
		gotForm = false;
		base.Start ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public override bool canInteract(){
		// whether or not this particular interactable can interact depends on if the form is active 
		return !(transform.parent.GetComponent<MainLobbyController> ().getFormActive()); 
	}
		
	public override void handleInteractionEnd(){
		if (!gotForm) {
			transform.parent.GetComponent<MainLobbyController> ().displayForm ();
			// change the text to indicate that it's over 
			text = "Text/Receptionist_MainLobby/AfterForm";
			interactionController.GetComponent<InteractionCollider>().updateText(text);
			gotForm = true; 
		}

	}


}
