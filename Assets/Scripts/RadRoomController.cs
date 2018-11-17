using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadRoomController : Room {

	void OnEnable(){
		// clear out extra boxes 
		Transform[] allChildren = GetComponentsInChildren<Transform> ();
		foreach (Transform child in allChildren) {
			if(child.gameObject.name == "MovableBlock(Clone)"){
				Destroy (child.gameObject); 
			}
		}
	}
		
}
