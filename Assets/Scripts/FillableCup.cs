using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillableCup : MonoBehaviour {

	public Sprite empty;
	public Sprite low;
	public Sprite med;
	public Sprite full;

	private int interval = 30; 
	private int liquidContact;

	// Use this for initialization
	void Start () {
		liquidContact = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Liquid"){
			liquidContact += 1; 
			// delete the game object associated with this 
			Destroy (other.gameObject);
		}

		if (liquidContact > interval && liquidContact <= 2*interval){
			GetComponent <SpriteRenderer>().sprite = low;
		}
		else if(liquidContact > 2*interval && liquidContact <= 3*interval){
			GetComponent <SpriteRenderer>().sprite = med;
		}
		else if(liquidContact > 3*interval){
			GetComponent <SpriteRenderer>().sprite = full;
		}
		else if(liquidContact <= interval){
			GetComponent <SpriteRenderer>().sprite = empty;
		}

	}
}
