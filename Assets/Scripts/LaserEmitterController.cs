using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitterController : MonoBehaviour {

	public float frequency = 1; 	// shoot off every second by default 
	public float speed = 1f; 
	public Vector2 direction; 
	private float timer;

	public float lapse = 0; 
	public float lapseInterval = 0; 
	private float lapseTimer1 = 0;
	private float lapseTimer2 = -1; 
	private bool firing; 

	// Use this for initialization
	void Start () {

		timer = 0; 
		firing = true; 
	}
	
	// Update is called once per frame
	void Update () {
	
		// on some kind of period, instantiate a laser. speed is handled by the frequency 
		// make the instance a child of this object 
		timer += Time.deltaTime; 
		if(timer >= frequency && firing){
			// instantiate a laser
			GameObject laser = Instantiate(Resources.Load("Prefabs/Laser"), transform.position, transform.rotation) as GameObject;
			laser.transform.SetParent (gameObject.transform);
			laser.GetComponent <LaserController>().setDir (direction, speed);
			timer = 0; 
		}

		if(lapseInterval > 0 && lapse > 0){

			lapseTimer1 += Time.deltaTime; 

			if(lapseTimer2 != -1){
				lapseTimer2 += Time.deltaTime; 
				if(lapseTimer2 > lapse){
					// restart laser
					firing = true; 
					lapseTimer2 = -1; 
					lapseTimer1 = 0; 
				}
			}
			else if(lapseTimer1 > lapseInterval){
				lapseTimer2 = 0; 
				// stop laser 
				firing = false;
			}

		}


	}

	// addendum to stop lasers upon pushing boxes over
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Movable"){
			firing = false;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Movable"){
			firing = true; 	
		}
	}

}
