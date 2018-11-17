using UnityEngine;
using System.Collections;

public class MainLobbyController : Room{
	private bool formActive; 

	// get references in awake. turn off in start

	void Awake(){  
		formActive = false;
	}

	// Use this for initialization
	void Start () {
		base.Start ();
		// start off the game's ambient noise (when does this change?) after surgery 1?
		// and then change again after the trach

		GameObject.FindGameObjectWithTag ("AmbientSound").GetComponent <AudioSource>().Play ();
	}
	
	// Update is called once per frame
	void Update () {

		if (formActive) {
			if (Input.anyKeyDown) {	
				// janky way to do this to avoid mouse input
				if (!(Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1) || Input.GetMouseButtonDown (2) || Input.GetKeyDown(KeyCode.Return))) {

					// tell the form text controller to update its text
					GameObject.FindObjectOfType<FormTextController>().updateText(this); 
				} 
			}
		}
	}

	public bool getFormActive(){
		return formActive;
	}

	public void displayForm(){

		// enable the inpatient form 
		GameObject form = gameObject.transform.Find ("InpatientForm").gameObject; 
		// get camera loc and move form to that place. text will move with it
		form.SetActiveRecursively(true); 
		Vector3 camLoc = getCamLoc (); 
		form.transform.position = new Vector3 (camLoc.x, camLoc.y, form.transform.position.z); 

		formActive = true; 
		player.pause ();
		playerObj.SetActive (false); 
	}

	public void deactivateForm(){
		gameObject.transform.Find ("InpatientForm").gameObject.SetActive (false); 	
		formActive = false;
		player.unpause (); 
		// reactivate stuff in the room 
		playerObj.SetActive (true); 
		//transform.Find ("LobbyDoorToHall").GetComponent<DoorController> ().setLock (); 

		// start second part of the receptionist interaction 
		//transform
	}


}
