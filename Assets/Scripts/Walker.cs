using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {

	public Vector3[] destinationPoints; 		// points for the ai to reach 
	public float speed; 
	protected int index; 
	protected Utils utils; 
	protected bool paused; 

	// Use this for initialization
	public void Start () {
		index = 0; 
		utils = Utils.Instance; 
		paused = false; 

		// normalize against parent transform 
		for(int i=0; i<destinationPoints.Length; i++){
			Vector3 point = destinationPoints[i] + gameObject.transform.parent.position; 
			destinationPoints [i] = point; 
		}
	}

	public void setPause(bool p){
		paused = p; 
	}

	// callback for when reach a destination point
	public virtual void goalHit(){}
	
	public void Update () {

		if (!paused) {

			// compare current position to position at intended index
			Vector3 currPos = gameObject.transform.position; 
			Vector3 dest = destinationPoints [index]; 

			if (utils.equalsWithinEps (currPos, dest)) {
				// have reached current destination. move on to the next destination point 
				index += 1; 
				if (index >= destinationPoints.Length) {
					index = 0; 
				}
				goalHit ();
			} else {
				// move toward destination 
				Vector3 direction = (dest - currPos).normalized;
				gameObject.transform.Translate (direction * Time.deltaTime * speed); 
			}
			
		}
	}
		
}
