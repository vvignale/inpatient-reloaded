using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadeSpeed = .8f; 

	private bool transitioning = false; 
	private int drawDepth = -1000; 
	private float alpha = 1.0f; 
	private int fadeDir = -1; 
	private GameController gameMaster; 

	void Start(){
		fadeOutTexture = Resources.Load ("Sprites/Black") as Texture2D;
		gameMaster = FindObjectOfType<GameController> (); 

	}

	void OnGUI(){
		alpha += fadeDir * fadeSpeed * Time.deltaTime; 
		alpha = Mathf.Clamp01 (alpha); 

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth; 
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);

		if (alpha >= 1.0f && transitioning) {		// hopefully we never want the whole screen black...
			// right before changing the direction we should do a callback to move the camera and load/unload assets
			// the screen is completely hidden right now so it's the ideal time  
			gameMaster.loadRooms(); 
			fadeDir = -1; 
		}
	}

	public void setFadeSpeed(float speed){
		fadeSpeed = speed; 
	}

	public void toggleTransitioning(bool t){
		transitioning = t; 
	}

	public float beginFade (int direction){
		fadeDir = direction; 
		return (fadeSpeed); 
	}

	public float beginFadeTransition (){
		transitioning = true; 
		fadeDir = 1; 
		return (fadeSpeed); 
	}
		
		
}
