using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDoor : DoorController {

	public float fadeSpeed; 
	public float fadeTime; 

	void OnTriggerEnter2D(Collider2D other){
		// adjust fade time 
		if(other.tag == "Player"){
			GameObject.FindGameObjectWithTag ("GameMaster").GetComponent <GameController>().setFadeSpeed (fadeSpeed, fadeTime);
			base.OnTriggerEnter2D (other);
		}

	}

	public override void transitionRooms(){
		GameObject.FindGameObjectWithTag ("GameMaster").GetComponent <GameController>().setFadeSpeed (fadeSpeed, fadeTime);
		base.transitionRooms ();
	}
}
