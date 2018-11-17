using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CeilingRoomController : Room {

	// pointless cinematic room 

	private bool firstSetup = false;
	private float waitTimer1 = 0f;
	private float waitTimer2 = 0f;
	private float maxTime1 = 10f;
	private float maxTime2 = 5f; 
	private float wipeSpeed = 3f; 

	private float maskTarget;
	private float maskVal; 

	private bool firstPeriod = true;
	private bool secondPeriod = false; 
	private bool thirdPeriod = false; 

	// Use this for initialization
	void Start () {
		base.Start ();
		maskTarget = 1f; 

		gameMaster.setFadeSpeed (.8f, 2f);
		playerObj.GetComponent <SpriteRenderer> ().sprite = player.right; 
		player.disableJump ();

		// probably temporarily disable the stats
		GameObject.FindGameObjectWithTag ("Inventory").GetComponent <Text> ().enabled = true; 


		// temporarily disable player render
		playerObj.GetComponent <SpriteRenderer>().enabled = false; 


		// set the mask on the camera all the way up 
		// might need to set the type of mask we want to use as well if this changes...
		maskVal = 1f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <ScreenTransitionImageEffect>().maskValue = maskVal; 
		player.setSprite (player.right);
	}

	void updateMask(){
		// move the mask towards the target a little at a time

		float delta = maskTarget - maskVal; 
		if(Mathf.Abs (delta) < .01){
			maskVal = maskTarget; 
		}
		else if(delta < 0){
			maskVal -= Time.deltaTime*wipeSpeed;
		}
		else{
			maskVal += Time.deltaTime*wipeSpeed; 
		}

		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <ScreenTransitionImageEffect>().maskValue = maskVal; 
	}


	// Update is called once per frame
	void Update () {

		// have to do this crap here so that in the right location after transition 
		if(!firstSetup){
			
			player.pause ();
			// start the music 
			GameObject.Find ("FollowMusic").GetComponent <AudioSource> ().Play ();

			// maybe play some fan sounds as well 
			firstSetup = true;
		}
			
		updateMask ();

		if (firstPeriod) {

			if (waitTimer1 < maxTime1)
				waitTimer1 += Time.deltaTime;
			else {
				// set open target for mask
				maskTarget = 0; 
				firstPeriod = false;
				secondPeriod = true;
			}
		}


		if(secondPeriod){
			// once the camera mask is open, start another wait timer 
			if(maskVal == maskTarget){
				if (waitTimer2 < maxTime2)
					waitTimer2 += Time.deltaTime; 
				else{
					// once second wait timer has expired, start another anim to close the mask
					maskTarget = 1f; 
					thirdPeriod = true; 
				}

			}
		}

		if(thirdPeriod){
			// once fully closed, transition to first music hall
			if (maskVal == maskTarget) {
				thirdPeriod = false; 
				base.transitionRooms (gameObject, transform.Find ("CeilingRoom_Entrace").GetComponent <DoorController>().portalEnd);
			}

		}

	}
}
