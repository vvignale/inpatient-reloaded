using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatform : MovablePlatform {


	// pretty much the same as a movable platform, except that once a player is on, there is a time limit
	// over the limit, the list of sprites change for the plat
	// once the limit is reached, the object is disabled

	public float fadeTime;			// amount of time before this object disappears
	public Sprite[] sprites; 		// list of sprites that object will cycle through over fade time
	private int spriteIndex; 
	private float fadeInterval; 
	private bool playerOn;	
	private float timer; 

	void Start(){
		OnEnable ();
	}

	void OnEnable(){
		base.OnEnable ();

		spriteIndex = 0; 
		timer = 0; 
		playerOn = false; 
		gameObject.GetComponent <SpriteRenderer>().enabled = true;
		gameObject.GetComponent <Collider2D>().enabled = true;

		// divide the fade time by the number of sprites to know how long to spend on each one 
		fadeInterval = fadeTime/sprites.Length; 
		gameObject.GetComponent <SpriteRenderer>().sprite = sprites[spriteIndex];
	}

	void Update(){


		if(playerOn){

			timer += Time.deltaTime; 


			if(timer >= fadeInterval){
				timer = 0; 
				spriteIndex += 1; 

				if(spriteIndex >= sprites.Length){
					// disable renderer and collider
					gameObject.GetComponent <SpriteRenderer>().enabled = false;
					gameObject.GetComponent <Collider2D>().enabled = false;
				}
				else{
					gameObject.GetComponent <SpriteRenderer>().sprite = sprites[spriteIndex];
				}

			}

		}	
	}

	// what to do when the player is on and off 
	void OnCollisionEnter2D(Collision2D other){
		if(other.collider.tag == "Player"){
			playerOn = true; 
		}	
	}


	void OnCollisionExit2D(Collision2D other){
		if(other.collider.tag == "Player"){
			playerOn = false;
			spriteIndex = 0; 
			gameObject.GetComponent <SpriteRenderer>().sprite = sprites[spriteIndex];
			timer = 0; 
		}
	}


}
