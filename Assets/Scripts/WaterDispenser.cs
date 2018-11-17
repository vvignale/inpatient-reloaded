using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDispenser : MonoBehaviour {

	private float dispenseTime = 8f;
	private float interval = .05f;
	private float intervalTimer;
	private float dispenserTimer;

	private bool dispensing;

	// Use this for initialization
	void Start () {
		dispensing = false; 
	}
	
	// Update is called once per frame
	void Update () {

		if (dispensing){
			intervalTimer += Time.deltaTime; 
			if(intervalTimer > interval){
				GameObject drop = Instantiate(Resources.Load("Prefabs/WaterDrop"), transform.position, transform.rotation) as GameObject;
				drop.transform.SetParent (gameObject.transform); 
				intervalTimer = 0;
			}
			dispenserTimer += Time.deltaTime;
			if(dispenserTimer > dispenseTime){
				dispensing = false; 
			}
		}
		
	}

	public void startDispensing(){
		dispensing = true; 
		intervalTimer = 0;
		dispenserTimer = 0;
	}
}
