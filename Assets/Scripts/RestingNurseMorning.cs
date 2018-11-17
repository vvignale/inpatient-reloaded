using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestingNurseMorning : Interactable {

	private GameObject interactionController;
	private Vector3 target; 

	// Use this for initialization
	void Start () {
		
		target = Vector3.zero; 

		text = "Text/RestingNurse3";	
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this); 

		base.Start ();
		player.GetComponent<PlayerController>().setSprite (player.GetComponent <PlayerController>().left);

	}
	
	// Update is called once per frame
	void Update () {

		if (target != Vector3.zero) {

			// while not at the target, move towards the target 
			Vector3 dir = target - transform.position; 
			float mag = dir.sqrMagnitude;

			if (mag <= .001) {
				transform.position = target;
				GetComponent <SpriteRenderer>().enabled = false;
				GetComponent <Collider2D> ().enabled = false;
				target= Vector3.zero;
			} else {
				transform.Translate (new Vector3(-1, 0, 0)* Time.deltaTime*2f);
			}
		}
	}

	public override void handleInteractionEnd(){

		// walk away to door and disappear
		target = transform.parent.Find("PlayerRestingArea_Entrance").transform.position; 
		interact = false; 
	}
}
