using UnityEngine;
using System.Collections;

public class DoctorController : Interactable {

	private GameObject interactionController; 

	// Use this for initialization
	void Start () {
		text = "Text/Doctor";	//starter text 

		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this);
		base.Start ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void handleInteractionEnd(){
		// have the nurse walk in at this point (if phase 1)
		if (text == "Text/Doctor") {
			transform.parent.Find ("Nurse").GetComponent <ConsultationNurseController> ().startWalk ();
			text = "Text/Doctor1";
			interactionController.GetComponent<InteractionCollider> ().doSetup (6, 6, text, this);	
		}
		else{
			transform.parent.Find ("Nurse").GetComponent <ConsultationNurseController> ().leavingWalk ();
		}
	}

}
