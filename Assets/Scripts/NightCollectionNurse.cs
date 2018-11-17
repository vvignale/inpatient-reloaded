using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCollectionNurse : Interactable {

	private float startX; 
	private float targetX; 
	private GameObject interactionController; 
	private bool started; 

	// Use this for initialization
	void Start () {
		base.Start ();
		started = false; 
		startX = transform.position.x;
		targetX = -1; 
		automatic = true; 
		text = "Text/NightCollectionNurse1";	
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this); 
		
	}

	public void intervene(){
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent <Collider2D>().enabled = true;
		targetX = player.transform.position.x - 2;
	}
	
	// Update is called once per frame
	void Update () {

		if (targetX != -1) {
			targetX = player.transform.position.x - 2;

			float distance = targetX - transform.position.x; 
			int dir; 
			if (distance < 0)
				dir = -1;
			else
				dir = 1; 

			if(Mathf.Abs(distance) <= 8 && !started){
				started = true; 	
				player.GetComponent <PlayerController>().pause ();
				interactionController.GetComponent<InteractionCollider> ().startInteraction ();
				// stop the music 
				GameObject.Find ("FollowMusic").GetComponent <AudioSource> ().Stop ();
			}
			if (Mathf.Abs (distance) < .01) {
				transform.position = new Vector3 (targetX, transform.position.y, transform.position.z);

				// transition out of room 
				targetX = -1; 

				if(!interactionController.GetComponent <InteractionCollider>().interacting)
					transform.parent.Find("MusicRoom5_Exit").GetComponent <SlowDoor>().transitionRooms ();

			} else {
				transform.Translate (new Vector3 (Time.deltaTime * dir * 4f, 0, 0));
			}
		}
		
	}

	public override void handleInteractionEnd ()
	{
		
		player.GetComponent <PlayerController>().pause ();
		if(targetX == -1){
			transform.parent.Find("MusicRoom5_Exit").GetComponent <SlowDoor>().transitionRooms ();
		}
		
	}
}
