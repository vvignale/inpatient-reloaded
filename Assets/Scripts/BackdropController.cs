using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropController : MonoBehaviour {

	private Camera camera; 

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <Camera> ();
	}
	
	// Update is called once per frame
	void Update () {

		// make sure the object is rendered at 0, 0 screen space and that's it

		transform.position = camera.ScreenToWorldPoint (new Vector3(0, Screen.height, 0) - new Vector3(0,0,-10));

		print (transform.position);

	}
}
