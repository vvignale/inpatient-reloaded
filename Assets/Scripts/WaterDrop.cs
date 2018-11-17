using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Ground"){
			Destroy (gameObject); 	
		}
	}

}
