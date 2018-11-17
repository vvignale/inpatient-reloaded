using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Collider2D player; 

	private Vector3 bbSize = new Vector3(2f,2f,0f); 
	private Vector3 bbCenter; 
	private float roomLft = 0;
	private float roomRgt = 0;
	private float roomTop = 0;
	private float roomBottom = 0;
	private GameObject playerObj;

	private float height; 
	private float width; 

	// Use this for initialization
	void Start () {

		Camera cam = Camera.main; 
		height = 2f * cam.orthographicSize;
		width = height * cam.aspect; 
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		player = playerObj.GetComponent <Collider2D> (); 

		bbCenter = playerObj.transform.position;  

	}
		
	void OnDrawGizmos(){
		Gizmos.color = new Color (1, 0, 0, .5f);
		Gizmos.DrawCube (bbCenter, bbSize);
	}

	public void setRoomBounds(GameObject room){

		roomLft = room.transform.GetChild (0).GetComponent<BoxCollider2D>().bounds.min.x; 
		roomRgt = room.transform.GetChild (1).GetComponent<BoxCollider2D>().bounds.max.x;
		roomTop = room.transform.GetChild (2).GetComponent<BoxCollider2D>().bounds.max.y;
		roomBottom = room.transform.GetChild (3).GetComponent<BoxCollider2D>().bounds.min.y; 
	}

	public void setPlayerCollider(Collider2D p){
		player = p;
	}

	// Update is called once per frame
	void Update () {
		
		// only update if not player paused
		if (!playerObj.GetComponent<PlayerController> ().getPause ()) {

			Vector3 playerPos = player.bounds.center;

			// locks the camera position to always center the player:
			Vector3 diff = playerPos - transform.position; 
			transform.Translate (diff.x, diff.y, 0);


			// when the player touches the edge of the bounding box, move it along to match that 

			if (player.bounds.max.x >= bbCenter.x + bbSize.x / 2) {		//right condition
				bbCenter.x += player.bounds.max.x - (bbCenter.x + bbSize.x / 2); 
				//print ("right collision"); 
			}
			if (player.bounds.min.x <= bbCenter.x - bbSize.x / 2) {
				bbCenter.x += player.bounds.min.x - (bbCenter.x - bbSize.x / 2);
				//print ("left collision"); 
			}
			if (player.bounds.max.y >= bbCenter.y + bbSize.y / 2) {		//up
				bbCenter.y += player.bounds.max.y - (bbCenter.y + bbSize.y / 2); 
				//print ("up collision collision"); 
			}
			if (player.bounds.min.y <= bbCenter.y - bbSize.y / 2) {
				bbCenter.y += player.bounds.min.y - (bbCenter.y - bbSize.y / 2); 
				//print ("down collision"); 
			}

			float centerX = bbCenter.x;
			float centerY = bbCenter.y; 

			//TESTING WITHOUT BB: 
			//centerX = playerPos.x; 
			//centerY = playerPos.y;

			if (roomLft != 0 && roomRgt != 0) {

				// if bounds are set, correct for location if it's outside 

				//need: camera's own center
				// camera's own size

				// check whether the camera is falling outside of bounds and correct if so
			 

				if (centerX + (width / 2f) > roomRgt) {
					centerX = centerX - (centerX + (width / 2f) - roomRgt); 
				}
				if (centerX - (width / 2f) < roomLft) {
					centerX = centerX + roomLft - (centerX - (width / 2f));
				}
				if (centerY + (height / 2f) > roomTop) {
					centerY = centerY - (centerY + (height / 2f) - roomTop);
				}
				if (centerY - (height / 2f) < roomBottom) {
					centerY = centerY + roomBottom - (centerY - (height / 2f)); 
				}
			}
			transform.position = new Vector3 (centerX, centerY, transform.position.z);
		}
	}
}
