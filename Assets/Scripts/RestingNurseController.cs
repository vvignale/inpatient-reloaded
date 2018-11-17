using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class RestingNurseController : Interactable {

	private GameObject interactionController;
	private Vector3 target;  
	private Vector3 returnPos; 
	private Camera camera; 

	private float bloodMax; 
	private float bloodAmount; 
	private bool bloodDraw; 

	private float fullTimerMax = 2f; 
	private float fullTimer = 0f; 

	private bool readyForBed; 

	// Use this for initialization
	void Start () {
	 
		readyForBed = false; 
		target = Vector3.zero;
		returnPos = transform.position; 
		bloodDraw = false; 
		bloodAmount = 10; 
		bloodMax = 40;	// "volume" of the rect we will fill up

		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <Camera> ();

		text = "Text/RestingNurse1";	
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this); 
		base.Start ();
		
	}

	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}

	void OnGUI(){

		if(bloodDraw){
			// increment our blood amount and draw a rectangle of that size
			if (bloodAmount < bloodMax) {
				bloodAmount += Time.deltaTime * 1.5f;
			}
			else if (fullTimer < fullTimerMax){

				fullTimer += Time.deltaTime; 
			}
			else{
				bloodDraw = false; 
				interact = true; 
				interactionController.GetComponent <InteractionCollider>().startInteraction ();
				target = Vector3.zero; 
			}

			GUIStyle style = new GUIStyle( GUI.skin.box );
			style.normal.background = MakeTex( (int)bloodAmount, 5, new Color( 1f, 0f, 0f, 1f ) );

			Vector3 playerScreenPos = camera.WorldToScreenPoint (player.transform.position);
			GUI.Box (new Rect (playerScreenPos.x-8, playerScreenPos.y + 210, bloodAmount, 5), "", style);
				
		}
		
	}
	
	// Update is called once per frame
	void Update () {


		if (target != Vector3.zero) {
			
			// while not at the target, move towards the target 
			Vector3 dir = target - transform.position; 
			float mag = dir.sqrMagnitude;

			if (mag <= .001) {
				transform.position = target;
				bloodDraw = true;
			} else {
				transform.Translate (dir.normalized * Time.deltaTime);
			}
		}
	}

	public bool getReadyForBed(){
		return readyForBed;
	}

	public override void handleInteractionEnd(){
		// if after the first one, initiate a little blood draw sequence. pause the player for this  
		// add on the dir of the player
		if(text == "Text/RestingNurse1"){
			target = player.transform.position - ((player.transform.position - transform.position).normalized); 
			player.GetComponent <PlayerController>().pause ();

			text = "Text/RestingNurse2";
			interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this);
			interact = false; 
		}

		else{
			readyForBed = true;
		}

	}

}
