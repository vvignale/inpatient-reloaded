using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class FacadeController : Room {

	void Awake(){
		gameObject.SetActive (false); 
	}

	// Use this for initialization
	void Start () {
		base.Start ();
		GameObject.FindGameObjectWithTag ("Player").SetActive (true); 

	}
	
	// Update is called once per frame
	void Update () {
	}
}

