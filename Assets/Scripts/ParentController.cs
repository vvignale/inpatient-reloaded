using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : Interactable {

	public GameObject interactionController; 

	// Use this for initialization
	void Start () {

		base.Start ();

		text = "Text/Parent1";	
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this);

	}

	public override void handleInteractionEnd(){
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, "Text/Parent2", this);
	}
}
