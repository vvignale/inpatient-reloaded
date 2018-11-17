using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall10Controller : PostSurgeryIRoom {

	// Use this for initialization
	void Start () {
		// lower player elasticity 
		base.Start ();
		player.weakenPlayer ();
	}
}
