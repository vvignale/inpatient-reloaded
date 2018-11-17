using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	protected GameController gameMaster; 
	protected PlayerController player; 
	protected GameObject playerObj; 
	protected Vector3 startingCoords; 

	protected TextBoxManager textManager; 

	// Use this for initialization
	protected virtual void Start () {
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		player = playerObj.GetComponent <PlayerController> ();
		gameMaster = FindObjectOfType<GameController> (); 
		textManager = FindObjectOfType<TextBoxManager> ();		// gets the script
	}

	// return the cam location as given in game master
	public Vector3 getCamLoc(){
		return gameMaster.getCamLoc (); 
	}
	
	// Update is called once per frame
	void Update () {		
	}

	public void transitionRooms(GameObject prev, GameObject portalEnd){ 
    		playerObj = GameObject.FindGameObjectWithTag ("Player");
		gameMaster = FindObjectOfType<GameController> (); 

		playerObj.transform.parent = null; 	// make sure player is attached to nothing before moving 
		gameMaster.transitionRooms (prev, portalEnd);
	}
		
}
