using UnityEngine;
using System.Collections;

public class Resetter : DoorController {

	public void clearObjects(){
		// look to the parent/container to clear out any objects as needed
		Transform blockEmitter = transform.parent.Find ("BlockEmitter"); 
		if(blockEmitter != null){
			blockEmitter.gameObject.GetComponent <BlockEmitterController>().clearBlocks ();
		}

	}

	public override void transitionRooms(){
		clearObjects ();
		base.transitionRooms ();	
	}

}
