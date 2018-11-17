using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFadeIn : MonoBehaviour {

	private bool fadeIn; 
	private float max = .5f; 

	// Use this for initialization
	void Start () {
		fadeIn = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(fadeIn){
			AudioSource audio = GetComponent <AudioSource> ();
			if(audio.volume < max){
				audio.volume += Time.deltaTime*.1f; 
			}
			else{
				audio.volume = max; 
				fadeIn = false; 
			}
		}
	}
}
