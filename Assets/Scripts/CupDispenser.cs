using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupDispenser : Interactable {
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.Return) && inRange){
			// instantiate a cup and give to player 
			if (! player.GetComponent <PlayerController>().hasInventory ("Cup")){
				GameObject cup = Instantiate(Resources.Load("Prefabs/Cup"), player.transform.position+new Vector3(.5f,0,-.01f), transform.rotation) as GameObject;
				cup.transform.parent = player.transform; 
				player.GetComponent <PlayerController>().addInventory ("Cup", 1);
			}
		}
	}
}
