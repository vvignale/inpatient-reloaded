using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEmitterController : MonoBehaviour {

	public int emitMax; 
	private int numberEmitted = 0; 
	private List <GameObject> blocks = new List<GameObject>(); 

	void OnEnable(){
		numberEmitted = 0; 
		clearBlocks ();
	}

	public void emitBlock(){

		if (numberEmitted < emitMax) {
			GameObject block = Instantiate (Resources.Load ("Prefabs/MovableBlock"), transform.position, transform.rotation) as GameObject;
			block.transform.SetParent (transform.parent.transform);
			numberEmitted += 1; 
			blocks.Add (block);
		}
	}

	public void clearBlocks(){
		foreach(GameObject block in blocks){
			Destroy (block);
		}
	}
}
