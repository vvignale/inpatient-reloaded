using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseChase : Interactable {
 
	public GameObject startingRoomDoor; 
	private Vector3 startingPos; 
	private bool startSet = false; 

	protected bool chasing; 
	private float speed = 3f; 

	public Vector3[] targets; 
	protected int targetIndex; 

	void Start(){
		startingPos = transform.position; 
		base.Start ();  
		automatic = true;

	}

	// Use this for initialization
	protected void OnEnable () {
		chasing = false; 

		// disable viewing and collisions on nurse until chase starts 
		gameObject.GetComponent <SpriteRenderer>().enabled = false;
		gameObject.GetComponent <Collider2D>().enabled = false;


		if(!startSet){
			startSet = true; 
			startingPos = gameObject.transform.position;
		}
		gameObject.transform.position = startingPos; 

		targetIndex = 0; 

		// loop through targets and add the parent's transform to it to get correct translation :(
//		for(int i=0; i<targets.Length; i++){
//			targets [i] += transform.parent.position; 
//		}
	}

	public virtual void startChase(){
		chasing = true; 
		gameObject.GetComponent <SpriteRenderer>().enabled = true;
		gameObject.GetComponent <Collider2D>().enabled = true;
	}

	// helper to cut ahead if the proper signal gotten from the player
	public virtual void jumpIndex(){}
		
	public void endChase(){
		chasing = false; 
	}
	
	// Update is called once per frame
	void Update () {

		// move towards the player 

		if (chasing && targetIndex<targets.Length) {

			// move towards the current target. what this is varies according to logic elsewhere
			// either catch player, get a message to move somewhere, or continue as hit the target
			Vector3 dir = ((targets[targetIndex]+transform.parent.position) - gameObject.transform.position);
			if(dir.sqrMagnitude < .01){
				// if close enough to target, snap there and look to the next if possible (else stop)
				gameObject.transform.position = targets [targetIndex]+transform.parent.position;
				targetIndex += 1;
			}
			dir = dir.normalized;
			transform.Translate (dir * Time.deltaTime * speed);

		}

	}


	void OnTriggerEnter2D(Collider2D other){
		// if catch the player while chasing, transition back to the first room 
		if (other.tag == "Player" && chasing) {
			chasing = false;
			// transition from whatever room we are currently in to the starting room door 
			GameObject.FindObjectOfType<Room> ().transitionRooms (gameObject.transform.parent.gameObject, startingRoomDoor);
		}
	}
		

}
