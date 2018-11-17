using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeRoomMusicController : MonoBehaviour {

	// only purpose is to start the music
	void OnEnable () {
		GameObject.Find ("EscapeMusic").GetComponent <AudioSource> ().Play ();
	}

}
