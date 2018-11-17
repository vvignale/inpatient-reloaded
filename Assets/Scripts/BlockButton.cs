using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButton : Interactable {

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.Return) && inRange){
			// tell parent to drop a block 
			transform.parent.GetComponent <BlockEmitterController>().emitBlock ();
		}

	}
}
