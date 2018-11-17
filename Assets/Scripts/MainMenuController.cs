using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class MainMenuController : MonoBehaviour {

	public GameObject firstRoom; 


	// Use this for initialization
	void Start () {
		// pause the player
		GameObject.FindObjectOfType<PlayerController>().pause(); 
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGame(){
		// start the game without worrying about pointless settings  
		GameObject.FindObjectOfType<GameController> ().transitionMainMenu (firstRoom);  
	}
		
}
