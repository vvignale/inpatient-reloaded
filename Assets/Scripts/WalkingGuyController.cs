using UnityEngine;
using System.Collections;

public class WalkingGuyController : Walker {

	public Sprite walking;
	public Sprite stopped; 

	private float windowTimer = 0; 
	private float windowMax = 5; 

	public override void goalHit(){
		// pause to look out the window
		paused = true; 
		GetComponent <SpriteRenderer>().sprite = stopped;
		windowMax = Random.Range (5, 10);
	}

	void Update(){

		if (paused){
			windowTimer += Time.deltaTime;

			if(windowTimer > windowMax){
				windowTimer = 0; 
				paused = false; 
				GetComponent <SpriteRenderer>().sprite = walking;
			}
		}

		base.Update ();

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Finish"){
			goalHit ();
		}
	}


}
