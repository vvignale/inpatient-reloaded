using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PermanentRoomController : Room {

	private Vector3 bedPos; 
	private bool setup;
	public GameObject bedResetterDoor; 
	public GameObject chairResetterDoor; 
	private bool tvOn; 
	private bool playerInBed; 

	public Sprite wheelSprite;
	public Sprite bedSprite; 
	public Sprite bedSprite2; 
	public Sprite toChange; 

	private float timer;
	private float dayTime = 60f; 
	private int feedingIncrement; 
	private float lightingIncrement; 

	private bool timerPaused; 
	private float reboundTimer;
	private float reboundMax; 

	private FeedingNurseController nurse;  

	private float hunger; 
	private float boredom;
	private float pain; 
	private float statMax = 94; 
	private float statMin = 8; 

	private float penaltyTimer; 
	private float penaltyWait; 

	private bool fadeMusicOut; 

	Transform light;

	void OnEnable(){
		base.Start ();

		GameObject inventory = GameObject.FindGameObjectWithTag ("PermanentInventory"); 
		if(inventory != null){
			inventory.GetComponent <Text>().enabled = true;
		}

		setup = true;
		timerPaused = false; 

		// change setup based on in wheelchair or not 
		player.setSprite (toChange);
		if (toChange == wheelSprite){
			// allow player to move again 
			setup = false; 
			player.disableJump ();
			player.unpause ();
			playerObj.GetComponent <Rigidbody2D>().WakeUp ();
			playerInBed = false;
		}
		else{
			// place in the bed
			player.putInBed ();
			playerInBed = true; 
		}


		// deal with whatever changes we wanted to make to the room 
		if(tvOn){
			GetComponent <AudioSource>().Play ();
			// turn on tv light
			transform.Find("TV").transform.Find("TVLight").GetComponent <Light>().enabled = true;
		}
		else{
			GetComponent <AudioSource>().Stop ();
			// turn off tv light
			transform.Find("TV").transform.Find("TVLight").GetComponent <Light>().enabled = false;
		}

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

	public void adjustHunger(float delta){
		hunger += delta; 

		if (hunger < statMin)
			hunger = statMin;
		if (hunger > statMax)
			hunger = statMax; 
	}

	public void resetPain(){
		pain = statMin; 
	}

	void OnGUI(){
		// draw 3 stat bars below regular stats

		int startY = 125; 

		GUIStyle container = new GUIStyle( GUI.skin.box );
		container.normal.background = MakeTex( 100, 15, new Color( .8f, .8f, .8f, 1f ) );
		GUI.Box (new Rect (5, startY, 100, 15), "", container);
		GUI.Box (new Rect (5, startY+45, 100, 15), "", container);
		GUI.Box (new Rect (5, startY+90, 100, 15), "", container);

		GUIStyle bar1 = new GUIStyle( GUI.skin.box );
		bar1.normal.background = MakeTex( (int)hunger, 9, new Color( 1f, 0f, 0f, 1f ) );
		GUI.Box (new Rect (8, startY+3, (int)hunger, 9), "", bar1);

		GUIStyle bar2 = new GUIStyle( GUI.skin.box );
		bar2.normal.background = MakeTex( (int)boredom, 9, new Color( 1f, 0f, 0f, 1f ) );
		GUI.Box (new Rect (8, startY+48, (int)boredom, 9), "", bar2);

		GUIStyle bar3 = new GUIStyle( GUI.skin.box );
		bar3.normal.background = MakeTex( (int)pain, 9, new Color( 1f, 0f, 0f, 1f ) );
		GUI.Box (new Rect (8, startY+93, (int)pain, 9), "", bar3);
	
	}

	void OnDisable(){
		GameObject inventory = GameObject.FindGameObjectWithTag ("PermanentInventory"); 
		if(inventory != null){
			inventory.GetComponent <Text>().enabled = false;
		}
	}

	// Use this for initialization
	void Start () {
		base.Start ();

		GameObject.Find ("AmbientLight").SetActive (false);
		GameObject.FindGameObjectWithTag ("PermanentInventory").GetComponent <Text>().enabled = true;

		player.GetComponent <PlayerController>().restoreEnergyStats ();

		nurse = transform.Find ("FeedingNurse").GetComponent <FeedingNurseController> ();
		timer = 0;
		lightingIncrement = 90f / (dayTime / .02f);	// amount by which to adjust light (since it has to go from 0 to 90 and back) 
		feedingIncrement = (int)dayTime / 2; 			// amount of time between feedings. int since will be modding  
		playerInBed = true; 
		light = transform.Find ("Sun"); 

		// change player's sprite to bedridden
		player.setSprite (bedSprite);

		// place on the bed 
		Vector3 bed = transform.Find("Bed").transform.position; 
		playerObj.transform.position = new Vector3 (bed.x-.8f, bed.y + .4f, player.transform.position.z);

		setup = true; 
		tvOn = false; 

		reboundMax = 1f; 
		reboundTimer = 0; 

		penaltyWait = 5f;
		penaltyTimer = -1f; 

		hunger = statMax / 2; 
		boredom = statMax / 2; 
		pain = statMax / 4; 

		fadeMusicOut = false; 

		// when player is reenabled, simply change sprite to wheelchair, unpause and set in correct spot 
	}

	public bool getBedState(){
		return playerInBed;
	}

	public bool getTVOn(){
		return tvOn;
	}

	public void resetRoom(){
		base.transitionRooms (gameObject, bedResetterDoor);
	}

	public void toggleTV(bool tv){
		tvOn = tv;
		base.transitionRooms (gameObject, bedResetterDoor);
	}

	public void movePlayer(){

		if(playerInBed){
			toChange = wheelSprite; 
			base.transitionRooms (gameObject, chairResetterDoor);
		}
		else{
			toChange = bedSprite; 
			base.transitionRooms (gameObject, bedResetterDoor);
		}
	}

	public void turnPlayer(){
		if(pain > (statMax/2))
			pain = statMax / 2; 
		player.addInventory ("Dignity: ", -1);

		if (toChange == bedSprite){
			toChange = bedSprite2;
		}
		else{
			toChange = bedSprite;
		}
		base.transitionRooms (gameObject, bedResetterDoor);

	}

	public void startFeeding(){
		timerPaused = true;
		nurse.startFeeding ();
	}

	public void endFeeding(){
		timerPaused = false; 
	}

	public void setTimerPause(bool t){
		timerPaused = t; 
	}

	public bool getTimerPause(){
		return timerPaused;
	}

	// Update is called once per frame
	void Update () {

		if (setup) {

			// pause player so all they can do is ask things
			player.pause ();
			playerObj.GetComponent <Rigidbody2D> ().Sleep ();
			GameObject.FindGameObjectWithTag ("GameMaster").GetComponent <GameController>().setFadeSpeed (.8f, 2);
			setup = false;
		}

		if (penaltyTimer != -1){
			penaltyTimer += Time.deltaTime;

			if(penaltyTimer > penaltyWait){
				// toll dignity 
				player.addInventory ("Dignity: ", -1);
				penaltyTimer = 0;
			}
		}

		// tv only keeps you sane 
		if(!tvOn){
			boredom += Time.deltaTime * .5f; 
		}
		else{
			boredom -= Time.deltaTime; 
		}

		// pain constantly increasing over time 
		pain += Time.deltaTime * .5f; 	

		if (pain > statMax)
			pain = statMax;

		if (boredom < statMin)
			boredom = statMin;
		if (boredom > statMax)
			boredom = statMax;
	
		if((pain == statMax || boredom == statMax) && penaltyTimer == -1){
			penaltyTimer = 0f; 
		}
		else if(pain < statMax && boredom < statMax && penaltyTimer != -1){	// if not maxed out on either and penalizing, stop
			penaltyTimer = -1; 
		}

		// slowly reduce volume as we fade out 
		if(fadeMusicOut){
			GetComponent <AudioSource> ().volume -= Time.deltaTime*.05f;
		}

	}

	void FixedUpdate(){

		if (!timerPaused) {
			reboundTimer += Time.fixedDeltaTime;
			timer += Time.fixedDeltaTime;		
			if (timer <= dayTime) {
				//timer = 0;
				// on every tick, move the sun around and back 
				light.Rotate (new Vector3 (lightingIncrement, 0, 0));
			}
			else{
				// transition into the night scene slowly 
				transform.Find("CallNurseButton").GetComponent <CallNurseButton>().setInteract (false); 
				// fade out music
				fadeMusicOut = true; 
				timer = 0; 
				transform.Find("Exit").GetComponent <SlowDoor>().transitionRooms ();
			}

			// can only feed in bed 
			if ((int)timer % feedingIncrement == 0 && (int)timer != 0 && (int)timer != dayTime && (toChange==bedSprite || toChange == bedSprite2) && reboundTimer>reboundMax) {
				startFeeding (); 
				reboundTimer = 0; 
			}
		}
	}

}
