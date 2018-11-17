using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
 
	private GameObject currRoom = null; 
	private GameObject oldRoom = null; 
	private GameObject endingDoor = null;
	private bool paused; 
	private Fader fader; 
	private GameObject camera; 
	private GameObject player; 
	private PlayerController playerScript; 
	private float overrideWaitTime = -1f; 

	public void setFadeSpeed(float speed, float wait){
		fader.GetComponent <Fader>().setFadeSpeed (speed);
		overrideWaitTime = wait; 
	}
		
	void Awake(){
		camera = GameObject.FindGameObjectWithTag ("MainCamera"); 
		player = GameObject.FindGameObjectWithTag ("Player"); 
		fader = FindObjectOfType<Fader> (); 
		playerScript = player.GetComponent <PlayerController> ();
	}

	// Use this for initialization
	void Start () {
		paused = true; 
	}

	public void setPause(bool p){
		paused = p; 
	}

	public bool getPause(){
		return paused; 
	}

	public Vector3 getCamLoc(){
		return camera.transform.position; 
	}

	public void movePlayerToRoom(){

		if (endingDoor != null) {
			DoorController door = endingDoor.GetComponent<DoorController> (); 
			// move player and camera will update to lock 
			player.transform.position = new Vector3(endingDoor.transform.position.x, endingDoor.transform.position.y, player.transform.position.z) + (door.getDirection () * 2f);
		}	 
	}

	public void loadRooms(){

		// this activates the correct room and will call to move the camera as necessary

		// Make sure that camera mask is open when enter a room (even if room immediately turns it off)
		// same for player and inventory being active and visible  
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <ScreenTransitionImageEffect>().maskValue = 0; 
		GameObject.FindGameObjectWithTag ("Inventory").GetComponent <Text> ().enabled = true; 
		playerScript.pause ();

		if (oldRoom != null) {		// regular room transition 
			oldRoom.SetActive (false);
		} else {	// only happens on the main menu
			GameObject.FindGameObjectWithTag("Menu").SetActive(false);
			// move player to start
			player.transform.position = new Vector3(0,0,-.5f); 	
			setPause (false); 	// game is ready to start with player
			player.GetComponent<PlayerController> ().initializeInventory ();
			player.GetComponent<PlayerController>().unpause(); 
		}

		currRoom.SetActive (true);	// moved to after if/else so that resetting works

		camera.GetComponent<CameraFollow>().setRoomBounds(currRoom);	// update room bounds for camera
		movePlayerToRoom (); 													// teleport to new room 

		player.GetComponent <SpriteRenderer>().enabled = true; 
		playerScript.unpause ();
		player.GetComponent <Rigidbody2D>().WakeUp ();
	}

	public void transitionMainMenu(GameObject next){
		//setFadeSpeed (.2f, 10f);
		oldRoom = null; 
		currRoom = next; 
		endingDoor = null;		// this object will store the coordinates that we want to move to for player and cam
		StartCoroutine (fade ());
	}

	public void transitionRooms(GameObject prev, GameObject portalEnd){

		// this sets up our class vars and initiates the fade (which will initiate the loading of the rooms)
		// pause the player while the transition is going 

		// CHECK IF PLAYER ALREADY FADING. ONLY ALLOW TRANSITION BETWEEN THAT 


		playerScript.pause ();
		player.GetComponent <Rigidbody2D>().Sleep ();
		oldRoom = prev; 
		currRoom = portalEnd.GetComponent<DoorController>().getContainer();
		endingDoor = portalEnd;		// this object will store the coordinates that we want to move to for player and cam
		StartCoroutine (fade ()); 
	}

	private IEnumerator fade(){
		// instruct the fade to begin 
		float fadeTime = fader.beginFadeTransition(); 

		if (overrideWaitTime == -1)
			overrideWaitTime = fadeTime; 
 
		yield return new WaitForSeconds (overrideWaitTime);	// removed double of fading in 
		// turn off transitioning flag after done. this cleans up so we can use the fader for other stuff 
		fader.toggleTransitioning (false); 
	}

}
