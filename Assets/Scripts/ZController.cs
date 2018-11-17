using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZController : MonoBehaviour {

	private float distanceTraveled;
	private float distanceMax;
	private float speed = 1f;
	private float scaleFactor = .2f; 

	private int dir;
	private float rotRange;
	private float currRot; 
	private float rotSpeed = 8f;

	// Use this for initialization
	void Start () {
		distanceTraveled = 0;
		distanceMax = 1.5f;
		rotRange = 7f;
		currRot = Random.Range (-1 * rotRange, rotRange);
		transform.Rotate (0, 0, currRot);
		float rand = Random.Range (0f, 1f);
		if(rand < .5){
			dir = 1;
		}
		else{
			dir = -1; 
		}
	}
	
	// Update is called once per frame
	void Update () {

		// rotational jitter
		currRot += Time.deltaTime*dir*rotSpeed; 
		transform.Rotate (0, 0, Time.deltaTime*dir*rotSpeed);
		if (currRot > rotRange || currRot < -rotRange)
			dir *= -1;

		// move upward until reach distance max
		distanceTraveled += Time.deltaTime * speed; 
		transform.Translate (new Vector3(0, Time.deltaTime*speed, 0));

		// increase size until reach full scale 
		if (scaleFactor <= 1){
			scaleFactor += Time.deltaTime*.4f; 
			gameObject.transform.localScale = new Vector3 (scaleFactor, scaleFactor, 1);
		}

		// start fading z at desired height 
		if(distanceTraveled > distanceMax){
			Color spriteColor = gameObject.GetComponent <SpriteRenderer> ().color;
			spriteColor.a -= Time.deltaTime * speed;
			if (spriteColor.a <= 0)
				Destroy (gameObject);
			else
				gameObject.GetComponent <SpriteRenderer>().color = spriteColor;
		}
			
	}
}
