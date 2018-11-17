using UnityEngine;
using System.Collections;

public class OperatorController : Interactable {

	private GameObject interactionController;
	private bool questionAsked; 
	private string[] questions; 
	private string[] texts; 
	private int textIndex; 
	private float waitTime = 20f; 

	// Use this for initialization
	void Start () {

		textIndex = 0; 
		// should add a final word on the matter 
		texts = new string[1]{"Text/InsuranceOperator"};
		questions = new string[1]{""};

		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, texts[textIndex], this);

		automatic = true;

		questionAsked = false; 
	}


	//heello this is a bug :)
	// love, sonia

	void OnGUI(){
		if(questionAsked){
			// display the current question
			GUI.Button (new Rect (player.transform.position.x, player.transform.position.y + 200, 400, 50), questions[textIndex]);
		}
	}

	public void resetIndex(){
		textIndex = 0; 
	}

	public void startInteraction(){
		interactionController.GetComponent<InteractionCollider>().startInteraction ();
	}

	// Update is called once per frame
	void Update () {

		// when player input received on a question asked, load the appropriate script to proceed
		if(questionAsked){
			if(Input.GetKeyDown (KeyCode.Alpha1)){

				// might need to change where we jump based on the question index (for branching convos)
				interact = true; 
				questionAsked = false; 

			}

			else if(Input.GetKeyDown (KeyCode.Alpha2)){

				interact = true; 
				questionAsked = false; 

			}
		}

		// enable the return key 

		// unmark flag for a question asked 


	}

	public override void handleInteractionEnd(){

		if(textIndex<texts.Length){
			// start some hold music between interactions 
			transform.parent.Find("PhoneBooth").GetComponent <PhoneBoothController>().startNewCall (GetComponent <AudioSource>(), waitTime);
			interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, texts[textIndex], this);
		}
		textIndex += 1;
		interact = false;
	}

}
