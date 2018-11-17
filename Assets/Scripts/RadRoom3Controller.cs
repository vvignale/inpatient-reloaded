using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadRoom3Controller : MonoBehaviour {


	// exists mainly to modify camera and freeze one plat
	private bool once; 


	// Use this for initialization
	void Start () {
		OnEnable ();
	}

	void OnEnable(){
		once = true; 

		// set camera
		//GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <Camera>().orthographicSize = 6;
	}

	void OnDisable(){
		//GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <Camera>().orthographicSize = 5;
	}


	
	// Update is called once per frame
	void Update () {

		if (once) {
			transform.Find ("Umbrella").transform.Find("Platform").GetComponent <MovablePlatform> ().toggleFrozen (true);
			once = false; 
		}
		
	}
}
