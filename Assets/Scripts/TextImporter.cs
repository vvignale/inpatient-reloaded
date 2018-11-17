using UnityEngine;
using System.Collections;

public class TextImporter : MonoBehaviour {

	public TextAsset textFile; 
	public string[] lines; 

	// Use this for initialization
	void Start () {

		if (textFile != null) {
			lines = textFile.text.Split ('\n'); 
		}
		print (lines); 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
