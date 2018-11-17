using UnityEngine;
using System.Collections;
using System.IO; 

public class FormTextController : MonoBehaviour {

	private char[] letters;
	private int index = 0; 
	private MeshRenderer rend;
	private PlayerController player; 
	private int exhaustionMax = 5;
	private int exhaustionCounter = 0; 
	private AudioSource audio;

	void Awake(){

		// updated to parse out of a script 
		StreamReader reader = new StreamReader("Assets/Resources/Text/testText.txt");	// parametrized based on part of game
		string contents = reader.ReadToEnd ();
		letters = contents.ToCharArray ();
		player = GameObject.FindObjectOfType<PlayerController> ();
		reader.Close ();
		audio = GetComponent <AudioSource> ();

	}

	void Start () {
		rend = GetComponent<MeshRenderer>();
		rend.sortingLayerName = "TextOverlay";
		rend.sortingOrder = 0;
	}

	public void updateText(MainLobbyController mlc){

		int newIndex = index;

		if (index >= letters.Length) {
			mlc.deactivateForm (); 
		} else {	// cannot have trailing spaces in our text 

			while (newIndex < letters.Length && char.IsWhiteSpace (letters[newIndex])) {
				newIndex += 1; 
			}
			// add everything from old index to new to the mesh. then set index to be the new index + 1, the next letter

			for (int i = index; i <= newIndex; i++) {
				gameObject.GetComponent<TextMesh> ().text += letters [i].ToString ();
				// play a sound as letters added
				// try to play with the pitch to make more interesting between .5 and 1.5
				audio.pitch = .6f + Random.Range (0f, 1f);
				audio.Play ();
			}
			index = newIndex + 1; 
			// add callback to reduce player energy
			if(exhaustionCounter >= exhaustionMax){
				exhaustionCounter = 0; 
				player.addInventory ("Energy: ", -1);
			}
			else{
				exhaustionCounter += 1; 
			}

		}

	}

}
