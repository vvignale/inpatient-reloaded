using UnityEngine;
using System.Collections;

public class PostDoctorController : Interactable {

	private GameObject interactionController;
	private bool questionAsked; 
	private string[] questions; 
	private string[] texts; 
	private int textIndex; 
	private Camera camera;

	private bool convoStarted = false;

	// Use this for initialization
	void Start () {

		base.Start ();
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <Camera>();
		textIndex = 0;

		automatic = false;
		// should add a final word on the matter 
		texts = new string[4]{"Text/PostDoctor", "Text/PostDoctor2", "Text/PostDoctor3", "Text/PostDoctor4"};
		questions = new string[3]{"", "(1) Terrible. Where is the g-tube? \n(2) Okay, but how did the surgery go?", "(1) Wait, what do you mean, inpatient? \n(2) I'm not staying for another surgery!"};

		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, texts[textIndex], this);

		questionAsked = false; 
	}


	//heello this is a bug :)
	// love, sonia

	void OnGUI(){
		if(questionAsked){
			// display the current question
			Vector3 screenPos = camera.WorldToScreenPoint (player.transform.position+ new Vector3(0,1,0));
			GUIStyle style = new GUIStyle ("button");
			style.fontSize = 16; 
			GUI.Box (new Rect (screenPos.x-100, screenPos.y+200, 300, 50), questions[textIndex], style);

		}

	}


	public override void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && !convoStarted){
			interactionController.GetComponent <InteractionCollider>().startInteraction ();
			convoStarted = true;
		}

		base.OnTriggerEnter2D (other);
	}

	// Update is called once per frame
	void Update () {

		// when player input received on a question asked, load the appropriate script to proceed
		if(questionAsked){
			if(Input.GetKeyDown (KeyCode.Alpha1)){

				// might need to change where we jump based on the question index (for branching convos)
				interact = true; 
				questionAsked = false; 
				interactionController.GetComponent <InteractionCollider>().startInteraction ();

			}

			else if(Input.GetKeyDown (KeyCode.Alpha2)){

				interact = true; 
				questionAsked = false; 
				interactionController.GetComponent <InteractionCollider>().startInteraction ();

			}
		}

		// enable the return key 

		// unmark flag for a question asked 


	}
		
	public override void handleInteractionEnd(){
		
		textIndex += 1;

		if(textIndex<texts.Length){
			interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, texts[textIndex], this);
			 
			if(textIndex<questions.Length){
				interact = false;
				questionAsked = true;
			}

		}
			

		// display a question prompt with 2 options (changing depending on point of conversation)
		// disable return key for interactible
		// mark flag for a question asked 


	}
		
}
