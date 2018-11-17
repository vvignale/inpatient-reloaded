using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformCollider : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D other){

		print ("collision");

		if (other.collider.tag == "MovingPlatform" || other.collider.tag == "Elevator"){
			print ("parenting to: " + other);

			transform.parent = other.transform; 
		}
	}


	void OnCollisionExit2D(Collision2D other){
		if (other.collider.tag == "MovingPlatform" || other.collider.tag == "Elevator"){
			print ("unparenting from: " + other);
			transform.parent = null; 
		}
	}
}
