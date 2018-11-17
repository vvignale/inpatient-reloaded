using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingNurseController : Interactable {

	PermanentRoomController room; 
	private float xTarget = -1; 
	private float startX; 
	private GameObject interactionController; 

	private bool feeding; 
	private float feedAmount; 
	private float feedMax = 20f; 

	void OnEnable(){
		
	}

	// Use this for initialization
	void Start () {

		base.Start ();

		room = transform.parent.gameObject.GetComponent <PermanentRoomController>();
		GetComponent <SpriteRenderer> ().enabled = false;
		GetComponent <Collider2D>().enabled = false;

		startX = transform.position.x; 
		automatic = true; 
		text = "Text/FeedingNurse1";	
		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this); 

		feeding = false; 

		OnEnable ();
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

		if(feeding){
			// increment our blood amount and draw a rectangle of that size
			if (feedAmount > 1) {
				feedAmount -= Time.deltaTime * 1.5f;
			}

			else{
				feeding = false; 
				interact = true; 

				// play the interaction saying you're done 
				interactionController.GetComponent <InteractionCollider>().startInteraction ();
			}

			GUIStyle style = new GUIStyle( GUI.skin.box );
			style.normal.background = MakeTex( 5, (int)feedAmount+1, new Color( .97f, .88f, .7f, 1f ) );

			Vector3 playerScreenPos = Camera.main.WorldToScreenPoint (player.transform.position + new Vector3(0,1,0));
			GUI.Box (new Rect (playerScreenPos.x, playerScreenPos.y, 5, -feedAmount), "", style);

			room.adjustHunger (-.05f);
		}

	}
	
	// Update is called once per frame
	void Update () {

		// only need to transform in x since 2d simple ;)
		if(xTarget != -1){

			float distance = xTarget - transform.position.x; 
			int dir; 
			if (distance < 0)
				dir = -1;
			else
				dir = 1; 

			if (Mathf.Abs (distance) < .05){
				transform.position = new Vector3(xTarget, transform.position.y, transform.position.z);

				if(xTarget == startX){
					// turn invisible and wait again
					GetComponent <SpriteRenderer> ().enabled = false;
					GetComponent <Collider2D> ().enabled = false; 
					room.endFeeding ();
				}
				else{
					// start the animation of feeding
					feeding = true; 
					feedAmount = feedMax; 
				}
					
				xTarget = -1; 
			}
			else{
				transform.Translate (new Vector3 (Time.deltaTime * dir * 2f, 0, 0));
			}

		}
	}

	public void startFeeding(){

		GetComponent <SpriteRenderer> ().enabled = true;
		GetComponent <Collider2D>().enabled = true;

		// start an interaction saying that it's time to feed 
		interactionController.GetComponent <InteractionCollider>().startInteraction (); 
	}

	public override void handleInteractionEnd(){

		if (text == "Text/FeedingNurse1" || text == "Text/FeedingNurse3") {
			// change to the second text 
			xTarget = player.transform.position.x + 3f; 

			text = "Text/FeedingNurse2";
			interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, text, this);
			interact = false; 

		}
		else{
			xTarget = startX; 
			text = "Text/FeedingNurse3";
			interactionController.GetComponent<InteractionCollider> ().updateText (text);
		}
			
		// can only do feedings in bed 
		player.GetComponent <PlayerController>().pause ();

	}

}
