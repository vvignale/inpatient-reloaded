using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;	// the unity ui panel 
	public Text text;			// the text that goes in the panel

	private int currLine;
	private int endLine; 
	private string[] lines;
	private PlayerController player; 

	private bool isActive; 
	private bool isTyping; 
	private bool cancelTyping; 
	private bool beginning = true; 

	private float typingSpeed;
	private float interval;
	private float timer; 

	private InteractionCollider interactionCollider; 

	// Use this for initialization
	void Start () {

		player = FindObjectOfType<PlayerController> (); 
		disableBox (); 	// should start out disabled 
		beginning = false; 

		isTyping = false; 
		cancelTyping = false; 
		typingSpeed = .01f;	// larger is slower	 
		interval = 0; 
	}
		
	// set amount of time between auto updates
	public void setInterval(float inter){
		interval = inter;
	}

	// Update is called once per frame
	void Update () {

		if (isActive) {


			if (interval!=0){
				timer += Time.deltaTime; 

				if(timer >= interval){
				
					if (!isTyping) {		// if not typing, can add to current line and set typing to true and start coroutine

						currLine += 1;
						if (currLine > endLine) {		// remove box when done displaying text 
							disableBox (); 
						} else {						// start coroutine 
							StartCoroutine (textScroll (lines [currLine])); 
						}
					} else if (isTyping && !cancelTyping) {					// if are typing, finish off the line 
						cancelTyping = true; 
					}

					timer = 0; 
				}
			}
			else{

				// instead of displaying a current line, we will be running a coroutine to show the letters 
				//text.text = lines [currLine]; 	// current text that is going to be displayed in the text box	
				if (Input.GetKeyDown (KeyCode.Return)) {

					if (!isTyping) {		// if not typing, can add to current line and set typing to true and start coroutine

						currLine += 1;
						if (currLine > endLine) {		// remove box when done displaying text 
							disableBox (); 
						} else {						// start coroutine 
							StartCoroutine (textScroll (lines [currLine])); 
						}
					} else if (isTyping && !cancelTyping) {					// if are typing, finish off the line 
						cancelTyping = true; 
					} 
				}
				
			}

		}
	}

	private IEnumerator textScroll(string lineOfText){

		int letter = 0; 
		text.text = ""; 
		isTyping = true; 
		cancelTyping = false;

		while (isTyping && !cancelTyping && (letter < lineOfText.Length)) {
			text.text += lineOfText [letter++]; 
			yield return new WaitForSeconds (typingSpeed); 	// pause after each letter
		}

		text.text = lineOfText; 		// this handles final case above, where is typing and have canceled typing
		isTyping = false;
		cancelTyping = false; 			// resets this after the scroll is done 
	}

	public void enableBox(TextAsset textFile, InteractionCollider curr){		// enable the box with a new text file 

		interactionCollider = curr; 
		lines = null; 	
		if (textFile != null) {
			lines = textFile.text.Split ('\n');  
		}
		endLine = lines.Length - 1;
		currLine = 0; 
		isActive = true; 
		player.pause (); 
		textBox.SetActive (true); 
		StartCoroutine(textScroll(lines[currLine])); 
	}

	public void disableBox(){
		lines = null; 
		isActive = false; 
		textBox.SetActive (false); 
		if (!beginning) {
			player.unpause ();
		}

		if (interactionCollider != null) {
			interactionCollider.enableInteraction (); 
		}

	}

}
