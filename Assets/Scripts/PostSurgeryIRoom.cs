using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostSurgeryIRoom : Room {

	public Sprite wheelchairSprite; 

	// Use this for initialization
	void Start () {

		// changing point of the game
		// change player sprite and disable jumping 
		base.Start ();
		playerObj.GetComponent <SpriteRenderer> ().sprite = player.right; 
		player.disableJump ();

		// restore normal settings to fading 
		GameObject.FindGameObjectWithTag ("GameMaster").GetComponent <GameController>().setFadeSpeed (.8f, -1f);
		GameObject.FindGameObjectWithTag ("Inventory").GetComponent <Text> ().enabled = true; 
	}
}
