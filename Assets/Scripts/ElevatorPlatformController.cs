using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatformController : MonoBehaviour {

	private Vector3 target;
	private Vector3 startingPos = Vector3.zero; 
	private bool moving; 

	// Use this for initialization
	void Start () {
		target = transform.position; 
		startingPos = transform.position; 
		moving = false; 
	}

	void OnEnable(){
		if (startingPos == Vector3.zero)
			Start ();
		transform.position = startingPos; 
		target = startingPos;
	}

	// Update is called once per frame
	void Update () {

		if (moving){

			Vector3 diff = target - transform.position; 
			if (diff.sqrMagnitude < .01f){
				transform.position = target; 
				moving = false; 
			}
			else{
				Vector3 dir = diff.normalized;
				transform.Translate (dir*Time.deltaTime);
			}
		}

	}

	public void setTarget(Vector3 newTarget){
		target = newTarget;
		moving = true; 
	}

}
