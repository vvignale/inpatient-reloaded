using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour {

	public Sprite right;
	public Sprite left; 
	public Sprite rightSleepy;
	public Sprite leftSleepy; 
	public Sprite bedridden; 
	public Sprite upright; 
	public Texture2D energyTex; 

	private SpriteRenderer sr; 
	private float oldMovement; 

	private float walkSpeed = 5f;
	private float jumpSpeed = 5f; 
	private float speed;
	private Animator animator;
	private Rigidbody2D rb; 
	private bool paused;

	private bool once = true; 
	private bool jumpPrepping = false; 

	private bool onPlatform; 
	private bool canJump; 
	private bool jumping; 
	private float jumpForce = 95f; 
	private Dictionary<string, float> inventory; 


	// params for how quickly you injure and rejuvinate and to what extent 
	private float energyMax; 
	private float energyDecreaseRate;	// speed we lose energy
	private float edrFlat = -.05f; 
	private bool canReboot;
	private float energyIncreaseRate;	// bounce back ability 

	void Awake(){
		transform.position = new Vector3 (100, 100, 0); 	// random far away starting point. Don't know why I implemented it like this instead of just deactivating like a normal fucking person 
		canJump = true;
		onPlatform = false; 
		animator = GetComponent<Animator> ();
	}

	public void restoreEnergyStats(){
		edrFlat = -.06f;
		energyDecreaseRate = edrFlat;
		energyIncreaseRate = .7f;
		canReboot = true; 
	}

	public void makeSleepy(){
		if(sr.sprite == right){
			sr.sprite = rightSleepy;
		}
		else if(sr.sprite == left){
			sr.sprite = leftSleepy;
		}
	}

	// increase flat decrease rate and decrease bounce back
	public void weakenPlayer(){
		edrFlat -= .015f;
		energyDecreaseRate = edrFlat;
		energyIncreaseRate -= .1f;
		// don't let this go negative 
		if(energyIncreaseRate < 0){
			energyIncreaseRate = .1f; 
		}
	}

	// Use this for initialization
	void Start () { 

		canReboot = true; 
		energyMax = 100f; 
		energyDecreaseRate = edrFlat;
		energyIncreaseRate = 1f; 

		paused = true; 
		speed = walkSpeed;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		jumping = false;
		sr = GetComponent <SpriteRenderer> ();
		oldMovement = 0; 

	}

	public void setSprite(Sprite s){
		sr.sprite = s;
	}

	public void setReboot(bool reboot){
		canReboot = reboot;
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

	public void initializeInventory(){
		GameObject.FindGameObjectWithTag ("Inventory").SetActive (true);	// inventory activated
		inventory = new Dictionary<string, float> ();
		addInventory ("Energy: ", 100);
		addInventory ("Savings: $", 10000);
		addInventory ("Dignity: ", 5);

	}

	public float getInventory(string key){
		if(hasInventory (key))
			return inventory [key];
		return -1000; 
	}

	public bool hasInventory(string key){
		return inventory.ContainsKey (key);
	}

	// adjust the inventory of type tag by delta (can be negative amount)
	public void addInventory(string tag, float delta){

		if(!inventory.ContainsKey (tag))
			inventory [tag] = 0;

		inventory [tag] += delta; 

		// Clamp vals
		if(inventory.ContainsKey ("Energy: ")){
			if (inventory ["Energy: "] > 100)
				inventory ["Energy: "] = 100;
			if (inventory ["Energy: "] < 1)
				inventory ["Energy: "] = 1;
		}
			
		if(inventory.ContainsKey ("Dignity: ")){
			if (inventory ["Dignity: "] < 0)
				inventory ["Dignity: "] = 0;	
		}
			
		// update text of the ui display
		string toDisplay = "";
		foreach (KeyValuePair<string, float> entry in inventory){
			if ((entry.Key == "Dignity: ") || (entry.Key == "Savings: $")) {
				string toAdd = entry.Key + entry.Value + "\n";
				toDisplay = string.Concat (toDisplay, toAdd);
			}
		}
		GameObject.FindGameObjectWithTag ("Inventory").GetComponent <Text>().text = toDisplay;
	
	}

	RaycastHit2D grounded(){
		// cast from bottom of bounding box minus epsilon so as not to hit self 
		Vector2 bottom = new Vector2(gameObject.GetComponent<Collider2D>().transform.position.x, gameObject.GetComponent<Collider2D>().bounds.min.y-.01f);
		return Physics2D.Raycast (bottom, new Vector2 (0, -1));	
	}
		
	void OnGUI(){
		if (inventory != null) {
			GUIStyle style = new GUIStyle( GUI.skin.box );
			style.normal.background = MakeTex( (int)inventory ["Energy: "], 10, new Color( 1f, 1f, 0f, 1f ) );

			GUI.Box (new Rect (5, 60, inventory ["Energy: "], 10), "", style);
		}
	}

	public void jumpPrepDone(){
		animator.SetInteger ("State", 0);
		jumpPrepping = false; 
		rb.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);

		jumping = true; 
	}

	void Update () {


		if (once){
			once = false; 
		}

		if (!paused) {		// access from main controller

			if (jumping)
				speed = jumpSpeed;
			else
				speed = walkSpeed; 

			// moderate speed by energy
			speed = speed * inventory["Energy: "] / 100f; 

			// very difficult to go up stairs if only able to apply velocity while grounded
			float movement = Input.GetAxis ("Horizontal");
			if (movement > 0f) {
				rb.velocity = new Vector2 (movement * speed, rb.velocity.y);
			}
			else if (movement < 0f) { 
				rb.velocity = new Vector2 (movement * speed, rb.velocity.y);
			} 
			else {
				rb.velocity = new Vector2 (0,rb.velocity.y);
			}
				
			RaycastHit2D hit = grounded ();
			bool onGround = (hit.distance < .1f); 

			if (onGround) {		// raycast from bottom of char, so very small tolerance .01. temporarily increased because player collider is inaccurate

				// check if need to change parenting 
				if (hit.collider != null && (hit.collider.tag == "MovingPlatform" || hit.collider.tag == "Elevator")) {
						transform.parent = hit.collider.gameObject.transform; 
						onPlatform = true;
				} else {
						transform.parent = null; 
						onPlatform = false; 
				}

				//print ("jumping:");
				//print (jumping); 
				 
				if (canJump) {
					
					jumping = false; 
					// TODO double jump?
					if (Input.GetKeyDown (KeyCode.W)) {
						// JUMP BEGINNING
						// 1. play animation 
						animator.Play ("Jumping");
						//rb.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
						jumpPrepping = true;	// this gets turned off when callback arrives 
						jumping = true; 
					}
				}
			}


			// DEALING WITH ANIMAITONS AND SPRITES:

			// swap out the sprites as needed
			/*
			if(oldMovement * movement <= 0 ){ // if direction changed
				if(! canJump){		
					if (movement < 0)
						sr.sprite = left;
					else if(movement > 0)
						sr.sprite = right; 
				}
			}
			*/

			if (!jumpPrepping && !jumping) {		// ground animations follow different logic  
				if (movement != 0 && onGround) {	// check for different directions of movement 

					// TODO make sure grounded 

					// will need two different walking animations for the chair time (use logic above)
					// maybe should also set the sprites actually...
					animator.SetInteger ("State", 1);
				} else {
					animator.SetInteger ("State", 0);
				}
			}
				



			// END ANIMATION AND SPRITE SECTION 

			oldMovement = movement;


			// deal with energy. penalize for movement. time based
			addInventory ("Energy: ", Mathf.Abs (movement) * energyDecreaseRate );
			if(movement == 0 && canReboot){
				addInventory ("Energy: ", energyIncreaseRate);
			}

		}
		else
			rb.velocity = new Vector2 (0, 0);		// hold the player still if paused

	}

	public bool getPause(){
		return paused; 
	}

	public void pause(){
		paused = true; 
		GetComponent <Rigidbody2D>().Sleep ();
		//animator.SetInteger ("State", 0);
	}

	public void unpause(){
		paused = false; 
		GetComponent <Rigidbody2D> ().WakeUp ();
	}

	public void disableJump(){
		canJump = false; 

		// remove old collider and put a new one on
		Destroy(GetComponent <Collider2D>());
//		Collider2D newCollider = gameObject.AddComponent (typeof(BoxCollider2D)) as Collider2D;
//		newCollider.GetComponent <BoxCollider2D>().size = new Vector2(1.775f, 1.51f);	// tested values
//		newCollider.GetComponent <BoxCollider2D>().offset = new Vector2(-.0625f, -0.246f);

		Collider2D newCollider = gameObject.AddComponent (typeof(CircleCollider2D)) as Collider2D;
		newCollider.GetComponent <CircleCollider2D>().offset = new Vector2(-0.094f, -0.193f);
		newCollider.GetComponent <CircleCollider2D> ().radius = 0.772f; 


		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <CameraFollow>().setPlayerCollider (newCollider);

	}

	public void putInBed(){
		canJump = false; 
		Destroy(GetComponent <Collider2D>());

		Collider2D newCollider = gameObject.AddComponent (typeof(BoxCollider2D)) as BoxCollider2D;

		//newCollider.GetComponent <CircleCollider2D>().offset = new Vector2(-0.094f, -0.193f);
		//newCollider.GetComponent <CircleCollider2D> ().radius = 0.772f; 

		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent <CameraFollow>().setPlayerCollider (newCollider);

		pause ();
	}


	void OnCollisionEnter2D(Collision2D other){
		if(other.collider.tag == "Movable"){
			energyDecreaseRate = 3*edrFlat; 
		}	
	}

	void OnCollisionExit2D(Collision2D other){
		if(other.collider.tag == "Movable"){
			energyDecreaseRate = edrFlat;
		}
	}
		
}
