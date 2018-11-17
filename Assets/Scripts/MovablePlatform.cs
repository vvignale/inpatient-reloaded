using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour {

	private Vector3 target;
	private Vector3 startingPos = Vector3.zero; 
	public float speed = 1f; 
	private bool frozen = false; 

	public List<GameObject> targets; 
	private int index; 

	// Use this for initialization
	void Start () {
		startingPos = transform.position; 
		OnEnable ();
	}
		
	public void OnEnable () {
		if (startingPos == Vector3.zero)
			startingPos = transform.position; 
		index = 0; 
		frozen = false; 
		target = targets [index].transform.position;
		transform.position = startingPos; 
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (!frozen) {
			Vector3 delta = new Vector3 (0, 0, 0); 
			Vector3 diff = target - transform.position; 

			if (diff.sqrMagnitude < .001f) {	
				transform.position = target;
				if (index < targets.Count-1)
					index += 1;
				else
					index = 0;
				target = targets[index].transform.position; 

			} else {
				Vector3 dir = diff.normalized;
				delta = dir * Time.deltaTime * speed;
				transform.Translate (delta);
			}
		}

	}

	public bool getFrozen(){
		return frozen; 
	}

	public void toggleFrozen(bool f){
		frozen = f; 
	}

}
