using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeNurseController : Interactable {

	private GameObject interactionController;
	private string[] texts; 
	private int textIndex; 


	private float sleepTimer; 
	private float sleepTime = 20f; 
	private bool asleep; 
	private float delay;
	private float delayMax = 2f; 
	private bool once;

	public Sprite sleepy1;
	public Sprite sleepy2;
	public Sprite sleepy3;
	public Sprite awake; 

	private float interval; 
	private float zInterval = .5f; 
	private float zTimer = 0; 

	void Start(){
		base.Start ();
		automatic = true; 
		interval = sleepTime / 3f; 
	}

	// using instead of start cause need to reset each time 
	void OnEnable () {
		asleep = false; 
		once = true;
		sleepTimer = 0f;
		delay = 0f; 

		textIndex = 0; 
		texts = new string[2]{"Text/EscapeNurse1", "Text/EscapeNurse2"};

		interactionController = Instantiate(Resources.Load("Prefabs/InteractionCollider"), transform.position, transform.rotation) as GameObject;
		interactionController.transform.SetParent (gameObject.transform); 
		interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, texts[textIndex], this);

		automatic = true;
		// set regular sprite 

		GetComponent<SpriteRenderer> ().sprite = awake;
	}

	public bool isAsleep(){
		return asleep; 
	}

	public void scoldPlayer(){
		// method gets called if player tries to leave before nurse asleep. run interaction and reset timer
		startInteraction (); 
		sleepTimer = 0f; 
		GetComponent<SpriteRenderer> ().sprite = awake;
	}
	
	// Update is called once per frame
	void Update () {

		if (once) {
			if (delay < delayMax)
				delay += Time.deltaTime;
			else {
				startInteraction ();
				once = false; 
			}
		}

		else{

			// then timer starts 
			sleepTimer += Time.deltaTime; 

			// when timer reaches limit, fall asleep. door unlocks 
			if(sleepTimer > sleepTime){
				asleep = true; 
				// sprite 3
				transform.parent.Find ("EscapeRoom1_Exit").GetComponent <DoorController> ().setLock (false);
				GetComponent<SpriteRenderer> ().sprite = sleepy3;

				// play z animation 
				zTimer += Time.deltaTime; 
				if(zTimer >= zInterval){
					zTimer = 0; 
					GameObject z = Instantiate(Resources.Load("Prefabs/Z"), transform.position+new Vector3(0,1,0), transform.rotation) as GameObject;
					z.transform.SetParent (gameObject.transform);
				}
			}

			else if(sleepTimer > interval && sleepTimer < interval*2){
				// sprite 1
				GetComponent<SpriteRenderer> ().sprite = sleepy1;
			}
			else if(sleepTimer > interval*2 && sleepTimer < interval*3){
				// sprite 2
				GetComponent<SpriteRenderer> ().sprite = sleepy2;
			}
		}
			


	}
		
	public void startInteraction(){
		interactionController.GetComponent<InteractionCollider>().startInteraction ();
	}

	public override void handleInteractionEnd(){
		textIndex += 1;
		if(textIndex < texts.Length){
			interactionController.GetComponent<InteractionCollider>().doSetup(6, 6, texts[textIndex], this);
		}
	}
		
}
