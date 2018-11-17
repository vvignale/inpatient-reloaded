using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	// transition container class  

	public GameObject container; 
	public Vector2 direction;
	public bool locked; 
	public GameObject portalEnd; 		// the other end of this door/portal


	public GameObject getContainer(){
		return container; 
	}

	public virtual void transitionRooms(){
		GameObject.FindObjectOfType<Room> ().transitionRooms (container, portalEnd);	// having to find every time instead of just once...
	}

	public void OnTriggerEnter2D(Collider2D other){
		// call the container to do the transition 

		if (!locked && other.tag == "Player") {
			transitionRooms ();
		}
	}

	public Vector3 getDirection(){
		return new Vector3(direction.x, direction.y, 0); 
	}

	public void setLock(bool l){
		locked = l; 
	}

	public bool getLock(){
		return locked; 
	}

}
