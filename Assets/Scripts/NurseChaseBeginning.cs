using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseChaseBeginning : NurseChase {

	// first iteration of the nurse chase. basically just also has a script. otherwise the same 

	private GameObject interactionController;
	private string[] texts; 
	private int textIndex;

	void Start(){
		base.Start ();
		automatic = true; 
		GameObject.Find ("EscapeMusic").GetComponent <AudioSource> ().Play ();
	}

	void OnEnable () {
		textIndex = 0; 
		texts = new string[1]{"Text/ChaseNurse1"};

		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(1, 1, texts[textIndex], this);

		automatic = true; 

		base.OnEnable ();
	}

	public void startInteraction(){
		if(textIndex<texts.Length)
			interactionController.GetComponent<InteractionCollider>().startInteraction ();
	}

	public override void startChase(){
		startInteraction (); 
		base.startChase ();
	}

	public override void handleInteractionEnd(){
		// start navigation towards player 
		textIndex += 1; 

	}

}
