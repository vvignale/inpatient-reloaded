using UnityEngine;
using System.Collections;

public class InteractionCollider : MonoBehaviour {

	private TextBoxManager textManager;
	private Interactable caller; 
	private string textPath; 
	private bool inRange = false;
	private bool wait = false; 
	public bool interacting = false; 

//	private GameObject interactionTriangle; 

	// Use this for initialization
	public void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		// only do this if the non automated type of collider (otherwise will call start interaction directly)
		if (!caller.getAutomatic ()) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				if (inRange && !interacting && !wait && caller.canInteract ()) {
					startInteraction ();
				}
				wait = false; 
			}
		}
			
	}

	public void startInteraction(){
		interacting = true;  
		textManager.enableBox (Resources.Load(textPath) as TextAsset, this);
		
	}

	public void doSetup(float sizeX, float sizeY, string text, Interactable call, float interval=0){
		textManager = FindObjectOfType<TextBoxManager> ();
		textPath = text; 
		gameObject.GetComponent<BoxCollider2D>().size = new Vector3 (sizeX, sizeY, 1);
		caller = call; 	// stores the calling object/script so that it can do some behavior

		textManager.setInterval (interval);
	}

	public void updateText(string newText){
		textPath = newText; 
	}

	public void enableInteraction(){
		interacting = false; 
		wait = true;		
		caller.handleInteractionEnd(); 		// callback to the parent 
	}

	public bool isInRange(){
		return inRange; 
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			inRange = true;
		}
		caller.OnTriggerEnter2D (other);
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			inRange = false; 
		}
		caller.OnTriggerExit2D (other);
	}
		
}
