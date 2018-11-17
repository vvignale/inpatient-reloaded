using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPlatform : MonoBehaviour {

	private Vector3 center; 
	public float radius = 1f;
	public float speed = 1f;
	public float dir = 1f; 
	private float timer; 

	// Use this for initialization
	void Start () {
		center = gameObject.transform.position; 
		timer = 0; 
	}



	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime*speed;
		if (timer >= 360)
			timer = 0; 

		// sin and cos of the current time. may multiply by speed
		gameObject.transform.position = center + new Vector3(dir*Mathf.Cos (timer)*radius, Mathf.Sin(timer)*radius, 0);
		
	}
}
