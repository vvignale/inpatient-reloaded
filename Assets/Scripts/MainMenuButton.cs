using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour {

	void OnMouseDown(){
		transform.root.gameObject.GetComponent<MainMenuController>().startGame(); 
	}
}
