using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

	protected bool inRange; 
	protected string text;		// current text that this interactable will display
	protected bool interact = true; 
	protected GameObject interactionTriangle;  
	protected GameObject player;
	protected bool automatic; 

	public void Start(){

		player = GameObject.FindGameObjectWithTag ("Player");
		automatic = false; 
		Vector3 trianglePos = new Vector3(transform.position.x, transform.position.y+2, transform.position.z-.1f);
		interactionTriangle = Instantiate(Resources.Load("Prefabs/InteractionTriangle"), trianglePos, transform.rotation) as GameObject;
		interactionTriangle.transform.SetParent (gameObject.transform); 
		interactionTriangle.GetComponent <SpriteRenderer>().enabled = false;	// can't see by default 
	}

	// helper method to place a gui box of boxSize around a target location
	public Vector2 placeBox(Vector3 target, Vector2 boxSize){

		Vector3 camLoc = Camera.main.WorldToScreenPoint (target);
		Vector2 toRet = new Vector2 (camLoc.x - (boxSize.x / 2), camLoc.y - 0);
		return toRet; 
		
	}

	public void setInteract(bool i){
		interact = i; 
	}

	public virtual bool canInteract (){
		return interact; 
	}

	public bool getAutomatic(){
		return automatic; 
	}

	// This is called after an interaction ends. in here we can keep track of things like
	// how many times we've interacted and call different scripts accordingly. 
	// we can also do a behavior on the end if necessary
	public virtual void handleInteractionEnd(){}

	public virtual void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && !automatic){
			interactionTriangle.GetComponent <SpriteRenderer> ().enabled = true;
			inRange = true; 
		}
	}

	public virtual void OnTriggerExit2D(Collider2D other){
		
		if (other.tag == "Player" && !automatic) {
			interactionTriangle.GetComponent <SpriteRenderer> ().enabled = false;
			inRange = false; 
		}
	}



}
