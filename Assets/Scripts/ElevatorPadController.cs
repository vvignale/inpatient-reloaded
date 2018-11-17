using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPadController : Interactable {

	protected bool playerOn; 
	protected bool elevatorOn; 

	private Vector3 other; 

	// Use this for initialization
	public void Start () {

		base.Start ();
		playerOn = false; 

		// set the other location for where other pad is 
		// janky ass method relying on the names being "PadA" and "PadB". Which might be okay since this is a prefab
		if (this.name == "PadA") {
			other = transform.parent.Find ("PadB").transform.position; 
		} else if (this.name == "PadB") {
			other = transform.parent.Find ("PadA").transform.position; 
		} else{
			print ("Error -- couldn't find corresponding landing pad"); 
		}
		
	}
	
	// Update is called once per frame
	public void Update () {

		if(Input.GetKeyDown(KeyCode.Return) && playerOn){	// only react if the player is waiting

			// if elevator on, initialize a move to the next pad 
			if(elevatorOn){
				transform.parent.Find("ElevatorPlatform").GetComponent <ElevatorPlatformController> ().setTarget (other);
			}
			else{ 			// if elevator off, call elevator to this location 
				transform.parent.Find("ElevatorPlatform").GetComponent <ElevatorPlatformController>().setTarget (transform.position);
			}
				
		}
	}
		
	void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D (other);
		if(other.tag == "Elevator"){
			elevatorOn = true; 
		}

		if (other.tag == "Player"){
			playerOn = true; 
		}
	}

	void OnTriggerExit2D(Collider2D other){
		base.OnTriggerExit2D (other);
		if (other.tag == "Elevator"){
			elevatorOn = false; 
		}

		if (other.tag == "Player"){
			playerOn = false; 
		}
	
	}
}
